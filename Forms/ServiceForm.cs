using System.Globalization;
using System.IO.Ports;
using System.Text;
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
    private SimA04ReaderStatic _staticServiceSim = null!;
    private SimA04ReaderStatic _staticCalibSim = null!;
    private SimA04ReaderDynamic _dynamicServiceSim = null!;
    private SimA04ReaderDynamic _dynamicCalibSim = null!;
    private LocalRepository _calib = null!;
    private SettingsService _settings = null!;
    private bool _adminUnlocked;
    private bool _calibUseCh0 = true;
    private int _frameCount;
    private long _dynamicServiceSampleRateCount;
    private long _dynamicServiceSampleUiPosted;
    private long _dynamicServiceSampleUiApplied;
    private long _dynamicServiceRawUiPosted;
    private long _dynamicServiceRawUiApplied;
    private long _dynamicServiceLogAppended;
    private long _dynamicServiceLogMaxMs;
    private long _dynamicServiceDiagLastTick;
    private long _dynamicCalibDiagLastTick;
    private long _dynamicCalibSampleUiPosted;
    private long _dynamicCalibSampleUiApplied;
    private long _dynamicCalibLogAppended;
    private long _dynamicCalibDisplayTicks;
    private long _dynamicCalibDisplayApplied;
    private long _dynamicCalibDisplaySkipped;
    private long _dynamicCalibDisplayMaxMs;
    private readonly object _dynamicSampleSync = new();
    private readonly System.Windows.Forms.Timer _dynamicCalibDisplayTimer = new() { Interval = 100 };
    private SimA04DynamicSample _latestDynamicSample;
    private long _latestDynamicSampleVersion;
    private long _displayedDynamicSampleVersion;
    private readonly object _dynamicServiceSampleSync = new();
    private readonly System.Windows.Forms.Timer _dynamicServiceDisplayTimer = new() { Interval = 100 };
    private SimA04DynamicSample _latestDynamicServiceSample;
    private long _latestDynamicServiceSampleVersion;
    private long _displayedDynamicServiceSampleVersion;
    private readonly object _dynamicServiceLogSync = new();
    private readonly Queue<(string Text, Color Color)> _dynamicServiceLogQueue = new();
    private const int DynamicServiceLogQueueLimit = 500;
    private long _dynamicServiceLogDropped;
    // Лог динамики — owner-drawn ListBox (_lstDynamicLog из Designer): элементы DynamicServiceLogLine,
    // число строк ограничено DynamicServiceLogLineLimit. ListBox рисует только видимые строки и не
    // копит внутреннее состояние (нет RTF/undo/переформатирования) — аллокации не растут со временем.
    private const int DynamicServiceLogLineLimit = 300;
    // Переиспользуемые буферы: формирование строки лога (только из потока reader-а — SerialPort
    // не поднимает DataReceived параллельно для одного порта, блокировка не нужна) и пачка на flush.
    private readonly StringBuilder _dynamicServiceLogBuilder = new(64);
    private readonly List<(string Text, Color Color)> _dynamicServiceLogBatch = new();
    private long _lastDiagRawBytes;
    private long _lastDiagSamples;
    private long _lastDiagSkippedBytes;
    private long _lastDiagSamplePosted;
    private long _lastDiagSampleApplied;
    private long _lastDiagRawPosted;
    private long _lastDiagRawApplied;
    private long _lastDiagLogAppended;
    private long _lastDiagLogDropped;
    private long _lastCalibDiagRawBytes;
    private long _lastCalibDiagSamples;
    private long _lastCalibDiagSkippedBytes;
    private long _lastCalibDiagSamplePosted;
    private long _lastCalibDiagSampleApplied;
    private long _lastCalibDiagDisplayTicks;
    private long _lastCalibDiagDisplayApplied;
    private long _lastCalibDiagDisplaySkipped;
    private long _lastCalibDiagLogAppended;
    private bool _dynamicServiceDataSubscribed;
    private bool _dynamicCalibDataSubscribed;
    private bool _dynamicCalibTabActive;
    private int _lastCh0;
    private int _lastCh1;
    private int _lastStaticCalibCh0;
    private int _lastStaticCalibCh1;
    private int _lastDynCh0;
    private int _lastDynCh1;


    public ServiceForm()
    {
        InitializeComponent();
        InitializeDynamicCalibDisplayTimer();
    }

    public ServiceForm(SimA04ReaderStatic adc, SimA04ReaderDynamic dynamicAdc, LocalRepository calib, SettingsService settings)
    {
        _staticServiceSim = adc;
        _staticCalibSim = new SimA04ReaderStatic { Channel = adc.Channel };
        _dynamicServiceSim = dynamicAdc;
        _dynamicCalibSim = new SimA04ReaderDynamic { Channel = dynamicAdc.Channel };
        _calib = calib;
        _settings = settings;
        InitializeComponent();
        InitializeDynamicCalibDisplayTimer();
    }

    private void InitializeDynamicCalibDisplayTimer()
    {
        _dynamicCalibDisplayTimer.Tick += (_, _) => RefreshDynamicSampleDisplay();
        _dynamicServiceDisplayTimer.Tick += (_, _) =>
        {
            RefreshDynamicServiceSampleDisplay();
            FlushDynamicServiceLogQueue();
        };
    }

    private void ApplyTheme()
    {
        BackColor = ServiceUiColors.AppBackground;
        _tabs.BackColor = ServiceUiColors.Surface;
        _tabs.Font = ServiceUiFonts.Medium;

        // Channel tab
        _btnAdmin.Font = ServiceUiFonts.Body;
        _btnAdmin.BackColor = ServiceUiColors.AdminLocked;
        _btnAdmin.ForeColor = ServiceUiColors.TextOnDark;
        _lblChannelTitle.Font = ServiceUiFonts.SubHeaderBold;
        _lblChannelTitle.ForeColor = ServiceUiColors.TextPrimary;
        _rbMain.Font = ServiceUiFonts.NavButton;
        _rbMain.ForeColor = ServiceUiColors.TextPrimary;
        _rbBackup.Font = ServiceUiFonts.NavButton;
        _rbBackup.ForeColor = ServiceUiColors.TextPrimary;
        _lblChannelNote.Font = ServiceUiFonts.Body;
        _lblChannelNote.ForeColor = ServiceUiColors.Disconnected;
        _tabChannel.BackColor = ServiceUiColors.Surface;

        // Monitor static tab
        _tabMonitor.BackColor = ServiceUiColors.Surface;
        _cmbPort.Font = ServiceUiFonts.Medium;
        _cmbPort.BackColor = ServiceUiColors.InputBack;
        _cmbPort.ForeColor = ServiceUiColors.InputFore;
        _btnConn.Font = ServiceUiFonts.Body;
        _btnConn.BackColor = ServiceUiColors.PrimaryAction;
        _btnConn.ForeColor = ServiceUiColors.TextOnDark;
        _btnPortRefresh.Font = ServiceUiFonts.SubHeader;
        _btnPortRefresh.BackColor = ServiceUiColors.NeutralAction;
        _btnPortRefresh.ForeColor = ServiceUiColors.TextPrimary;
        _lblConn.Font = ServiceUiFonts.Body;
        _lblConn.ForeColor = ServiceUiColors.Disconnected;
        _lblRate.Font = ServiceUiFonts.Body;
        _lblRate.ForeColor = ServiceUiColors.Disconnected;
        _lblCh0Cap.Font = ServiceUiFonts.Body;
        _lblCh0Cap.ForeColor = ServiceUiColors.TextOnDarkMuted;
        _lblCh0.Font = ServiceUiFonts.MonitorDisplay;
        _lblCh0.ForeColor = ServiceUiColors.Disconnected;
        _lblCh1Cap.Font = ServiceUiFonts.Body;
        _lblCh1Cap.ForeColor = ServiceUiColors.TextOnDarkMuted;
        _lblCh1.Font = ServiceUiFonts.MonitorDisplay;
        _lblCh1.ForeColor = ServiceUiColors.Disconnected;
        _chkLog.Font = ServiceUiFonts.Body;
        _chkLog.ForeColor = ServiceUiColors.TextPrimary;
        _btnClearLog.Font = ServiceUiFonts.Body;
        _btnClearLog.BackColor = ServiceUiColors.NeutralAction;
        _btnClearLog.ForeColor = ServiceUiColors.TextPrimary;
        _rtbLog.Font = ServiceUiFonts.MonoSmall;
        _rtbLog.BackColor = ServiceUiColors.LogBackground;
        _rtbLog.ForeColor = ServiceUiColors.LogText;
        _pnlCh0.BackColor = ServiceUiColors.MonitorBackground;
        _pnlCh1.BackColor = ServiceUiColors.MonitorBackground;
        ApplyDynamicServiceTheme();

        // CalibStatic tab
        _tabCalibS.BackColor = ServiceUiColors.Surface;
        _pnlCalibS.BackColor = ServiceUiColors.Surface;
        _pnlCalibSHead.BackColor = ServiceUiColors.Surface;
        _pnlCalibSBody.BackColor = ServiceUiColors.Surface;
        _cmbStaticCalibPort.Font = ServiceUiFonts.Medium;
        _cmbStaticCalibPort.BackColor = ServiceUiColors.InputBack;
        _cmbStaticCalibPort.ForeColor = ServiceUiColors.InputFore;
        _btnStaticCalibConn.Font = ServiceUiFonts.Body;
        _btnStaticCalibConn.BackColor = ServiceUiColors.PrimaryAction;
        _btnStaticCalibConn.ForeColor = ServiceUiColors.TextOnDark;
        _btnStaticCalibPortRefresh.Font = ServiceUiFonts.SubHeader;
        _btnStaticCalibPortRefresh.BackColor = ServiceUiColors.NeutralAction;
        _btnStaticCalibPortRefresh.ForeColor = ServiceUiColors.TextPrimary;
        _lblStaticCalibConn.Font = ServiceUiFonts.Body;
        _lblStaticCalibConn.ForeColor = ServiceUiColors.Disconnected;
        _rbCh0Calib.Font = ServiceUiFonts.SubHeader;
        _rbCh0Calib.ForeColor = ServiceUiColors.TextPrimary;
        _rbCh1Calib.Font = ServiceUiFonts.SubHeader;
        _rbCh1Calib.ForeColor = ServiceUiColors.TextPrimary;
        _lblLiveAdcCap.Font = ServiceUiFonts.Body;
        _lblLiveAdcCap.ForeColor = ServiceUiColors.Disconnected;
        _lblLiveAdc.Font = ServiceUiFonts.MonoLiveAdc;
        _lblLiveAdc.ForeColor = ServiceUiColors.Info;
        if (_lblStaticCalibMassCap is not null)
        {
            _lblStaticCalibMassCap.Font = ServiceUiFonts.Body;
            _lblStaticCalibMassCap.ForeColor = ServiceUiColors.Disconnected;
        }
        if (_lblStaticCalibMass is not null)
        {
            _lblStaticCalibMass.Font = ServiceUiFonts.MonoLiveAdc;
            _lblStaticCalibMass.ForeColor = ServiceUiColors.Info;
        }
        _btnCapture.Font = ServiceUiFonts.Body;
        _btnCapture.BackColor = ServiceUiColors.NeutralAction;
        _btnCapture.ForeColor = ServiceUiColors.TextPrimary;
        _dgvCalib.Font = ServiceUiFonts.GridBody;
        _dgvCalib.BackgroundColor = ServiceUiColors.Surface;
        _dgvCalib.ColumnHeadersDefaultCellStyle.BackColor = ServiceUiColors.GridHeaderBack;
        _dgvCalib.ColumnHeadersDefaultCellStyle.ForeColor = ServiceUiColors.GridHeaderText;
        _dgvCalib.ColumnHeadersDefaultCellStyle.SelectionBackColor = ServiceUiColors.GridHeaderBack;
        _dgvCalib.ColumnHeadersDefaultCellStyle.SelectionForeColor = ServiceUiColors.GridHeaderText;
        _dgvCalib.DefaultCellStyle.BackColor = ServiceUiColors.GridRowBack;
        _dgvCalib.DefaultCellStyle.ForeColor = ServiceUiColors.TextPrimary;
        _dgvCalib.DefaultCellStyle.SelectionBackColor = ServiceUiColors.GridSelectionBack;
        _dgvCalib.DefaultCellStyle.SelectionForeColor = ServiceUiColors.GridSelectionText;
        _dgvCalib.GridColor = ServiceUiColors.GridLine;
        _btnAddRow.Font = ServiceUiFonts.Body;
        _btnAddRow.BackColor = ServiceUiColors.NeutralAction;
        _btnAddRow.ForeColor = ServiceUiColors.TextPrimary;
        _btnDelRow.Font = ServiceUiFonts.Body;
        _btnDelRow.BackColor = ServiceUiColors.NeutralAction;
        _btnDelRow.ForeColor = ServiceUiColors.TextPrimary;
        _lblKEquals.Font = ServiceUiFonts.Medium;
        _lblKEquals.ForeColor = ServiceUiColors.TextPrimary;
        _txtK.Font = ServiceUiFonts.Mono;
        _txtK.BackColor = ServiceUiColors.InputBack;
        _txtK.ForeColor = ServiceUiColors.InputFore;
        _lblBEquals.Font = ServiceUiFonts.Medium;
        _lblBEquals.ForeColor = ServiceUiColors.TextPrimary;
        _txtB.Font = ServiceUiFonts.Mono;
        _txtB.BackColor = ServiceUiColors.InputBack;
        _txtB.ForeColor = ServiceUiColors.InputFore;
        _lblFormula.Font = ServiceUiFonts.Body;
        _lblFormula.ForeColor = ServiceUiColors.TextMuted;
        _btnLsq.Font = ServiceUiFonts.Medium;
        _btnLsq.BackColor = ServiceUiColors.SecondaryAction;
        _btnLsq.ForeColor = ServiceUiColors.TextOnDark;
        _btnCalibSave.Font = ServiceUiFonts.Body;
        _btnCalibSave.BackColor = ServiceUiColors.PrimaryAction;
        _btnCalibSave.ForeColor = ServiceUiColors.TextOnDark;

        // CalibDynamic tab
        _tabCalibD.BackColor = ServiceUiColors.Surface;
        _pnlCalibD.BackColor = ServiceUiColors.Surface;
        _pnlCalibDHead.BackColor = ServiceUiColors.Surface;
        _pnlCalibDBody.BackColor = ServiceUiColors.Surface;
        _pnlCalibDBottom.BackColor = ServiceUiColors.Surface;
        _lblLiveAdcCapD.Font = ServiceUiFonts.Body;
        _lblLiveAdcCapD.ForeColor = ServiceUiColors.TextPrimary;
        _lblLiveAdcD.Font = ServiceUiFonts.MonoLiveAdc;
        _lblLiveAdcD.ForeColor = ServiceUiColors.TextOnDark;
        _lblLiveWeightCapD.Font = ServiceUiFonts.Body;
        _lblLiveWeightCapD.ForeColor = ServiceUiColors.TextPrimary;
        _lblLiveWeightD.Font = ServiceUiFonts.MonoLiveAdc;
        _lblLiveWeightD.ForeColor = ServiceUiColors.TextOnDark;
        _cmbDynamicCalibPort.Font = ServiceUiFonts.Medium;
        _cmbDynamicCalibPort.BackColor = ServiceUiColors.InputBack;
        _cmbDynamicCalibPort.ForeColor = ServiceUiColors.InputFore;
        _btnDynamicCalibConn.Font = ServiceUiFonts.Body;
        _btnDynamicCalibConn.BackColor = ServiceUiColors.PrimaryAction;
        _btnDynamicCalibConn.ForeColor = ServiceUiColors.TextOnDark;
        _btnDynamicCalibPortRefresh.Font = ServiceUiFonts.SubHeader;
        _btnDynamicCalibPortRefresh.BackColor = ServiceUiColors.NeutralAction;
        _btnDynamicCalibPortRefresh.ForeColor = ServiceUiColors.TextPrimary;
        _lblDynamicCalibConn.Font = ServiceUiFonts.Body;
        _lblDynamicCalibConn.ForeColor = ServiceUiColors.TextPrimary;
        _lblSecPlus_00.Font = ServiceUiFonts.BodyBold;
        _lblSecPlus_00.ForeColor = ServiceUiColors.TextPrimary;
        _lblSecPlus_01.Font = ServiceUiFonts.BodyBold;
        _lblSecPlus_01.ForeColor = ServiceUiColors.TextPrimary;
        _lblSecPlus_02.Font = ServiceUiFonts.BodyBold;
        _lblSecPlus_02.ForeColor = ServiceUiColors.TextPrimary;
        _lblKPlusEquals.Font = ServiceUiFonts.Medium;
        _lblKPlusEquals.ForeColor = ServiceUiColors.TextPrimary;
        _txtKPlus.Font = ServiceUiFonts.Mono;
        _txtKPlus.BackColor = ServiceUiColors.InputBack;
        _txtKPlus.ForeColor = ServiceUiColors.InputFore;
        _lblAutoCalcPlus.Font = ServiceUiFonts.Body;
        _lblAutoCalcPlus.ForeColor = ServiceUiColors.Disconnected;
        _lblCodePlusCap.Font = ServiceUiFonts.Body;
        _lblCodePlusCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtCodePlus.Font = ServiceUiFonts.MonoSmall;
        _txtCodePlus.BackColor = ServiceUiColors.InputBack;
        _txtCodePlus.ForeColor = ServiceUiColors.InputFore;
        _btnCapPlus.Font = ServiceUiFonts.Small;
        _btnCapPlus.BackColor = ServiceUiColors.NeutralAction;
        _btnCapPlus.ForeColor = ServiceUiColors.TextPrimary;
        _lblMassPlusCap.Font = ServiceUiFonts.Body;
        _lblMassPlusCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtMassPlus.Font = ServiceUiFonts.MonoSmall;
        _txtMassPlus.BackColor = ServiceUiColors.InputBack;
        _txtMassPlus.ForeColor = ServiceUiColors.InputFore;
        _btnCalcPlus.Font = ServiceUiFonts.Body;
        _btnCalcPlus.BackColor = ServiceUiColors.SecondaryAction;
        _btnCalcPlus.ForeColor = ServiceUiColors.TextOnDark;
        _lblSecMinus_00.Font = ServiceUiFonts.BodyBold;
        _lblSecMinus_00.ForeColor = ServiceUiColors.TextPrimary;
        _lblSecMinus_01.Font = ServiceUiFonts.BodyBold;
        _lblSecMinus_01.ForeColor = ServiceUiColors.TextPrimary;
        _lblSecMinus_02.Font = ServiceUiFonts.BodyBold;
        _lblSecMinus_02.ForeColor = ServiceUiColors.TextPrimary;
        _lblKMinusEquals.Font = ServiceUiFonts.Medium;
        _lblKMinusEquals.ForeColor = ServiceUiColors.TextPrimary;
        _txtKMinus.Font = ServiceUiFonts.Mono;
        _txtKMinus.BackColor = ServiceUiColors.InputBack;
        _txtKMinus.ForeColor = ServiceUiColors.InputFore;
        _lblAutoCalcMinus.Font = ServiceUiFonts.Body;
        _lblAutoCalcMinus.ForeColor = ServiceUiColors.Disconnected;
        _lblCodeMinusCap.Font = ServiceUiFonts.Body;
        _lblCodeMinusCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtCodeMinus.Font = ServiceUiFonts.MonoSmall;
        _txtCodeMinus.BackColor = ServiceUiColors.InputBack;
        _txtCodeMinus.ForeColor = ServiceUiColors.InputFore;
        _btnCapMinus.Font = ServiceUiFonts.Small;
        _btnCapMinus.BackColor = ServiceUiColors.NeutralAction;
        _btnCapMinus.ForeColor = ServiceUiColors.TextPrimary;
        _lblMassMinusCap.Font = ServiceUiFonts.Body;
        _lblMassMinusCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtMassMinus.Font = ServiceUiFonts.MonoSmall;
        _txtMassMinus.BackColor = ServiceUiColors.InputBack;
        _txtMassMinus.ForeColor = ServiceUiColors.InputFore;
        _btnCalcMinus.Font = ServiceUiFonts.Body;
        _btnCalcMinus.BackColor = ServiceUiColors.SecondaryAction;
        _btnCalcMinus.ForeColor = ServiceUiColors.TextOnDark;
        _lblFormulaD.Font = ServiceUiFonts.Body;
        _lblFormulaD.ForeColor = ServiceUiColors.TextMuted;
        _btnCalibDynSave.Font = ServiceUiFonts.Body;
        _btnCalibDynSave.BackColor = ServiceUiColors.PrimaryAction;
        _btnCalibDynSave.ForeColor = ServiceUiColors.TextOnDark;
        _dgvDynCalib.Font = ServiceUiFonts.GridBody;
        _dgvDynCalib.BackgroundColor = ServiceUiColors.Surface;
        _dgvDynCalib.ColumnHeadersDefaultCellStyle.BackColor = ServiceUiColors.GridHeaderBack;
        _dgvDynCalib.ColumnHeadersDefaultCellStyle.ForeColor = ServiceUiColors.GridHeaderText;
        _dgvDynCalib.ColumnHeadersDefaultCellStyle.Font = ServiceUiFonts.GridHeader;
        _dgvDynCalib.ColumnHeadersDefaultCellStyle.SelectionBackColor = ServiceUiColors.GridHeaderBack;
        _dgvDynCalib.ColumnHeadersDefaultCellStyle.SelectionForeColor = ServiceUiColors.GridHeaderText;
        _dgvDynCalib.DefaultCellStyle.BackColor = ServiceUiColors.GridRowBack;
        _dgvDynCalib.DefaultCellStyle.ForeColor = ServiceUiColors.TextPrimary;
        _dgvDynCalib.DefaultCellStyle.SelectionBackColor = ServiceUiColors.GridSelectionBack;
        _dgvDynCalib.DefaultCellStyle.SelectionForeColor = ServiceUiColors.GridSelectionText;
        _dgvDynCalib.GridColor = ServiceUiColors.GridLine;

        // Settings tab
        _tabSett.BackColor = ServiceUiColors.Surface;
        _lblPortCap.Font = ServiceUiFonts.Medium;
        _lblPortCap.ForeColor = ServiceUiColors.TextPrimary;
        _cmbSettPort.Font = ServiceUiFonts.Body;
        _cmbSettPort.BackColor = ServiceUiColors.InputBack;
        _cmbSettPort.ForeColor = ServiceUiColors.InputFore;
        _lblNpvCap.Font = ServiceUiFonts.Medium;
        _lblNpvCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtNpv.Font = ServiceUiFonts.Body;
        _txtNpv.BackColor = ServiceUiColors.InputBack;
        _txtNpv.ForeColor = ServiceUiColors.InputFore;
        _lblDiscCap.Font = ServiceUiFonts.Medium;
        _lblDiscCap.ForeColor = ServiceUiColors.TextPrimary;
        _cmbDisc.Font = ServiceUiFonts.Body;
        _cmbDisc.BackColor = ServiceUiColors.InputBack;
        _cmbDisc.ForeColor = ServiceUiColors.InputFore;
        _lblZeroCap.Font = ServiceUiFonts.Medium;
        _lblZeroCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtZeroLimit.Font = ServiceUiFonts.Body;
        _txtZeroLimit.BackColor = ServiceUiColors.InputBack;
        _txtZeroLimit.ForeColor = ServiceUiColors.InputFore;
        _lblPasswordCap.Font = ServiceUiFonts.Medium;
        _lblPasswordCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtNewPassword.Font = ServiceUiFonts.Body;
        _txtNewPassword.BackColor = ServiceUiColors.InputBack;
        _txtNewPassword.ForeColor = ServiceUiColors.InputFore;
        _btnSaveSettings.Font = ServiceUiFonts.Medium;
        _btnSaveSettings.BackColor = ServiceUiColors.PrimaryAction;
        _btnSaveSettings.ForeColor = ServiceUiColors.TextOnDark;
    }

    // ── Lifecycle ───────────────────────────────────────────────────────────

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ApplyTheme();
        if (DesignMode || _staticServiceSim is null) return;
        AuditLogger.Action(AuditLogger.FormOpened, "Form", "ServiceForm");
        _staticServiceSim.ConnectionTimeoutMs = 1000;
        _staticCalibSim.ConnectionTimeoutMs = 1000;
        _dynamicServiceSim.ConnectionTimeoutMs = 5000;
        _dynamicCalibSim.ConnectionTimeoutMs = 5000;
        _tabs.SelectedIndexChanged += Tabs_SelectedIndexChanged;
        _staticServiceSim.RawFrameReceived += OnStaticServiceRawFrame;
        _staticServiceSim.ConnectionChanged += OnStaticServiceConnectionChanged;
        _staticCalibSim.RawFrameReceived += OnStaticCalibRawFrame;
        _staticCalibSim.ConnectionChanged += OnStaticCalibConnectionChanged;
        _dynamicServiceSim.ConnectionChanged += OnDynamicServiceConnectionChanged;
        _dynamicCalibSim.ConnectionChanged += OnDynamicCalibConnectionChanged;
        _dgvCalib.CellValueChanged += DgvCalib_CellValueChanged;
        _dgvCalib.CurrentCellDirtyStateChanged += DgvCalib_CurrentCellDirtyStateChanged;
        _rateTimer.Start();
        _rbMain.Checked = _staticServiceSim.Channel == ActiveChannel.Main;
        _rbBackup.Checked = _staticServiceSim.Channel == ActiveChannel.Backup;
        RefreshPorts();
        RefreshDynamicPorts();
        LoadSettingsUi();
        LoadCalibPoints();
        LoadCalibDynamic();
        SetAdminTabs(false);
        UpdateStaticServiceMonitorConn(_staticServiceSim.IsConnected);
        UpdateStaticCalibMonitorConn(_staticCalibSim.IsConnected);
        UpdateDynamicServiceMonitorConn(_dynamicServiceSim.IsConnected);
        UpdateDynamicCalibMonitorConn(_dynamicCalibSim.IsConnected);
        UpdateDynamicDataSubscriptions();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (!DesignMode && _staticServiceSim is not null)
        {
            _tabs.SelectedIndexChanged -= Tabs_SelectedIndexChanged;
            _staticServiceSim.RawFrameReceived -= OnStaticServiceRawFrame;
            _staticServiceSim.ConnectionChanged -= OnStaticServiceConnectionChanged;
            if (_staticServiceSim.IsPortOpen)
                _staticServiceSim.Close();

            _staticCalibSim.RawFrameReceived -= OnStaticCalibRawFrame;
            _staticCalibSim.ConnectionChanged -= OnStaticCalibConnectionChanged;
            if (_staticCalibSim.IsPortOpen)
                _staticCalibSim.Close();
            _staticCalibSim.Dispose();

            SetDynamicServiceDataSubscription(false);
            SetDynamicCalibDataSubscription(false);
            _dynamicServiceSim.ConnectionChanged -= OnDynamicServiceConnectionChanged;
            _dynamicCalibSim.ConnectionChanged -= OnDynamicCalibConnectionChanged;
            if (_dynamicServiceSim.IsPortOpen)
                _dynamicServiceSim.Close();
            if (_dynamicCalibSim.IsPortOpen)
                _dynamicCalibSim.Close();
            _dynamicCalibSim.Dispose();

            _rateTimer.Stop();
            _dynamicCalibDisplayTimer.Stop();
            _dynamicCalibDisplayTimer.Dispose();
            _dynamicServiceDisplayTimer.Stop();
            _dynamicServiceDisplayTimer.Dispose();
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
        _dgvCalib.CurrentCell = _dgvCalib.Rows[row].Cells[1];
        _dgvCalib.BeginEdit(true);
    }
    private void BtnCapture_Click(object? sender, EventArgs e)
    {
        int code = _calibUseCh0 ? _lastStaticCalibCh0 : _lastStaticCalibCh1;
        if (code == 0) return;
        int row = _dgvCalib.Rows.Add();
        _dgvCalib.Rows[row].Cells[1].Value = code;
        SetCalibRowActive(_dgvCalib.Rows[row], true);
        _dgvCalib.CurrentCell = _dgvCalib.Rows[row].Cells[2];
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
        if (_staticServiceSim is null) return;
        if (_staticServiceSim.Channel == channel &&
            (_staticCalibSim is null || _staticCalibSim.Channel == channel) &&
            (_dynamicServiceSim is null || _dynamicServiceSim.Channel == channel) &&
            (_dynamicCalibSim is null || _dynamicCalibSim.Channel == channel)) return;

        ActiveChannel old = _staticServiceSim.Channel;
        _staticServiceSim.Channel = channel;
        if (_staticCalibSim is not null)
            _staticCalibSim.Channel = channel;
        if (_dynamicServiceSim is not null)
            _dynamicServiceSim.Channel = channel;
        if (_dynamicCalibSim is not null)
            _dynamicCalibSim.Channel = channel;
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
            _lblDynamicRate.Text = $"{Interlocked.Exchange(ref _dynamicServiceSampleRateCount, 0)} сэмпл/с";
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
        if (_staticServiceSim is null || _staticCalibSim is null) return;
        var ports = SerialPort.GetPortNames();
        FillStaticPortCombo(_cmbPort, ports, _staticServiceSim.PortName);
        FillStaticPortCombo(_cmbStaticCalibPort, ports, _staticCalibSim.PortName);
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

    private static void FillStaticPortCombo(ComboBox? combo, string[] ports, string fallbackPort)
    {
        if (combo is null) return;
        string? selected = combo.SelectedItem as string;
        combo.Items.Clear();
        if (ports.Length == 0) return;

        combo.Items.AddRange(ports);
        int idx = Array.IndexOf(ports, selected ?? fallbackPort);
        combo.SelectedIndex = idx >= 0 ? idx : 0;
    }

    private void BtnMonConn_Click(object? sender, EventArgs e)
    {
        ToggleStaticServiceConnection(_cmbPort.SelectedItem as string, ex => AppendLog($"ОШИБКА: {ex.Message}", ServiceUiColors.Error));
    }

    private void BtnStaticCalibConn_Click(object? sender, EventArgs e)
    {
        ToggleStaticCalibConnection(_cmbStaticCalibPort.SelectedItem as string,
            ex => MessageBox.Show($"Не удалось подключить АЦП статики.\n{ex.Message}", "АЦП статики", MessageBoxButtons.OK, MessageBoxIcon.Error));
    }

    private void ToggleStaticServiceConnection(string? selectedPort, Action<Exception> onError)
    {
        if (_staticServiceSim.IsPortOpen)
        {
            CloseStaticServiceConnection();
            return;
        }

        if (string.IsNullOrWhiteSpace(selectedPort)) return;

        try
        {
            CloseStaticCalibConnection();
            CloseDynamicConnections();
            _staticServiceSim.Open(selectedPort);
            AuditLogger.Action(AuditLogger.AdcConnected, "AdcConnection", selectedPort, "SimA04StaticService", selectedPort);
            UpdateStaticServiceMonitorConn(_staticServiceSim.IsConnected);
        }
        catch (Exception ex)
        {
            onError(ex);
            AuditLogger.Error(AuditLogger.ErrorAdc, "AdcConnection", selectedPort, "SimA04StaticService", selectedPort);
        }
    }

    private void ToggleStaticCalibConnection(string? selectedPort, Action<Exception> onError)
    {
        if (_staticCalibSim.IsPortOpen)
        {
            CloseStaticCalibConnection();
            return;
        }

        if (string.IsNullOrWhiteSpace(selectedPort)) return;

        try
        {
            CloseStaticServiceConnection();
            CloseDynamicConnections();
            _staticCalibSim.Open(selectedPort);
            AuditLogger.Action(AuditLogger.AdcConnected, "AdcConnection", selectedPort, "SimA04StaticCalib", selectedPort);
            UpdateStaticCalibMonitorConn(_staticCalibSim.IsConnected);
        }
        catch (Exception ex)
        {
            onError(ex);
            AuditLogger.Error(AuditLogger.ErrorAdc, "AdcConnection", selectedPort, "SimA04StaticCalib", selectedPort);
        }
    }


    private void ApplyDynamicServiceTheme()
    {
        if (_tabDynamicService is null) return;
        _cmbDynamicPort.Font = ServiceUiFonts.Medium;
        _cmbDynamicPort.BackColor = ServiceUiColors.InputBack;
        _cmbDynamicPort.ForeColor = ServiceUiColors.InputFore;
        _dotDynamicConn.BackColor = ServiceUiColors.Disconnected;
        _btnDynamicConn.Font = ServiceUiFonts.Body;
        _btnDynamicConn.BackColor = ServiceUiColors.PrimaryAction;
        _btnDynamicConn.ForeColor = ServiceUiColors.TextOnDark;
        _btnDynamicPortRefresh.Font = ServiceUiFonts.SubHeader;
        _btnDynamicPortRefresh.BackColor = ServiceUiColors.NeutralAction;
        _btnDynamicPortRefresh.ForeColor = ServiceUiColors.TextPrimary;
        _lblDynamicConn.Font = ServiceUiFonts.Body;
        _lblDynamicConn.ForeColor = ServiceUiColors.Disconnected;
        _lblDynamicRate.Font = ServiceUiFonts.Body;
        _lblDynamicRate.ForeColor = ServiceUiColors.Disconnected;
        _pnlDynamicCh0.BackColor = ServiceUiColors.MonitorBackground;
        _pnlDynamicCh1.BackColor = ServiceUiColors.MonitorBackground;
        _lblDynamicCh0Cap.Font = ServiceUiFonts.Body;
        _lblDynamicCh0Cap.ForeColor = ServiceUiColors.TextOnDarkMuted;
        _lblDynamicCh1Cap.Font = ServiceUiFonts.Body;
        _lblDynamicCh1Cap.ForeColor = ServiceUiColors.TextOnDarkMuted;
        _lblDynamicCh0.Font = ServiceUiFonts.MonitorDisplay;
        _lblDynamicCh0.ForeColor = ServiceUiColors.Disconnected;
        _lblDynamicCh1.Font = ServiceUiFonts.MonitorDisplay;
        _lblDynamicCh1.ForeColor = ServiceUiColors.Disconnected;
        _chkDynamicLog.Font = ServiceUiFonts.Body;
        _chkDynamicLog.ForeColor = ServiceUiColors.TextPrimary;
        _btnDynamicClearLog.Font = ServiceUiFonts.Body;
        _btnDynamicClearLog.BackColor = ServiceUiColors.NeutralAction;
        _btnDynamicClearLog.ForeColor = ServiceUiColors.TextPrimary;
        _lstDynamicLog.Font = ServiceUiFonts.MonoSmall;
        _lstDynamicLog.ItemHeight = ServiceUiFonts.MonoSmall.Height;
        _lstDynamicLog.BackColor = ServiceUiColors.LogBackground;
        _lstDynamicLog.ForeColor = ServiceUiColors.LogText;
    }

    private void RefreshDynamicPorts()
    {
        if (_dynamicServiceSim is null || _dynamicCalibSim is null) return;
        var ports = SerialPort.GetPortNames();
        FillDynamicPortCombo(_cmbDynamicPort, ports, _dynamicServiceSim.PortName);
        FillDynamicPortCombo(_cmbDynamicCalibPort, ports, _dynamicCalibSim.PortName);
        if (_btnDynamicConn is not null)
            _btnDynamicConn.Enabled = ports.Length > 0;
        if (_btnDynamicCalibConn is not null)
            _btnDynamicCalibConn.Enabled = ports.Length > 0;
    }

    private static void FillDynamicPortCombo(ComboBox? combo, string[] ports, string fallbackPort)
    {
        if (combo is null) return;
        string? selected = combo.SelectedItem as string;
        combo.Items.Clear();
        if (ports.Length == 0) return;

        combo.Items.AddRange(ports);
        int idx = Array.IndexOf(ports, selected ?? fallbackPort);
        combo.SelectedIndex = idx >= 0 ? idx : 0;
    }

    private void BtnDynamicConn_Click(object? sender, EventArgs e)
    {
        ToggleDynamicServiceConnection(_cmbDynamicPort.SelectedItem as string, ex => AppendDynamicLog($"ОШИБКА: {ex.Message}", ServiceUiColors.Error));
    }

    private void BtnDynamicPortRefresh_Click(object? sender, EventArgs e)
    {
        RefreshDynamicPorts();
    }

    private void BtnDynamicClearLog_Click(object? sender, EventArgs e)
    {
        lock (_dynamicServiceLogSync)
        {
            _dynamicServiceLogQueue.Clear();
        }
        _lstDynamicLog.Items.Clear();
    }

    private void BtnDynamicCalibConn_Click(object? sender, EventArgs e)
    {
        ToggleDynamicCalibConnection(_cmbDynamicCalibPort.SelectedItem as string,
            ex => MessageBox.Show($"Не удалось подключить АЦП динамики.\n{ex.Message}", "АЦП динамики", MessageBoxButtons.OK, MessageBoxIcon.Error));
    }

    private void ToggleDynamicServiceConnection(string? selectedPort, Action<Exception> onError)
    {
        if (_dynamicServiceSim.IsPortOpen)
        {
            CloseDynamicServiceConnection();
            return;
        }

        if (string.IsNullOrWhiteSpace(selectedPort)) return;
        try
        {
            CloseStaticConnections();
            CloseDynamicCalibConnection();
            _dynamicServiceSim.Open(selectedPort);
            AuditLogger.Action(AuditLogger.AdcConnected, "AdcConnection", selectedPort, "SimA04DynamicService", selectedPort);
            UpdateDynamicServiceMonitorConn(_dynamicServiceSim.IsConnected);
        }
        catch (Exception ex)
        {
            onError(ex);
            AuditLogger.Error(AuditLogger.ErrorAdc, "AdcConnection", selectedPort, "SimA04DynamicService", selectedPort);
        }
    }

    private void ToggleDynamicCalibConnection(string? selectedPort, Action<Exception> onError)
    {
        if (_dynamicCalibSim.IsPortOpen)
        {
            CloseDynamicCalibConnection();
            return;
        }

        if (string.IsNullOrWhiteSpace(selectedPort)) return;
        try
        {
            CloseStaticConnections();
            CloseDynamicServiceConnection();
            _dynamicCalibSim.Open(selectedPort);
            AuditLogger.Action(AuditLogger.AdcConnected, "AdcConnection", selectedPort, "SimA04DynamicCalib", selectedPort);
            UpdateDynamicCalibMonitorConn(_dynamicCalibSim.IsConnected);
        }
        catch (Exception ex)
        {
            onError(ex);
            AuditLogger.Error(AuditLogger.ErrorAdc, "AdcConnection", selectedPort, "SimA04DynamicCalib", selectedPort);
        }
    }

    private void CloseStaticConnections()
    {
        CloseStaticServiceConnection();
        CloseStaticCalibConnection();
    }

    private void CloseStaticServiceConnection()
    {
        if (!_staticServiceSim.IsPortOpen) return;

        var port = _staticServiceSim.PortName;
        _staticServiceSim.Close();
        AuditLogger.Action(AuditLogger.AdcDisconnected, "AdcConnection", port, "SimA04StaticService", port);
        UpdateStaticServiceMonitorConn(false);
    }

    private void CloseStaticCalibConnection()
    {
        if (!_staticCalibSim.IsPortOpen) return;

        var port = _staticCalibSim.PortName;
        _staticCalibSim.Close();
        AuditLogger.Action(AuditLogger.AdcDisconnected, "AdcConnection", port, "SimA04StaticCalib", port);
        UpdateStaticCalibMonitorConn(false);
    }

    private void CloseDynamicConnections()
    {
        CloseDynamicServiceConnection();
        CloseDynamicCalibConnection();
    }

    private void CloseDynamicServiceConnection()
    {
        if (!_dynamicServiceSim.IsPortOpen) return;

        var port = _dynamicServiceSim.PortName;
        _dynamicServiceSim.Close();
        AuditLogger.Action(AuditLogger.AdcDisconnected, "AdcConnection", port, "SimA04DynamicService", port);
        UpdateDynamicServiceMonitorConn(false);
    }

    private void CloseDynamicCalibConnection()
    {
        if (!_dynamicCalibSim.IsPortOpen) return;

        var port = _dynamicCalibSim.PortName;
        _dynamicCalibSim.Close();
        AuditLogger.Action(AuditLogger.AdcDisconnected, "AdcConnection", port, "SimA04DynamicCalib", port);
        UpdateDynamicCalibMonitorConn(false);
    }

    private void Tabs_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (DesignMode || _staticServiceSim is null || _staticCalibSim is null || _dynamicServiceSim is null || _dynamicCalibSim is null) return;
        UpdateDynamicDataSubscriptions();
        var tab = _tabs.SelectedTab;
        if (tab == _tabMonitor)
        {
            CloseStaticCalibConnection();
            CloseDynamicConnections();
            return;
        }

        if (tab == _tabCalibS)
        {
            CloseStaticServiceConnection();
            CloseDynamicConnections();
            return;
        }

        if (tab == _tabDynamicService)
        {
            CloseStaticConnections();
            CloseDynamicCalibConnection();
            return;
        }

        if (tab == _tabCalibD)
        {
            CloseStaticConnections();
            CloseDynamicServiceConnection();
            return;
        }

        CloseStaticConnections();
        CloseDynamicConnections();
    }

    private void UpdateDynamicDataSubscriptions()
    {
        if (DesignMode || _dynamicServiceSim is null || _dynamicCalibSim is null || _tabs is null) return;
        var tab = _tabs.SelectedTab;
        bool calibTabActive = tab == _tabCalibD;
        Volatile.Write(ref _dynamicCalibTabActive, calibTabActive);
        SetDynamicServiceDataSubscription(tab == _tabDynamicService);
        SetDynamicCalibDataSubscription(calibTabActive);
    }

    private void SetDynamicServiceDataSubscription(bool enabled)
    {
        if (_dynamicServiceDataSubscribed == enabled) return;

        if (enabled)
        {
            _dynamicServiceSim.RawSampleReceived += OnDynamicServiceRawSample;
            _dynamicServiceSim.SampleReceived += OnDynamicServiceSample;
            _dynamicServiceDisplayTimer.Start();
        }
        else
        {
            _dynamicServiceSim.RawSampleReceived -= OnDynamicServiceRawSample;
            _dynamicServiceSim.SampleReceived -= OnDynamicServiceSample;
            _dynamicServiceDisplayTimer.Stop();
            lock (_dynamicServiceLogSync)
            {
                _dynamicServiceLogQueue.Clear();
            }
        }

        _dynamicServiceDataSubscribed = enabled;
    }

    private void SetDynamicCalibDataSubscription(bool enabled)
    {
        if (_dynamicCalibDataSubscribed == enabled) return;

        if (enabled)
        {
            _dynamicCalibSim.SampleReceived += OnDynamicCalibSample;
            _dynamicCalibDisplayTimer.Start();
        }
        else
        {
            _dynamicCalibSim.SampleReceived -= OnDynamicCalibSample;
            _dynamicCalibDisplayTimer.Stop();
        }

        _dynamicCalibDataSubscribed = enabled;
    }

    private void UpdateDynamicServiceMonitorConn(bool connected)
    {
        if (_lblDynamicConn is null) return;
        _dotDynamicConn.BackColor = connected ? ServiceUiColors.Connected : ServiceUiColors.Disconnected;
        _lblDynamicConn.Text = connected ? $"Подключено: {_dynamicServiceSim.PortName}  4800/Even/8/1" : (_dynamicServiceSim.IsPortOpen ? $"Порт открыт: {_dynamicServiceSim.PortName}, нет потока АЦП" : "Нет подключения");
        _lblDynamicConn.ForeColor = connected ? ServiceUiColors.PrimaryAction : ServiceUiColors.Disconnected;
        _btnDynamicConn.Text = _dynamicServiceSim.IsPortOpen ? "Отключить" : "Подключить";
        _btnDynamicConn.BackColor = _dynamicServiceSim.IsPortOpen ? ServiceUiColors.DangerAction : ServiceUiColors.PrimaryAction;
        _cmbDynamicPort.Enabled = !_dynamicServiceSim.IsPortOpen;
        SelectComboValue(_cmbDynamicPort, _dynamicServiceSim.PortName);
        AppendDynamicLog(connected ? $"=== Подключено: {_dynamicServiceSim.PortName}  4800/Even/8/1 ===" : "=== Отключено ===", connected ? ServiceUiColors.PrimaryAction : ServiceUiColors.Disconnected);
    }

    private void UpdateDynamicCalibMonitorConn(bool connected)
    {
        if (_btnDynamicCalibConn is null || _cmbDynamicCalibPort is null) return;
        _btnDynamicCalibConn.Text = _dynamicCalibSim.IsPortOpen ? "Отключить" : "Подключить";
        _btnDynamicCalibConn.BackColor = _dynamicCalibSim.IsPortOpen ? ServiceUiColors.DangerAction : ServiceUiColors.PrimaryAction;
        _cmbDynamicCalibPort.Enabled = !_dynamicCalibSim.IsPortOpen;
        SelectComboValue(_cmbDynamicCalibPort, _dynamicCalibSim.PortName);
        UpdateDynamicCalibrationConnectionLabel();
    }

    private void OnDynamicServiceConnectionChanged(object? sender, bool connected)
    {
        if (InvokeRequired) { BeginInvoke(() => OnDynamicServiceConnectionChanged(sender, connected)); return; }
        UpdateDynamicServiceMonitorConn(connected);
    }

    private void OnDynamicCalibConnectionChanged(object? sender, bool connected)
    {
        if (InvokeRequired) { BeginInvoke(() => OnDynamicCalibConnectionChanged(sender, connected)); return; }
        UpdateDynamicCalibMonitorConn(connected);
    }

    private void OnDynamicServiceSample(object? sender, SimA04DynamicSample sample)
    {
        Interlocked.Increment(ref _dynamicServiceSampleRateCount);
        lock (_dynamicServiceSampleSync)
        {
            _latestDynamicServiceSample = sample;
            _latestDynamicServiceSampleVersion++;
        }

        Interlocked.Increment(ref _dynamicServiceSampleUiPosted);
        WriteServiceDynamicDiagnostics();
    }

    private void RefreshDynamicServiceSampleDisplay()
    {
        if (DesignMode || _dynamicServiceSim is null) return;

        SimA04DynamicSample sample;
        long version;
        lock (_dynamicServiceSampleSync)
        {
            if (_latestDynamicServiceSampleVersion == _displayedDynamicServiceSampleVersion) return;
            sample = _latestDynamicServiceSample;
            version = _latestDynamicServiceSampleVersion;
        }

        _displayedDynamicServiceSampleVersion = version;
        Interlocked.Increment(ref _dynamicServiceSampleUiApplied);
        _lblDynamicCh0.Text = sample.Ch0.ToString();
        _lblDynamicCh1.Text = sample.Ch1.ToString();
        _lblDynamicCh0.ForeColor = ServiceUiColors.Info;
        _lblDynamicCh1.ForeColor = ServiceUiColors.Info;
    }

    private void OnDynamicCalibSample(object? sender, SimA04DynamicSample sample)
    {
        lock (_dynamicSampleSync)
        {
            _latestDynamicSample = sample;
            _latestDynamicSampleVersion++;
        }

        Interlocked.Increment(ref _dynamicCalibSampleUiPosted);
        WriteDynamicCalibDiagnostics();
    }

    private void RefreshDynamicSampleDisplay()
    {
        if (DesignMode || _dynamicCalibSim is null) return;

        var sw = System.Diagnostics.Stopwatch.StartNew();
        Interlocked.Increment(ref _dynamicCalibDisplayTicks);
        try
        {
            SimA04DynamicSample sample;
            long version;
            lock (_dynamicSampleSync)
            {
                if (_latestDynamicSampleVersion == _displayedDynamicSampleVersion)
                {
                    Interlocked.Increment(ref _dynamicCalibDisplaySkipped);
                    return;
                }
                sample = _latestDynamicSample;
                version = _latestDynamicSampleVersion;
            }

            _displayedDynamicSampleVersion = version;
            Interlocked.Increment(ref _dynamicCalibSampleUiApplied);
            Interlocked.Increment(ref _dynamicCalibDisplayApplied);
            _lastDynCh0 = sample.Ch0;
            _lastDynCh1 = sample.Ch1;
            UpdateLiveDynamicCalibrationLabels();
        }
        finally
        {
            sw.Stop();
            UpdateMax(ref _dynamicCalibDisplayMaxMs, sw.ElapsedMilliseconds);
            WriteDynamicCalibDiagnostics();
        }
    }

    private void OnDynamicServiceRawSample(object? sender, byte[] raw)
    {
        Interlocked.Increment(ref _dynamicServiceRawUiPosted);
        if (!_chkDynamicLog.Checked) return;

        var sample = SimA04DynamicSample.Parse(raw);
        string text = FormatDynamicServiceLogLine(raw, sample);
        var color = sample.Valid ? ServiceUiColors.LogText : ServiceUiColors.Warning;

        lock (_dynamicServiceLogSync)
        {
            _dynamicServiceLogQueue.Enqueue((text, color));
            while (_dynamicServiceLogQueue.Count > DynamicServiceLogQueueLimit)
            {
                _dynamicServiceLogQueue.Dequeue();
                Interlocked.Increment(ref _dynamicServiceLogDropped);
            }
        }
    }

    // Без LINQ/string.Join/интерполяции с выравниванием — они на каждый байт/число аллоцируют
    // отдельную строку. Здесь только числа дописываются в StringBuilder напрямую (Append(int)
    // не аллоцирует), а итоговая строка собирается один раз через ToString().
    private string FormatDynamicServiceLogLine(byte[] raw, SimA04DynamicSample sample)
    {
        var sb = _dynamicServiceLogBuilder;
        sb.Clear();
        sb.Append(DateTime.Now.ToString("HH:mm:ss.fff"));
        sb.Append("  [");
        for (int i = 0; i < raw.Length; i++)
        {
            if (i > 0) sb.Append("  ");
            AppendPadded(sb, raw[i], 3);
        }
        sb.Append(']');

        if (sample.Valid)
        {
            sb.Append("  CH0=");
            AppendPadded(sb, sample.Ch0, 5);
            sb.Append("  CH1=");
            AppendPadded(sb, sample.Ch1, 5);
            sb.Append(" AUX=");
            AppendPadded(sb, sample.Aux, 3);
        }
        else
        {
            sb.Append("  INVALID (").Append(raw.Length).Append(" байт)");
        }

        return sb.ToString();
    }

    // Дописывает число с ведущими пробелами до заданной ширины, без промежуточных строк.
    private static void AppendPadded(StringBuilder sb, int value, int width)
    {
        int digits = value switch
        {
            < 10 => 1,
            < 100 => 2,
            < 1000 => 3,
            < 10000 => 4,
            _ => 5,
        };
        for (int i = digits; i < width; i++) sb.Append(' ');
        sb.Append(value);
    }

    // Строка owner-drawn лога: текст уже отформатирован, цвет задаёт валидность сэмпла.
    private sealed class DynamicServiceLogLine
    {
        public readonly string Text;
        public readonly Color Color;
        public DynamicServiceLogLine(string text, Color color) { Text = text; Color = color; }
        public override string ToString() => Text;
    }

    private void FlushDynamicServiceLogQueue()
    {
        if (_lstDynamicLog is null) return;

        lock (_dynamicServiceLogSync)
        {
            if (_dynamicServiceLogQueue.Count == 0) return;
            _dynamicServiceLogBatch.Clear();
            _dynamicServiceLogBatch.AddRange(_dynamicServiceLogQueue);
            _dynamicServiceLogQueue.Clear();
        }

        var sw = System.Diagnostics.Stopwatch.StartNew();

        _lstDynamicLog.BeginUpdate();
        foreach (var (text, color) in _dynamicServiceLogBatch)
            AddDynamicLogLine(text, color);
        _lstDynamicLog.TopIndex = 0;
        _lstDynamicLog.EndUpdate();

        sw.Stop();
        Interlocked.Add(ref _dynamicServiceRawUiApplied, _dynamicServiceLogBatch.Count);
        Interlocked.Add(ref _dynamicServiceLogAppended, _dynamicServiceLogBatch.Count);
        UpdateMax(ref _dynamicServiceLogMaxMs, sw.ElapsedMilliseconds);
    }

    // Новая строка — сверху (index 0), число строк ограничено; лишние старые снимаются с конца.
    private void AddDynamicLogLine(string text, Color color)
    {
        _lstDynamicLog.Items.Insert(0, new DynamicServiceLogLine(text, color));
        while (_lstDynamicLog.Items.Count > DynamicServiceLogLineLimit)
            _lstDynamicLog.Items.RemoveAt(_lstDynamicLog.Items.Count - 1);
    }

    // Рисуется только для видимых строк — ListBox виртуализирует отрисовку.
    private void LstDynamicLog_DrawItem(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0 || e.Index >= _lstDynamicLog.Items.Count) return;
        e.DrawBackground();
        if (_lstDynamicLog.Items[e.Index] is DynamicServiceLogLine line)
            TextRenderer.DrawText(e.Graphics, line.Text, _lstDynamicLog.Font, e.Bounds, line.Color,
                TextFormatFlags.Left | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);
    }

    private void AppendDynamicLog(string text, Color color)
    {
        if (_lstDynamicLog is null) return;
        AddDynamicLogLine(text, color);
        _lstDynamicLog.TopIndex = 0;
        Interlocked.Increment(ref _dynamicServiceLogAppended);
    }

    private void WriteDynamicCalibDiagnostics()
    {
        const long intervalMs = 5000;
        long now = Environment.TickCount64;
        long last = Interlocked.Read(ref _dynamicCalibDiagLastTick);
        if (last != 0 && now - last < intervalMs) return;
        if (Interlocked.CompareExchange(ref _dynamicCalibDiagLastTick, now, last) != last) return;

        long elapsedMs = last == 0 ? intervalMs : now - last;
        long rawBytes = _dynamicCalibSim.RawBytesReceived;
        long samples = _dynamicCalibSim.SamplesReceived;
        long skipped = _dynamicCalibSim.SkippedBytes;
        long samplePosted = Interlocked.Read(ref _dynamicCalibSampleUiPosted);
        long sampleApplied = Interlocked.Read(ref _dynamicCalibSampleUiApplied);
        long displayTicks = Interlocked.Read(ref _dynamicCalibDisplayTicks);
        long displayApplied = Interlocked.Read(ref _dynamicCalibDisplayApplied);
        long displaySkipped = Interlocked.Read(ref _dynamicCalibDisplaySkipped);
        long logAppended = Interlocked.Read(ref _dynamicCalibLogAppended);
        long displayMaxMs = Interlocked.Exchange(ref _dynamicCalibDisplayMaxMs, 0);
        long latestVersion;
        long displayedVersion;
        lock (_dynamicSampleSync)
        {
            latestVersion = _latestDynamicSampleVersion;
            displayedVersion = _displayedDynamicSampleVersion;
        }

        long rawBytesDelta = Delta(rawBytes, ref _lastCalibDiagRawBytes);
        long samplesDelta = Delta(samples, ref _lastCalibDiagSamples);
        long skippedDelta = Delta(skipped, ref _lastCalibDiagSkippedBytes);
        long samplePostedDelta = Delta(samplePosted, ref _lastCalibDiagSamplePosted);
        long sampleAppliedDelta = Delta(sampleApplied, ref _lastCalibDiagSampleApplied);
        long displayTicksDelta = Delta(displayTicks, ref _lastCalibDiagDisplayTicks);
        long displayAppliedDelta = Delta(displayApplied, ref _lastCalibDiagDisplayApplied);
        long displaySkippedDelta = Delta(displaySkipped, ref _lastCalibDiagDisplaySkipped);
        long logAppendedDelta = Delta(logAppended, ref _lastCalibDiagLogAppended);
        long sampleNotDisplayed = samplePosted - sampleApplied;
        long displayPending = latestVersion - displayedVersion;
        bool activeTab = Volatile.Read(ref _dynamicCalibTabActive);

        AuditLogger.Action(AuditLogger.AdcDynamicDiag, "AdcDynamicCalib",
            $"periodMs={elapsedMs}; activeTab={activeTab}; bytes={rawBytesDelta}; samples={samplesDelta}; skipped={skippedDelta}; " +
            $"samplePosted={samplePostedDelta}; sampleApplied={sampleAppliedDelta}; sampleNotDisplayed={sampleNotDisplayed}; " +
            $"displayTicks={displayTicksDelta}; displayApplied={displayAppliedDelta}; displaySkipped={displaySkippedDelta}; displayPending={displayPending}; " +
            $"latestVersion={latestVersion}; displayedVersion={displayedVersion}; logAppended={logAppendedDelta}; displayMaxMs={displayMaxMs}",
            "SimA04DynamicCalib", _dynamicCalibSim.PortName);
    }

    private void WriteServiceDynamicDiagnostics()
    {
        const long intervalMs = 5000;
        long now = Environment.TickCount64;
        long last = Interlocked.Read(ref _dynamicServiceDiagLastTick);
        if (last != 0 && now - last < intervalMs) return;
        if (Interlocked.CompareExchange(ref _dynamicServiceDiagLastTick, now, last) != last) return;

        long elapsedMs = last == 0 ? intervalMs : now - last;
        long rawBytes = _dynamicServiceSim.RawBytesReceived;
        long samples = _dynamicServiceSim.SamplesReceived;
        long skipped = _dynamicServiceSim.SkippedBytes;
        long samplePosted = Interlocked.Read(ref _dynamicServiceSampleUiPosted);
        long sampleApplied = Interlocked.Read(ref _dynamicServiceSampleUiApplied);
        long rawPosted = Interlocked.Read(ref _dynamicServiceRawUiPosted);
        long rawApplied = Interlocked.Read(ref _dynamicServiceRawUiApplied);
        long logAppended = Interlocked.Read(ref _dynamicServiceLogAppended);
        long logMaxMs = Interlocked.Exchange(ref _dynamicServiceLogMaxMs, 0);
        long logDropped = Interlocked.Read(ref _dynamicServiceLogDropped);

        long rawBytesDelta = Delta(rawBytes, ref _lastDiagRawBytes);
        long samplesDelta = Delta(samples, ref _lastDiagSamples);
        long skippedDelta = Delta(skipped, ref _lastDiagSkippedBytes);
        long samplePostedDelta = Delta(samplePosted, ref _lastDiagSamplePosted);
        long sampleAppliedDelta = Delta(sampleApplied, ref _lastDiagSampleApplied);
        long rawPostedDelta = Delta(rawPosted, ref _lastDiagRawPosted);
        long rawAppliedDelta = Delta(rawApplied, ref _lastDiagRawApplied);
        long logAppendedDelta = Delta(logAppended, ref _lastDiagLogAppended);
        long logDroppedDelta = Delta(logDropped, ref _lastDiagLogDropped);
        long samplePending = samplePosted - sampleApplied;
        long rawPending = rawPosted - rawApplied;

        AuditLogger.Action(AuditLogger.AdcDynamicDiag, "AdcDynamicService",
            $"periodMs={elapsedMs}; bytes={rawBytesDelta}; samples={samplesDelta}; skipped={skippedDelta}; " +
            $"samplePosted={samplePostedDelta}; sampleApplied={sampleAppliedDelta}; samplePending={samplePending}; " +
            $"rawPosted={rawPostedDelta}; rawApplied={rawAppliedDelta}; rawPending={rawPending}; " +
            $"logAppended={logAppendedDelta}; logMaxMs={logMaxMs}; logDropped={logDroppedDelta}",
            "SimA04DynamicService", _dynamicServiceSim.PortName);
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

    private void UpdateStaticServiceMonitorConn(bool connected)
    {
        _dotConn.BackColor = connected ? ServiceUiColors.PrimaryAction : ServiceUiColors.Disconnected;
        _lblConn.Text = connected ? $"Подключено: {_staticServiceSim.PortName}  4800/Even/8/1" : (_staticServiceSim.IsPortOpen ? $"Порт открыт: {_staticServiceSim.PortName}, нет ответа АЦП" : "Нет подключения");
        _lblConn.ForeColor = connected ? ServiceUiColors.PrimaryAction : ServiceUiColors.Disconnected;
        _btnConn.Text = _staticServiceSim.IsPortOpen ? "Отключить" : "Подключить";
        _btnConn.BackColor = _staticServiceSim.IsPortOpen ? ServiceUiColors.DangerAction : ServiceUiColors.PrimaryAction;
        _cmbPort.Enabled = !_staticServiceSim.IsPortOpen;
        SelectComboValue(_cmbPort, _staticServiceSim.PortName);
        AppendLog(connected ? $"=== Подключено: {_staticServiceSim.PortName}  4800/Even/8/1 ===" : "=== Отключено ===",
            connected ? ServiceUiColors.PrimaryAction : ServiceUiColors.Disconnected);
    }

    private void UpdateStaticCalibMonitorConn(bool connected)
    {
        if (_btnStaticCalibConn is null || _cmbStaticCalibPort is null) return;
        _btnStaticCalibConn.Text = _staticCalibSim.IsPortOpen ? "Отключить" : "Подключить";
        _btnStaticCalibConn.BackColor = _staticCalibSim.IsPortOpen ? ServiceUiColors.DangerAction : ServiceUiColors.PrimaryAction;
        _cmbStaticCalibPort.Enabled = !_staticCalibSim.IsPortOpen;
        SelectComboValue(_cmbStaticCalibPort, _staticCalibSim.PortName);
        UpdateStaticCalibConnectionLabel();
    }

    private void UpdateStaticCalibConnectionLabel()
    {
        if (_lblStaticCalibConn is null || _staticCalibSim is null) return;

        if (_staticCalibSim.IsConnected)
        {
            _lblStaticCalibConn.Text = $"Подключено: {_staticCalibSim.PortName}  4800/Even/8/1";
            _lblStaticCalibConn.ForeColor = ServiceUiColors.PrimaryAction;
        }
        else if (_staticCalibSim.IsPortOpen)
        {
            _lblStaticCalibConn.Text = $"Порт открыт: {_staticCalibSim.PortName}, нет ответа АЦП";
            _lblStaticCalibConn.ForeColor = ServiceUiColors.Warning;
        }
        else
        {
            _lblStaticCalibConn.Text = "Нет подключения";
            _lblStaticCalibConn.ForeColor = ServiceUiColors.Disconnected;
        }
    }

    private void OnStaticServiceConnectionChanged(object? sender, bool connected)
    {
        if (InvokeRequired) { BeginInvoke(() => OnStaticServiceConnectionChanged(sender, connected)); return; }
        UpdateStaticServiceMonitorConn(connected);
    }

    private void OnStaticCalibConnectionChanged(object? sender, bool connected)
    {
        if (InvokeRequired) { BeginInvoke(() => OnStaticCalibConnectionChanged(sender, connected)); return; }
        UpdateStaticCalibMonitorConn(connected);
    }

    private void OnStaticServiceRawFrame(object? sender, byte[] raw)
    {
        if (InvokeRequired) { BeginInvoke(() => OnStaticServiceRawFrame(sender, raw)); return; }
        var frame = SimA04Frame.Parse(raw);
        _frameCount++;
        if (frame.Valid)
        {
            _lastCh0 = frame.Ch0;
            _lastCh1 = frame.Ch1;
            _lblCh0.Text = frame.Ch0.ToString();
            _lblCh1.Text = frame.Ch1.ToString();
            _lblCh0.ForeColor = ServiceUiColors.Info;
            _lblCh1.ForeColor = ServiceUiColors.Info;
        }
        else
        {
            _lblCh0.ForeColor = ServiceUiColors.Error;
            _lblCh1.ForeColor = ServiceUiColors.Error;
        }
        if (!_chkLog.Checked) return;
        string bytes = string.Join("  ", raw.Select(b => b.ToString("D3")));
        string time = DateTime.Now.ToString("HH:mm:ss.fff");
        if (frame.Valid)
            AppendLog($"{time}  [{bytes}]  CH0={frame.Ch0,5}  CH1={frame.Ch1,5}", ServiceUiColors.LogText);
        else
            AppendLog($"{time}  [{bytes}]  INVALID ({raw.Length} байт)", ServiceUiColors.Warning);
    }

    private void OnStaticCalibRawFrame(object? sender, byte[] raw)
    {
        if (InvokeRequired) { BeginInvoke(() => OnStaticCalibRawFrame(sender, raw)); return; }
        var frame = SimA04Frame.Parse(raw);
        if (!frame.Valid) return;
        _lastStaticCalibCh0 = frame.Ch0;
        _lastStaticCalibCh1 = frame.Ch1;
        UpdateLiveAdcLabel();
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
            _dgvCalib.Rows[row].Cells[0].Value = p.IsActive ? "Да" : "Нет";
            _dgvCalib.Rows[row].Cells[1].Value = p.AdcCode;
            _dgvCalib.Rows[row].Cells[2].Value = ((double)p.Mass).ToString("G8", CultureInfo.InvariantCulture);
            if (p.AdcCode != 0)
                _dgvCalib.Rows[row].Cells[3].Value = ((double)p.Mass / p.AdcCode * 65535).ToString("F4", CultureInfo.InvariantCulture);
            _dgvCalib.Rows[row].Cells[4].Value = p.CreatedAt == default ? "" : p.CreatedAt.ToLocalTime().ToString("dd.MM.yy HH:mm");
            _dgvCalib.Rows[row].Cells[5].Value = p.DeletedAt?.ToLocalTime().ToString("dd.MM.yy HH:mm") ?? "";
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
        if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
            RefreshCalibK(e.RowIndex);
    }

    private void RefreshCalibK(int rowIndex)
    {
        var row = _dgvCalib.Rows[rowIndex];
        if (int.TryParse(row.Cells[1].Value?.ToString(), out int code) &&
            double.TryParse(row.Cells[2].Value?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out double mass) &&
            code != 0)
            row.Cells[3].Value = (mass / code * 65535).ToString("F4", CultureInfo.InvariantCulture);
        else
            row.Cells[3].Value = "";
    }

    private void SetCalibRowActive(DataGridViewRow row, bool isActive, DateTime? deletedAt = null)
    {
        row.Cells[0].Value = isActive ? "Да" : "Нет";
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
        row.Cells[5].Value = point?.DeletedAt?.ToLocalTime().ToString("dd.MM.yy HH:mm") ?? "";
        ApplyCalibRowStyle(row);
    }

    private static void ApplyCalibRowStyle(DataGridViewRow row)
    {
        bool active = row.Cells[0].Value?.ToString() == "Да";
        if (active)
        {
            row.DefaultCellStyle.BackColor = ServiceUiColors.GridRowBack;
            row.DefaultCellStyle.ForeColor = row.DataGridView?.DefaultCellStyle.ForeColor ?? ServiceUiColors.TextPrimary;
            row.DefaultCellStyle.SelectionBackColor = ServiceUiColors.GridRowBack;
            row.DefaultCellStyle.SelectionForeColor = row.DataGridView?.DefaultCellStyle.SelectionForeColor ?? ServiceUiColors.GridSelectionText;
            row.ReadOnly = false;
            return;
        }

        var deletedBack = Color.FromArgb(255, 228, 232);
        row.DefaultCellStyle.BackColor = deletedBack;
        row.DefaultCellStyle.ForeColor = ServiceUiColors.TextPrimary;
        row.DefaultCellStyle.SelectionBackColor = deletedBack;
        row.DefaultCellStyle.SelectionForeColor = ServiceUiColors.TextPrimary;
        row.ReadOnly = true;
    }

    private List<CalibPoint> ReadGridPoints()
    {
        var result = new List<CalibPoint>();
        int channel = _calibUseCh0 ? 0 : 1;

        foreach (DataGridViewRow row in _dgvCalib.Rows)
        {
            if (!int.TryParse(row.Cells[1].Value?.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int code))
                continue;
            if (!decimal.TryParse(row.Cells[2].Value?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out decimal mass))
                continue;

            bool active = row.Cells[0].Value?.ToString() == "Да";
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
                CreatedAt = existing?.CreatedAt ?? default,
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
        int code = _calibUseCh0 ? _lastStaticCalibCh0 : _lastStaticCalibCh1;
        _lblLiveAdc.Text = code == 0 ? "—" : code.ToString();
        UpdateStaticCalibMassLabel(code);
        UpdateLiveDynamicCalibrationLabels();
    }

    private void UpdateStaticCalibMassLabel(int code)
    {
        if (_lblStaticCalibMass is null) return;
        if (code == 0)
        {
            _lblStaticCalibMass.Text = "—";
            return;
        }

        var channel = _calibUseCh0 ? ActiveChannel.Main : ActiveChannel.Backup;
        double? mass = CalibrationCalculator.Convert(ReadGridPoints(), code, channel);
        _lblStaticCalibMass.Text = mass is null
            ? "нет калибровки"
            : mass.Value.ToString("F5", CultureInfo.InvariantCulture);
    }

    // ── Calibration dynamic ─────────────────────────────────────────────────

    private int CurrentDynamicAdcCode()
    {
        if (_dynamicCalibSim is null) return 0;
        return _dynamicCalibSim.Channel == ActiveChannel.Main ? _lastDynCh0 : _lastDynCh1;
    }

    private void UpdateLiveDynamicCalibrationLabels()
    {
        if (_lblLiveAdcD is null) return;

        int code = CurrentDynamicAdcCode();
        _lblLiveAdcD.Text = code == 0 ? "—" : code.ToString();
        _lblLiveAdcD.ForeColor = code == 0 ? ServiceUiColors.Disconnected : ServiceUiColors.Info;
        UpdateLiveDynamicWeight(code);
    }

    private static string FormatServiceDynamicWeight(double tonnes) => tonnes.ToString("F5", CultureInfo.InvariantCulture);

    private void UpdateLiveDynamicWeight(int code)
    {
        if (_lblLiveWeightD is null) return;
        if (code == 0)
        {
            _lblLiveWeightD.Text = "—";
            _lblLiveWeightD.ForeColor = ServiceUiColors.Disconnected;
            return;
        }

        bool plusOk = double.TryParse(_txtKPlus.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double kp);
        bool minusOk = double.TryParse(_txtKMinus.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double km);

        if (!plusOk && !minusOk)
        {
            _lblLiveWeightD.Text = "—";
            _lblLiveWeightD.ForeColor = ServiceUiColors.Disconnected;
            return;
        }

        string plus = plusOk ? FormatServiceDynamicWeight(code * kp) : "—";
        string minus = minusOk ? FormatServiceDynamicWeight(code * km) : "—";
        _lblLiveWeightD.Text = $"→ {plus} т  ← {minus} т";
        _lblLiveWeightD.ForeColor = ServiceUiColors.Info;
    }

    private void UpdateDynamicCalibrationConnectionLabel()
    {
        if (_lblDynamicCalibConn is null || _dynamicCalibSim is null) return;

        if (_dynamicCalibSim.IsConnected)
        {
            _lblDynamicCalibConn.Text = $"Динамика: {_dynamicCalibSim.PortName}";
            _lblDynamicCalibConn.ForeColor = ServiceUiColors.PrimaryAction;
        }
        else if (_dynamicCalibSim.IsPortOpen)
        {
            _lblDynamicCalibConn.Text = $"Порт открыт: {_dynamicCalibSim.PortName}";
            _lblDynamicCalibConn.ForeColor = ServiceUiColors.Warning;
        }
        else
        {
            _lblDynamicCalibConn.Text = "Динамика: нет подключения";
            _lblDynamicCalibConn.ForeColor = ServiceUiColors.Disconnected;
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
            row.DefaultCellStyle.BackColor = ServiceUiColors.GridRowBack;
            row.DefaultCellStyle.ForeColor = row.DataGridView?.DefaultCellStyle.ForeColor ?? ServiceUiColors.TextPrimary;
            row.DefaultCellStyle.SelectionBackColor = ServiceUiColors.GridRowBack;
            row.DefaultCellStyle.SelectionForeColor = row.DataGridView?.DefaultCellStyle.SelectionForeColor ?? ServiceUiColors.GridSelectionText;
            row.ReadOnly = false;
            return;
        }

        var deletedBack = Color.FromArgb(255, 228, 232);
        row.DefaultCellStyle.BackColor = deletedBack;
        row.DefaultCellStyle.ForeColor = ServiceUiColors.TextPrimary;
        row.DefaultCellStyle.SelectionBackColor = deletedBack;
        row.DefaultCellStyle.SelectionForeColor = ServiceUiColors.TextPrimary;
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
                    row.IsActive ? "Да" : "Нет",
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
            _btnAdmin.BackColor = ServiceUiColors.AdminLocked;
            SetAdminTabs(false);
            AuditLogger.Action(AuditLogger.AdminLogin, "AdminSession", "выход из режима администратора");
        }
        else
        {
            using var dlg = new PasswordDialog(_settings);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            _adminUnlocked = true;
            _btnAdmin.Text = "🔓 Выйти из режима администратора";
            _btnAdmin.BackColor = ServiceUiColors.AdminUnlocked;
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
