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
        DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
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
        tableLayoutPanel1 = new TableLayoutPanel();
        _rbCh1Calib = new RadioButton();
        _lblLiveAdc = new Label();
        _lblLiveAdcCap = new Label();
        _rbCh0Calib = new RadioButton();
        _btnCapture = new Button();
        _tabCalibD = new TabPage();
        _pnlCalibD = new Panel();
        _pnlCalibDBody = new Panel();
        _lblSecPlus_02 = new Label();
        _lblSecPlus_01 = new Label();
        panel1 = new Panel();
        tableLayoutPanel2 = new TableLayoutPanel();
        _dgvDynCalib = new DataGridView();
        dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
        tlpDirections = new TableLayoutPanel();
        tableLayoutPanel3 = new TableLayoutPanel();
        _txtKPlus = new TextBox();
        _lblKPlusEquals = new Label();
        _btnCalcPlus = new Button();
        _lblMassPlusCap = new Label();
        _btnCapPlus = new Button();
        _txtMassPlus = new TextBox();
        _txtCodePlus = new TextBox();
        _lblCodePlusCap = new Label();
        _lblAutoCalcPlus = new Label();
        tableLayoutPanel4 = new TableLayoutPanel();
        _txtKMinus = new TextBox();
        _lblKMinusEquals = new Label();
        _lblAutoCalcMinus = new Label();
        _btnCalcMinus = new Button();
        _lblMassMinusCap = new Label();
        _lblCodeMinusCap = new Label();
        _txtMassMinus = new TextBox();
        _txtCodeMinus = new TextBox();
        _btnCapMinus = new Button();
        _lblSecPlus_00 = new Label();
        _lblSecMinus_00 = new Label();
        _pnlCalibDBottom = new Panel();
        _lblFormulaD = new Label();
        _btnCalibDynSave = new Button();
        _pnlCalibDHead = new Panel();
        _tlpHeaders = new TableLayoutPanel();
        _lblLiveWeightD = new Label();
        _lblLiveAdcD = new Label();
        _cmbDynamicCalibPort = new ComboBox();
        _lblLiveAdcCapD = new Label();
        _btnDynamicCalibPortRefresh = new Button();
        _btnDynamicCalibConn = new Button();
        _lblDynamicCalibConn = new Label();
        _lblLiveWeightCapD = new Label();
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
        _lblSecMinus_02 = new Label();
        _lblSecMinus_01 = new Label();
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
        tableLayoutPanel1.SuspendLayout();
        _tabCalibD.SuspendLayout();
        _pnlCalibD.SuspendLayout();
        _pnlCalibDBody.SuspendLayout();
        panel1.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvDynCalib).BeginInit();
        tlpDirections.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
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
        _btnAdmin.Location = new Point(1221, 11);
        _btnAdmin.Margin = new Padding(3, 4, 3, 4);
        _btnAdmin.Name = "_btnAdmin";
        _btnAdmin.Size = new Size(309, 37);
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
        _tabs.Location = new Point(0, 59);
        _tabs.Margin = new Padding(3, 4, 3, 4);
        _tabs.Name = "_tabs";
        _tabs.SelectedIndex = 0;
        _tabs.Size = new Size(1541, 726);
        _tabs.TabIndex = 1;
        //
        // _tabChannel
        //
        _tabChannel.Controls.Add(_lblChannelTitle);
        _tabChannel.Controls.Add(_rbMain);
        _tabChannel.Controls.Add(_rbBackup);
        _tabChannel.Controls.Add(_lblChannelNote);
        _tabChannel.Location = new Point(4, 32);
        _tabChannel.Margin = new Padding(3, 4, 3, 4);
        _tabChannel.Name = "_tabChannel";
        _tabChannel.Size = new Size(1533, 690);
        _tabChannel.TabIndex = 0;
        _tabChannel.Text = "Канал";
        //
        // _lblChannelTitle
        //
        _lblChannelTitle.AutoSize = true;
        _lblChannelTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        _lblChannelTitle.Location = new Point(23, 21);
        _lblChannelTitle.Name = "_lblChannelTitle";
        _lblChannelTitle.Size = new Size(246, 25);
        _lblChannelTitle.TabIndex = 0;
        _lblChannelTitle.Text = "Выбор активного канала";
        //
        // _rbMain
        //
        _rbMain.AutoSize = true;
        _rbMain.Checked = true;
        _rbMain.Font = new Font("Segoe UI", 13F);
        _rbMain.Location = new Point(23, 69);
        _rbMain.Margin = new Padding(3, 4, 3, 4);
        _rbMain.Name = "_rbMain";
        _rbMain.Size = new Size(271, 34);
        _rbMain.TabIndex = 1;
        _rbMain.TabStop = true;
        _rbMain.Text = "Канал: Основной (CH0)";
        _rbMain.CheckedChanged += RbMain_CheckedChanged;
        //
        // _rbBackup
        //
        _rbBackup.AutoSize = true;
        _rbBackup.Font = new Font("Segoe UI", 13F);
        _rbBackup.Location = new Point(23, 120);
        _rbBackup.Margin = new Padding(3, 4, 3, 4);
        _rbBackup.Name = "_rbBackup";
        _rbBackup.Size = new Size(280, 34);
        _rbBackup.TabIndex = 2;
        _rbBackup.Text = "Канал: Резервный (CH1)";
        _rbBackup.CheckedChanged += RbBackup_CheckedChanged;
        //
        // _lblChannelNote
        //
        _lblChannelNote.AutoSize = true;
        _lblChannelNote.Font = new Font("Segoe UI", 12F);
        _lblChannelNote.Location = new Point(23, 181);
        _lblChannelNote.Name = "_lblChannelNote";
        _lblChannelNote.Size = new Size(622, 28);
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
        _tabMonitor.Location = new Point(4, 32);
        _tabMonitor.Margin = new Padding(3, 4, 3, 4);
        _tabMonitor.Name = "_tabMonitor";
        _tabMonitor.Size = new Size(1533, 690);
        _tabMonitor.TabIndex = 1;
        _tabMonitor.Text = "Сервисный режим Статика";
        //
        // _cmbPort
        //
        _cmbPort.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbPort.Font = new Font("Segoe UI", 10F);
        _cmbPort.Location = new Point(11, 16);
        _cmbPort.Margin = new Padding(3, 4, 3, 4);
        _cmbPort.Name = "_cmbPort";
        _cmbPort.Size = new Size(102, 31);
        _cmbPort.TabIndex = 0;
        //
        // _dotConn
        //
        _dotConn.Location = new Point(123, 21);
        _dotConn.Margin = new Padding(3, 4, 3, 4);
        _dotConn.Name = "_dotConn";
        _dotConn.Size = new Size(11, 13);
        _dotConn.TabIndex = 1;
        //
        // _btnConn
        //
        _btnConn.FlatAppearance.BorderSize = 0;
        _btnConn.FlatStyle = FlatStyle.Flat;
        _btnConn.Font = new Font("Segoe UI", 12F);
        _btnConn.Location = new Point(142, 11);
        _btnConn.Margin = new Padding(3, 4, 3, 4);
        _btnConn.Name = "_btnConn";
        _btnConn.Size = new Size(147, 37);
        _btnConn.TabIndex = 2;
        _btnConn.Text = "Подключить";
        _btnConn.UseVisualStyleBackColor = false;
        _btnConn.Click += BtnMonConn_Click;
        //
        // _btnPortRefresh
        //
        _btnPortRefresh.FlatStyle = FlatStyle.Flat;
        _btnPortRefresh.Font = new Font("Segoe UI", 11F);
        _btnPortRefresh.Location = new Point(322, 11);
        _btnPortRefresh.Margin = new Padding(3, 4, 3, 4);
        _btnPortRefresh.Name = "_btnPortRefresh";
        _btnPortRefresh.Size = new Size(34, 37);
        _btnPortRefresh.TabIndex = 3;
        _btnPortRefresh.Text = "↺";
        _btnPortRefresh.UseVisualStyleBackColor = false;
        _btnPortRefresh.Click += BtnPortRefresh_Click;
        //
        // _lblConn
        //
        _lblConn.AutoSize = true;
        _lblConn.Font = new Font("Segoe UI", 12F);
        _lblConn.Location = new Point(461, 19);
        _lblConn.Name = "_lblConn";
        _lblConn.Size = new Size(176, 28);
        _lblConn.TabIndex = 4;
        _lblConn.Text = "Нет подключения";
        //
        // _lblRate
        //
        _lblRate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblRate.AutoSize = true;
        _lblRate.Font = new Font("Segoe UI", 12F);
        _lblRate.Location = new Point(1023, 19);
        _lblRate.Name = "_lblRate";
        _lblRate.Size = new Size(80, 28);
        _lblRate.TabIndex = 5;
        _lblRate.Text = "— фр/с";
        //
        // _pnlCh0
        //
        _pnlCh0.Controls.Add(_lblCh0Cap);
        _pnlCh0.Controls.Add(_lblCh0);
        _pnlCh0.Location = new Point(11, 64);
        _pnlCh0.Margin = new Padding(3, 4, 3, 4);
        _pnlCh0.Name = "_pnlCh0";
        _pnlCh0.Size = new Size(389, 192);
        _pnlCh0.TabIndex = 6;
        //
        // _lblCh0Cap
        //
        _lblCh0Cap.AutoSize = true;
        _lblCh0Cap.Font = new Font("Segoe UI", 12F);
        _lblCh0Cap.Location = new Point(9, 8);
        _lblCh0Cap.Name = "_lblCh0Cap";
        _lblCh0Cap.Size = new Size(225, 28);
        _lblCh0Cap.TabIndex = 0;
        _lblCh0Cap.Text = "Канал: Основной (CH0)";
        //
        // _lblCh0
        //
        _lblCh0.Font = new Font("Courier New", 48F, FontStyle.Bold);
        _lblCh0.Location = new Point(9, 37);
        _lblCh0.Name = "_lblCh0";
        _lblCh0.Size = new Size(370, 120);
        _lblCh0.TabIndex = 1;
        _lblCh0.Text = "—";
        _lblCh0.TextAlign = ContentAlignment.MiddleRight;
        //
        // _pnlCh1
        //
        _pnlCh1.Controls.Add(_lblCh1Cap);
        _pnlCh1.Controls.Add(_lblCh1);
        _pnlCh1.Location = new Point(411, 64);
        _pnlCh1.Margin = new Padding(3, 4, 3, 4);
        _pnlCh1.Name = "_pnlCh1";
        _pnlCh1.Size = new Size(389, 192);
        _pnlCh1.TabIndex = 7;
        //
        // _lblCh1Cap
        //
        _lblCh1Cap.AutoSize = true;
        _lblCh1Cap.Font = new Font("Segoe UI", 12F);
        _lblCh1Cap.Location = new Point(9, 8);
        _lblCh1Cap.Name = "_lblCh1Cap";
        _lblCh1Cap.Size = new Size(231, 28);
        _lblCh1Cap.TabIndex = 0;
        _lblCh1Cap.Text = "Канал: Резервный (CH1)";
        //
        // _lblCh1
        //
        _lblCh1.Font = new Font("Courier New", 48F, FontStyle.Bold);
        _lblCh1.Location = new Point(9, 37);
        _lblCh1.Name = "_lblCh1";
        _lblCh1.Size = new Size(370, 120);
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
        _chkLog.Location = new Point(11, 264);
        _chkLog.Margin = new Padding(3, 4, 3, 4);
        _chkLog.Name = "_chkLog";
        _chkLog.Size = new Size(145, 32);
        _chkLog.TabIndex = 8;
        _chkLog.Text = "Лог активен";
        //
        // _btnClearLog
        //
        _btnClearLog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnClearLog.FlatStyle = FlatStyle.Flat;
        _btnClearLog.Font = new Font("Segoe UI", 12F);
        _btnClearLog.Location = new Point(1066, 264);
        _btnClearLog.Margin = new Padding(3, 4, 3, 4);
        _btnClearLog.Name = "_btnClearLog";
        _btnClearLog.Size = new Size(115, 43);
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
        _rtbLog.Location = new Point(11, 319);
        _rtbLog.Margin = new Padding(3, 4, 3, 4);
        _rtbLog.Name = "_rtbLog";
        _rtbLog.ReadOnly = true;
        _rtbLog.ScrollBars = RichTextBoxScrollBars.Vertical;
        _rtbLog.Size = new Size(1171, 354);
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
        _tabDynamicService.Location = new Point(4, 32);
        _tabDynamicService.Margin = new Padding(3, 4, 3, 4);
        _tabDynamicService.Name = "_tabDynamicService";
        _tabDynamicService.Size = new Size(1533, 690);
        _tabDynamicService.TabIndex = 2;
        _tabDynamicService.Text = "Сервисный режим Динамика";
        //
        // _cmbDynamicPort
        //
        _cmbDynamicPort.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbDynamicPort.Font = new Font("Segoe UI", 10F);
        _cmbDynamicPort.Location = new Point(20, 14);
        _cmbDynamicPort.Margin = new Padding(3, 4, 3, 4);
        _cmbDynamicPort.Name = "_cmbDynamicPort";
        _cmbDynamicPort.Size = new Size(140, 31);
        _cmbDynamicPort.TabIndex = 0;
        //
        // _dotDynamicConn
        //
        _dotDynamicConn.Location = new Point(180, 18);
        _dotDynamicConn.Margin = new Padding(3, 4, 3, 4);
        _dotDynamicConn.Name = "_dotDynamicConn";
        _dotDynamicConn.Size = new Size(16, 16);
        _dotDynamicConn.TabIndex = 1;
        //
        // _btnDynamicConn
        //
        _btnDynamicConn.FlatStyle = FlatStyle.Flat;
        _btnDynamicConn.Font = new Font("Segoe UI", 12F);
        _btnDynamicConn.Location = new Point(210, 10);
        _btnDynamicConn.Margin = new Padding(3, 4, 3, 4);
        _btnDynamicConn.Name = "_btnDynamicConn";
        _btnDynamicConn.Size = new Size(161, 40);
        _btnDynamicConn.TabIndex = 2;
        _btnDynamicConn.Text = "Подключить";
        _btnDynamicConn.UseVisualStyleBackColor = false;
        _btnDynamicConn.Click += BtnDynamicConn_Click;
        //
        // _btnDynamicPortRefresh
        //
        _btnDynamicPortRefresh.FlatStyle = FlatStyle.Flat;
        _btnDynamicPortRefresh.Font = new Font("Segoe UI", 11F);
        _btnDynamicPortRefresh.Location = new Point(377, 10);
        _btnDynamicPortRefresh.Margin = new Padding(3, 4, 3, 4);
        _btnDynamicPortRefresh.Name = "_btnDynamicPortRefresh";
        _btnDynamicPortRefresh.Size = new Size(42, 40);
        _btnDynamicPortRefresh.TabIndex = 3;
        _btnDynamicPortRefresh.Text = "↺";
        _btnDynamicPortRefresh.UseVisualStyleBackColor = false;
        _btnDynamicPortRefresh.Click += BtnDynamicPortRefresh_Click;
        //
        // _lblDynamicConn
        //
        _lblDynamicConn.Font = new Font("Segoe UI", 12F);
        _lblDynamicConn.Location = new Point(489, 10);
        _lblDynamicConn.Name = "_lblDynamicConn";
        _lblDynamicConn.Size = new Size(290, 23);
        _lblDynamicConn.TabIndex = 4;
        _lblDynamicConn.Text = "Нет подключения";
        //
        // _lblDynamicRate
        //
        _lblDynamicRate.Font = new Font("Segoe UI", 12F);
        _lblDynamicRate.Location = new Point(20, 52);
        _lblDynamicRate.Name = "_lblDynamicRate";
        _lblDynamicRate.Size = new Size(120, 24);
        _lblDynamicRate.TabIndex = 5;
        _lblDynamicRate.Text = "— сэмпл/с";
        //
        // _pnlDynamicCh0
        //
        _pnlDynamicCh0.Controls.Add(_lblDynamicCh0Cap);
        _pnlDynamicCh0.Controls.Add(_lblDynamicCh0);
        _pnlDynamicCh0.Location = new Point(20, 86);
        _pnlDynamicCh0.Margin = new Padding(3, 4, 3, 4);
        _pnlDynamicCh0.Name = "_pnlDynamicCh0";
        _pnlDynamicCh0.Size = new Size(340, 144);
        _pnlDynamicCh0.TabIndex = 6;
        //
        // _lblDynamicCh0Cap
        //
        _lblDynamicCh0Cap.AutoSize = true;
        _lblDynamicCh0Cap.Font = new Font("Segoe UI", 12F);
        _lblDynamicCh0Cap.Location = new Point(8, 6);
        _lblDynamicCh0Cap.Name = "_lblDynamicCh0Cap";
        _lblDynamicCh0Cap.Size = new Size(225, 28);
        _lblDynamicCh0Cap.TabIndex = 0;
        _lblDynamicCh0Cap.Text = "Канал: Основной (CH0)";
        //
        // _lblDynamicCh0
        //
        _lblDynamicCh0.Font = new Font("Courier New", 48F, FontStyle.Bold);
        _lblDynamicCh0.Location = new Point(8, 28);
        _lblDynamicCh0.Name = "_lblDynamicCh0";
        _lblDynamicCh0.Size = new Size(324, 90);
        _lblDynamicCh0.TabIndex = 1;
        _lblDynamicCh0.Text = "—";
        _lblDynamicCh0.TextAlign = ContentAlignment.MiddleRight;
        //
        // _pnlDynamicCh1
        //
        _pnlDynamicCh1.Controls.Add(_lblDynamicCh1Cap);
        _pnlDynamicCh1.Controls.Add(_lblDynamicCh1);
        _pnlDynamicCh1.Location = new Point(360, 86);
        _pnlDynamicCh1.Margin = new Padding(3, 4, 3, 4);
        _pnlDynamicCh1.Name = "_pnlDynamicCh1";
        _pnlDynamicCh1.Size = new Size(340, 144);
        _pnlDynamicCh1.TabIndex = 7;
        //
        // _lblDynamicCh1Cap
        //
        _lblDynamicCh1Cap.AutoSize = true;
        _lblDynamicCh1Cap.Font = new Font("Segoe UI", 12F);
        _lblDynamicCh1Cap.Location = new Point(8, 6);
        _lblDynamicCh1Cap.Name = "_lblDynamicCh1Cap";
        _lblDynamicCh1Cap.Size = new Size(231, 28);
        _lblDynamicCh1Cap.TabIndex = 0;
        _lblDynamicCh1Cap.Text = "Канал: Резервный (CH1)";
        //
        // _lblDynamicCh1
        //
        _lblDynamicCh1.Font = new Font("Courier New", 48F, FontStyle.Bold);
        _lblDynamicCh1.Location = new Point(8, 28);
        _lblDynamicCh1.Name = "_lblDynamicCh1";
        _lblDynamicCh1.Size = new Size(324, 90);
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
        _chkDynamicLog.Location = new Point(20, 198);
        _chkDynamicLog.Margin = new Padding(3, 4, 3, 4);
        _chkDynamicLog.Name = "_chkDynamicLog";
        _chkDynamicLog.Size = new Size(145, 32);
        _chkDynamicLog.TabIndex = 8;
        _chkDynamicLog.Text = "Лог активен";
        //
        // _btnDynamicClearLog
        //
        _btnDynamicClearLog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnDynamicClearLog.FlatStyle = FlatStyle.Flat;
        _btnDynamicClearLog.Font = new Font("Segoe UI", 12F);
        _btnDynamicClearLog.Location = new Point(864, 184);
        _btnDynamicClearLog.Margin = new Padding(3, 4, 3, 4);
        _btnDynamicClearLog.Name = "_btnDynamicClearLog";
        _btnDynamicClearLog.Size = new Size(136, 46);
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
        _rtbDynamicLog.Location = new Point(10, 239);
        _rtbDynamicLog.Margin = new Padding(3, 4, 3, 4);
        _rtbDynamicLog.Name = "_rtbDynamicLog";
        _rtbDynamicLog.ReadOnly = true;
        _rtbDynamicLog.ScrollBars = RichTextBoxScrollBars.Vertical;
        _rtbDynamicLog.Size = new Size(990, 450);
        _rtbDynamicLog.TabIndex = 10;
        _rtbDynamicLog.Text = "";
        _rtbDynamicLog.WordWrap = false;
        //
        // _tabCalibS
        //
        _tabCalibS.Controls.Add(_pnlCalibS);
        _tabCalibS.Location = new Point(4, 32);
        _tabCalibS.Margin = new Padding(3, 4, 3, 4);
        _tabCalibS.Name = "_tabCalibS";
        _tabCalibS.Size = new Size(1533, 690);
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
        _pnlCalibS.Margin = new Padding(3, 4, 3, 4);
        _pnlCalibS.Name = "_pnlCalibS";
        _pnlCalibS.Size = new Size(1533, 690);
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
        _pnlCalibSBody.Location = new Point(0, 96);
        _pnlCalibSBody.Margin = new Padding(3, 4, 3, 4);
        _pnlCalibSBody.Name = "_pnlCalibSBody";
        _pnlCalibSBody.Padding = new Padding(18, 16, 18, 16);
        _pnlCalibSBody.Size = new Size(1533, 594);
        _pnlCalibSBody.TabIndex = 1;
        //
        // _dgvCalib
        //
        _dgvCalib.AllowUserToAddRows = false;
        _dgvCalib.AllowUserToDeleteRows = false;
        _dgvCalib.AllowUserToResizeRows = false;
        _dgvCalib.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dgvCalib.BackgroundColor = Color.FromArgb(245, 245, 247);
        dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(196, 225, 230);
        dataGridViewCellStyle5.SelectionForeColor = Color.FromArgb(26, 26, 26);
        dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
        _dgvCalib.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
        _dgvCalib.ColumnHeadersHeight = 34;
        _dgvCalib.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _dgvCalib.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewCheckBoxColumn1, dataGridViewTextBoxColumn3 });
        dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle6.BackColor = SystemColors.Window;
        dataGridViewCellStyle6.Font = new Font("Segoe UI", 12F);
        dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(220, 232, 247);
        dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(26, 26, 26);
        dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
        _dgvCalib.DefaultCellStyle = dataGridViewCellStyle6;
        _dgvCalib.EditMode = DataGridViewEditMode.EditOnEnter;
        _dgvCalib.EnableHeadersVisualStyles = false;
        _dgvCalib.Font = new Font("Segoe UI", 12F);
        _dgvCalib.GridColor = Color.FromArgb(212, 216, 222);
        _dgvCalib.Location = new Point(18, 0);
        _dgvCalib.Margin = new Padding(3, 4, 3, 4);
        _dgvCalib.Name = "_dgvCalib";
        _dgvCalib.RowHeadersVisible = false;
        _dgvCalib.RowHeadersWidth = 62;
        _dgvCalib.RowTemplate.Height = 30;
        _dgvCalib.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvCalib.Size = new Size(526, 333);
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
        _btnAddRow.Location = new Point(18, 341);
        _btnAddRow.Margin = new Padding(3, 4, 3, 4);
        _btnAddRow.Name = "_btnAddRow";
        _btnAddRow.Size = new Size(183, 37);
        _btnAddRow.TabIndex = 6;
        _btnAddRow.Text = "Добавить строку";
        _btnAddRow.UseVisualStyleBackColor = false;
        _btnAddRow.Click += BtnAddRow_Click;
        //
        // _btnDelRow
        //
        _btnDelRow.FlatStyle = FlatStyle.Flat;
        _btnDelRow.Font = new Font("Segoe UI", 12F);
        _btnDelRow.Location = new Point(215, 341);
        _btnDelRow.Margin = new Padding(3, 4, 3, 4);
        _btnDelRow.Name = "_btnDelRow";
        _btnDelRow.Size = new Size(194, 37);
        _btnDelRow.TabIndex = 7;
        _btnDelRow.Text = "Удалить выбранную";
        _btnDelRow.UseVisualStyleBackColor = false;
        _btnDelRow.Click += BtnDelRow_Click;
        //
        // _lblKEquals
        //
        _lblKEquals.AutoSize = true;
        _lblKEquals.Font = new Font("Segoe UI", 10F);
        _lblKEquals.Location = new Point(565, 81);
        _lblKEquals.Name = "_lblKEquals";
        _lblKEquals.Size = new Size(40, 23);
        _lblKEquals.TabIndex = 8;
        _lblKEquals.Text = "k  =";
        //
        // _txtK
        //
        _txtK.Font = new Font("Courier New", 10F);
        _txtK.Location = new Point(606, 75);
        _txtK.Margin = new Padding(3, 4, 3, 4);
        _txtK.Name = "_txtK";
        _txtK.Size = new Size(148, 26);
        _txtK.TabIndex = 9;
        _txtK.Text = "0";
        //
        // _lblBEquals
        //
        _lblBEquals.AutoSize = true;
        _lblBEquals.Font = new Font("Segoe UI", 10F);
        _lblBEquals.Location = new Point(565, 129);
        _lblBEquals.Name = "_lblBEquals";
        _lblBEquals.Size = new Size(42, 23);
        _lblBEquals.TabIndex = 10;
        _lblBEquals.Text = "b  =";
        //
        // _txtB
        //
        _txtB.Font = new Font("Courier New", 10F);
        _txtB.Location = new Point(606, 124);
        _txtB.Margin = new Padding(3, 4, 3, 4);
        _txtB.Name = "_txtB";
        _txtB.Size = new Size(148, 26);
        _txtB.TabIndex = 11;
        _txtB.Text = "0";
        //
        // _lblFormula
        //
        _lblFormula.AutoSize = true;
        _lblFormula.Font = new Font("Segoe UI", 12F);
        _lblFormula.Location = new Point(593, 32);
        _lblFormula.Name = "_lblFormula";
        _lblFormula.Size = new Size(197, 28);
        _lblFormula.TabIndex = 12;
        _lblFormula.Text = "Масса = k × Код + b";
        //
        // _btnLsq
        //
        _btnLsq.FlatAppearance.BorderSize = 0;
        _btnLsq.FlatStyle = FlatStyle.Flat;
        _btnLsq.Font = new Font("Segoe UI", 10F);
        _btnLsq.Location = new Point(567, 183);
        _btnLsq.Margin = new Padding(3, 4, 3, 4);
        _btnLsq.Name = "_btnLsq";
        _btnLsq.Size = new Size(206, 45);
        _btnLsq.TabIndex = 13;
        _btnLsq.Text = "Рассчитать МНК";
        _btnLsq.UseVisualStyleBackColor = false;
        _btnLsq.Click += BtnLsq_Click;
        //
        // _btnCalibSave
        //
        _btnCalibSave.FlatAppearance.BorderSize = 0;
        _btnCalibSave.FlatStyle = FlatStyle.Flat;
        _btnCalibSave.Font = new Font("Segoe UI", 10F);
        _btnCalibSave.Location = new Point(567, 263);
        _btnCalibSave.Margin = new Padding(3, 4, 3, 4);
        _btnCalibSave.Name = "_btnCalibSave";
        _btnCalibSave.Size = new Size(185, 45);
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
        _pnlCalibSHead.Controls.Add(tableLayoutPanel1);
        _pnlCalibSHead.Dock = DockStyle.Top;
        _pnlCalibSHead.Location = new Point(0, 0);
        _pnlCalibSHead.Margin = new Padding(3, 4, 3, 4);
        _pnlCalibSHead.Name = "_pnlCalibSHead";
        _pnlCalibSHead.Size = new Size(1533, 96);
        _pnlCalibSHead.TabIndex = 0;
        //
        // _dotStaticCalibConn
        //
        _dotStaticCalibConn.Controls.Add(_cmbStaticCalibPort);
        _dotStaticCalibConn.Dock = DockStyle.Left;
        _dotStaticCalibConn.Location = new Point(0, 0);
        _dotStaticCalibConn.Name = "_dotStaticCalibConn";
        _dotStaticCalibConn.Size = new Size(180, 94);
        _dotStaticCalibConn.TabIndex = 1;
        //
        // _cmbStaticCalibPort
        //
        _cmbStaticCalibPort.Font = new Font("Segoe UI", 10F);
        _cmbStaticCalibPort.Location = new Point(18, 29);
        _cmbStaticCalibPort.Name = "_cmbStaticCalibPort";
        _cmbStaticCalibPort.Size = new Size(121, 31);
        _cmbStaticCalibPort.TabIndex = 0;
        //
        // _btnStaticCalibConn
        //
        _btnStaticCalibConn.Font = new Font("Segoe UI", 12F);
        _btnStaticCalibConn.Location = new Point(0, 0);
        _btnStaticCalibConn.Name = "_btnStaticCalibConn";
        _btnStaticCalibConn.Size = new Size(75, 23);
        _btnStaticCalibConn.TabIndex = 2;
        _btnStaticCalibConn.Click += BtnStaticCalibConn_Click;
        //
        // _btnStaticCalibPortRefresh
        //
        _btnStaticCalibPortRefresh.Font = new Font("Segoe UI", 11F);
        _btnStaticCalibPortRefresh.Location = new Point(0, 0);
        _btnStaticCalibPortRefresh.Name = "_btnStaticCalibPortRefresh";
        _btnStaticCalibPortRefresh.Size = new Size(75, 23);
        _btnStaticCalibPortRefresh.TabIndex = 3;
        _btnStaticCalibPortRefresh.Click += BtnPortRefresh_Click;
        //
        // _lblStaticCalibConn
        //
        _lblStaticCalibConn.Font = new Font("Segoe UI", 12F);
        _lblStaticCalibConn.Location = new Point(0, 0);
        _lblStaticCalibConn.Name = "_lblStaticCalibConn";
        _lblStaticCalibConn.Size = new Size(100, 23);
        _lblStaticCalibConn.TabIndex = 4;
        //
        // tableLayoutPanel1
        //
        tableLayoutPanel1.ColumnCount = 5;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.7917442F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.41651F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.73546F));
        tableLayoutPanel1.Controls.Add(_rbCh1Calib, 4, 0);
        tableLayoutPanel1.Controls.Add(_lblLiveAdc, 1, 0);
        tableLayoutPanel1.Controls.Add(_lblLiveAdcCap, 0, 0);
        tableLayoutPanel1.Controls.Add(_rbCh0Calib, 4, 1);
        tableLayoutPanel1.Controls.Add(_btnCapture, 2, 0);
        tableLayoutPanel1.Location = new Point(247, 0);
        tableLayoutPanel1.Margin = new Padding(2, 3, 2, 3);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Size = new Size(963, 97);
        tableLayoutPanel1.TabIndex = 5;
        //
        // _rbCh1Calib
        //
        _rbCh1Calib.AutoSize = true;
        _rbCh1Calib.Dock = DockStyle.Fill;
        _rbCh1Calib.Font = new Font("Segoe UI", 11F);
        _rbCh1Calib.Location = new Point(711, 4);
        _rbCh1Calib.Margin = new Padding(8, 4, 3, 4);
        _rbCh1Calib.Name = "_rbCh1Calib";
        _rbCh1Calib.Size = new Size(249, 40);
        _rbCh1Calib.TabIndex = 1;
        _rbCh1Calib.Text = "Канал: Резервный (CH1)";
        _rbCh1Calib.CheckedChanged += RbCh1Calib_CheckedChanged;
        //
        // _lblLiveAdc
        //
        _lblLiveAdc.AutoSize = true;
        _lblLiveAdc.Dock = DockStyle.Fill;
        _lblLiveAdc.Font = new Font("Courier New", 13F, FontStyle.Bold);
        _lblLiveAdc.Location = new Point(195, 0);
        _lblLiveAdc.Name = "_lblLiveAdc";
        _lblLiveAdc.Size = new Size(186, 48);
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
        _lblLiveAdcCap.Size = new Size(186, 48);
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
        _rbCh0Calib.Location = new Point(711, 52);
        _rbCh0Calib.Margin = new Padding(8, 4, 3, 4);
        _rbCh0Calib.Name = "_rbCh0Calib";
        _rbCh0Calib.Size = new Size(249, 41);
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
        _btnCapture.Location = new Point(392, 3);
        _btnCapture.Margin = new Padding(8, 3, 8, 0);
        _btnCapture.Name = "_btnCapture";
        _btnCapture.Size = new Size(145, 45);
        _btnCapture.TabIndex = 4;
        _btnCapture.Text = "Захватить";
        _btnCapture.UseVisualStyleBackColor = false;
        _btnCapture.Click += BtnCapture_Click;
        //
        // _tabCalibD
        //
        _tabCalibD.Controls.Add(_pnlCalibD);
        _tabCalibD.Location = new Point(4, 32);
        _tabCalibD.Margin = new Padding(3, 4, 3, 4);
        _tabCalibD.Name = "_tabCalibD";
        _tabCalibD.Size = new Size(1533, 690);
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
        _pnlCalibD.Margin = new Padding(3, 4, 3, 4);
        _pnlCalibD.Name = "_pnlCalibD";
        _pnlCalibD.Size = new Size(1533, 690);
        _pnlCalibD.TabIndex = 0;
        //
        // _pnlCalibDBody
        //
        _pnlCalibDBody.AutoScroll = true;
        _pnlCalibDBody.BorderStyle = BorderStyle.FixedSingle;
        _pnlCalibDBody.Controls.Add(panel1);
        _pnlCalibDBody.Dock = DockStyle.Fill;
        _pnlCalibDBody.Location = new Point(0, 99);
        _pnlCalibDBody.Margin = new Padding(3, 4, 3, 4);
        _pnlCalibDBody.Name = "_pnlCalibDBody";
        _pnlCalibDBody.Padding = new Padding(18, 16, 18, 16);
        _pnlCalibDBody.Size = new Size(1533, 519);
        _pnlCalibDBody.TabIndex = 1;
        //
        // _lblSecPlus_02
        //
        _lblSecPlus_02.AutoSize = true;
        _lblSecPlus_02.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecPlus_02.Location = new Point(277, 0);
        _lblSecPlus_02.Name = "_lblSecPlus_02";
        _lblSecPlus_02.Size = new Size(133, 37);
        _lblSecPlus_02.TabIndex = 28;
        _lblSecPlus_02.Text = "──────────────────";
        //
        // _lblSecPlus_01
        //
        _lblSecPlus_01.AutoSize = true;
        _lblSecPlus_01.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecPlus_01.Location = new Point(140, 0);
        _lblSecPlus_01.Name = "_lblSecPlus_01";
        _lblSecPlus_01.Size = new Size(120, 37);
        _lblSecPlus_01.TabIndex = 27;
        _lblSecPlus_01.Text = "Направление  →";
        //
        // panel1
        //
        panel1.Controls.Add(tableLayoutPanel2);
        panel1.Location = new Point(475, 29);
        panel1.Name = "panel1";
        panel1.Size = new Size(887, 508);
        panel1.TabIndex = 26;
        //
        // tableLayoutPanel2
        //
        tableLayoutPanel2.ColumnCount = 2;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.2847824F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55.7152176F));
        tableLayoutPanel2.Controls.Add(_dgvDynCalib, 1, 0);
        tableLayoutPanel2.Controls.Add(tlpDirections, 0, 0);
        tableLayoutPanel2.Location = new Point(52, 19);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 1;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel2.Size = new Size(1479, 469);
        tableLayoutPanel2.TabIndex = 25;
        //
        // _dgvDynCalib
        //
        _dgvDynCalib.AllowUserToAddRows = false;
        _dgvDynCalib.AllowUserToDeleteRows = false;
        _dgvDynCalib.AllowUserToResizeRows = false;
        _dgvDynCalib.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _dgvDynCalib.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dgvDynCalib.BackgroundColor = Color.FromArgb(245, 245, 247);
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = Color.FromArgb(0, 255, 255);
        dataGridViewCellStyle4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        dataGridViewCellStyle4.ForeColor = Color.FromArgb(35, 49, 63);
        dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(0, 255, 255);
        dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(35, 49, 63);
        dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
        _dgvDynCalib.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
        _dgvDynCalib.ColumnHeadersHeight = 34;
        _dgvDynCalib.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _dgvDynCalib.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8 });
        _dgvDynCalib.EditMode = DataGridViewEditMode.EditProgrammatically;
        _dgvDynCalib.EnableHeadersVisualStyles = false;
        _dgvDynCalib.Font = new Font("Segoe UI", 12F);
        _dgvDynCalib.GridColor = Color.FromArgb(212, 216, 222);
        _dgvDynCalib.Location = new Point(657, 4);
        _dgvDynCalib.Margin = new Padding(3, 4, 3, 4);
        _dgvDynCalib.MultiSelect = false;
        _dgvDynCalib.Name = "_dgvDynCalib";
        _dgvDynCalib.ReadOnly = true;
        _dgvDynCalib.RowHeadersVisible = false;
        _dgvDynCalib.RowHeadersWidth = 62;
        _dgvDynCalib.RowTemplate.Height = 28;
        _dgvDynCalib.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvDynCalib.Size = new Size(819, 461);
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
        // tlpDirections
        //
        tlpDirections.ColumnCount = 1;
        tlpDirections.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlpDirections.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlpDirections.Controls.Add(tableLayoutPanel3, 0, 0);
        tlpDirections.Controls.Add(tableLayoutPanel4, 0, 1);
        tlpDirections.Location = new Point(3, 3);
        tlpDirections.Name = "tlpDirections";
        tlpDirections.RowCount = 2;
        tlpDirections.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpDirections.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpDirections.Size = new Size(419, 459);
        tlpDirections.TabIndex = 25;
        //
        // tableLayoutPanel3
        //
        tableLayoutPanel3.ColumnCount = 3;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel3.Controls.Add(_lblSecPlus_01, 1, 0);
        tableLayoutPanel3.Controls.Add(_lblSecPlus_00, 0, 0);
        tableLayoutPanel3.Controls.Add(_lblSecPlus_02, 2, 0);
        tableLayoutPanel3.Controls.Add(_txtKPlus, 1, 1);
        tableLayoutPanel3.Controls.Add(_lblKPlusEquals, 0, 1);
        tableLayoutPanel3.Controls.Add(_btnCalcPlus, 1, 5);
        tableLayoutPanel3.Controls.Add(_lblMassPlusCap, 0, 4);
        tableLayoutPanel3.Controls.Add(_btnCapPlus, 2, 3);
        tableLayoutPanel3.Controls.Add(_txtMassPlus, 1, 4);
        tableLayoutPanel3.Controls.Add(_txtCodePlus, 1, 3);
        tableLayoutPanel3.Controls.Add(_lblCodePlusCap, 0, 3);
        tableLayoutPanel3.Controls.Add(_lblAutoCalcPlus, 0, 2);
        tableLayoutPanel3.Dock = DockStyle.Fill;
        tableLayoutPanel3.Location = new Point(3, 3);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 6;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
        tableLayoutPanel3.Size = new Size(413, 223);
        tableLayoutPanel3.TabIndex = 0;
        //
        // _txtKPlus
        //
        _txtKPlus.Font = new Font("Courier New", 10F);
        _txtKPlus.Location = new Point(140, 41);
        _txtKPlus.Margin = new Padding(3, 4, 3, 4);
        _txtKPlus.Name = "_txtKPlus";
        _txtKPlus.Size = new Size(131, 26);
        _txtKPlus.TabIndex = 4;
        //
        // _lblKPlusEquals
        //
        _lblKPlusEquals.AutoSize = true;
        _lblKPlusEquals.Font = new Font("Segoe UI", 10F);
        _lblKPlusEquals.Location = new Point(3, 37);
        _lblKPlusEquals.Name = "_lblKPlusEquals";
        _lblKPlusEquals.Size = new Size(57, 23);
        _lblKPlusEquals.TabIndex = 3;
        _lblKPlusEquals.Text = "K→  =";
        //
        // _btnCalcPlus
        //
        _btnCalcPlus.FlatAppearance.BorderSize = 0;
        _btnCalcPlus.FlatStyle = FlatStyle.Flat;
        _btnCalcPlus.Font = new Font("Segoe UI", 12F);
        _btnCalcPlus.Location = new Point(140, 189);
        _btnCalcPlus.Margin = new Padding(3, 4, 3, 4);
        _btnCalcPlus.Name = "_btnCalcPlus";
        _btnCalcPlus.Size = new Size(131, 30);
        _btnCalcPlus.TabIndex = 11;
        _btnCalcPlus.Text = "Рассчитать K→";
        _btnCalcPlus.UseVisualStyleBackColor = false;
        _btnCalcPlus.Click += BtnCalcPlus_Click;
        //
        // _lblMassPlusCap
        //
        _lblMassPlusCap.AutoSize = true;
        _lblMassPlusCap.Font = new Font("Segoe UI", 12F);
        _lblMassPlusCap.Location = new Point(3, 148);
        _lblMassPlusCap.Name = "_lblMassPlusCap";
        _lblMassPlusCap.Size = new Size(106, 28);
        _lblMassPlusCap.TabIndex = 9;
        _lblMassPlusCap.Text = "Эталон (т):";
        //
        // _btnCapPlus
        //
        _btnCapPlus.FlatAppearance.BorderSize = 0;
        _btnCapPlus.FlatStyle = FlatStyle.Flat;
        _btnCapPlus.Font = new Font("Segoe UI", 8F);
        _btnCapPlus.Location = new Point(277, 115);
        _btnCapPlus.Margin = new Padding(3, 4, 3, 4);
        _btnCapPlus.Name = "_btnCapPlus";
        _btnCapPlus.Size = new Size(114, 29);
        _btnCapPlus.TabIndex = 8;
        _btnCapPlus.Text = "Захватить";
        _btnCapPlus.UseVisualStyleBackColor = false;
        _btnCapPlus.Click += BtnCapPlus_Click;
        //
        // _txtMassPlus
        //
        _txtMassPlus.Font = new Font("Courier New", 9F);
        _txtMassPlus.Location = new Point(140, 152);
        _txtMassPlus.Margin = new Padding(3, 4, 3, 4);
        _txtMassPlus.Name = "_txtMassPlus";
        _txtMassPlus.Size = new Size(131, 24);
        _txtMassPlus.TabIndex = 10;
        //
        // _txtCodePlus
        //
        _txtCodePlus.Font = new Font("Courier New", 9F);
        _txtCodePlus.Location = new Point(140, 115);
        _txtCodePlus.Margin = new Padding(3, 4, 3, 4);
        _txtCodePlus.Name = "_txtCodePlus";
        _txtCodePlus.Size = new Size(131, 24);
        _txtCodePlus.TabIndex = 7;
        //
        // _lblCodePlusCap
        //
        _lblCodePlusCap.AutoSize = true;
        _lblCodePlusCap.Font = new Font("Segoe UI", 12F);
        _lblCodePlusCap.Location = new Point(3, 111);
        _lblCodePlusCap.Name = "_lblCodePlusCap";
        _lblCodePlusCap.Size = new Size(98, 28);
        _lblCodePlusCap.TabIndex = 6;
        _lblCodePlusCap.Text = "Код АЦП:";
        //
        // _lblAutoCalcPlus
        //
        _lblAutoCalcPlus.AutoSize = true;
        _lblAutoCalcPlus.Font = new Font("Segoe UI", 12F);
        _lblAutoCalcPlus.Location = new Point(3, 74);
        _lblAutoCalcPlus.Name = "_lblAutoCalcPlus";
        _lblAutoCalcPlus.Size = new Size(120, 28);
        _lblAutoCalcPlus.TabIndex = 5;
        _lblAutoCalcPlus.Text = "Авторасчёт:";
        //
        // tableLayoutPanel4
        //
        tableLayoutPanel4.ColumnCount = 3;
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel4.Controls.Add(_lblSecMinus_01, 1, 0);
        tableLayoutPanel4.Controls.Add(_lblSecMinus_00, 0, 0);
        tableLayoutPanel4.Controls.Add(_txtKMinus, 1, 1);
        tableLayoutPanel4.Controls.Add(_lblSecMinus_02, 2, 0);
        tableLayoutPanel4.Controls.Add(_lblKMinusEquals, 0, 1);
        tableLayoutPanel4.Controls.Add(_lblAutoCalcMinus, 0, 2);
        tableLayoutPanel4.Controls.Add(_btnCalcMinus, 1, 5);
        tableLayoutPanel4.Controls.Add(_lblMassMinusCap, 0, 4);
        tableLayoutPanel4.Controls.Add(_lblCodeMinusCap, 0, 3);
        tableLayoutPanel4.Controls.Add(_txtMassMinus, 1, 4);
        tableLayoutPanel4.Controls.Add(_txtCodeMinus, 1, 3);
        tableLayoutPanel4.Controls.Add(_btnCapMinus, 2, 3);
        tableLayoutPanel4.Dock = DockStyle.Fill;
        tableLayoutPanel4.Location = new Point(3, 232);
        tableLayoutPanel4.Name = "tableLayoutPanel4";
        tableLayoutPanel4.RowCount = 6;
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
        tableLayoutPanel4.Size = new Size(413, 224);
        tableLayoutPanel4.TabIndex = 1;
        //
        // _txtKMinus
        //
        _txtKMinus.Font = new Font("Courier New", 10F);
        _txtKMinus.Location = new Point(140, 41);
        _txtKMinus.Margin = new Padding(3, 4, 3, 4);
        _txtKMinus.Name = "_txtKMinus";
        _txtKMinus.Size = new Size(131, 26);
        _txtKMinus.TabIndex = 14;
        //
        // _lblKMinusEquals
        //
        _lblKMinusEquals.AutoSize = true;
        _lblKMinusEquals.Font = new Font("Segoe UI", 10F);
        _lblKMinusEquals.Location = new Point(3, 37);
        _lblKMinusEquals.Name = "_lblKMinusEquals";
        _lblKMinusEquals.Size = new Size(57, 23);
        _lblKMinusEquals.TabIndex = 13;
        _lblKMinusEquals.Text = "K←  =";
        //
        // _lblAutoCalcMinus
        //
        _lblAutoCalcMinus.AutoSize = true;
        _lblAutoCalcMinus.Font = new Font("Segoe UI", 12F);
        _lblAutoCalcMinus.Location = new Point(3, 74);
        _lblAutoCalcMinus.Name = "_lblAutoCalcMinus";
        _lblAutoCalcMinus.Size = new Size(120, 28);
        _lblAutoCalcMinus.TabIndex = 15;
        _lblAutoCalcMinus.Text = "Авторасчёт:";
        //
        // _btnCalcMinus
        //
        _btnCalcMinus.FlatAppearance.BorderSize = 0;
        _btnCalcMinus.FlatStyle = FlatStyle.Flat;
        _btnCalcMinus.Font = new Font("Segoe UI", 12F);
        _btnCalcMinus.Location = new Point(140, 189);
        _btnCalcMinus.Margin = new Padding(3, 4, 3, 4);
        _btnCalcMinus.Name = "_btnCalcMinus";
        _btnCalcMinus.Size = new Size(131, 31);
        _btnCalcMinus.TabIndex = 21;
        _btnCalcMinus.Text = "Рассчитать K←";
        _btnCalcMinus.UseVisualStyleBackColor = false;
        _btnCalcMinus.Click += BtnCalcMinus_Click;
        //
        // _lblMassMinusCap
        //
        _lblMassMinusCap.AutoSize = true;
        _lblMassMinusCap.Font = new Font("Segoe UI", 12F);
        _lblMassMinusCap.Location = new Point(3, 148);
        _lblMassMinusCap.Name = "_lblMassMinusCap";
        _lblMassMinusCap.Size = new Size(106, 28);
        _lblMassMinusCap.TabIndex = 19;
        _lblMassMinusCap.Text = "Эталон (т):";
        //
        // _lblCodeMinusCap
        //
        _lblCodeMinusCap.AutoSize = true;
        _lblCodeMinusCap.Font = new Font("Segoe UI", 12F);
        _lblCodeMinusCap.Location = new Point(3, 111);
        _lblCodeMinusCap.Name = "_lblCodeMinusCap";
        _lblCodeMinusCap.Size = new Size(98, 28);
        _lblCodeMinusCap.TabIndex = 16;
        _lblCodeMinusCap.Text = "Код АЦП:";
        //
        // _txtMassMinus
        //
        _txtMassMinus.Font = new Font("Courier New", 9F);
        _txtMassMinus.Location = new Point(140, 152);
        _txtMassMinus.Margin = new Padding(3, 4, 3, 4);
        _txtMassMinus.Name = "_txtMassMinus";
        _txtMassMinus.Size = new Size(131, 24);
        _txtMassMinus.TabIndex = 20;
        //
        // _txtCodeMinus
        //
        _txtCodeMinus.Font = new Font("Courier New", 9F);
        _txtCodeMinus.Location = new Point(140, 115);
        _txtCodeMinus.Margin = new Padding(3, 4, 3, 4);
        _txtCodeMinus.Name = "_txtCodeMinus";
        _txtCodeMinus.Size = new Size(131, 24);
        _txtCodeMinus.TabIndex = 17;
        //
        // _btnCapMinus
        //
        _btnCapMinus.FlatAppearance.BorderSize = 0;
        _btnCapMinus.FlatStyle = FlatStyle.Flat;
        _btnCapMinus.Font = new Font("Segoe UI", 8F);
        _btnCapMinus.Location = new Point(277, 115);
        _btnCapMinus.Margin = new Padding(3, 4, 3, 4);
        _btnCapMinus.Name = "_btnCapMinus";
        _btnCapMinus.Size = new Size(114, 29);
        _btnCapMinus.TabIndex = 18;
        _btnCapMinus.Text = "Захватить";
        _btnCapMinus.UseVisualStyleBackColor = false;
        _btnCapMinus.Click += BtnCapMinus_Click;
        //
        // _lblSecPlus_00
        //
        _lblSecPlus_00.AutoSize = true;
        _lblSecPlus_00.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecPlus_00.Location = new Point(3, 0);
        _lblSecPlus_00.Name = "_lblSecPlus_00";
        _lblSecPlus_00.Size = new Size(34, 28);
        _lblSecPlus_00.TabIndex = 2;
        _lblSecPlus_00.Text = "──";
        //
        // _lblSecMinus_00
        //
        _lblSecMinus_00.AutoSize = true;
        _lblSecMinus_00.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecMinus_00.Location = new Point(3, 0);
        _lblSecMinus_00.Name = "_lblSecMinus_00";
        _lblSecMinus_00.Size = new Size(34, 28);
        _lblSecMinus_00.TabIndex = 12;
        _lblSecMinus_00.Text = "──";
        //
        // _pnlCalibDBottom
        //
        _pnlCalibDBottom.BorderStyle = BorderStyle.FixedSingle;
        _pnlCalibDBottom.Controls.Add(_lblFormulaD);
        _pnlCalibDBottom.Controls.Add(_btnCalibDynSave);
        _pnlCalibDBottom.Dock = DockStyle.Bottom;
        _pnlCalibDBottom.Location = new Point(0, 618);
        _pnlCalibDBottom.Margin = new Padding(3, 4, 3, 4);
        _pnlCalibDBottom.Name = "_pnlCalibDBottom";
        _pnlCalibDBottom.Padding = new Padding(18, 12, 18, 12);
        _pnlCalibDBottom.Size = new Size(1533, 72);
        _pnlCalibDBottom.TabIndex = 2;
        //
        // _lblFormulaD
        //
        _lblFormulaD.AutoSize = true;
        _lblFormulaD.Font = new Font("Segoe UI", 12F);
        _lblFormulaD.Location = new Point(18, 20);
        _lblFormulaD.Name = "_lblFormulaD";
        _lblFormulaD.Size = new Size(210, 28);
        _lblFormulaD.TabIndex = 22;
        _lblFormulaD.Text = "Масса = K × Код АЦП";
        //
        // _btnCalibDynSave
        //
        _btnCalibDynSave.FlatAppearance.BorderSize = 0;
        _btnCalibDynSave.FlatStyle = FlatStyle.Flat;
        _btnCalibDynSave.Font = new Font("Segoe UI", 10F);
        _btnCalibDynSave.Location = new Point(284, 13);
        _btnCalibDynSave.Margin = new Padding(3, 4, 3, 4);
        _btnCalibDynSave.Name = "_btnCalibDynSave";
        _btnCalibDynSave.Size = new Size(251, 45);
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
        _pnlCalibDHead.Margin = new Padding(3, 4, 3, 4);
        _pnlCalibDHead.Name = "_pnlCalibDHead";
        _pnlCalibDHead.Size = new Size(1533, 99);
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
        _tlpHeaders.Controls.Add(_cmbDynamicCalibPort, 0, 0);
        _tlpHeaders.Controls.Add(_lblLiveAdcCapD, 2, 0);
        _tlpHeaders.Controls.Add(_btnDynamicCalibPortRefresh, 1, 1);
        _tlpHeaders.Controls.Add(_btnDynamicCalibConn, 0, 1);
        _tlpHeaders.Controls.Add(_lblDynamicCalibConn, 1, 0);
        _tlpHeaders.Controls.Add(_lblLiveWeightCapD, 2, 1);
        _tlpHeaders.Dock = DockStyle.Fill;
        _tlpHeaders.Location = new Point(0, 0);
        _tlpHeaders.Margin = new Padding(0);
        _tlpHeaders.Name = "_tlpHeaders";
        _tlpHeaders.RowCount = 2;
        _tlpHeaders.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _tlpHeaders.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _tlpHeaders.Size = new Size(1531, 97);
        _tlpHeaders.TabIndex = 9;
        //
        // _lblLiveWeightD
        //
        _lblLiveWeightD.BackColor = Color.Transparent;
        _lblLiveWeightD.Dock = DockStyle.Fill;
        _lblLiveWeightD.Font = new Font("Courier New", 13F, FontStyle.Bold);
        _lblLiveWeightD.ForeColor = Color.FromArgb(192, 0, 192);
        _lblLiveWeightD.Location = new Point(1130, 49);
        _lblLiveWeightD.Name = "_lblLiveWeightD";
        _lblLiveWeightD.Size = new Size(397, 47);
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
        _lblLiveAdcD.Location = new Point(1130, 1);
        _lblLiveAdcD.Name = "_lblLiveAdcD";
        _lblLiveAdcD.Size = new Size(397, 47);
        _lblLiveAdcD.TabIndex = 1;
        _lblLiveAdcD.Text = "—";
        _lblLiveAdcD.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _cmbDynamicCalibPort
        //
        _cmbDynamicCalibPort.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cmbDynamicCalibPort.BackColor = Color.FromArgb(255, 255, 255);
        _cmbDynamicCalibPort.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbDynamicCalibPort.Font = new Font("Segoe UI", 10F);
        _cmbDynamicCalibPort.ForeColor = Color.FromArgb(35, 49, 63);
        _cmbDynamicCalibPort.Location = new Point(5, 7);
        _cmbDynamicCalibPort.Margin = new Padding(4, 0, 4, 4);
        _cmbDynamicCalibPort.Name = "_cmbDynamicCalibPort";
        _cmbDynamicCalibPort.Size = new Size(253, 31);
        _cmbDynamicCalibPort.TabIndex = 4;
        //
        // _lblLiveAdcCapD
        //
        _lblLiveAdcCapD.AutoSize = true;
        _lblLiveAdcCapD.BackColor = Color.Transparent;
        _lblLiveAdcCapD.Dock = DockStyle.Fill;
        _lblLiveAdcCapD.Font = new Font("Segoe UI", 12F);
        _lblLiveAdcCapD.ForeColor = Color.Blue;
        _lblLiveAdcCapD.Location = new Point(772, 1);
        _lblLiveAdcCapD.Name = "_lblLiveAdcCapD";
        _lblLiveAdcCapD.Size = new Size(351, 47);
        _lblLiveAdcCapD.TabIndex = 0;
        _lblLiveAdcCapD.Text = "Текущий код АЦП";
        _lblLiveAdcCapD.TextAlign = ContentAlignment.MiddleRight;
        //
        // _btnDynamicCalibPortRefresh
        //
        _btnDynamicCalibPortRefresh.Dock = DockStyle.Fill;
        _btnDynamicCalibPortRefresh.FlatAppearance.BorderSize = 0;
        _btnDynamicCalibPortRefresh.FlatStyle = FlatStyle.Flat;
        _btnDynamicCalibPortRefresh.Font = new Font("Segoe UI", 11F);
        _btnDynamicCalibPortRefresh.Location = new Point(266, 52);
        _btnDynamicCalibPortRefresh.Name = "_btnDynamicCalibPortRefresh";
        _btnDynamicCalibPortRefresh.Size = new Size(499, 41);
        _btnDynamicCalibPortRefresh.TabIndex = 7;
        _btnDynamicCalibPortRefresh.Text = "↺";
        _btnDynamicCalibPortRefresh.TextAlign = ContentAlignment.MiddleLeft;
        _btnDynamicCalibPortRefresh.UseVisualStyleBackColor = false;
        _btnDynamicCalibPortRefresh.Click += BtnDynamicPortRefresh_Click;
        //
        // _btnDynamicCalibConn
        //
        _btnDynamicCalibConn.Dock = DockStyle.Fill;
        _btnDynamicCalibConn.FlatAppearance.BorderSize = 0;
        _btnDynamicCalibConn.FlatStyle = FlatStyle.Flat;
        _btnDynamicCalibConn.Font = new Font("Segoe UI", 12F);
        _btnDynamicCalibConn.Location = new Point(4, 52);
        _btnDynamicCalibConn.Name = "_btnDynamicCalibConn";
        _btnDynamicCalibConn.Size = new Size(255, 41);
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
        _lblDynamicCalibConn.Location = new Point(266, 1);
        _lblDynamicCalibConn.Name = "_lblDynamicCalibConn";
        _lblDynamicCalibConn.Size = new Size(499, 47);
        _lblDynamicCalibConn.TabIndex = 8;
        _lblDynamicCalibConn.Text = "Динамика: нет подключения";
        //
        // _lblLiveWeightCapD
        //
        _lblLiveWeightCapD.AutoSize = true;
        _lblLiveWeightCapD.BackColor = Color.Transparent;
        _lblLiveWeightCapD.Dock = DockStyle.Fill;
        _lblLiveWeightCapD.Font = new Font("Segoe UI", 12F);
        _lblLiveWeightCapD.ForeColor = Color.Blue;
        _lblLiveWeightCapD.Location = new Point(772, 49);
        _lblLiveWeightCapD.Name = "_lblLiveWeightCapD";
        _lblLiveWeightCapD.Size = new Size(351, 47);
        _lblLiveWeightCapD.TabIndex = 2;
        _lblLiveWeightCapD.Text = "Текущая масса";
        _lblLiveWeightCapD.TextAlign = ContentAlignment.MiddleRight;
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
        _tabSett.Location = new Point(4, 32);
        _tabSett.Margin = new Padding(3, 4, 3, 4);
        _tabSett.Name = "_tabSett";
        _tabSett.Size = new Size(1533, 690);
        _tabSett.TabIndex = 5;
        _tabSett.Text = "Настройки";
        //
        // _lblPortCap
        //
        _lblPortCap.AutoSize = true;
        _lblPortCap.Font = new Font("Segoe UI", 10F);
        _lblPortCap.Location = new Point(23, 27);
        _lblPortCap.Name = "_lblPortCap";
        _lblPortCap.Size = new Size(97, 23);
        _lblPortCap.TabIndex = 0;
        _lblPortCap.Text = "COM-порт:";
        //
        // _cmbSettPort
        //
        _cmbSettPort.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbSettPort.Font = new Font("Segoe UI", 12F);
        _cmbSettPort.Location = new Point(251, 21);
        _cmbSettPort.Margin = new Padding(3, 4, 3, 4);
        _cmbSettPort.Name = "_cmbSettPort";
        _cmbSettPort.Size = new Size(228, 36);
        _cmbSettPort.TabIndex = 1;
        //
        // _lblNpvCap
        //
        _lblNpvCap.AutoSize = true;
        _lblNpvCap.Font = new Font("Segoe UI", 10F);
        _lblNpvCap.Location = new Point(23, 75);
        _lblNpvCap.Name = "_lblNpvCap";
        _lblNpvCap.Size = new Size(70, 23);
        _lblNpvCap.TabIndex = 2;
        _lblNpvCap.Text = "НПВ (т):";
        //
        // _txtNpv
        //
        _txtNpv.Font = new Font("Segoe UI", 12F);
        _txtNpv.Location = new Point(251, 69);
        _txtNpv.Margin = new Padding(3, 4, 3, 4);
        _txtNpv.Name = "_txtNpv";
        _txtNpv.Size = new Size(228, 34);
        _txtNpv.TabIndex = 3;
        _txtNpv.Text = "150";
        //
        // _lblDiscCap
        //
        _lblDiscCap.AutoSize = true;
        _lblDiscCap.Font = new Font("Segoe UI", 10F);
        _lblDiscCap.Location = new Point(23, 123);
        _lblDiscCap.Name = "_lblDiscCap";
        _lblDiscCap.Size = new Size(122, 23);
        _lblDiscCap.TabIndex = 4;
        _lblDiscCap.Text = "Дискретность:";
        //
        // _cmbDisc
        //
        _cmbDisc.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbDisc.Font = new Font("Segoe UI", 12F);
        _cmbDisc.Items.AddRange(new object[] { "0.1 т", "0.05 т", "0.01 т" });
        _cmbDisc.Location = new Point(251, 117);
        _cmbDisc.Margin = new Padding(3, 4, 3, 4);
        _cmbDisc.Name = "_cmbDisc";
        _cmbDisc.Size = new Size(228, 36);
        _cmbDisc.TabIndex = 5;
        //
        // _lblZeroCap
        //
        _lblZeroCap.AutoSize = true;
        _lblZeroCap.Font = new Font("Segoe UI", 10F);
        _lblZeroCap.Location = new Point(23, 171);
        _lblZeroCap.Name = "_lblZeroCap";
        _lblZeroCap.Size = new Size(173, 23);
        _lblZeroCap.TabIndex = 6;
        _lblZeroCap.Text = "Лимит нуля (% НПВ):";
        //
        // _txtZeroLimit
        //
        _txtZeroLimit.Font = new Font("Segoe UI", 12F);
        _txtZeroLimit.Location = new Point(251, 165);
        _txtZeroLimit.Margin = new Padding(3, 4, 3, 4);
        _txtZeroLimit.Name = "_txtZeroLimit";
        _txtZeroLimit.Size = new Size(228, 34);
        _txtZeroLimit.TabIndex = 7;
        _txtZeroLimit.Text = "2";
        //
        // _lblPasswordCap
        //
        _lblPasswordCap.AutoSize = true;
        _lblPasswordCap.Font = new Font("Segoe UI", 10F);
        _lblPasswordCap.Location = new Point(23, 219);
        _lblPasswordCap.Name = "_lblPasswordCap";
        _lblPasswordCap.Size = new Size(129, 23);
        _lblPasswordCap.TabIndex = 8;
        _lblPasswordCap.Text = "Новый пароль:";
        //
        // _txtNewPassword
        //
        _txtNewPassword.Font = new Font("Segoe UI", 12F);
        _txtNewPassword.Location = new Point(251, 213);
        _txtNewPassword.Margin = new Padding(3, 4, 3, 4);
        _txtNewPassword.Name = "_txtNewPassword";
        _txtNewPassword.Size = new Size(228, 34);
        _txtNewPassword.TabIndex = 9;
        _txtNewPassword.UseSystemPasswordChar = true;
        //
        // _btnSaveSettings
        //
        _btnSaveSettings.FlatStyle = FlatStyle.Flat;
        _btnSaveSettings.Font = new Font("Segoe UI", 10F);
        _btnSaveSettings.Location = new Point(23, 272);
        _btnSaveSettings.Margin = new Padding(3, 4, 3, 4);
        _btnSaveSettings.Name = "_btnSaveSettings";
        _btnSaveSettings.Size = new Size(149, 45);
        _btnSaveSettings.TabIndex = 10;
        _btnSaveSettings.Text = "Сохранить";
        _btnSaveSettings.UseVisualStyleBackColor = false;
        _btnSaveSettings.Click += BtnSaveSettings_Click;
        //
        // _rateTimer
        //
        _rateTimer.Tick += RateTimer_Tick;
        //
        // _lblSecMinus_02
        //
        _lblSecMinus_02.AutoSize = true;
        _lblSecMinus_02.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecMinus_02.Location = new Point(277, 0);
        _lblSecMinus_02.Name = "_lblSecMinus_02";
        _lblSecMinus_02.Size = new Size(133, 37);
        _lblSecMinus_02.TabIndex = 27;
        _lblSecMinus_02.Text = "──────────────────";
        //
        // _lblSecMinus_01
        //
        _lblSecMinus_01.AutoSize = true;
        _lblSecMinus_01.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecMinus_01.Location = new Point(140, 0);
        _lblSecMinus_01.Name = "_lblSecMinus_01";
        _lblSecMinus_01.Size = new Size(120, 37);
        _lblSecMinus_01.TabIndex = 28;
        _lblSecMinus_01.Text = "Направление  ←";
        //
        // ServiceForm
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(255, 192, 255);
        ClientSize = new Size(1541, 798);
        Controls.Add(_btnAdmin);
        Controls.Add(_tabs);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(727, 615);
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
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        _tabCalibD.ResumeLayout(false);
        _pnlCalibD.ResumeLayout(false);
        _pnlCalibDBody.ResumeLayout(false);
        panel1.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_dgvDynCalib).EndInit();
        tlpDirections.ResumeLayout(false);
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel3.PerformLayout();
        tableLayoutPanel4.ResumeLayout(false);
        tableLayoutPanel4.PerformLayout();
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
    private TableLayoutPanel tableLayoutPanel1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    private TableLayoutPanel _tlpHeaders;
    private TableLayoutPanel tableLayoutPanel2;
    private Panel panel1;
    private TableLayoutPanel tlpDirections;
    private TableLayoutPanel tableLayoutPanel3;
    private TableLayoutPanel tableLayoutPanel4;
    private Label _lblSecPlus_02;
    private Label _lblSecPlus_01;
    private Label _lblSecMinus_01;
    private Label _lblSecMinus_02;
}
