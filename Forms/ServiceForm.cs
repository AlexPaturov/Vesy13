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
    private sealed class CalibCounterSuffixLabel : Label
    {
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Enabled)
            {
                base.OnPaint(e);
                return;
            }

            e.Graphics.Clear(BackColor);
            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
        }
    }

    private SimA04ReaderStatic _staticServiceSim = null!;
    private SimA04ReaderStatic _staticCalibSim = null!;
    private SimA04ReaderDynamic _dynamicServiceSim = null!;
    private SimA04ReaderDynamic _directionCorrectionSim = null!;
    private LocalRepository _calib = null!;
    private SettingsService _settings = null!;
    private bool _adminUnlocked;
    private bool _calibUseCh0 = true;
    private int _frameCount;
    private long _dynamicServiceSampleRateCount;
    private readonly object _dynamicSampleSync = new();
    private readonly System.Windows.Forms.Timer _directionCorrectionDisplayTimer = new() { Interval = 100 };
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
    // Лог динамики — owner-drawn ListBox (_lstDynamicLog из Designer): элементы DynamicServiceLogLine,
    // число строк ограничено DynamicServiceLogLineLimit. ListBox рисует только видимые строки и не
    // копит внутреннее состояние (нет RTF/undo/переформатирования) — аллокации не растут со временем.
    private const int DynamicServiceLogLineLimit = 300;
    // Переиспользуемые буферы: формирование строки лога (только из потока reader-а — SerialPort
    // не поднимает DataReceived параллельно для одного порта, блокировка не нужна) и пачка на flush.
    private readonly StringBuilder _dynamicServiceLogBuilder = new(64);
    private readonly List<(string Text, Color Color)> _dynamicServiceLogBatch = new();
    private bool _dynamicServiceDataSubscribed;
    private bool _directionCorrectionDataSubscribed;
    private bool _directionCorrectionTabActive;
    private bool _staticServiceConnectionEstablished;
    private bool _staticCalibConnectionEstablished;
    private bool _dynamicServiceConnectionEstablished;
    private bool _directionCorrectionConnectionEstablished;
    private int _lastCh0;
    private int _lastCh1;
    private int _lastStaticCalibCh0;
    private int _lastStaticCalibCh1;
    private int _lastDynCh0;
    private int _lastDynCh1;


    public ServiceForm()
    {
        InitializeComponent();
        InitializeDirectionCorrectionProfileDisplayTimer();
    }

    public ServiceForm(LocalRepository calib, SettingsService settings)
    {
        ActiveChannel channel = settings.Current.ActiveChannel;
        _staticServiceSim = new SimA04ReaderStatic { Channel = channel };
        _staticCalibSim = new SimA04ReaderStatic { Channel = channel };
        _dynamicServiceSim = new SimA04ReaderDynamic { Channel = channel };
        _directionCorrectionSim = new SimA04ReaderDynamic { Channel = channel };
        _calib = calib;
        _settings = settings;
        InitializeComponent();
        InitializeDirectionCorrectionProfileDisplayTimer();
    }

    private void InitializeDirectionCorrectionProfileDisplayTimer()
    {
        _directionCorrectionDisplayTimer.Tick += (_, _) => RefreshDynamicSampleDisplay();
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
        _lstLog.Font = ServiceUiFonts.MonoSmall;
        _lstLog.ItemHeight = ServiceUiFonts.MonoSmall.Height;
        _lstLog.BackColor = ServiceUiColors.LogBackground;
        _lstLog.ForeColor = ServiceUiColors.LogText;
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
        _btnStaticCalibPortRefresh.Font = ServiceUiFonts.Body;
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
        _chbCalibCounter.Font = ServiceUiFonts.Body;
        _chbCalibCounter.UseVisualStyleBackColor = false;
        _lblCalibCounterSuffix.Font = ServiceUiFonts.Body;
        _lblCalibCounterSuffix.BackColor = _chbCalibCounter.BackColor;
        _lblCalibCounterSuffix.ForeColor = _chbCalibCounter.ForeColor;
        _btnAddRow.Font = ServiceUiFonts.Body;
        _btnAddRow.BackColor = ServiceUiColors.NeutralAction;
        _btnAddRow.ForeColor = ServiceUiColors.TextPrimary;
        _btnDelRow.Font = ServiceUiFonts.Body;
        _btnDelRow.BackColor = ServiceUiColors.NeutralAction;
        _btnDelRow.ForeColor = ServiceUiColors.TextPrimary;
        _btnCalibSave.Font = ServiceUiFonts.Body;
        _btnCalibSave.BackColor = ServiceUiColors.PrimaryAction;
        _btnCalibSave.ForeColor = ServiceUiColors.TextOnDark;

        // CalibDynamic tab
        _tabDirectionCorrections.BackColor = ServiceUiColors.Surface;
        _pnlDirectionCorrections.BackColor = ServiceUiColors.Surface;
        _pnlDirectionCorrectionsHead.BackColor = ServiceUiColors.Surface;
        _pnlDirectionCorrectionsBody.BackColor = ServiceUiColors.Surface;
        _pnlDirectionCorrectionsBottom.BackColor = ServiceUiColors.Surface;
        _lblLiveAdcCapD.Font = ServiceUiFonts.Body;
        _lblLiveAdcCapD.ForeColor = ServiceUiColors.TextPrimary;
        _lblLiveAdcD.Font = ServiceUiFonts.MonoLiveAdc;
        _lblLiveAdcD.ForeColor = ServiceUiColors.TextOnDark;
        _lblLiveWeightCapD.Font = ServiceUiFonts.Body;
        _lblLiveWeightCapD.ForeColor = ServiceUiColors.TextPrimary;
        _lblLiveWeightD.Font = ServiceUiFonts.MonoLiveAdc;
        _lblLiveWeightD.ForeColor = ServiceUiColors.TextOnDark;
        _cmbDirectionCorrectionPort.Font = ServiceUiFonts.Medium;
        _cmbDirectionCorrectionPort.BackColor = ServiceUiColors.InputBack;
        _cmbDirectionCorrectionPort.ForeColor = ServiceUiColors.InputFore;
        _btnDirectionCorrectionConn.Font = ServiceUiFonts.Body;
        _btnDirectionCorrectionConn.BackColor = ServiceUiColors.PrimaryAction;
        _btnDirectionCorrectionConn.ForeColor = ServiceUiColors.TextOnDark;
        _btnDirectionCorrectionPortRefresh.Font = ServiceUiFonts.SubHeader;
        _btnDirectionCorrectionPortRefresh.BackColor = ServiceUiColors.NeutralAction;
        _btnDirectionCorrectionPortRefresh.ForeColor = ServiceUiColors.TextPrimary;
        _lblDirectionCorrectionConn.Font = ServiceUiFonts.Body;
        _lblDirectionCorrectionConn.ForeColor = ServiceUiColors.TextPrimary;
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
        _btnDirectionCorrectionProfileSave.Font = ServiceUiFonts.Body;
        _btnDirectionCorrectionProfileSave.BackColor = ServiceUiColors.PrimaryAction;
        _btnDirectionCorrectionProfileSave.ForeColor = ServiceUiColors.TextOnDark;
        _dgvDirectionCorrectionProfiles.Font = ServiceUiFonts.GridBody;
        _dgvDirectionCorrectionProfiles.BackgroundColor = ServiceUiColors.Surface;
        _dgvDirectionCorrectionProfiles.ColumnHeadersDefaultCellStyle.BackColor = ServiceUiColors.GridHeaderBack;
        _dgvDirectionCorrectionProfiles.ColumnHeadersDefaultCellStyle.ForeColor = ServiceUiColors.GridHeaderText;
        _dgvDirectionCorrectionProfiles.ColumnHeadersDefaultCellStyle.Font = ServiceUiFonts.GridHeader;
        _dgvDirectionCorrectionProfiles.ColumnHeadersDefaultCellStyle.SelectionBackColor = ServiceUiColors.GridHeaderBack;
        _dgvDirectionCorrectionProfiles.ColumnHeadersDefaultCellStyle.SelectionForeColor = ServiceUiColors.GridHeaderText;
        _dgvDirectionCorrectionProfiles.DefaultCellStyle.BackColor = ServiceUiColors.GridRowBack;
        _dgvDirectionCorrectionProfiles.DefaultCellStyle.ForeColor = ServiceUiColors.TextPrimary;
        _dgvDirectionCorrectionProfiles.DefaultCellStyle.SelectionBackColor = ServiceUiColors.GridSelectionBack;
        _dgvDirectionCorrectionProfiles.DefaultCellStyle.SelectionForeColor = ServiceUiColors.GridSelectionText;
        _dgvDirectionCorrectionProfiles.GridColor = ServiceUiColors.GridLine;

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

        _lblFilterStaticCap.Font = ServiceUiFonts.MediumBold;
        _lblFilterStaticCap.ForeColor = ServiceUiColors.TextSection;
        _chkStaticClamp.Font = ServiceUiFonts.Medium;
        _chkStaticClamp.ForeColor = ServiceUiColors.TextPrimary;
        _lblStaticClampMinCap.Font = ServiceUiFonts.Medium;
        _lblStaticClampMinCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtStaticClampMin.Font = ServiceUiFonts.Body;
        _txtStaticClampMin.BackColor = ServiceUiColors.InputBack;
        _txtStaticClampMin.ForeColor = ServiceUiColors.InputFore;
        _lblStaticClampMaxCap.Font = ServiceUiFonts.Medium;
        _lblStaticClampMaxCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtStaticClampMax.Font = ServiceUiFonts.Body;
        _txtStaticClampMax.BackColor = ServiceUiColors.InputBack;
        _txtStaticClampMax.ForeColor = ServiceUiColors.InputFore;
        _chkStaticDelta.Font = ServiceUiFonts.Medium;
        _chkStaticDelta.ForeColor = ServiceUiColors.TextPrimary;
        _lblStaticDeltaMaxCap.Font = ServiceUiFonts.Medium;
        _lblStaticDeltaMaxCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtStaticDeltaMax.Font = ServiceUiFonts.Body;
        _txtStaticDeltaMax.BackColor = ServiceUiColors.InputBack;
        _txtStaticDeltaMax.ForeColor = ServiceUiColors.InputFore;
        _chkStaticEma.Font = ServiceUiFonts.Medium;
        _chkStaticEma.ForeColor = ServiceUiColors.TextPrimary;
        _lblStaticEmaAlphaCap.Font = ServiceUiFonts.Medium;
        _lblStaticEmaAlphaCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtStaticEmaAlpha.Font = ServiceUiFonts.Body;
        _txtStaticEmaAlpha.BackColor = ServiceUiColors.InputBack;
        _txtStaticEmaAlpha.ForeColor = ServiceUiColors.InputFore;

        _lblFilterDynamicCap.Font = ServiceUiFonts.MediumBold;
        _lblFilterDynamicCap.ForeColor = ServiceUiColors.TextSection;
        _chkDynamicClamp.Font = ServiceUiFonts.Medium;
        _chkDynamicClamp.ForeColor = ServiceUiColors.TextPrimary;
        _lblDynamicClampMinCap.Font = ServiceUiFonts.Medium;
        _lblDynamicClampMinCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtDynamicClampMin.Font = ServiceUiFonts.Body;
        _txtDynamicClampMin.BackColor = ServiceUiColors.InputBack;
        _txtDynamicClampMin.ForeColor = ServiceUiColors.InputFore;
        _lblDynamicClampMaxCap.Font = ServiceUiFonts.Medium;
        _lblDynamicClampMaxCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtDynamicClampMax.Font = ServiceUiFonts.Body;
        _txtDynamicClampMax.BackColor = ServiceUiColors.InputBack;
        _txtDynamicClampMax.ForeColor = ServiceUiColors.InputFore;
        _chkDynamicDelta.Font = ServiceUiFonts.Medium;
        _chkDynamicDelta.ForeColor = ServiceUiColors.TextPrimary;
        _lblDynamicDeltaMaxCap.Font = ServiceUiFonts.Medium;
        _lblDynamicDeltaMaxCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtDynamicDeltaMax.Font = ServiceUiFonts.Body;
        _txtDynamicDeltaMax.BackColor = ServiceUiColors.InputBack;
        _txtDynamicDeltaMax.ForeColor = ServiceUiColors.InputFore;
        _chkDynamicStuck.Font = ServiceUiFonts.Medium;
        _chkDynamicStuck.ForeColor = ServiceUiColors.TextPrimary;
        _lblDynamicStuckSamplesCap.Font = ServiceUiFonts.Medium;
        _lblDynamicStuckSamplesCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtDynamicStuckSamples.Font = ServiceUiFonts.Body;
        _txtDynamicStuckSamples.BackColor = ServiceUiColors.InputBack;
        _txtDynamicStuckSamples.ForeColor = ServiceUiColors.InputFore;
        _chkDynamicEma.Font = ServiceUiFonts.Medium;
        _chkDynamicEma.ForeColor = ServiceUiColors.TextPrimary;
        _lblDynamicEmaAlphaCap.Font = ServiceUiFonts.Medium;
        _lblDynamicEmaAlphaCap.ForeColor = ServiceUiColors.TextPrimary;
        _txtDynamicEmaAlpha.Font = ServiceUiFonts.Body;
        _txtDynamicEmaAlpha.BackColor = ServiceUiColors.InputBack;
        _txtDynamicEmaAlpha.ForeColor = ServiceUiColors.InputFore;
    }

    // ── Lifecycle ───────────────────────────────────────────────────────────

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ApplyTheme();
        if (DesignMode || _staticServiceSim is null) return;
        AuditLogger.Action(AuditLogger.FormOpened, "Form Open", "ServiceForm");
        _staticServiceSim.ConnectionTimeoutMs = 1000;
        _staticCalibSim.ConnectionTimeoutMs = 1000;
        _dynamicServiceSim.ConnectionTimeoutMs = 5000;
        _directionCorrectionSim.ConnectionTimeoutMs = 5000;
        _tabs.SelectedIndexChanged += Tabs_SelectedIndexChanged;
        _staticServiceSim.RawFrameReceived += OnStaticServiceRawFrame;
        _staticServiceSim.ConnectionChanged += OnStaticServiceConnectionChanged;
        _staticCalibSim.RawFrameReceived += OnStaticCalibRawFrame;
        _staticCalibSim.ConnectionChanged += OnStaticCalibConnectionChanged;
        _dynamicServiceSim.ConnectionChanged += OnDynamicServiceConnectionChanged;
        _directionCorrectionSim.ConnectionChanged += OnDirectionCorrectionProfileConnectionChanged;
        _dgvCalib.CellValueChanged += DgvCalib_CellValueChanged;
        _dgvCalib.CellEndEdit += DgvCalib_CellEndEdit;
        _dgvCalib.CurrentCellDirtyStateChanged += DgvCalib_CurrentCellDirtyStateChanged;
        _chbCalibCounter.CheckedChanged += ChbCalibCounter_CheckedChanged;
        UpdateCalibCounterMode(recalculateNewRows: false);
        _rateTimer.Start();
        _rbMain.Checked = _staticServiceSim.Channel == ActiveChannel.Main;
        _rbBackup.Checked = _staticServiceSim.Channel == ActiveChannel.Backup;
        RefreshPorts();
        RefreshDynamicPorts();
        LoadSettingsUi();
        LoadCalibPoints();
        LoadDirectionCorrectionProfile();
        SetAdminTabs(false);
        UpdateStaticServiceMonitorConn(_staticServiceSim.IsConnected);
        UpdateStaticCalibMonitorConn(_staticCalibSim.IsConnected);
        UpdateDynamicServiceMonitorConn(_dynamicServiceSim.IsConnected);
        UpdateDirectionCorrectionProfileMonitorConn(_directionCorrectionSim.IsConnected);
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
            _staticServiceSim.Dispose();

            _staticCalibSim.RawFrameReceived -= OnStaticCalibRawFrame;
            _staticCalibSim.ConnectionChanged -= OnStaticCalibConnectionChanged;
            if (_staticCalibSim.IsPortOpen)
                _staticCalibSim.Close();
            _staticCalibSim.Dispose();

            SetDynamicServiceDataSubscription(false);
            SetDirectionCorrectionProfileDataSubscription(false);
            _dynamicServiceSim.ConnectionChanged -= OnDynamicServiceConnectionChanged;
            _directionCorrectionSim.ConnectionChanged -= OnDirectionCorrectionProfileConnectionChanged;
            if (_dynamicServiceSim.IsPortOpen)
                _dynamicServiceSim.Close();
            _dynamicServiceSim.Dispose();
            if (_directionCorrectionSim.IsPortOpen)
                _directionCorrectionSim.Close();
            _directionCorrectionSim.Dispose();

            _rateTimer.Stop();
            _directionCorrectionDisplayTimer.Stop();
            _directionCorrectionDisplayTimer.Dispose();
            _dynamicServiceDisplayTimer.Stop();
            _dynamicServiceDisplayTimer.Dispose();
        }
        if (!DesignMode)
            AuditLogger.Action(AuditLogger.FormClosed, "Form Close", "ServiceForm");
        base.OnFormClosed(e);
    }

    // ── Designer event handlers ─────────────────────────────────────────────

    private void BtnPortRefresh_Click(object? sender, EventArgs e) => RefreshPorts();
    private void BtnClearLog_Click(object? sender, EventArgs e) => _lstLog.Items.Clear();
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
        if (_staticCalibSim is null || !_staticCalibSim.IsConnected || code == 0) return;
        int row = _dgvCalib.Rows.Add();
        _dgvCalib.Rows[row].Cells[1].Value = code;
        SetCalibRowActive(_dgvCalib.Rows[row], true);
        _dgvCalib.CurrentCell = _dgvCalib.Rows[row].Cells[2];
        _dgvCalib.BeginEdit(true);
    }
    private void BtnCapPlus_Click(object? sender, EventArgs e)
    {
        int code = CurrentDynamicAdcCode();
        if (_directionCorrectionSim is null || !_directionCorrectionSim.IsConnected || code == 0) return;
        _txtCodePlus.Text = code.ToString();
    }

    private void BtnCalcPlus_Click(object? sender, EventArgs e) =>
        CalculateDirectionCorrection(_txtCodePlus, _txtMassPlus, _txtKPlus, "→");

    private void BtnCapMinus_Click(object? sender, EventArgs e)
    {
        int code = CurrentDynamicAdcCode();
        if (_directionCorrectionSim is null || !_directionCorrectionSim.IsConnected || code == 0) return;
        _txtCodeMinus.Text = code.ToString();
    }
    private void BtnCalcMinus_Click(object? sender, EventArgs e) =>
        CalculateDirectionCorrection(_txtCodeMinus, _txtMassMinus, _txtKMinus, "←");

    private void CalculateDirectionCorrection(TextBox codeInput, TextBox massInput, TextBox factorOutput, string direction)
    {
        if (!int.TryParse(codeInput.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int code) || code == 0 ||
            !double.TryParse(massInput.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double referenceMass) || referenceMass <= 0)
        {
            MessageBox.Show("Введите ненулевой код АЦП и положительную эталонную массу.", $"Расчёт коэффициента {direction}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var staticResult = CalibrationCalculator.CalculateStatic(_calib.CalibPoints, code, _directionCorrectionSim.Channel);
        if (staticResult is null)
        {
            string channel = _directionCorrectionSim.Channel == ActiveChannel.Main ? "CH0" : "CH1";
            MessageBox.Show($"Для канала {channel} нет активной статической калибровочной точки.\nСначала сохраните статическую калибровку этого канала.",
                $"Расчёт коэффициента {direction}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (staticResult.Tonnes <= 0)
        {
            MessageBox.Show($"Статический вес для кода {code} равен нулю.\nПроверьте статическую калибровочную точку.",
                $"Расчёт коэффициента {direction}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        factorOutput.Text = (referenceMass / staticResult.Tonnes).ToString("G8", CultureInfo.InvariantCulture);
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
            (_directionCorrectionSim is null || _directionCorrectionSim.Channel == channel)) return;

        ActiveChannel old = _staticServiceSim.Channel;
        _staticServiceSim.Channel = channel;
        if (_staticCalibSim is not null)
            _staticCalibSim.Channel = channel;
        if (_dynamicServiceSim is not null)
            _dynamicServiceSim.Channel = channel;
        if (_directionCorrectionSim is not null)
            _directionCorrectionSim.Channel = channel;
        _settings.Current.ActiveChannel = channel;
        _settings.Save();
        UpdateLiveAdcLabel();
        AuditLogger.Action(AuditLogger.AdcChannelChanged, "AdcChannel", $"{old} -> {channel}", Environment.UserDomainName, Environment.UserName);
    }

    // ── Замена «отравленного» (IsPoisoned) reader-а на месте ─────────────────
    // Reader локальный для формы, но переиспользуется в рамках одной сессии
    // (переключения вкладок, повторные подключения) - при замене нужно перенести
    // подписки на события со старого объекта на новый, иначе форма "онемеет".

    private void ReplaceStaticServiceSim()
    {
        _staticServiceSim.RawFrameReceived -= OnStaticServiceRawFrame;
        _staticServiceSim.ConnectionChanged -= OnStaticServiceConnectionChanged;
        _staticServiceSim = new SimA04ReaderStatic { Channel = _staticServiceSim.Channel };
        _staticServiceSim.RawFrameReceived += OnStaticServiceRawFrame;
        _staticServiceSim.ConnectionChanged += OnStaticServiceConnectionChanged;
    }

    private void ReplaceStaticCalibSim()
    {
        _staticCalibSim.RawFrameReceived -= OnStaticCalibRawFrame;
        _staticCalibSim.ConnectionChanged -= OnStaticCalibConnectionChanged;
        _staticCalibSim = new SimA04ReaderStatic { Channel = _staticCalibSim.Channel };
        _staticCalibSim.RawFrameReceived += OnStaticCalibRawFrame;
        _staticCalibSim.ConnectionChanged += OnStaticCalibConnectionChanged;
        UpdateCaptureButton();
    }

    private void ReplaceDynamicServiceSim()
    {
        _dynamicServiceSim.ConnectionChanged -= OnDynamicServiceConnectionChanged;
        bool dataSubscribed = _dynamicServiceDataSubscribed;
        if (dataSubscribed)
        {
            _dynamicServiceSim.RawSampleReceived -= OnDynamicServiceRawSample;
            _dynamicServiceSim.SampleReceived -= OnDynamicServiceSample;
        }
        _dynamicServiceSim = new SimA04ReaderDynamic { Channel = _dynamicServiceSim.Channel };
        _dynamicServiceSim.ConnectionChanged += OnDynamicServiceConnectionChanged;
        if (dataSubscribed)
        {
            _dynamicServiceSim.RawSampleReceived += OnDynamicServiceRawSample;
            _dynamicServiceSim.SampleReceived += OnDynamicServiceSample;
        }
    }

    private void ReplaceDirectionCorrectionProfileSim()
    {
        _directionCorrectionSim.ConnectionChanged -= OnDirectionCorrectionProfileConnectionChanged;
        bool dataSubscribed = _directionCorrectionDataSubscribed;
        if (dataSubscribed)
            _directionCorrectionSim.SampleReceived -= OnDirectionCorrectionProfileSample;
        _directionCorrectionSim = new SimA04ReaderDynamic { Channel = _directionCorrectionSim.Channel };
        _directionCorrectionSim.ConnectionChanged += OnDirectionCorrectionProfileConnectionChanged;
        if (dataSubscribed)
            _directionCorrectionSim.SampleReceived += OnDirectionCorrectionProfileSample;
        UpdateDynamicCaptureButtons();
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

        _chkStaticClamp.Checked = _settings.Current.StaticClampEnabled;
        _txtStaticClampMin.Text = _settings.Current.StaticClampMinCode.ToString(CultureInfo.InvariantCulture);
        _txtStaticClampMax.Text = _settings.Current.StaticClampMaxCode.ToString(CultureInfo.InvariantCulture);
        _chkStaticDelta.Checked = _settings.Current.StaticDeltaEnabled;
        _txtStaticDeltaMax.Text = _settings.Current.StaticDeltaMaxCodes.ToString(CultureInfo.InvariantCulture);
        _chkStaticEma.Checked = _settings.Current.StaticEmaEnabled;
        _txtStaticEmaAlpha.Text = _settings.Current.StaticEmaAlpha.ToString("G", CultureInfo.InvariantCulture);

        _chkDynamicClamp.Checked = _settings.Current.DynamicClampEnabled;
        _txtDynamicClampMin.Text = _settings.Current.DynamicClampMinCode.ToString(CultureInfo.InvariantCulture);
        _txtDynamicClampMax.Text = _settings.Current.DynamicClampMaxCode.ToString(CultureInfo.InvariantCulture);
        _chkDynamicDelta.Checked = _settings.Current.DynamicDeltaEnabled;
        _txtDynamicDeltaMax.Text = _settings.Current.DynamicDeltaMaxCodes.ToString(CultureInfo.InvariantCulture);
        _chkDynamicStuck.Checked = _settings.Current.DynamicStuckEnabled;
        _txtDynamicStuckSamples.Text = _settings.Current.DynamicStuckSamples.ToString(CultureInfo.InvariantCulture);
        _chkDynamicEma.Checked = _settings.Current.DynamicEmaEnabled;
        _txtDynamicEmaAlpha.Text = _settings.Current.DynamicEmaAlpha.ToString("G", CultureInfo.InvariantCulture);
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

        if (!TryReadCode(_txtStaticClampMin, "Мин. код клэмпа статики", out int staticClampMin) ||
            !TryReadCode(_txtStaticClampMax, "Макс. код клэмпа статики", out int staticClampMax) ||
            !TryReadCode(_txtDynamicClampMin, "Мин. код клэмпа динамики", out int dynamicClampMin) ||
            !TryReadCode(_txtDynamicClampMax, "Макс. код клэмпа динамики", out int dynamicClampMax))
            return;

        if (staticClampMax <= staticClampMin)
        {
            MessageBox.Show("Макс. код клэмпа статики должен быть больше мин. кода.", "Настройки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtStaticClampMax.Focus();
            return;
        }

        if (dynamicClampMax <= dynamicClampMin)
        {
            MessageBox.Show("Макс. код клэмпа динамики должен быть больше мин. кода.", "Настройки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtDynamicClampMax.Focus();
            return;
        }

        if (!TryReadInt(_txtStaticDeltaMax, "Макс. скачок статики", 1, 65535, out int staticDeltaMax) ||
            !TryReadInt(_txtDynamicDeltaMax, "Макс. скачок динамики", 1, 65535, out int dynamicDeltaMax) ||
            !TryReadInt(_txtDynamicStuckSamples, "Порог застрявшего датчика", 2, 100000, out int dynamicStuckSamples))
            return;

        if (!TryReadAlpha(_txtStaticEmaAlpha, "Альфа EMA статики", out double staticEmaAlpha) ||
            !TryReadAlpha(_txtDynamicEmaAlpha, "Альфа EMA динамики", out double dynamicEmaAlpha))
            return;

        _settings.Current.MaxCapacityTonnes = npv;
        _settings.Current.WeightDiscretizationTonnes = discretization;
        _settings.Current.OperatorZeroLimitPercent = zeroLimit;

        _settings.Current.StaticClampEnabled = _chkStaticClamp.Checked;
        _settings.Current.StaticClampMinCode = staticClampMin;
        _settings.Current.StaticClampMaxCode = staticClampMax;
        _settings.Current.StaticDeltaEnabled = _chkStaticDelta.Checked;
        _settings.Current.StaticDeltaMaxCodes = staticDeltaMax;
        _settings.Current.StaticEmaEnabled = _chkStaticEma.Checked;
        _settings.Current.StaticEmaAlpha = staticEmaAlpha;

        _settings.Current.DynamicClampEnabled = _chkDynamicClamp.Checked;
        _settings.Current.DynamicClampMinCode = dynamicClampMin;
        _settings.Current.DynamicClampMaxCode = dynamicClampMax;
        _settings.Current.DynamicDeltaEnabled = _chkDynamicDelta.Checked;
        _settings.Current.DynamicDeltaMaxCodes = dynamicDeltaMax;
        _settings.Current.DynamicStuckEnabled = _chkDynamicStuck.Checked;
        _settings.Current.DynamicStuckSamples = dynamicStuckSamples;
        _settings.Current.DynamicEmaEnabled = _chkDynamicEma.Checked;
        _settings.Current.DynamicEmaAlpha = dynamicEmaAlpha;

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

    private static bool TryReadCode(TextBox box, string caption, out int value)
        => TryReadInt(box, caption, 0, 65535, out value);

    private static bool TryReadInt(TextBox box, string caption, int min, int max, out int value)
    {
        if (int.TryParse(box.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out value) && value >= min && value <= max)
            return true;

        MessageBox.Show($"{caption}: введите целое число от {min} до {max}.", "Настройки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        box.Focus();
        return false;
    }

    private static bool TryReadAlpha(TextBox box, string caption, out double value)
    {
        if (double.TryParse(box.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out value) && value is > 0 and <= 1)
            return true;

        MessageBox.Show($"{caption}: введите число больше 0 и не больше 1.", "Настройки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        box.Focus();
        return false;
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
        ToggleStaticServiceConnection(_cmbPort.SelectedItem as string, _ => AppendLog("ОШИБКА: Не удалось подключить АЦП.", ServiceUiColors.Error));
    }

    private void BtnStaticCalibConn_Click(object? sender, EventArgs e)
    {
        ToggleStaticCalibConnection(_cmbStaticCalibPort.SelectedItem as string,
            _ => MessageBox.Show("Не удалось подключить АЦП статики. Обратитесь к администратору.", "АЦП статики", MessageBoxButtons.OK, MessageBoxIcon.Error));
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
            if (_staticServiceSim.IsPoisoned)
                ReplaceStaticServiceSim();
            _staticServiceSim.Open(selectedPort);
            UpdateStaticServiceMonitorConn(_staticServiceSim.IsConnected);
        }
        catch (Exception ex)
        {
            onError(ex);
            AuditLogger.Exception(AuditLogger.ErrorAdc, "AdcConnection", selectedPort, "SimA04StaticService", selectedPort, ex);
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
            if (_staticCalibSim.IsPoisoned)
                ReplaceStaticCalibSim();
            _staticCalibSim.Open(selectedPort);
            UpdateStaticCalibMonitorConn(_staticCalibSim.IsConnected);
        }
        catch (Exception ex)
        {
            onError(ex);
            AuditLogger.Exception(AuditLogger.ErrorAdc, "AdcConnection", selectedPort, "SimA04StaticCalib", selectedPort, ex);
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
        if (_dynamicServiceSim is null || _directionCorrectionSim is null) return;
        var ports = SerialPort.GetPortNames();
        FillDynamicPortCombo(_cmbDynamicPort, ports, _dynamicServiceSim.PortName);
        FillDynamicPortCombo(_cmbDirectionCorrectionPort, ports, _directionCorrectionSim.PortName);
        if (_btnDynamicConn is not null)
            _btnDynamicConn.Enabled = ports.Length > 0;
        if (_btnDirectionCorrectionConn is not null)
            _btnDirectionCorrectionConn.Enabled = ports.Length > 0;
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
        ToggleDynamicServiceConnection(_cmbDynamicPort.SelectedItem as string, _ => AppendDynamicLog("ОШИБКА: Не удалось подключить АЦП.", ServiceUiColors.Error));
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

    private void BtnDirectionCorrectionConn_Click(object? sender, EventArgs e)
    {
        ToggleDirectionCorrectionProfileConnection(_cmbDirectionCorrectionPort.SelectedItem as string,
            _ => MessageBox.Show("Не удалось подключить АЦП динамики. Обратитесь к администратору.", "АЦП динамики", MessageBoxButtons.OK, MessageBoxIcon.Error));
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
            CloseDirectionCorrectionProfileConnection();
            if (_dynamicServiceSim.IsPoisoned)
                ReplaceDynamicServiceSim();
            _dynamicServiceSim.Open(selectedPort);
            UpdateDynamicServiceMonitorConn(_dynamicServiceSim.IsConnected);
        }
        catch (Exception ex)
        {
            onError(ex);
            AuditLogger.Exception(AuditLogger.ErrorAdc, "AdcConnection", selectedPort, "SimA04DynamicService", selectedPort, ex);
        }
    }

    private void ToggleDirectionCorrectionProfileConnection(string? selectedPort, Action<Exception> onError)
    {
        if (_directionCorrectionSim.IsPortOpen)
        {
            CloseDirectionCorrectionProfileConnection();
            return;
        }

        if (string.IsNullOrWhiteSpace(selectedPort)) return;
        try
        {
            CloseStaticConnections();
            CloseDynamicServiceConnection();
            if (_directionCorrectionSim.IsPoisoned)
                ReplaceDirectionCorrectionProfileSim();
            _directionCorrectionSim.Open(selectedPort);
            UpdateDirectionCorrectionProfileMonitorConn(_directionCorrectionSim.IsConnected);
        }
        catch (Exception ex)
        {
            onError(ex);
            AuditLogger.Exception(AuditLogger.ErrorAdc, "AdcConnection", selectedPort, "SimA04DirectionCorrectionProfile", selectedPort, ex);
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
        _staticServiceConnectionEstablished = false;
        _staticServiceSim.Close();
        AuditLogger.Action(AuditLogger.AdcDisconnected, "AdcPort", "port closed", "SimA04StaticService", port);
        UpdateStaticServiceMonitorConn(false);
    }

    private void CloseStaticCalibConnection()
    {
        if (!_staticCalibSim.IsPortOpen) return;

        var port = _staticCalibSim.PortName;
        _staticCalibConnectionEstablished = false;
        _staticCalibSim.Close();
        AuditLogger.Action(AuditLogger.AdcDisconnected, "AdcPort", "port closed", "SimA04StaticCalib", port);
        UpdateStaticCalibMonitorConn(false);
    }

    private void CloseDynamicConnections()
    {
        CloseDynamicServiceConnection();
        CloseDirectionCorrectionProfileConnection();
    }

    private void CloseDynamicServiceConnection()
    {
        if (!_dynamicServiceSim.IsPortOpen) return;

        var port = _dynamicServiceSim.PortName;
        _dynamicServiceConnectionEstablished = false;
        _dynamicServiceSim.Close();
        AuditLogger.Action(AuditLogger.AdcDisconnected, "AdcPort", "port closed", "SimA04DynamicService", port);
        UpdateDynamicServiceMonitorConn(false);
    }

    private void CloseDirectionCorrectionProfileConnection()
    {
        if (!_directionCorrectionSim.IsPortOpen) return;

        var port = _directionCorrectionSim.PortName;
        _directionCorrectionConnectionEstablished = false;
        _directionCorrectionSim.Close();
        AuditLogger.Action(AuditLogger.AdcDisconnected, "AdcPort", "port closed", "SimA04DirectionCorrectionProfile", port);
        UpdateDirectionCorrectionProfileMonitorConn(false);
    }

    private void Tabs_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (DesignMode || _staticServiceSim is null || _staticCalibSim is null || _dynamicServiceSim is null || _directionCorrectionSim is null) return;
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
            CloseDirectionCorrectionProfileConnection();
            return;
        }

        if (tab == _tabDirectionCorrections)
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
        if (DesignMode || _dynamicServiceSim is null || _directionCorrectionSim is null || _tabs is null) return;
        var tab = _tabs.SelectedTab;
        bool calibTabActive = tab == _tabDirectionCorrections;
        Volatile.Write(ref _directionCorrectionTabActive, calibTabActive);
        SetDynamicServiceDataSubscription(tab == _tabDynamicService);
        SetDirectionCorrectionProfileDataSubscription(calibTabActive);
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

    private void SetDirectionCorrectionProfileDataSubscription(bool enabled)
    {
        if (_directionCorrectionDataSubscribed == enabled) return;

        if (enabled)
        {
            _directionCorrectionSim.SampleReceived += OnDirectionCorrectionProfileSample;
            _directionCorrectionDisplayTimer.Start();
        }
        else
        {
            _directionCorrectionSim.SampleReceived -= OnDirectionCorrectionProfileSample;
            _directionCorrectionDisplayTimer.Stop();
        }

        _directionCorrectionDataSubscribed = enabled;
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

    private void UpdateDirectionCorrectionProfileMonitorConn(bool connected)
    {
        if (_btnDirectionCorrectionConn is null || _cmbDirectionCorrectionPort is null) return;
        _btnDirectionCorrectionConn.Text = _directionCorrectionSim.IsPortOpen ? "Отключить" : "Подключить";
        _btnDirectionCorrectionConn.BackColor = _directionCorrectionSim.IsPortOpen ? ServiceUiColors.DangerAction : ServiceUiColors.PrimaryAction;
        _cmbDirectionCorrectionPort.Enabled = !_directionCorrectionSim.IsPortOpen;
        SelectComboValue(_cmbDirectionCorrectionPort, _directionCorrectionSim.PortName);
        UpdateDirectionCorrectionConnectionLabel();
        UpdateDynamicCaptureButtons();
    }

    private void OnDynamicServiceConnectionChanged(object? sender, bool connected)
    {
        if (InvokeRequired) { BeginInvoke(() => OnDynamicServiceConnectionChanged(sender, connected)); return; }
        AuditAdcConnectionTransition(connected, ref _dynamicServiceConnectionEstablished,
            "SimA04DynamicService", _dynamicServiceSim.PortName);
        UpdateDynamicServiceMonitorConn(connected);
    }

    private void OnDirectionCorrectionProfileConnectionChanged(object? sender, bool connected)
    {
        if (InvokeRequired) { BeginInvoke(() => OnDirectionCorrectionProfileConnectionChanged(sender, connected)); return; }
        AuditAdcConnectionTransition(connected, ref _directionCorrectionConnectionEstablished,
            "SimA04DirectionCorrectionProfile", _directionCorrectionSim.PortName);
        if (!connected)
        {
            _lastDynCh0 = 0;
            _lastDynCh1 = 0;
            UpdateLiveDirectionCorrectionLabels();
        }
        UpdateDirectionCorrectionProfileMonitorConn(connected);
    }

    private static void AuditAdcConnectionTransition(bool connected, ref bool connectionEstablished,
        string objectServer, string portName)
    {
        if (connected)
        {
            string state = connectionEstablished ? "connection restored" : "connection established";
            AuditLogger.Action(AuditLogger.AdcConnected, "AdcConnection", state, objectServer, portName);
            connectionEstablished = true;
        }
        else if (connectionEstablished)
        {
            AuditLogger.Error(AuditLogger.AdcDisconnected, "AdcConnection", "connection lost", objectServer, portName);
        }
    }

    private void OnDynamicServiceSample(object? sender, SimA04DynamicSample sample)
    {
        Interlocked.Increment(ref _dynamicServiceSampleRateCount);
        lock (_dynamicServiceSampleSync)
        {
            _latestDynamicServiceSample = sample;
            _latestDynamicServiceSampleVersion++;
        }
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
        _lblDynamicCh0.Text = sample.Ch0.ToString();
        _lblDynamicCh1.Text = sample.Ch1.ToString();
        _lblDynamicCh0.ForeColor = ServiceUiColors.Info;
        _lblDynamicCh1.ForeColor = ServiceUiColors.Info;
    }

    private void OnDirectionCorrectionProfileSample(object? sender, SimA04DynamicSample sample)
    {
        lock (_dynamicSampleSync)
        {
            _latestDynamicSample = sample;
            _latestDynamicSampleVersion++;
        }
    }

    private void RefreshDynamicSampleDisplay()
    {
        if (DesignMode || _directionCorrectionSim is null) return;

        SimA04DynamicSample sample;
        long version;
        lock (_dynamicSampleSync)
        {
            if (_latestDynamicSampleVersion == _displayedDynamicSampleVersion) return;
            sample = _latestDynamicSample;
            version = _latestDynamicSampleVersion;
        }

        _displayedDynamicSampleVersion = version;
        _lastDynCh0 = sample.Ch0;
        _lastDynCh1 = sample.Ch1;
        UpdateLiveDirectionCorrectionLabels();
    }

    private void OnDynamicServiceRawSample(object? sender, byte[] raw)
    {
        if (!_chkDynamicLog.Checked) return;

        var sample = SimA04DynamicSample.Parse(raw);
        string text = FormatDynamicServiceLogLine(raw, sample);
        var color = sample.Valid ? ServiceUiColors.LogText : ServiceUiColors.Warning;

        lock (_dynamicServiceLogSync)
        {
            _dynamicServiceLogQueue.Enqueue((text, color));
            while (_dynamicServiceLogQueue.Count > DynamicServiceLogQueueLimit)
                _dynamicServiceLogQueue.Dequeue();
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

        _lstDynamicLog.BeginUpdate();
        foreach (var (text, color) in _dynamicServiceLogBatch)
            AddDynamicLogLine(text, color);
        _lstDynamicLog.TopIndex = 0;
        _lstDynamicLog.EndUpdate();
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
        UpdateCaptureButton();
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
        AuditAdcConnectionTransition(connected, ref _staticServiceConnectionEstablished,
            "SimA04StaticService", _staticServiceSim.PortName);
        UpdateStaticServiceMonitorConn(connected);
    }

    private void OnStaticCalibConnectionChanged(object? sender, bool connected)
    {
        if (InvokeRequired) { BeginInvoke(() => OnStaticCalibConnectionChanged(sender, connected)); return; }
        AuditAdcConnectionTransition(connected, ref _staticCalibConnectionEstablished,
            "SimA04StaticCalib", _staticCalibSim.PortName);
        if (!connected)
        {
            _lastStaticCalibCh0 = 0;
            _lastStaticCalibCh1 = 0;
            UpdateLiveAdcLabel();
        }
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

    private const int StaticServiceLogLineLimit = 300;

    private sealed class StaticServiceLogLine
    {
        public readonly string Text;
        public readonly Color Color;
        public StaticServiceLogLine(string text, Color color) { Text = text; Color = color; }
        public override string ToString() => Text;
    }

    // Новая строка — сверху (index 0), как на _lstDynamicLog; лишние старые снимаются с конца.
    private void AppendLog(string text, Color color)
    {
        _lstLog.Items.Insert(0, new StaticServiceLogLine(text, color));
        while (_lstLog.Items.Count > StaticServiceLogLineLimit)
            _lstLog.Items.RemoveAt(_lstLog.Items.Count - 1);
        _lstLog.TopIndex = 0;
    }

    // Рисуется только для видимых строк — ListBox виртуализирует отрисовку.
    private void LstLog_DrawItem(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0 || e.Index >= _lstLog.Items.Count) return;
        e.DrawBackground();
        if (_lstLog.Items[e.Index] is StaticServiceLogLine line)
            TextRenderer.DrawText(e.Graphics, line.Text, _lstLog.Font, e.Bounds, line.Color,
                TextFormatFlags.Left | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);
    }

    // ── Calibration static ──────────────────────────────────────────────────

    private async void BtnCalibSave_Click(object? sender, EventArgs e)
    {
        _dgvCalib.EndEdit();
        if (!ValidateCalibGrid("сохранить калибровку")) return;

        var pts = ReadGridPoints();
        if (pts.Count == 0)
        {
            MessageBox.Show("Нет точек для сохранения.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var duplicateMass = pts.Where(p => p.IsActive)
            .GroupBy(p => p.Mass)
            .FirstOrDefault(group => group.Count() > 1);
        if (duplicateMass is not null)
        {
            MessageBox.Show($"Для массы {duplicateMass.Key:G} т уже существует активная калибровочная точка.\nСделайте прежнюю точку неактивной или укажите другую массу.",
                "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var duplicateCode = pts.Where(p => p.IsActive)
            .GroupBy(p => p.AdcCode)
            .FirstOrDefault(group => group.Count() > 1);
        if (duplicateCode is not null)
        {
            var rows = _dgvCalib.Rows.Cast<DataGridViewRow>()
                .Where(row => row.Cells[0].Value?.ToString() == "Да" &&
                    int.TryParse(row.Cells[1].Value?.ToString(), NumberStyles.Integer,
                        CultureInfo.InvariantCulture, out int code) && code == duplicateCode.Key)
                .Select(row => row.Index + 1)
                .ToArray();
            if (rows.Length > 0)
                _dgvCalib.CurrentCell = _dgvCalib.Rows[rows[0] - 1].Cells[1];

            string rowsText = string.Join(" и ", rows);
            MessageBox.Show("В строках " + rowsText + " указан одинаковый код АЦП: " + duplicateCode.Key +
                ".\nДля одного канала активной может быть только одна точка с этим кодом.\nСделайте одну из точек неактивной или укажите другой код АЦП.",
                "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int channel = _calibUseCh0 ? 0 : 1;
        try
        {
            var changedPoints = await _calib.SaveCalibPointsAsync(channel, pts);
            _settings.UpdateCalibrationCache(_calib.CalibPoints, _calib.ActiveDirectionCorrectionProfile);
            _settings.Save();
            MessageBox.Show("Калибровка сохранена.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            foreach (var point in changedPoints)
            {
                string operation = point.IsActive ? "added" : "retired";
                AuditLogger.Action(AuditLogger.CalibrationSaved, "calibration_points",
                    $"operation={operation}; id={point.Id}; channel=CH{point.Channel}; adc_code={point.AdcCode}; " +
                    $"mass={point.Mass.ToString("G", CultureInfo.InvariantCulture)}; " +
                    $"calibration_value={point.CalibrationValue.ToString("F3", CultureInfo.InvariantCulture)}; is_active={point.IsActive}; " +
                    $"created_at={point.CreatedAt.ToUniversalTime():O}; deleted_at={point.DeletedAt?.ToUniversalTime().ToString("O", CultureInfo.InvariantCulture) ?? "null"}");
            }
            await LoadCalibPointsAsync();
        }
        catch (Exception ex)
        {
            AuditLogger.Exception(AuditLogger.ErrorDb, "calibration_points", "static", "PostgreSQL", ex);
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
            _dgvCalib.Rows[row].Cells[3].Value = p.CalibrationValue.ToString("F3", CultureInfo.InvariantCulture);
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
        if (!_chbCalibCounter.Checked && (e.ColumnIndex == 1 || e.ColumnIndex == 2))
            RefreshNewCalibK();
    }

    private void DgvCalib_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex is not (2 or 3)) return;

        var cell = _dgvCalib.Rows[e.RowIndex].Cells[e.ColumnIndex];
        string text = cell.Value?.ToString()?.Trim() ?? "";
        if (text.Length == 0)
        {
            SetCalibCellError(cell, null);
            return;
        }

        decimal calibrationValue = default;
        decimal mass = default;
        bool valid = e.ColumnIndex == 3
            ? TryParseCalibrationValue(text, out calibrationValue)
            : TryParseCalibDecimal(text, out mass);
        if (!valid)
        {
            string message = e.ColumnIndex == 3
                ? "Введите калибровочное число с точностью до 0,001."
                : "Введите число с одним десятичным разделителем: , или .";
            SetCalibCellError(cell, message);
            if (e.ColumnIndex == 2 && !_chbCalibCounter.Checked)
                _dgvCalib.Rows[e.RowIndex].Cells[3].Value = "";
            UpdateStaticCalibMassLabel(_calibUseCh0 ? _lastStaticCalibCh0 : _lastStaticCalibCh1);
            return;
        }

        SetCalibCellError(cell, null);
        string normalized = e.ColumnIndex == 3
            ? calibrationValue.ToString("F3", CultureInfo.InvariantCulture)
            : mass.ToString("G29", CultureInfo.InvariantCulture);
        if (!string.Equals(text, normalized, StringComparison.Ordinal))
            cell.Value = normalized;
    }

    private void ChbCalibCounter_CheckedChanged(object? sender, EventArgs e) =>
        UpdateCalibCounterMode(recalculateNewRows: true);

    private void RefreshCalibK(int rowIndex)
    {
        var row = _dgvCalib.Rows[rowIndex];
        if (int.TryParse(row.Cells[1].Value?.ToString(), out int code) &&
            TryParseCalibDecimal(row.Cells[2].Value?.ToString(), out decimal mass))
        {
            if (mass == 0)
            {
                row.Cells[3].Value = "0.000";
                return;
            }

            int zeroCode = ReadZeroCalibCode() ?? 0;
            int scaleCode = code - zeroCode;
            if (scaleCode <= 0)
            {
                row.Cells[3].Value = "";
                return;
            }

            decimal calculated = decimal.Round(mass / scaleCode * 65535m, 3, MidpointRounding.AwayFromZero);
            row.Cells[3].Value = Math.Abs(calculated) <= 999999999.999m
                ? calculated.ToString("F3", CultureInfo.InvariantCulture)
                : "";
        }
        else
            row.Cells[3].Value = "";
    }

    private void RefreshNewCalibK()
    {
        foreach (DataGridViewRow row in _dgvCalib.Rows)
        {
            if (row.Tag is not CalibPoint { Id: > 0 })
                RefreshCalibK(row.Index);
        }
    }

    private int? ReadZeroCalibCode()
    {
        foreach (DataGridViewRow row in _dgvCalib.Rows)
        {
            if (row.Cells[0].Value?.ToString() != "Да")
                continue;

            if (int.TryParse(row.Cells[1].Value?.ToString(), out int code) &&
                TryParseCalibDecimal(row.Cells[2].Value?.ToString(), out decimal mass) &&
                mass == 0)
                return code;
        }

        return null;
    }

    private void UpdateCalibCounterMode(bool recalculateNewRows)
    {
        bool manualMode = _chbCalibCounter.Checked;
        _dgvCalib.Columns[3].ReadOnly = !manualMode;

        var backColor = manualMode ? ServiceUiColors.GridAlertRow : ServiceUiColors.Surface;
        _pnlCalibSForm.BackColor = backColor;
        _pnlCalibSFormInner.BackColor = backColor;
        _tlpCalibSForm.BackColor = backColor;
        _chbCalibCounter.BackColor = backColor;
        _chbCalibCounter.ForeColor = manualMode ? ServiceUiColors.Error : ServiceUiColors.TextPrimary;
        _lblCalibCounterSuffix.BackColor = backColor;
        _lblCalibCounterSuffix.ForeColor = manualMode ? ServiceUiColors.Error : ServiceUiColors.TextPrimary;

        if (!manualMode && recalculateNewRows)
            RefreshNewCalibK();

        UpdateStaticCalibMassLabel(_calibUseCh0 ? _lastStaticCalibCh0 : _lastStaticCalibCh1);
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
        bool immutable = row.Tag is CalibPoint { Id: > 0 };
        if (active)
        {
            row.DefaultCellStyle.BackColor = ServiceUiColors.GridRowBack;
            row.DefaultCellStyle.ForeColor = row.DataGridView?.DefaultCellStyle.ForeColor ?? ServiceUiColors.TextPrimary;
            row.DefaultCellStyle.SelectionBackColor = ServiceUiColors.GridSelectionBack;
            row.DefaultCellStyle.SelectionForeColor = ServiceUiColors.GridSelectionText;
            row.ReadOnly = immutable;
            return;
        }

        var deletedBack = Color.FromArgb(255, 228, 232);
        row.DefaultCellStyle.BackColor = deletedBack;
        row.DefaultCellStyle.ForeColor = ServiceUiColors.TextPrimary;
        row.DefaultCellStyle.SelectionBackColor = ServiceUiColors.GridSelectionBack;
        row.DefaultCellStyle.SelectionForeColor = ServiceUiColors.GridSelectionText;
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
            if (!TryParseCalibDecimal(row.Cells[2].Value?.ToString(), out decimal mass))
                continue;
            if (!TryParseCalibrationValue(row.Cells[3].Value?.ToString(), out decimal calibrationValue))
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
                CalibrationValue = calibrationValue,
                IsActive = active,
                CreatedAt = existing?.CreatedAt ?? default,
                DeletedAt = deletedAt,
            });
        }

        return SortCalibPoints(result).ToList();
    }

    private static bool TryParseCalibDecimal(string? text, out decimal value)
    {
        string normalized = text?.Trim() ?? "";
        if (normalized.Count(character => character == '.' || character == ',') > 1)
        {
            value = default;
            return false;
        }

        normalized = normalized.Replace(',', '.');
        return decimal.TryParse(normalized, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
            CultureInfo.InvariantCulture, out value);
    }

    private static bool TryParseCalibrationValue(string? text, out decimal value)
    {
        if (!TryParseCalibDecimal(text, out value))
            return false;

        return decimal.Round(value, 3) == value &&
            value >= -999999999.999m && value <= 999999999.999m;
    }

    private static void SetCalibCellError(DataGridViewCell cell, string? message)
    {
        cell.ErrorText = message ?? "";
        cell.Style.BackColor = message is null ? Color.Empty : ServiceUiColors.GridAlertRow;
    }

    private bool ValidateCalibGrid(string action)
    {
        var invalid = new List<(DataGridViewCell Cell, string Description)>();
        foreach (DataGridViewRow row in _dgvCalib.Rows)
        {
            if (row.Cells[0].Value?.ToString() != "Да") continue;

            string mass = row.Cells[2].Value?.ToString()?.Trim() ?? "";
            if (mass.Length > 0 && !TryParseCalibDecimal(mass, out _))
            {
                SetCalibCellError(row.Cells[2], "Введите число с одним десятичным разделителем: , или .");
                invalid.Add((row.Cells[2], string.Format("Строка {0}, «Масса, т»: «{1}».", row.Index + 1, mass)));
            }

            string calibrationValue = row.Cells[3].Value?.ToString()?.Trim() ?? "";
            if (_chbCalibCounter.Checked && calibrationValue.Length > 0 && !TryParseCalibrationValue(calibrationValue, out _))
            {
                SetCalibCellError(row.Cells[3], "Введите калибровочное число с точностью до 0,001.");
                invalid.Add((row.Cells[3], string.Format("Строка {0}, «Калибр. число»: «{1}».", row.Index + 1, calibrationValue)));
            }
        }

        if (invalid.Count == 0) return true;

        _dgvCalib.CurrentCell = invalid[0].Cell;
        string details = string.Join(Environment.NewLine, invalid.Select(error => error.Description));
        MessageBox.Show(string.Format("Нельзя {0}.\n\n{1}\n\nПроверьте значения, отмеченные в таблице.", action, details),
            "Калибровка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
    }

    private static IEnumerable<CalibPoint> SortCalibPoints(IEnumerable<CalibPoint> points)
    {
        return points
            .OrderBy(p => !p.IsActive)
            .ThenByDescending(p => p.IsActive ? p.Mass : decimal.MinValue)
            .ThenBy(p => p.AdcCode);
    }

    private void UpdateCaptureButton()
    {
        int code = _calibUseCh0 ? _lastStaticCalibCh0 : _lastStaticCalibCh1;
        _btnCapture.Enabled = _staticCalibSim is { IsConnected: true } && code != 0;
    }

    private void UpdateLiveAdcLabel()
    {
        int code = _calibUseCh0 ? _lastStaticCalibCh0 : _lastStaticCalibCh1;
        _lblLiveAdc.Text = code == 0 ? "—" : code.ToString();
        UpdateCaptureButton();
        UpdateStaticCalibMassLabel(code);
        UpdateLiveDirectionCorrectionLabels();
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

    // ── Direction correction profile ─────────────────────────────────────────────────

    private int CurrentDynamicAdcCode()
    {
        if (_directionCorrectionSim is null) return 0;
        return _directionCorrectionSim.Channel == ActiveChannel.Main ? _lastDynCh0 : _lastDynCh1;
    }

    private void UpdateDynamicCaptureButtons()
    {
        bool canCapture = _directionCorrectionSim is { IsConnected: true } && CurrentDynamicAdcCode() != 0;
        _btnCapPlus.Enabled = canCapture;
        _btnCapMinus.Enabled = canCapture;
    }

    private void UpdateLiveDirectionCorrectionLabels()
    {
        if (_lblLiveAdcD is null) return;

        int code = CurrentDynamicAdcCode();
        _lblLiveAdcD.Text = code == 0 ? "—" : code.ToString();
        _lblLiveAdcD.ForeColor = code == 0 ? ServiceUiColors.Disconnected : ServiceUiColors.Info;
        UpdateDynamicCaptureButtons();
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

        var staticResult = CalibrationCalculator.CalculateStatic(_calib.CalibPoints, code, _directionCorrectionSim.Channel);
        if (staticResult is null)
        {
            _lblLiveWeightD.Text = "нет стат. калибровки";
            _lblLiveWeightD.ForeColor = ServiceUiColors.Warning;
            return;
        }

        bool rightOk = double.TryParse(_txtKPlus.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double rightFactor);
        bool leftOk = double.TryParse(_txtKMinus.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double leftFactor);
        if (!rightOk && !leftOk)
        {
            _lblLiveWeightD.Text = "—";
            _lblLiveWeightD.ForeColor = ServiceUiColors.Disconnected;
            return;
        }

        string right = rightOk ? FormatServiceDynamicWeight(staticResult.Tonnes * rightFactor) : "—";
        string left = leftOk ? FormatServiceDynamicWeight(staticResult.Tonnes * leftFactor) : "—";
        _lblLiveWeightD.Text = $"→ {right} т  ← {left} т";
        _lblLiveWeightD.ForeColor = ServiceUiColors.Info;
    }

    private void UpdateDirectionCorrectionConnectionLabel()
    {
        if (_lblDirectionCorrectionConn is null || _directionCorrectionSim is null) return;

        if (_directionCorrectionSim.IsConnected)
        {
            _lblDirectionCorrectionConn.Text = $"Динамика: {_directionCorrectionSim.PortName}";
            _lblDirectionCorrectionConn.ForeColor = ServiceUiColors.PrimaryAction;
        }
        else if (_directionCorrectionSim.IsPortOpen)
        {
            _lblDirectionCorrectionConn.Text = $"Порт открыт: {_directionCorrectionSim.PortName}";
            _lblDirectionCorrectionConn.ForeColor = ServiceUiColors.Warning;
        }
        else
        {
            _lblDirectionCorrectionConn.Text = "Динамика: нет подключения";
            _lblDirectionCorrectionConn.ForeColor = ServiceUiColors.Disconnected;
        }
    }

    private async void BtnDirectionCorrectionProfileSave_Click(object? sender, EventArgs e)
    {
        string plusText = _txtKPlus.Text.Trim();
        string minusText = _txtKMinus.Text.Trim();
        bool hasPlus = plusText.Length > 0;
        bool hasMinus = minusText.Length > 0;

        if (!hasPlus && !hasMinus)
        {
            MessageBox.Show("Введите коэффициент направления → или ← для сохранения.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        double kp = _calib.ActiveDirectionCorrectionProfile.RightDirectionCorrectionFactor;
        double km = _calib.ActiveDirectionCorrectionProfile.LeftDirectionCorrectionFactor;
        if (hasPlus && !double.TryParse(plusText, NumberStyles.Float, CultureInfo.InvariantCulture, out kp))
        {
            MessageBox.Show("Некорректное значение коэффициента направления →.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (hasMinus && !double.TryParse(minusText, NumberStyles.Float, CultureInfo.InvariantCulture, out km))
        {
            MessageBox.Show("Некорректное значение коэффициента направления ←.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var changedProfiles = await _calib.SaveDirectionCorrectionProfileAsync(new DirectionCorrectionProfile { RightDirectionCorrectionFactor = kp, LeftDirectionCorrectionFactor = km });
            _settings.UpdateCalibrationCache(_calib.CalibPoints, _calib.ActiveDirectionCorrectionProfile);
            _settings.Save();
            await LoadDirectionCorrectionProfileAsync();
            MessageBox.Show("Профиль поправочных коэффициентов направления сохранён.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            foreach (var profile in changedProfiles)
            {
                string operation = profile.IsActive ? "added" : "retired";
                AuditLogger.Action(AuditLogger.CalibrationSaved, "direction_correction_profiles",
                    $"operation={operation}; id={profile.Id}; right_direction_correction_factor={profile.RightDirectionCorrectionFactor.ToString("F5", CultureInfo.InvariantCulture)}; " +
                    $"left_direction_correction_factor={profile.LeftDirectionCorrectionFactor.ToString("F5", CultureInfo.InvariantCulture)}; is_active={profile.IsActive}; " +
                    $"created_at={profile.CreatedAt.ToUniversalTime():O}; deleted_at={profile.DeletedAt?.ToUniversalTime().ToString("O", CultureInfo.InvariantCulture) ?? "null"}");
            }
        }
        catch (Exception ex)
        {
            AuditLogger.Exception(AuditLogger.ErrorDb, "DirectionCorrectionProfile", "save", "PostgreSQL", ex);
            MessageBox.Show("Не удалось сохранить профиль поправочных коэффициентов направления.\nОбратитесь к администратору.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void ApplyDirectionCorrectionProfileRowStyle(DataGridViewRow row, bool isActive)
    {
        if (isActive)
        {
            row.DefaultCellStyle.BackColor = ServiceUiColors.GridRowBack;
            row.DefaultCellStyle.ForeColor = row.DataGridView?.DefaultCellStyle.ForeColor ?? ServiceUiColors.TextPrimary;
            row.DefaultCellStyle.SelectionBackColor = ServiceUiColors.GridSelectionBack;
            row.DefaultCellStyle.SelectionForeColor = ServiceUiColors.GridSelectionText;
            row.ReadOnly = false;
            return;
        }

        var deletedBack = Color.FromArgb(255, 228, 232);
        row.DefaultCellStyle.BackColor = deletedBack;
        row.DefaultCellStyle.ForeColor = ServiceUiColors.TextPrimary;
        row.DefaultCellStyle.SelectionBackColor = ServiceUiColors.GridSelectionBack;
        row.DefaultCellStyle.SelectionForeColor = ServiceUiColors.GridSelectionText;
        row.ReadOnly = true;
    }

    private void LoadDirectionCorrectionProfile() => _ = LoadDirectionCorrectionProfileAsync();

    private async Task LoadDirectionCorrectionProfileAsync()
    {
        if (_calib is null) return;

        _txtKPlus.Text = _calib.ActiveDirectionCorrectionProfile.RightDirectionCorrectionFactor.ToString("G8", CultureInfo.InvariantCulture);
        _txtKMinus.Text = _calib.ActiveDirectionCorrectionProfile.LeftDirectionCorrectionFactor.ToString("G8", CultureInfo.InvariantCulture);

        try
        {
            var rows = await _calib.GetDirectionCorrectionProfilesAsync();
            _dgvDirectionCorrectionProfiles.Rows.Clear();
            foreach (var row in rows)
            {
                int idx = _dgvDirectionCorrectionProfiles.Rows.Add(
                    row.IsActive ? "Да" : "Нет",
                    row.RightDirectionCorrectionFactor.ToString("G8", CultureInfo.InvariantCulture),
                    row.LeftDirectionCorrectionFactor.ToString("G8", CultureInfo.InvariantCulture),
                    row.CreatedAt == default ? "" : row.CreatedAt.ToLocalTime().ToString("dd.MM.yy HH:mm"),
                    row.DeletedAt?.ToLocalTime().ToString("dd.MM.yy HH:mm") ?? "");

                ApplyDirectionCorrectionProfileRowStyle(_dgvDirectionCorrectionProfiles.Rows[idx], row.IsActive);
            }
        }
        catch (Exception ex)
        {
            AuditLogger.Exception(AuditLogger.ErrorDb, "DirectionCorrectionProfile", "history", "PostgreSQL", ex);
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
        _tabDirectionCorrections.Enabled = enabled;
        _tabSett.Enabled = enabled;
    }
}
