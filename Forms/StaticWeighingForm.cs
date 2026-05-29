using Vesy13.Models;
using Vesy13.Services;

namespace Vesy13.Forms;

public class StaticWeighingForm : Form
{
    private readonly AdcService _adc;

    private Label  _lblValue    = null!;
    private Label  _lblStatus   = null!;
    private Label  _lblChannel  = null!;
    private Button _btnWeigh    = null!;
    private Button _btnZero     = null!;
    private Button _btnSave     = null!;
    private Button _btnCancel   = null!;
    private Panel  _dotConn     = null!;
    private Label  _lblConn     = null!;

    private AdcFrame _lastFrame;
    private int      _capturedCode;

    public StaticWeighingForm(AdcService adc)
    {
        _adc = adc;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text          = "Взвешивание — Статика";
        ClientSize    = new Size(560, 460);
        MinimumSize   = new Size(480, 400);
        StartPosition = FormStartPosition.CenterScreen;
        KeyPreview    = true;
        BackColor     = Color.FromArgb(240, 242, 245);

        // ── Channel indicator ──────────────────────────────────────────────
        _lblChannel = new Label
        {
            Location  = new Point(8, 10),
            AutoSize  = true,
            Font      = new Font("Segoe UI", 10),
            ForeColor = Color.FromArgb(80, 80, 80),
        };

        // ── Mass display ───────────────────────────────────────────────────
        var pnlDisplay = new Panel
        {
            Location  = new Point(8, 36),
            Size      = new Size(544, 210),
            BackColor = Color.Black,
        };

        _lblValue = new Label
        {
            Text      = "—",
            Font      = new Font("Courier New", 72, FontStyle.Bold),
            ForeColor = Color.DimGray,
            TextAlign = ContentAlignment.MiddleRight,
            Bounds    = new Rectangle(8, 8, 450, 130),
            AutoSize  = false,
        };

        var lblUnit = new Label
        {
            Text      = "т",
            Font      = new Font("Segoe UI", 24),
            ForeColor = Color.Gray,
            TextAlign = ContentAlignment.BottomLeft,
            Bounds    = new Rectangle(462, 80, 60, 70),
            AutoSize  = false,
        };

        _lblStatus = new Label
        {
            Text      = "Готов к взвешиванию",
            Font      = new Font("Segoe UI", 12),
            ForeColor = Color.Silver,
            TextAlign = ContentAlignment.MiddleCenter,
            Bounds    = new Rectangle(8, 148, 528, 54),
            AutoSize  = false,
        };

        pnlDisplay.Controls.AddRange(new Control[] { _lblValue, lblUnit, _lblStatus });

        // ── Buttons ────────────────────────────────────────────────────────
        _btnWeigh = new Button
        {
            Text      = "ВЗВЕСИТЬ   [Пробел]",
            Location  = new Point(8, 258),
            Size      = new Size(340, 62),
            Font      = new Font("Segoe UI", 15, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(0, 130, 0),
            ForeColor = Color.White,
        };
        _btnWeigh.Click += BtnWeigh_Click;

        _btnZero = new Button
        {
            Text      = "Ноль",
            Location  = new Point(356, 258),
            Size      = new Size(110, 62),
            Font      = new Font("Segoe UI", 12),
            FlatStyle = FlatStyle.Flat,
        };
        _btnZero.Click += BtnZero_Click;

        _btnSave = new Button
        {
            Text      = "Сохранить",
            Location  = new Point(8, 330),
            Size      = new Size(220, 38),
            Font      = new Font("Segoe UI", 12),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(0, 100, 180),
            ForeColor = Color.White,
            Visible   = false,
        };
        _btnSave.Click += BtnSave_Click;

        _btnCancel = new Button
        {
            Text      = "Отмена",
            Location  = new Point(236, 330),
            Size      = new Size(220, 38),
            Font      = new Font("Segoe UI", 12),
            FlatStyle = FlatStyle.Flat,
            Visible   = false,
        };
        _btnCancel.Click += (_, _) => ResetSession();

        // ── Status bar ─────────────────────────────────────────────────────
        var pnlStatus = new Panel
        {
            Dock      = DockStyle.Bottom,
            Height    = 34,
            BackColor = Color.FromArgb(18, 32, 65),
        };

        var btnBack = new Button
        {
            Text      = "← Назад",
            Location  = new Point(8, 6),
            Size      = new Size(80, 22),
            FlatStyle = FlatStyle.Flat,
            Font      = new Font("Segoe UI", 8),
            BackColor = Color.FromArgb(40, 70, 130),
            ForeColor = Color.White,
        };
        btnBack.FlatAppearance.BorderSize = 0;
        btnBack.Click += (_, _) => Close();

        _dotConn = new Panel { Size = new Size(10, 10), Location = new Point(100, 12), BackColor = Color.Gray };
        _lblConn = new Label { Text = "АЦП: —", Font = new Font("Segoe UI", 9), ForeColor = Color.Silver, Location = new Point(116, 8), AutoSize = true };

        pnlStatus.Controls.AddRange(new Control[] { btnBack, _dotConn, _lblConn });

        Controls.AddRange(new Control[]
        {
            _lblChannel, pnlDisplay,
            _btnWeigh, _btnZero, _btnSave, _btnCancel,
            pnlStatus,
        });
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        _adc.FrameReceived     += OnFrame;
        _adc.ConnectionChanged += OnConnectionChanged;
        UpdateConn(_adc.IsConnected);
        UpdateChannelLabel();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _adc.FrameReceived     -= OnFrame;
        _adc.ConnectionChanged -= OnConnectionChanged;
        base.OnFormClosed(e);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Space && _btnWeigh.Enabled)
        {
            BtnWeigh_Click(this, EventArgs.Empty);
            return true;
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void OnFrame(object? sender, AdcFrame frame)
    {
        if (InvokeRequired) { BeginInvoke(() => OnFrame(sender, frame)); return; }
        _lastFrame = frame;
        if (_btnWeigh.Enabled)
            _lblValue.Text = ActiveCode(frame).ToString();
    }

    private void OnConnectionChanged(object? sender, bool connected)
    {
        if (InvokeRequired) { BeginInvoke(() => OnConnectionChanged(sender, connected)); return; }
        UpdateConn(connected);
    }

    private void UpdateConn(bool connected)
    {
        _dotConn.BackColor  = connected ? Color.LimeGreen : Color.Gray;
        _lblConn.Text       = connected ? $"АЦП: {_adc.PortName}" : "АЦП: отключён";
        _lblValue.ForeColor = connected ? Color.LimeGreen : Color.DimGray;
    }

    private void UpdateChannelLabel()
    {
        _lblChannel.Text = _adc.Channel == ActiveChannel.Main
            ? "Канал: Основной (CH0)"
            : "Канал: Резервный (CH1)";
    }

    private int ActiveCode(AdcFrame f) =>
        _adc.Channel == ActiveChannel.Main ? f.Ch0 : f.Ch1;

    private void BtnWeigh_Click(object? sender, EventArgs e)
    {
        // TODO: накопить фреймы за окно (1–2 с), усреднить, применить калибровку
        _capturedCode       = ActiveCode(_lastFrame);
        _lblValue.Text      = _capturedCode.ToString();
        _lblValue.ForeColor = Color.Yellow;
        _lblStatus.Text     = "Масса зафиксирована";

        _btnWeigh.Enabled  = false;
        _btnSave.Visible   = true;
        _btnCancel.Visible = true;
    }

    private void BtnZero_Click(object? sender, EventArgs e)
    {
        // TODO: сохранить смещение нуля в ZeroService
        MessageBox.Show("Ноль установлен (в разработке)", "Ноль",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        // TODO: сохранить в БД через DatabaseService
        MessageBox.Show($"Код АЦП: {_capturedCode}\nСохранение в БД — в разработке.",
            "Сохранить", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ResetSession();
    }

    private void ResetSession()
    {
        _lblStatus.Text     = "Готов к взвешиванию";
        _lblValue.ForeColor = _adc.IsConnected ? Color.LimeGreen : Color.DimGray;
        _btnWeigh.Enabled   = true;
        _btnSave.Visible    = false;
        _btnCancel.Visible  = false;
    }
}