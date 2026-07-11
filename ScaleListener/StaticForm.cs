using System.IO.Ports;
using ScaleListener.FaultInjection;

namespace ScaleListener;

public partial class StaticForm : Form
{
    private const decimal MaxWeightTonnes = 140m;
    private const int MaxAdcCode = 65535;

    // ── Serial ────────────────────────────────────────────────────────────
    private readonly SerialPort _port;
    private readonly Random _rng = new();
    private readonly FaultEngine _faultEngine = new();
    private StaticFaultForm? _faultForm;
    private DateTime _driftStartedAt;
    private int _stuckCode;
    private volatile bool _isShuttingDown;

    public StaticForm()
    {
        InitializeComponent();

        _port = new SerialPort("COM4", 4800, Parity.Even, 8, StopBits.One)
        {
            ReadTimeout = SerialPort.InfiniteTimeout,
            WriteTimeout = 500,
        };
        _port.DataReceived += Port_DataReceived;

        _faultEngine.WindowChanged += OnFaultWindowChanged;

        UpdateCodePreview();
    }

    private void NumWeight_ValueChanged(object? sender, EventArgs e) => UpdateCodePreview();
    private void NumTolerance_ValueChanged(object? sender, EventArgs e) => UpdateCodePreview();
    private void BtnClear_Click(object? sender, EventArgs e) => _log.Clear();
    private void BtnFaults_Click(object? sender, EventArgs e) => OpenFaultForm();

    private void OpenFaultForm()
    {
        if (_faultForm is { IsDisposed: false })
        {
            _faultForm.Activate();
            return;
        }

        _faultForm = new StaticFaultForm(_faultEngine) { Owner = this };
        _faultForm.Show();
    }

    private void OnFaultWindowChanged(FaultType type, bool active)
    {
        if (InvokeRequired) { BeginInvoke(() => OnFaultWindowChanged(type, active)); return; }

        int currentCode = WeightToAdcCode(_numWeight.Value);
        string correct = $"{_numWeight.Value:F2} т (код {currentCode})";

        switch (type)
        {
            case FaultType.Drift:
                if (active) _driftStartedAt = DateTime.UtcNow;
                _faultEngine.AppendHistory(type, active ? FaultEventKind.Started : FaultEventKind.Stopped,
                    correct, active ? "дрейф включён" : "дрейф выключен");
                break;
            case FaultType.Stuck:
                if (active) _stuckCode = currentCode;
                _faultEngine.AppendHistory(type, active ? FaultEventKind.Started : FaultEventKind.Stopped,
                    correct, active ? $"застрял на коде {_stuckCode}" : "отпущен");
                break;
            case FaultType.Silence:
                _faultEngine.AppendHistory(type, active ? FaultEventKind.Started : FaultEventKind.Stopped,
                    correct, active ? "нет ответа на poll" : "ответ возобновлён");
                break;
        }
    }

    private void BtnConnect_Click(object? sender, EventArgs e)
    {
        if (_port.IsOpen)
        {
            _port.Close();
            _btnConnect.Text = "Connect";
            _log.BackColor = Color.LightGray;
            AppendMsg(">>>> OFFLINE", Color.Gray);
        }
        else
        {
            try
            {
                _port.Open();
                _btnConnect.Text = "Disconnect";
                _log.BackColor = Color.White;
                AppendMsg(">>>> ONLINE  COM4  4800/Even", Color.Green);
            }
            catch (Exception ex)
            {
                AppendMsg($">>>> ERROR: {ex.Message}", Color.Red);
            }
        }
    }

    private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            if (_isShuttingDown || !_port.IsOpen)
                return;

            int bytesToRead = _port.BytesToRead;
            if (bytesToRead <= 0)
                return;

            var buf = new byte[bytesToRead];
            _port.Read(buf, 0, bytesToRead);

            BeginInvoke(() => HandleIncomingBytes(buf));
        }
        catch { /* порт закрылся во время чтения */ }
    }

    private void HandleIncomingBytes(byte[] buf)
    {
        if (_isShuttingDown || !_port.IsOpen)
            return;

        foreach (byte b in buf)
        {
            AppendMsg($"<< {b}");
            if (b == 214)
                SendStaticResponse();
        }
    }

    private void SendStaticResponse()
    {
        _faultEngine.Pump();

        if (_faultEngine.IsWindowActive(FaultType.Silence))
            return; // эмулятор "молчит" - не отвечает на poll вообще

        decimal weight = _numWeight.Value;
        decimal tolerance = _numTolerance.Value;
        decimal ch0Weight = ApplyTolerance(weight, tolerance);
        decimal ch1Weight = ApplyTolerance(weight, tolerance);

        if (_faultEngine.IsWindowActive(FaultType.Drift))
        {
            double t = (DateTime.UtcNow - _driftStartedAt).TotalSeconds;
            decimal amplitude = (decimal)_faultEngine.Get(FaultType.Drift).Magnitude;
            decimal offset = (decimal)Math.Sin(t * Math.PI) * amplitude;
            ch0Weight = Math.Clamp(ch0Weight + offset, 0, MaxWeightTonnes);
            ch1Weight = Math.Clamp(ch1Weight + offset, 0, MaxWeightTonnes);
        }

        int ch0 = WeightToAdcCode(ch0Weight);
        int ch1 = WeightToAdcCode(ch1Weight);

        if (_faultEngine.IsWindowActive(FaultType.Stuck))
        {
            ch0 = _stuckCode;
            ch1 = _stuckCode;
        }

        if (_faultEngine.ShouldFireDiscrete(FaultType.Spike))
        {
            int correctCh0 = ch0, correctCh1 = ch1;
            int spikeAdc = (int)((decimal)_faultEngine.Get(FaultType.Spike).Magnitude / MaxWeightTonnes * MaxAdcCode);
            ch0 = Math.Clamp(ch0 + spikeAdc, 0, MaxAdcCode);
            ch1 = Math.Clamp(ch1 + spikeAdc, 0, MaxAdcCode);
            _faultEngine.AppendHistory(FaultType.Spike, FaultEventKind.Fired,
                $"CH0={correctCh0} CH1={correctCh1}", $"CH0={ch0} CH1={ch1}");
        }

        byte[] buf = BuildFrame(ch0, ch1);

        if (_faultEngine.ShouldFireDiscrete(FaultType.Corrupt))
        {
            string correctBytes = string.Join(" ", buf);
            int garbageBytes = Math.Max(1, (int)_faultEngine.Get(FaultType.Corrupt).Magnitude);
            for (int i = 0; i < garbageBytes; i++)
                buf[_rng.Next(buf.Length)] = (byte)_rng.Next(256);
            _faultEngine.AppendHistory(FaultType.Corrupt, FaultEventKind.Fired, correctBytes, string.Join(" ", buf));
        }

        try
        {
            _port.Write(buf, 0, buf.Length);
            AppendMsg($">> {string.Join(" ", buf)}  CH0={ch0} ({ch0Weight:F2} т)  CH1={ch1} ({ch1Weight:F2} т)", Color.DodgerBlue);
        }
        catch (Exception ex)
        {
            AppendMsg($">>>> SEND ERROR: {ex.Message}", Color.Red);
        }
    }

    private decimal ApplyTolerance(decimal weight, decimal tolerance)
    {
        if (tolerance <= 0) return weight;

        decimal delta = (decimal)(_rng.NextDouble() * 2.0 - 1.0) * tolerance;
        return Math.Clamp(weight + delta, 0, MaxWeightTonnes);
    }

    private static int WeightToAdcCode(decimal weight)
    {
        decimal clamped = Math.Clamp(weight, 0, MaxWeightTonnes);
        return (int)Math.Round(clamped / MaxWeightTonnes * MaxAdcCode, MidpointRounding.AwayFromZero);
    }

    private static byte[] BuildFrame(int ch0, int ch1)
    {
        ch0 = Math.Clamp(ch0, 0, MaxAdcCode);
        ch1 = Math.Clamp(ch1, 0, MaxAdcCode);
        return new[]
        {
            (byte)(ch0 & 0xFF),
            (byte)((ch0 >> 8) & 0xFF),
            (byte)(ch1 & 0xFF),
            (byte)((ch1 >> 8) & 0xFF),
        };
    }

    private void UpdateCodePreview()
    {
        int code = WeightToAdcCode(_numWeight.Value);
        byte[] buf = BuildFrame(code, code);
        _lblCode.Text = $"ADC={code}  bytes={string.Join(" ", buf)}";
    }

    private void AppendMsg(string text, Color? color = null)
    {
        var prev = _log.SelectionColor;
        _log.SelectionColor = color ?? prev;
        _log.AppendText(text + "\r\n");
        _log.SelectionColor = prev;
        _log.ScrollToCaret();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _isShuttingDown = true;
        _port.DataReceived -= Port_DataReceived;
        if (_port.IsOpen) _port.Close();
        _faultForm?.Close();
        base.OnFormClosing(e);
    }
}
