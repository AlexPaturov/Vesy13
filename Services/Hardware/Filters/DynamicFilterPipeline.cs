using Vesy13.Models;
using Vesy13.Services.Configuration;
using Vesy13.Services.Repositories;

namespace Vesy13.Services.Hardware.Filters;

/// <summary>
/// Фильтр входного потока динамики. Стоит между <see cref="SimA04ReaderDynamic"/> и формой:
/// подписывается на сэмплы драйвера, прогоняет их через клэмп → застревание → дельта → EMA
/// и публикует только те, что фильтры пропустили. Отбракованный сэмпл не публикуется вообще —
/// если сбой длится дольше ConnectionTimeoutMs, watchdog драйвера сам переведёт связь в
/// «нет валидного потока» и форма заблокирует взвешивание.
///
/// Решение об отбраковке принимается по активному каналу (в вес идёт только он,
/// см. DynamicWeighingForm.ActiveCode). Срабатывания на неактивном канале логируются,
/// но сэмпл не отбраковывают.
///
/// Порядок фильтров не косметический: застревание проверяется до дельта-фильтра, потому что
/// у застрявшего кода дельта нулевая и дельта-фильтр принял бы его за идеально стабильный.
///
/// Логика намеренно продублирована с <see cref="StaticFilterPipeline"/>: статика и динамика
/// развязаны, общего кода фильтров между ними нет.
/// </summary>
public sealed class DynamicFilterPipeline : IDisposable
{
    private const string ObjectType = "FilterDynamic";

    private readonly SimA04ReaderDynamic _reader;
    private readonly AppSettings _settings;

    /// <summary>Сэмпл, прошедший все включённые фильтры. Коды могут быть сглажены EMA.</summary>
    public event EventHandler<SimA04DynamicSample>? FilteredSampleReceived;

    public long ClampDropped { get; private set; }
    public long StuckDropped { get; private set; }
    public long DeltaDropped { get; private set; }
    public long ChecksumRejected { get; private set; }
    public long Passed { get; private set; }

    // Состояние фильтров — своё на каждый канал.
    private int? _prevCh0;
    private int? _prevCh1;
    private double? _emaCh0;
    private double? _emaCh1;
    private int? _stuckCodeCh0;
    private int? _stuckCodeCh1;
    private int _stuckCountCh0;
    private int _stuckCountCh1;

    public DynamicFilterPipeline(SimA04ReaderDynamic reader, AppSettings settings)
    {
        _reader = reader;
        _settings = settings;
        _reader.SampleReceived += OnSample;
        _reader.ChecksumRejected += OnChecksumRejected;

        if (_settings.DynamicEmaEnabled)
            AuditLogger.Action(AuditLogger.FilterEma, ObjectType, $"alpha={_settings.DynamicEmaAlpha:F2}");
    }

    private void OnChecksumRejected(object? sender, byte[] raw)
    {
        ChecksumRejected++;
        AuditLogger.Action(AuditLogger.FilterChecksum, ObjectType, $"bytes={string.Join(" ", raw)}");
    }

    private void OnSample(object? sender, SimA04DynamicSample sample)
    {
        if (!sample.Valid) return;

        bool ch0Dropped = !Accept(sample.Ch0, "CH0", ref _prevCh0, ref _stuckCodeCh0, ref _stuckCountCh0);
        bool ch1Dropped = !Accept(sample.Ch1, "CH1", ref _prevCh1, ref _stuckCodeCh1, ref _stuckCountCh1);

        bool activeDropped = _reader.Channel == ActiveChannel.Main ? ch0Dropped : ch1Dropped;
        if (activeDropped) return;

        int ch0 = ch0Dropped ? sample.Ch0 : Smooth(sample.Ch0, ref _emaCh0);
        int ch1 = ch1Dropped ? sample.Ch1 : Smooth(sample.Ch1, ref _emaCh1);

        Passed++;
        FilteredSampleReceived?.Invoke(this, SimA04DynamicSample.FromCodes(ch0, ch1, sample.Aux));
    }

    /// <summary>
    /// Прогоняет код одного канала через клэмп, детектор застревания и дельта-фильтр.
    /// Возвращает false, если код отбракован. Предыдущее значение обновляется только принятым
    /// кодом, иначе один выброс стал бы точкой отсчёта для следующей дельты.
    /// </summary>
    private bool Accept(int code, string channel, ref int? previous, ref int? stuckCode, ref int stuckCount)
    {
        if (_settings.DynamicClampEnabled &&
            (code < _settings.DynamicClampMinCode || code > _settings.DynamicClampMaxCode))
        {
            ClampDropped++;
            AuditLogger.Action(AuditLogger.FilterClamp, ObjectType,
                $"{channel} code={code} range=[{_settings.DynamicClampMinCode}..{_settings.DynamicClampMaxCode}]");
            return false;
        }

        if (!IsAlive(code, channel, ref stuckCode, ref stuckCount))
            return false;

        if (_settings.DynamicDeltaEnabled && previous is { } prev)
        {
            int delta = Math.Abs(code - prev);
            if (delta > _settings.DynamicDeltaMaxCodes)
            {
                DeltaDropped++;
                AuditLogger.Action(AuditLogger.FilterDelta, ObjectType,
                    $"{channel} code={code} prev={prev} delta={delta} max={_settings.DynamicDeltaMaxCodes}");
                return false;
            }
        }

        previous = code;
        return true;
    }

    /// <summary>
    /// Считает строго равные коды подряд. Живой датчик всегда шумит на единицы младшего
    /// разряда, поэтому длинная серия бит-в-бит одинаковых кодов означает застрявший канал.
    /// Как только код изменился, счётчик сбрасывается и поток восстанавливается сам.
    /// </summary>
    private bool IsAlive(int code, string channel, ref int? stuckCode, ref int stuckCount)
    {
        if (!_settings.DynamicStuckEnabled)
        {
            stuckCode = null;
            stuckCount = 0;
            return true;
        }

        if (stuckCode == code)
            stuckCount++;
        else
        {
            stuckCode = code;
            stuckCount = 1;
        }

        if (stuckCount < _settings.DynamicStuckSamples)
            return true;

        StuckDropped++;
        AuditLogger.Action(AuditLogger.FilterStuck, ObjectType,
            $"{channel} code={code} samples={stuckCount} threshold={_settings.DynamicStuckSamples}");
        return false;
    }

    private int Smooth(int code, ref double? ema)
    {
        if (!_settings.DynamicEmaEnabled)
        {
            ema = null;
            return code;
        }

        double alpha = _settings.DynamicEmaAlpha;
        ema = ema is { } previous ? alpha * code + (1 - alpha) * previous : code;
        return (int)Math.Round(ema.Value);
    }

    public void Dispose()
    {
        _reader.SampleReceived -= OnSample;
        _reader.ChecksumRejected -= OnChecksumRejected;
    }
}
