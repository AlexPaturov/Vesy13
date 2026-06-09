using System.IO.Ports;

namespace ScaleListener;

public class MainForm : Form
{
    // ── UI ────────────────────────────────────────────────────────────────
    private RichTextBox _log    = null!;
    private Button      _btnConnect = null!;
    private Button      _btnClear   = null!;

    // ── Serial ────────────────────────────────────────────────────────────
    private SerialPort _port;
    private readonly Random _rng = new();

    // ── Static stub ───────────────────────────────────────────────────────
    // TODO: заменить на реальные значения из таблицы sensor_static
    // Реальные ответы от устройства (ненагруженные весы)
    // CH0 = byte1*256 + byte0,  CH1 = byte3*256 + byte2
    private static readonly (byte b0, byte b1, byte b2, byte b3)[] _stub =
    {
        (7, 20, 6, 20),   // CH0=5127  CH1=5126
        (6, 20, 5, 20),   // CH0=5126  CH1=5125
        (6, 20, 6, 20),   // CH0=5126  CH1=5126
        (7, 20, 6, 20),   // CH0=5127  CH1=5126
        (7, 20, 4, 20),   // CH0=5127  CH1=5124
        (7, 20, 6, 20),   // CH0=5127  CH1=5126
        (7, 20, 5, 20),   // CH0=5127  CH1=5125
        (7, 20, 5, 20),   // CH0=5127  CH1=5125
        (7, 20, 6, 20),   // CH0=5127  CH1=5126
        (5, 20, 5, 20),   // CH0=5125  CH1=5125
        (7, 20, 6, 20),   // CH0=5127  CH1=5126
        (7, 20, 5, 20),   // CH0=5127  CH1=5125
        (7, 20, 5, 20),   // CH0=5127  CH1=5125
        (6, 20, 5, 20),   // CH0=5126  CH1=5125
        (7, 20, 6, 20),   // CH0=5127  CH1=5126
        (6, 20, 5, 20),   // CH0=5126  CH1=5125
        (6, 20, 5, 20),   // CH0=5126  CH1=5125
        (6, 20, 5, 20),   // CH0=5126  CH1=5125
        (7, 20, 5, 20),   // CH0=5127  CH1=5125
        (7, 20, 6, 20),   // CH0=5127  CH1=5126
        (7, 20, 5, 20),   // CH0=5127  CH1=5125
        (7, 20, 5, 20),   // CH0=5127  CH1=5125
        (6, 20, 5, 20),   // CH0=5126  CH1=5125
        (7, 20, 6, 20),   // CH0=5127  CH1=5126
        (6, 20, 5, 20),   // CH0=5126  CH1=5125
        (5, 20, 5, 20),   // CH0=5125  CH1=5125
    };

    // ── ctor ──────────────────────────────────────────────────────────────
    public MainForm()
    {
        BuildUi();

        _port = new SerialPort("COM4", 4800, Parity.Even, 8, StopBits.One)
        {
            ReadTimeout  = SerialPort.InfiniteTimeout,
            WriteTimeout = 500,
        };
        _port.DataReceived += Port_DataReceived;
    }

    // ── UI layout ────────────────────────────────────────────────────────
    private void BuildUi()
    {
        Text        = "Scale Listener – COM4  4800/Even";
        ClientSize  = new Size(480, 360);
        MinimumSize = new Size(380, 240);

        _log = new RichTextBox
        {
            Anchor     = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            Location   = new Point(8, 8),
            Size       = new Size(464, 306),
            ReadOnly   = true,
            BackColor  = Color.LightGray,
            Font       = new Font("Courier New", 9.75f),
            DetectUrls = false,
            ScrollBars = RichTextBoxScrollBars.Vertical,
        };

        _btnConnect = new Button
        {
            Anchor    = AnchorStyles.Bottom | AnchorStyles.Left,
            Location  = new Point(8, 324),
            Size      = new Size(100, 26),
            Text      = "Connect",
            FlatStyle = FlatStyle.Flat,
        };
        _btnConnect.Click += BtnConnect_Click;

        _btnClear = new Button
        {
            Anchor    = AnchorStyles.Bottom | AnchorStyles.Right,
            Location  = new Point(398, 324),
            Size      = new Size(74, 26),
            Text      = "Clear",
            FlatStyle = FlatStyle.Flat,
        };
        _btnClear.Click += (_, _) => _log.Clear();

        Controls.AddRange(new Control[] { _log, _btnConnect, _btnClear });
    }

    // ── Connect / Disconnect ──────────────────────────────────────────────
    private void BtnConnect_Click(object? sender, EventArgs e)
    {
        if (_port.IsOpen)
        {
            _port.Close();
            _btnConnect.Text = "Connect";
            _log.BackColor   = Color.LightGray;
            AppendMsg(">>>> OFFLINE", Color.Gray);
        }
        else
        {
            try
            {
                _port.Open();
                _btnConnect.Text = "Disconnect";
                _log.BackColor   = Color.White;
                AppendMsg(">>>> ONLINE  COM4  4800/Even", Color.Green);
            }
            catch (Exception ex)
            {
                AppendMsg($">>>> ERROR: {ex.Message}", Color.Red);
            }
        }
    }

    // ── Data received (thread-pool thread) ────────────────────────────────
    private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            while (_port.IsOpen && _port.BytesToRead > 0)
            {
                int raw = _port.ReadByte();
                if (raw < 0) break;
                byte b = (byte)raw;

                Invoke(() =>
                {
                    AppendMsg($"<< {b}");
                    if (b == 214)
                        SendStaticResponse();
                });
            }
        }
        catch { /* порт закрылся во время чтения */ }
    }

    // ── Ответ на 214 ──────────────────────────────────────────────────────
    private void SendStaticResponse()
    {
        var row = _stub[_rng.Next(_stub.Length)];
        byte[] buf = { row.b0, row.b1, row.b2, row.b3 };

        int ch0 = row.b1 * 256 + row.b0;
        int ch1 = row.b3 * 256 + row.b2;

        try
        {
            _port.Write(buf, 0, buf.Length);
            AppendMsg($">> {string.Join(" ", buf)}  CH0={ch0}  CH1={ch1}", Color.DodgerBlue);
        }
        catch (Exception ex)
        {
            AppendMsg($">>>> SEND ERROR: {ex.Message}", Color.Red);
        }
    }

    // ── Log helper ────────────────────────────────────────────────────────
    private void AppendMsg(string text, Color? color = null)
    {
        var prev = _log.SelectionColor;
        _log.SelectionColor = color ?? prev;
        _log.AppendText(text + "\r\n");
        _log.SelectionColor = prev;
        _log.ScrollToCaret();
    }

    // ── Cleanup ───────────────────────────────────────────────────────────
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_port.IsOpen) _port.Close();
        base.OnFormClosing(e);
    }
}
