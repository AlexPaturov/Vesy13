using System.Globalization;
using System.IO.Ports;
using Vesy13.Application;
using Vesy13.Models;
using Vesy13.Services.Hardware;
using Vesy13.Services.Repositories;

namespace Vesy13.Forms;

/// <summary>
/// Форма обслуживания: монитор АЦП, редактор тарировочных точек (статика/динамика),
/// вкладка администратора защищена паролем.
/// </summary>
public partial class ServiceForm : Form
{
    private SimA04Reader    _sim   = null!;
    private LocalRepository _calib = null!;
    private bool _adminUnlocked;
    private bool _calibUseCh0 = true;
    private int  _frameCount;
    private int  _lastCh0;
    private int  _lastCh1;

    public ServiceForm()
    {
        InitializeComponent();
    }

    public ServiceForm(SimA04Reader adc, LocalRepository calib)
    {
        _sim   = adc;
        _calib = calib;
        InitializeComponent();
    }

    private void ApplyFonts()
    {
        // Channel tab
        _btnAdmin.Font        = UiFonts.Body;
        _lblChannelTitle.Font = UiFonts.SubHeaderBold;
        _rbMain.Font          = UiFonts.NavButton;
        _rbBackup.Font        = UiFonts.NavButton;
        _lblChannelNote.Font  = UiFonts.Body;
        // Monitor tab
        _cmbPort.Font         = UiFonts.Medium;
        _btnConn.Font         = UiFonts.Body;
        _btnPortRefresh.Font  = UiFonts.SubHeader;
        _lblConn.Font         = UiFonts.Body;
        _lblRate.Font         = UiFonts.Body;
        _lblCh0Cap.Font       = UiFonts.Body;
        _lblCh0.Font          = UiFonts.MonitorDisplay;
        _lblCh1Cap.Font       = UiFonts.Body;
        _lblCh1.Font          = UiFonts.MonitorDisplay;
        _chkLog.Font          = UiFonts.Body;
        _btnClearLog.Font     = UiFonts.Body;
        _rtbLog.Font          = UiFonts.MonoSmall;
        // CalibStatic tab
        _rbCh0Calib.Font      = UiFonts.SubHeader;
        _rbCh1Calib.Font      = UiFonts.SubHeader;
        _lblLiveAdcCap.Font   = UiFonts.Body;
        _lblLiveAdc.Font      = UiFonts.MonoLiveAdc;
        _btnCapture.Font      = UiFonts.Body;
        _dgvCalib.Font        = UiFonts.GridBody;
        _btnAddRow.Font       = UiFonts.Body;
        _btnDelRow.Font       = UiFonts.Body;
        _lblKEquals.Font      = UiFonts.Medium;
        _txtK.Font            = UiFonts.Mono;
        _lblBEquals.Font      = UiFonts.Medium;
        _txtB.Font            = UiFonts.Mono;
        _lblFormula.Font      = UiFonts.Body;
        _btnLsq.Font          = UiFonts.Medium;
        _btnCalibSave.Font    = UiFonts.Medium;
        // CalibDynamic tab
        _lblLiveAdcCapD.Font  = UiFonts.Body;
        _lblLiveAdcD.Font     = UiFonts.MonoLiveAdc;
        _lblSecPlus.Font      = UiFonts.BodyBold;
        _lblKPlusEquals.Font  = UiFonts.Medium;
        _txtKPlus.Font        = UiFonts.Mono;
        _lblAutoCalcPlus.Font = UiFonts.Body;
        _lblCodePlusCap.Font  = UiFonts.Body;
        _txtCodePlus.Font     = UiFonts.MonoSmall;
        _btnCapPlus.Font      = UiFonts.Small;
        _lblMassPlusCap.Font  = UiFonts.Body;
        _txtMassPlus.Font     = UiFonts.MonoSmall;
        _btnCalcPlus.Font     = UiFonts.Body;
        _lblSecMinus.Font     = UiFonts.BodyBold;
        _lblKMinusEquals.Font = UiFonts.Medium;
        _txtKMinus.Font       = UiFonts.Mono;
        _lblAutoCalcMinus.Font= UiFonts.Body;
        _lblCodeMinusCap.Font = UiFonts.Body;
        _txtCodeMinus.Font    = UiFonts.MonoSmall;
        _btnCapMinus.Font     = UiFonts.Small;
        _lblMassMinusCap.Font = UiFonts.Body;
        _txtMassMinus.Font    = UiFonts.MonoSmall;
        _btnCalcMinus.Font    = UiFonts.Body;
        _lblFormulaD.Font     = UiFonts.Body;
        _btnCalibDynSave.Font = UiFonts.Medium;
        // Settings tab
        _lblPortCap.Font         = UiFonts.Medium;
        _lblNpvCap.Font          = UiFonts.Medium;
        _lblDiscCap.Font         = UiFonts.Medium;
        _lblZeroCap.Font         = UiFonts.Medium;
        _lblDynWinCap.Font       = UiFonts.Medium;
        _lblBogieTimeoutCap.Font = UiFonts.Medium;
        _lblPasswordCap.Font     = UiFonts.Medium;
        _btnSaveSettings.Font    = UiFonts.Medium;
        // General
        _tabs.Font    = UiFonts.Medium;
        _btnBack.Font = UiFonts.Small;
    }

    // ── Lifecycle ───────────────────────────────────────────────────────────

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ApplyFonts();
        if (DesignMode || _sim is null) return;
        AuditLogger.Action(AuditLogger.FormOpened, "Form", "ServiceForm");
        _sim.RawFrameReceived  += OnRawFrame;
        _sim.ConnectionChanged += OnConnectionChanged;
        _rateTimer.Start();
        _rbMain.Checked   = _sim.Channel == ActiveChannel.Main;
        _rbBackup.Checked = _sim.Channel == ActiveChannel.Backup;
        RefreshPorts();
        LoadCalibPoints();
        LoadCalibDynamic();
        SetAdminTabs(false);
        UpdateMonitorConn(_sim.IsConnected);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (!DesignMode && _sim is not null)
        {
            _sim.RawFrameReceived  -= OnRawFrame;
            _sim.ConnectionChanged -= OnConnectionChanged;
            _rateTimer.Stop();
        }
        base.OnFormClosed(e);
    }

    // ── Designer event handlers ─────────────────────────────────────────────

    private void BtnBack_Click(object? sender, EventArgs e)         => Close();
    private void BtnPortRefresh_Click(object? sender, EventArgs e)  => RefreshPorts();
    private void BtnClearLog_Click(object? sender, EventArgs e)     => _rtbLog.Clear();
    private void BtnDelRow_Click(object? sender, EventArgs e)
    {
        if (_dgvCalib.SelectedRows.Count > 0)
            _dgvCalib.Rows.Remove(_dgvCalib.SelectedRows[0]);
    }
    private void BtnAddRow_Click(object? sender, EventArgs e)
    {
        int row = _dgvCalib.Rows.Add();
        _dgvCalib.CurrentCell = _dgvCalib.Rows[row].Cells[0];
        _dgvCalib.BeginEdit(true);
    }
    private void BtnCapture_Click(object? sender, EventArgs e)
    {
        int code = _calibUseCh0 ? _lastCh0 : _lastCh1;
        if (code == 0) return;
        int row = _dgvCalib.Rows.Add();
        _dgvCalib.Rows[row].Cells[0].Value = code;
        _dgvCalib.CurrentCell = _dgvCalib.Rows[row].Cells[1];
        _dgvCalib.BeginEdit(true);
    }
    private void BtnLsq_Click(object? sender, EventArgs e)
    {
        var pts = ReadGridPoints();
        if (pts.Count < 2) { MessageBox.Show("Нужно минимум 2 точки.", "МНК", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        var (k, b) = CalibrationCalculator.CalculateLsq(pts);
        _txtK.Text = k.ToString("G8", CultureInfo.InvariantCulture);
        _txtB.Text = b.ToString("G8", CultureInfo.InvariantCulture);
    }
    private void BtnCapPlus_Click(object? sender, EventArgs e)
    {
        int code = _sim.Channel == ActiveChannel.Main ? _lastCh0 : _lastCh1;
        if (code != 0) _txtCodePlus.Text = code.ToString();
    }
    private void BtnCalcPlus_Click(object? sender, EventArgs e)
    {
        if (int.TryParse(_txtCodePlus.Text, out int code) &&
            double.TryParse(_txtMassPlus.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double mass) && code != 0)
            _txtKPlus.Text = (mass / code).ToString("G8", CultureInfo.InvariantCulture);
        else
            MessageBox.Show("Введите корректный код АЦП и эталонную массу.", "Авторасчёт", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
    private void BtnCapMinus_Click(object? sender, EventArgs e)
    {
        int code = _sim.Channel == ActiveChannel.Main ? _lastCh0 : _lastCh1;
        if (code != 0) _txtCodeMinus.Text = code.ToString();
    }
    private void BtnCalcMinus_Click(object? sender, EventArgs e)
    {
        if (int.TryParse(_txtCodeMinus.Text, out int code) &&
            double.TryParse(_txtMassMinus.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double mass) && code != 0)
            _txtKMinus.Text = (mass / code).ToString("G8", CultureInfo.InvariantCulture);
        else
            MessageBox.Show("Введите корректный код АЦП и эталонную массу.", "Авторасчёт", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
    private void BtnSaveSettings_Click(object? sender, EventArgs e) =>
        MessageBox.Show("Сохранение настроек — в разработке.", "Настройки", MessageBoxButtons.OK, MessageBoxIcon.Information);

    private void RbMain_CheckedChanged(object? sender, EventArgs e)
    {
        if (_rbMain.Checked && _sim is not null) _sim.Channel = ActiveChannel.Main;
    }
    private void RbBackup_CheckedChanged(object? sender, EventArgs e)
    {
        if (_rbBackup.Checked && _sim is not null) _sim.Channel = ActiveChannel.Backup;
    }
    private void RbCh0Calib_CheckedChanged(object? sender, EventArgs e)
    {
        if (_rbCh0Calib.Checked) { _calibUseCh0 = true;  LoadCalibPoints(); UpdateLiveAdcLabel(); }
    }
    private void RbCh1Calib_CheckedChanged(object? sender, EventArgs e)
    {
        if (_rbCh1Calib.Checked) { _calibUseCh0 = false; LoadCalibPoints(); UpdateLiveAdcLabel(); }
    }
    private void RateTimer_Tick(object? sender, EventArgs e)
    {
        _lblRate.Text = $"{_frameCount} фр/с";
        _frameCount   = 0;
    }

    // ── Monitor ─────────────────────────────────────────────────────────────

    private void RefreshPorts()
    {
        if (_sim is null) return;
        var ports = SerialPort.GetPortNames();
        _cmbPort.Items.Clear();
        if (ports.Length > 0)
        {
            _cmbPort.Items.AddRange(ports);
            int idx = Array.IndexOf(ports, _sim.PortName);
            _cmbPort.SelectedIndex = idx >= 0 ? idx : 0;
        }
        _btnConn.Enabled = ports.Length > 0;

        _cmbSettPort.Items.Clear();
        if (ports.Length > 0) { _cmbSettPort.Items.AddRange(ports); _cmbSettPort.SelectedIndex = 0; }
    }

    private void BtnMonConn_Click(object? sender, EventArgs e)
    {
        if (_sim.IsConnected)
        {
            var port = _sim.PortName;
            _sim.Close();
            AuditLogger.Action(AuditLogger.AdcDisconnected, "AdcConnection", port, "SimA04", port);
            return;
        }
        if (_cmbPort.SelectedItem is not string selectedPort) return;
        try
        {
            _sim.Open(selectedPort);
            AuditLogger.Action(AuditLogger.AdcConnected, "AdcConnection", selectedPort, "SimA04", selectedPort);
        }
        catch (Exception ex)
        {
            AppendLog($"ОШИБКА: {ex.Message}", UiColors.Error);
            AuditLogger.Error(AuditLogger.ErrorAdc, "AdcConnection", selectedPort, "SimA04", selectedPort);
        }
    }

    private void UpdateMonitorConn(bool connected)
    {
        _dotConn.BackColor = connected ? UiColors.PrimaryAction : UiColors.Disconnected;
        _lblConn.Text      = connected ? $"Подключено: {_sim.PortName}  4800/Even/8/1" : "Нет подключения";
        _lblConn.ForeColor = connected ? UiColors.PrimaryAction : UiColors.Disconnected;
        _btnConn.Text      = connected ? "Отключить" : "Подключить";
        _btnConn.BackColor = connected ? UiColors.DangerAction : UiColors.PrimaryAction;
        _cmbPort.Enabled   = !connected;
        AppendLog(connected ? $"=== Подключено: {_sim.PortName}  4800/Even/8/1 ===" : "=== Отключено ===",
            connected ? UiColors.PrimaryAction : UiColors.Disconnected);
    }

    private void OnConnectionChanged(object? sender, bool connected)
    {
        if (InvokeRequired) { BeginInvoke(() => OnConnectionChanged(sender, connected)); return; }
        UpdateMonitorConn(connected);
    }

    private void OnRawFrame(object? sender, byte[] raw)
    {
        if (InvokeRequired) { BeginInvoke(() => OnRawFrame(sender, raw)); return; }
        var frame = SimA04Frame.Parse(raw);
        _frameCount++;
        if (frame.Valid)
        {
            _lastCh0          = frame.Ch0;
            _lastCh1          = frame.Ch1;
            _lblCh0.Text      = frame.Ch0.ToString();
            _lblCh1.Text      = frame.Ch1.ToString();
            _lblCh0.ForeColor = UiColors.Info;
            _lblCh1.ForeColor = UiColors.Info;
            UpdateLiveAdcLabel();
        }
        else
        {
            _lblCh0.ForeColor = UiColors.Error;
            _lblCh1.ForeColor = UiColors.Error;
        }
        if (!_chkLog.Checked) return;
        string bytes = string.Join("  ", raw.Select(b => b.ToString("D3")));
        string time  = DateTime.Now.ToString("HH:mm:ss.fff");
        if (frame.Valid)
            AppendLog($"{time}  [{bytes}]  CH0={frame.Ch0,5}  CH1={frame.Ch1,5}", UiColors.LogText);
        else
            AppendLog($"{time}  [{bytes}]  INVALID ({raw.Length} байт)", UiColors.Warning);
    }

    private void AppendLog(string text, Color color)
    {
        var prev = _rtbLog.SelectionColor;
        _rtbLog.SelectionColor = color;
        _rtbLog.AppendText(text + "\n");
        _rtbLog.SelectionColor = prev;
        if (_rtbLog.Lines.Length > 300)
        {
            int cut = _rtbLog.GetFirstCharIndexFromLine(50);
            _rtbLog.Select(0, cut);
            _rtbLog.SelectedText = "";
        }
        _rtbLog.ScrollToCaret();
    }

    // ── Calibration static ──────────────────────────────────────────────────

    private async void BtnCalibSave_Click(object? sender, EventArgs e)
    {
        if (!double.TryParse(_txtK.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double k) ||
            !double.TryParse(_txtB.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double b))
        {
            MessageBox.Show("Некорректные значения k или b.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        var ch    = _calibUseCh0 ? _calib.Profile.Ch0 : _calib.Profile.Ch1;
        ch.K      = k;
        ch.B      = b;
        ch.Points = ReadGridPoints();
        try
        {
            await _calib.SaveAsync();
            MessageBox.Show("Калибровка сохранена.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AuditLogger.Action(AuditLogger.CalibrationSaved,
                "CalibProfile", $"static ch={(_calibUseCh0 ? "CH0" : "CH1")}");
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb, "CalibProfile", "static", "PostgreSQL", ex.Message);
            MessageBox.Show("Не удалось сохранить калибровку.\nОбратитесь к администратору.",
                "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadCalibPoints()
    {
        if (_dgvCalib == null || _calib is null) return;
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
            if (int.TryParse(row.Cells[0].Value?.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int code) &&
                double.TryParse(row.Cells[1].Value?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out double mass))
                pts.Add(new CalibrationPoint { AdcCode = code, MassTonnes = mass });
        }
        return pts;
    }

    private void UpdateLiveAdcLabel()
    {
        int code = _calibUseCh0 ? _lastCh0 : _lastCh1;
        _lblLiveAdc.Text = code == 0 ? "—" : code.ToString();
        if (_sim is not null)
        {
            int dynCode = _sim.Channel == ActiveChannel.Main ? _lastCh0 : _lastCh1;
            _lblLiveAdcD.Text = dynCode == 0 ? "—" : dynCode.ToString();
        }
    }

    // ── Calibration dynamic ─────────────────────────────────────────────────

    private async void BtnCalibDynSave_Click(object? sender, EventArgs e)
    {
        if (!double.TryParse(_txtKPlus.Text,  NumberStyles.Float, CultureInfo.InvariantCulture, out double kp) ||
            !double.TryParse(_txtKMinus.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double km))
        {
            MessageBox.Show("Некорректные значения K→ или K←.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        _calib.Profile.Dynamic.KPlus  = kp;
        _calib.Profile.Dynamic.KMinus = km;
        try
        {
            await _calib.SaveAsync();
            MessageBox.Show("Калибровка динамики сохранена.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AuditLogger.Action(AuditLogger.CalibrationSaved,
                "CalibProfile", $"dynamic kp={kp:G4} km={km:G4}");
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb, "CalibProfile", "dynamic", "PostgreSQL", ex.Message);
            MessageBox.Show("Не удалось сохранить калибровку динамики.\nОбратитесь к администратору.",
                "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadCalibDynamic()
    {
        if (_calib is null) return;
        _txtKPlus .Text = _calib.Profile.Dynamic.KPlus .ToString("G8", CultureInfo.InvariantCulture);
        _txtKMinus.Text = _calib.Profile.Dynamic.KMinus.ToString("G8", CultureInfo.InvariantCulture);
    }

    // ── Admin ────────────────────────────────────────────────────────────────

    private void BtnAdmin_Click(object? sender, EventArgs e)
    {
        if (_adminUnlocked)
        {
            _adminUnlocked      = false;
            _btnAdmin.Text      = "🔒 Войти как администратор";
            _btnAdmin.BackColor = UiColors.AdminLocked;
            SetAdminTabs(false);
            AuditLogger.Action(AuditLogger.AdminLogin, "AdminSession", "выход из режима администратора");
        }
        else
        {
            using var dlg = new PasswordDialog();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            _adminUnlocked      = true;
            _btnAdmin.Text      = "🔓 Выйти из режима администратора";
            _btnAdmin.BackColor = UiColors.AdminUnlocked;
            SetAdminTabs(true);
            AuditLogger.Action(AuditLogger.AdminLogin, "AdminSession", "вход в режим администратора");
        }
    }

    private void SetAdminTabs(bool enabled)
    {
        _tabCalibS.Enabled = enabled;
        _tabCalibD.Enabled = enabled;
        _tabSett.Enabled   = enabled;
    }
}
