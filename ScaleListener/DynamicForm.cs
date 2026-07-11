using System.IO.Ports;
using ScaleListener.FaultInjection;

namespace ScaleListener;

public partial class DynamicForm : Form
{
    private const decimal MaxWeightTonnes = 140m;
    private const int MaxAdcCode = 65535;
    private const byte SetByte = 126;
    private const byte ReqByte = 254;
    private const int AuxOffset = 28;
    private const int DefaultHz = 47;
    private const int MaxLogLines = 500;

    private readonly SerialPort _port;
    private readonly Random _rng = new();
    private readonly FaultEngine _faultEngine = new();
    private DynamicFaultForm? _faultForm;
    private DateTime _driftStartedAt;
    private int _stuckCode;
    private volatile bool _isShuttingDown;
    private bool _streaming;
    private int _sampleIndex;
    private int _logLineCount;

    public DynamicForm()
    {
        InitializeComponent();

        _port = new SerialPort("COM4", 4800, Parity.Even, 8, StopBits.One)
        {
            ReadTimeout = SerialPort.InfiniteTimeout,
            WriteTimeout = 500,
        };
        _port.DataReceived += Port_DataReceived;

        _streamTimer.Interval = HzToIntervalMs((int)_numHz.Value);

        _faultEngine.WindowChanged += OnFaultWindowChanged;

        UpdateStateLabel();
        UpdateCodePreview();
    }

    private void NumWeight_ValueChanged(object? sender, EventArgs e) => UpdateCodePreview();
    private void NumTolerance_ValueChanged(object? sender, EventArgs e) => UpdateCodePreview();
    private void NumHz_ValueChanged(object? sender, EventArgs e) => UpdateStreamInterval();
    private void CmbScenario_SelectedIndexChanged(object? sender, EventArgs e) => UpdateCodePreview();
    private void CmbPacketLog_SelectedIndexChanged(object? sender, EventArgs e) => UpdateStateLabel();
    private void BtnStopStream_Click(object? sender, EventArgs e) => StopStream("manual");
    private void BtnClear_Click(object? sender, EventArgs e) => ClearLog();
    private void BtnFaults_Click(object? sender, EventArgs e) => OpenFaultForm();
    private void StreamTimer_Tick(object? sender, EventArgs e) => SendDynamicResponse();

    private void OpenFaultForm()
    {
        if (_faultForm is { IsDisposed: false })
        {
            _faultForm.Activate();
            return;
        }

        _faultForm = new DynamicFaultForm(_faultEngine) { Owner = this };
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
                    correct, active ? "нет ответа" : "ответ возобновлён");
                break;
        }
    }

    private void BtnConnect_Click(object? sender, EventArgs e)
    {
        if (_port.IsOpen)
        {
            StopStream("disconnect");
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
        catch { }
    }

    private void HandleIncomingBytes(byte[] buf)
    {
        if (_isShuttingDown || !_port.IsOpen)
            return;

        foreach (byte b in buf)
        {
            AppendMsg($"<< {b}");
            if (b == SetByte)
            {
                StopStream("set");
                AppendMsg(">>>> DYNAMIC SET", Color.DarkOrange);
            }
            else if (b == ReqByte)
            {
                StartStream();
            }
        }
    }

    private void StartStream()
    {
        if (_streaming)
            return;

        _sampleIndex = 0;
        _streaming = true;
        UpdateStreamInterval();
        _streamTimer.Start();
        _btnStopStream.Enabled = true;
        UpdateStateLabel();
        AppendMsg($">>>> STREAM START  hz={(int)_numHz.Value}  interval={_streamTimer.Interval}ms  scenario={_cmbScenario.Text}", Color.Green);
    }

    private void StopStream(string reason)
    {
        if (!_streaming)
            return;

        _streamTimer.Stop();
        _streaming = false;
        _btnStopStream.Enabled = false;
        UpdateStateLabel();
        AppendMsg($">>>> STREAM STOP  reason={reason}", Color.Gray);
    }

    private void SendDynamicResponse()
    {
        if (_isShuttingDown || !_port.IsOpen)
            return;

        _faultEngine.Pump();

        if (_faultEngine.IsWindowActive(FaultType.Silence))
        {
            _sampleIndex++;
            return; // эмулятор "молчит" - не отвечает вообще, как реальный оборвавшийся АЦП
        }

        decimal baseWeight = GetScenarioWeight();
        decimal tolerance = _numTolerance.Value;
        decimal ch0Weight = ApplyTolerance(baseWeight, tolerance);
        decimal ch1Weight = ApplyTolerance(baseWeight, tolerance);

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

        byte[] buf = BuildSample(ch0, ch1);

        if (_cmbScenario.Text == "Битые сэмплы" && _sampleIndex > 0 && _sampleIndex % 50 == 0)
            buf[4]++;

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
            _sampleIndex++;
            if (_sampleIndex % PacketLogStep == 0)
                AppendMsg($">> {string.Join(" ", buf)}  CH0={ch0} ({ch0Weight:F2} т)  CH1={ch1} ({ch1Weight:F2} т)", Color.DodgerBlue);
        }
        catch (Exception ex)
        {
            StopStream("send-error");
            AppendMsg($">>>> SEND ERROR: {ex.Message}", Color.Red);
        }
    }

    private decimal GetScenarioWeight()
    {
        decimal weight = _numWeight.Value;
        if (_cmbScenario.Text != "Две тележки")
            return weight;

        var hz = Math.Max(1, (int)_numHz.Value);
        var cycleSamples = hz * 8;
        var phase = _sampleIndex % cycleSamples;
        var t = phase / (double)hz;
        var first = Pulse(t, 2.0, 0.45);
        var second = Pulse(t, 5.0, 0.45);
        var factor = Math.Min(1.0, first + second);
        return Math.Clamp(weight * (decimal)factor, 0, MaxWeightTonnes);
    }

    private static double Pulse(double t, double center, double width)
    {
        var x = (t - center) / width;
        return Math.Exp(-x * x);
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

    private static byte[] BuildSample(int ch0, int ch1)
    {
        ch0 = Math.Clamp(ch0, 0, MaxAdcCode);
        ch1 = Math.Clamp(ch1, 0, MaxAdcCode);
        var b0 = (byte)(ch0 & 0xFF);
        var b1 = (byte)((ch0 >> 8) & 0xFF);
        var b2 = (byte)(ch1 & 0xFF);
        var b3 = (byte)((ch1 >> 8) & 0xFF);
        var aux = (byte)((b0 + b2 + AuxOffset) & 0xFF);

        return new[] { b0, b1, b2, b3, aux };
    }

    private void UpdateStreamInterval()
    {
        _streamTimer.Interval = HzToIntervalMs((int)_numHz.Value);
        UpdateStateLabel();
    }

    private static int HzToIntervalMs(int hz)
        => Math.Max(1, (int)Math.Round(1000.0 / Math.Max(1, hz), MidpointRounding.AwayFromZero));

    private void UpdateStateLabel()
    {
        if (_lblState is null || _numHz is null || _cmbScenario is null || _cmbPacketLog is null)
            return;

        _lblState.Text = $"STREAM={(_streaming ? "ON" : "OFF")}  SET={SetByte}  REQ={ReqByte}  Hz={(int)_numHz.Value}  Scenario={_cmbScenario.Text}  Лог={_cmbPacketLog.Text}";
    }

    private void UpdateCodePreview()
    {
        if (_lblCode is null || _numWeight is null)
            return;

        int code = WeightToAdcCode(_numWeight.Value);
        byte[] buf = BuildSample(code, code);
        _lblCode.Text = $"ADC={code}  bytes={string.Join(" ", buf)}";
    }

    private void AppendMsg(string text, Color? color = null)
    {
        if (_logLineCount >= MaxLogLines)
        {
            var endOfFirstLine = _log.Text.IndexOf('\n');
            if (endOfFirstLine < 0)
            {
                ClearLog();
            }
            else
            {
                _log.Select(0, endOfFirstLine + 1);
                _log.SelectedText = string.Empty;
                _logLineCount--;
            }
        }

        var prev = _log.SelectionColor;
        _log.SelectionColor = color ?? prev;
        _log.AppendText(text + "\r\n");
        _logLineCount++;
        _log.SelectionColor = prev;
        _log.ScrollToCaret();
    }

    private int PacketLogStep => _cmbPacketLog.SelectedIndex == 0 ? 1 : 10;

    private void ClearLog()
    {
        _log.Clear();
        _logLineCount = 0;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _isShuttingDown = true;
        _streamTimer.Stop();
        _port.DataReceived -= Port_DataReceived;
        if (_port.IsOpen) _port.Close();
        _faultForm?.Close();
        base.OnFormClosing(e);
    }
}
