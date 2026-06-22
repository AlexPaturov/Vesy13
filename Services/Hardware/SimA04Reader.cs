using System.IO.Ports;
using Vesy13.Models;

namespace Vesy13.Services.Hardware;

/// <summary>
/// Активный канал АЦП: Main — основной (CH0), Backup — резервный (CH1).
/// </summary>
public enum ActiveChannel { Main, Backup }

/// <summary>
/// Драйвер COM-порта для АЦП «СИМ А04».
/// Реализует цикл поллинга: регулярно отправляет запросы и разбирает 4-байтовые ответы.
/// </summary>
public class SimA04Reader : IDisposable
{
    /// <summary>Распарсенный фрейм АЦП — CH0, CH1, флаг валидности.</summary>
    public event EventHandler<SimA04Frame>? FrameReceived;

    /// <summary>Сырые байты ответа — для отладочного мониторинга в ServiceForm.</summary>
    public event EventHandler<byte[]>?  RawFrameReceived;

    /// <summary>Изменение состояния соединения: true — подключён, false — отключён.</summary>
    public event EventHandler<bool>?    ConnectionChanged;

    private const int ConnectionTimeoutMs = 1000;

    private SerialPort?              _port;
    private readonly List<byte>      _buffer = new();
    private readonly object          _lock   = new();
    private System.Threading.Timer?  _frameTimer;
    private System.Threading.Timer?  _pollTimer;
    private volatile bool            _polling;
    private DateTime                 _lastValidFrameAt = DateTime.MinValue;
    private byte                     _pollByte = 214;

    /// <summary>COM-порт открыт программой.</summary>
    public bool          IsPortOpen    { get; private set; }

    /// <summary>Контроллер АЦП отвечает валидными кадрами.</summary>
    public bool          IsConnected   { get; private set; }

    /// <summary>Имя COM-порта, переданного в <see cref="Open"/>.</summary>
    public string        PortName      { get; private set; } = "COM1";

    /// <summary>Активный канал: Main → CH0, Backup → CH1.</summary>
    public ActiveChannel Channel       { get; set; } = ActiveChannel.Main;

    /// <summary>
    /// Открывает COM-порт, запускает цикл поллинга и генерирует <see cref="ConnectionChanged"/>.
    /// Параметры порта фиксированы протоколом АЦП: 4800 бод, чётность Even, 8 бит, 1 стоп-бит.
    /// </summary>
    public void Open(string portName = "COM1")
    {
        PortName = portName;
        _port = new SerialPort(portName, 4800, Parity.Even, 8, StopBits.One)
        {
            ReadTimeout  = SerialPort.InfiniteTimeout,
            WriteTimeout = 500,
        };
        _port.DataReceived += OnDataReceived;
        _port.Open();
        _port.DiscardInBuffer();
        _port.DiscardOutBuffer();

        _frameTimer = new System.Threading.Timer(OnFrameTimer, null, Timeout.Infinite, Timeout.Infinite);
        _polling     = true;
        IsPortOpen   = true;
        SetConnected(false, force: true);
        _pollTimer   = new System.Threading.Timer(OnPollTimer, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(200));
    }

    /// <summary>
    /// Останавливает поллинг, закрывает и освобождает COM-порт.
    /// </summary>
    public void Close()
    {
        _polling = false;
        _frameTimer?.Dispose();
        _frameTimer = null;
        _pollTimer?.Dispose();
        _pollTimer = null;
        lock (_lock) _buffer.Clear();
        _port?.Close();
        _port?.Dispose();
        _port       = null;
        IsPortOpen = false;
        SetConnected(false, force: true);
    }

    /// <summary>
    /// Задаёт байт запроса к АЦП. По умолчанию 214 — Static, CH0+CH1, Avg=256.
    /// </summary>
    public void SetPollByte(byte b) => _pollByte = b;

    /// <summary>
    /// Накапливает байты от COM-порта в буфер и взводит таймер на 20 мс.
    /// Таймер нужен, чтобы собрать полный 4-байтовый ответ если DataReceived
    /// сработал несколько раз на один пакет.
    /// </summary>
    private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        if (_port == null || !_port.IsOpen) return;
        int n   = _port.BytesToRead;
        var buf = new byte[n];
        _port.Read(buf, 0, n);
        lock (_lock) _buffer.AddRange(buf);
        _frameTimer?.Change(20, Timeout.Infinite);
    }

    /// <summary>
    /// Срабатывает через 20 мс после последнего байта: разбирает буфер
    /// и публикует события с сырым и распарсенным фреймом.
    /// </summary>
    private void OnFrameTimer(object? state)
    {
        byte[] raw;
        lock (_lock)
        {
            if (_buffer.Count == 0) return;
            raw = _buffer.ToArray();
            _buffer.Clear();
        }

        RawFrameReceived?.Invoke(this, raw);

        var frame = SimA04Frame.Parse(raw);
        if (frame.Valid)
        {
            _lastValidFrameAt = DateTime.UtcNow;
            SetConnected(true);
            FrameReceived?.Invoke(this, frame);
        }
    }

    private void OnPollTimer(object? state)
    {
        if (!_polling) return;

        if (IsConnected && (DateTime.UtcNow - _lastValidFrameAt).TotalMilliseconds > ConnectionTimeoutMs)
            SetConnected(false);

        Send(_pollByte);
    }

    private void SetConnected(bool connected, bool force = false)
    {
        if (!force && IsConnected == connected) return;
        IsConnected = connected;
        ConnectionChanged?.Invoke(this, connected);
    }

    /// <summary>Отправляет один байт в COM-порт. Ошибки записи не критичны — следующий цикл повторит.</summary>
    private void Send(byte b)
    {
        try { _port?.Write(new[] { b }, 0, 1); }
        catch { }
    }

    /// <inheritdoc/>
    public void Dispose() => Close();
}
