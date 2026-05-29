using System.IO.Ports;
using Vesy13.Models;
using Vesy13.Services;

namespace Vesy13.Forms;

public class ServiceForm : Form
{
    private readonly AdcService _adc;
    private bool _adminUnlocked;

    // ── Monitoring ─────────────────────────────────────────────────────────
    private Label _lblCh0  = null!;
    private Label _lblCh1  = null!;
    private Label _lblRate = null!;
    private int   _frameCount;
    private System.Windows.Forms.Timer _rateTimer = null!;

    // ── Admin lock ─────────────────────────────────────────────────────────
    private Button   _btnAdmin  = null!;
    private TabPage  _tabCalibS = null!;
    private TabPage  _tabCalibD = null!;
    private TabPage  _tabSett   = null!;

    public ServiceForm(AdcService adc)
    {
        _adc = adc;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text          = "Сервисный режим";
        ClientSize    = new Size(700, 520);
        MinimumSize   = new Size(600, 460);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor     = Color.FromArgb(240, 242, 245);

        // ── Admin button ───────────────────────────────────────────────────
        _btnAdmin = new Button
        {
            Text      = "🔒 Войти как администратор",
            Anchor    = AnchorStyles.Top | AnchorStyles.Right,
            Location  = new Point(430, 8),
            Size      = new Size(260, 28),
            FlatStyle = FlatStyle.Flat,
            Font      = new Font("Segoe UI", 9),
            BackColor = Color.FromArgb(80, 60, 20),
            ForeColor = Color.White,
        };
        _btnAdmin.FlatAppearance.BorderSize = 0;
        _btnAdmin.Click += BtnAdmin_Click;

        // ── Tabs ───────────────────────────────────────────────────────────
        var tabs = new TabControl
        {
            Location = new Point(0, 44),
            Anchor   = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            Size     = new Size(700, 440),
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

        // ── Back button ────────────────────────────────────────────────────
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
        pnlStatus.Controls.Add(btnBack);

        Controls.AddRange(new Control[] { _btnAdmin, tabs, pnlStatus });

        _rateTimer = new System.Windows.Forms.Timer { Interval = 1000 };
        _rateTimer.Tick += (_, _) => { _lblRate.Text = $"{_frameCount} фр/с"; _frameCount = 0; };
    }

    // ── Tab: Канал (без пароля) ────────────────────────────────────────────
    private TabPage BuildChannelTab()
    {
        var tab = new TabPage("Канал");

        var lbl = new Label
        {
            Text      = "Выбор активного канала",
            Location  = new Point(20, 16),
            AutoSize  = true,
            Font      = new Font("Segoe UI", 11, FontStyle.Bold),
        };

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

        var hint = new Label
        {
            Text      = "Изменение канала применяется немедленно и не требует пароля.",
            Location  = new Point(20, 136),
            AutoSize  = true,
            Font      = new Font("Segoe UI", 9),
            ForeColor = Color.Gray,
        };

        tab.Controls.AddRange(new Control[] { lbl, rbMain, rbBackup, hint });
        return tab;
    }

    // ── Tab: Мониторинг ────────────────────────────────────────────────────
    private TabPage BuildMonitorTab()
    {
        var tab = new TabPage("Мониторинг");

        Label Make(string text, int x, int y, Font? font = null, Color? color = null)
        {
            var l = new Label { Text = text, Location = new Point(x, y), AutoSize = true, Font = font ?? new Font("Segoe UI", 10) };
            if (color.HasValue) l.ForeColor = color.Value;
            tab.Controls.Add(l);
            return l;
        }

        Make("CH0 — Основной канал", 20, 20, new Font("Segoe UI", 10, FontStyle.Bold));
        Make("Код АЦП:", 20, 48);
        _lblCh0 = Make("—", 110, 44, new Font("Courier New", 16, FontStyle.Bold), Color.DodgerBlue);

        Make("CH1 — Резервный канал", 20, 100, new Font("Segoe UI", 10, FontStyle.Bold));
        Make("Код АЦП:", 20, 128);
        _lblCh1 = Make("—", 110, 124, new Font("Courier New", 16, FontStyle.Bold), Color.DodgerBlue);

        Make("Частота:", 20, 180);
        _lblRate = Make("— фр/с", 110, 180, null, Color.Gray);

        return tab;
    }

    // ── Tab: Калибровка Статика ────────────────────────────────────────────
    private TabPage BuildCalibStaticTab()
    {
        var tab = new TabPage("Калибровка Статика");
        tab.Controls.Add(new Label
        {
            Text      = "TODO (Этап 5а):\n\n" +
                        "• Выбор канала: CH0 / CH1\n" +
                        "• Таблица точек (код АЦП ↔ масса т)\n" +
                        "• Кнопки: Добавить / Удалить / Рассчитать\n" +
                        "• Поля k и b — ручной ввод или результат МНК",
            Location  = new Point(20, 20),
            AutoSize  = true,
            Font      = new Font("Segoe UI", 10),
            ForeColor = Color.Gray,
        });
        return tab;
    }

    // ── Tab: Калибровка Динамика ───────────────────────────────────────────
    private TabPage BuildCalibDynamicTab()
    {
        var tab = new TabPage("Калибровка Динамика");
        tab.Controls.Add(new Label
        {
            Text      = "TODO (Этап 5б):\n\n" +
                        "• Коэффициент → (Plus)\n" +
                        "• Коэффициент ← (Minus)\n" +
                        "• Ввод эталонной массы → авторасчёт коэффициента\n" +
                        "• Ручной ввод коэффициентов",
            Location  = new Point(20, 20),
            AutoSize  = true,
            Font      = new Font("Segoe UI", 10),
            ForeColor = Color.Gray,
        });
        return tab;
    }

    // ── Tab: Настройки ─────────────────────────────────────────────────────
    private TabPage BuildSettingsTab()
    {
        var tab = new TabPage("Настройки");
        int y   = 16;

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

        var txtNpv = new TextBox { Text = "150" };
        Row("НПВ (т):", txtNpv);

        var cmbDisc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        cmbDisc.Items.AddRange(new[] { "0.1 т", "0.05 т", "0.01 т" });
        cmbDisc.SelectedIndex = 0;
        Row("Дискретность:", cmbDisc);

        var txtZeroLim = new TextBox { Text = "1" };
        Row("Лимит нуля (% НПВ):", txtZeroLim);

        var txtWindow = new TextBox { Text = "1500" };
        Row("Окно Динамики (мс):", txtWindow);

        var txtTimeout = new TextBox { Text = "30000" };
        Row("Таймаут тележек (мс):", txtTimeout);

        var txtPwd = new TextBox { UseSystemPasswordChar = true };
        Row("Новый пароль:", txtPwd);

        y += 8;
        var btnSave = new Button
        {
            Text      = "Сохранить",
            Location  = new Point(20, y),
            Size      = new Size(130, 34),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(25, 45, 90),
            ForeColor = Color.White,
            Font      = new Font("Segoe UI", 10),
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
            _adminUnlocked    = false;
            _btnAdmin.Text    = "🔒 Войти как администратор";
            _btnAdmin.BackColor = Color.FromArgb(80, 60, 20);
            SetAdminTabs(false);
        }
        else
        {
            using var dlg = new PasswordDialog();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            _adminUnlocked    = true;
            _btnAdmin.Text    = "🔓 Выйти из режима администратора";
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
        _adc.FrameReceived += OnFrame;
        _rateTimer.Start();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _adc.FrameReceived -= OnFrame;
        _rateTimer.Stop();
        base.OnFormClosed(e);
    }

    private void OnFrame(object? sender, AdcFrame frame)
    {
        if (InvokeRequired) { BeginInvoke(() => OnFrame(sender, frame)); return; }
        _lblCh0.Text = frame.Ch0.ToString();
        _lblCh1.Text = frame.Ch1.ToString();
        _frameCount++;
    }
}