using System.IO.Ports;

namespace Vesy13;

public class MainForm : Form
{
    // ── UI controls ───────────────────────────────────────────────────────────
    private TextBox      txtSend   = null!;
    private RichTextBox  txtLog    = null!;
    private Button       btnOnline = null!;
    private Button       btnSend   = null!;
    private Button       btnStop   = null!;
    private Button       btnClear  = null!;

    // ── COM port ──────────────────────────────────────────────────────────────
    private SerialPort?  _port;

    // ── Frame detection ───────────────────────────────────────────────────────
    private readonly List<byte>  _buffer    = new();
    private readonly object      _lock      = new();
    private System.Threading.Timer? _frameTimer;

    // ── Polling ───────────────────────────────────────────────────────────────
    private volatile bool _isPolling;
    private byte          _pollByte = 214;

    public MainForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        txtSend   = new TextBox();
        txtLog    = new RichTextBox();
        btnOnline = new Button();
        btnSend   = new Button();
        btnStop   = new Button();
        btnClear  = new Button();

        SuspendLayout();

        // txtSend
        txtSend.Anchor   = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtSend.Font     = new Font("Courier New", 9.75F);
        txtSend.Location = new Point(8, 8);
        txtSend.Size     = new Size(340, 22);

        // btnSend
        btnSend.Anchor    = AnchorStyles.Top | AnchorStyles.Right;
        btnSend.FlatStyle = FlatStyle.Flat;
        btnSend.Location  = new Point(356, 8);
        btnSend.Size      = new Size(90, 23);
        btnSend.Text      = "Send";
        btnSend.Click    += BtnSend_Click;

        // btnStop
        btnStop.Anchor    = AnchorStyles.Top | AnchorStyles.Right;
        btnStop.Enabled   = false;
        btnStop.FlatStyle = FlatStyle.Flat;
        btnStop.Location  = new Point(356, 38);
        btnStop.Size      = new Size(90, 23);
        btnStop.Text      = "Stop";
        btnStop.Click    += BtnStop_Click;

        // txtLog
        txtLog.Anchor     = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        txtLog.BackColor  = Color.LightGray;
        txtLog.DetectUrls = false;
        txtLog.Font       = new Font("Courier New", 9.75F);
        txtLog.Location   = new Point(8, 70);
        txtLog.ReadOnly   = true;
        txtLog.Size       = new Size(438, 150);

        // btnOnline
        btnOnline.Anchor    = AnchorStyles.Bottom | AnchorStyles.Left;
        btnOnline.FlatStyle = FlatStyle.Flat;
        btnOnline.Location  = new Point(8, 228);
        btnOnline.Size      = new Size(90, 23);
        btnOnline.Text      = "Offline";
        btnOnline.Click    += BtnOnline_Click;

        // btnClear
        btnClear.Anchor    = AnchorStyles.Bottom | AnchorStyles.Right;
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.Location  = new Point(356, 228);
        btnClear.Size      = new Size(90, 23);
        btnClear.Text      = "Clear";
        btnClear.Click    += (_, _) => txtLog.Clear();

        ClientSize = new Size(454, 260);
        MinimumSize = new Size(434, 180);
        Text = "Vesy13: Offline";
        Controls.AddRange(new Control[] { txtSend, btnSend, btnStop, txtLog, btnOnline, btnClear });

        ResumeLayout(false);
        PerformLayout();
    }

    // ── COM port ──────────────────────────────────────────────────────────────

    private void BtnOnline_Click(object? sender, EventArgs e)
    {
        if (_port?.IsOpen == true)
        {
            ClosePort();
        }
        else
        {
            try { OpenPort(); }
            catch (Exception ex) { Log($">>>> ERROR: {ex.Message}", Color.Red); }
        }
    }

    private void OpenPort()
    {
        _port = new SerialPort("COM1", 4800, Parity.Even, 8, StopBits.One)
        {
            ReadTimeout  = SerialPort.InfiniteTimeout,
            WriteTimeout = 500,
        };
        _port.DataReceived += OnDataReceived;
        _port.Open();

        _frameTimer = new System.Threading.Timer(OnFrameTimer, null, Timeout.Infinite, Timeout.Infinite);

        txtLog.BackColor = Color.White;
        btnOnline.Text   = "Online";
        Text             = "Vesy13: COM1";
        Log(">>>> ONLINE", Color.Green);
    }

    private void ClosePort()
    {
        _isPolling = false;
        _frameTimer?.Dispose();
        _frameTimer = null;
        lock (_lock) _buffer.Clear();

        _port?.Close();
        _port?.Dispose();
        _port = null;

        txtLog.BackColor = Color.LightGray;
        btnOnline.Text   = "Offline";
        btnSend.Enabled  = true;
        btnStop.Enabled  = false;
        Text             = "Vesy13: Offline";
        Log(">>>> OFFLINE", Color.Green);
    }

    // ── Receive ───────────────────────────────────────────────────────────────

    private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        if (_port == null || !_port.IsOpen) return;

        int count = _port.BytesToRead;
        var buf = new byte[count];
        _port.Read(buf, 0, count);

        var text = string.Join(" ", buf) + " ";
        ShowText(text);

        lock (_lock) _buffer.AddRange(buf);
        _frameTimer?.Change(20, Timeout.Infinite);
    }

    private void OnFrameTimer(object? state)
    {
        byte[] frame;
        lock (_lock)
        {
            if (_buffer.Count == 0) return;
            frame = _buffer.ToArray();
            _buffer.Clear();
        }

        OnFrameReceived(frame);

        if (_isPolling)
            SendRaw(_pollByte);
    }

    private void OnFrameReceived(byte[] frame)
    {
        // TODO: parse frame, save to DB
        Log($">>>> Frame [{frame.Length}]: {string.Join(" ", frame)}", Color.DarkCyan);
    }

    // ── Send ──────────────────────────────────────────────────────────────────

    private void BtnSend_Click(object? sender, EventArgs e)
    {
        if (!byte.TryParse(txtSend.Text.Trim(), out byte value)) return;
        if (_port == null || !_port.IsOpen) { Log(">>>> PORT CLOSED", Color.Red); return; }

        txtSend.Text    = "";
        btnSend.Enabled = false;
        btnStop.Enabled = true;

        _pollByte  = value;
        _isPolling = true;
        SendRaw(value);
        Log($">> Poll started: {value}", Color.Green);
    }

    private void BtnStop_Click(object? sender, EventArgs e)
    {
        _isPolling      = false;
        btnSend.Enabled = true;
        btnStop.Enabled = false;
        Log(">>>> STOPPED", Color.Green);
    }

    private void SendRaw(byte b)
    {
        try { _port?.Write(new[] { b }, 0, 1); }
        catch (Exception ex) { Log($">>>> SEND ERROR: {ex.Message}", Color.Red); }
    }

    // ── UI helpers ────────────────────────────────────────────────────────────

    private void ShowText(string s)
    {
        if (txtLog.InvokeRequired) { txtLog.BeginInvoke(() => ShowText(s)); return; }
        txtLog.AppendText(s);
    }

    private void Log(string s, Color color)
    {
        if (txtLog.InvokeRequired) { txtLog.BeginInvoke(() => Log(s, color)); return; }
        var prev = txtLog.SelectionColor;
        txtLog.SelectionColor = color;
        txtLog.AppendText("\r\n" + s + "\r\n");
        txtLog.SelectionColor = prev;
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        ClosePort();
        base.OnFormClosed(e);
    }
}
