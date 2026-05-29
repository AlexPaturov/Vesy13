using System.IO.Ports;
using Vesy13.Models;
using Vesy13.Services;

namespace Vesy13.Forms;

public class ServiceForm : Form
{
    private readonly AdcService _adc;
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
    private System.Windows.Forms.Timer _rateTimer = null!;

    // ── Admin lock ─────────────────────────────────────────────────────────
    private Button  _btnAdmin  = null!;
    private TabPage _tabCalibS = null!;
    private TabPage _tabCalibD = null!;
    private TabPage _tabSett   = null!;

    public ServiceForm(AdcService adc)
    {
        _adc = adc;
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

    // ── Tabs: Calibration / Settings (stubs) ──────────────────────────────

    private TabPage BuildCalibStaticTab()
    {
        var tab = new TabPage("Калибровка Статика");
        tab.Controls.Add(new Label
        {
            Text      = "TODO (Этап 5а):\n\n• Выбор канала: CH0 / CH1\n• Таблица точек (код АЦП ↔ масса т)\n• Кнопки: Добавить / Удалить / Рассчитать\n• Поля k и b — ручной ввод или МНК",
            Location  = new Point(20, 20), AutoSize = true,
            Font      = new Font("Segoe UI", 10), ForeColor = Color.Gray,
        });
        return tab;
    }

    private TabPage BuildCalibDynamicTab()
    {
        var tab = new TabPage("Калибровка Динамика");
        tab.Controls.Add(new Label
        {
            Text      = "TODO (Этап 5б):\n\n• Коэффициент → (Plus)\n• Коэффициент ← (Minus)\n• Ввод эталонной массы → авторасчёт\n• Ручной ввод коэффициентов",
            Location  = new Point(20, 20), AutoSize = true,
            Font      = new Font("Segoe UI", 10), ForeColor = Color.Gray,
        });
        return tab;
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
            _lblCh0.Text      = frame.Ch0.ToString();
            _lblCh1.Text      = frame.Ch1.ToString();
            _lblCh0.ForeColor = Color.DodgerBlue;
            _lblCh1.ForeColor = Color.DodgerBlue;
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