using System.Globalization;
using System.IO.Ports;
using Vesy13.Models;
using Vesy13.Services;

namespace Vesy13.Forms;

public class ServiceForm : Form
{
    private readonly AdcService        _adc;
    private readonly CalibrationService _calib;
    private bool _adminUnlocked;

    // ── Monitoring controls ────────────────────────────────────────────────
    private ComboBox    _cmbPort   = null!;
    private Button      _btnConn   = null!;
    private Panel       _dotConn   = null!;
    private Label       _lblConn   = null!;
    private Label       _lblCh0    = null!;
    private Label       _lblCh1    = null!;
    private Label       _lblRate   = null!;
    private RichTextBox _rtbLog    = null!;
    private CheckBox    _chkLog    = null!;
    private int         _frameCount;
    private int         _lastCh0;
    private int         _lastCh1;
    private System.Windows.Forms.Timer _rateTimer = null!;

    // ── Calibration static controls ────────────────────────────────────────
    private bool         _calibUseCh0 = true;
    private DataGridView _dgvCalib    = null!;
    private Label        _lblLiveAdc  = null!;
    private TextBox      _txtK        = null!;
    private TextBox      _txtB        = null!;

    // ── Calibration dynamic controls ───────────────────────────────────────
    private Label   _lblLiveAdcD  = null!;
    private TextBox _txtKPlus     = null!;
    private TextBox _txtKMinus    = null!;
    private TextBox _txtCodePlus  = null!;
    private TextBox _txtCodeMinus = null!;
    private TextBox _txtMassPlus  = null!;
    private TextBox _txtMassMinus = null!;

    // ── Admin lock ─────────────────────────────────────────────────────────
    private Button  _btnAdmin  = null!;
    private TabPage _tabCalibS = null!;
    private TabPage _tabCalibD = null!;
    private TabPage _tabSett   = null!;

    public ServiceForm(AdcService adc, CalibrationService calib)
    {
        _adc   = adc;
        _calib = calib;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text          = "Сервисный режим";
        ClientSize    = new Size(720, 560);
        MinimumSize   = new Size(640, 480);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor     = Color.FromArgb(240, 242, 245);

        _btnAdmin = new Button
        {
            Text      = "🔒 Войти как администратор",
            Anchor    = AnchorStyles.Top | AnchorStyles.Right,
            Location  = new Point(440, 8),
            Size      = new Size(270, 28),
            FlatStyle = FlatStyle.Flat,
            Font      = new Font("Segoe UI", 9),
            BackColor = Color.FromArgb(80, 60, 20),
            ForeColor = Color.White,
        };
        _btnAdmin.FlatAppearance.BorderSize = 0;
        _btnAdmin.Click += BtnAdmin_Click;

        var tabs = new TabControl
        {
            Location = new Point(0, 44),
            Anchor   = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            Size     = new Size(720, 480),
            Font     = new Font("Segoe UI", 10),
        };

        _tabCalibS = BuildCalibStaticTab();
        _tabCalibD = BuildCalibDynamicTab();
        _tabSett   = BuildSettingsTab();

        tabs.TabPages.Add(BuildChannelTab());
        tabs.TabPages.Add(BuildMonitorTab());
        tabs.TabPages.Add(_tabCalibS);
        tabs.TabPages.Add(_tabCalibD);
        tabs.TabPages.Add(_tabSett);

        SetAdminTabs(false);

        var pnlStatus = new Panel { Dock = DockStyle.Bottom, Height = 34, BackColor = Color.FromArgb(18, 32, 65) };
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
        pnlStatus.Controls.Add(btnBack);

        Controls.AddRange(new Control[] { _btnAdmin, tabs, pnlStatus });

        _rateTimer = new System.Windows.Forms.Timer { Interval = 1000 };
        _rateTimer.Tick += (_, _) =>
        {
            _lblRate.Text = $"{_frameCount} фр/с";
            _frameCount   = 0;
        };
    }

    // ── Tab: Канал ─────────────────────────────────────────────────────────
    private TabPage BuildChannelTab()
    {
        var tab = new TabPage("Канал");

        tab.Controls.Add(new Label
        {
            Text     = "Выбор активного канала",
            Location = new Point(20, 16),
            AutoSize = true,
            Font     = new Font("Segoe UI", 11, FontStyle.Bold),
        });

        var rbMain = new RadioButton
        {
            Text     = "Основной  —  CH0",
            Location = new Point(20, 52),
            AutoSize = true,
            Font     = new Font("Segoe UI", 13),
            Checked  = _adc.Channel == ActiveChannel.Main,
        };
        var rbBackup = new RadioButton
        {
            Text     = "Резервный  —  CH1",
            Location = new Point(20, 90),
            AutoSize = true,
            Font     = new Font("Segoe UI", 13),
            Checked  = _adc.Channel == ActiveChannel.Backup,
        };
        rbMain.CheckedChanged   += (_, _) => { if (rbMain.Checked)   _adc.Channel = ActiveChannel.Main; };
        rbBackup.CheckedChanged += (_, _) => { if (rbBackup.Checked) _adc.Channel = ActiveChannel.Backup; };

        tab.Controls.Add(new Label
        {
            Text      = "Изменение канала применяется немедленно и не требует пароля.",
            Location  = new Point(20, 136),
            AutoSize  = true,
            Font      = new Font("Segoe UI", 9),
            ForeColor = Color.Gray,
        });

        tab.Controls.AddRange(new Control[] { rbMain, rbBackup });
        return tab;
    }

    // ── Tab: Мониторинг ────────────────────────────────────────────────────
    private TabPage BuildMonitorTab()
    {
        var tab = new TabPage("Мониторинг");

        // ── Connection row ─────────────────────────────────────────────────
        _cmbPort = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location      = new Point(10, 12),
            Width         = 90,
            Font          = new Font("Segoe UI", 10),
        };

        _dotConn = new Panel
        {
            Size      = new Size(10, 10),
            Location  = new Point(108, 16),
            BackColor = Color.Gray,
        };

        _btnConn = new Button
        {
            Text      = "Подключить",
            Location  = new Point(124, 8),
            Size      = new Size(110, 28),
            FlatStyle = FlatStyle.Flat,
            Font      = new Font("Segoe UI", 9),
            BackColor = Color.FromArgb(0, 100, 50),
            ForeColor = Color.White,
        };
        _btnConn.FlatAppearance.BorderSize = 0;
        _btnConn.Click += BtnMonConn_Click;

        RefreshPorts();

        var btnRefresh = new Button
        {
            Text      = "↺",
            Location  = new Point(242, 8),
            Size      = new Size(30, 28),
            FlatStyle = FlatStyle.Flat,
            Font      = new Font("Segoe UI", 11),
        };
        btnRefresh.Click += (_, _) => RefreshPorts();

        _lblConn = new Label
        {
            Text      = "Нет подключения",
            Location  = new Point(280, 14),
            AutoSize  = true,
            Font      = new Font("Segoe UI", 9),
            ForeColor = Color.Gray,
        };

        _lblRate = new Label
        {
            Text      = "— фр/с",
            Anchor    = AnchorStyles.Top | AnchorStyles.Right,
            Location  = new Point(590, 14),
            AutoSize  = true,
            Font      = new Font("Segoe UI", 9),
            ForeColor = Color.Gray,
        };

        // ── ADC value panels ───────────────────────────────────────────────
        var pnlCh0 = MakeChannelPanel("CH0 — Основной", out _lblCh0);
        var pnlCh1 = MakeChannelPanel("CH1 — Резервный", out _lblCh1);
        pnlCh0.Location = new Point(10, 48);
        pnlCh1.Location = new Point(360, 48);

        // ── Log ───────────────────────────────────────────────────────────
        _chkLog = new CheckBox
        {
            Text      = "Лог активен",
            Location  = new Point(10, 198),
            AutoSize  = true,
            Font      = new Font("Segoe UI", 9),
            Checked   = true,
        };

        var btnClear = new Button
        {
            Text      = "Очистить",
            Anchor    = AnchorStyles.Top | AnchorStyles.Right,
            Location  = new Point(600, 194),
            Size      = new Size(80, 24),
            FlatStyle = FlatStyle.Flat,
            Font      = new Font("Segoe UI", 9),
        };
        btnClear.Click += (_, _) => _rtbLog.Clear();

        _rtbLog = new RichTextBox
        {
            Location   = new Point(10, 228),
            Size       = new Size(680, 200),
            Anchor     = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            ReadOnly   = true,
            BackColor  = Color.FromArgb(18, 18, 18),
            ForeColor  = Color.LightGray,
            Font       = new Font("Courier New", 9),
            DetectUrls = false,
            ScrollBars = RichTextBoxScrollBars.Vertical,
            WordWrap   = false,
        };

        tab.Controls.AddRange(new Control[]
        {
            _cmbPort, _dotConn, _btnConn, btnRefresh, _lblConn, _lblRate,
            pnlCh0, pnlCh1,
            _chkLog, btnClear, _rtbLog,
        });

        return tab;
    }

    private static Panel MakeChannelPanel(string title, out Label lblValue)
    {
        var pnl = new Panel
        {
            Size      = new Size(340, 144),
            BackColor = Color.FromArgb(25, 30, 50),
        };
        pnl.Controls.Add(new Label
        {
            Text      = title,
            Location  = new Point(8, 6),
            AutoSize  = true,
            Font      = new Font("Segoe UI", 9),
            ForeColor = Color.Silver,
        });
        var lbl = new Label
        {
            Text      = "—",
            Location  = new Point(8, 28),
            Size      = new Size(324, 90),
            Font      = new Font("Courier New", 48, FontStyle.Bold),
            ForeColor = Color.DimGray,
            TextAlign = ContentAlignment.MiddleRight,
            AutoSize  = false,
        };
        pnl.Controls.Add(lbl);
        lblValue = lbl;
        return pnl;
    }

    private void RefreshPorts()
    {
        var ports = SerialPort.GetPortNames();
        _cmbPort.Items.Clear();
        if (ports.Length > 0)
        {
            _cmbPort.Items.AddRange(ports);
            // prefer current port if in list
            int idx = Array.IndexOf(ports, _adc.PortName);
            _cmbPort.SelectedIndex = idx >= 0 ? idx : 0;
        }
        _btnConn.Enabled = ports.Length > 0;
    }

    private void BtnMonConn_Click(object? sender, EventArgs e)
    {
        if (_adc.IsConnected)
        {
            _adc.Close();
        }
        else
        {
            if (_cmbPort.SelectedItem is not string port) return;
            try { _adc.Open(port); }
            catch (Exception ex)
            {
                AppendLog($"ОШИБКА: {ex.Message}", Color.Red);
            }
        }
    }

    private void UpdateMonitorConn(bool connected)
    {
        _dotConn.BackColor = connected ? Color.LimeGreen : Color.Gray;
        _lblConn.Text      = connected ? $"Подключено: {_adc.PortName}  4800/Even/8/1" : "Нет подключения";
        _lblConn.ForeColor = connected ? Color.LimeGreen : Color.Gray;
        _btnConn.Text      = connected ? "Отключить" : "Подключить";
        _btnConn.BackColor = connected ? Color.FromArgb(120, 40, 0) : Color.FromArgb(0, 100, 50);
        _cmbPort.Enabled   = !connected;

        if (connected)
            AppendLog($"=== Подключено: {_adc.PortName}  4800/Even/8/1 ===", Color.LimeGreen);
        else
            AppendLog("=== Отключено ===", Color.Gray);
    }

    // ── Tab: Калибровка Статика ────────────────────────────────────────────

    private TabPage BuildCalibStaticTab()
    {
        var tab = new TabPage("Калибровка Статика");

        // ── Выбор канала
        var rbCh0 = new RadioButton
        {
            Text = "CH0 — Основной", Location = new Point(20, 16),
            AutoSize = true, Font = new Font("Segoe UI", 11), Checked = true,
        };
        var rbCh1 = new RadioButton
        {
            Text = "CH1 — Резервный", Location = new Point(220, 16),
            AutoSize = true, Font = new Font("Segoe UI", 11),
        };
        rbCh0.CheckedChanged += (_, _) => { if (rbCh0.Checked) { _calibUseCh0 = true;  LoadCalibPoints(); UpdateLiveAdcLabel(); } };
        rbCh1.CheckedChanged += (_, _) => { if (rbCh1.Checked) { _calibUseCh0 = false; LoadCalibPoints(); UpdateLiveAdcLabel(); } };

        // ── Живой код АЦП
        tab.Controls.Add(new Label
        {
            Text = "Текущий код АЦП:", Location = new Point(20, 54),
            AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray,
        });
        _lblLiveAdc = new Label
        {
            Text = "—", Location = new Point(165, 50),
            AutoSize = true, Font = new Font("Courier New", 13, FontStyle.Bold), ForeColor = Color.DodgerBlue,
        };
        var btnCapture = new Button
        {
            Text = "Захватить", Location = new Point(290, 46), Size = new Size(110, 28),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9),
            BackColor = Color.FromArgb(25, 45, 90), ForeColor = Color.White,
        };
        btnCapture.FlatAppearance.BorderSize = 0;
        btnCapture.Click += (_, _) =>
        {
            int code = _calibUseCh0 ? _lastCh0 : _lastCh1;
            if (code == 0) return;
            int row = _dgvCalib.Rows.Add();
            _dgvCalib.Rows[row].Cells[0].Value = code;
            _dgvCalib.CurrentCell = _dgvCalib.Rows[row].Cells[1];
            _dgvCalib.BeginEdit(true);
        };

        // ── Таблица точек
        _dgvCalib = new DataGridView
        {
            Location  = new Point(20, 82),
            Size      = new Size(380, 190),
            Font      = new Font("Segoe UI", 10),
            AllowUserToAddRows          = false,
            AllowUserToDeleteRows       = false,
            AllowUserToResizeRows       = false,
            RowHeadersVisible           = false,
            SelectionMode               = DataGridViewSelectionMode.FullRowSelect,
            EditMode                    = DataGridViewEditMode.EditOnEnter,
            BackgroundColor             = Color.White,
            BorderStyle                 = BorderStyle.FixedSingle,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
            ColumnHeadersHeight         = 28,
        };
        _dgvCalib.RowTemplate.Height = 24;
        _dgvCalib.Columns.Add(new DataGridViewTextBoxColumn
            { HeaderText = "Код АЦП",   Width = 140, SortMode = DataGridViewColumnSortMode.NotSortable });
        _dgvCalib.Columns.Add(new DataGridViewTextBoxColumn
            { HeaderText = "Масса (т)", Width = 140, SortMode = DataGridViewColumnSortMode.NotSortable });

        var btnAdd = new Button
        {
            Text = "Добавить строку", Location = new Point(20, 282), Size = new Size(160, 28),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9),
        };
        btnAdd.Click += (_, _) =>
        {
            int row = _dgvCalib.Rows.Add();
            _dgvCalib.CurrentCell = _dgvCalib.Rows[row].Cells[0];
            _dgvCalib.BeginEdit(true);
        };

        var btnDel = new Button
        {
            Text = "Удалить выбранную", Location = new Point(190, 282), Size = new Size(170, 28),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9),
        };
        btnDel.Click += (_, _) =>
        {
            if (_dgvCalib.SelectedRows.Count > 0)
                _dgvCalib.Rows.Remove(_dgvCalib.SelectedRows[0]);
        };

        // ── k и b
        tab.Controls.Add(new Label
            { Text = "k  =", Location = new Point(20, 326), AutoSize = true, Font = new Font("Segoe UI", 10) });
        _txtK = new TextBox
            { Location = new Point(56, 322), Size = new Size(150, 26), Font = new Font("Courier New", 10), Text = "0" };
        tab.Controls.Add(new Label
            { Text = "b  =", Location = new Point(224, 326), AutoSize = true, Font = new Font("Segoe UI", 10) });
        _txtB = new TextBox
            { Location = new Point(260, 322), Size = new Size(150, 26), Font = new Font("Courier New", 10), Text = "0" };

        tab.Controls.Add(new Label
        {
            Text = "Масса = k × Код + b",
            Location = new Point(20, 358), AutoSize = true,
            Font = new Font("Segoe UI", 9), ForeColor = Color.Gray,
        });

        var btnLsq = new Button
        {
            Text = "Рассчитать МНК", Location = new Point(20, 388), Size = new Size(180, 34),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10),
            BackColor = Color.FromArgb(25, 60, 120), ForeColor = Color.White,
        };
        btnLsq.FlatAppearance.BorderSize = 0;
        btnLsq.Click += (_, _) =>
        {
            var pts = ReadGridPoints();
            if (pts.Count < 2)
            {
                MessageBox.Show("Нужно минимум 2 точки.", "МНК", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var (k, b) = CalibrationService.CalculateLsq(pts);
            _txtK.Text = k.ToString("G8", CultureInfo.InvariantCulture);
            _txtB.Text = b.ToString("G8", CultureInfo.InvariantCulture);
        };

        var btnSave = new Button
        {
            Text = "Применить и сохранить", Location = new Point(212, 388), Size = new Size(220, 34),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10),
            BackColor = Color.FromArgb(0, 110, 40), ForeColor = Color.White,
        };
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.Click += BtnCalibSave_Click;

        tab.Controls.AddRange(new Control[]
        {
            rbCh0, rbCh1,
            _lblLiveAdc, btnCapture,
            _dgvCalib, btnAdd, btnDel,
            _txtK, _txtB,
            btnLsq, btnSave,
        });

        LoadCalibPoints();
        return tab;
    }

    private async void BtnCalibSave_Click(object? sender, EventArgs e)
    {
        if (!double.TryParse(_txtK.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double k) ||
            !double.TryParse(_txtB.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double b))
        {
            MessageBox.Show("Некорректные значения k или b.", "Сохранение",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        var ch    = _calibUseCh0 ? _calib.Profile.Ch0 : _calib.Profile.Ch1;
        ch.K      = k;
        ch.B      = b;
        ch.Points = ReadGridPoints();
        try
        {
            await _calib.SaveAsync();
            MessageBox.Show("Калибровка сохранена.", "Сохранение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка сохранения:\n{ex.Message}", "Сохранение",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadCalibPoints()
    {
        if (_dgvCalib == null) return;
        _dgvCalib.Rows.Clear();
        var ch = _calibUseCh0 ? _calib.Profile.Ch0 : _calib.Profile.Ch1;
        foreach (var p in ch.Points)
            _dgvCalib.Rows.Add(p.AdcCode, p.MassTonnes.ToString("G8", CultureInfo.InvariantCulture));
        _txtK.Text = ch.K.ToString("G8", CultureInfo.InvariantCulture);
        _txtB.Text = ch.B.ToString("G8", CultureInfo.InvariantCulture);
    }

    private List<CalibrationPoint> ReadGridPoints()
    {
        var pts = new List<CalibrationPoint>();
        foreach (DataGridViewRow row in _dgvCalib.Rows)
        {
            if (int.TryParse(row.Cells[0].Value?.ToString(), NumberStyles.Integer,
                    CultureInfo.InvariantCulture, out int code) &&
                double.TryParse(row.Cells[1].Value?.ToString(), NumberStyles.Float,
                    CultureInfo.InvariantCulture, out double mass))
                pts.Add(new CalibrationPoint { AdcCode = code, MassTonnes = mass });
        }
        return pts;
    }

    private void UpdateLiveAdcLabel()
    {
        if (_lblLiveAdc != null)
        {
            int code = _calibUseCh0 ? _lastCh0 : _lastCh1;
            _lblLiveAdc.Text = code == 0 ? "—" : code.ToString();
        }
        if (_lblLiveAdcD != null)
        {
            int dynCode = _adc.Channel == ActiveChannel.Main ? _lastCh0 : _lastCh1;
            _lblLiveAdcD.Text = dynCode == 0 ? "—" : dynCode.ToString();
        }
    }

    private TabPage BuildCalibDynamicTab()
    {
        var tab = new TabPage("Калибровка Динамика");

        // ── Живой код АЦП (активный канал)
        tab.Controls.Add(new Label
        {
            Text = "Текущий код АЦП:", Location = new Point(20, 16),
            AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray,
        });
        _lblLiveAdcD = new Label
        {
            Text = "—", Location = new Point(165, 12),
            AutoSize = true, Font = new Font("Courier New", 13, FontStyle.Bold), ForeColor = Color.DodgerBlue,
        };
        tab.Controls.Add(_lblLiveAdcD);

        // ── Направление → ─────────────────────────────────────────────────
        tab.Controls.Add(MakeSectionLabel("── Направление  →  ──────────────────", new Point(20, 44)));

        tab.Controls.Add(new Label { Text = "K→  =", Location = new Point(20, 72), AutoSize = true, Font = new Font("Segoe UI", 10) });
        _txtKPlus = new TextBox { Location = new Point(72, 68), Size = new Size(160, 26), Font = new Font("Courier New", 10), Text = "0" };

        tab.Controls.Add(new Label { Text = "Авторасчёт:", Location = new Point(20, 104), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray });

        tab.Controls.Add(new Label { Text = "Код АЦП:", Location = new Point(36, 126), AutoSize = true, Font = new Font("Segoe UI", 9) });
        _txtCodePlus = new TextBox { Location = new Point(160, 122), Size = new Size(120, 24), Font = new Font("Courier New", 9) };
        var btnCapPlus = new Button
        {
            Text = "Захватить", Location = new Point(288, 120), Size = new Size(100, 26),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8),
            BackColor = Color.FromArgb(25, 45, 90), ForeColor = Color.White,
        };
        btnCapPlus.FlatAppearance.BorderSize = 0;
        btnCapPlus.Click += (_, _) =>
        {
            int code = _adc.Channel == ActiveChannel.Main ? _lastCh0 : _lastCh1;
            if (code != 0) _txtCodePlus.Text = code.ToString();
        };

        tab.Controls.Add(new Label { Text = "Эталон (т):", Location = new Point(36, 154), AutoSize = true, Font = new Font("Segoe UI", 9) });
        _txtMassPlus = new TextBox { Location = new Point(160, 150), Size = new Size(120, 24), Font = new Font("Courier New", 9) };

        var btnCalcPlus = new Button
        {
            Text = "Рассчитать K→", Location = new Point(36, 182), Size = new Size(180, 30),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9),
            BackColor = Color.FromArgb(25, 60, 120), ForeColor = Color.White,
        };
        btnCalcPlus.FlatAppearance.BorderSize = 0;
        btnCalcPlus.Click += (_, _) =>
        {
            if (int.TryParse(_txtCodePlus.Text, out int code) &&
                double.TryParse(_txtMassPlus.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double mass) &&
                code != 0)
                _txtKPlus.Text = (mass / code).ToString("G8", CultureInfo.InvariantCulture);
            else
                MessageBox.Show("Введите корректный код АЦП и эталонную массу.", "Авторасчёт",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        };

        // ── Направление ← ─────────────────────────────────────────────────
        tab.Controls.Add(MakeSectionLabel("── Направление  ←  ──────────────────", new Point(20, 224)));

        tab.Controls.Add(new Label { Text = "K←  =", Location = new Point(20, 252), AutoSize = true, Font = new Font("Segoe UI", 10) });
        _txtKMinus = new TextBox { Location = new Point(72, 248), Size = new Size(160, 26), Font = new Font("Courier New", 10), Text = "0" };

        tab.Controls.Add(new Label { Text = "Авторасчёт:", Location = new Point(20, 284), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray });

        tab.Controls.Add(new Label { Text = "Код АЦП:", Location = new Point(36, 306), AutoSize = true, Font = new Font("Segoe UI", 9) });
        _txtCodeMinus = new TextBox { Location = new Point(160, 302), Size = new Size(120, 24), Font = new Font("Courier New", 9) };
        var btnCapMinus = new Button
        {
            Text = "Захватить", Location = new Point(288, 300), Size = new Size(100, 26),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8),
            BackColor = Color.FromArgb(25, 45, 90), ForeColor = Color.White,
        };
        btnCapMinus.FlatAppearance.BorderSize = 0;
        btnCapMinus.Click += (_, _) =>
        {
            int code = _adc.Channel == ActiveChannel.Main ? _lastCh0 : _lastCh1;
            if (code != 0) _txtCodeMinus.Text = code.ToString();
        };

        tab.Controls.Add(new Label { Text = "Эталон (т):", Location = new Point(36, 334), AutoSize = true, Font = new Font("Segoe UI", 9) });
        _txtMassMinus = new TextBox { Location = new Point(160, 330), Size = new Size(120, 24), Font = new Font("Courier New", 9) };

        var btnCalcMinus = new Button
        {
            Text = "Рассчитать K←", Location = new Point(36, 362), Size = new Size(180, 30),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9),
            BackColor = Color.FromArgb(25, 60, 120), ForeColor = Color.White,
        };
        btnCalcMinus.FlatAppearance.BorderSize = 0;
        btnCalcMinus.Click += (_, _) =>
        {
            if (int.TryParse(_txtCodeMinus.Text, out int code) &&
                double.TryParse(_txtMassMinus.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double mass) &&
                code != 0)
                _txtKMinus.Text = (mass / code).ToString("G8", CultureInfo.InvariantCulture);
            else
                MessageBox.Show("Введите корректный код АЦП и эталонную массу.", "Авторасчёт",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        };

        // ── Сохранение ─────────────────────────────────────────────────────
        tab.Controls.Add(new Label
        {
            Text = "Масса = K × Код",
            Location = new Point(20, 406), AutoSize = true,
            Font = new Font("Segoe UI", 9), ForeColor = Color.Gray,
        });

        var btnSave = new Button
        {
            Text = "Применить и сохранить", Location = new Point(20, 430), Size = new Size(220, 34),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10),
            BackColor = Color.FromArgb(0, 110, 40), ForeColor = Color.White,
        };
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.Click += BtnCalibDynSave_Click;

        tab.Controls.AddRange(new Control[]
        {
            _txtKPlus, btnCapPlus, _txtCodePlus, _txtMassPlus, btnCalcPlus,
            _txtKMinus, btnCapMinus, _txtCodeMinus, _txtMassMinus, btnCalcMinus,
            btnSave,
        });

        LoadCalibDynamic();
        return tab;
    }

    private static Label MakeSectionLabel(string text, Point location) => new()
    {
        Text      = text,
        Location  = location,
        AutoSize  = true,
        Font      = new Font("Segoe UI", 9, FontStyle.Bold),
        ForeColor = Color.FromArgb(80, 100, 140),
    };

    private async void BtnCalibDynSave_Click(object? sender, EventArgs e)
    {
        if (!double.TryParse(_txtKPlus.Text,  NumberStyles.Float, CultureInfo.InvariantCulture, out double kp) ||
            !double.TryParse(_txtKMinus.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double km))
        {
            MessageBox.Show("Некорректные значения K→ или K←.", "Сохранение",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        _calib.Profile.Dynamic.KPlus  = kp;
        _calib.Profile.Dynamic.KMinus = km;
        try
        {
            await _calib.SaveAsync();
            MessageBox.Show("Калибровка динамики сохранена.", "Сохранение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка сохранения:\n{ex.Message}", "Сохранение",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadCalibDynamic()
    {
        if (_txtKPlus == null) return;
        _txtKPlus .Text = _calib.Profile.Dynamic.KPlus .ToString("G8", CultureInfo.InvariantCulture);
        _txtKMinus.Text = _calib.Profile.Dynamic.KMinus.ToString("G8", CultureInfo.InvariantCulture);
    }

    private TabPage BuildSettingsTab()
    {
        var tab = new TabPage("Настройки");
        int y = 16;

        void Row(string label, Control ctrl)
        {
            tab.Controls.Add(new Label { Text = label, Location = new Point(20, y + 4), AutoSize = true, Font = new Font("Segoe UI", 10) });
            ctrl.Location = new Point(220, y);
            ctrl.Size     = new Size(200, ctrl.PreferredSize.Height);
            tab.Controls.Add(ctrl);
            y += 36;
        }

        var cmbPort = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        var ports   = SerialPort.GetPortNames();
        if (ports.Length > 0) { cmbPort.Items.AddRange(ports); cmbPort.SelectedIndex = 0; }
        Row("COM-порт:", cmbPort);

        Row("НПВ (т):",             new TextBox { Text = "150" });
        var cmbDisc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        cmbDisc.Items.AddRange(new[] { "0.1 т", "0.05 т", "0.01 т" });
        cmbDisc.SelectedIndex = 0;
        Row("Дискретность:",        cmbDisc);
        Row("Лимит нуля (% НПВ):", new TextBox { Text = "1" });
        Row("Окно Динамики (мс):",  new TextBox { Text = "1500" });
        Row("Таймаут тележек (мс):", new TextBox { Text = "30000" });
        Row("Новый пароль:",        new TextBox { UseSystemPasswordChar = true });

        y += 8;
        var btnSave = new Button
        {
            Text = "Сохранить", Location = new Point(20, y), Size = new Size(130, 34),
            FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(25, 45, 90), ForeColor = Color.White,
            Font = new Font("Segoe UI", 10),
        };
        btnSave.Click += (_, _) =>
            MessageBox.Show("Сохранение настроек — в разработке.", "Настройки",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        tab.Controls.Add(btnSave);
        return tab;
    }

    // ── Admin unlock ────────────────────────────────────────────────────────

    private void BtnAdmin_Click(object? sender, EventArgs e)
    {
        if (_adminUnlocked)
        {
            _adminUnlocked      = false;
            _btnAdmin.Text      = "🔒 Войти как администратор";
            _btnAdmin.BackColor = Color.FromArgb(80, 60, 20);
            SetAdminTabs(false);
        }
        else
        {
            using var dlg = new PasswordDialog();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            _adminUnlocked      = true;
            _btnAdmin.Text      = "🔓 Выйти из режима администратора";
            _btnAdmin.BackColor = Color.FromArgb(20, 80, 30);
            SetAdminTabs(true);
        }
    }

    private void SetAdminTabs(bool enabled)
    {
        _tabCalibS.Enabled = enabled;
        _tabCalibD.Enabled = enabled;
        _tabSett.Enabled   = enabled;
    }

    // ── Lifecycle ───────────────────────────────────────────────────────────

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        _adc.RawFrameReceived  += OnRawFrame;
        _adc.ConnectionChanged += OnConnectionChanged;
        _rateTimer.Start();
        UpdateMonitorConn(_adc.IsConnected);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _adc.RawFrameReceived  -= OnRawFrame;
        _adc.ConnectionChanged -= OnConnectionChanged;
        _rateTimer.Stop();
        base.OnFormClosed(e);
    }

    private void OnConnectionChanged(object? sender, bool connected)
    {
        if (InvokeRequired) { BeginInvoke(() => OnConnectionChanged(sender, connected)); return; }
        UpdateMonitorConn(connected);
    }

    private void OnRawFrame(object? sender, byte[] raw)
    {
        if (InvokeRequired) { BeginInvoke(() => OnRawFrame(sender, raw)); return; }

        var frame = AdcFrame.Parse(raw);
        _frameCount++;

        // live values
        if (frame.Valid)
        {
            _lastCh0          = frame.Ch0;
            _lastCh1          = frame.Ch1;
            _lblCh0.Text      = frame.Ch0.ToString();
            _lblCh1.Text      = frame.Ch1.ToString();
            _lblCh0.ForeColor = Color.DodgerBlue;
            _lblCh1.ForeColor = Color.DodgerBlue;
            UpdateLiveAdcLabel();
        }
        else
        {
            _lblCh0.ForeColor = Color.Red;
            _lblCh1.ForeColor = Color.Red;
        }

        // log
        if (!_chkLog.Checked) return;

        string bytes = string.Join("  ", raw.Select(b => b.ToString("D3")));
        string time  = DateTime.Now.ToString("HH:mm:ss.fff");

        if (frame.Valid)
            AppendLog($"{time}  [{bytes}]  CH0={frame.Ch0,5}  CH1={frame.Ch1,5}", Color.LightGray);
        else
            AppendLog($"{time}  [{bytes}]  INVALID ({raw.Length} байт)", Color.OrangeRed);
    }

    private void AppendLog(string text, Color color)
    {
        var prev = _rtbLog.SelectionColor;
        _rtbLog.SelectionColor = color;
        _rtbLog.AppendText(text + "\n");
        _rtbLog.SelectionColor = prev;

        // trim to 300 lines
        if (_rtbLog.Lines.Length > 300)
        {
            int cut = _rtbLog.GetFirstCharIndexFromLine(50);
            _rtbLog.Select(0, cut);
            _rtbLog.SelectedText = "";
        }

        _rtbLog.ScrollToCaret();
    }
}