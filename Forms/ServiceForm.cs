using System.Globalization;
using System.IO.Ports;
using Vesy13.Application;
using Vesy13.Models;
using Vesy13.Services.Configuration;
using Vesy13.Services.Hardware;
using Vesy13.Services.Repositories;

namespace Vesy13.Forms;

/// <summary>
/// Форма обслуживания: монитор АЦП, редактор тарировочных точек (статика/динамика),
/// вкладка администратора защищена паролем.
/// </summary>
public partial class ServiceForm : Form
{
    private SimA04ReaderStatic _sim = null!;
    private SimA04ReaderDynamic _dynamicSim = null!;
    private LocalRepository _calib = null!;
    private SettingsService _settings = null!;
    private bool _adminUnlocked;
    private bool _calibUseCh0 = true;
    private int _frameCount;
    private int _dynamicSampleCount;
    private long _dynamicSampleUiPosted;
    private long _dynamicSampleUiApplied;
    private long _dynamicRawUiPosted;
    private long _dynamicRawUiApplied;
    private long _dynamicLogAppended;
    private long _dynamicLogMaxMs;
    private long _dynamicDiagLastTick;
    private long _lastDiagRawBytes;
    private long _lastDiagSamples;
    private long _lastDiagSkippedBytes;
    private long _lastDiagSamplePosted;
    private long _lastDiagSampleApplied;
    private long _lastDiagRawPosted;
    private long _lastDiagRawApplied;
    private long _lastDiagLogAppended;
    private int _lastCh0;
    private int _lastCh1;
    private int _lastDynCh0;
    private int _lastDynCh1;


    public ServiceForm()
    {
        InitializeComponent();
    }

    public ServiceForm(SimA04ReaderStatic adc, SimA04ReaderDynamic dynamicAdc, LocalRepository calib, SettingsService settings)
    {
        _sim = adc;
        _dynamicSim = dynamicAdc;
        _calib = calib;
        _settings = settings;
        InitializeComponent();
    }

    private void ApplyTheme()
    {
        BackColor = UiColors.AppBackground;
        _tabs.BackColor = UiColors.Surface;
        _tabs.Font = UiFonts.Medium;

        // Channel tab
        _btnAdmin.Font = UiFonts.Body;
        _btnAdmin.BackColor = UiColors.AdminLocked;
        _btnAdmin.ForeColor = UiColors.TextOnDark;
        _lblChannelTitle.Font = UiFonts.SubHeaderBold;
        _lblChannelTitle.ForeColor = UiColors.TextPrimary;
        _rbMain.Font = UiFonts.NavButton;
        _rbMain.ForeColor = UiColors.TextPrimary;
        _rbBackup.Font = UiFonts.NavButton;
        _rbBackup.ForeColor = UiColors.TextPrimary;
        _lblChannelNote.Font = UiFonts.Body;
        _lblChannelNote.ForeColor = UiColors.Disconnected;
        _tabChannel.BackColor = UiColors.Surface;

        // Monitor static tab
        _tabMonitor.BackColor = UiColors.Surface;
        _cmbPort.Font = UiFonts.Medium;
        _cmbPort.BackColor = UiColors.InputBack;
        _cmbPort.ForeColor = UiColors.InputFore;
        _btnConn.Font = UiFonts.Body;
        _btnConn.BackColor = UiColors.PrimaryAction;
        _btnConn.ForeColor = UiColors.TextOnDark;
        _btnPortRefresh.Font = UiFonts.SubHeader;
        _btnPortRefresh.BackColor = UiColors.NeutralAction;
        _btnPortRefresh.ForeColor = UiColors.TextPrimary;
        _lblConn.Font = UiFonts.Body;
        _lblConn.ForeColor = UiColors.Disconnected;
        _lblRate.Font = UiFonts.Body;
        _lblRate.ForeColor = UiColors.Disconnected;
        _lblCh0Cap.Font = UiFonts.Body;
        _lblCh0Cap.ForeColor = UiColors.TextOnDarkMuted;
        _lblCh0.Font = UiFonts.MonitorDisplay;
        _lblCh0.ForeColor = UiColors.Disconnected;
        _lblCh1Cap.Font = UiFonts.Body;
        _lblCh1Cap.ForeColor = UiColors.TextOnDarkMuted;
        _lblCh1.Font = UiFonts.MonitorDisplay;
        _lblCh1.ForeColor = UiColors.Disconnected;
        _chkLog.Font = UiFonts.Body;
        _chkLog.ForeColor = UiColors.TextPrimary;
        _btnClearLog.Font = UiFonts.Body;
        _btnClearLog.BackColor = UiColors.NeutralAction;
        _btnClearLog.ForeColor = UiColors.TextPrimary;
        _rtbLog.Font = UiFonts.MonoSmall;
        _rtbLog.BackColor = UiColors.LogBackground;
        _rtbLog.ForeColor = UiColors.LogText;
        _pnlCh0.BackColor = UiColors.MonitorBackground;
        _pnlCh1.BackColor = UiColors.MonitorBackground;
        ApplyDynamicServiceTheme();

        // CalibStatic tab
        _tabCalibS.BackColor = UiColors.Surface;
        _pnlCalibS.BackColor = UiColors.Surface;
        _pnlCalibSHead.BackColor = UiColors.Surface;
        _pnlCalibSBody.BackColor = UiColors.Surface;
        _cmbStaticCalibPort.Font = UiFonts.Medium;
        _cmbStaticCalibPort.BackColor = UiColors.InputBack;
        _cmbStaticCalibPort.ForeColor = UiColors.InputFore;
        _dotStaticCalibConn.BackColor = UiColors.Disconnected;
        _btnStaticCalibConn.Font = UiFonts.Body;
        _btnStaticCalibConn.BackColor = UiColors.PrimaryAction;
        _btnStaticCalibConn.ForeColor = UiColors.TextOnDark;
        _btnStaticCalibPortRefresh.Font = UiFonts.SubHeader;
        _btnStaticCalibPortRefresh.BackColor = UiColors.NeutralAction;
        _btnStaticCalibPortRefresh.ForeColor = UiColors.TextPrimary;
        _lblStaticCalibConn.Font = UiFonts.Body;
        _lblStaticCalibConn.ForeColor = UiColors.Disconnected;
        _rbCh0Calib.Font = UiFonts.SubHeader;
        _rbCh0Calib.ForeColor = UiColors.TextPrimary;
        _rbCh1Calib.Font = UiFonts.SubHeader;
        _rbCh1Calib.ForeColor = UiColors.TextPrimary;
        _lblLiveAdcCap.Font = UiFonts.Body;
        _lblLiveAdcCap.ForeColor = UiColors.Disconnected;
        _lblLiveAdc.Font = UiFonts.MonoLiveAdc;
        _lblLiveAdc.ForeColor = UiColors.Info;
        _btnCapture.Font = UiFonts.Body;
        _btnCapture.BackColor = UiColors.NeutralAction;
        _btnCapture.ForeColor = UiColors.TextPrimary;
        _dgvCalib.Font = UiFonts.GridBody;
        _dgvCalib.BackgroundColor = UiColors.Surface;
        _dgvCalib.ColumnHeadersDefaultCellStyle.BackColor = UiColors.GridHeaderBack;
        _dgvCalib.ColumnHeadersDefaultCellStyle.ForeColor = UiColors.GridHeaderText;
        _dgvCalib.ColumnHeadersDefaultCellStyle.SelectionBackColor = UiColors.GridHeaderBack;
        _dgvCalib.ColumnHeadersDefaultCellStyle.SelectionForeColor = UiColors.GridHeaderText;
        _dgvCalib.DefaultCellStyle.BackColor = UiColors.Surface;
        _dgvCalib.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
        _dgvCalib.DefaultCellStyle.SelectionBackColor = UiColors.GridSelectionBack;
        _dgvCalib.DefaultCellStyle.SelectionForeColor = UiColors.GridSelectionText;
        _dgvCalib.GridColor = UiColors.GridLine;
        _btnAddRow.Font = UiFonts.Body;
        _btnAddRow.BackColor = UiColors.NeutralAction;
        _btnAddRow.ForeColor = UiColors.TextPrimary;
        _btnDelRow.Font = UiFonts.Body;
        _btnDelRow.BackColor = UiColors.NeutralAction;
        _btnDelRow.ForeColor = UiColors.TextPrimary;
        _lblKEquals.Font = UiFonts.Medium;
        _lblKEquals.ForeColor = UiColors.TextPrimary;
        _txtK.Font = UiFonts.Mono;
        _txtK.BackColor = UiColors.InputBack;
        _txtK.ForeColor = UiColors.InputFore;
        _lblBEquals.Font = UiFonts.Medium;
        _lblBEquals.ForeColor = UiColors.TextPrimary;
        _txtB.Font = UiFonts.Mono;
        _txtB.BackColor = UiColors.InputBack;
        _txtB.ForeColor = UiColors.InputFore;
        _lblFormula.Font = UiFonts.Body;
        _lblFormula.ForeColor = UiColors.TextMuted;
        _btnLsq.Font = UiFonts.Medium;
        _btnLsq.BackColor = UiColors.SecondaryAction;
        _btnLsq.ForeColor = UiColors.TextOnDark;
        _btnCalibSave.Font = UiFonts.Medium;
        _btnCalibSave.BackColor = UiColors.PrimaryAction;
        _btnCalibSave.ForeColor = UiColors.TextOnDark;

        // CalibDynamic tab
        _tabCalibD.BackColor = UiColors.Surface;
        _pnlCalibD.BackColor = UiColors.Surface;
        _pnlCalibDHead.BackColor = UiColors.Surface;
        _pnlCalibDBody.BackColor = UiColors.Surface;
        _lblLiveAdcCapD.Font = UiFonts.Body;
        _lblLiveAdcCapD.ForeColor = UiColors.Disconnected;
        _lblLiveAdcD.Font = UiFonts.MonoLiveAdc;
        _lblLiveAdcD.ForeColor = UiColors.Info;
        _lblLiveWeightCapD.Font = UiFonts.Body;
        _lblLiveWeightCapD.ForeColor = UiColors.Disconnected;
        _lblLiveWeightD.Font = UiFonts.MonoLiveAdc;
        _lblLiveWeightD.ForeColor = UiColors.Info;
        _cmbDynamicCalibPort.Font = UiFonts.Medium;
        _cmbDynamicCalibPort.BackColor = UiColors.InputBack;
        _cmbDynamicCalibPort.ForeColor = UiColors.InputFore;
        _dotDynamicCalibConn.BackColor = UiColors.Disconnected;
        _btnDynamicCalibConn.Font = UiFonts.Body;
        _btnDynamicCalibConn.BackColor = UiColors.PrimaryAction;
        _btnDynamicCalibConn.ForeColor = UiColors.TextOnDark;
        _btnDynamicCalibPortRefresh.Font = UiFonts.SubHeader;
        _btnDynamicCalibPortRefresh.BackColor = UiColors.NeutralAction;
        _btnDynamicCalibPortRefresh.ForeColor = UiColors.TextPrimary;
        _lblDynamicCalibConn.Font = UiFonts.Body;
        _lblDynamicCalibConn.ForeColor = UiColors.Disconnected;
        _lblSecPlus.Font = UiFonts.BodyBold;
        _lblSecPlus.ForeColor = UiColors.TextSection;
        _lblKPlusEquals.Font = UiFonts.Medium;
        _lblKPlusEquals.ForeColor = UiColors.TextPrimary;
        _txtKPlus.Font = UiFonts.Mono;
        _txtKPlus.BackColor = UiColors.InputBack;
        _txtKPlus.ForeColor = UiColors.InputFore;
        _lblAutoCalcPlus.Font = UiFonts.Body;
        _lblAutoCalcPlus.ForeColor = UiColors.Disconnected;
        _lblCodePlusCap.Font = UiFonts.Body;
        _lblCodePlusCap.ForeColor = UiColors.TextPrimary;
        _txtCodePlus.Font = UiFonts.MonoSmall;
        _txtCodePlus.BackColor = UiColors.InputBack;
        _txtCodePlus.ForeColor = UiColors.InputFore;
        _btnCapPlus.Font = UiFonts.Small;
        _btnCapPlus.BackColor = UiColors.NeutralAction;
        _btnCapPlus.ForeColor = UiColors.TextPrimary;
        _lblMassPlusCap.Font = UiFonts.Body;
        _lblMassPlusCap.ForeColor = UiColors.TextPrimary;
        _txtMassPlus.Font = UiFonts.MonoSmall;
        _txtMassPlus.BackColor = UiColors.InputBack;
        _txtMassPlus.ForeColor = UiColors.InputFore;
        _btnCalcPlus.Font = UiFonts.Body;
        _btnCalcPlus.BackColor = UiColors.SecondaryAction;
        _btnCalcPlus.ForeColor = UiColors.TextOnDark;
        _lblSecMinus.Font = UiFonts.BodyBold;
        _lblSecMinus.ForeColor = UiColors.TextSection;
        _lblKMinusEquals.Font = UiFonts.Medium;
        _lblKMinusEquals.ForeColor = UiColors.TextPrimary;
        _txtKMinus.Font = UiFonts.Mono;
        _txtKMinus.BackColor = UiColors.InputBack;
        _txtKMinus.ForeColor = UiColors.InputFore;
        _lblAutoCalcMinus.Font = UiFonts.Body;
        _lblAutoCalcMinus.ForeColor = UiColors.Disconnected;
        _lblCodeMinusCap.Font = UiFonts.Body;
        _lblCodeMinusCap.ForeColor = UiColors.TextPrimary;
        _txtCodeMinus.Font = UiFonts.MonoSmall;
        _txtCodeMinus.BackColor = UiColors.InputBack;
        _txtCodeMinus.ForeColor = UiColors.InputFore;
        _btnCapMinus.Font = UiFonts.Small;
        _btnCapMinus.BackColor = UiColors.NeutralAction;
        _btnCapMinus.ForeColor = UiColors.TextPrimary;
        _lblMassMinusCap.Font = UiFonts.Body;
        _lblMassMinusCap.ForeColor = UiColors.TextPrimary;
        _txtMassMinus.Font = UiFonts.MonoSmall;
        _txtMassMinus.BackColor = UiColors.InputBack;
        _txtMassMinus.ForeColor = UiColors.InputFore;
        _btnCalcMinus.Font = UiFonts.Body;
        _btnCalcMinus.BackColor = UiColors.SecondaryAction;
        _btnCalcMinus.ForeColor = UiColors.TextOnDark;
        _lblFormulaD.Font = UiFonts.Body;
        _lblFormulaD.ForeColor = UiColors.TextMuted;
        _btnCalibDynSave.Font = UiFonts.Medium;
        _btnCalibDynSave.BackColor = UiColors.PrimaryAction;
        _btnCalibDynSave.ForeColor = UiColors.TextOnDark;
        _dgvDynCalib.Font = UiFonts.GridBody;
        _dgvDynCalib.BackgroundColor = UiColors.Surface;
        _dgvDynCalib.ColumnHeadersDefaultCellStyle.BackColor = UiColors.GridHeaderBack;
        _dgvDynCalib.ColumnHeadersDefaultCellStyle.ForeColor = UiColors.GridHeaderText;
        _dgvDynCalib.ColumnHeadersDefaultCellStyle.SelectionBackColor = UiColors.GridHeaderBack;
        _dgvDynCalib.ColumnHeadersDefaultCellStyle.SelectionForeColor = UiColors.GridHeaderText;
        _dgvDynCalib.DefaultCellStyle.BackColor = UiColors.Surface;
        _dgvDynCalib.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
        _dgvDynCalib.DefaultCellStyle.SelectionBackColor = UiColors.GridSelectionBack;
        _dgvDynCalib.DefaultCellStyle.SelectionForeColor = UiColors.GridSelectionText;
        _dgvDynCalib.GridColor = UiColors.GridLine;

        // Settings tab
        _tabSett.BackColor = UiColors.Surface;
        _lblPortCap.Font = UiFonts.Medium;
        _lblPortCap.ForeColor = UiColors.TextPrimary;
        _cmbSettPort.Font = UiFonts.Body;
        _cmbSettPort.BackColor = UiColors.InputBack;
        _cmbSettPort.ForeColor = UiColors.InputFore;
        _lblNpvCap.Font = UiFonts.Medium;
        _lblNpvCap.ForeColor = UiColors.TextPrimary;
        _txtNpv.Font = UiFonts.Body;
        _txtNpv.BackColor = UiColors.InputBack;
        _txtNpv.ForeColor = UiColors.InputFore;
        _lblDiscCap.Font = UiFonts.Medium;
        _lblDiscCap.ForeColor = UiColors.TextPrimary;
        _cmbDisc.Font = UiFonts.Body;
        _cmbDisc.BackColor = UiColors.InputBack;
        _cmbDisc.ForeColor = UiColors.InputFore;
        _lblZeroCap.Font = UiFonts.Medium;
        _lblZeroCap.ForeColor = UiColors.TextPrimary;
        _txtZeroLimit.Font = UiFonts.Body;
        _txtZeroLimit.BackColor = UiColors.InputBack;
        _txtZeroLimit.ForeColor = UiColors.InputFore;
        _lblPasswordCap.Font = UiFonts.Medium;
        _lblPasswordCap.ForeColor = UiColors.TextPrimary;
        _txtNewPassword.Font = UiFonts.Body;
        _txtNewPassword.BackColor = UiColors.InputBack;
        _txtNewPassword.ForeColor = UiColors.InputFore;
        _btnSaveSettings.Font = UiFonts.Medium;
        _btnSaveSettings.BackColor = UiColors.PrimaryAction;
        _btnSaveSettings.ForeColor = UiColors.TextOnDark;
    }

    // ── Lifecycle ───────────────────────────────────────────────────────────

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ApplyTheme();
        if (DesignMode || _sim is null) return;
        AuditLogger.Action(AuditLogger.FormOpened, "Form", "ServiceForm");
        _sim.ConnectionTimeoutMs = 1000;
        _dynamicSim.ConnectionTimeoutMs = 5000;
        _tabs.SelectedIndexChanged += Tabs_SelectedIndexChanged;
        _sim.RawFrameReceived += OnRawFrame;
        _sim.ConnectionChanged += OnConnectionChanged;
        _dynamicSim.RawSampleReceived += OnDynamicRawSample;
        _dynamicSim.SampleReceived += OnDynamicSample;
        _dynamicSim.ConnectionChanged += OnDynamicConnectionChanged;
        _dgvCalib.CellValueChanged += DgvCalib_CellValueChanged;
        _dgvCalib.CurrentCellDirtyStateChanged += DgvCalib_CurrentCellDirtyStateChanged;
        _rateTimer.Start();
        _rbMain.Checked = _sim.Channel == ActiveChannel.Main;
        _rbBackup.Checked = _sim.Channel == ActiveChannel.Backup;
        RefreshPorts();
        RefreshDynamicPorts();
        LoadSettingsUi();
        LoadCalibPoints();
        LoadCalibDynamic();
        SetAdminTabs(false);
        UpdateMonitorConn(_sim.IsConnected);
        UpdateDynamicMonitorConn(_dynamicSim.IsConnected);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (!DesignMode && _sim is not null)
        {
            _tabs.SelectedIndexChanged -= Tabs_SelectedIndexChanged;
            _sim.RawFrameReceived -= OnRawFrame;
            _sim.ConnectionChanged -= OnConnectionChanged;
            if (_sim.IsPortOpen)
                _sim.Close();

            _dynamicSim.RawSampleReceived -= OnDynamicRawSample;
            _dynamicSim.SampleReceived -= OnDynamicSample;
            _dynamicSim.ConnectionChanged -= OnDynamicConnectionChanged;
            if (_dynamicSim.IsPortOpen)
                _dynamicSim.Close();

            _rateTimer.Stop();
        }
        base.OnFormClosed(e);
    }

    // ── Designer event handlers ─────────────────────────────────────────────

    private void BtnPortRefresh_Click(object? sender, EventArgs e) => RefreshPorts();
    private void BtnClearLog_Click(object? sender, EventArgs e) => _rtbLog.Clear();
    private void BtnDelRow_Click(object? sender, EventArgs e)
    {
        if (_dgvCalib.SelectedRows.Count == 0) return;
        SetCalibRowActive(_dgvCalib.SelectedRows[0], false, DateTime.Now);
    }
    private void BtnAddRow_Click(object? sender, EventArgs e)
    {
        int row = _dgvCalib.Rows.Add();
        SetCalibRowActive(_dgvCalib.Rows[row], true);
        _dgvCalib.CurrentCell = _dgvCalib.Rows[row].Cells[0];
        _dgvCalib.BeginEdit(true);
    }
    private void BtnCapture_Click(object? sender, EventArgs e)
    {
        int code = _calibUseCh0 ? _lastCh0 : _lastCh1;
        if (code == 0) return;
        int row = _dgvCalib.Rows.Add();
        _dgvCalib.Rows[row].Cells[0].Value = code;
        SetCalibRowActive(_dgvCalib.Rows[row], true);
        _dgvCalib.CurrentCell = _dgvCalib.Rows[row].Cells[1];
        _dgvCalib.BeginEdit(true);
    }
    private void BtnLsq_Click(object? sender, EventArgs e)
    {
        var pts = ReadGridPoints().Where(p => p.IsActive).ToList();
        if (pts.Count < 2)
        {
            MessageBox.Show("Нужно минимум 2 точки.", "МНК", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        var (k, b) = CalibrationCalculator.CalculateLsq(pts.Select(p => (p.AdcCode, p.Mass, p.IsActive)));
        _txtK.Text = k.ToString("G8", CultureInfo.InvariantCulture);
        _txtB.Text = b.ToString("G8", CultureInfo.InvariantCulture);
    }

    private void BtnCapPlus_Click(object? sender, EventArgs e)
    {
        int code = CurrentDynamicAdcCode();
        if (code != 0)
        {
            _txtCodePlus.Text = code.ToString();
            return;
        }

        MessageBox.Show("Нет текущего кода АЦП динамики.\nПодключите АЦП на этой вкладке.", "Захват кода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        int code = CurrentDynamicAdcCode();
        if (code != 0)
        {
            _txtCodeMinus.Text = code.ToString();
            return;
        }

        MessageBox.Show("Нет текущего кода АЦП динамики.\nПодключите АЦП на этой вкладке.", "Захват кода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
    private void BtnCalcMinus_Click(object? sender, EventArgs e)
    {
        if (int.TryParse(_txtCodeMinus.Text, out int code) &&
            double.TryParse(_txtMassMinus.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double mass) && code != 0)
            _txtKMinus.Text = (mass / code).ToString("G8", CultureInfo.InvariantCulture);
        else
            MessageBox.Show("Введите корректный код АЦП и эталонную массу.", "Авторасчёт", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void BtnSaveSettings_Click(object? sender, EventArgs e) => SaveSettingsFromUi();

    private void RbMain_CheckedChanged(object? sender, EventArgs e)
    {
        if (_rbMain.Checked)
            SetActiveChannel(ActiveChannel.Main);
    }

    private void RbBackup_CheckedChanged(object? sender, EventArgs e)
    {
        if (_rbBackup.Checked)
            SetActiveChannel(ActiveChannel.Backup);
    }

    private void SetActiveChannel(ActiveChannel channel)
    {
        if (_sim is null) return;
        if (_sim.Channel == channel && (_dynamicSim is null || _dynamicSim.Channel == channel)) return;

        ActiveChannel old = _sim.Channel;
        _sim.Channel = channel;
        if (_dynamicSim is not null)
            _dynamicSim.Channel = channel;
        UpdateLiveAdcLabel();
        AuditLogger.Action(AuditLogger.AdcChannelChanged, "AdcChannel", $"{old} -> {channel}", Environment.UserDomainName, Environment.UserName);
    }

    private void RbCh0Calib_CheckedChanged(object? sender, EventArgs e)
    {
        if (_rbCh0Calib.Checked)
        {
            _calibUseCh0 = true;
            LoadCalibPoints();
            UpdateLiveAdcLabel();
        }
    }

    private void RbCh1Calib_CheckedChanged(object? sender, EventArgs e)
    {
        if (_rbCh1Calib.Checked)
        {
            _calibUseCh0 = false;
            LoadCalibPoints();
            UpdateLiveAdcLabel();
        }
    }

    private void RateTimer_Tick(object? sender, EventArgs e)
    {
        _lblRate.Text = $"{_frameCount} фр/с";
        _frameCount = 0;
        if (_lblDynamicRate is not null)
        {
            _lblDynamicRate.Text = $"{_dynamicSampleCount} сэмпл/с";
            _dynamicSampleCount = 0;
        }
    }

    private void LoadSettingsUi()
    {
        if (_settings is null) return;

        SelectComboValue(_cmbSettPort, _settings.Current.AdcPortName);
        _txtNpv.Text = _settings.Current.MaxCapacityTonnes.ToString("G", CultureInfo.InvariantCulture);
        SelectComboValue(_cmbDisc, FormatDiscretization(_settings.Current.WeightDiscretizationTonnes));
        _txtZeroLimit.Text = _settings.Current.OperatorZeroLimitPercent.ToString("G", CultureInfo.InvariantCulture);
        _txtNewPassword.Clear();
    }

    private void SaveSettingsFromUi()
    {
        if (_settings is null) return;

        if (_cmbSettPort.SelectedItem is string portName)
            _settings.Current.AdcPortName = portName;

        if (!double.TryParse(_txtNpv.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double npv) || npv <= 0)
        {
            MessageBox.Show("Введите корректное значение НПВ.", "Настройки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtNpv.Focus();
            return;
        }

        if (!TryParseDiscretization(_cmbDisc.Text, out double discretization))
        {
            MessageBox.Show("Выберите корректную дискретность.", "Настройки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _cmbDisc.Focus();
            return;
        }

        if (!double.TryParse(_txtZeroLimit.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double zeroLimit) || zeroLimit < 0 || zeroLimit > _settings.Current.AdminZeroLimitPercent)
        {
            MessageBox.Show($"Лимит нуля должен быть от 0 до {_settings.Current.AdminZeroLimitPercent:G} % НПВ.", "Настройки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtZeroLimit.Focus();
            return;
        }

        _settings.Current.MaxCapacityTonnes = npv;
        _settings.Current.WeightDiscretizationTonnes = discretization;
        _settings.Current.OperatorZeroLimitPercent = zeroLimit;

        string newPassword = _txtNewPassword.Text;
        if (!string.IsNullOrWhiteSpace(newPassword))
            _settings.SetAdminPassword(newPassword);

        _settings.Save();
        _txtNewPassword.Clear();
        AuditLogger.Action(AuditLogger.SettingsSaved, "Settings", "settings.json", "Vesy13", _settings.Path);
        MessageBox.Show("Настройки сохранены.", "Настройки", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private static void SelectComboValue(ComboBox combo, string value)
    {
        int idx = combo.Items.IndexOf(value);
        if (idx >= 0)
            combo.SelectedIndex = idx;
    }

    private static string FormatDiscretization(double value) => value.ToString("0.##", CultureInfo.InvariantCulture) + " т";

    private static bool TryParseDiscretization(string text, out double value)
    {
        text = text.Replace("т", "", StringComparison.OrdinalIgnoreCase).Trim();
        return double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value) && value > 0;
    }

    // ── Monitor ─────────────────────────────────────────────────────────────

    private void RefreshPorts()
    {
        if (_sim is null) return;
        var ports = SerialPort.GetPortNames();
        FillStaticPortCombo(_cmbPort, ports);
        FillStaticPortCombo(_cmbStaticCalibPort, ports);
        _btnConn.Enabled = ports.Length > 0;

        if (_btnStaticCalibConn is not null)
            _btnStaticCalibConn.Enabled = ports.Length > 0;

        _cmbSettPort.Items.Clear();
        if (!string.IsNullOrWhiteSpace(_settings.Current.AdcPortName))
            _cmbSettPort.Items.Add(_settings.Current.AdcPortName);

        foreach (string portName in ports)
        {
            if (!_cmbSettPort.Items.Contains(portName))
                _cmbSettPort.Items.Add(portName);
        }

        SelectComboValue(_cmbSettPort, _settings.Current.AdcPortName);
    }

    private void FillStaticPortCombo(ComboBox? combo, string[] ports)
    {
        if (combo is null) return;
        string? selected = combo.SelectedItem as string;
        combo.Items.Clear();
        if (ports.Length == 0) return;

        combo.Items.AddRange(ports);
        int idx = Array.IndexOf(ports, selected ?? _sim.PortName);
        combo.SelectedIndex = idx >= 0 ? idx : 0;
    }

    private void BtnMonConn_Click(object? sender, EventArgs e)
    {
        ToggleStaticConnection(_cmbPort.SelectedItem as string, ex => AppendLog($"ОШИБКА: {ex.Message}", UiColors.Error));
    }

    private void BtnStaticCalibConn_Click(object? sender, EventArgs e)
    {
        ToggleStaticConnection(_cmbStaticCalibPort.SelectedItem as string,
            ex => MessageBox.Show($"Не удалось подключить АЦП статики.\n{ex.Message}", "АЦП статики", MessageBoxButtons.OK, MessageBoxIcon.Error));
    }

    private void ToggleStaticConnection(string? selectedPort, Action<Exception> onError)
    {
        if (_sim.IsPortOpen)
        {
            CloseStaticConnection();
            return;
        }

        if (string.IsNullOrWhiteSpace(selectedPort)) return;

        try
        {
            CloseDynamicConnection();
            _sim.Open(selectedPort);
            AuditLogger.Action(AuditLogger.AdcConnected, "AdcConnection", selectedPort, "SimA04", selectedPort);
            UpdateMonitorConn(_sim.IsConnected);
        }
        catch (Exception ex)
        {
            onError(ex);
            AuditLogger.Error(AuditLogger.ErrorAdc, "AdcConnection", selectedPort, "SimA04", selectedPort);
        }
    }


    private void ApplyDynamicServiceTheme()
    {
        if (_tabDynamicService is null) return;
        _cmbDynamicPort.Font = UiFonts.Medium;
        _cmbDynamicPort.BackColor = UiColors.InputBack;
        _cmbDynamicPort.ForeColor = UiColors.InputFore;
        _dotDynamicConn.BackColor = UiColors.Disconnected;
        _btnDynamicConn.Font = UiFonts.Body;
        _btnDynamicConn.BackColor = UiColors.PrimaryAction;
        _btnDynamicConn.ForeColor = UiColors.TextOnDark;
        _btnDynamicPortRefresh.Font = UiFonts.SubHeader;
        _btnDynamicPortRefresh.BackColor = UiColors.NeutralAction;
        _btnDynamicPortRefresh.ForeColor = UiColors.TextPrimary;
        _lblDynamicConn.Font = UiFonts.Body;
        _lblDynamicConn.ForeColor = UiColors.Disconnected;
        _lblDynamicRate.Font = UiFonts.Body;
        _lblDynamicRate.ForeColor = UiColors.Disconnected;
        _pnlDynamicCh0.BackColor = UiColors.MonitorBackground;
        _pnlDynamicCh1.BackColor = UiColors.MonitorBackground;
        _lblDynamicCh0Cap.Font = UiFonts.Body;
        _lblDynamicCh0Cap.ForeColor = UiColors.TextOnDarkMuted;
        _lblDynamicCh1Cap.Font = UiFonts.Body;
        _lblDynamicCh1Cap.ForeColor = UiColors.TextOnDarkMuted;
        _lblDynamicCh0.Font = UiFonts.MonitorDisplay;
        _lblDynamicCh0.ForeColor = UiColors.Disconnected;
        _lblDynamicCh1.Font = UiFonts.MonitorDisplay;
        _lblDynamicCh1.ForeColor = UiColors.Disconnected;
        _chkDynamicLog.Font = UiFonts.Body;
        _chkDynamicLog.ForeColor = UiColors.TextPrimary;
        _btnDynamicClearLog.Font = UiFonts.Body;
        _btnDynamicClearLog.BackColor = UiColors.NeutralAction;
        _btnDynamicClearLog.ForeColor = UiColors.TextPrimary;
        _rtbDynamicLog.Font = UiFonts.MonoSmall;
        _rtbDynamicLog.BackColor = UiColors.LogBackground;
        _rtbDynamicLog.ForeColor = UiColors.LogText;
    }

    private void RefreshDynamicPorts()
    {
        if (_dynamicSim is null) return;
        var ports = SerialPort.GetPortNames();
        FillDynamicPortCombo(_cmbDynamicPort, ports);
        FillDynamicPortCombo(_cmbDynamicCalibPort, ports);
        if (_btnDynamicConn is not null)
            _btnDynamicConn.Enabled = ports.Length > 0;
        if (_btnDynamicCalibConn is not null)
            _btnDynamicCalibConn.Enabled = ports.Length > 0;
    }

    private void FillDynamicPortCombo(ComboBox? combo, string[] ports)
    {
        if (combo is null) return;
        string? selected = combo.SelectedItem as string;
        combo.Items.Clear();
        if (ports.Length == 0) return;

        combo.Items.AddRange(ports);
        int idx = Array.IndexOf(ports, selected ?? _dynamicSim.PortName);
        combo.SelectedIndex = idx >= 0 ? idx : 0;
    }

    private void BtnDynamicConn_Click(object? sender, EventArgs e)
    {
        ToggleDynamicConnection(_cmbDynamicPort.SelectedItem as string, ex => AppendDynamicLog($"ОШИБКА: {ex.Message}", UiColors.Error));
    }

    private void BtnDynamicPortRefresh_Click(object? sender, EventArgs e)
    {
        RefreshDynamicPorts();
    }

    private void BtnDynamicClearLog_Click(object? sender, EventArgs e)
    {
        _rtbDynamicLog.Clear();
    }

    private void BtnDynamicCalibConn_Click(object? sender, EventArgs e)
    {
        ToggleDynamicConnection(_cmbDynamicCalibPort.SelectedItem as string,
            ex => MessageBox.Show($"Не удалось подключить АЦП динамики.\n{ex.Message}", "АЦП динамики", MessageBoxButtons.OK, MessageBoxIcon.Error));
    }

    private void ToggleDynamicConnection(string? selectedPort, Action<Exception> onError)
    {
        if (_dynamicSim.IsPortOpen)
        {
            CloseDynamicConnection();
            return;
        }

        if (string.IsNullOrWhiteSpace(selectedPort)) return;
        try
        {
            CloseStaticConnection();
            _dynamicSim.Open(selectedPort);
            AuditLogger.Action(AuditLogger.AdcConnected, "AdcConnection", selectedPort, "SimA04Dynamic", selectedPort);
            UpdateDynamicMonitorConn(_dynamicSim.IsConnected);
        }
        catch (Exception ex)
        {
            onError(ex);
            AuditLogger.Error(AuditLogger.ErrorAdc, "AdcConnection", selectedPort, "SimA04Dynamic", selectedPort);
        }
    }

    private void CloseStaticConnection()
    {
        if (!_sim.IsPortOpen) return;

        var port = _sim.PortName;
        _sim.Close();
        AuditLogger.Action(AuditLogger.AdcDisconnected, "AdcConnection", port, "SimA04", port);
        UpdateMonitorConn(false);
    }

    private void CloseDynamicConnection()
    {
        if (!_dynamicSim.IsPortOpen) return;

        var port = _dynamicSim.PortName;
        _dynamicSim.Close();
        AuditLogger.Action(AuditLogger.AdcDisconnected, "AdcConnection", port, "SimA04Dynamic", port);
        UpdateDynamicMonitorConn(false);
    }

    private void Tabs_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (DesignMode || _sim is null || _dynamicSim is null) return;
        var tab = _tabs.SelectedTab;
        if (tab == _tabMonitor || tab == _tabCalibS)
        {
            CloseDynamicConnection();
            return;
        }

        if (tab == _tabDynamicService || tab == _tabCalibD)
        {
            CloseStaticConnection();
            return;
        }

        CloseStaticConnection();
        CloseDynamicConnection();
    }

    private void UpdateDynamicMonitorConn(bool connected)
    {
        if (_lblDynamicConn is null) return;
        _dotDynamicConn.BackColor = connected ? UiColors.PrimaryAction : UiColors.Disconnected;
        _lblDynamicConn.Text = connected ? $"Подключено: {_dynamicSim.PortName}  4800/Even/8/1" : (_dynamicSim.IsPortOpen ? $"Порт открыт: {_dynamicSim.PortName}, нет потока АЦП" : "Нет подключения");
        _lblDynamicConn.ForeColor = connected ? UiColors.PrimaryAction : UiColors.Disconnected;
        _btnDynamicConn.Text = _dynamicSim.IsPortOpen ? "Отключить" : "Подключить";
        _btnDynamicConn.BackColor = _dynamicSim.IsPortOpen ? UiColors.DangerAction : UiColors.PrimaryAction;
        _cmbDynamicPort.Enabled = !_dynamicSim.IsPortOpen;
        SelectComboValue(_cmbDynamicPort, _dynamicSim.PortName);
        SelectComboValue(_cmbDynamicCalibPort, _dynamicSim.PortName);
        if (_dotDynamicCalibConn is not null)
            _dotDynamicCalibConn.BackColor = connected ? UiColors.PrimaryAction : UiColors.Disconnected;
        if (_btnDynamicCalibConn is not null)
        {
            _btnDynamicCalibConn.Text = _dynamicSim.IsPortOpen ? "Отключить" : "Подключить";
            _btnDynamicCalibConn.BackColor = _dynamicSim.IsPortOpen ? UiColors.DangerAction : UiColors.PrimaryAction;
        }
        if (_cmbDynamicCalibPort is not null)
            _cmbDynamicCalibPort.Enabled = !_dynamicSim.IsPortOpen;
        UpdateDynamicCalibrationConnectionLabel();
        AppendDynamicLog(connected ? $"=== Подключено: {_dynamicSim.PortName}  4800/Even/8/1 ===" : "=== Отключено ===", connected ? UiColors.PrimaryAction : UiColors.Disconnected);
    }

    private void OnDynamicConnectionChanged(object? sender, bool connected)
    {
        if (InvokeRequired) { BeginInvoke(() => OnDynamicConnectionChanged(sender, connected)); return; }
        UpdateDynamicMonitorConn(connected);
    }

    private void OnDynamicSample(object? sender, SimA04DynamicSample sample)
    {
        if (InvokeRequired)
        {
            Interlocked.Increment(ref _dynamicSampleUiPosted);
            WriteServiceDynamicDiagnostics();
            BeginInvoke(() => OnDynamicSample(sender, sample));
            return;
        }

        Interlocked.Increment(ref _dynamicSampleUiApplied);
        _dynamicSampleCount++;
        _lastDynCh0 = sample.Ch0;
        _lastDynCh1 = sample.Ch1;
        _lblDynamicCh0.Text = sample.Ch0.ToString();
        _lblDynamicCh1.Text = sample.Ch1.ToString();
        _lblDynamicCh0.ForeColor = UiColors.Info;
        _lblDynamicCh1.ForeColor = UiColors.Info;
        UpdateLiveDynamicCalibrationLabels();
    }

    private void OnDynamicRawSample(object? sender, byte[] raw)
    {
        if (InvokeRequired)
        {
            Interlocked.Increment(ref _dynamicRawUiPosted);
            WriteServiceDynamicDiagnostics();
            BeginInvoke(() => OnDynamicRawSample(sender, raw));
            return;
        }

        Interlocked.Increment(ref _dynamicRawUiApplied);
        if (!_chkDynamicLog.Checked) return;
        var sample = SimA04DynamicSample.Parse(raw);
        string bytes = string.Join("  ", raw.Select(b => b.ToString("D3")));
        string time = DateTime.Now.ToString("HH:mm:ss.fff");
        AppendDynamicLog(sample.Valid ? $"{time}  [{bytes}]  CH0={sample.Ch0,5}  CH1={sample.Ch1,5} AUX={sample.Aux,3}" : $"{time}  [{bytes}]  INVALID ({raw.Length} байт)", sample.Valid ? UiColors.LogText : UiColors.Warning);
    }

    private void AppendDynamicLog(string text, Color color)
    {
        if (_rtbDynamicLog is null) return;
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var prev = _rtbDynamicLog.SelectionColor;
        _rtbDynamicLog.SelectionColor = color;
        _rtbDynamicLog.AppendText(text + "\n");
        _rtbDynamicLog.SelectionColor = prev;
        if (_rtbDynamicLog.Lines.Length > 300)
        {
            int cut = _rtbDynamicLog.GetFirstCharIndexFromLine(50);
            _rtbDynamicLog.Select(0, cut);
            _rtbDynamicLog.SelectedText = "";
        }
        _rtbDynamicLog.ScrollToCaret();
        sw.Stop();
        Interlocked.Increment(ref _dynamicLogAppended);
        UpdateMax(ref _dynamicLogMaxMs, sw.ElapsedMilliseconds);
    }

    private void WriteServiceDynamicDiagnostics()
    {
        const long intervalMs = 5000;
        long now = Environment.TickCount64;
        long last = Interlocked.Read(ref _dynamicDiagLastTick);
        if (last != 0 && now - last < intervalMs) return;
        if (Interlocked.CompareExchange(ref _dynamicDiagLastTick, now, last) != last) return;

        long elapsedMs = last == 0 ? intervalMs : now - last;
        long rawBytes = _dynamicSim.RawBytesReceived;
        long samples = _dynamicSim.SamplesReceived;
        long skipped = _dynamicSim.SkippedBytes;
        long samplePosted = Interlocked.Read(ref _dynamicSampleUiPosted);
        long sampleApplied = Interlocked.Read(ref _dynamicSampleUiApplied);
        long rawPosted = Interlocked.Read(ref _dynamicRawUiPosted);
        long rawApplied = Interlocked.Read(ref _dynamicRawUiApplied);
        long logAppended = Interlocked.Read(ref _dynamicLogAppended);
        long logMaxMs = Interlocked.Exchange(ref _dynamicLogMaxMs, 0);

        long rawBytesDelta = Delta(rawBytes, ref _lastDiagRawBytes);
        long samplesDelta = Delta(samples, ref _lastDiagSamples);
        long skippedDelta = Delta(skipped, ref _lastDiagSkippedBytes);
        long samplePostedDelta = Delta(samplePosted, ref _lastDiagSamplePosted);
        long sampleAppliedDelta = Delta(sampleApplied, ref _lastDiagSampleApplied);
        long rawPostedDelta = Delta(rawPosted, ref _lastDiagRawPosted);
        long rawAppliedDelta = Delta(rawApplied, ref _lastDiagRawApplied);
        long logAppendedDelta = Delta(logAppended, ref _lastDiagLogAppended);
        long samplePending = samplePosted - sampleApplied;
        long rawPending = rawPosted - rawApplied;

        AuditLogger.Action(AuditLogger.AdcDynamicDiag, "AdcDynamicService",
            $"periodMs={elapsedMs}; bytes={rawBytesDelta}; samples={samplesDelta}; skipped={skippedDelta}; " +
            $"samplePosted={samplePostedDelta}; sampleApplied={sampleAppliedDelta}; samplePending={samplePending}; " +
            $"rawPosted={rawPostedDelta}; rawApplied={rawAppliedDelta}; rawPending={rawPending}; " +
            $"logAppended={logAppendedDelta}; logMaxMs={logMaxMs}",
            "SimA04Dynamic", _dynamicSim.PortName);
    }

    // Возвращает прирост монотонного счетчика за период диагностики и обновляет предыдущую точку отсчета.
    private static long Delta(long current, ref long previous)
    {
        long delta = current >= previous ? current - previous : current;
        previous = current;
        return delta;
    }

    private static void UpdateMax(ref long target, long value)
    {
        long current;
        do
        {
            current = Interlocked.Read(ref target);
            if (value <= current) return;
        } while (Interlocked.CompareExchange(ref target, value, current) != current);
    }

    private void UpdateMonitorConn(bool connected)
    {
        _dotConn.BackColor = connected ? UiColors.PrimaryAction : UiColors.Disconnected;
        _lblConn.Text = connected ? $"Подключено: {_sim.PortName}  4800/Even/8/1" : (_sim.IsPortOpen ? $"Порт открыт: {_sim.PortName}, нет ответа АЦП" : "Нет подключения");
        _lblConn.ForeColor = connected ? UiColors.PrimaryAction : UiColors.Disconnected;
        _btnConn.Text = _sim.IsPortOpen ? "Отключить" : "Подключить";
        _btnConn.BackColor = _sim.IsPortOpen ? UiColors.DangerAction : UiColors.PrimaryAction;
        _cmbPort.Enabled = !_sim.IsPortOpen;
        SelectComboValue(_cmbPort, _sim.PortName);
        SelectComboValue(_cmbStaticCalibPort, _sim.PortName);
        if (_dotStaticCalibConn is not null)
            _dotStaticCalibConn.BackColor = connected ? UiColors.PrimaryAction : UiColors.Disconnected;
        if (_lblStaticCalibConn is not null)
        {
            _lblStaticCalibConn.Text = connected ? $"Подключено: {_sim.PortName}  4800/Even/8/1" : (_sim.IsPortOpen ? $"Порт открыт: {_sim.PortName}, нет ответа АЦП" : "Нет подключения");
            _lblStaticCalibConn.ForeColor = connected ? UiColors.PrimaryAction : UiColors.Disconnected;
        }
        if (_btnStaticCalibConn is not null)
        {
            _btnStaticCalibConn.Text = _sim.IsPortOpen ? "Отключить" : "Подключить";
            _btnStaticCalibConn.BackColor = _sim.IsPortOpen ? UiColors.DangerAction : UiColors.PrimaryAction;
        }
        if (_cmbStaticCalibPort is not null)
            _cmbStaticCalibPort.Enabled = !_sim.IsPortOpen;
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
            _lastCh0 = frame.Ch0;
            _lastCh1 = frame.Ch1;
            _lblCh0.Text = frame.Ch0.ToString();
            _lblCh1.Text = frame.Ch1.ToString();
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
        string time = DateTime.Now.ToString("HH:mm:ss.fff");
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
        var pts = ReadGridPoints();
        if (pts.Count == 0)
        {
            MessageBox.Show("Нет точек для сохранения.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        int channel = _calibUseCh0 ? 0 : 1;
        try
        {
            await _calib.SaveCalibPointsAsync(channel, pts);
            MessageBox.Show("Калибровка сохранена.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AuditLogger.Action(AuditLogger.CalibrationSaved, "calibration_points", $"ch={(_calibUseCh0 ? "CH0" : "CH1")} rows={pts.Count}");
            await LoadCalibPointsAsync();
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb, "calibration_points", "static", "PostgreSQL", ex.Message);
            MessageBox.Show("Не удалось сохранить калибровку.\nОбратитесь к администратору.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task LoadCalibPointsAsync()
    {
        if (_dgvCalib == null || _calib is null) return;
        int channel = _calibUseCh0 ? 0 : 1;
        var pts = SortCalibPoints(await _calib.GetCalibPointsAsync(channel));
        _dgvCalib.Rows.Clear();
        foreach (var p in pts)
        {
            int row = _dgvCalib.Rows.Add();
            _dgvCalib.Rows[row].Tag = p;
            _dgvCalib.Rows[row].Cells[0].Value = p.AdcCode;
            _dgvCalib.Rows[row].Cells[1].Value = ((double)p.Mass).ToString("G8", CultureInfo.InvariantCulture);
            _dgvCalib.Rows[row].Cells[2].Value = p.IsActive;
            if (p.AdcCode != 0)
                _dgvCalib.Rows[row].Cells[3].Value = ((double)p.Mass / p.AdcCode * 65535).ToString("F4", CultureInfo.InvariantCulture);
            ApplyCalibRowStyle(_dgvCalib.Rows[row]);
        }
    }

    private void LoadCalibPoints() => _ = LoadCalibPointsAsync();

    private void DgvCalib_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
    {
        if (_dgvCalib.IsCurrentCellDirty)
            _dgvCalib.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }

    private void DgvCalib_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            RefreshCalibK(e.RowIndex);
        if (e.ColumnIndex == 2)
            SyncCalibRowActiveState(_dgvCalib.Rows[e.RowIndex]);
    }

    private void RefreshCalibK(int rowIndex)
    {
        var row = _dgvCalib.Rows[rowIndex];
        if (int.TryParse(row.Cells[0].Value?.ToString(), out int code) &&
            double.TryParse(row.Cells[1].Value?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out double mass) &&
            code != 0)
            row.Cells[3].Value = (mass / code * 65535).ToString("F4", CultureInfo.InvariantCulture);
        else
            row.Cells[3].Value = "";
    }

    private void SetCalibRowActive(DataGridViewRow row, bool isActive, DateTime? deletedAt = null)
    {
        row.Cells[2].Value = isActive;
        var point = row.Tag as CalibPoint;
        if (point is null && !isActive)
        {
            point = new CalibPoint();
            row.Tag = point;
        }
        if (point is not null)
        {
            point.IsActive = isActive;
            point.DeletedAt = isActive ? null : deletedAt ?? point.DeletedAt ?? DateTime.Now;
        }
        ApplyCalibRowStyle(row);
    }

    private void SyncCalibRowActiveState(DataGridViewRow row)
    {
        bool active = row.Cells[2].Value is true;
        var point = row.Tag as CalibPoint;

        if (point?.DeletedAt is not null && active)
        {
            row.Cells[2].Value = false;
            point.IsActive = false;
            ApplyCalibRowStyle(row);
            return;
        }

        if (point is null && !active)
        {
            point = new CalibPoint();
            row.Tag = point;
        }

        if (point is not null)
        {
            point.IsActive = active;
            point.DeletedAt = active ? null : point.DeletedAt ?? DateTime.Now;
        }

        ApplyCalibRowStyle(row);
    }

    private static void ApplyCalibRowStyle(DataGridViewRow row)
    {
        bool active = row.Cells[2].Value is true;
        if (active)
        {
            row.DefaultCellStyle.BackColor = UiColors.Surface;
            row.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
            row.DefaultCellStyle.SelectionBackColor = UiColors.GridSelectionBack;
            row.DefaultCellStyle.SelectionForeColor = UiColors.GridSelectionText;
            row.ReadOnly = false;
            return;
        }

        var deletedBack = Color.FromArgb(255, 228, 232);
        row.DefaultCellStyle.BackColor = deletedBack;
        row.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
        row.DefaultCellStyle.SelectionBackColor = deletedBack;
        row.DefaultCellStyle.SelectionForeColor = UiColors.TextPrimary;
        row.ReadOnly = true;
    }

    private List<CalibPoint> ReadGridPoints()
    {
        var result = new List<CalibPoint>();
        int channel = _calibUseCh0 ? 0 : 1;

        foreach (DataGridViewRow row in _dgvCalib.Rows)
        {
            if (!int.TryParse(row.Cells[0].Value?.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int code))
                continue;
            if (!decimal.TryParse(row.Cells[1].Value?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out decimal mass))
                continue;

            bool active = row.Cells[2].Value is true;
            var existing = row.Tag as CalibPoint;
            DateTime? deletedAt = existing?.DeletedAt ?? (active ? null : DateTime.Now);
            active = active && deletedAt is null;
            result.Add(new CalibPoint
            {
                Id = existing?.Id ?? 0,
                Channel = channel,
                AdcCode = code,
                Mass = mass,
                IsActive = active,
                DeletedAt = deletedAt,
            });
        }

        return SortCalibPoints(result).ToList();
    }

    private static IEnumerable<CalibPoint> SortCalibPoints(IEnumerable<CalibPoint> points)
    {
        return points
            .OrderBy(p => !p.IsActive)
            .ThenByDescending(p => p.IsActive ? p.Mass : decimal.MinValue)
            .ThenBy(p => p.AdcCode);
    }

    private void UpdateLiveAdcLabel()
    {
        int code = _calibUseCh0 ? _lastCh0 : _lastCh1;
        _lblLiveAdc.Text = code == 0 ? "—" : code.ToString();
        UpdateLiveDynamicCalibrationLabels();
    }

    // ── Calibration dynamic ─────────────────────────────────────────────────

    private int CurrentDynamicAdcCode()
    {
        if (_dynamicSim is null) return 0;
        return _dynamicSim.Channel == ActiveChannel.Main ? _lastDynCh0 : _lastDynCh1;
    }

    private void UpdateLiveDynamicCalibrationLabels()
    {
        if (_lblLiveAdcD is null) return;

        int code = CurrentDynamicAdcCode();
        _lblLiveAdcD.Text = code == 0 ? "—" : code.ToString();
        _lblLiveAdcD.ForeColor = code == 0 ? UiColors.Disconnected : UiColors.Info;
        UpdateLiveDynamicWeight(code);
    }

    private void UpdateLiveDynamicWeight(int code)
    {
        if (_lblLiveWeightD is null) return;
        if (code == 0)
        {
            _lblLiveWeightD.Text = "—";
            _lblLiveWeightD.ForeColor = UiColors.Disconnected;
            return;
        }

        bool plusOk = double.TryParse(_txtKPlus.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double kp);
        bool minusOk = double.TryParse(_txtKMinus.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double km);

        if (!plusOk && !minusOk)
        {
            _lblLiveWeightD.Text = "—";
            _lblLiveWeightD.ForeColor = UiColors.Disconnected;
            return;
        }

        string plus = plusOk ? (code * kp).ToString("F2", CultureInfo.InvariantCulture) : "—";
        string minus = minusOk ? (code * km).ToString("F2", CultureInfo.InvariantCulture) : "—";
        _lblLiveWeightD.Text = $"→ {plus} т  ← {minus} т";
        _lblLiveWeightD.ForeColor = UiColors.Info;
    }

    private void UpdateDynamicCalibrationConnectionLabel()
    {
        if (_lblDynamicCalibConn is null || _dynamicSim is null) return;

        if (_dynamicSim.IsConnected)
        {
            _lblDynamicCalibConn.Text = $"Динамика: {_dynamicSim.PortName}";
            _lblDynamicCalibConn.ForeColor = UiColors.PrimaryAction;
        }
        else if (_dynamicSim.IsPortOpen)
        {
            _lblDynamicCalibConn.Text = $"Порт открыт: {_dynamicSim.PortName}";
            _lblDynamicCalibConn.ForeColor = UiColors.Warning;
        }
        else
        {
            _lblDynamicCalibConn.Text = "Динамика: нет подключения";
            _lblDynamicCalibConn.ForeColor = UiColors.Disconnected;
        }
    }

    private async void BtnCalibDynSave_Click(object? sender, EventArgs e)
    {
        string plusText = _txtKPlus.Text.Trim();
        string minusText = _txtKMinus.Text.Trim();
        bool hasPlus = plusText.Length > 0;
        bool hasMinus = minusText.Length > 0;

        if (!hasPlus && !hasMinus)
        {
            MessageBox.Show("Введите K→ или K← для сохранения.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        double kp = _calib.Dynamic.KPlus;
        double km = _calib.Dynamic.KMinus;
        if (hasPlus && !double.TryParse(plusText, NumberStyles.Float, CultureInfo.InvariantCulture, out kp))
        {
            MessageBox.Show("Некорректное значение K→.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (hasMinus && !double.TryParse(minusText, NumberStyles.Float, CultureInfo.InvariantCulture, out km))
        {
            MessageBox.Show("Некорректное значение K←.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            await _calib.SaveDynamicCalibAsync(new DynamicCalib { KPlus = kp, KMinus = km });
            await LoadCalibDynamicAsync();
            MessageBox.Show("Калибровка динамики сохранена.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AuditLogger.Action(AuditLogger.CalibrationSaved, "CalibProfile", $"dynamic id={_calib.Dynamic.Id} kp={kp:G4} km={km:G4}");
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb, "CalibProfile", "dynamic", "PostgreSQL", ex.Message);
            MessageBox.Show("Не удалось сохранить калибровку динамики.\nОбратитесь к администратору.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void ApplyDynamicCalibRowStyle(DataGridViewRow row, bool isActive)
    {
        if (isActive)
        {
            row.DefaultCellStyle.BackColor = UiColors.Surface;
            row.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
            row.DefaultCellStyle.SelectionBackColor = UiColors.GridSelectionBack;
            row.DefaultCellStyle.SelectionForeColor = UiColors.GridSelectionText;
            row.ReadOnly = false;
            return;
        }

        var deletedBack = Color.FromArgb(255, 228, 232);
        row.DefaultCellStyle.BackColor = deletedBack;
        row.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
        row.DefaultCellStyle.SelectionBackColor = deletedBack;
        row.DefaultCellStyle.SelectionForeColor = UiColors.TextPrimary;
        row.ReadOnly = true;
    }

    private void LoadCalibDynamic() => _ = LoadCalibDynamicAsync();

    private async Task LoadCalibDynamicAsync()
    {
        if (_calib is null) return;

        _txtKPlus.Text = _calib.Dynamic.KPlus.ToString("G8", CultureInfo.InvariantCulture);
        _txtKMinus.Text = _calib.Dynamic.KMinus.ToString("G8", CultureInfo.InvariantCulture);

        try
        {
            var rows = await _calib.GetDynamicCalibsAsync();
            _dgvDynCalib.Rows.Clear();
            foreach (var row in rows)
            {
                int idx = _dgvDynCalib.Rows.Add(
                    row.IsActive ? "Да" : "",
                    row.KPlus.ToString("G8", CultureInfo.InvariantCulture),
                    row.KMinus.ToString("G8", CultureInfo.InvariantCulture),
                    row.CreatedAt == default ? "" : row.CreatedAt.ToLocalTime().ToString("dd.MM.yy HH:mm"),
                    row.DeletedAt?.ToLocalTime().ToString("dd.MM.yy HH:mm") ?? "");

                ApplyDynamicCalibRowStyle(_dgvDynCalib.Rows[idx], row.IsActive);
            }
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb, "CalibProfile", "dynamic history", "PostgreSQL", ex.Message);
        }
    }

    // ── Admin ────────────────────────────────────────────────────────────────

    private void BtnAdmin_Click(object? sender, EventArgs e)
    {
        if (_adminUnlocked)
        {
            _adminUnlocked = false;
            _btnAdmin.Text = "🔒 Войти как администратор";
            _btnAdmin.BackColor = UiColors.AdminLocked;
            SetAdminTabs(false);
            AuditLogger.Action(AuditLogger.AdminLogin, "AdminSession", "выход из режима администратора");
        }
        else
        {
            using var dlg = new PasswordDialog(_settings);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            _adminUnlocked = true;
            _btnAdmin.Text = "🔓 Выйти из режима администратора";
            _btnAdmin.BackColor = UiColors.AdminUnlocked;
            SetAdminTabs(true);
            AuditLogger.Action(AuditLogger.AdminLogin, "AdminSession", "вход в режим администратора");
        }
    }

    private void SetAdminTabs(bool enabled)
    {
        _tabCalibS.Enabled = enabled;
        _tabCalibD.Enabled = enabled;
        _tabSett.Enabled = enabled;
    }
}
