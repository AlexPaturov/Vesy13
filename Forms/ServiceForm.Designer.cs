namespace Vesy13.Forms;

partial class ServiceForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        _btnAdmin        = new Button();
        _tabs            = new TabControl();
        _tabChannel      = new TabPage();
        _tabMonitor      = new TabPage();
        _tabCalibS       = new TabPage();
        _tabCalibD       = new TabPage();
        _tabSett         = new TabPage();
        _pnlStatus       = new Panel();

        // Channel tab
        _lblChannelTitle = new Label();
        _rbMain          = new RadioButton();
        _rbBackup        = new RadioButton();
        _lblChannelNote  = new Label();

        // Monitor tab
        _cmbPort         = new ComboBox();
        _dotConn         = new Panel();
        _btnConn         = new Button();
        _btnPortRefresh  = new Button();
        _lblConn         = new Label();
        _lblRate         = new Label();
        _pnlCh0          = new Panel();
        _lblCh0Cap       = new Label();
        _lblCh0          = new Label();
        _pnlCh1          = new Panel();
        _lblCh1Cap       = new Label();
        _lblCh1          = new Label();
        _chkLog          = new CheckBox();
        _btnClearLog     = new Button();
        _rtbLog          = new RichTextBox();

        // CalibStatic tab
        _rbCh0Calib      = new RadioButton();
        _rbCh1Calib      = new RadioButton();
        _lblLiveAdcCap   = new Label();
        _lblLiveAdc      = new Label();
        _btnCapture      = new Button();
        _dgvCalib        = new DataGridView();
        _btnAddRow       = new Button();
        _btnDelRow       = new Button();
        _lblKEquals      = new Label();
        _txtK            = new TextBox();
        _lblBEquals      = new Label();
        _txtB            = new TextBox();
        _lblFormula      = new Label();
        _btnLsq          = new Button();
        _btnCalibSave    = new Button();

        // CalibDynamic tab
        _lblLiveAdcCapD   = new Label();
        _lblLiveAdcD      = new Label();
        _lblSecPlus       = new Label();
        _lblKPlusEquals   = new Label();
        _txtKPlus         = new TextBox();
        _lblAutoCalcPlus  = new Label();
        _lblCodePlusCap   = new Label();
        _txtCodePlus      = new TextBox();
        _btnCapPlus       = new Button();
        _lblMassPlusCap   = new Label();
        _txtMassPlus      = new TextBox();
        _btnCalcPlus      = new Button();
        _lblSecMinus      = new Label();
        _lblKMinusEquals  = new Label();
        _txtKMinus        = new TextBox();
        _lblAutoCalcMinus = new Label();
        _lblCodeMinusCap  = new Label();
        _txtCodeMinus     = new TextBox();
        _btnCapMinus      = new Button();
        _lblMassMinusCap  = new Label();
        _txtMassMinus     = new TextBox();
        _btnCalcMinus     = new Button();
        _lblFormulaD      = new Label();
        _btnCalibDynSave  = new Button();

        // Settings tab
        _lblPortCap          = new Label();
        _cmbSettPort         = new ComboBox();
        _lblNpvCap           = new Label();
        _txtNpv              = new TextBox();
        _lblDiscCap          = new Label();
        _cmbDisc             = new ComboBox();
        _lblZeroCap          = new Label();
        _txtZeroLimit        = new TextBox();
        _lblDynWinCap        = new Label();
        _txtDynWindow        = new TextBox();
        _lblBogieTimeoutCap  = new Label();
        _txtBogieTimeout     = new TextBox();
        _lblPasswordCap      = new Label();
        _txtNewPassword      = new TextBox();
        _btnSaveSettings     = new Button();

        _rateTimer = new System.Windows.Forms.Timer(components) { Interval = 1000 };

        _tabs.SuspendLayout();
        _tabChannel.SuspendLayout();
        _tabMonitor.SuspendLayout();
        _tabCalibS.SuspendLayout();
        _tabCalibD.SuspendLayout();
        _tabSett.SuspendLayout();
        _pnlCh0.SuspendLayout();
        _pnlCh1.SuspendLayout();
        _pnlStatus.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvCalib).BeginInit();
        SuspendLayout();

        // ── _btnAdmin ─────────────────────────────────────────────────────────
        _btnAdmin.Text      = "🔒 Войти как администратор";
        _btnAdmin.Anchor    = AnchorStyles.Top | AnchorStyles.Right;
        _btnAdmin.Location  = new Point(440, 8);
        _btnAdmin.Size      = new Size(270, 28);
        _btnAdmin.FlatStyle = FlatStyle.Flat;
        _btnAdmin.Font      = UiFonts.Body;
        _btnAdmin.BackColor = UiColors.AdminLocked;
        _btnAdmin.ForeColor = UiColors.TextOnDark;
        _btnAdmin.UseVisualStyleBackColor = false;
        _btnAdmin.FlatAppearance.BorderSize = 0;
        _btnAdmin.Click    += BtnAdmin_Click;

        // ── Tab: Канал ────────────────────────────────────────────────────────
        _lblChannelTitle.Text      = "Выбор активного канала";
        _lblChannelTitle.Location  = new Point(20, 16);
        _lblChannelTitle.AutoSize  = true;
        _lblChannelTitle.Font      = UiFonts.SubHeaderBold;
        _lblChannelTitle.ForeColor = UiColors.TextPrimary;

        _rbMain.Text      = "Основной  —  CH0";
        _rbMain.Location  = new Point(20, 52);
        _rbMain.AutoSize  = true;
        _rbMain.Checked   = true;
        _rbMain.Font      = UiFonts.NavButton;
        _rbMain.ForeColor = UiColors.TextPrimary;
        _rbMain.CheckedChanged += RbMain_CheckedChanged;

        _rbBackup.Text      = "Резервный  —  CH1";
        _rbBackup.Location  = new Point(20, 90);
        _rbBackup.AutoSize  = true;
        _rbBackup.Font      = UiFonts.NavButton;
        _rbBackup.ForeColor = UiColors.TextPrimary;
        _rbBackup.CheckedChanged += RbBackup_CheckedChanged;

        _lblChannelNote.Text      = "Изменение канала применяется немедленно и не требует пароля.";
        _lblChannelNote.Location  = new Point(20, 136);
        _lblChannelNote.AutoSize  = true;
        _lblChannelNote.Font      = UiFonts.Body;
        _lblChannelNote.ForeColor = UiColors.Disconnected;

        _tabChannel.Text = "Канал";
        _tabChannel.BackColor = UiColors.Surface;
        _tabChannel.Controls.Add(_lblChannelTitle);
        _tabChannel.Controls.Add(_rbMain);
        _tabChannel.Controls.Add(_rbBackup);
        _tabChannel.Controls.Add(_lblChannelNote);

        // ── Tab: Мониторинг ───────────────────────────────────────────────────
        _cmbPort.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbPort.Location      = new Point(10, 12);
        _cmbPort.Width         = 90;
        _cmbPort.Font          = UiFonts.Medium;
        _cmbPort.BackColor     = UiColors.InputBack;
        _cmbPort.ForeColor     = UiColors.InputFore;

        _dotConn.Size      = new Size(10, 10);
        _dotConn.Location  = new Point(108, 16);
        _dotConn.BackColor = UiColors.Disconnected;

        _btnConn.Text      = "Подключить";
        _btnConn.Location  = new Point(124, 8);
        _btnConn.Size      = new Size(110, 28);
        _btnConn.FlatStyle = FlatStyle.Flat;
        _btnConn.Font      = UiFonts.Body;
        _btnConn.BackColor = UiColors.PrimaryAction;
        _btnConn.ForeColor = UiColors.TextOnDark;
        _btnConn.UseVisualStyleBackColor = false;
        _btnConn.FlatAppearance.BorderSize = 0;
        _btnConn.Click    += BtnMonConn_Click;

        _btnPortRefresh.Text      = "↺";
        _btnPortRefresh.Location  = new Point(242, 8);
        _btnPortRefresh.Size      = new Size(30, 28);
        _btnPortRefresh.FlatStyle = FlatStyle.Flat;
        _btnPortRefresh.Font      = UiFonts.SubHeader;
        _btnPortRefresh.BackColor = UiColors.NeutralAction;
        _btnPortRefresh.ForeColor = UiColors.TextPrimary;
        _btnPortRefresh.UseVisualStyleBackColor = false;
        _btnPortRefresh.Click    += BtnPortRefresh_Click;

        _lblConn.Text      = "Нет подключения";
        _lblConn.Location  = new Point(280, 14);
        _lblConn.AutoSize  = true;
        _lblConn.Font      = UiFonts.Body;
        _lblConn.ForeColor = UiColors.Disconnected;

        _lblRate.Text      = "— фр/с";
        _lblRate.Anchor    = AnchorStyles.Top | AnchorStyles.Right;
        _lblRate.Location  = new Point(590, 14);
        _lblRate.AutoSize  = true;
        _lblRate.Font      = UiFonts.Body;
        _lblRate.ForeColor = UiColors.Disconnected;

        _lblCh0Cap.Text      = "CH0 — Основной";
        _lblCh0Cap.Location  = new Point(8, 6);
        _lblCh0Cap.AutoSize  = true;
        _lblCh0Cap.Font      = UiFonts.Body;
        _lblCh0Cap.ForeColor = UiColors.TextOnDarkMuted;

        _lblCh0.Text      = "—";
        _lblCh0.Location  = new Point(8, 28);
        _lblCh0.Size      = new Size(324, 90);
        _lblCh0.Font      = UiFonts.MonitorDisplay;
        _lblCh0.ForeColor = UiColors.Disconnected;
        _lblCh0.TextAlign = ContentAlignment.MiddleRight;
        _lblCh0.AutoSize  = false;

        _pnlCh0.Location  = new Point(10, 48);
        _pnlCh0.Size      = new Size(340, 144);
        _pnlCh0.BackColor = UiColors.MonitorBackground;
        _pnlCh0.Controls.Add(_lblCh0Cap);
        _pnlCh0.Controls.Add(_lblCh0);

        _lblCh1Cap.Text      = "CH1 — Резервный";
        _lblCh1Cap.Location  = new Point(8, 6);
        _lblCh1Cap.AutoSize  = true;
        _lblCh1Cap.Font      = UiFonts.Body;
        _lblCh1Cap.ForeColor = UiColors.TextOnDarkMuted;

        _lblCh1.Text      = "—";
        _lblCh1.Location  = new Point(8, 28);
        _lblCh1.Size      = new Size(324, 90);
        _lblCh1.Font      = UiFonts.MonitorDisplay;
        _lblCh1.ForeColor = UiColors.Disconnected;
        _lblCh1.TextAlign = ContentAlignment.MiddleRight;
        _lblCh1.AutoSize  = false;

        _pnlCh1.Location  = new Point(360, 48);
        _pnlCh1.Size      = new Size(340, 144);
        _pnlCh1.BackColor = UiColors.MonitorBackground;
        _pnlCh1.Controls.Add(_lblCh1Cap);
        _pnlCh1.Controls.Add(_lblCh1);

        _chkLog.Text     = "Лог активен";
        _chkLog.Location = new Point(10, 198);
        _chkLog.AutoSize = true;
        _chkLog.Font     = UiFonts.Body;
        _chkLog.ForeColor = UiColors.TextPrimary;
        _chkLog.Checked  = true;

        _btnClearLog.Text      = "Очистить";
        _btnClearLog.Anchor    = AnchorStyles.Top | AnchorStyles.Right;
        _btnClearLog.Location  = new Point(600, 194);
        _btnClearLog.Size      = new Size(80, 24);
        _btnClearLog.FlatStyle = FlatStyle.Flat;
        _btnClearLog.Font      = UiFonts.Body;
        _btnClearLog.BackColor = UiColors.NeutralAction;
        _btnClearLog.ForeColor = UiColors.TextPrimary;
        _btnClearLog.UseVisualStyleBackColor = false;
        _btnClearLog.Click    += BtnClearLog_Click;

        _rtbLog.Location   = new Point(10, 228);
        _rtbLog.Size       = new Size(680, 200);
        _rtbLog.Anchor     = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _rtbLog.ReadOnly   = true;
        _rtbLog.BackColor  = UiColors.LogBackground;
        _rtbLog.ForeColor  = UiColors.LogText;
        _rtbLog.Font       = UiFonts.MonoSmall;
        _rtbLog.DetectUrls = false;
        _rtbLog.ScrollBars = RichTextBoxScrollBars.Vertical;
        _rtbLog.WordWrap   = false;

        _tabMonitor.Text = "Мониторинг";
        _tabMonitor.BackColor = UiColors.Surface;
        _tabMonitor.Controls.Add(_cmbPort);
        _tabMonitor.Controls.Add(_dotConn);
        _tabMonitor.Controls.Add(_btnConn);
        _tabMonitor.Controls.Add(_btnPortRefresh);
        _tabMonitor.Controls.Add(_lblConn);
        _tabMonitor.Controls.Add(_lblRate);
        _tabMonitor.Controls.Add(_pnlCh0);
        _tabMonitor.Controls.Add(_pnlCh1);
        _tabMonitor.Controls.Add(_chkLog);
        _tabMonitor.Controls.Add(_btnClearLog);
        _tabMonitor.Controls.Add(_rtbLog);

        // ── Tab: Калибровка Статика ────────────────────────────────────────────
        _rbCh0Calib.Text      = "CH0 — Основной";
        _rbCh0Calib.Location  = new Point(20, 16);
        _rbCh0Calib.AutoSize  = true;
        _rbCh0Calib.Checked   = true;
        _rbCh0Calib.Font      = UiFonts.SubHeader;
        _rbCh0Calib.ForeColor = UiColors.TextPrimary;
        _rbCh0Calib.CheckedChanged += RbCh0Calib_CheckedChanged;

        _rbCh1Calib.Text      = "CH1 — Резервный";
        _rbCh1Calib.Location  = new Point(220, 16);
        _rbCh1Calib.AutoSize  = true;
        _rbCh1Calib.Font      = UiFonts.SubHeader;
        _rbCh1Calib.ForeColor = UiColors.TextPrimary;
        _rbCh1Calib.CheckedChanged += RbCh1Calib_CheckedChanged;

        _lblLiveAdcCap.Text      = "Текущий код АЦП:";
        _lblLiveAdcCap.Location  = new Point(20, 54);
        _lblLiveAdcCap.AutoSize  = true;
        _lblLiveAdcCap.Font      = UiFonts.Body;
        _lblLiveAdcCap.ForeColor = UiColors.Disconnected;

        _lblLiveAdc.Text      = "—";
        _lblLiveAdc.Location  = new Point(165, 50);
        _lblLiveAdc.AutoSize  = true;
        _lblLiveAdc.Font      = UiFonts.MonoLiveAdc;
        _lblLiveAdc.ForeColor = UiColors.Info;

        _btnCapture.Text      = "Захватить";
        _btnCapture.Location  = new Point(290, 46);
        _btnCapture.Size      = new Size(110, 28);
        _btnCapture.FlatStyle = FlatStyle.Flat;
        _btnCapture.Font      = UiFonts.Body;
        _btnCapture.BackColor = UiColors.NeutralAction;
        _btnCapture.ForeColor = UiColors.TextPrimary;
        _btnCapture.UseVisualStyleBackColor = false;
        _btnCapture.FlatAppearance.BorderSize = 0;
        _btnCapture.Click    += BtnCapture_Click;

        _dgvCalib.Location                      = new Point(20, 82);
        _dgvCalib.Size                          = new Size(380, 190);
        _dgvCalib.Font                          = UiFonts.GridBody;
        _dgvCalib.AllowUserToAddRows            = false;
        _dgvCalib.AllowUserToDeleteRows         = false;
        _dgvCalib.AllowUserToResizeRows         = false;
        _dgvCalib.RowHeadersVisible             = false;
        _dgvCalib.SelectionMode                 = DataGridViewSelectionMode.FullRowSelect;
        _dgvCalib.EditMode                      = DataGridViewEditMode.EditOnEnter;
        _dgvCalib.BackgroundColor               = UiColors.Surface;
        _dgvCalib.BorderStyle                   = BorderStyle.FixedSingle;
        _dgvCalib.ColumnHeadersDefaultCellStyle.BackColor = UiColors.GridHeaderBack;
        _dgvCalib.ColumnHeadersDefaultCellStyle.ForeColor = UiColors.GridHeaderText;
        _dgvCalib.ColumnHeadersDefaultCellStyle.SelectionBackColor = UiColors.GridHeaderBack;
        _dgvCalib.ColumnHeadersDefaultCellStyle.SelectionForeColor = UiColors.GridHeaderText;
        _dgvCalib.ColumnHeadersHeightSizeMode   = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _dgvCalib.ColumnHeadersHeight           = 34;
        _dgvCalib.DefaultCellStyle.ForeColor    = UiColors.TextPrimary;
        _dgvCalib.DefaultCellStyle.SelectionBackColor = UiColors.GridSelectionBack;
        _dgvCalib.DefaultCellStyle.SelectionForeColor = UiColors.GridSelectionText;
        _dgvCalib.EnableHeadersVisualStyles     = false;
        _dgvCalib.GridColor                     = UiColors.GridLine;
        _dgvCalib.RowTemplate.Height            = 30;
        _dgvCalib.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Код АЦП",   Width = 170, SortMode = DataGridViewColumnSortMode.NotSortable });
        _dgvCalib.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Масса (т)", Width = 170, SortMode = DataGridViewColumnSortMode.NotSortable });

        _btnAddRow.Text      = "Добавить строку";
        _btnAddRow.Location  = new Point(20, 282);
        _btnAddRow.Size      = new Size(160, 28);
        _btnAddRow.FlatStyle = FlatStyle.Flat;
        _btnAddRow.Font      = UiFonts.Body;
        _btnAddRow.BackColor = UiColors.NeutralAction;
        _btnAddRow.ForeColor = UiColors.TextPrimary;
        _btnAddRow.UseVisualStyleBackColor = false;
        _btnAddRow.Click    += BtnAddRow_Click;

        _btnDelRow.Text      = "Удалить выбранную";
        _btnDelRow.Location  = new Point(190, 282);
        _btnDelRow.Size      = new Size(170, 28);
        _btnDelRow.FlatStyle = FlatStyle.Flat;
        _btnDelRow.Font      = UiFonts.Body;
        _btnDelRow.BackColor = UiColors.NeutralAction;
        _btnDelRow.ForeColor = UiColors.TextPrimary;
        _btnDelRow.UseVisualStyleBackColor = false;
        _btnDelRow.Click    += BtnDelRow_Click;

        _lblKEquals.Text      = "k  =";
        _lblKEquals.Location  = new Point(20, 326);
        _lblKEquals.AutoSize  = true;
        _lblKEquals.Font      = UiFonts.Medium;
        _lblKEquals.ForeColor = UiColors.TextPrimary;

        _txtK.Text      = "0";
        _txtK.Location  = new Point(56, 322);
        _txtK.Size      = new Size(150, 26);
        _txtK.Font      = UiFonts.Mono;
        _txtK.BackColor = UiColors.InputBack;
        _txtK.ForeColor = UiColors.InputFore;

        _lblBEquals.Text      = "b  =";
        _lblBEquals.Location  = new Point(224, 326);
        _lblBEquals.AutoSize  = true;
        _lblBEquals.Font      = UiFonts.Medium;
        _lblBEquals.ForeColor = UiColors.TextPrimary;

        _txtB.Text      = "0";
        _txtB.Location  = new Point(260, 322);
        _txtB.Size      = new Size(150, 26);
        _txtB.Font      = UiFonts.Mono;
        _txtB.BackColor = UiColors.InputBack;
        _txtB.ForeColor = UiColors.InputFore;

        _lblFormula.Text      = "Масса = k × Код + b";
        _lblFormula.Location  = new Point(20, 358);
        _lblFormula.AutoSize  = true;
        _lblFormula.Font      = UiFonts.Body;
        _lblFormula.ForeColor = UiColors.Disconnected;

        _btnLsq.Text      = "Рассчитать МНК";
        _btnLsq.Location  = new Point(20, 388);
        _btnLsq.Size      = new Size(180, 34);
        _btnLsq.FlatStyle = FlatStyle.Flat;
        _btnLsq.Font      = UiFonts.Medium;
        _btnLsq.BackColor = UiColors.SecondaryAction;
        _btnLsq.ForeColor = UiColors.TextOnDark;
        _btnLsq.UseVisualStyleBackColor = false;
        _btnLsq.FlatAppearance.BorderSize = 0;
        _btnLsq.Click    += BtnLsq_Click;

        _btnCalibSave.Text      = "Применить и сохранить";
        _btnCalibSave.Location  = new Point(212, 388);
        _btnCalibSave.Size      = new Size(220, 34);
        _btnCalibSave.FlatStyle = FlatStyle.Flat;
        _btnCalibSave.Font      = UiFonts.Medium;
        _btnCalibSave.BackColor = UiColors.PrimaryAction;
        _btnCalibSave.ForeColor = UiColors.TextOnDark;
        _btnCalibSave.UseVisualStyleBackColor = false;
        _btnCalibSave.FlatAppearance.BorderSize = 0;
        _btnCalibSave.Click    += BtnCalibSave_Click;

        _tabCalibS.Text = "Калибровка Статика";
        _tabCalibS.BackColor = UiColors.Surface;
        _tabCalibS.Controls.Add(_rbCh0Calib);
        _tabCalibS.Controls.Add(_rbCh1Calib);
        _tabCalibS.Controls.Add(_lblLiveAdcCap);
        _tabCalibS.Controls.Add(_lblLiveAdc);
        _tabCalibS.Controls.Add(_btnCapture);
        _tabCalibS.Controls.Add(_dgvCalib);
        _tabCalibS.Controls.Add(_btnAddRow);
        _tabCalibS.Controls.Add(_btnDelRow);
        _tabCalibS.Controls.Add(_lblKEquals);
        _tabCalibS.Controls.Add(_txtK);
        _tabCalibS.Controls.Add(_lblBEquals);
        _tabCalibS.Controls.Add(_txtB);
        _tabCalibS.Controls.Add(_lblFormula);
        _tabCalibS.Controls.Add(_btnLsq);
        _tabCalibS.Controls.Add(_btnCalibSave);

        // ── Tab: Калибровка Динамика ──────────────────────────────────────────
        _lblLiveAdcCapD.Text      = "Текущий код АЦП:";
        _lblLiveAdcCapD.Location  = new Point(20, 16);
        _lblLiveAdcCapD.AutoSize  = true;
        _lblLiveAdcCapD.Font      = UiFonts.Body;
        _lblLiveAdcCapD.ForeColor = UiColors.Disconnected;

        _lblLiveAdcD.Text      = "—";
        _lblLiveAdcD.Location  = new Point(165, 12);
        _lblLiveAdcD.AutoSize  = true;
        _lblLiveAdcD.Font      = UiFonts.MonoLiveAdc;
        _lblLiveAdcD.ForeColor = UiColors.Info;

        _lblSecPlus.Text      = "── Направление  →  ──────────────────";
        _lblSecPlus.Location  = new Point(20, 44);
        _lblSecPlus.AutoSize  = true;
        _lblSecPlus.Font      = UiFonts.BodyBold;
        _lblSecPlus.ForeColor = UiColors.TextSection;

        _lblKPlusEquals.Text      = "K→  =";
        _lblKPlusEquals.Location  = new Point(20, 72);
        _lblKPlusEquals.AutoSize  = true;
        _lblKPlusEquals.Font      = UiFonts.Medium;
        _lblKPlusEquals.ForeColor = UiColors.TextPrimary;

        _txtKPlus.Text      = "0";
        _txtKPlus.Location  = new Point(72, 68);
        _txtKPlus.Size      = new Size(160, 26);
        _txtKPlus.Font      = UiFonts.Mono;
        _txtKPlus.BackColor = UiColors.InputBack;
        _txtKPlus.ForeColor = UiColors.InputFore;

        _lblAutoCalcPlus.Text      = "Авторасчёт:";
        _lblAutoCalcPlus.Location  = new Point(20, 104);
        _lblAutoCalcPlus.AutoSize  = true;
        _lblAutoCalcPlus.Font      = UiFonts.Body;
        _lblAutoCalcPlus.ForeColor = UiColors.Disconnected;

        _lblCodePlusCap.Text      = "Код АЦП:";
        _lblCodePlusCap.Location  = new Point(36, 126);
        _lblCodePlusCap.AutoSize  = true;
        _lblCodePlusCap.Font      = UiFonts.Body;
        _lblCodePlusCap.ForeColor = UiColors.TextPrimary;

        _txtCodePlus.Location  = new Point(160, 122);
        _txtCodePlus.Size      = new Size(120, 24);
        _txtCodePlus.Font      = UiFonts.MonoSmall;
        _txtCodePlus.BackColor = UiColors.InputBack;
        _txtCodePlus.ForeColor = UiColors.InputFore;

        _btnCapPlus.Text      = "Захватить";
        _btnCapPlus.Location  = new Point(288, 120);
        _btnCapPlus.Size      = new Size(100, 26);
        _btnCapPlus.FlatStyle = FlatStyle.Flat;
        _btnCapPlus.Font      = UiFonts.Small;
        _btnCapPlus.BackColor = UiColors.HeaderBar;
        _btnCapPlus.ForeColor = UiColors.TextOnDark;
        _btnCapPlus.UseVisualStyleBackColor = false;
        _btnCapPlus.FlatAppearance.BorderSize = 0;
        _btnCapPlus.Click    += BtnCapPlus_Click;

        _lblMassPlusCap.Text      = "Эталон (т):";
        _lblMassPlusCap.Location  = new Point(36, 154);
        _lblMassPlusCap.AutoSize  = true;
        _lblMassPlusCap.Font      = UiFonts.Body;
        _lblMassPlusCap.ForeColor = UiColors.TextPrimary;

        _txtMassPlus.Location  = new Point(160, 150);
        _txtMassPlus.Size      = new Size(120, 24);
        _txtMassPlus.Font      = UiFonts.MonoSmall;
        _txtMassPlus.BackColor = UiColors.InputBack;
        _txtMassPlus.ForeColor = UiColors.InputFore;

        _btnCalcPlus.Text      = "Рассчитать K→";
        _btnCalcPlus.Location  = new Point(36, 182);
        _btnCalcPlus.Size      = new Size(180, 30);
        _btnCalcPlus.FlatStyle = FlatStyle.Flat;
        _btnCalcPlus.Font      = UiFonts.Body;
        _btnCalcPlus.BackColor = UiColors.SecondaryAction;
        _btnCalcPlus.ForeColor = UiColors.TextOnDark;
        _btnCalcPlus.UseVisualStyleBackColor = false;
        _btnCalcPlus.FlatAppearance.BorderSize = 0;
        _btnCalcPlus.Click    += BtnCalcPlus_Click;

        _lblSecMinus.Text      = "── Направление  ←  ──────────────────";
        _lblSecMinus.Location  = new Point(20, 224);
        _lblSecMinus.AutoSize  = true;
        _lblSecMinus.Font      = UiFonts.BodyBold;
        _lblSecMinus.ForeColor = UiColors.TextSection;

        _lblKMinusEquals.Text      = "K←  =";
        _lblKMinusEquals.Location  = new Point(20, 252);
        _lblKMinusEquals.AutoSize  = true;
        _lblKMinusEquals.Font      = UiFonts.Medium;
        _lblKMinusEquals.ForeColor = UiColors.TextPrimary;

        _txtKMinus.Text      = "0";
        _txtKMinus.Location  = new Point(72, 248);
        _txtKMinus.Size      = new Size(160, 26);
        _txtKMinus.Font      = UiFonts.Mono;
        _txtKMinus.BackColor = UiColors.InputBack;
        _txtKMinus.ForeColor = UiColors.InputFore;

        _lblAutoCalcMinus.Text      = "Авторасчёт:";
        _lblAutoCalcMinus.Location  = new Point(20, 284);
        _lblAutoCalcMinus.AutoSize  = true;
        _lblAutoCalcMinus.Font      = UiFonts.Body;
        _lblAutoCalcMinus.ForeColor = UiColors.Disconnected;

        _lblCodeMinusCap.Text      = "Код АЦП:";
        _lblCodeMinusCap.Location  = new Point(36, 306);
        _lblCodeMinusCap.AutoSize  = true;
        _lblCodeMinusCap.Font      = UiFonts.Body;
        _lblCodeMinusCap.ForeColor = UiColors.TextPrimary;

        _txtCodeMinus.Location  = new Point(160, 302);
        _txtCodeMinus.Size      = new Size(120, 24);
        _txtCodeMinus.Font      = UiFonts.MonoSmall;
        _txtCodeMinus.BackColor = UiColors.InputBack;
        _txtCodeMinus.ForeColor = UiColors.InputFore;

        _btnCapMinus.Text      = "Захватить";
        _btnCapMinus.Location  = new Point(288, 300);
        _btnCapMinus.Size      = new Size(100, 26);
        _btnCapMinus.FlatStyle = FlatStyle.Flat;
        _btnCapMinus.Font      = UiFonts.Small;
        _btnCapMinus.BackColor = UiColors.HeaderBar;
        _btnCapMinus.ForeColor = UiColors.TextOnDark;
        _btnCapMinus.UseVisualStyleBackColor = false;
        _btnCapMinus.FlatAppearance.BorderSize = 0;
        _btnCapMinus.Click    += BtnCapMinus_Click;

        _lblMassMinusCap.Text      = "Эталон (т):";
        _lblMassMinusCap.Location  = new Point(36, 334);
        _lblMassMinusCap.AutoSize  = true;
        _lblMassMinusCap.Font      = UiFonts.Body;
        _lblMassMinusCap.ForeColor = UiColors.TextPrimary;

        _txtMassMinus.Location  = new Point(160, 330);
        _txtMassMinus.Size      = new Size(120, 24);
        _txtMassMinus.Font      = UiFonts.MonoSmall;
        _txtMassMinus.BackColor = UiColors.InputBack;
        _txtMassMinus.ForeColor = UiColors.InputFore;

        _btnCalcMinus.Text      = "Рассчитать K←";
        _btnCalcMinus.Location  = new Point(36, 362);
        _btnCalcMinus.Size      = new Size(180, 30);
        _btnCalcMinus.FlatStyle = FlatStyle.Flat;
        _btnCalcMinus.Font      = UiFonts.Body;
        _btnCalcMinus.BackColor = UiColors.SecondaryAction;
        _btnCalcMinus.ForeColor = UiColors.TextOnDark;
        _btnCalcMinus.UseVisualStyleBackColor = false;
        _btnCalcMinus.FlatAppearance.BorderSize = 0;
        _btnCalcMinus.Click    += BtnCalcMinus_Click;

        _lblFormulaD.Text      = "Масса = K × Код";
        _lblFormulaD.Location  = new Point(20, 406);
        _lblFormulaD.AutoSize  = true;
        _lblFormulaD.Font      = UiFonts.Body;
        _lblFormulaD.ForeColor = UiColors.Disconnected;

        _btnCalibDynSave.Text      = "Применить и сохранить";
        _btnCalibDynSave.Location  = new Point(20, 430);
        _btnCalibDynSave.Size      = new Size(220, 34);
        _btnCalibDynSave.FlatStyle = FlatStyle.Flat;
        _btnCalibDynSave.Font      = UiFonts.Medium;
        _btnCalibDynSave.BackColor = UiColors.PrimaryAction;
        _btnCalibDynSave.ForeColor = UiColors.TextOnDark;
        _btnCalibDynSave.UseVisualStyleBackColor = false;
        _btnCalibDynSave.FlatAppearance.BorderSize = 0;
        _btnCalibDynSave.Click    += BtnCalibDynSave_Click;

        _tabCalibD.Text = "Калибровка Динамика";
        _tabCalibD.BackColor = UiColors.Surface;
        _tabCalibD.Controls.Add(_lblLiveAdcCapD);
        _tabCalibD.Controls.Add(_lblLiveAdcD);
        _tabCalibD.Controls.Add(_lblSecPlus);
        _tabCalibD.Controls.Add(_lblKPlusEquals);
        _tabCalibD.Controls.Add(_txtKPlus);
        _tabCalibD.Controls.Add(_lblAutoCalcPlus);
        _tabCalibD.Controls.Add(_lblCodePlusCap);
        _tabCalibD.Controls.Add(_txtCodePlus);
        _tabCalibD.Controls.Add(_btnCapPlus);
        _tabCalibD.Controls.Add(_lblMassPlusCap);
        _tabCalibD.Controls.Add(_txtMassPlus);
        _tabCalibD.Controls.Add(_btnCalcPlus);
        _tabCalibD.Controls.Add(_lblSecMinus);
        _tabCalibD.Controls.Add(_lblKMinusEquals);
        _tabCalibD.Controls.Add(_txtKMinus);
        _tabCalibD.Controls.Add(_lblAutoCalcMinus);
        _tabCalibD.Controls.Add(_lblCodeMinusCap);
        _tabCalibD.Controls.Add(_txtCodeMinus);
        _tabCalibD.Controls.Add(_btnCapMinus);
        _tabCalibD.Controls.Add(_lblMassMinusCap);
        _tabCalibD.Controls.Add(_txtMassMinus);
        _tabCalibD.Controls.Add(_btnCalcMinus);
        _tabCalibD.Controls.Add(_lblFormulaD);
        _tabCalibD.Controls.Add(_btnCalibDynSave);

        // ── Tab: Настройки ────────────────────────────────────────────────────
        _lblPortCap.Text      = "COM-порт:";
        _lblPortCap.Location  = new Point(20, 20);
        _lblPortCap.AutoSize  = true;
        _lblPortCap.Font      = UiFonts.Medium;
        _lblPortCap.ForeColor = UiColors.TextPrimary;

        _cmbSettPort.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbSettPort.Location      = new Point(220, 16);
        _cmbSettPort.Size          = new Size(200, 24);
        _cmbSettPort.BackColor     = UiColors.InputBack;
        _cmbSettPort.ForeColor     = UiColors.InputFore;

        _lblNpvCap.Text      = "НПВ (т):";
        _lblNpvCap.Location  = new Point(20, 56);
        _lblNpvCap.AutoSize  = true;
        _lblNpvCap.Font      = UiFonts.Medium;
        _lblNpvCap.ForeColor = UiColors.TextPrimary;

        _txtNpv.Text      = "150";
        _txtNpv.Location  = new Point(220, 52);
        _txtNpv.Size      = new Size(200, 24);
        _txtNpv.BackColor = UiColors.InputBack;
        _txtNpv.ForeColor = UiColors.InputFore;

        _lblDiscCap.Text      = "Дискретность:";
        _lblDiscCap.Location  = new Point(20, 92);
        _lblDiscCap.AutoSize  = true;
        _lblDiscCap.Font      = UiFonts.Medium;
        _lblDiscCap.ForeColor = UiColors.TextPrimary;

        _cmbDisc.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbDisc.Location      = new Point(220, 88);
        _cmbDisc.Size          = new Size(200, 24);
        _cmbDisc.BackColor     = UiColors.InputBack;
        _cmbDisc.ForeColor     = UiColors.InputFore;
        _cmbDisc.Items.AddRange(new object[] { "0.1 т", "0.05 т", "0.01 т" });
        _cmbDisc.SelectedIndex = 0;

        _lblZeroCap.Text      = "Лимит нуля (% НПВ):";
        _lblZeroCap.Location  = new Point(20, 128);
        _lblZeroCap.AutoSize  = true;
        _lblZeroCap.Font      = UiFonts.Medium;
        _lblZeroCap.ForeColor = UiColors.TextPrimary;

        _txtZeroLimit.Text      = "1";
        _txtZeroLimit.Location  = new Point(220, 124);
        _txtZeroLimit.Size      = new Size(200, 24);
        _txtZeroLimit.BackColor = UiColors.InputBack;
        _txtZeroLimit.ForeColor = UiColors.InputFore;

        _lblDynWinCap.Text      = "Окно Динамики (мс):";
        _lblDynWinCap.Location  = new Point(20, 164);
        _lblDynWinCap.AutoSize  = true;
        _lblDynWinCap.Font      = UiFonts.Medium;
        _lblDynWinCap.ForeColor = UiColors.TextPrimary;

        _txtDynWindow.Text      = "1500";
        _txtDynWindow.Location  = new Point(220, 160);
        _txtDynWindow.Size      = new Size(200, 24);
        _txtDynWindow.BackColor = UiColors.InputBack;
        _txtDynWindow.ForeColor = UiColors.InputFore;

        _lblBogieTimeoutCap.Text      = "Таймаут тележек (мс):";
        _lblBogieTimeoutCap.Location  = new Point(20, 200);
        _lblBogieTimeoutCap.AutoSize  = true;
        _lblBogieTimeoutCap.Font      = UiFonts.Medium;
        _lblBogieTimeoutCap.ForeColor = UiColors.TextPrimary;

        _txtBogieTimeout.Text      = "30000";
        _txtBogieTimeout.Location  = new Point(220, 196);
        _txtBogieTimeout.Size      = new Size(200, 24);
        _txtBogieTimeout.BackColor = UiColors.InputBack;
        _txtBogieTimeout.ForeColor = UiColors.InputFore;

        _lblPasswordCap.Text      = "Новый пароль:";
        _lblPasswordCap.Location  = new Point(20, 236);
        _lblPasswordCap.AutoSize  = true;
        _lblPasswordCap.Font      = UiFonts.Medium;
        _lblPasswordCap.ForeColor = UiColors.TextPrimary;

        _txtNewPassword.Location              = new Point(220, 232);
        _txtNewPassword.Size                  = new Size(200, 24);
        _txtNewPassword.UseSystemPasswordChar = true;
        _txtNewPassword.BackColor             = UiColors.InputBack;
        _txtNewPassword.ForeColor             = UiColors.InputFore;

        _btnSaveSettings.Text      = "Сохранить";
        _btnSaveSettings.Location  = new Point(20, 276);
        _btnSaveSettings.Size      = new Size(130, 34);
        _btnSaveSettings.FlatStyle = FlatStyle.Flat;
        _btnSaveSettings.BackColor = UiColors.HeaderBar;
        _btnSaveSettings.ForeColor = UiColors.TextOnDark;
        _btnSaveSettings.UseVisualStyleBackColor = false;
        _btnSaveSettings.Font      = UiFonts.Medium;
        _btnSaveSettings.Click    += BtnSaveSettings_Click;

        _tabSett.Text = "Настройки";
        _tabSett.BackColor = UiColors.Surface;
        _tabSett.Controls.Add(_lblPortCap);
        _tabSett.Controls.Add(_cmbSettPort);
        _tabSett.Controls.Add(_lblNpvCap);
        _tabSett.Controls.Add(_txtNpv);
        _tabSett.Controls.Add(_lblDiscCap);
        _tabSett.Controls.Add(_cmbDisc);
        _tabSett.Controls.Add(_lblZeroCap);
        _tabSett.Controls.Add(_txtZeroLimit);
        _tabSett.Controls.Add(_lblDynWinCap);
        _tabSett.Controls.Add(_txtDynWindow);
        _tabSett.Controls.Add(_lblBogieTimeoutCap);
        _tabSett.Controls.Add(_txtBogieTimeout);
        _tabSett.Controls.Add(_lblPasswordCap);
        _tabSett.Controls.Add(_txtNewPassword);
        _tabSett.Controls.Add(_btnSaveSettings);

        // ── TabControl ────────────────────────────────────────────────────────
        _tabs.Location = new Point(0, 44);
        _tabs.Anchor   = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _tabs.Size     = new Size(720, 480);
        _tabs.Font     = UiFonts.Medium;
        _tabs.TabPages.Add(_tabChannel);
        _tabs.TabPages.Add(_tabMonitor);
        _tabs.TabPages.Add(_tabCalibS);
        _tabs.TabPages.Add(_tabCalibD);
        _tabs.TabPages.Add(_tabSett);

        // ── Status panel ──────────────────────────────────────────────────────

        _pnlStatus.Dock      = DockStyle.Bottom;
        _pnlStatus.Height    = 4;
        _pnlStatus.BackColor = UiColors.StatusBar;

        // ── Timer ──────────────────────────────────────────────────────────────
        _rateTimer.Tick += RateTimer_Tick;

        // ── Form ──────────────────────────────────────────────────────────────
        Text          = "Сервисный режим";
        ClientSize    = new Size(720, 560);
        MinimumSize   = new Size(640, 480);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor     = UiColors.AppBackground;
        Controls.Add(_btnAdmin);
        Controls.Add(_tabs);
        Controls.Add(_pnlStatus);

        _tabs.ResumeLayout(false);
        _tabChannel.ResumeLayout(false);
        _tabChannel.PerformLayout();
        _tabMonitor.ResumeLayout(false);
        _tabMonitor.PerformLayout();
        _tabCalibS.ResumeLayout(false);
        _tabCalibS.PerformLayout();
        _tabCalibD.ResumeLayout(false);
        _tabCalibD.PerformLayout();
        _tabSett.ResumeLayout(false);
        _tabSett.PerformLayout();
        _pnlCh0.ResumeLayout(false);
        _pnlCh0.PerformLayout();
        _pnlCh1.ResumeLayout(false);
        _pnlCh1.PerformLayout();
        _pnlStatus.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_dgvCalib).EndInit();
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ResumeLayout(false);
    }

    private Button      _btnAdmin;
    private TabControl  _tabs;
    private TabPage     _tabChannel;
    private TabPage     _tabMonitor;
    private TabPage     _tabCalibS;
    private TabPage     _tabCalibD;
    private TabPage     _tabSett;
    private Panel       _pnlStatus;

    private Label       _lblChannelTitle;
    private RadioButton _rbMain;
    private RadioButton _rbBackup;
    private Label       _lblChannelNote;

    private ComboBox    _cmbPort;
    private Panel       _dotConn;
    private Button      _btnConn;
    private Button      _btnPortRefresh;
    private Label       _lblConn;
    private Label       _lblRate;
    private Panel       _pnlCh0;
    private Label       _lblCh0Cap;
    private Label       _lblCh0;
    private Panel       _pnlCh1;
    private Label       _lblCh1Cap;
    private Label       _lblCh1;
    private CheckBox    _chkLog;
    private Button      _btnClearLog;
    private RichTextBox _rtbLog;

    private RadioButton  _rbCh0Calib;
    private RadioButton  _rbCh1Calib;
    private Label        _lblLiveAdcCap;
    private Label        _lblLiveAdc;
    private Button       _btnCapture;
    private DataGridView _dgvCalib;
    private Button       _btnAddRow;
    private Button       _btnDelRow;
    private Label        _lblKEquals;
    private TextBox      _txtK;
    private Label        _lblBEquals;
    private TextBox      _txtB;
    private Label        _lblFormula;
    private Button       _btnLsq;
    private Button       _btnCalibSave;

    private Label   _lblLiveAdcCapD;
    private Label   _lblLiveAdcD;
    private Label   _lblSecPlus;
    private Label   _lblKPlusEquals;
    private TextBox _txtKPlus;
    private Label   _lblAutoCalcPlus;
    private Label   _lblCodePlusCap;
    private TextBox _txtCodePlus;
    private Button  _btnCapPlus;
    private Label   _lblMassPlusCap;
    private TextBox _txtMassPlus;
    private Button  _btnCalcPlus;
    private Label   _lblSecMinus;
    private Label   _lblKMinusEquals;
    private TextBox _txtKMinus;
    private Label   _lblAutoCalcMinus;
    private Label   _lblCodeMinusCap;
    private TextBox _txtCodeMinus;
    private Button  _btnCapMinus;
    private Label   _lblMassMinusCap;
    private TextBox _txtMassMinus;
    private Button  _btnCalcMinus;
    private Label   _lblFormulaD;
    private Button  _btnCalibDynSave;

    private Label    _lblPortCap;
    private ComboBox _cmbSettPort;
    private Label    _lblNpvCap;
    private TextBox  _txtNpv;
    private Label    _lblDiscCap;
    private ComboBox _cmbDisc;
    private Label    _lblZeroCap;
    private TextBox  _txtZeroLimit;
    private Label    _lblDynWinCap;
    private TextBox  _txtDynWindow;
    private Label    _lblBogieTimeoutCap;
    private TextBox  _txtBogieTimeout;
    private Label    _lblPasswordCap;
    private TextBox  _txtNewPassword;
    private Button   _btnSaveSettings;

    private System.Windows.Forms.Timer _rateTimer;
}
