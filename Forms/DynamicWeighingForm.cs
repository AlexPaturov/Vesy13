using Vesy13.Models;
using Vesy13.Services;

namespace Vesy13.Forms;

public class DynamicWeighingForm : Form
{
    private readonly AdcService _adc;

    private RadioButton _rbPlus     = null!;
    private RadioButton _rbMinus    = null!;
    private Label       _lblChannel = null!;
    private Label       _lblValue   = null!;
    private Label       _lblStatus  = null!;
    private Label       _lblBogie1  = null!;
    private Label       _lblBogie2  = null!;
    private Label       _lblTotal   = null!;
    private Button      _btnWeigh   = null!;
    private Button      _btnSave    = null!;
    private Button      _btnCancel  = null!;
    private Panel       _dotConn    = null!;
    private Label       _lblConn    = null!;

    private AdcFrame _lastFrame;
    private int      _bogie1Code;
    private int      _bogie2Code;
    private bool     _waitingBogie2;

    public DynamicWeighingForm(AdcService adc)
    {
        _adc = adc;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text          = "Взвешивание — Динамика";
        ClientSize    = new Size(560, 500);
        MinimumSize   = new Size(480, 440);
        StartPosition = FormStartPosition.CenterScreen;
        KeyPreview    = true;
        BackColor     = Color.FromArgb(240, 242, 245);

        // ── Direction selector ─────────────────────────────────────────────
        var gbDir = new GroupBox
        {
            Text     = "Направление движения состава",
            Location = new Point(8, 8),
            Size     = new Size(350, 50),
            Font     = new Font("Segoe UI", 9),
        };
        _rbPlus  = new RadioButton { Text = "→ (+)",  Location = new Point(12, 20), Checked = true, Font = new Font("Segoe UI", 10) };
        _rbMinus = new RadioButton { Text = "← (–)", Location = new Point(120, 20),                 Font = new Font("Segoe UI", 10) };
        gbDir.Controls.AddRange(new Control[] { _rbPlus, _rbMinus });

        // ── Channel indicator ──────────────────────────────────────────────
        _lblChannel = new Label
        {
            Location  = new Point(366, 22),
            AutoSize  = true,
            Font      = new Font("Segoe UI", 10),
            ForeColor = Color.FromArgb(80, 80, 80),
        };

        // ── Mass display ───────────────────────────────────────────────────
        var pnlDisplay = new Panel
        {
            Location  = new Point(8, 66),
            Size      = new Size(544, 175),
            BackColor = Color.Black,
        };

        _lblValue = new Label
        {
            Text      = "—",
            Font      = new Font("Courier New", 62, FontStyle.Bold),
            ForeColor = Color.DimGray,
            TextAlign = ContentAlignment.MiddleRight,
            Bounds    = new Rectangle(8, 4, 450, 110),
            AutoSize  = false,
        };

        var lblUnit = new Label
        {
            Text      = "т",
            Font      = new Font("Segoe UI", 22),
            ForeColor = Color.Gray,
            TextAlign = ContentAlignment.BottomLeft,
            Bounds    = new Rectangle(462, 60, 60, 64),
            AutoSize  = false,
        };

        _lblStatus = new Label
        {
            Text      = "Выберите направление и нажмите ВЗВЕСИТЬ для тележки 1",
            Font      = new Font("Segoe UI", 10),
            ForeColor = Color.Silver,
            TextAlign = ContentAlignment.MiddleCenter,
            Bounds    = new Rectangle(8, 124, 528, 44),
            AutoSize  = false,
        };

        pnlDisplay.Controls.AddRange(new Control[] { _lblValue, lblUnit, _lblStatus });

        // ── Bogie results ──────────────────────────────────────────────────
        var pnlResults = new Panel
        {
            Location  = new Point(8, 250),
            Size      = new Size(544, 80),
            BackColor = Color.FromArgb(225, 228, 235),
        };

        _lblBogie1 = MakeResultLabel("Тележка 1: —", 10, 10, pnlResults);
        _lblBogie2 = MakeResultLabel("Тележка 2: —", 10, 44, pnlResults);
        _lblTotal  = MakeResultLabel("Вагон: —",     290, 28, pnlResults,
            new Font("Segoe UI", 13, FontStyle.Bold), Color.FromArgb(0, 80, 160));

        // ── Weigh button ───────────────────────────────────────────────────
        _btnWeigh = new Button
        {
            Text      = "ВЗВЕСИТЬ   [Пробел]  — Тележка 1",
            Location  = new Point(8, 340),
            Size      = new Size(544, 62),
            Font      = new Font("Segoe UI", 14, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(0, 130, 0),
            ForeColor = Color.White,
        };
        _btnWeigh.Click += BtnWeigh_Click;

        _btnSave = new Button
        {
            Text      = "Сохранить вагон",
            Location  = new Point(8, 412),
            Size      = new Size(264, 38),
            Font      = new Font("Segoe UI", 12),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(0, 100, 180),
            ForeColor = Color.White,
            Visible   = false,
        };
        _btnSave.Click += BtnSave_Click;

        _btnCancel = new Button
        {
            Text      = "Сброс",
            Location  = new Point(280, 412),
            Size      = new Size(264, 38),
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
            gbDir, _lblChannel,
            pnlDisplay, pnlResults,
            _btnWeigh, _btnSave, _btnCancel,
            pnlStatus,
        });
    }

    private static Label MakeResultLabel(string text, int x, int y, Control parent,
        Font? font = null, Color? color = null)
    {
        var l = new Label
        {
            Text     = text,
            Location = new Point(x, y),
            AutoSize = true,
            Font     = font ?? new Font("Segoe UI", 11),
        };
        if (color.HasValue) l.ForeColor = color.Value;
        parent.Controls.Add(l);
        return l;
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
        int code = ActiveCode(_lastFrame);

        if (!_waitingBogie2)
        {
            _bogie1Code     = code;
            _lblBogie1.Text = $"Тележка 1: {_bogie1Code}";
            _lblValue.ForeColor = Color.Yellow;
            _lblStatus.Text = "Тележка 1 зафиксирована. Нажмите ВЗВЕСИТЬ для тележки 2";
            _btnWeigh.Text  = "ВЗВЕСИТЬ   [Пробел]  — Тележка 2";
            _btnWeigh.BackColor = Color.FromArgb(180, 100, 0);
            _waitingBogie2  = true;
        }
        else
        {
            _bogie2Code     = code;
            _lblBogie2.Text = $"Тележка 2: {_bogie2Code}";
            int total       = _bogie1Code + _bogie2Code;
            _lblTotal.Text  = $"Вагон: {total}";
            _lblValue.Text  = total.ToString();
            _lblValue.ForeColor = Color.Cyan;
            _lblStatus.Text = "Взвешивание завершено";

            _btnWeigh.Enabled  = false;
            _btnSave.Visible   = true;
            _btnCancel.Visible = true;
        }
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        // TODO: сохранить в БД через DatabaseService
        string dir = _rbPlus.Checked ? "→ (+)" : "← (–)";
        MessageBox.Show(
            $"Тележка 1: {_bogie1Code}\nТележка 2: {_bogie2Code}\n" +
            $"Вагон: {_bogie1Code + _bogie2Code}\nНаправление: {dir}\n\n" +
            "Сохранение в БД — в разработке.",
            "Сохранить", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ResetSession();
    }

    private void ResetSession()
    {
        _waitingBogie2      = false;
        _bogie1Code         = 0;
        _bogie2Code         = 0;
        _lblBogie1.Text     = "Тележка 1: —";
        _lblBogie2.Text     = "Тележка 2: —";
        _lblTotal.Text      = "Вагон: —";
        _lblValue.Text      = "—";
        _lblValue.ForeColor = _adc.IsConnected ? Color.LimeGreen : Color.DimGray;
        _lblStatus.Text     = "Выберите направление и нажмите ВЗВЕСИТЬ для тележки 1";
        _btnWeigh.Text      = "ВЗВЕСИТЬ   [Пробел]  — Тележка 1";
        _btnWeigh.BackColor = Color.FromArgb(0, 130, 0);
        _btnWeigh.Enabled   = true;
        _btnSave.Visible    = false;
        _btnCancel.Visible  = false;
    }
}