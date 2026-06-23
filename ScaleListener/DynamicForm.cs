using System.IO.Ports;

namespace ScaleListener;

public class DynamicForm : Form
{
    private const decimal MaxWeightTonnes = 140m;
    private const int MaxAdcCode = 65535;
    private const int StreamIntervalMs = 17;

    private RichTextBox _log = null!;
    private Button _btnConnect = null!;
    private Button _btnStopStream = null!;
    private Button _btnClear = null!;
    private NumericUpDown _numWeight = null!;
    private NumericUpDown _numTolerance = null!;
    private Label _lblCode = null!;
    private Label _lblState = null!;

    private readonly SerialPort _port;
    private readonly Random _rng = new();
    private readonly System.Windows.Forms.Timer _streamTimer = new();
    private volatile bool _isShuttingDown;
    private bool _streaming;

    public DynamicForm()
    {
        BuildUi();

        _port = new SerialPort("COM4", 4800, Parity.Even, 8, StopBits.One)
        {
            ReadTimeout = SerialPort.InfiniteTimeout,
            WriteTimeout = 500,
        };
        _port.DataReceived += Port_DataReceived;

        _streamTimer.Interval = StreamIntervalMs;
        _streamTimer.Tick += (_, _) => SendDynamicResponse();
    }

    private void BuildUi()
    {
        Text = "Scale Listener - Динамика - COM4  4800/Even";
        ClientSize = new Size(720, 470);
        MinimumSize = new Size(620, 380);

        var lblWeight = new Label
        {
            Location = new Point(8, 12),
            Size = new Size(85, 24),
            Text = "Вес, т:",
            TextAlign = ContentAlignment.MiddleLeft,
        };

        _numWeight = new NumericUpDown
        {
            DecimalPlaces = 2,
            Increment = 0.10m,
            Location = new Point(95, 10),
            Maximum = MaxWeightTonnes,
            Minimum = 0,
            Size = new Size(100, 24),
            Value = 0,
        };
        _numWeight.ValueChanged += (_, _) => UpdateCodePreview();

        var lblTolerance = new Label
        {
            Location = new Point(215, 12),
            Size = new Size(105, 24),
            Text = "Погр., т:",
            TextAlign = ContentAlignment.MiddleLeft,
        };

        _numTolerance = new NumericUpDown
        {
            DecimalPlaces = 2,
            Increment = 0.01m,
            Location = new Point(322, 10),
            Maximum = 10,
            Minimum = 0,
            Size = new Size(90, 24),
            Value = 0.02m,
        };
        _numTolerance.ValueChanged += (_, _) => UpdateCodePreview();

        _lblCode = new Label
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            Location = new Point(430, 12),
            Size = new Size(282, 24),
            TextAlign = ContentAlignment.MiddleLeft,
        };

        _lblState = new Label
        {
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            Location = new Point(118, 436),
            Size = new Size(410, 24),
            Text = "STREAM=OFF  SET=126  REQ=254",
            TextAlign = ContentAlignment.MiddleLeft,
        };

        _log = new RichTextBox
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            Location = new Point(8, 46),
            Size = new Size(704, 380),
            ReadOnly = true,
            BackColor = Color.LightGray,
            Font = new Font("Courier New", 9.75f),
            DetectUrls = false,
            ScrollBars = RichTextBoxScrollBars.Vertical,
        };

        _btnConnect = new Button
        {
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
            Location = new Point(8, 436),
            Size = new Size(100, 26),
            Text = "Connect",
            FlatStyle = FlatStyle.Flat,
        };
        _btnConnect.Click += BtnConnect_Click;

        _btnStopStream = new Button
        {
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
            Location = new Point(538, 436),
            Size = new Size(86, 26),
            Text = "Stop stream",
            FlatStyle = FlatStyle.Flat,
            Enabled = false,
        };
        _btnStopStream.Click += (_, _) => StopStream("manual");

        _btnClear = new Button
        {
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
            Location = new Point(638, 436),
            Size = new Size(74, 26),
            Text = "Clear",
            FlatStyle = FlatStyle.Flat,
        };
        _btnClear.Click += (_, _) => _log.Clear();

        Controls.AddRange(new Control[]
        {
            lblWeight, _numWeight, lblTolerance, _numTolerance, _lblCode,
            _log, _btnConnect, _lblState, _btnStopStream, _btnClear
        });
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
        catch { /* порт закрылся во время чтения */ }
    }

    private void HandleIncomingBytes(byte[] buf)
    {
        if (_isShuttingDown || !_port.IsOpen)
            return;

        foreach (byte b in buf)
        {
            AppendMsg($"<< {b}");
            if (b == 126)
            {
                StopStream("set");
                AppendMsg(">>>> DYNAMIC SET", Color.DarkOrange);
            }
            else if (b == 254)
            {
                StartStream();
            }
        }
    }

    private void StartStream()
    {
        if (_streaming)
            return;

        _streaming = true;
        _streamTimer.Start();
        _btnStopStream.Enabled = true;
        _lblState.Text = "STREAM=ON  SET=126  REQ=254";
        AppendMsg($">>>> STREAM START  interval={StreamIntervalMs}ms", Color.Green);
    }

    private void StopStream(string reason)
    {
        if (!_streaming)
            return;

        _streamTimer.Stop();
        _streaming = false;
        _btnStopStream.Enabled = false;
        _lblState.Text = "STREAM=OFF  SET=126  REQ=254";
        AppendMsg($">>>> STREAM STOP  reason={reason}", Color.Gray);
    }

    private void SendDynamicResponse()
    {
        if (_isShuttingDown || !_port.IsOpen)
            return;

        decimal weight = _numWeight.Value;
        decimal tolerance = _numTolerance.Value;
        decimal ch0Weight = ApplyTolerance(weight, tolerance);
        decimal ch1Weight = ApplyTolerance(weight, tolerance);
        int ch0 = WeightToAdcCode(ch0Weight);
        int ch1 = WeightToAdcCode(ch1Weight);
        byte[] buf = BuildFrame(ch0, ch1);

        try
        {
            _port.Write(buf, 0, buf.Length);
            AppendMsg($">> {string.Join(" ", buf)}  CH0={ch0} ({ch0Weight:F2} т)  CH1={ch1} ({ch1Weight:F2} т)", Color.DodgerBlue);
        }
        catch (Exception ex)
        {
            StopStream("send-error");
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
        _streamTimer.Stop();
        _streamTimer.Dispose();
        _port.DataReceived -= Port_DataReceived;
        if (_port.IsOpen) _port.Close();
        base.OnFormClosing(e);
    }
}
