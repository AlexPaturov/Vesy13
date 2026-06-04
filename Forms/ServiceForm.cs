using System.Globalization;
using System.IO.Ports;
using Vesy13.Models;
using Vesy13.Services;

namespace Vesy13.Forms;

public partial class ServiceForm : Form
{
    private AdcService         _adc   = null!;
    private CalibrationService _calib = null!;
    private bool _adminUnlocked;
    private bool _calibUseCh0 = true;
    private int  _frameCount;
    private int  _lastCh0;
    private int  _lastCh1;

    public ServiceForm()
    {
        InitializeComponent();
    }

    public ServiceForm(AdcService adc, CalibrationService calib)
    {
        _adc   = adc;
        _calib = calib;
        InitializeComponent();
    }

    // ── Lifecycle ───────────────────────────────────────────────────────────

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (DesignMode || _adc is null) return;
        _adc.RawFrameReceived  += OnRawFrame;
        _adc.ConnectionChanged += OnConnectionChanged;
        _rateTimer.Start();
        _rbMain.Checked   = _adc.Channel == ActiveChannel.Main;
        _rbBackup.Checked = _adc.Channel == ActiveChannel.Backup;
        RefreshPorts();
        LoadCalibPoints();
        LoadCalibDynamic();
        SetAdminTabs(false);
        UpdateMonitorConn(_adc.IsConnected);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (!DesignMode && _adc is not null)
        {
            _adc.RawFrameReceived  -= OnRawFrame;
            _adc.ConnectionChanged -= OnConnectionChanged;
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
        var (k, b) = CalibrationService.CalculateLsq(pts);
        _txtK.Text = k.ToString("G8", CultureInfo.InvariantCulture);
        _txtB.Text = b.ToString("G8", CultureInfo.InvariantCulture);
    }
    private void BtnCapPlus_Click(object? sender, EventArgs e)
    {
        int code = _adc.Channel == ActiveChannel.Main ? _lastCh0 : _lastCh1;
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
        int code = _adc.Channel == ActiveChannel.Main ? _lastCh0 : _lastCh1;
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
        if (_rbMain.Checked && _adc is not null) _adc.Channel = ActiveChannel.Main;
    }
    private void RbBackup_CheckedChanged(object? sender, EventArgs e)
    {
        if (_rbBackup.Checked && _adc is not null) _adc.Channel = ActiveChannel.Backup;
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
        if (_adc is null) return;
        var ports = SerialPort.GetPortNames();
        _cmbPort.Items.Clear();
        if (ports.Length > 0)
        {
            _cmbPort.Items.AddRange(ports);
            int idx = Array.IndexOf(ports, _adc.PortName);
            _cmbPort.SelectedIndex = idx >= 0 ? idx : 0;
        }
        _btnConn.Enabled = ports.Length > 0;

        _cmbSettPort.Items.Clear();
        if (ports.Length > 0) { _cmbSettPort.Items.AddRange(ports); _cmbSettPort.SelectedIndex = 0; }
    }

    private void BtnMonConn_Click(object? sender, EventArgs e)
    {
        if (_adc.IsConnected) { _adc.Close(); return; }
        if (_cmbPort.SelectedItem is not string port) return;
        try   { _adc.Open(port); }
        catch (Exception ex) { AppendLog($"ОШИБКА: {ex.Message}", Color.Red); }
    }

    private void UpdateMonitorConn(bool connected)
    {
        _dotConn.BackColor = connected ? Color.LimeGreen : Color.Gray;
        _lblConn.Text      = connected ? $"Подключено: {_adc.PortName}  4800/Even/8/1" : "Нет подключения";
        _lblConn.ForeColor = connected ? Color.LimeGreen : Color.Gray;
        _btnConn.Text      = connected ? "Отключить" : "Подключить";
        _btnConn.BackColor = connected ? Color.FromArgb(120, 40, 0) : Color.FromArgb(0, 100, 50);
        _cmbPort.Enabled   = !connected;
        AppendLog(connected ? $"=== Подключено: {_adc.PortName}  4800/Even/8/1 ===" : "=== Отключено ===",
            connected ? Color.LimeGreen : Color.Gray);
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
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка сохранения:\n{ex.Message}", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        if (_adc is not null)
        {
            int dynCode = _adc.Channel == ActiveChannel.Main ? _lastCh0 : _lastCh1;
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
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка сохранения:\n{ex.Message}", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
}
