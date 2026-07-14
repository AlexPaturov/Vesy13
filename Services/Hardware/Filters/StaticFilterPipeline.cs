using Vesy13.Models;
using Vesy13.Services.Configuration;
using Vesy13.Services.Repositories;

namespace Vesy13.Services.Hardware.Filters;

/// <summary>
/// Фильтр входного потока статики. Стоит между <see cref="SimA04ReaderStatic"/> и формой:
/// подписывается на кадры драйвера, прогоняет их через клэмп → дельта → EMA и публикует
/// только те, что фильтры пропустили. Отбракованный кадр не публикуется вообще — если
/// сбой длится дольше ConnectionTimeoutMs, watchdog драйвера сам переведёт связь в
/// «нет валидного потока» и формы заблокируют взвешивание.
///
/// Решение об отбраковке принимается по активному каналу (в вес идёт только он,
/// см. StaticWeighingForm.ActiveCode). Срабатывания на неактивном канале логируются,
/// но кадр не отбраковывают.
///
/// Детектора застревания здесь нет намеренно: при взвешивании с расцепкой под башмак
/// вагон законно стоит неподвижно, и постоянный код — признак нормального процесса,
/// а не неисправности. Подробности — docs/hardware.md.
///
/// Логика намеренно продублирована с <see cref="DynamicFilterPipeline"/>: статика и
/// динамика развязаны, общего кода фильтров между ними нет.
/// </summary>
public sealed class StaticFilterPipeline : IDisposable
{
    private const string ObjectType = "FilterStatic";

    private readonly SimA04ReaderStatic _reader;
    private readonly AppSettings _settings;

    /// <summary>Кадр, прошедший все включённые фильтры. Коды могут быть сглажены EMA.</summary>
    public event EventHandler<SimA04Frame>? FilteredFrameReceived;

    public long ClampDropped { get; private set; }
    public long DeltaDropped { get; private set; }
    public long Passed { get; private set; }

    // Состояние фильтров — своё на каждый канал.
    private int? _prevCh0;
    private int? _prevCh1;
    private double? _emaCh0;
    private double? _emaCh1;

    public StaticFilterPipeline(SimA04ReaderStatic reader, AppSettings settings)
    {
        _reader = reader;
        _settings = settings;
        _reader.FrameReceived += OnFrame;

        if (_settings.StaticEmaEnabled)
            AuditLogger.Action(AuditLogger.FilterEma, ObjectType, $"alpha={_settings.StaticEmaAlpha:F2}");
    }

    private void OnFrame(object? sender, SimA04Frame frame)
    {
        if (!frame.Valid) return;

        bool ch0Dropped = !Accept(frame.Ch0, "CH0", ref _prevCh0);
        bool ch1Dropped = !Accept(frame.Ch1, "CH1", ref _prevCh1);

        bool activeDropped = _reader.Channel == ActiveChannel.Main ? ch0Dropped : ch1Dropped;
        if (activeDropped) return;

        int ch0 = ch0Dropped ? frame.Ch0 : Smooth(frame.Ch0, ref _emaCh0);
        int ch1 = ch1Dropped ? frame.Ch1 : Smooth(frame.Ch1, ref _emaCh1);

        Passed++;
        FilteredFrameReceived?.Invoke(this, SimA04Frame.FromCodes(ch0, ch1));
    }

    /// <summary>
    /// Прогоняет код одного канала через клэмп и дельта-фильтр. Возвращает false, если код
    /// отбракован. Предыдущее значение обновляется только принятым кодом, иначе один выброс
    /// стал бы точкой отсчёта для следующей дельты.
    /// </summary>
    private bool Accept(int code, string channel, ref int? previous)
    {
        if (_settings.StaticClampEnabled &&
            (code < _settings.StaticClampMinCode || code > _settings.StaticClampMaxCode))
        {
            ClampDropped++;
            AuditLogger.Action(AuditLogger.FilterClamp, ObjectType,
                $"{channel} code={code} range=[{_settings.StaticClampMinCode}..{_settings.StaticClampMaxCode}]");
            return false;
        }

        if (_settings.StaticDeltaEnabled && previous is { } prev)
        {
            int delta = Math.Abs(code - prev);
            if (delta > _settings.StaticDeltaMaxCodes)
            {
                DeltaDropped++;
                AuditLogger.Action(AuditLogger.FilterDelta, ObjectType,
                    $"{channel} code={code} prev={prev} delta={delta} max={_settings.StaticDeltaMaxCodes}");
                return false;
            }
        }

        previous = code;
        return true;
    }

    private int Smooth(int code, ref double? ema)
    {
        if (!_settings.StaticEmaEnabled)
        {
            ema = null;
            return code;
        }

        double alpha = _settings.StaticEmaAlpha;
        ema = ema is { } previous ? alpha * code + (1 - alpha) * previous : code;
        return (int)Math.Round(ema.Value);
    }

    public void Dispose() => _reader.FrameReceived -= OnFrame;
}
