using System.IO.Ports;

namespace ScaleListener;

public class DynamicForm : Form
{
    private const decimal MaxWeightTonnes = 140m;
    private const int MaxAdcCode = 65535;
    private const byte SetByte = 126;
    private const byte ReqByte = 254;
    private const int AuxOffset = 28;
    private const int DefaultHz = 47;

    private RichTextBox _log = null!;
    private Button _btnConnect = null!;
    private Button _btnStopStream = null!;
    private Button _btnClear = null!;
    private NumericUpDown _numWeight = null!;
    private NumericUpDown _numTolerance = null!;
    private NumericUpDown _numHz = null!;
    private ComboBox _cmbScenario = null!;
    private Label _lblCode = null!;
    private Label _lblState = null!;

    private readonly SerialPort _port;
    private readonly Random _rng = new();
    private readonly System.Windows.Forms.Timer _streamTimer = new();
    private volatile bool _isShuttingDown;
    private bool _streaming;
    private int _sampleIndex;

    public DynamicForm()
    {
        BuildUi();

        _port = new SerialPort("COM4", 4800, Parity.Even, 8, StopBits.One)
        {
            ReadTimeout = SerialPort.InfiniteTimeout,
            WriteTimeout = 500,
        };
        _port.DataReceived += Port_DataReceived;

        _streamTimer.Interval = HzToIntervalMs((int)_numHz.Value);
        _streamTimer.Tick += (_, _) => SendDynamicResponse();
    }

    private void BuildUi()
    {
        Text = "Scale Listener - Динамика - COM4  4800/Even";
        ClientSize = new Size(860, 520);
        MinimumSize = new Size(760, 430);

        var lblWeight = new Label
        {
            Location = new Point(8, 12),
            Size = new Size(65, 24),
            Text = "Вес, т:",
            TextAlign = ContentAlignment.MiddleLeft,
        };

        _numWeight = new NumericUpDown
        {
            DecimalPlaces = 2,
            Increment = 0.10m,
            Location = new Point(75, 10),
            Maximum = MaxWeightTonnes,
            Minimum = 0,
            Size = new Size(90, 24),
            Value = 0,
        };
        _numWeight.ValueChanged += (_, _) => UpdateCodePreview();

        var lblTolerance = new Label
        {
            Location = new Point(178, 12),
            Size = new Size(70, 24),
            Text = "Шум, т:",
            TextAlign = ContentAlignment.MiddleLeft,
        };

        _numTolerance = new NumericUpDown
        {
            DecimalPlaces = 2,
            Increment = 0.01m,
            Location = new Point(250, 10),
            Maximum = 10,
            Minimum = 0,
            Size = new Size(80, 24),
            Value = 0.02m,
        };
        _numTolerance.ValueChanged += (_, _) => UpdateCodePreview();

        var lblHz = new Label
        {
            Location = new Point(342, 12),
            Size = new Size(32, 24),
            Text = "Hz:",
            TextAlign = ContentAlignment.MiddleLeft,
        };

        _numHz = new NumericUpDown
        {
            DecimalPlaces = 0,
            Increment = 1,
            Location = new Point(376, 10),
            Maximum = 200,
            Minimum = 1,
            Size = new Size(64, 24),
            Value = DefaultHz,
        };
        _numHz.ValueChanged += (_, _) => UpdateStreamInterval();

        var lblScenario = new Label
        {
            Location = new Point(452, 12),
            Size = new Size(76, 24),
            Text = "Сценарий:",
            TextAlign = ContentAlignment.MiddleLeft,
        };

        _cmbScenario = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location = new Point(532, 10),
            Size = new Size(150, 24),
        };
        _cmbScenario.Items.AddRange(new object[] { "Постоянная", "Две тележки", "Битые сэмплы" });
        _cmbScenario.SelectedIndex = 0;
        _cmbScenario.SelectedIndexChanged += (_, _) => UpdateCodePreview();

        _lblCode = new Label
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            Location = new Point(696, 12),
            Size = new Size(156, 24),
            TextAlign = ContentAlignment.MiddleLeft,
        };

        _lblState = new Label
        {
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            Location = new Point(118, 486),
            Size = new Size(560, 24),
            TextAlign = ContentAlignment.MiddleLeft,
        };

        _log = new RichTextBox
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            Location = new Point(8, 46),
            Size = new Size(844, 430),
            ReadOnly = true,
            BackColor = Color.LightGray,
            Font = new Font("Courier New", 9.75f),
            DetectUrls = false,
            ScrollBars = RichTextBoxScrollBars.Vertical,
        };

        _btnConnect = new Button
        {
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
            Location = new Point(8, 486),
            Size = new Size(100, 26),
            Text = "Connect",
            FlatStyle = FlatStyle.Flat,
        };
        _btnConnect.Click += BtnConnect_Click;

        _btnStopStream = new Button
        {
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
            Location = new Point(678, 486),
            Size = new Size(96, 26),
            Text = "Stop stream",
            FlatStyle = FlatStyle.Flat,
            Enabled = false,
        };
        _btnStopStream.Click += (_, _) => StopStream("manual");

        _btnClear = new Button
        {
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
            Location = new Point(786, 486),
            Size = new Size(66, 26),
            Text = "Clear",
            FlatStyle = FlatStyle.Flat,
        };
        _btnClear.Click += (_, _) => _log.Clear();

        Controls.AddRange(new Control[]
        {
            lblWeight, _numWeight, lblTolerance, _numTolerance, lblHz, _numHz,
            lblScenario, _cmbScenario, _lblCode, _log, _btnConnect, _lblState,
            _btnStopStream, _btnClear
        });

        UpdateStateLabel();
        UpdateCodePreview();
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

        decimal baseWeight = GetScenarioWeight();
        decimal tolerance = _numTolerance.Value;
        decimal ch0Weight = ApplyTolerance(baseWeight, tolerance);
        decimal ch1Weight = ApplyTolerance(baseWeight, tolerance);
        int ch0 = WeightToAdcCode(ch0Weight);
        int ch1 = WeightToAdcCode(ch1Weight);
        byte[] buf = BuildSample(ch0, ch1);

        if (_cmbScenario.Text == "Битые сэмплы" && _sampleIndex > 0 && _sampleIndex % 50 == 0)
            buf[4]++;

        try
        {
            _port.Write(buf, 0, buf.Length);
            AppendMsg($">> {string.Join(" ", buf)}  CH0={ch0} ({ch0Weight:F2} т)  CH1={ch1} ({ch1Weight:F2} т)", Color.DodgerBlue);
            _sampleIndex++;
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
        if (_lblState is null || _numHz is null || _cmbScenario is null)
            return;

        _lblState.Text = $"STREAM={(_streaming ? "ON" : "OFF")}  SET={SetByte}  REQ={ReqByte}  Hz={(int)_numHz.Value}  Scenario={_cmbScenario.Text}";
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
        var prev = _log.SelectionColor;
        _log.SelectionColor = color ?? prev;
        _log.AppendText(text + "\r\n");
        _log.SelectionColor = prev;
        _log.ScrollToCaret();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _isShuttingDown = true;
        _streamTimer.Stop();
        _streamTimer.Dispose();
        _port.DataReceived -= Port_DataReceived;
        if (_port.IsOpen) _port.Close();
        base.OnFormClosing(e);
    }
}
