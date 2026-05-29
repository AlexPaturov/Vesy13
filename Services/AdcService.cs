using System.IO.Ports;
using Vesy13.Models;

namespace Vesy13.Services;

public enum ActiveChannel { Main, Backup }

public class AdcService : IDisposable
{
    public event EventHandler<AdcFrame>? FrameReceived;
    public event EventHandler<bool>?     ConnectionChanged;

    private SerialPort?              _port;
    private readonly List<byte>      _buffer = new();
    private readonly object          _lock   = new();
    private System.Threading.Timer?  _frameTimer;
    private volatile bool            _polling;
    private byte                     _pollByte = 214;

    public bool          IsConnected   { get; private set; }
    public string        PortName      { get; private set; } = "COM1";
    public ActiveChannel Channel       { get; set; } = ActiveChannel.Main;

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

        _frameTimer = new System.Threading.Timer(OnFrameTimer, null, Timeout.Infinite, Timeout.Infinite);
        _polling     = true;
        IsConnected  = true;
        Send(_pollByte);
        ConnectionChanged?.Invoke(this, true);
    }

    public void Close()
    {
        _polling = false;
        _frameTimer?.Dispose();
        _frameTimer = null;
        lock (_lock) _buffer.Clear();
        _port?.Close();
        _port?.Dispose();
        _port       = null;
        IsConnected = false;
        ConnectionChanged?.Invoke(this, false);
    }

    public void SetPollByte(byte b) => _pollByte = b;

    private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        if (_port == null || !_port.IsOpen) return;
        int n   = _port.BytesToRead;
        var buf = new byte[n];
        _port.Read(buf, 0, n);
        lock (_lock) _buffer.AddRange(buf);
        _frameTimer?.Change(20, Timeout.Infinite);
    }

    private void OnFrameTimer(object? state)
    {
        byte[] raw;
        lock (_lock)
        {
            if (_buffer.Count == 0) return;
            raw = _buffer.ToArray();
            _buffer.Clear();
        }

        var frame = AdcFrame.Parse(raw);
        if (frame.Valid)
            FrameReceived?.Invoke(this, frame);

        if (_polling)
            Send(_pollByte);
    }

    private void Send(byte b)
    {
        try { _port?.Write(new[] { b }, 0, 1); }
        catch { }
    }

    public void Dispose() => Close();
}