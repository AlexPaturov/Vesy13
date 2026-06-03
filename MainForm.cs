using Vesy13.Services;

namespace Vesy13;

public class MainForm : Form
{
    private readonly AdcService         _adc;
    private readonly CalibrationService _calib;

    private Panel _dotConn  = null!;
    private Label _lblConn  = null!;
    private Button _btnConn = null!;

    public MainForm(AdcService adc, CalibrationService calib)
    {
        _adc   = adc;
        _calib = calib;
        _adc.ConnectionChanged += (_, connected) => UpdateConn(connected);
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text          = "Весы №13";
        ClientSize    = new Size(380, 500);
        MinimumSize   = new Size(320, 440);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox   = false;
        BackColor     = Color.FromArgb(240, 242, 245);

        // ── Header ──────────────────────────────────────────────────────────
        var pnlHeader = new Panel
        {
            Dock      = DockStyle.Top,
            Height    = 100,
            BackColor = Color.FromArgb(25, 45, 90),
        };
        pnlHeader.Controls.Add(new Label
        {
            Text      = "ВЕСЫ №13",
            Font      = new Font("Segoe UI", 22, FontStyle.Bold),
            ForeColor = Color.White,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock      = DockStyle.Fill,
        });

        // ── Menu buttons ────────────────────────────────────────────────────
        var items = new (string Text, EventHandler Click)[]
        {
            ("Взвешивание — Статика",  BtnStatic_Click),
            ("Взвешивание — Динамика", BtnDynamic_Click),
            ("Сервис",                 BtnService_Click),
            ("Корректировки",          BtnCorrections_Click),
            ("Печать отвесной",        BtnPrint_Click),
            ("Просмотр логов",         BtnLogs_Click),
        };

        var pnlMenu = new TableLayoutPanel
        {
            Dock        = DockStyle.Fill,
            ColumnCount = 1,
            RowCount    = items.Length,
            Padding     = new Padding(30, 16, 30, 16),
        };
        for (int i = 0; i < items.Length; i++)
        {
            pnlMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / items.Length));
            var (text, handler) = items[i];
            var btn = new Button
            {
                Text      = text,
                Dock      = DockStyle.Fill,
                Font      = new Font("Segoe UI", 13),
                FlatStyle = FlatStyle.Flat,
                Margin    = new Padding(0, 5, 0, 5),
                BackColor = Color.FromArgb(25, 45, 90),
                ForeColor = Color.White,
                Cursor    = Cursors.Hand,
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(50, 80, 140);
            btn.Click += handler;
            pnlMenu.Controls.Add(btn, 0, i);
        }

        // ── Status bar ──────────────────────────────────────────────────────
        var pnlStatus = new Panel
        {
            Dock      = DockStyle.Bottom,
            Height    = 36,
            BackColor = Color.FromArgb(18, 32, 65),
            Padding   = new Padding(8, 0, 8, 0),
        };

        _dotConn = new Panel
        {
            Size      = new Size(10, 10),
            Location  = new Point(10, 13),
            BackColor = Color.Gray,
        };

        _lblConn = new Label
        {
            Text      = "АЦП: отключён",
            Font      = new Font("Segoe UI", 9),
            ForeColor = Color.Silver,
            Location  = new Point(26, 9),
            AutoSize  = true,
        };

        _btnConn = new Button
        {
            Text      = "Подключить",
            Size      = new Size(90, 22),
            Anchor    = AnchorStyles.Right | AnchorStyles.Top,
            Location  = new Point(272, 7),
            Font      = new Font("Segoe UI", 8),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(40, 70, 130),
            ForeColor = Color.White,
        };
        _btnConn.FlatAppearance.BorderSize = 0;
        _btnConn.Click += BtnConn_Click;

        pnlStatus.Controls.AddRange(new Control[] { _dotConn, _lblConn, _btnConn });

        Controls.AddRange(new Control[] { pnlMenu, pnlHeader, pnlStatus });
    }

    // ── Connection ──────────────────────────────────────────────────────────

    private void BtnConn_Click(object? sender, EventArgs e)
    {
        if (_adc.IsConnected)
        {
            _adc.Close();
        }
        else
        {
            try { _adc.Open("COM1"); }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void UpdateConn(bool connected)
    {
        if (InvokeRequired) { BeginInvoke(() => UpdateConn(connected)); return; }
        _dotConn.BackColor = connected ? Color.LimeGreen : Color.Gray;
        _lblConn.Text      = connected ? $"АЦП: {_adc.PortName}" : "АЦП: отключён";
        _btnConn.Text      = connected ? "Отключить" : "Подключить";
    }

    // ── Navigation ──────────────────────────────────────────────────────────

    private void BtnStatic_Click(object? sender, EventArgs e) => OpenForm(new Forms.StaticWeighingForm(_adc, _calib));
    private void BtnDynamic_Click(object? sender, EventArgs e) => OpenForm(new Forms.DynamicWeighingForm(_adc, _calib));

    private void BtnService_Click(object? sender, EventArgs e) => OpenForm(new Forms.ServiceForm(_adc, _calib));

    private void OpenForm(Form form)
    {
        form.FormClosed += (_, _) => Show();
        Hide();
        form.Show();
    }

    private void BtnCorrections_Click(object? sender, EventArgs e) =>
        MessageBox.Show("В разработке", "Корректировки", MessageBoxButtons.OK, MessageBoxIcon.Information);

    private void BtnPrint_Click(object? sender, EventArgs e) =>
        MessageBox.Show("В разработке", "Печать отвесной", MessageBoxButtons.OK, MessageBoxIcon.Information);

    private void BtnLogs_Click(object? sender, EventArgs e) =>
        MessageBox.Show("В разработке", "Просмотр логов", MessageBoxButtons.OK, MessageBoxIcon.Information);

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _adc.Close();
        base.OnFormClosed(e);
    }
}
