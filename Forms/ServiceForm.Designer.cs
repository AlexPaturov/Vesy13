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
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        _btnAdmin = new Button();
        _tabs = new TabControl();
        _tabChannel = new TabPage();
        _lblChannelTitle = new Label();
        _rbMain = new RadioButton();
        _rbBackup = new RadioButton();
        _lblChannelNote = new Label();
        _tabMonitor = new TabPage();
        _cmbPort = new ComboBox();
        _dotConn = new Panel();
        _btnConn = new Button();
        _btnPortRefresh = new Button();
        _lblConn = new Label();
        _lblRate = new Label();
        _pnlCh0 = new Panel();
        _lblCh0Cap = new Label();
        _lblCh0 = new Label();
        _pnlCh1 = new Panel();
        _lblCh1Cap = new Label();
        _lblCh1 = new Label();
        _chkLog = new CheckBox();
        _btnClearLog = new Button();
        _rtbLog = new RichTextBox();
        _tabDynamicService = new TabPage();
        _cmbDynamicPort = new ComboBox();
        _dotDynamicConn = new Panel();
        _btnDynamicConn = new Button();
        _btnDynamicPortRefresh = new Button();
        _lblDynamicConn = new Label();
        _lblDynamicRate = new Label();
        _pnlDynamicCh0 = new Panel();
        _lblDynamicCh0Cap = new Label();
        _lblDynamicCh0 = new Label();
        _pnlDynamicCh1 = new Panel();
        _lblDynamicCh1Cap = new Label();
        _lblDynamicCh1 = new Label();
        _chkDynamicLog = new CheckBox();
        _btnDynamicClearLog = new Button();
        _rtbDynamicLog = new RichTextBox();
        _tabCalibS = new TabPage();
        _pnlCalibS = new Panel();
        _pnlCalibSBody = new Panel();
        _dgvCalib = new DataGridView();
        dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
        dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
        dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
        _btnAddRow = new Button();
        _btnDelRow = new Button();
        _lblKEquals = new Label();
        _txtK = new TextBox();
        _lblBEquals = new Label();
        _txtB = new TextBox();
        _lblFormula = new Label();
        _btnLsq = new Button();
        _btnCalibSave = new Button();
        _pnlCalibSHead = new Panel();
        _dotStaticCalibConn = new Panel();
        _cmbStaticCalibPort = new ComboBox();
        _btnStaticCalibConn = new Button();
        _btnStaticCalibPortRefresh = new Button();
        _lblStaticCalibConn = new Label();
        tlpCalibSHead = new TableLayoutPanel();
        _rbCh1Calib = new RadioButton();
        _lblLiveAdc = new Label();
        _lblLiveAdcCap = new Label();
        _rbCh0Calib = new RadioButton();
        _btnCapture = new Button();
        _tabCalibD = new TabPage();
        _pnlCalibD = new Panel();
        _pnlCalibDBody = new Panel();
        pnlCalibDMain = new Panel();
        tlpCalibDMain = new TableLayoutPanel();
        tlpDirections = new TableLayoutPanel();
        tlpCalibDPlus = new TableLayoutPanel();
        _lblSecPlus_01 = new Label();
        _lblSecPlus_00 = new Label();
        _lblSecPlus_02 = new Label();
        _txtKPlus = new TextBox();
        _lblKPlusEquals = new Label();
        _btnCalcPlus = new Button();
        _lblMassPlusCap = new Label();
        _btnCapPlus = new Button();
        _txtMassPlus = new TextBox();
        _txtCodePlus = new TextBox();
        _lblCodePlusCap = new Label();
        _lblAutoCalcPlus = new Label();
        tlpCalibDMinus = new TableLayoutPanel();
        _lblSecMinus_01 = new Label();
        _lblSecMinus_00 = new Label();
        _txtKMinus = new TextBox();
        _lblSecMinus_02 = new Label();
        _lblKMinusEquals = new Label();
        _lblAutoCalcMinus = new Label();
        _btnCalcMinus = new Button();
        _lblMassMinusCap = new Label();
        _lblCodeMinusCap = new Label();
        _txtMassMinus = new TextBox();
        _txtCodeMinus = new TextBox();
        _btnCapMinus = new Button();
        _dgvDynCalib = new DataGridView();
        dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
        _pnlCalibDBottom = new Panel();
        _lblFormulaD = new Label();
        _btnCalibDynSave = new Button();
        _pnlCalibDHead = new Panel();
        _tlpHeaders = new TableLayoutPanel();
        _lblLiveWeightD = new Label();
        _lblLiveAdcD = new Label();
        _lblLiveAdcCapD = new Label();
        _btnDynamicCalibPortRefresh = new Button();
        _btnDynamicCalibConn = new Button();
        _lblDynamicCalibConn = new Label();
        _lblLiveWeightCapD = new Label();
        _cmbDynamicCalibPort = new ComboBox();
        _tabSett = new TabPage();
        _lblPortCap = new Label();
        _cmbSettPort = new ComboBox();
        _lblNpvCap = new Label();
        _txtNpv = new TextBox();
        _lblDiscCap = new Label();
        _cmbDisc = new ComboBox();
        _lblZeroCap = new Label();
        _txtZeroLimit = new TextBox();
        _lblPasswordCap = new Label();
        _txtNewPassword = new TextBox();
        _btnSaveSettings = new Button();
        _rateTimer = new System.Windows.Forms.Timer(components);
        _tabs.SuspendLayout();
        _tabChannel.SuspendLayout();
        _tabMonitor.SuspendLayout();
        _pnlCh0.SuspendLayout();
        _pnlCh1.SuspendLayout();
        _tabDynamicService.SuspendLayout();
        _pnlDynamicCh0.SuspendLayout();
        _pnlDynamicCh1.SuspendLayout();
        _tabCalibS.SuspendLayout();
        _pnlCalibS.SuspendLayout();
        _pnlCalibSBody.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvCalib).BeginInit();
        _pnlCalibSHead.SuspendLayout();
        _dotStaticCalibConn.SuspendLayout();
        tlpCalibSHead.SuspendLayout();
        _tabCalibD.SuspendLayout();
        _pnlCalibD.SuspendLayout();
        _pnlCalibDBody.SuspendLayout();
        pnlCalibDMain.SuspendLayout();
        tlpCalibDMain.SuspendLayout();
        tlpDirections.SuspendLayout();
        tlpCalibDPlus.SuspendLayout();
        tlpCalibDMinus.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvDynCalib).BeginInit();
        _pnlCalibDBottom.SuspendLayout();
        _pnlCalibDHead.SuspendLayout();
        _tlpHeaders.SuspendLayout();
        _tabSett.SuspendLayout();
        SuspendLayout();
        //
        // _btnAdmin
        //
        _btnAdmin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnAdmin.FlatAppearance.BorderSize = 0;
        _btnAdmin.FlatStyle = FlatStyle.Flat;
        _btnAdmin.Font = new Font("Segoe UI", 12F);
        _btnAdmin.Location = new Point(1068, 8);
        _btnAdmin.Name = "_btnAdmin";
        _btnAdmin.Size = new Size(270, 28);
        _btnAdmin.TabIndex = 0;
        _btnAdmin.Text = "🔒 Войти как администратор";
        _btnAdmin.UseVisualStyleBackColor = false;
        _btnAdmin.Click += BtnAdmin_Click;
        //
        // _tabs
        //
        _tabs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _tabs.Controls.Add(_tabChannel);
        _tabs.Controls.Add(_tabMonitor);
        _tabs.Controls.Add(_tabDynamicService);
        _tabs.Controls.Add(_tabCalibS);
        _tabs.Controls.Add(_tabCalibD);
        _tabs.Controls.Add(_tabSett);
        _tabs.Font = new Font("Segoe UI", 10F);
        _tabs.Location = new Point(0, 44);
        _tabs.Name = "_tabs";
        _tabs.SelectedIndex = 0;
        _tabs.Size = new Size(1348, 544);
        _tabs.TabIndex = 1;
        //
        // _tabChannel
        //
        _tabChannel.Controls.Add(_lblChannelTitle);
        _tabChannel.Controls.Add(_rbMain);
        _tabChannel.Controls.Add(_rbBackup);
        _tabChannel.Controls.Add(_lblChannelNote);
        _tabChannel.Location = new Point(4, 26);
        _tabChannel.Name = "_tabChannel";
        _tabChannel.Size = new Size(1340, 514);
        _tabChannel.TabIndex = 0;
        _tabChannel.Text = "Канал";
        //
        // _lblChannelTitle
        //
        _lblChannelTitle.AutoSize = true;
        _lblChannelTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        _lblChannelTitle.Location = new Point(20, 16);
        _lblChannelTitle.Name = "_lblChannelTitle";
        _lblChannelTitle.Size = new Size(190, 20);
        _lblChannelTitle.TabIndex = 0;
        _lblChannelTitle.Text = "Выбор активного канала";
        //
        // _rbMain
        //
        _rbMain.AutoSize = true;
        _rbMain.Checked = true;
        _rbMain.Font = new Font("Segoe UI", 13F);
        _rbMain.Location = new Point(20, 52);
        _rbMain.Name = "_rbMain";
        _rbMain.Size = new Size(219, 29);
        _rbMain.TabIndex = 1;
        _rbMain.TabStop = true;
        _rbMain.Text = "Канал: Основной (CH0)";
        _rbMain.CheckedChanged += RbMain_CheckedChanged;
        //
        // _rbBackup
        //
        _rbBackup.AutoSize = true;
        _rbBackup.Font = new Font("Segoe UI", 13F);
        _rbBackup.Location = new Point(20, 90);
        _rbBackup.Name = "_rbBackup";
        _rbBackup.Size = new Size(225, 29);
        _rbBackup.TabIndex = 2;
        _rbBackup.Text = "Канал: Резервный (CH1)";
        _rbBackup.CheckedChanged += RbBackup_CheckedChanged;
        //
        // _lblChannelNote
        //
        _lblChannelNote.AutoSize = true;
        _lblChannelNote.Font = new Font("Segoe UI", 12F);
        _lblChannelNote.Location = new Point(20, 136);
        _lblChannelNote.Name = "_lblChannelNote";
        _lblChannelNote.Size = new Size(485, 21);
        _lblChannelNote.TabIndex = 3;
        _lblChannelNote.Text = "Изменение канала применяется немедленно и не требует пароля.";
        //
        // _tabMonitor
        //
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
        _tabMonitor.Location = new Point(4, 26);
        _tabMonitor.Name = "_tabMonitor";
        _tabMonitor.Size = new Size(1340, 514);
        _tabMonitor.TabIndex = 1;
        _tabMonitor.Text = "Сервисный режим Статика";
        //
        // _cmbPort
        //
        _cmbPort.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbPort.Font = new Font("Segoe UI", 10F);
        _cmbPort.Location = new Point(10, 12);
        _cmbPort.Name = "_cmbPort";
        _cmbPort.Size = new Size(90, 25);
        _cmbPort.TabIndex = 0;
        //
        // _dotConn
        //
        _dotConn.Location = new Point(108, 16);
        _dotConn.Name = "_dotConn";
        _dotConn.Size = new Size(10, 10);
        _dotConn.TabIndex = 1;
        //
        // _btnConn
        //
        _btnConn.FlatAppearance.BorderSize = 0;
        _btnConn.FlatStyle = FlatStyle.Flat;
        _btnConn.Font = new Font("Segoe UI", 12F);
        _btnConn.Location = new Point(124, 8);
        _btnConn.Name = "_btnConn";
        _btnConn.Size = new Size(129, 28);
        _btnConn.TabIndex = 2;
        _btnConn.Text = "Подключить";
        _btnConn.UseVisualStyleBackColor = false;
        _btnConn.Click += BtnMonConn_Click;
        //
        // _btnPortRefresh
        //
        _btnPortRefresh.FlatStyle = FlatStyle.Flat;
        _btnPortRefresh.Font = new Font("Segoe UI", 11F);
        _btnPortRefresh.Location = new Point(282, 8);
        _btnPortRefresh.Name = "_btnPortRefresh";
        _btnPortRefresh.Size = new Size(30, 28);
        _btnPortRefresh.TabIndex = 3;
        _btnPortRefresh.Text = "↺";
        _btnPortRefresh.UseVisualStyleBackColor = false;
        _btnPortRefresh.Click += BtnPortRefresh_Click;
        //
        // _lblConn
        //
        _lblConn.AutoSize = true;
        _lblConn.Font = new Font("Segoe UI", 12F);
        _lblConn.Location = new Point(403, 14);
        _lblConn.Name = "_lblConn";
        _lblConn.Size = new Size(139, 21);
        _lblConn.TabIndex = 4;
        _lblConn.Text = "Нет подключения";
        //
        // _lblRate
        //
        _lblRate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblRate.AutoSize = true;
        _lblRate.Font = new Font("Segoe UI", 12F);
        _lblRate.Location = new Point(895, 14);
        _lblRate.Name = "_lblRate";
        _lblRate.Size = new Size(63, 21);
        _lblRate.TabIndex = 5;
        _lblRate.Text = "— фр/с";
        //
        // _pnlCh0
        //
        _pnlCh0.Controls.Add(_lblCh0Cap);
        _pnlCh0.Controls.Add(_lblCh0);
        _pnlCh0.Location = new Point(10, 48);
        _pnlCh0.Name = "_pnlCh0";
        _pnlCh0.Size = new Size(340, 144);
        _pnlCh0.TabIndex = 6;
        //
        // _lblCh0Cap
        //
        _lblCh0Cap.AutoSize = true;
        _lblCh0Cap.Font = new Font("Segoe UI", 12F);
        _lblCh0Cap.Location = new Point(8, 6);
        _lblCh0Cap.Name = "_lblCh0Cap";
        _lblCh0Cap.Size = new Size(175, 21);
        _lblCh0Cap.TabIndex = 0;
        _lblCh0Cap.Text = "Канал: Основной (CH0)";
        //
        // _lblCh0
        //
        _lblCh0.Font = new Font("Courier New", 48F, FontStyle.Bold);
        _lblCh0.Location = new Point(8, 28);
        _lblCh0.Name = "_lblCh0";
        _lblCh0.Size = new Size(324, 90);
        _lblCh0.TabIndex = 1;
        _lblCh0.Text = "—";
        _lblCh0.TextAlign = ContentAlignment.MiddleRight;
        //
        // _pnlCh1
        //
        _pnlCh1.Controls.Add(_lblCh1Cap);
        _pnlCh1.Controls.Add(_lblCh1);
        _pnlCh1.Location = new Point(360, 48);
        _pnlCh1.Name = "_pnlCh1";
        _pnlCh1.Size = new Size(340, 144);
        _pnlCh1.TabIndex = 7;
        //
        // _lblCh1Cap
        //
        _lblCh1Cap.AutoSize = true;
        _lblCh1Cap.Font = new Font("Segoe UI", 12F);
        _lblCh1Cap.Location = new Point(8, 6);
        _lblCh1Cap.Name = "_lblCh1Cap";
        _lblCh1Cap.Size = new Size(181, 21);
        _lblCh1Cap.TabIndex = 0;
        _lblCh1Cap.Text = "Канал: Резервный (CH1)";
        //
        // _lblCh1
        //
        _lblCh1.Font = new Font("Courier New", 48F, FontStyle.Bold);
        _lblCh1.Location = new Point(8, 28);
        _lblCh1.Name = "_lblCh1";
        _lblCh1.Size = new Size(324, 90);
        _lblCh1.TabIndex = 1;
        _lblCh1.Text = "—";
        _lblCh1.TextAlign = ContentAlignment.MiddleRight;
        //
        // _chkLog
        //
        _chkLog.AutoSize = true;
        _chkLog.Checked = true;
        _chkLog.CheckState = CheckState.Checked;
        _chkLog.Font = new Font("Segoe UI", 12F);
        _chkLog.Location = new Point(10, 198);
        _chkLog.Name = "_chkLog";
        _chkLog.Size = new Size(116, 25);
        _chkLog.TabIndex = 8;
        _chkLog.Text = "Лог активен";
        //
        // _btnClearLog
        //
        _btnClearLog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnClearLog.FlatStyle = FlatStyle.Flat;
        _btnClearLog.Font = new Font("Segoe UI", 12F);
        _btnClearLog.Location = new Point(933, 198);
        _btnClearLog.Name = "_btnClearLog";
        _btnClearLog.Size = new Size(101, 32);
        _btnClearLog.TabIndex = 9;
        _btnClearLog.Text = "Очистить";
        _btnClearLog.UseVisualStyleBackColor = false;
        _btnClearLog.Click += BtnClearLog_Click;
        //
        // _rtbLog
        //
        _rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _rtbLog.DetectUrls = false;
        _rtbLog.Font = new Font("Courier New", 9F);
        _rtbLog.Location = new Point(10, 239);
        _rtbLog.Name = "_rtbLog";
        _rtbLog.ReadOnly = true;
        _rtbLog.ScrollBars = RichTextBoxScrollBars.Vertical;
        _rtbLog.Size = new Size(1025, 266);
        _rtbLog.TabIndex = 10;
        _rtbLog.Text = "";
        _rtbLog.WordWrap = false;
        //
        // _tabDynamicService
        //
        _tabDynamicService.Controls.Add(_cmbDynamicPort);
        _tabDynamicService.Controls.Add(_dotDynamicConn);
        _tabDynamicService.Controls.Add(_btnDynamicConn);
        _tabDynamicService.Controls.Add(_btnDynamicPortRefresh);
        _tabDynamicService.Controls.Add(_lblDynamicConn);
        _tabDynamicService.Controls.Add(_lblDynamicRate);
        _tabDynamicService.Controls.Add(_pnlDynamicCh0);
        _tabDynamicService.Controls.Add(_pnlDynamicCh1);
        _tabDynamicService.Controls.Add(_chkDynamicLog);
        _tabDynamicService.Controls.Add(_btnDynamicClearLog);
        _tabDynamicService.Controls.Add(_rtbDynamicLog);
        _tabDynamicService.Location = new Point(4, 26);
        _tabDynamicService.Name = "_tabDynamicService";
        _tabDynamicService.Size = new Size(1340, 514);
        _tabDynamicService.TabIndex = 2;
        _tabDynamicService.Text = "Сервисный режим Динамика";
        //
        // _cmbDynamicPort
        //
        _cmbDynamicPort.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbDynamicPort.Font = new Font("Segoe UI", 10F);
        _cmbDynamicPort.Location = new Point(18, 10);
        _cmbDynamicPort.Name = "_cmbDynamicPort";
        _cmbDynamicPort.Size = new Size(123, 25);
        _cmbDynamicPort.TabIndex = 0;
        //
        // _dotDynamicConn
        //
        _dotDynamicConn.Location = new Point(158, 14);
        _dotDynamicConn.Name = "_dotDynamicConn";
        _dotDynamicConn.Size = new Size(14, 12);
        _dotDynamicConn.TabIndex = 1;
        //
        // _btnDynamicConn
        //
        _btnDynamicConn.FlatStyle = FlatStyle.Flat;
        _btnDynamicConn.Font = new Font("Segoe UI", 12F);
        _btnDynamicConn.Location = new Point(184, 8);
        _btnDynamicConn.Name = "_btnDynamicConn";
        _btnDynamicConn.Size = new Size(141, 30);
        _btnDynamicConn.TabIndex = 2;
        _btnDynamicConn.Text = "Подключить";
        _btnDynamicConn.UseVisualStyleBackColor = false;
        _btnDynamicConn.Click += BtnDynamicConn_Click;
        //
        // _btnDynamicPortRefresh
        //
        _btnDynamicPortRefresh.FlatStyle = FlatStyle.Flat;
        _btnDynamicPortRefresh.Font = new Font("Segoe UI", 11F);
        _btnDynamicPortRefresh.Location = new Point(330, 8);
        _btnDynamicPortRefresh.Name = "_btnDynamicPortRefresh";
        _btnDynamicPortRefresh.Size = new Size(37, 30);
        _btnDynamicPortRefresh.TabIndex = 3;
        _btnDynamicPortRefresh.Text = "↺";
        _btnDynamicPortRefresh.UseVisualStyleBackColor = false;
        _btnDynamicPortRefresh.Click += BtnDynamicPortRefresh_Click;
        //
        // _lblDynamicConn
        //
        _lblDynamicConn.Font = new Font("Segoe UI", 12F);
        _lblDynamicConn.Location = new Point(428, 8);
        _lblDynamicConn.Name = "_lblDynamicConn";
        _lblDynamicConn.Size = new Size(254, 17);
        _lblDynamicConn.TabIndex = 4;
        _lblDynamicConn.Text = "Нет подключения";
        //
        // _lblDynamicRate
        //
        _lblDynamicRate.Font = new Font("Segoe UI", 12F);
        _lblDynamicRate.Location = new Point(18, 39);
        _lblDynamicRate.Name = "_lblDynamicRate";
        _lblDynamicRate.Size = new Size(105, 18);
        _lblDynamicRate.TabIndex = 5;
        _lblDynamicRate.Text = "— сэмпл/с";
        //
        // _pnlDynamicCh0
        //
        _pnlDynamicCh0.Controls.Add(_lblDynamicCh0Cap);
        _pnlDynamicCh0.Controls.Add(_lblDynamicCh0);
        _pnlDynamicCh0.Location = new Point(18, 64);
        _pnlDynamicCh0.Name = "_pnlDynamicCh0";
        _pnlDynamicCh0.Size = new Size(298, 108);
        _pnlDynamicCh0.TabIndex = 6;
        //
        // _lblDynamicCh0Cap
        //
        _lblDynamicCh0Cap.AutoSize = true;
        _lblDynamicCh0Cap.Font = new Font("Segoe UI", 12F);
        _lblDynamicCh0Cap.Location = new Point(7, 4);
        _lblDynamicCh0Cap.Name = "_lblDynamicCh0Cap";
        _lblDynamicCh0Cap.Size = new Size(175, 21);
        _lblDynamicCh0Cap.TabIndex = 0;
        _lblDynamicCh0Cap.Text = "Канал: Основной (CH0)";
        //
        // _lblDynamicCh0
        //
        _lblDynamicCh0.Font = new Font("Courier New", 48F, FontStyle.Bold);
        _lblDynamicCh0.Location = new Point(7, 21);
        _lblDynamicCh0.Name = "_lblDynamicCh0";
        _lblDynamicCh0.Size = new Size(284, 68);
        _lblDynamicCh0.TabIndex = 1;
        _lblDynamicCh0.Text = "—";
        _lblDynamicCh0.TextAlign = ContentAlignment.MiddleRight;
        //
        // _pnlDynamicCh1
        //
        _pnlDynamicCh1.Controls.Add(_lblDynamicCh1Cap);
        _pnlDynamicCh1.Controls.Add(_lblDynamicCh1);
        _pnlDynamicCh1.Location = new Point(315, 64);
        _pnlDynamicCh1.Name = "_pnlDynamicCh1";
        _pnlDynamicCh1.Size = new Size(298, 108);
        _pnlDynamicCh1.TabIndex = 7;
        //
        // _lblDynamicCh1Cap
        //
        _lblDynamicCh1Cap.AutoSize = true;
        _lblDynamicCh1Cap.Font = new Font("Segoe UI", 12F);
        _lblDynamicCh1Cap.Location = new Point(7, 4);
        _lblDynamicCh1Cap.Name = "_lblDynamicCh1Cap";
        _lblDynamicCh1Cap.Size = new Size(181, 21);
        _lblDynamicCh1Cap.TabIndex = 0;
        _lblDynamicCh1Cap.Text = "Канал: Резервный (CH1)";
        //
        // _lblDynamicCh1
        //
        _lblDynamicCh1.Font = new Font("Courier New", 48F, FontStyle.Bold);
        _lblDynamicCh1.Location = new Point(7, 21);
        _lblDynamicCh1.Name = "_lblDynamicCh1";
        _lblDynamicCh1.Size = new Size(284, 68);
        _lblDynamicCh1.TabIndex = 1;
        _lblDynamicCh1.Text = "—";
        _lblDynamicCh1.TextAlign = ContentAlignment.MiddleRight;
        //
        // _chkDynamicLog
        //
        _chkDynamicLog.AutoSize = true;
        _chkDynamicLog.Checked = true;
        _chkDynamicLog.CheckState = CheckState.Checked;
        _chkDynamicLog.Font = new Font("Segoe UI", 12F);
        _chkDynamicLog.Location = new Point(18, 148);
        _chkDynamicLog.Name = "_chkDynamicLog";
        _chkDynamicLog.Size = new Size(116, 25);
        _chkDynamicLog.TabIndex = 8;
        _chkDynamicLog.Text = "Лог активен";
        //
        // _btnDynamicClearLog
        //
        _btnDynamicClearLog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnDynamicClearLog.FlatStyle = FlatStyle.Flat;
        _btnDynamicClearLog.Font = new Font("Segoe UI", 12F);
        _btnDynamicClearLog.Location = new Point(756, 138);
        _btnDynamicClearLog.Name = "_btnDynamicClearLog";
        _btnDynamicClearLog.Size = new Size(119, 34);
        _btnDynamicClearLog.TabIndex = 9;
        _btnDynamicClearLog.Text = "Очистить";
        _btnDynamicClearLog.UseVisualStyleBackColor = false;
        _btnDynamicClearLog.Click += BtnDynamicClearLog_Click;
        //
        // _rtbDynamicLog
        //
        _rtbDynamicLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _rtbDynamicLog.DetectUrls = false;
        _rtbDynamicLog.Font = new Font("Courier New", 9F);
        _rtbDynamicLog.Location = new Point(9, 179);
        _rtbDynamicLog.Name = "_rtbDynamicLog";
        _rtbDynamicLog.ReadOnly = true;
        _rtbDynamicLog.ScrollBars = RichTextBoxScrollBars.Vertical;
        _rtbDynamicLog.Size = new Size(867, 338);
        _rtbDynamicLog.TabIndex = 10;
        _rtbDynamicLog.Text = "";
        _rtbDynamicLog.WordWrap = false;
        //
        // _tabCalibS
        //
        _tabCalibS.Controls.Add(_pnlCalibS);
        _tabCalibS.Location = new Point(4, 26);
        _tabCalibS.Name = "_tabCalibS";
        _tabCalibS.Size = new Size(1340, 514);
        _tabCalibS.TabIndex = 3;
        _tabCalibS.Text = "Калибровка Статика";
        //
        // _pnlCalibS
        //
        _pnlCalibS.BackColor = Color.NavajoWhite;
        _pnlCalibS.Controls.Add(_pnlCalibSBody);
        _pnlCalibS.Controls.Add(_pnlCalibSHead);
        _pnlCalibS.Dock = DockStyle.Fill;
        _pnlCalibS.Location = new Point(0, 0);
        _pnlCalibS.Name = "_pnlCalibS";
        _pnlCalibS.Size = new Size(1340, 514);
        _pnlCalibS.TabIndex = 0;
        //
        // _pnlCalibSBody
        //
        _pnlCalibSBody.AutoScroll = true;
        _pnlCalibSBody.BackColor = SystemColors.ActiveCaption;
        _pnlCalibSBody.BorderStyle = BorderStyle.FixedSingle;
        _pnlCalibSBody.Controls.Add(_dgvCalib);
        _pnlCalibSBody.Controls.Add(_btnAddRow);
        _pnlCalibSBody.Controls.Add(_btnDelRow);
        _pnlCalibSBody.Controls.Add(_lblKEquals);
        _pnlCalibSBody.Controls.Add(_txtK);
        _pnlCalibSBody.Controls.Add(_lblBEquals);
        _pnlCalibSBody.Controls.Add(_txtB);
        _pnlCalibSBody.Controls.Add(_lblFormula);
        _pnlCalibSBody.Controls.Add(_btnLsq);
        _pnlCalibSBody.Controls.Add(_btnCalibSave);
        _pnlCalibSBody.Dock = DockStyle.Fill;
        _pnlCalibSBody.Location = new Point(0, 72);
        _pnlCalibSBody.Name = "_pnlCalibSBody";
        _pnlCalibSBody.Padding = new Padding(16, 12, 16, 12);
        _pnlCalibSBody.Size = new Size(1340, 442);
        _pnlCalibSBody.TabIndex = 1;
        //
        // _dgvCalib
        //
        _dgvCalib.AllowUserToAddRows = false;
        _dgvCalib.AllowUserToDeleteRows = false;
        _dgvCalib.AllowUserToResizeRows = false;
        _dgvCalib.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dgvCalib.BackgroundColor = Color.FromArgb(245, 245, 247);
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(196, 225, 230);
        dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(26, 26, 26);
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        _dgvCalib.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        _dgvCalib.ColumnHeadersHeight = 34;
        _dgvCalib.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _dgvCalib.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewCheckBoxColumn1, dataGridViewTextBoxColumn3 });
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = SystemColors.Window;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(220, 232, 247);
        dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(26, 26, 26);
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        _dgvCalib.DefaultCellStyle = dataGridViewCellStyle2;
        _dgvCalib.EditMode = DataGridViewEditMode.EditOnEnter;
        _dgvCalib.EnableHeadersVisualStyles = false;
        _dgvCalib.Font = new Font("Segoe UI", 12F);
        _dgvCalib.GridColor = Color.FromArgb(212, 216, 222);
        _dgvCalib.Location = new Point(16, 0);
        _dgvCalib.Name = "_dgvCalib";
        _dgvCalib.RowHeadersVisible = false;
        _dgvCalib.RowHeadersWidth = 62;
        _dgvCalib.RowTemplate.Height = 30;
        _dgvCalib.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvCalib.Size = new Size(460, 250);
        _dgvCalib.TabIndex = 5;
        //
        // dataGridViewTextBoxColumn1
        //
        dataGridViewTextBoxColumn1.FillWeight = 25F;
        dataGridViewTextBoxColumn1.HeaderText = "Код АЦП";
        dataGridViewTextBoxColumn1.MinimumWidth = 125;
        dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
        //
        // dataGridViewTextBoxColumn2
        //
        dataGridViewTextBoxColumn2.FillWeight = 25F;
        dataGridViewTextBoxColumn2.HeaderText = "Масса, т";
        dataGridViewTextBoxColumn2.MinimumWidth = 115;
        dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
        //
        // dataGridViewCheckBoxColumn1
        //
        dataGridViewCheckBoxColumn1.FillWeight = 25F;
        dataGridViewCheckBoxColumn1.HeaderText = "Активна";
        dataGridViewCheckBoxColumn1.MinimumWidth = 90;
        dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
        dataGridViewCheckBoxColumn1.ReadOnly = true;
        //
        // dataGridViewTextBoxColumn3
        //
        dataGridViewTextBoxColumn3.FillWeight = 25F;
        dataGridViewTextBoxColumn3.HeaderText = "K/65535";
        dataGridViewTextBoxColumn3.MinimumWidth = 105;
        dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
        dataGridViewTextBoxColumn3.ReadOnly = true;
        //
        // _btnAddRow
        //
        _btnAddRow.FlatStyle = FlatStyle.Flat;
        _btnAddRow.Font = new Font("Segoe UI", 12F);
        _btnAddRow.Location = new Point(16, 256);
        _btnAddRow.Name = "_btnAddRow";
        _btnAddRow.Size = new Size(160, 28);
        _btnAddRow.TabIndex = 6;
        _btnAddRow.Text = "Добавить строку";
        _btnAddRow.UseVisualStyleBackColor = false;
        _btnAddRow.Click += BtnAddRow_Click;
        //
        // _btnDelRow
        //
        _btnDelRow.FlatStyle = FlatStyle.Flat;
        _btnDelRow.Font = new Font("Segoe UI", 12F);
        _btnDelRow.Location = new Point(188, 256);
        _btnDelRow.Name = "_btnDelRow";
        _btnDelRow.Size = new Size(170, 28);
        _btnDelRow.TabIndex = 7;
        _btnDelRow.Text = "Удалить выбранную";
        _btnDelRow.UseVisualStyleBackColor = false;
        _btnDelRow.Click += BtnDelRow_Click;
        //
        // _lblKEquals
        //
        _lblKEquals.AutoSize = true;
        _lblKEquals.Font = new Font("Segoe UI", 10F);
        _lblKEquals.Location = new Point(494, 61);
        _lblKEquals.Name = "_lblKEquals";
        _lblKEquals.Size = new Size(34, 19);
        _lblKEquals.TabIndex = 8;
        _lblKEquals.Text = "k  =";
        //
        // _txtK
        //
        _txtK.Font = new Font("Courier New", 10F);
        _txtK.Location = new Point(530, 56);
        _txtK.Name = "_txtK";
        _txtK.Size = new Size(130, 23);
        _txtK.TabIndex = 9;
        _txtK.Text = "0";
        //
        // _lblBEquals
        //
        _lblBEquals.AutoSize = true;
        _lblBEquals.Font = new Font("Segoe UI", 10F);
        _lblBEquals.Location = new Point(494, 97);
        _lblBEquals.Name = "_lblBEquals";
        _lblBEquals.Size = new Size(35, 19);
        _lblBEquals.TabIndex = 10;
        _lblBEquals.Text = "b  =";
        //
        // _txtB
        //
        _txtB.Font = new Font("Courier New", 10F);
        _txtB.Location = new Point(530, 93);
        _txtB.Name = "_txtB";
        _txtB.Size = new Size(130, 23);
        _txtB.TabIndex = 11;
        _txtB.Text = "0";
        //
        // _lblFormula
        //
        _lblFormula.AutoSize = true;
        _lblFormula.Font = new Font("Segoe UI", 12F);
        _lblFormula.Location = new Point(519, 24);
        _lblFormula.Name = "_lblFormula";
        _lblFormula.Size = new Size(155, 21);
        _lblFormula.TabIndex = 12;
        _lblFormula.Text = "Масса = k × Код + b";
        //
        // _btnLsq
        //
        _btnLsq.FlatAppearance.BorderSize = 0;
        _btnLsq.FlatStyle = FlatStyle.Flat;
        _btnLsq.Font = new Font("Segoe UI", 10F);
        _btnLsq.Location = new Point(496, 137);
        _btnLsq.Name = "_btnLsq";
        _btnLsq.Size = new Size(180, 34);
        _btnLsq.TabIndex = 13;
        _btnLsq.Text = "Рассчитать МНК";
        _btnLsq.UseVisualStyleBackColor = false;
        _btnLsq.Click += BtnLsq_Click;
        //
        // _btnCalibSave
        //
        _btnCalibSave.FlatAppearance.BorderSize = 0;
        _btnCalibSave.FlatStyle = FlatStyle.Flat;
        _btnCalibSave.Font = new Font("Segoe UI", 12F);
        _btnCalibSave.Location = new Point(496, 197);
        _btnCalibSave.Name = "_btnCalibSave";
        _btnCalibSave.Size = new Size(162, 34);
        _btnCalibSave.TabIndex = 14;
        _btnCalibSave.Text = "Применить и сохранить";
        _btnCalibSave.UseVisualStyleBackColor = false;
        _btnCalibSave.Click += BtnCalibSave_Click;
        //
        // _pnlCalibSHead
        //
        _pnlCalibSHead.BorderStyle = BorderStyle.FixedSingle;
        _pnlCalibSHead.Controls.Add(_dotStaticCalibConn);
        _pnlCalibSHead.Controls.Add(_btnStaticCalibConn);
        _pnlCalibSHead.Controls.Add(_btnStaticCalibPortRefresh);
        _pnlCalibSHead.Controls.Add(_lblStaticCalibConn);
        _pnlCalibSHead.Controls.Add(tlpCalibSHead);
        _pnlCalibSHead.Dock = DockStyle.Top;
        _pnlCalibSHead.Location = new Point(0, 0);
        _pnlCalibSHead.Name = "_pnlCalibSHead";
        _pnlCalibSHead.Size = new Size(1340, 72);
        _pnlCalibSHead.TabIndex = 0;
        //
        // _dotStaticCalibConn
        //
        _dotStaticCalibConn.Controls.Add(_cmbStaticCalibPort);
        _dotStaticCalibConn.Dock = DockStyle.Left;
        _dotStaticCalibConn.Location = new Point(0, 0);
        _dotStaticCalibConn.Margin = new Padding(3, 2, 3, 2);
        _dotStaticCalibConn.Name = "_dotStaticCalibConn";
        _dotStaticCalibConn.Size = new Size(158, 70);
        _dotStaticCalibConn.TabIndex = 1;
        //
        // _cmbStaticCalibPort
        //
        _cmbStaticCalibPort.Font = new Font("Segoe UI", 10F);
        _cmbStaticCalibPort.Location = new Point(16, 22);
        _cmbStaticCalibPort.Margin = new Padding(3, 2, 3, 2);
        _cmbStaticCalibPort.Name = "_cmbStaticCalibPort";
        _cmbStaticCalibPort.Size = new Size(106, 25);
        _cmbStaticCalibPort.TabIndex = 0;
        //
        // _btnStaticCalibConn
        //
        _btnStaticCalibConn.Font = new Font("Segoe UI", 12F);
        _btnStaticCalibConn.Location = new Point(0, 0);
        _btnStaticCalibConn.Margin = new Padding(3, 2, 3, 2);
        _btnStaticCalibConn.Name = "_btnStaticCalibConn";
        _btnStaticCalibConn.Size = new Size(66, 17);
        _btnStaticCalibConn.TabIndex = 2;
        _btnStaticCalibConn.Click += BtnStaticCalibConn_Click;
        //
        // _btnStaticCalibPortRefresh
        //
        _btnStaticCalibPortRefresh.Font = new Font("Segoe UI", 11F);
        _btnStaticCalibPortRefresh.Location = new Point(0, 0);
        _btnStaticCalibPortRefresh.Margin = new Padding(3, 2, 3, 2);
        _btnStaticCalibPortRefresh.Name = "_btnStaticCalibPortRefresh";
        _btnStaticCalibPortRefresh.Size = new Size(66, 17);
        _btnStaticCalibPortRefresh.TabIndex = 3;
        _btnStaticCalibPortRefresh.Click += BtnPortRefresh_Click;
        //
        // _lblStaticCalibConn
        //
        _lblStaticCalibConn.Font = new Font("Segoe UI", 12F);
        _lblStaticCalibConn.Location = new Point(0, 0);
        _lblStaticCalibConn.Name = "_lblStaticCalibConn";
        _lblStaticCalibConn.Size = new Size(88, 17);
        _lblStaticCalibConn.TabIndex = 4;
        //
        // tlpCalibSHead
        //
        tlpCalibSHead.ColumnCount = 5;
        tlpCalibSHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tlpCalibSHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tlpCalibSHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.7917442F));
        tlpCalibSHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.41651F));
        tlpCalibSHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.73546F));
        tlpCalibSHead.Controls.Add(_rbCh1Calib, 4, 0);
        tlpCalibSHead.Controls.Add(_lblLiveAdc, 1, 0);
        tlpCalibSHead.Controls.Add(_lblLiveAdcCap, 0, 0);
        tlpCalibSHead.Controls.Add(_rbCh0Calib, 4, 1);
        tlpCalibSHead.Controls.Add(_btnCapture, 2, 0);
        tlpCalibSHead.Location = new Point(216, 0);
        tlpCalibSHead.Margin = new Padding(2);
        tlpCalibSHead.Name = "tlpCalibSHead";
        tlpCalibSHead.RowCount = 2;
        tlpCalibSHead.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpCalibSHead.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpCalibSHead.Size = new Size(843, 73);
        tlpCalibSHead.TabIndex = 5;
        //
        // _rbCh1Calib
        //
        _rbCh1Calib.AutoSize = true;
        _rbCh1Calib.Dock = DockStyle.Fill;
        _rbCh1Calib.Font = new Font("Segoe UI", 11F);
        _rbCh1Calib.Location = new Point(622, 3);
        _rbCh1Calib.Margin = new Padding(7, 3, 3, 3);
        _rbCh1Calib.Name = "_rbCh1Calib";
        _rbCh1Calib.Size = new Size(218, 30);
        _rbCh1Calib.TabIndex = 1;
        _rbCh1Calib.Text = "Канал: Резервный (CH1)";
        _rbCh1Calib.CheckedChanged += RbCh1Calib_CheckedChanged;
        //
        // _lblLiveAdc
        //
        _lblLiveAdc.AutoSize = true;
        _lblLiveAdc.Dock = DockStyle.Fill;
        _lblLiveAdc.Font = new Font("Courier New", 13F, FontStyle.Bold);
        _lblLiveAdc.Location = new Point(171, 0);
        _lblLiveAdc.Name = "_lblLiveAdc";
        _lblLiveAdc.Size = new Size(162, 36);
        _lblLiveAdc.TabIndex = 3;
        _lblLiveAdc.Text = "—";
        _lblLiveAdc.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _lblLiveAdcCap
        //
        _lblLiveAdcCap.AutoSize = true;
        _lblLiveAdcCap.Dock = DockStyle.Fill;
        _lblLiveAdcCap.Font = new Font("Segoe UI", 12F);
        _lblLiveAdcCap.Location = new Point(3, 0);
        _lblLiveAdcCap.Name = "_lblLiveAdcCap";
        _lblLiveAdcCap.Size = new Size(162, 36);
        _lblLiveAdcCap.TabIndex = 2;
        _lblLiveAdcCap.Text = "Текущий код АЦП:";
        _lblLiveAdcCap.TextAlign = ContentAlignment.MiddleCenter;
        //
        // _rbCh0Calib
        //
        _rbCh0Calib.AutoSize = true;
        _rbCh0Calib.Checked = true;
        _rbCh0Calib.Dock = DockStyle.Fill;
        _rbCh0Calib.Font = new Font("Segoe UI", 11F);
        _rbCh0Calib.Location = new Point(622, 39);
        _rbCh0Calib.Margin = new Padding(7, 3, 3, 3);
        _rbCh0Calib.Name = "_rbCh0Calib";
        _rbCh0Calib.Size = new Size(218, 31);
        _rbCh0Calib.TabIndex = 0;
        _rbCh0Calib.TabStop = true;
        _rbCh0Calib.Text = "Канал: Основной (CH0)";
        _rbCh0Calib.CheckedChanged += RbCh0Calib_CheckedChanged;
        //
        // _btnCapture
        //
        _btnCapture.Dock = DockStyle.Fill;
        _btnCapture.FlatAppearance.BorderSize = 0;
        _btnCapture.FlatStyle = FlatStyle.Flat;
        _btnCapture.Font = new Font("Segoe UI", 12F);
        _btnCapture.Location = new Point(343, 2);
        _btnCapture.Margin = new Padding(7, 2, 7, 0);
        _btnCapture.Name = "_btnCapture";
        _btnCapture.Size = new Size(127, 34);
        _btnCapture.TabIndex = 4;
        _btnCapture.Text = "Захватить";
        _btnCapture.UseVisualStyleBackColor = false;
        _btnCapture.Click += BtnCapture_Click;
        //
        // _tabCalibD
        //
        _tabCalibD.Controls.Add(_pnlCalibD);
        _tabCalibD.Location = new Point(4, 26);
        _tabCalibD.Name = "_tabCalibD";
        _tabCalibD.Size = new Size(1340, 514);
        _tabCalibD.TabIndex = 4;
        _tabCalibD.Text = "Калибровка Динамика";
        //
        // _pnlCalibD
        //
        _pnlCalibD.Controls.Add(_pnlCalibDBody);
        _pnlCalibD.Controls.Add(_pnlCalibDBottom);
        _pnlCalibD.Controls.Add(_pnlCalibDHead);
        _pnlCalibD.Dock = DockStyle.Fill;
        _pnlCalibD.Location = new Point(0, 0);
        _pnlCalibD.Name = "_pnlCalibD";
        _pnlCalibD.Size = new Size(1340, 514);
        _pnlCalibD.TabIndex = 0;
        //
        // _pnlCalibDBody
        //
        _pnlCalibDBody.AutoScroll = true;
        _pnlCalibDBody.BorderStyle = BorderStyle.FixedSingle;
        _pnlCalibDBody.Controls.Add(pnlCalibDMain);
        _pnlCalibDBody.Dock = DockStyle.Fill;
        _pnlCalibDBody.Location = new Point(0, 75);
        _pnlCalibDBody.Name = "_pnlCalibDBody";
        _pnlCalibDBody.Size = new Size(1340, 385);
        _pnlCalibDBody.TabIndex = 1;
        //
        // pnlCalibDMain
        //
        pnlCalibDMain.Controls.Add(tlpCalibDMain);
        pnlCalibDMain.Dock = DockStyle.Fill;
        pnlCalibDMain.Location = new Point(0, 0);
        pnlCalibDMain.Margin = new Padding(0);
        pnlCalibDMain.Name = "pnlCalibDMain";
        pnlCalibDMain.Size = new Size(1338, 383);
        pnlCalibDMain.TabIndex = 26;
        //
        // tlpCalibDMain
        //
        tlpCalibDMain.ColumnCount = 2;
        tlpCalibDMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.9083F));
        tlpCalibDMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.0917F));
        tlpCalibDMain.Controls.Add(tlpDirections, 0, 0);
        tlpCalibDMain.Controls.Add(_dgvDynCalib, 1, 0);
        tlpCalibDMain.Dock = DockStyle.Fill;
        tlpCalibDMain.Location = new Point(0, 0);
        tlpCalibDMain.Margin = new Padding(3, 2, 3, 2);
        tlpCalibDMain.Name = "tlpCalibDMain";
        tlpCalibDMain.RowCount = 1;
        tlpCalibDMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpCalibDMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpCalibDMain.Size = new Size(1338, 383);
        tlpCalibDMain.TabIndex = 25;
        //
        // tlpDirections
        //
        tlpDirections.ColumnCount = 1;
        tlpDirections.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlpDirections.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlpDirections.Controls.Add(tlpCalibDPlus, 0, 0);
        tlpDirections.Controls.Add(tlpCalibDMinus, 0, 1);
        tlpDirections.Dock = DockStyle.Fill;
        tlpDirections.Location = new Point(3, 2);
        tlpDirections.Margin = new Padding(3, 2, 3, 2);
        tlpDirections.Name = "tlpDirections";
        tlpDirections.RowCount = 2;
        tlpDirections.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpDirections.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpDirections.Size = new Size(648, 379);
        tlpDirections.TabIndex = 25;
        //
        // tlpCalibDPlus
        //
        tlpCalibDPlus.ColumnCount = 3;
        tlpCalibDPlus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tlpCalibDPlus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.4890976F));
        tlpCalibDPlus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.17757F));
        tlpCalibDPlus.Controls.Add(_lblSecPlus_01, 1, 0);
        tlpCalibDPlus.Controls.Add(_lblSecPlus_00, 0, 0);
        tlpCalibDPlus.Controls.Add(_lblSecPlus_02, 2, 0);
        tlpCalibDPlus.Controls.Add(_txtKPlus, 1, 1);
        tlpCalibDPlus.Controls.Add(_lblKPlusEquals, 0, 1);
        tlpCalibDPlus.Controls.Add(_btnCalcPlus, 1, 5);
        tlpCalibDPlus.Controls.Add(_lblMassPlusCap, 0, 4);
        tlpCalibDPlus.Controls.Add(_btnCapPlus, 2, 3);
        tlpCalibDPlus.Controls.Add(_txtMassPlus, 1, 4);
        tlpCalibDPlus.Controls.Add(_txtCodePlus, 1, 3);
        tlpCalibDPlus.Controls.Add(_lblCodePlusCap, 0, 3);
        tlpCalibDPlus.Controls.Add(_lblAutoCalcPlus, 0, 2);
        tlpCalibDPlus.Dock = DockStyle.Fill;
        tlpCalibDPlus.Location = new Point(3, 2);
        tlpCalibDPlus.Margin = new Padding(3, 2, 3, 2);
        tlpCalibDPlus.Name = "tlpCalibDPlus";
        tlpCalibDPlus.RowCount = 6;
        tlpCalibDPlus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDPlus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDPlus.RowStyles.Add(new RowStyle(SizeType.Percent, 12F));
        tlpCalibDPlus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDPlus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDPlus.RowStyles.Add(new RowStyle(SizeType.Percent, 24F));
        tlpCalibDPlus.Size = new Size(642, 185);
        tlpCalibDPlus.TabIndex = 0;
        //
        // _lblSecPlus_01
        //
        _lblSecPlus_01.AutoSize = true;
        _lblSecPlus_01.Dock = DockStyle.Fill;
        _lblSecPlus_01.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecPlus_01.ForeColor = Color.FromArgb(46, 58, 70);
        _lblSecPlus_01.Location = new Point(217, 0);
        _lblSecPlus_01.Name = "_lblSecPlus_01";
        _lblSecPlus_01.Size = new Size(209, 30);
        _lblSecPlus_01.TabIndex = 27;
        _lblSecPlus_01.Text = "Направление  →";
        _lblSecPlus_01.TextAlign = ContentAlignment.MiddleCenter;
        //
        // _lblSecPlus_00
        //
        _lblSecPlus_00.AutoSize = true;
        _lblSecPlus_00.Dock = DockStyle.Fill;
        _lblSecPlus_00.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecPlus_00.ForeColor = Color.FromArgb(46, 58, 70);
        _lblSecPlus_00.Location = new Point(3, 0);
        _lblSecPlus_00.Name = "_lblSecPlus_00";
        _lblSecPlus_00.Size = new Size(208, 30);
        _lblSecPlus_00.TabIndex = 2;
        _lblSecPlus_00.Text = "──────────────────";
        _lblSecPlus_00.TextAlign = ContentAlignment.MiddleRight;
        //
        // _lblSecPlus_02
        //
        _lblSecPlus_02.AutoSize = true;
        _lblSecPlus_02.Dock = DockStyle.Fill;
        _lblSecPlus_02.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecPlus_02.ForeColor = Color.FromArgb(46, 58, 70);
        _lblSecPlus_02.Location = new Point(432, 0);
        _lblSecPlus_02.Name = "_lblSecPlus_02";
        _lblSecPlus_02.Size = new Size(207, 30);
        _lblSecPlus_02.TabIndex = 28;
        _lblSecPlus_02.Text = "──────────────────";
        _lblSecPlus_02.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _txtKPlus
        //
        _txtKPlus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtKPlus.Font = new Font("Courier New", 10F);
        _txtKPlus.Location = new Point(217, 33);
        _txtKPlus.Margin = new Padding(4, 0, 4, 0);
        _txtKPlus.Name = "_txtKPlus";
        _txtKPlus.Size = new Size(208, 23);
        _txtKPlus.TabIndex = 4;
        //
        // _lblKPlusEquals
        //
        _lblKPlusEquals.AutoSize = true;
        _lblKPlusEquals.Dock = DockStyle.Fill;
        _lblKPlusEquals.Font = new Font("Segoe UI", 10F);
        _lblKPlusEquals.Location = new Point(3, 30);
        _lblKPlusEquals.Name = "_lblKPlusEquals";
        _lblKPlusEquals.Size = new Size(208, 30);
        _lblKPlusEquals.TabIndex = 3;
        _lblKPlusEquals.Text = "K→  =";
        _lblKPlusEquals.TextAlign = ContentAlignment.MiddleRight;
        //
        // _btnCalcPlus
        //
        _btnCalcPlus.Dock = DockStyle.Fill;
        _btnCalcPlus.FlatAppearance.BorderSize = 0;
        _btnCalcPlus.FlatStyle = FlatStyle.Flat;
        _btnCalcPlus.Font = new Font("Segoe UI", 12F);
        _btnCalcPlus.Location = new Point(226, 145);
        _btnCalcPlus.Margin = new Padding(12, 5, 12, 5);
        _btnCalcPlus.Name = "_btnCalcPlus";
        _btnCalcPlus.Size = new Size(191, 35);
        _btnCalcPlus.TabIndex = 11;
        _btnCalcPlus.Text = "Рассчитать K→";
        _btnCalcPlus.UseVisualStyleBackColor = false;
        _btnCalcPlus.Click += BtnCalcPlus_Click;
        //
        // _lblMassPlusCap
        //
        _lblMassPlusCap.AutoSize = true;
        _lblMassPlusCap.Dock = DockStyle.Fill;
        _lblMassPlusCap.Font = new Font("Segoe UI", 12F);
        _lblMassPlusCap.Location = new Point(3, 111);
        _lblMassPlusCap.Name = "_lblMassPlusCap";
        _lblMassPlusCap.Size = new Size(208, 30);
        _lblMassPlusCap.TabIndex = 9;
        _lblMassPlusCap.Text = "Эталон (т)";
        _lblMassPlusCap.TextAlign = ContentAlignment.MiddleRight;
        //
        // _btnCapPlus
        //
        _btnCapPlus.Dock = DockStyle.Fill;
        _btnCapPlus.FlatAppearance.BorderSize = 0;
        _btnCapPlus.FlatStyle = FlatStyle.Flat;
        _btnCapPlus.Font = new Font("Segoe UI", 8F);
        _btnCapPlus.Location = new Point(443, 85);
        _btnCapPlus.Margin = new Padding(14, 4, 14, 4);
        _btnCapPlus.Name = "_btnCapPlus";
        _btnCapPlus.Size = new Size(185, 21);
        _btnCapPlus.TabIndex = 8;
        _btnCapPlus.Text = "Захватить";
        _btnCapPlus.UseVisualStyleBackColor = false;
        _btnCapPlus.Click += BtnCapPlus_Click;
        //
        // _txtMassPlus
        //
        _txtMassPlus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtMassPlus.Font = new Font("Courier New", 9F);
        _txtMassPlus.Location = new Point(217, 114);
        _txtMassPlus.Margin = new Padding(4, 0, 4, 0);
        _txtMassPlus.Name = "_txtMassPlus";
        _txtMassPlus.Size = new Size(208, 23);
        _txtMassPlus.TabIndex = 10;
        //
        // _txtCodePlus
        //
        _txtCodePlus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtCodePlus.Font = new Font("Courier New", 9F);
        _txtCodePlus.Location = new Point(217, 85);
        _txtCodePlus.Margin = new Padding(4, 0, 4, 0);
        _txtCodePlus.Name = "_txtCodePlus";
        _txtCodePlus.Size = new Size(208, 23);
        _txtCodePlus.TabIndex = 7;
        //
        // _lblCodePlusCap
        //
        _lblCodePlusCap.AutoSize = true;
        _lblCodePlusCap.Dock = DockStyle.Fill;
        _lblCodePlusCap.Font = new Font("Segoe UI", 12F);
        _lblCodePlusCap.Location = new Point(3, 81);
        _lblCodePlusCap.Name = "_lblCodePlusCap";
        _lblCodePlusCap.Size = new Size(208, 30);
        _lblCodePlusCap.TabIndex = 6;
        _lblCodePlusCap.Text = "Код АЦП";
        _lblCodePlusCap.TextAlign = ContentAlignment.MiddleRight;
        //
        // _lblAutoCalcPlus
        //
        _lblAutoCalcPlus.AutoSize = true;
        _lblAutoCalcPlus.Dock = DockStyle.Fill;
        _lblAutoCalcPlus.Font = new Font("Segoe UI", 12F);
        _lblAutoCalcPlus.Location = new Point(3, 60);
        _lblAutoCalcPlus.Name = "_lblAutoCalcPlus";
        _lblAutoCalcPlus.Size = new Size(208, 21);
        _lblAutoCalcPlus.TabIndex = 5;
        _lblAutoCalcPlus.Text = "Авторасчёт";
        _lblAutoCalcPlus.TextAlign = ContentAlignment.MiddleCenter;
        //
        // tlpCalibDMinus
        //
        tlpCalibDMinus.ColumnCount = 3;
        tlpCalibDMinus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tlpCalibDMinus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tlpCalibDMinus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tlpCalibDMinus.Controls.Add(_lblSecMinus_01, 1, 0);
        tlpCalibDMinus.Controls.Add(_lblSecMinus_00, 0, 0);
        tlpCalibDMinus.Controls.Add(_txtKMinus, 1, 1);
        tlpCalibDMinus.Controls.Add(_lblSecMinus_02, 2, 0);
        tlpCalibDMinus.Controls.Add(_lblKMinusEquals, 0, 1);
        tlpCalibDMinus.Controls.Add(_lblAutoCalcMinus, 0, 2);
        tlpCalibDMinus.Controls.Add(_btnCalcMinus, 1, 5);
        tlpCalibDMinus.Controls.Add(_lblMassMinusCap, 0, 4);
        tlpCalibDMinus.Controls.Add(_lblCodeMinusCap, 0, 3);
        tlpCalibDMinus.Controls.Add(_txtMassMinus, 1, 4);
        tlpCalibDMinus.Controls.Add(_txtCodeMinus, 1, 3);
        tlpCalibDMinus.Controls.Add(_btnCapMinus, 2, 3);
        tlpCalibDMinus.Dock = DockStyle.Fill;
        tlpCalibDMinus.Location = new Point(3, 191);
        tlpCalibDMinus.Margin = new Padding(3, 2, 3, 2);
        tlpCalibDMinus.Name = "tlpCalibDMinus";
        tlpCalibDMinus.RowCount = 6;
        tlpCalibDMinus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDMinus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDMinus.RowStyles.Add(new RowStyle(SizeType.Percent, 12F));
        tlpCalibDMinus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDMinus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDMinus.RowStyles.Add(new RowStyle(SizeType.Percent, 24F));
        tlpCalibDMinus.Size = new Size(642, 186);
        tlpCalibDMinus.TabIndex = 1;
        //
        // _lblSecMinus_01
        //
        _lblSecMinus_01.AutoSize = true;
        _lblSecMinus_01.Dock = DockStyle.Fill;
        _lblSecMinus_01.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecMinus_01.ForeColor = Color.FromArgb(46, 58, 70);
        _lblSecMinus_01.Location = new Point(217, 0);
        _lblSecMinus_01.Name = "_lblSecMinus_01";
        _lblSecMinus_01.Size = new Size(208, 31);
        _lblSecMinus_01.TabIndex = 28;
        _lblSecMinus_01.Text = "Направление  ←";
        _lblSecMinus_01.TextAlign = ContentAlignment.MiddleCenter;
        //
        // _lblSecMinus_00
        //
        _lblSecMinus_00.AutoSize = true;
        _lblSecMinus_00.Dock = DockStyle.Fill;
        _lblSecMinus_00.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecMinus_00.ForeColor = Color.FromArgb(46, 58, 70);
        _lblSecMinus_00.Location = new Point(3, 0);
        _lblSecMinus_00.Name = "_lblSecMinus_00";
        _lblSecMinus_00.Size = new Size(208, 31);
        _lblSecMinus_00.TabIndex = 12;
        _lblSecMinus_00.Text = "──────────────────";
        _lblSecMinus_00.TextAlign = ContentAlignment.MiddleRight;
        //
        // _txtKMinus
        //
        _txtKMinus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtKMinus.Font = new Font("Courier New", 10F);
        _txtKMinus.Location = new Point(217, 34);
        _txtKMinus.Margin = new Padding(4, 0, 4, 0);
        _txtKMinus.Name = "_txtKMinus";
        _txtKMinus.Size = new Size(207, 23);
        _txtKMinus.TabIndex = 14;
        //
        // _lblSecMinus_02
        //
        _lblSecMinus_02.AutoSize = true;
        _lblSecMinus_02.Dock = DockStyle.Fill;
        _lblSecMinus_02.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecMinus_02.ForeColor = Color.FromArgb(46, 58, 70);
        _lblSecMinus_02.Location = new Point(431, 0);
        _lblSecMinus_02.Name = "_lblSecMinus_02";
        _lblSecMinus_02.Size = new Size(208, 31);
        _lblSecMinus_02.TabIndex = 27;
        _lblSecMinus_02.Text = "──────────────────";
        _lblSecMinus_02.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _lblKMinusEquals
        //
        _lblKMinusEquals.AutoSize = true;
        _lblKMinusEquals.Dock = DockStyle.Fill;
        _lblKMinusEquals.Font = new Font("Segoe UI", 10F);
        _lblKMinusEquals.Location = new Point(3, 31);
        _lblKMinusEquals.Name = "_lblKMinusEquals";
        _lblKMinusEquals.Size = new Size(208, 31);
        _lblKMinusEquals.TabIndex = 13;
        _lblKMinusEquals.Text = "K←  =";
        _lblKMinusEquals.TextAlign = ContentAlignment.MiddleRight;
        //
        // _lblAutoCalcMinus
        //
        _lblAutoCalcMinus.AutoSize = true;
        _lblAutoCalcMinus.Dock = DockStyle.Fill;
        _lblAutoCalcMinus.Font = new Font("Segoe UI", 12F);
        _lblAutoCalcMinus.Location = new Point(3, 62);
        _lblAutoCalcMinus.Name = "_lblAutoCalcMinus";
        _lblAutoCalcMinus.Size = new Size(208, 26);
        _lblAutoCalcMinus.TabIndex = 15;
        _lblAutoCalcMinus.Text = "Авторасчёт";
        _lblAutoCalcMinus.TextAlign = ContentAlignment.MiddleCenter;
        //
        // _btnCalcMinus
        //
        _btnCalcMinus.Dock = DockStyle.Fill;
        _btnCalcMinus.FlatAppearance.BorderSize = 0;
        _btnCalcMinus.FlatStyle = FlatStyle.Flat;
        _btnCalcMinus.Font = new Font("Segoe UI", 12F);
        _btnCalcMinus.Location = new Point(226, 151);
        _btnCalcMinus.Margin = new Padding(12, 5, 12, 5);
        _btnCalcMinus.Name = "_btnCalcMinus";
        _btnCalcMinus.Size = new Size(190, 35);
        _btnCalcMinus.TabIndex = 21;
        _btnCalcMinus.Text = "Рассчитать K←";
        _btnCalcMinus.UseVisualStyleBackColor = false;
        _btnCalcMinus.Click += BtnCalcMinus_Click;
        //
        // _lblMassMinusCap
        //
        _lblMassMinusCap.AutoSize = true;
        _lblMassMinusCap.Dock = DockStyle.Fill;
        _lblMassMinusCap.Font = new Font("Segoe UI", 12F);
        _lblMassMinusCap.Location = new Point(3, 118);
        _lblMassMinusCap.Name = "_lblMassMinusCap";
        _lblMassMinusCap.Size = new Size(208, 31);
        _lblMassMinusCap.TabIndex = 19;
        _lblMassMinusCap.Text = "Эталон (т)";
        _lblMassMinusCap.TextAlign = ContentAlignment.MiddleRight;
        //
        // _lblCodeMinusCap
        //
        _lblCodeMinusCap.AutoSize = true;
        _lblCodeMinusCap.Dock = DockStyle.Fill;
        _lblCodeMinusCap.Font = new Font("Segoe UI", 12F);
        _lblCodeMinusCap.Location = new Point(3, 88);
        _lblCodeMinusCap.Name = "_lblCodeMinusCap";
        _lblCodeMinusCap.Size = new Size(208, 30);
        _lblCodeMinusCap.TabIndex = 16;
        _lblCodeMinusCap.Text = "Код АЦП";
        _lblCodeMinusCap.TextAlign = ContentAlignment.MiddleRight;
        //
        // _txtMassMinus
        //
        _txtMassMinus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtMassMinus.Font = new Font("Courier New", 9F);
        _txtMassMinus.Location = new Point(217, 121);
        _txtMassMinus.Margin = new Padding(4, 0, 4, 0);
        _txtMassMinus.Name = "_txtMassMinus";
        _txtMassMinus.Size = new Size(207, 23);
        _txtMassMinus.TabIndex = 20;
        //
        // _txtCodeMinus
        //
        _txtCodeMinus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtCodeMinus.Font = new Font("Courier New", 9F);
        _txtCodeMinus.Location = new Point(217, 91);
        _txtCodeMinus.Margin = new Padding(4, 0, 4, 0);
        _txtCodeMinus.Name = "_txtCodeMinus";
        _txtCodeMinus.Size = new Size(207, 23);
        _txtCodeMinus.TabIndex = 17;
        //
        // _btnCapMinus
        //
        _btnCapMinus.Dock = DockStyle.Fill;
        _btnCapMinus.FlatAppearance.BorderSize = 0;
        _btnCapMinus.FlatStyle = FlatStyle.Flat;
        _btnCapMinus.Font = new Font("Segoe UI", 8F);
        _btnCapMinus.Location = new Point(442, 92);
        _btnCapMinus.Margin = new Padding(14, 4, 14, 4);
        _btnCapMinus.Name = "_btnCapMinus";
        _btnCapMinus.Size = new Size(186, 21);
        _btnCapMinus.TabIndex = 18;
        _btnCapMinus.Text = "Захватить";
        _btnCapMinus.UseVisualStyleBackColor = false;
        _btnCapMinus.Click += BtnCapMinus_Click;
        //
        // _dgvDynCalib
        //
        _dgvDynCalib.AllowUserToAddRows = false;
        _dgvDynCalib.AllowUserToDeleteRows = false;
        _dgvDynCalib.AllowUserToResizeRows = false;
        _dgvDynCalib.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dgvDynCalib.BackgroundColor = Color.FromArgb(245, 245, 247);
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = Color.FromArgb(0, 255, 255);
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        dataGridViewCellStyle3.ForeColor = Color.FromArgb(35, 49, 63);
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(0, 255, 255);
        dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(35, 49, 63);
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
        _dgvDynCalib.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
        _dgvDynCalib.ColumnHeadersHeight = 34;
        _dgvDynCalib.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _dgvDynCalib.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8 });
        _dgvDynCalib.Dock = DockStyle.Fill;
        _dgvDynCalib.EditMode = DataGridViewEditMode.EditProgrammatically;
        _dgvDynCalib.EnableHeadersVisualStyles = false;
        _dgvDynCalib.Font = new Font("Segoe UI", 12F);
        _dgvDynCalib.GridColor = Color.FromArgb(212, 216, 222);
        _dgvDynCalib.Location = new Point(657, 3);
        _dgvDynCalib.MultiSelect = false;
        _dgvDynCalib.Name = "_dgvDynCalib";
        _dgvDynCalib.ReadOnly = true;
        _dgvDynCalib.RowHeadersVisible = false;
        _dgvDynCalib.RowHeadersWidth = 62;
        _dgvDynCalib.RowTemplate.Height = 28;
        _dgvDynCalib.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvDynCalib.Size = new Size(678, 377);
        _dgvDynCalib.TabIndex = 24;
        //
        // dataGridViewTextBoxColumn4
        //
        dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dataGridViewTextBoxColumn4.FillWeight = 18F;
        dataGridViewTextBoxColumn4.HeaderText = "Акт.";
        dataGridViewTextBoxColumn4.MinimumWidth = 6;
        dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
        dataGridViewTextBoxColumn4.ReadOnly = true;
        //
        // dataGridViewTextBoxColumn5
        //
        dataGridViewTextBoxColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dataGridViewTextBoxColumn5.FillWeight = 22F;
        dataGridViewTextBoxColumn5.HeaderText = "K→";
        dataGridViewTextBoxColumn5.MinimumWidth = 6;
        dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
        dataGridViewTextBoxColumn5.ReadOnly = true;
        //
        // dataGridViewTextBoxColumn6
        //
        dataGridViewTextBoxColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dataGridViewTextBoxColumn6.FillWeight = 22F;
        dataGridViewTextBoxColumn6.HeaderText = "K←";
        dataGridViewTextBoxColumn6.MinimumWidth = 6;
        dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
        dataGridViewTextBoxColumn6.ReadOnly = true;
        //
        // dataGridViewTextBoxColumn7
        //
        dataGridViewTextBoxColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dataGridViewTextBoxColumn7.FillWeight = 19F;
        dataGridViewTextBoxColumn7.HeaderText = "Создан";
        dataGridViewTextBoxColumn7.MinimumWidth = 6;
        dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
        dataGridViewTextBoxColumn7.ReadOnly = true;
        //
        // dataGridViewTextBoxColumn8
        //
        dataGridViewTextBoxColumn8.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dataGridViewTextBoxColumn8.FillWeight = 19F;
        dataGridViewTextBoxColumn8.HeaderText = "Снят";
        dataGridViewTextBoxColumn8.MinimumWidth = 6;
        dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
        dataGridViewTextBoxColumn8.ReadOnly = true;
        //
        // _pnlCalibDBottom
        //
        _pnlCalibDBottom.BorderStyle = BorderStyle.FixedSingle;
        _pnlCalibDBottom.Controls.Add(_lblFormulaD);
        _pnlCalibDBottom.Controls.Add(_btnCalibDynSave);
        _pnlCalibDBottom.Dock = DockStyle.Bottom;
        _pnlCalibDBottom.Location = new Point(0, 460);
        _pnlCalibDBottom.Name = "_pnlCalibDBottom";
        _pnlCalibDBottom.Padding = new Padding(16, 9, 16, 9);
        _pnlCalibDBottom.Size = new Size(1340, 54);
        _pnlCalibDBottom.TabIndex = 2;
        //
        // _lblFormulaD
        //
        _lblFormulaD.AutoSize = true;
        _lblFormulaD.Font = new Font("Segoe UI", 12F);
        _lblFormulaD.Location = new Point(16, 15);
        _lblFormulaD.Name = "_lblFormulaD";
        _lblFormulaD.Size = new Size(165, 21);
        _lblFormulaD.TabIndex = 22;
        _lblFormulaD.Text = "Масса = K × Код АЦП";
        //
        // _btnCalibDynSave
        //
        _btnCalibDynSave.FlatAppearance.BorderSize = 0;
        _btnCalibDynSave.FlatStyle = FlatStyle.Flat;
        _btnCalibDynSave.Font = new Font("Segoe UI", 12F);
        _btnCalibDynSave.Location = new Point(248, 10);
        _btnCalibDynSave.Name = "_btnCalibDynSave";
        _btnCalibDynSave.Size = new Size(220, 34);
        _btnCalibDynSave.TabIndex = 23;
        _btnCalibDynSave.Text = "Применить и сохранить";
        _btnCalibDynSave.UseVisualStyleBackColor = false;
        _btnCalibDynSave.Click += BtnCalibDynSave_Click;
        //
        // _pnlCalibDHead
        //
        _pnlCalibDHead.BackColor = Color.FromArgb(247, 249, 252);
        _pnlCalibDHead.BorderStyle = BorderStyle.FixedSingle;
        _pnlCalibDHead.Controls.Add(_tlpHeaders);
        _pnlCalibDHead.Dock = DockStyle.Top;
        _pnlCalibDHead.Location = new Point(0, 0);
        _pnlCalibDHead.Name = "_pnlCalibDHead";
        _pnlCalibDHead.Size = new Size(1340, 75);
        _pnlCalibDHead.TabIndex = 0;
        //
        // _tlpHeaders
        //
        _tlpHeaders.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        _tlpHeaders.ColumnCount = 4;
        _tlpHeaders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.1232872F));
        _tlpHeaders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.0851936F));
        _tlpHeaders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.4077759F));
        _tlpHeaders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.2200165F));
        _tlpHeaders.Controls.Add(_lblLiveWeightD, 3, 1);
        _tlpHeaders.Controls.Add(_lblLiveAdcD, 3, 0);
        _tlpHeaders.Controls.Add(_lblLiveAdcCapD, 2, 0);
        _tlpHeaders.Controls.Add(_btnDynamicCalibPortRefresh, 1, 1);
        _tlpHeaders.Controls.Add(_btnDynamicCalibConn, 0, 1);
        _tlpHeaders.Controls.Add(_lblDynamicCalibConn, 1, 0);
        _tlpHeaders.Controls.Add(_lblLiveWeightCapD, 2, 1);
        _tlpHeaders.Controls.Add(_cmbDynamicCalibPort, 0, 0);
        _tlpHeaders.Dock = DockStyle.Fill;
        _tlpHeaders.Location = new Point(0, 0);
        _tlpHeaders.Margin = new Padding(0);
        _tlpHeaders.Name = "_tlpHeaders";
        _tlpHeaders.RowCount = 2;
        _tlpHeaders.RowStyles.Add(new RowStyle(SizeType.Percent, 48.61111F));
        _tlpHeaders.RowStyles.Add(new RowStyle(SizeType.Percent, 51.38889F));
        _tlpHeaders.Size = new Size(1338, 73);
        _tlpHeaders.TabIndex = 9;
        //
        // _lblLiveWeightD
        //
        _lblLiveWeightD.BackColor = Color.Transparent;
        _lblLiveWeightD.Dock = DockStyle.Fill;
        _lblLiveWeightD.Font = new Font("Courier New", 13F, FontStyle.Bold);
        _lblLiveWeightD.ForeColor = Color.FromArgb(192, 0, 192);
        _lblLiveWeightD.Location = new Point(988, 36);
        _lblLiveWeightD.Name = "_lblLiveWeightD";
        _lblLiveWeightD.Size = new Size(346, 36);
        _lblLiveWeightD.TabIndex = 3;
        _lblLiveWeightD.Text = "—";
        _lblLiveWeightD.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _lblLiveAdcD
        //
        _lblLiveAdcD.AutoSize = true;
        _lblLiveAdcD.BackColor = Color.Transparent;
        _lblLiveAdcD.Dock = DockStyle.Fill;
        _lblLiveAdcD.Font = new Font("Courier New", 13F, FontStyle.Bold);
        _lblLiveAdcD.ForeColor = Color.Fuchsia;
        _lblLiveAdcD.Location = new Point(988, 1);
        _lblLiveAdcD.Name = "_lblLiveAdcD";
        _lblLiveAdcD.Size = new Size(346, 34);
        _lblLiveAdcD.TabIndex = 1;
        _lblLiveAdcD.Text = "—";
        _lblLiveAdcD.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _lblLiveAdcCapD
        //
        _lblLiveAdcCapD.AutoSize = true;
        _lblLiveAdcCapD.BackColor = Color.Transparent;
        _lblLiveAdcCapD.Dock = DockStyle.Fill;
        _lblLiveAdcCapD.Font = new Font("Segoe UI", 12F);
        _lblLiveAdcCapD.ForeColor = Color.FromArgb(46, 58, 70);
        _lblLiveAdcCapD.Location = new Point(675, 1);
        _lblLiveAdcCapD.Name = "_lblLiveAdcCapD";
        _lblLiveAdcCapD.Size = new Size(306, 34);
        _lblLiveAdcCapD.TabIndex = 0;
        _lblLiveAdcCapD.Text = "Текущий код АЦП";
        _lblLiveAdcCapD.TextAlign = ContentAlignment.MiddleRight;
        //
        // _btnDynamicCalibPortRefresh
        //
        _btnDynamicCalibPortRefresh.Anchor = AnchorStyles.Left;
        _btnDynamicCalibPortRefresh.FlatAppearance.BorderSize = 0;
        _btnDynamicCalibPortRefresh.FlatStyle = FlatStyle.Flat;
        _btnDynamicCalibPortRefresh.Font = new Font("Segoe UI", 11F);
        _btnDynamicCalibPortRefresh.Location = new Point(246, 39);
        _btnDynamicCalibPortRefresh.Margin = new Padding(16, 2, 3, 2);
        _btnDynamicCalibPortRefresh.Name = "_btnDynamicCalibPortRefresh";
        _btnDynamicCalibPortRefresh.Size = new Size(120, 30);
        _btnDynamicCalibPortRefresh.TabIndex = 7;
        _btnDynamicCalibPortRefresh.Text = "↺ Обновить";
        _btnDynamicCalibPortRefresh.TextAlign = ContentAlignment.MiddleCenter;
        _btnDynamicCalibPortRefresh.UseVisualStyleBackColor = false;
        _btnDynamicCalibPortRefresh.Click += BtnDynamicPortRefresh_Click;
        //
        // _btnDynamicCalibConn
        //
        _btnDynamicCalibConn.Dock = DockStyle.Fill;
        _btnDynamicCalibConn.FlatAppearance.BorderSize = 0;
        _btnDynamicCalibConn.FlatStyle = FlatStyle.Flat;
        _btnDynamicCalibConn.Font = new Font("Segoe UI", 12F);
        _btnDynamicCalibConn.Location = new Point(4, 38);
        _btnDynamicCalibConn.Margin = new Padding(3, 2, 3, 2);
        _btnDynamicCalibConn.Name = "_btnDynamicCalibConn";
        _btnDynamicCalibConn.Size = new Size(222, 32);
        _btnDynamicCalibConn.TabIndex = 6;
        _btnDynamicCalibConn.Text = "Подключить";
        _btnDynamicCalibConn.UseVisualStyleBackColor = false;
        _btnDynamicCalibConn.Click += BtnDynamicCalibConn_Click;
        //
        // _lblDynamicCalibConn
        //
        _lblDynamicCalibConn.AutoSize = true;
        _lblDynamicCalibConn.BackColor = Color.Transparent;
        _lblDynamicCalibConn.Dock = DockStyle.Fill;
        _lblDynamicCalibConn.Font = new Font("Segoe UI", 12F);
        _lblDynamicCalibConn.ForeColor = Color.FromArgb(46, 58, 70);
        _lblDynamicCalibConn.Location = new Point(233, 1);
        _lblDynamicCalibConn.Name = "_lblDynamicCalibConn";
        _lblDynamicCalibConn.Size = new Size(435, 34);
        _lblDynamicCalibConn.TabIndex = 8;
        _lblDynamicCalibConn.Text = "Динамика: нет подключения";
        _lblDynamicCalibConn.TextAlign = ContentAlignment.MiddleCenter;
        //
        // _lblLiveWeightCapD
        //
        _lblLiveWeightCapD.AutoSize = true;
        _lblLiveWeightCapD.BackColor = Color.Transparent;
        _lblLiveWeightCapD.Dock = DockStyle.Fill;
        _lblLiveWeightCapD.Font = new Font("Segoe UI", 12F);
        _lblLiveWeightCapD.ForeColor = Color.FromArgb(46, 58, 70);
        _lblLiveWeightCapD.Location = new Point(675, 36);
        _lblLiveWeightCapD.Name = "_lblLiveWeightCapD";
        _lblLiveWeightCapD.Size = new Size(306, 36);
        _lblLiveWeightCapD.TabIndex = 2;
        _lblLiveWeightCapD.Text = "Текущая масса";
        _lblLiveWeightCapD.TextAlign = ContentAlignment.MiddleRight;
        //
        // _cmbDynamicCalibPort
        //
        _cmbDynamicCalibPort.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cmbDynamicCalibPort.BackColor = Color.FromArgb(255, 255, 255);
        _cmbDynamicCalibPort.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbDynamicCalibPort.Font = new Font("Segoe UI", 10F);
        _cmbDynamicCalibPort.ForeColor = Color.FromArgb(35, 49, 63);
        _cmbDynamicCalibPort.Location = new Point(5, 3);
        _cmbDynamicCalibPort.Margin = new Padding(4, 0, 4, 3);
        _cmbDynamicCalibPort.Name = "_cmbDynamicCalibPort";
        _cmbDynamicCalibPort.Size = new Size(220, 25);
        _cmbDynamicCalibPort.TabIndex = 4;
        //
        // _tabSett
        //
        _tabSett.Controls.Add(_lblPortCap);
        _tabSett.Controls.Add(_cmbSettPort);
        _tabSett.Controls.Add(_lblNpvCap);
        _tabSett.Controls.Add(_txtNpv);
        _tabSett.Controls.Add(_lblDiscCap);
        _tabSett.Controls.Add(_cmbDisc);
        _tabSett.Controls.Add(_lblZeroCap);
        _tabSett.Controls.Add(_txtZeroLimit);
        _tabSett.Controls.Add(_lblPasswordCap);
        _tabSett.Controls.Add(_txtNewPassword);
        _tabSett.Controls.Add(_btnSaveSettings);
        _tabSett.Location = new Point(4, 26);
        _tabSett.Name = "_tabSett";
        _tabSett.Size = new Size(1340, 514);
        _tabSett.TabIndex = 5;
        _tabSett.Text = "Настройки";
        //
        // _lblPortCap
        //
        _lblPortCap.AutoSize = true;
        _lblPortCap.Font = new Font("Segoe UI", 10F);
        _lblPortCap.Location = new Point(20, 20);
        _lblPortCap.Name = "_lblPortCap";
        _lblPortCap.Size = new Size(81, 19);
        _lblPortCap.TabIndex = 0;
        _lblPortCap.Text = "COM-порт:";
        //
        // _cmbSettPort
        //
        _cmbSettPort.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbSettPort.Font = new Font("Segoe UI", 12F);
        _cmbSettPort.Location = new Point(220, 16);
        _cmbSettPort.Name = "_cmbSettPort";
        _cmbSettPort.Size = new Size(200, 29);
        _cmbSettPort.TabIndex = 1;
        //
        // _lblNpvCap
        //
        _lblNpvCap.AutoSize = true;
        _lblNpvCap.Font = new Font("Segoe UI", 10F);
        _lblNpvCap.Location = new Point(20, 56);
        _lblNpvCap.Name = "_lblNpvCap";
        _lblNpvCap.Size = new Size(58, 19);
        _lblNpvCap.TabIndex = 2;
        _lblNpvCap.Text = "НПВ (т):";
        //
        // _txtNpv
        //
        _txtNpv.Font = new Font("Segoe UI", 12F);
        _txtNpv.Location = new Point(220, 52);
        _txtNpv.Name = "_txtNpv";
        _txtNpv.Size = new Size(200, 29);
        _txtNpv.TabIndex = 3;
        _txtNpv.Text = "150";
        //
        // _lblDiscCap
        //
        _lblDiscCap.AutoSize = true;
        _lblDiscCap.Font = new Font("Segoe UI", 10F);
        _lblDiscCap.Location = new Point(20, 92);
        _lblDiscCap.Name = "_lblDiscCap";
        _lblDiscCap.Size = new Size(99, 19);
        _lblDiscCap.TabIndex = 4;
        _lblDiscCap.Text = "Дискретность:";
        //
        // _cmbDisc
        //
        _cmbDisc.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbDisc.Font = new Font("Segoe UI", 12F);
        _cmbDisc.Items.AddRange(new object[] { "0.1 т", "0.05 т", "0.01 т" });
        _cmbDisc.Location = new Point(220, 88);
        _cmbDisc.Name = "_cmbDisc";
        _cmbDisc.Size = new Size(200, 29);
        _cmbDisc.TabIndex = 5;
        //
        // _lblZeroCap
        //
        _lblZeroCap.AutoSize = true;
        _lblZeroCap.Font = new Font("Segoe UI", 10F);
        _lblZeroCap.Location = new Point(20, 128);
        _lblZeroCap.Name = "_lblZeroCap";
        _lblZeroCap.Size = new Size(141, 19);
        _lblZeroCap.TabIndex = 6;
        _lblZeroCap.Text = "Лимит нуля (% НПВ):";
        //
        // _txtZeroLimit
        //
        _txtZeroLimit.Font = new Font("Segoe UI", 12F);
        _txtZeroLimit.Location = new Point(220, 124);
        _txtZeroLimit.Name = "_txtZeroLimit";
        _txtZeroLimit.Size = new Size(200, 29);
        _txtZeroLimit.TabIndex = 7;
        _txtZeroLimit.Text = "2";
        //
        // _lblPasswordCap
        //
        _lblPasswordCap.AutoSize = true;
        _lblPasswordCap.Font = new Font("Segoe UI", 10F);
        _lblPasswordCap.Location = new Point(20, 164);
        _lblPasswordCap.Name = "_lblPasswordCap";
        _lblPasswordCap.Size = new Size(104, 19);
        _lblPasswordCap.TabIndex = 8;
        _lblPasswordCap.Text = "Новый пароль:";
        //
        // _txtNewPassword
        //
        _txtNewPassword.Font = new Font("Segoe UI", 12F);
        _txtNewPassword.Location = new Point(220, 160);
        _txtNewPassword.Name = "_txtNewPassword";
        _txtNewPassword.Size = new Size(200, 29);
        _txtNewPassword.TabIndex = 9;
        _txtNewPassword.UseSystemPasswordChar = true;
        //
        // _btnSaveSettings
        //
        _btnSaveSettings.FlatStyle = FlatStyle.Flat;
        _btnSaveSettings.Font = new Font("Segoe UI", 10F);
        _btnSaveSettings.Location = new Point(20, 204);
        _btnSaveSettings.Name = "_btnSaveSettings";
        _btnSaveSettings.Size = new Size(130, 34);
        _btnSaveSettings.TabIndex = 10;
        _btnSaveSettings.Text = "Сохранить";
        _btnSaveSettings.UseVisualStyleBackColor = false;
        _btnSaveSettings.Click += BtnSaveSettings_Click;
        //
        // _rateTimer
        //
        _rateTimer.Tick += RateTimer_Tick;
        //
        // ServiceForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(255, 192, 255);
        ClientSize = new Size(1348, 598);
        Controls.Add(_btnAdmin);
        Controls.Add(_tabs);
        MinimumSize = new Size(638, 471);
        Name = "ServiceForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Сервисный режим";
        _tabs.ResumeLayout(false);
        _tabChannel.ResumeLayout(false);
        _tabChannel.PerformLayout();
        _tabMonitor.ResumeLayout(false);
        _tabMonitor.PerformLayout();
        _pnlCh0.ResumeLayout(false);
        _pnlCh0.PerformLayout();
        _pnlCh1.ResumeLayout(false);
        _pnlCh1.PerformLayout();
        _tabDynamicService.ResumeLayout(false);
        _tabDynamicService.PerformLayout();
        _pnlDynamicCh0.ResumeLayout(false);
        _pnlDynamicCh0.PerformLayout();
        _pnlDynamicCh1.ResumeLayout(false);
        _pnlDynamicCh1.PerformLayout();
        _tabCalibS.ResumeLayout(false);
        _pnlCalibS.ResumeLayout(false);
        _pnlCalibSBody.ResumeLayout(false);
        _pnlCalibSBody.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvCalib).EndInit();
        _pnlCalibSHead.ResumeLayout(false);
        _dotStaticCalibConn.ResumeLayout(false);
        tlpCalibSHead.ResumeLayout(false);
        tlpCalibSHead.PerformLayout();
        _tabCalibD.ResumeLayout(false);
        _pnlCalibD.ResumeLayout(false);
        _pnlCalibDBody.ResumeLayout(false);
        pnlCalibDMain.ResumeLayout(false);
        tlpCalibDMain.ResumeLayout(false);
        tlpDirections.ResumeLayout(false);
        tlpCalibDPlus.ResumeLayout(false);
        tlpCalibDPlus.PerformLayout();
        tlpCalibDMinus.ResumeLayout(false);
        tlpCalibDMinus.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvDynCalib).EndInit();
        _pnlCalibDBottom.ResumeLayout(false);
        _pnlCalibDBottom.PerformLayout();
        _pnlCalibDHead.ResumeLayout(false);
        _tlpHeaders.ResumeLayout(false);
        _tlpHeaders.PerformLayout();
        _tabSett.ResumeLayout(false);
        _tabSett.PerformLayout();
        ResumeLayout(false);
    }

    private Button      _btnAdmin;
    private TabControl  _tabs;
    private TabPage     _tabChannel;
    private TabPage     _tabMonitor;
    private TabPage     _tabDynamicService;
    private TabPage     _tabCalibS;
    private Panel       _pnlCalibS;
    private Panel       _pnlCalibSHead;
    private Panel       _pnlCalibSBody;
    private TabPage     _tabCalibD;
    private Panel       _pnlCalibD;
    private Panel       _pnlCalibDHead;
    private Panel       _pnlCalibDBody;
    private Panel       _pnlCalibDBottom;
    private TabPage     _tabSett;

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

    private ComboBox    _cmbStaticCalibPort;
    private Panel       _dotStaticCalibConn;
    private Button      _btnStaticCalibConn;
    private Button      _btnStaticCalibPortRefresh;
    private Label       _lblStaticCalibConn;

    private ComboBox    _cmbDynamicPort;
    private Panel       _dotDynamicConn;
    private Button      _btnDynamicConn;
    private Button      _btnDynamicPortRefresh;
    private Label       _lblDynamicConn;
    private Label       _lblDynamicRate;
    private Panel       _pnlDynamicCh0;
    private Label       _lblDynamicCh0Cap;
    private Label       _lblDynamicCh0;
    private Panel       _pnlDynamicCh1;
    private Label       _lblDynamicCh1Cap;
    private Label       _lblDynamicCh1;
    private CheckBox    _chkDynamicLog;
    private Button      _btnDynamicClearLog;
    private RichTextBox _rtbDynamicLog;

    private Label       _lblLiveWeightCapD;
    private Label       _lblLiveWeightD;
    private ComboBox    _cmbDynamicCalibPort;
    private Button      _btnDynamicCalibConn;
    private Button      _btnDynamicCalibPortRefresh;
    private Label       _lblDynamicCalibConn;

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
    private Label   _lblSecPlus_00;
    private Label   _lblKPlusEquals;
    private Label   _lblAutoCalcPlus;
    private Label   _lblCodePlusCap;
    private TextBox _txtCodePlus;
    private Button  _btnCapPlus;
    private Label   _lblMassPlusCap;
    private TextBox _txtMassPlus;
    private Button  _btnCalcPlus;
    private Label   _lblSecMinus_00;
    private Label   _lblKMinusEquals;
    private Label   _lblAutoCalcMinus;
    private Label   _lblCodeMinusCap;
    private TextBox _txtCodeMinus;
    private Button  _btnCapMinus;
    private Label   _lblMassMinusCap;
    private TextBox _txtMassMinus;
    private Button  _btnCalcMinus;
    private Button  _btnCalibDynSave;
    private TextBox _txtKPlus;
    private TextBox _txtKMinus;
    private Label   _lblFormulaD;
    private DataGridView _dgvDynCalib;

    private Label    _lblPortCap;
    private ComboBox _cmbSettPort;
    private Label    _lblNpvCap;
    private TextBox  _txtNpv;
    private Label    _lblDiscCap;
    private ComboBox _cmbDisc;
    private Label    _lblZeroCap;
    private TextBox  _txtZeroLimit;
    private Label    _lblPasswordCap;
    private TextBox  _txtNewPassword;
    private Button   _btnSaveSettings;

    private System.Windows.Forms.Timer _rateTimer;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private TableLayoutPanel tlpCalibSHead;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    private TableLayoutPanel _tlpHeaders;
    private TableLayoutPanel tlpCalibDMain;
    private Panel pnlCalibDMain;
    private TableLayoutPanel tlpDirections;
    private TableLayoutPanel tlpCalibDPlus;
    private TableLayoutPanel tlpCalibDMinus;
    private Label _lblSecPlus_02;
    private Label _lblSecPlus_01;
    private Label _lblSecMinus_01;
    private Label _lblSecMinus_02;
}
