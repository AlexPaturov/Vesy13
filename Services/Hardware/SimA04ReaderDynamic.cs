using System.IO.Ports;
using Vesy13.Models;

namespace Vesy13.Services.Hardware;

/// <summary>
/// Драйвер динамического потока АЦП "СИМ А04".
/// Открывает поток командой Set=126, Req=254 и собирает 5-байтовые сэмплы.
/// </summary>
public class SimA04ReaderDynamic : IDisposable
{
    private const byte SetByte = 126;
    private const byte ReqByte = 254;
    private const int SampleSize = 5;
    private const int ReconnectIntervalMs = 5000;

    public event EventHandler<SimA04DynamicSample>? SampleReceived;
    public event EventHandler<byte[]>? RawSampleReceived;
    public event EventHandler<bool>? ConnectionChanged;

    /// <summary>
    /// Пятибайтовое окно не сошлось по контрольной сумме aux и отброшено (дальше идёт ресинк
    /// сдвигом на байт). Единственный контроль целостности, который даёт протокол динамики.
    /// </summary>
    public event EventHandler<byte[]>? ChecksumRejected;

    public int ConnectionTimeoutMs { get; set; } = 5000;

    private SerialPort? _port;
    private readonly byte[] _sample = new byte[SampleSize];
    private readonly object _lock = new();
    private System.Threading.Timer? _watchdogTimer;
    private volatile bool _streaming;
    private int _sampleBytes;
    private DateTime _lastValidSampleAt = DateTime.MinValue;
    private DateTime _lastReconnectAttemptAt = DateTime.MinValue;
    private int _reconnectInProgress;

    public bool IsPortOpen { get; private set; }
    public bool IsConnected { get; private set; }

    /// <summary>
    /// true, если последнее закрытие порта пришлось форсировать (устройство не отвечало
    /// дольше штатного таймаута) — SerialPort в этом случае мог остаться в неопределённом
    /// внутреннем состоянии, этот экземпляр дальше переиспользовать для Open() не стоит.
    /// </summary>
    public bool IsPoisoned { get; private set; }

    public string PortName { get; private set; } = "COM1";
    public ActiveChannel Channel { get; set; } = ActiveChannel.Main;
    public long RawBytesReceived { get; private set; }
    public long SkippedBytes { get; private set; }
    public long SamplesReceived { get; private set; }

    public void Open(string portName = "COM1")
    {
        PortName = portName;
        IsPoisoned = false;
        _port = new SerialPort(portName, 4800, Parity.Even, 8, StopBits.One)
        {
            ReadTimeout = SerialPort.InfiniteTimeout,
            WriteTimeout = 500,
        };
        _port.DataReceived += OnDataReceived;
        _port.Open();
        _port.DiscardInBuffer();
        _port.DiscardOutBuffer();

        lock (_lock)
        {
            _sampleBytes = 0;
            RawBytesReceived = 0;
            SkippedBytes = 0;
            SamplesReceived = 0;
        }

        IsPortOpen = true;
        _streaming = true;
        SetConnected(false, force: true);
        _watchdogTimer = new System.Threading.Timer(OnWatchdogTimer, null, TimeSpan.FromMilliseconds(200), TimeSpan.FromMilliseconds(200));
        RequestStream();
    }

    public void Close()
    {
        _streaming = false;
        _watchdogTimer?.Dispose();
        _watchdogTimer = null;
        lock (_lock) _sampleBytes = 0;

        if (_port is not null)
        {
            _port.DataReceived -= OnDataReceived;
            IsPoisoned = !SerialPortForceCloser.CloseWithTimeout(_port);
        }
        _port = null;

        IsPortOpen = false;
        SetConnected(false, force: true);
    }

    private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        if (_port == null || !_port.IsOpen) return;

        var count = _port.BytesToRead;
        if (count <= 0) return;

        var buffer = new byte[count];
        _port.Read(buffer, 0, count);

        foreach (var value in buffer)
            ProcessByte(value);
    }

    private void ProcessByte(byte value)
    {
        byte[]? raw = null;
        byte[]? rejected = null;
        SimA04DynamicSample sample = default;

        lock (_lock)
        {
            _sample[_sampleBytes] = value;
            _sampleBytes++;
            RawBytesReceived++;

            if (_sampleBytes < SampleSize)
                return;

            raw = _sample.ToArray();
            sample = SimA04DynamicSample.Parse(raw);
            if (!sample.Valid)
            {
                rejected = raw;
                ShiftLeft();
                SkippedBytes++;
            }
            else
            {
                _sampleBytes = 0;
                SamplesReceived++;
                _lastValidSampleAt = DateTime.UtcNow;
            }
        }

        if (rejected is not null)
        {
            ChecksumRejected?.Invoke(this, rejected);
            return;
        }

        SetConnected(true);
        RawSampleReceived?.Invoke(this, raw);
        SampleReceived?.Invoke(this, sample);
    }

    private void ShiftLeft()
    {
        for (var index = 1; index < _sampleBytes; index++)
            _sample[index - 1] = _sample[index];

        _sampleBytes--;
    }

    private void OnWatchdogTimer(object? state)
    {
        if (!_streaming) return;

        var now = DateTime.UtcNow;
        if (IsConnected && (now - _lastValidSampleAt).TotalMilliseconds > ConnectionTimeoutMs)
            SetConnected(false);

        if (!IsConnected && (now - _lastReconnectAttemptAt).TotalMilliseconds >= ReconnectIntervalMs)
            RequestStream();
    }

    private void RequestStream()
    {
        if (Interlocked.Exchange(ref _reconnectInProgress, 1) != 0) return;

        try
        {
            _lastReconnectAttemptAt = DateTime.UtcNow;
            Send(SetByte);
            Thread.Sleep(50);
            if (_streaming) Send(ReqByte);
        }
        finally
        {
            Volatile.Write(ref _reconnectInProgress, 0);
        }
    }

    private void SetConnected(bool connected, bool force = false)
    {
        if (!force && IsConnected == connected) return;
        IsConnected = connected;
        ConnectionChanged?.Invoke(this, connected);
    }

    private void Send(byte value)
    {
        try { _port?.Write(new[] { value }, 0, 1); }
        catch { }
    }

    public void Dispose() => Close();
}
