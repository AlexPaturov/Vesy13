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
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
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
        _pnlLogs = new Panel();
        _lstDynamicLog = new ListBox();
        _pnlChannels = new Panel();
        _tlpChannels = new TableLayoutPanel();
        _pnlDynamicCh0 = new Panel();
        _tlpCh0 = new TableLayoutPanel();
        _lblDynamicCh0 = new Label();
        _lblDynamicCh0Cap = new Label();
        _pnlDynamicCh1 = new Panel();
        _tlpCh1 = new TableLayoutPanel();
        _lblDynamicCh1 = new Label();
        _lblDynamicCh1Cap = new Label();
        _pnlTop = new Panel();
        _tlpTop = new TableLayoutPanel();
        _cmbDynamicPort = new ComboBox();
        _lblDynamicRate = new Label();
        _lblDynamicConn = new Label();
        _btnDynamicClearLog = new Button();
        _chkDynamicLog = new CheckBox();
        _btnDynamicPortRefresh = new Button();
        _btnDynamicConn = new Button();
        _dotDynamicConn = new Panel();
        _tabCalibS = new TabPage();
        _pnlCalibS = new Panel();
        _pnlCalibSBody = new Panel();
        tableLayoutPanel1 = new TableLayoutPanel();
        _dgvCalib = new DataGridView();
        dataGridViewTextBoxColumnCalibActive = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumnCalibCreated = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumnCalibDeleted = new DataGridViewTextBoxColumn();
        panel1 = new Panel();
        panel3 = new Panel();
        tableLayoutPanel2 = new TableLayoutPanel();
        _txtK = new TextBox();
        _lblKEquals = new Label();
        _txtB = new TextBox();
        _lblBEquals = new Label();
        _btnLsq = new Button();
        _btnAddRow = new Button();
        _btnDelRow = new Button();
        _btnCalibSave = new Button();
        panel2 = new Panel();
        tableLayoutPanel3 = new TableLayoutPanel();
        _lblFormula = new Label();
        _pnlCalibSHead = new Panel();
        tlpCalibSHead = new TableLayoutPanel();
        _lblStaticCalibConn = new Label();
        _cmbStaticCalibPort = new ComboBox();
        _btnStaticCalibConn = new Button();
        _btnCapture = new Button();
        _btnStaticCalibPortRefresh = new Button();
        _rbCh0Calib = new RadioButton();
        _rbCh1Calib = new RadioButton();
        _lblLiveAdcCap = new Label();
        _lblStaticCalibMassCap = new Label();
        _lblStaticCalibMass = new Label();
        _lblLiveAdc = new Label();
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
        _pnlLogs.SuspendLayout();
        _pnlChannels.SuspendLayout();
        _tlpChannels.SuspendLayout();
        _pnlDynamicCh0.SuspendLayout();
        _tlpCh0.SuspendLayout();
        _pnlDynamicCh1.SuspendLayout();
        _tlpCh1.SuspendLayout();
        _pnlTop.SuspendLayout();
        _tlpTop.SuspendLayout();
        _tabCalibS.SuspendLayout();
        _pnlCalibS.SuspendLayout();
        _pnlCalibSBody.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvCalib).BeginInit();
        panel1.SuspendLayout();
        panel3.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        panel2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        _pnlCalibSHead.SuspendLayout();
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
        _tabs.Size = new Size(1541, 725);
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
        _tabChannel.Size = new Size(1533, 689);
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
        _tabMonitor.Size = new Size(1533, 689);
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
        _rtbLog.Size = new Size(1171, 353);
        _rtbLog.TabIndex = 10;
        _rtbLog.Text = "";
        _rtbLog.WordWrap = false;
        // 
        // _tabDynamicService
        // 
        _tabDynamicService.Controls.Add(_pnlLogs);
        _tabDynamicService.Controls.Add(_pnlChannels);
        _tabDynamicService.Controls.Add(_pnlTop);
        _tabDynamicService.Location = new Point(4, 32);
        _tabDynamicService.Margin = new Padding(3, 4, 3, 4);
        _tabDynamicService.Name = "_tabDynamicService";
        _tabDynamicService.Size = new Size(1533, 689);
        _tabDynamicService.TabIndex = 2;
        _tabDynamicService.Text = "Сервисный режим Динамика";
        // 
        // _pnlLogs
        // 
        _pnlLogs.Controls.Add(_lstDynamicLog);
        _pnlLogs.Dock = DockStyle.Fill;
        _pnlLogs.Location = new Point(0, 284);
        _pnlLogs.Name = "_pnlLogs";
        _pnlLogs.Size = new Size(1533, 405);
        _pnlLogs.TabIndex = 13;
        // 
        // _lstDynamicLog
        // 
        _lstDynamicLog.Dock = DockStyle.Fill;
        _lstDynamicLog.DrawMode = DrawMode.OwnerDrawFixed;
        _lstDynamicLog.Font = new Font("Courier New", 9F);
        _lstDynamicLog.IntegralHeight = false;
        _lstDynamicLog.ItemHeight = 15;
        _lstDynamicLog.Location = new Point(0, 0);
        _lstDynamicLog.Margin = new Padding(3, 4, 3, 4);
        _lstDynamicLog.Name = "_lstDynamicLog";
        _lstDynamicLog.Size = new Size(1533, 405);
        _lstDynamicLog.TabIndex = 10;
        _lstDynamicLog.DrawItem += LstDynamicLog_DrawItem;
        // 
        // _pnlChannels
        // 
        _pnlChannels.Controls.Add(_tlpChannels);
        _pnlChannels.Dock = DockStyle.Top;
        _pnlChannels.Location = new Point(0, 101);
        _pnlChannels.Name = "_pnlChannels";
        _pnlChannels.Size = new Size(1533, 183);
        _pnlChannels.TabIndex = 12;
        // 
        // _tlpChannels
        // 
        _tlpChannels.ColumnCount = 2;
        _tlpChannels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _tlpChannels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _tlpChannels.Controls.Add(_pnlDynamicCh0, 0, 0);
        _tlpChannels.Controls.Add(_pnlDynamicCh1, 1, 0);
        _tlpChannels.Dock = DockStyle.Fill;
        _tlpChannels.Location = new Point(0, 0);
        _tlpChannels.Name = "_tlpChannels";
        _tlpChannels.RowCount = 1;
        _tlpChannels.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpChannels.Size = new Size(1533, 183);
        _tlpChannels.TabIndex = 0;
        // 
        // _pnlDynamicCh0
        // 
        _pnlDynamicCh0.Controls.Add(_tlpCh0);
        _pnlDynamicCh0.Dock = DockStyle.Fill;
        _pnlDynamicCh0.Location = new Point(3, 4);
        _pnlDynamicCh0.Margin = new Padding(3, 4, 3, 4);
        _pnlDynamicCh0.Name = "_pnlDynamicCh0";
        _pnlDynamicCh0.Size = new Size(760, 175);
        _pnlDynamicCh0.TabIndex = 6;
        // 
        // _tlpCh0
        // 
        _tlpCh0.ColumnCount = 1;
        _tlpCh0.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpCh0.Controls.Add(_lblDynamicCh0, 0, 1);
        _tlpCh0.Controls.Add(_lblDynamicCh0Cap, 0, 0);
        _tlpCh0.Dock = DockStyle.Fill;
        _tlpCh0.Location = new Point(0, 0);
        _tlpCh0.Name = "_tlpCh0";
        _tlpCh0.RowCount = 2;
        _tlpCh0.RowStyles.Add(new RowStyle(SizeType.Percent, 27.4285717F));
        _tlpCh0.RowStyles.Add(new RowStyle(SizeType.Percent, 72.57143F));
        _tlpCh0.Size = new Size(760, 175);
        _tlpCh0.TabIndex = 2;
        // 
        // _lblDynamicCh0
        // 
        _lblDynamicCh0.Dock = DockStyle.Fill;
        _lblDynamicCh0.Font = new Font("Courier New", 48F, FontStyle.Bold);
        _lblDynamicCh0.Location = new Point(3, 48);
        _lblDynamicCh0.Name = "_lblDynamicCh0";
        _lblDynamicCh0.Size = new Size(754, 127);
        _lblDynamicCh0.TabIndex = 1;
        _lblDynamicCh0.Text = "—";
        _lblDynamicCh0.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _lblDynamicCh0Cap
        // 
        _lblDynamicCh0Cap.AutoSize = true;
        _lblDynamicCh0Cap.Dock = DockStyle.Fill;
        _lblDynamicCh0Cap.Font = new Font("Segoe UI", 12F);
        _lblDynamicCh0Cap.Location = new Point(3, 0);
        _lblDynamicCh0Cap.Name = "_lblDynamicCh0Cap";
        _lblDynamicCh0Cap.Size = new Size(754, 48);
        _lblDynamicCh0Cap.TabIndex = 0;
        _lblDynamicCh0Cap.Text = "Канал: Основной (CH0)";
        _lblDynamicCh0Cap.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _pnlDynamicCh1
        // 
        _pnlDynamicCh1.Controls.Add(_tlpCh1);
        _pnlDynamicCh1.Dock = DockStyle.Fill;
        _pnlDynamicCh1.Location = new Point(769, 4);
        _pnlDynamicCh1.Margin = new Padding(3, 4, 3, 4);
        _pnlDynamicCh1.Name = "_pnlDynamicCh1";
        _pnlDynamicCh1.Size = new Size(761, 175);
        _pnlDynamicCh1.TabIndex = 7;
        // 
        // _tlpCh1
        // 
        _tlpCh1.ColumnCount = 1;
        _tlpCh1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpCh1.Controls.Add(_lblDynamicCh1, 0, 1);
        _tlpCh1.Controls.Add(_lblDynamicCh1Cap, 0, 0);
        _tlpCh1.Dock = DockStyle.Fill;
        _tlpCh1.Location = new Point(0, 0);
        _tlpCh1.Name = "_tlpCh1";
        _tlpCh1.RowCount = 2;
        _tlpCh1.RowStyles.Add(new RowStyle(SizeType.Percent, 29.1428566F));
        _tlpCh1.RowStyles.Add(new RowStyle(SizeType.Percent, 70.85714F));
        _tlpCh1.Size = new Size(761, 175);
        _tlpCh1.TabIndex = 3;
        // 
        // _lblDynamicCh1
        // 
        _lblDynamicCh1.Dock = DockStyle.Fill;
        _lblDynamicCh1.Font = new Font("Courier New", 48F, FontStyle.Bold);
        _lblDynamicCh1.Location = new Point(3, 51);
        _lblDynamicCh1.Name = "_lblDynamicCh1";
        _lblDynamicCh1.Size = new Size(755, 124);
        _lblDynamicCh1.TabIndex = 1;
        _lblDynamicCh1.Text = "—";
        _lblDynamicCh1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _lblDynamicCh1Cap
        // 
        _lblDynamicCh1Cap.AutoSize = true;
        _lblDynamicCh1Cap.Dock = DockStyle.Fill;
        _lblDynamicCh1Cap.Font = new Font("Segoe UI", 12F);
        _lblDynamicCh1Cap.Location = new Point(3, 0);
        _lblDynamicCh1Cap.Name = "_lblDynamicCh1Cap";
        _lblDynamicCh1Cap.Size = new Size(755, 51);
        _lblDynamicCh1Cap.TabIndex = 0;
        _lblDynamicCh1Cap.Text = "Канал: Резервный (CH1)";
        _lblDynamicCh1Cap.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _pnlTop
        // 
        _pnlTop.Controls.Add(_tlpTop);
        _pnlTop.Dock = DockStyle.Top;
        _pnlTop.Location = new Point(0, 0);
        _pnlTop.Name = "_pnlTop";
        _pnlTop.Size = new Size(1533, 101);
        _pnlTop.TabIndex = 11;
        // 
        // _tlpTop
        // 
        _tlpTop.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        _tlpTop.ColumnCount = 6;
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.316591F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.7747335F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.1978693F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.5274143F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.6710186F));
        _tlpTop.Controls.Add(_cmbDynamicPort, 0, 0);
        _tlpTop.Controls.Add(_lblDynamicRate, 0, 1);
        _tlpTop.Controls.Add(_lblDynamicConn, 4, 0);
        _tlpTop.Controls.Add(_btnDynamicClearLog, 5, 0);
        _tlpTop.Controls.Add(_chkDynamicLog, 5, 1);
        _tlpTop.Controls.Add(_btnDynamicPortRefresh, 3, 0);
        _tlpTop.Controls.Add(_btnDynamicConn, 2, 0);
        _tlpTop.Controls.Add(_dotDynamicConn, 1, 0);
        _tlpTop.Dock = DockStyle.Fill;
        _tlpTop.Location = new Point(0, 0);
        _tlpTop.Name = "_tlpTop";
        _tlpTop.RowCount = 2;
        _tlpTop.RowStyles.Add(new RowStyle(SizeType.Percent, 52F));
        _tlpTop.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));
        _tlpTop.Size = new Size(1533, 101);
        _tlpTop.TabIndex = 0;
        // 
        // _cmbDynamicPort
        // 
        _cmbDynamicPort.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cmbDynamicPort.BackColor = Color.FromArgb(255, 255, 255);
        _cmbDynamicPort.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbDynamicPort.Font = new Font("Segoe UI", 10F);
        _cmbDynamicPort.ForeColor = Color.FromArgb(35, 49, 63);
        _cmbDynamicPort.Location = new Point(6, 8);
        _cmbDynamicPort.Margin = new Padding(5, 0, 5, 4);
        _cmbDynamicPort.Name = "_cmbDynamicPort";
        _cmbDynamicPort.Size = new Size(243, 31);
        _cmbDynamicPort.TabIndex = 0;
        // 
        // _lblDynamicRate
        // 
        _lblDynamicRate.Dock = DockStyle.Fill;
        _lblDynamicRate.Font = new Font("Segoe UI", 12F);
        _lblDynamicRate.Location = new Point(4, 52);
        _lblDynamicRate.Name = "_lblDynamicRate";
        _lblDynamicRate.Size = new Size(247, 48);
        _lblDynamicRate.TabIndex = 5;
        _lblDynamicRate.Text = "— сэмпл/с";
        _lblDynamicRate.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _lblDynamicConn
        // 
        _lblDynamicConn.Dock = DockStyle.Fill;
        _lblDynamicConn.Font = new Font("Segoe UI", 12F);
        _lblDynamicConn.Location = new Point(721, 1);
        _lblDynamicConn.Name = "_lblDynamicConn";
        _lblDynamicConn.Size = new Size(474, 50);
        _lblDynamicConn.TabIndex = 4;
        _lblDynamicConn.Text = "Нет подключения";
        _lblDynamicConn.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _btnDynamicClearLog
        // 
        _btnDynamicClearLog.Dock = DockStyle.Fill;
        _btnDynamicClearLog.FlatStyle = FlatStyle.Flat;
        _btnDynamicClearLog.Font = new Font("Segoe UI", 12F);
        _btnDynamicClearLog.Location = new Point(1209, 5);
        _btnDynamicClearLog.Margin = new Padding(10, 4, 10, 4);
        _btnDynamicClearLog.Name = "_btnDynamicClearLog";
        _btnDynamicClearLog.Size = new Size(313, 42);
        _btnDynamicClearLog.TabIndex = 9;
        _btnDynamicClearLog.Text = "Очистить";
        _btnDynamicClearLog.UseVisualStyleBackColor = false;
        _btnDynamicClearLog.Click += BtnDynamicClearLog_Click;
        // 
        // _chkDynamicLog
        // 
        _chkDynamicLog.AutoSize = true;
        _chkDynamicLog.Checked = true;
        _chkDynamicLog.CheckState = CheckState.Checked;
        _chkDynamicLog.Dock = DockStyle.Fill;
        _chkDynamicLog.Font = new Font("Segoe UI", 12F);
        _chkDynamicLog.Location = new Point(1219, 56);
        _chkDynamicLog.Margin = new Padding(20, 4, 20, 4);
        _chkDynamicLog.Name = "_chkDynamicLog";
        _chkDynamicLog.Size = new Size(293, 40);
        _chkDynamicLog.TabIndex = 8;
        _chkDynamicLog.Text = "Лог активен";
        // 
        // _btnDynamicPortRefresh
        // 
        _btnDynamicPortRefresh.Dock = DockStyle.Fill;
        _btnDynamicPortRefresh.FlatStyle = FlatStyle.Flat;
        _btnDynamicPortRefresh.Font = new Font("Segoe UI", 11F);
        _btnDynamicPortRefresh.Location = new Point(572, 5);
        _btnDynamicPortRefresh.Margin = new Padding(10, 4, 10, 4);
        _btnDynamicPortRefresh.Name = "_btnDynamicPortRefresh";
        _btnDynamicPortRefresh.Size = new Size(135, 42);
        _btnDynamicPortRefresh.TabIndex = 3;
        _btnDynamicPortRefresh.Text = "↺  Обновить";
        _btnDynamicPortRefresh.UseVisualStyleBackColor = false;
        _btnDynamicPortRefresh.Click += BtnDynamicPortRefresh_Click;
        // 
        // _btnDynamicConn
        // 
        _btnDynamicConn.Dock = DockStyle.Fill;
        _btnDynamicConn.FlatStyle = FlatStyle.Flat;
        _btnDynamicConn.Font = new Font("Segoe UI", 12F);
        _btnDynamicConn.Location = new Point(362, 5);
        _btnDynamicConn.Margin = new Padding(10, 4, 10, 4);
        _btnDynamicConn.Name = "_btnDynamicConn";
        _btnDynamicConn.Size = new Size(189, 42);
        _btnDynamicConn.TabIndex = 2;
        _btnDynamicConn.Text = "Подключить";
        _btnDynamicConn.UseVisualStyleBackColor = false;
        _btnDynamicConn.Click += BtnDynamicConn_Click;
        // 
        // _dotDynamicConn
        // 
        _dotDynamicConn.Dock = DockStyle.Fill;
        _dotDynamicConn.Location = new Point(265, 11);
        _dotDynamicConn.Margin = new Padding(10);
        _dotDynamicConn.Name = "_dotDynamicConn";
        _dotDynamicConn.Size = new Size(76, 30);
        _dotDynamicConn.TabIndex = 1;
        // 
        // _tabCalibS
        // 
        _tabCalibS.Controls.Add(_pnlCalibS);
        _tabCalibS.Location = new Point(4, 32);
        _tabCalibS.Margin = new Padding(3, 4, 3, 4);
        _tabCalibS.Name = "_tabCalibS";
        _tabCalibS.Size = new Size(1533, 689);
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
        _pnlCalibS.Size = new Size(1533, 689);
        _pnlCalibS.TabIndex = 0;
        // 
        // _pnlCalibSBody
        // 
        _pnlCalibSBody.AutoScroll = true;
        _pnlCalibSBody.BackColor = SystemColors.ActiveCaption;
        _pnlCalibSBody.BorderStyle = BorderStyle.FixedSingle;
        _pnlCalibSBody.Controls.Add(tableLayoutPanel1);
        _pnlCalibSBody.Dock = DockStyle.Fill;
        _pnlCalibSBody.Location = new Point(0, 100);
        _pnlCalibSBody.Margin = new Padding(3, 4, 3, 4);
        _pnlCalibSBody.Name = "_pnlCalibSBody";
        _pnlCalibSBody.Padding = new Padding(4);
        _pnlCalibSBody.Size = new Size(1533, 589);
        _pnlCalibSBody.TabIndex = 1;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.BackColor = SystemColors.Window;
        tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Controls.Add(_dgvCalib, 1, 0);
        tableLayoutPanel1.Controls.Add(panel1, 0, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(4, 4);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Size = new Size(1523, 579);
        tableLayoutPanel1.TabIndex = 15;
        // 
        // _dgvCalib
        // 
        _dgvCalib.AllowUserToAddRows = false;
        _dgvCalib.AllowUserToDeleteRows = false;
        _dgvCalib.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.White;
        dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(220, 232, 247);
        _dgvCalib.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        _dgvCalib.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dgvCalib.BackgroundColor = Color.White;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(196, 225, 230);
        dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(26, 26, 26);
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        _dgvCalib.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        _dgvCalib.ColumnHeadersHeight = 34;
        _dgvCalib.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _dgvCalib.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumnCalibActive, dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumnCalibCreated, dataGridViewTextBoxColumnCalibDeleted });
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = SystemColors.Window;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F);
        dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(220, 232, 247);
        dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(26, 26, 26);
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        _dgvCalib.DefaultCellStyle = dataGridViewCellStyle3;
        _dgvCalib.Dock = DockStyle.Fill;
        _dgvCalib.EditMode = DataGridViewEditMode.EditOnEnter;
        _dgvCalib.EnableHeadersVisualStyles = false;
        _dgvCalib.Font = new Font("Segoe UI", 12F);
        _dgvCalib.GridColor = Color.FromArgb(212, 216, 222);
        _dgvCalib.Location = new Point(765, 5);
        _dgvCalib.Margin = new Padding(3, 4, 3, 4);
        _dgvCalib.Name = "_dgvCalib";
        _dgvCalib.RowHeadersVisible = false;
        _dgvCalib.RowHeadersWidth = 62;
        dataGridViewCellStyle4.BackColor = Color.White;
        dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(220, 232, 247);
        _dgvCalib.RowsDefaultCellStyle = dataGridViewCellStyle4;
        _dgvCalib.RowTemplate.Height = 30;
        _dgvCalib.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvCalib.Size = new Size(754, 569);
        _dgvCalib.TabIndex = 5;
        // 
        // dataGridViewTextBoxColumnCalibActive
        // 
        dataGridViewTextBoxColumnCalibActive.FillWeight = 25F;
        dataGridViewTextBoxColumnCalibActive.HeaderText = "Активна";
        dataGridViewTextBoxColumnCalibActive.MinimumWidth = 90;
        dataGridViewTextBoxColumnCalibActive.Name = "dataGridViewTextBoxColumnCalibActive";
        dataGridViewTextBoxColumnCalibActive.ReadOnly = true;
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
        // dataGridViewTextBoxColumn3
        // 
        dataGridViewTextBoxColumn3.FillWeight = 25F;
        dataGridViewTextBoxColumn3.HeaderText = "K/65535";
        dataGridViewTextBoxColumn3.MinimumWidth = 105;
        dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
        dataGridViewTextBoxColumn3.ReadOnly = true;
        // 
        // dataGridViewTextBoxColumnCalibCreated
        // 
        dataGridViewTextBoxColumnCalibCreated.FillWeight = 25F;
        dataGridViewTextBoxColumnCalibCreated.HeaderText = "Добавлена";
        dataGridViewTextBoxColumnCalibCreated.MinimumWidth = 105;
        dataGridViewTextBoxColumnCalibCreated.Name = "dataGridViewTextBoxColumnCalibCreated";
        dataGridViewTextBoxColumnCalibCreated.ReadOnly = true;
        // 
        // dataGridViewTextBoxColumnCalibDeleted
        // 
        dataGridViewTextBoxColumnCalibDeleted.FillWeight = 25F;
        dataGridViewTextBoxColumnCalibDeleted.HeaderText = "Снята";
        dataGridViewTextBoxColumnCalibDeleted.MinimumWidth = 105;
        dataGridViewTextBoxColumnCalibDeleted.Name = "dataGridViewTextBoxColumnCalibDeleted";
        dataGridViewTextBoxColumnCalibDeleted.ReadOnly = true;
        // 
        // panel1
        // 
        panel1.BorderStyle = BorderStyle.FixedSingle;
        panel1.Controls.Add(panel3);
        panel1.Controls.Add(panel2);
        panel1.Dock = DockStyle.Fill;
        panel1.Location = new Point(4, 4);
        panel1.Name = "panel1";
        panel1.Size = new Size(754, 571);
        panel1.TabIndex = 6;
        // 
        // panel3
        // 
        panel3.Controls.Add(tableLayoutPanel2);
        panel3.Dock = DockStyle.Fill;
        panel3.Location = new Point(0, 62);
        panel3.Name = "panel3";
        panel3.Size = new Size(752, 507);
        panel3.TabIndex = 1;
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel2.ColumnCount = 3;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel2.Controls.Add(_txtK, 1, 0);
        tableLayoutPanel2.Controls.Add(_lblKEquals, 0, 0);
        tableLayoutPanel2.Controls.Add(_txtB, 1, 1);
        tableLayoutPanel2.Controls.Add(_lblBEquals, 0, 1);
        tableLayoutPanel2.Controls.Add(_btnLsq, 1, 2);
        tableLayoutPanel2.Controls.Add(_btnAddRow, 2, 4);
        tableLayoutPanel2.Controls.Add(_btnDelRow, 2, 5);
        tableLayoutPanel2.Controls.Add(_btnCalibSave, 1, 6);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Location = new Point(0, 0);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 7;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1111126F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1111107F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1111107F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1111107F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1111107F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1111107F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
        tableLayoutPanel2.Size = new Size(752, 507);
        tableLayoutPanel2.TabIndex = 6;
        // 
        // _txtK
        // 
        _txtK.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtK.Font = new Font("Courier New", 10F);
        _txtK.Location = new Point(256, 15);
        _txtK.Margin = new Padding(5, 0, 5, 0);
        _txtK.Name = "_txtK";
        _txtK.Size = new Size(239, 26);
        _txtK.TabIndex = 9;
        _txtK.Text = "0";
        // 
        // _lblKEquals
        // 
        _lblKEquals.AutoSize = true;
        _lblKEquals.Dock = DockStyle.Fill;
        _lblKEquals.Font = new Font("Segoe UI", 10F);
        _lblKEquals.Location = new Point(4, 1);
        _lblKEquals.Name = "_lblKEquals";
        _lblKEquals.Size = new Size(243, 55);
        _lblKEquals.TabIndex = 8;
        _lblKEquals.Text = "k  =";
        _lblKEquals.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _txtB
        // 
        _txtB.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtB.Font = new Font("Courier New", 10F);
        _txtB.Location = new Point(256, 71);
        _txtB.Margin = new Padding(5, 0, 5, 0);
        _txtB.Name = "_txtB";
        _txtB.Size = new Size(239, 26);
        _txtB.TabIndex = 11;
        _txtB.Text = "0";
        // 
        // _lblBEquals
        // 
        _lblBEquals.AutoSize = true;
        _lblBEquals.Dock = DockStyle.Fill;
        _lblBEquals.Font = new Font("Segoe UI", 10F);
        _lblBEquals.Location = new Point(4, 57);
        _lblBEquals.Name = "_lblBEquals";
        _lblBEquals.Size = new Size(243, 55);
        _lblBEquals.TabIndex = 10;
        _lblBEquals.Text = "b  =";
        _lblBEquals.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _btnLsq
        // 
        _btnLsq.BackColor = SystemColors.ActiveBorder;
        _btnLsq.FlatAppearance.BorderSize = 0;
        _btnLsq.FlatStyle = FlatStyle.Flat;
        _btnLsq.Font = new Font("Segoe UI", 12F);
        _btnLsq.Location = new Point(254, 117);
        _btnLsq.Margin = new Padding(3, 4, 3, 4);
        _btnLsq.Name = "_btnLsq";
        _btnLsq.Size = new Size(243, 45);
        _btnLsq.TabIndex = 13;
        _btnLsq.Text = "Рассчитать МНК";
        _btnLsq.UseVisualStyleBackColor = false;
        _btnLsq.Click += BtnLsq_Click;
        // 
        // _btnAddRow
        // 
        _btnAddRow.BackColor = SystemColors.ActiveBorder;
        _btnAddRow.FlatStyle = FlatStyle.Flat;
        _btnAddRow.Font = new Font("Segoe UI", 12F);
        _btnAddRow.Location = new Point(504, 229);
        _btnAddRow.Margin = new Padding(3, 4, 3, 4);
        _btnAddRow.Name = "_btnAddRow";
        _btnAddRow.Size = new Size(244, 37);
        _btnAddRow.TabIndex = 6;
        _btnAddRow.Text = "Добавить строку →";
        _btnAddRow.UseVisualStyleBackColor = false;
        _btnAddRow.Click += BtnAddRow_Click;
        // 
        // _btnDelRow
        // 
        _btnDelRow.BackColor = SystemColors.ActiveBorder;
        _btnDelRow.FlatStyle = FlatStyle.Flat;
        _btnDelRow.Font = new Font("Segoe UI", 12F);
        _btnDelRow.Location = new Point(504, 285);
        _btnDelRow.Margin = new Padding(3, 4, 3, 4);
        _btnDelRow.Name = "_btnDelRow";
        _btnDelRow.Size = new Size(244, 37);
        _btnDelRow.TabIndex = 7;
        _btnDelRow.Text = "Удалить выбранную →";
        _btnDelRow.UseVisualStyleBackColor = false;
        _btnDelRow.Click += BtnDelRow_Click;
        // 
        // _btnCalibSave
        // 
        _btnCalibSave.BackColor = SystemColors.ActiveBorder;
        _btnCalibSave.FlatAppearance.BorderSize = 0;
        _btnCalibSave.FlatStyle = FlatStyle.Flat;
        _btnCalibSave.Font = new Font("Segoe UI", 12F);
        _btnCalibSave.Location = new Point(254, 341);
        _btnCalibSave.Margin = new Padding(3, 4, 3, 4);
        _btnCalibSave.Name = "_btnCalibSave";
        _btnCalibSave.Size = new Size(243, 45);
        _btnCalibSave.TabIndex = 14;
        _btnCalibSave.Text = "Применить и сохранить";
        _btnCalibSave.UseVisualStyleBackColor = false;
        _btnCalibSave.Click += BtnCalibSave_Click;
        // 
        // panel2
        // 
        panel2.Controls.Add(tableLayoutPanel3);
        panel2.Dock = DockStyle.Top;
        panel2.Location = new Point(0, 0);
        panel2.Name = "panel2";
        panel2.Size = new Size(752, 62);
        panel2.TabIndex = 0;
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.ColumnCount = 1;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.Controls.Add(_lblFormula, 0, 0);
        tableLayoutPanel3.Dock = DockStyle.Fill;
        tableLayoutPanel3.Location = new Point(0, 0);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 1;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.Size = new Size(752, 62);
        tableLayoutPanel3.TabIndex = 0;
        // 
        // _lblFormula
        // 
        _lblFormula.AutoSize = true;
        _lblFormula.Dock = DockStyle.Fill;
        _lblFormula.Font = new Font("Segoe UI", 12F);
        _lblFormula.Location = new Point(3, 0);
        _lblFormula.Name = "_lblFormula";
        _lblFormula.Size = new Size(746, 62);
        _lblFormula.TabIndex = 12;
        _lblFormula.Text = "Масса = k × Код + b";
        _lblFormula.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _pnlCalibSHead
        // 
        _pnlCalibSHead.BackColor = Color.Transparent;
        _pnlCalibSHead.BorderStyle = BorderStyle.FixedSingle;
        _pnlCalibSHead.Controls.Add(tlpCalibSHead);
        _pnlCalibSHead.Dock = DockStyle.Top;
        _pnlCalibSHead.Location = new Point(0, 0);
        _pnlCalibSHead.Margin = new Padding(3, 4, 3, 4);
        _pnlCalibSHead.Name = "_pnlCalibSHead";
        _pnlCalibSHead.Size = new Size(1533, 100);
        _pnlCalibSHead.TabIndex = 0;
        // 
        // tlpCalibSHead
        // 
        tlpCalibSHead.BackColor = Color.Transparent;
        tlpCalibSHead.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tlpCalibSHead.ColumnCount = 6;
        tlpCalibSHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.1568632F));
        tlpCalibSHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.3137255F));
        tlpCalibSHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.198699F));
        tlpCalibSHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.3120728F));
        tlpCalibSHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.4882317F));
        tlpCalibSHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.5383453F));
        tlpCalibSHead.Controls.Add(_lblStaticCalibConn, 1, 0);
        tlpCalibSHead.Controls.Add(_cmbStaticCalibPort, 0, 0);
        tlpCalibSHead.Controls.Add(_btnStaticCalibConn, 0, 1);
        tlpCalibSHead.Controls.Add(_btnCapture, 2, 0);
        tlpCalibSHead.Controls.Add(_btnStaticCalibPortRefresh, 1, 1);
        tlpCalibSHead.Controls.Add(_rbCh0Calib, 3, 1);
        tlpCalibSHead.Controls.Add(_rbCh1Calib, 3, 0);
        tlpCalibSHead.Controls.Add(_lblLiveAdcCap, 4, 0);
        tlpCalibSHead.Controls.Add(_lblStaticCalibMassCap, 4, 1);
        tlpCalibSHead.Controls.Add(_lblStaticCalibMass, 5, 1);
        tlpCalibSHead.Controls.Add(_lblLiveAdc, 5, 0);
        tlpCalibSHead.Dock = DockStyle.Fill;
        tlpCalibSHead.Location = new Point(0, 0);
        tlpCalibSHead.Margin = new Padding(2, 3, 2, 3);
        tlpCalibSHead.Name = "tlpCalibSHead";
        tlpCalibSHead.RowCount = 2;
        tlpCalibSHead.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpCalibSHead.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpCalibSHead.Size = new Size(1531, 98);
        tlpCalibSHead.TabIndex = 5;
        // 
        // _lblStaticCalibConn
        // 
        _lblStaticCalibConn.Dock = DockStyle.Fill;
        _lblStaticCalibConn.Font = new Font("Segoe UI", 12F);
        _lblStaticCalibConn.Location = new Point(190, 1);
        _lblStaticCalibConn.Name = "_lblStaticCalibConn";
        _lblStaticCalibConn.Size = new Size(212, 47);
        _lblStaticCalibConn.TabIndex = 4;
        _lblStaticCalibConn.Text = "Нет подключения";
        _lblStaticCalibConn.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _cmbStaticCalibPort
        // 
        _cmbStaticCalibPort.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cmbStaticCalibPort.Font = new Font("Segoe UI", 10F);
        _cmbStaticCalibPort.Location = new Point(6, 7);
        _cmbStaticCalibPort.Margin = new Padding(5, 0, 5, 4);
        _cmbStaticCalibPort.Name = "_cmbStaticCalibPort";
        _cmbStaticCalibPort.Size = new Size(175, 31);
        _cmbStaticCalibPort.TabIndex = 0;
        // 
        // _btnStaticCalibConn
        // 
        _btnStaticCalibConn.BackColor = Color.Transparent;
        _btnStaticCalibConn.Dock = DockStyle.Fill;
        _btnStaticCalibConn.Font = new Font("Segoe UI", 12F);
        _btnStaticCalibConn.Location = new Point(4, 52);
        _btnStaticCalibConn.Name = "_btnStaticCalibConn";
        _btnStaticCalibConn.Size = new Size(179, 42);
        _btnStaticCalibConn.TabIndex = 2;
        _btnStaticCalibConn.Text = "Подключить";
        _btnStaticCalibConn.UseVisualStyleBackColor = false;
        _btnStaticCalibConn.Click += BtnStaticCalibConn_Click;
        // 
        // _btnCapture
        // 
        _btnCapture.BackColor = Color.Gainsboro;
        _btnCapture.Dock = DockStyle.Fill;
        _btnCapture.FlatAppearance.BorderSize = 0;
        _btnCapture.FlatStyle = FlatStyle.Flat;
        _btnCapture.Font = new Font("Segoe UI", 12F);
        _btnCapture.Location = new Point(409, 4);
        _btnCapture.Name = "_btnCapture";
        _btnCapture.Size = new Size(164, 41);
        _btnCapture.TabIndex = 4;
        _btnCapture.Text = "Захватить";
        _btnCapture.UseVisualStyleBackColor = false;
        _btnCapture.Click += BtnCapture_Click;
        // 
        // _btnStaticCalibPortRefresh
        // 
        _btnStaticCalibPortRefresh.BackColor = Color.Transparent;
        _btnStaticCalibPortRefresh.Dock = DockStyle.Fill;
        _btnStaticCalibPortRefresh.Font = new Font("Segoe UI", 11F);
        _btnStaticCalibPortRefresh.Location = new Point(190, 52);
        _btnStaticCalibPortRefresh.Name = "_btnStaticCalibPortRefresh";
        _btnStaticCalibPortRefresh.Size = new Size(212, 42);
        _btnStaticCalibPortRefresh.TabIndex = 3;
        _btnStaticCalibPortRefresh.Text = "↺ Обновить";
        _btnStaticCalibPortRefresh.UseVisualStyleBackColor = false;
        _btnStaticCalibPortRefresh.Click += BtnPortRefresh_Click;
        // 
        // _rbCh0Calib
        // 
        _rbCh0Calib.AutoSize = true;
        _rbCh0Calib.Checked = true;
        _rbCh0Calib.Font = new Font("Segoe UI", 11F);
        _rbCh0Calib.Location = new Point(585, 53);
        _rbCh0Calib.Margin = new Padding(8, 4, 3, 4);
        _rbCh0Calib.Name = "_rbCh0Calib";
        _rbCh0Calib.Size = new Size(234, 29);
        _rbCh0Calib.TabIndex = 0;
        _rbCh0Calib.TabStop = true;
        _rbCh0Calib.Text = "Канал: Основной (CH0)";
        _rbCh0Calib.CheckedChanged += RbCh0Calib_CheckedChanged;
        // 
        // _rbCh1Calib
        // 
        _rbCh1Calib.AutoSize = true;
        _rbCh1Calib.Font = new Font("Segoe UI", 11F);
        _rbCh1Calib.Location = new Point(585, 5);
        _rbCh1Calib.Margin = new Padding(8, 4, 3, 4);
        _rbCh1Calib.Name = "_rbCh1Calib";
        _rbCh1Calib.Size = new Size(241, 29);
        _rbCh1Calib.TabIndex = 1;
        _rbCh1Calib.Text = "Канал: Резервный (CH1)";
        _rbCh1Calib.CheckedChanged += RbCh1Calib_CheckedChanged;
        // 
        // _lblLiveAdcCap
        // 
        _lblLiveAdcCap.AutoSize = true;
        _lblLiveAdcCap.Dock = DockStyle.Fill;
        _lblLiveAdcCap.Font = new Font("Segoe UI", 12F);
        _lblLiveAdcCap.Location = new Point(844, 1);
        _lblLiveAdcCap.Name = "_lblLiveAdcCap";
        _lblLiveAdcCap.Size = new Size(321, 47);
        _lblLiveAdcCap.TabIndex = 2;
        _lblLiveAdcCap.Text = "Текущий код АЦП";
        _lblLiveAdcCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblStaticCalibMassCap
        // 
        _lblStaticCalibMassCap.AutoSize = true;
        _lblStaticCalibMassCap.BackColor = Color.Transparent;
        _lblStaticCalibMassCap.Dock = DockStyle.Fill;
        _lblStaticCalibMassCap.Font = new Font("Segoe UI", 12F);
        _lblStaticCalibMassCap.ForeColor = Color.FromArgb(46, 58, 70);
        _lblStaticCalibMassCap.Location = new Point(844, 49);
        _lblStaticCalibMassCap.Name = "_lblStaticCalibMassCap";
        _lblStaticCalibMassCap.Size = new Size(321, 48);
        _lblStaticCalibMassCap.TabIndex = 5;
        _lblStaticCalibMassCap.Text = "Текущая масса";
        _lblStaticCalibMassCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblStaticCalibMass
        // 
        _lblStaticCalibMass.BackColor = Color.Transparent;
        _lblStaticCalibMass.Dock = DockStyle.Fill;
        _lblStaticCalibMass.Font = new Font("Courier New", 13F, FontStyle.Bold);
        _lblStaticCalibMass.ForeColor = Color.FromArgb(192, 0, 192);
        _lblStaticCalibMass.Location = new Point(1172, 49);
        _lblStaticCalibMass.Name = "_lblStaticCalibMass";
        _lblStaticCalibMass.Size = new Size(355, 48);
        _lblStaticCalibMass.TabIndex = 6;
        _lblStaticCalibMass.Text = "—";
        _lblStaticCalibMass.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblLiveAdc
        // 
        _lblLiveAdc.AutoSize = true;
        _lblLiveAdc.Dock = DockStyle.Fill;
        _lblLiveAdc.Font = new Font("Courier New", 13F, FontStyle.Bold);
        _lblLiveAdc.Location = new Point(1172, 1);
        _lblLiveAdc.Name = "_lblLiveAdc";
        _lblLiveAdc.Size = new Size(355, 47);
        _lblLiveAdc.TabIndex = 3;
        _lblLiveAdc.Text = "—";
        _lblLiveAdc.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _tabCalibD
        // 
        _tabCalibD.Controls.Add(_pnlCalibD);
        _tabCalibD.Location = new Point(4, 32);
        _tabCalibD.Margin = new Padding(3, 4, 3, 4);
        _tabCalibD.Name = "_tabCalibD";
        _tabCalibD.Size = new Size(1533, 689);
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
        _pnlCalibD.Size = new Size(1533, 689);
        _pnlCalibD.TabIndex = 0;
        // 
        // _pnlCalibDBody
        // 
        _pnlCalibDBody.AutoScroll = true;
        _pnlCalibDBody.BorderStyle = BorderStyle.FixedSingle;
        _pnlCalibDBody.Controls.Add(pnlCalibDMain);
        _pnlCalibDBody.Dock = DockStyle.Fill;
        _pnlCalibDBody.Location = new Point(0, 99);
        _pnlCalibDBody.Margin = new Padding(3, 4, 3, 4);
        _pnlCalibDBody.Name = "_pnlCalibDBody";
        _pnlCalibDBody.Size = new Size(1533, 519);
        _pnlCalibDBody.TabIndex = 1;
        // 
        // pnlCalibDMain
        // 
        pnlCalibDMain.Controls.Add(tlpCalibDMain);
        pnlCalibDMain.Dock = DockStyle.Fill;
        pnlCalibDMain.Location = new Point(0, 0);
        pnlCalibDMain.Margin = new Padding(0);
        pnlCalibDMain.Name = "pnlCalibDMain";
        pnlCalibDMain.Size = new Size(1531, 517);
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
        tlpCalibDMain.Name = "tlpCalibDMain";
        tlpCalibDMain.RowCount = 1;
        tlpCalibDMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpCalibDMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpCalibDMain.Size = new Size(1531, 517);
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
        tlpDirections.Location = new Point(3, 3);
        tlpDirections.Name = "tlpDirections";
        tlpDirections.RowCount = 2;
        tlpDirections.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpDirections.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpDirections.Size = new Size(742, 511);
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
        tlpCalibDPlus.Location = new Point(3, 3);
        tlpCalibDPlus.Name = "tlpCalibDPlus";
        tlpCalibDPlus.RowCount = 6;
        tlpCalibDPlus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDPlus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDPlus.RowStyles.Add(new RowStyle(SizeType.Percent, 12F));
        tlpCalibDPlus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDPlus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDPlus.RowStyles.Add(new RowStyle(SizeType.Percent, 24F));
        tlpCalibDPlus.Size = new Size(736, 249);
        tlpCalibDPlus.TabIndex = 0;
        // 
        // _lblSecPlus_01
        // 
        _lblSecPlus_01.AutoSize = true;
        _lblSecPlus_01.Dock = DockStyle.Fill;
        _lblSecPlus_01.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecPlus_01.ForeColor = Color.FromArgb(46, 58, 70);
        _lblSecPlus_01.Location = new Point(248, 0);
        _lblSecPlus_01.Name = "_lblSecPlus_01";
        _lblSecPlus_01.Size = new Size(240, 39);
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
        _lblSecPlus_00.Size = new Size(239, 39);
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
        _lblSecPlus_02.Location = new Point(494, 0);
        _lblSecPlus_02.Name = "_lblSecPlus_02";
        _lblSecPlus_02.Size = new Size(239, 39);
        _lblSecPlus_02.TabIndex = 28;
        _lblSecPlus_02.Text = "──────────────────";
        _lblSecPlus_02.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _txtKPlus
        // 
        _txtKPlus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtKPlus.Font = new Font("Courier New", 10F);
        _txtKPlus.Location = new Point(250, 45);
        _txtKPlus.Margin = new Padding(5, 0, 5, 0);
        _txtKPlus.Name = "_txtKPlus";
        _txtKPlus.Size = new Size(236, 26);
        _txtKPlus.TabIndex = 4;
        // 
        // _lblKPlusEquals
        // 
        _lblKPlusEquals.AutoSize = true;
        _lblKPlusEquals.Dock = DockStyle.Fill;
        _lblKPlusEquals.Font = new Font("Segoe UI", 10F);
        _lblKPlusEquals.Location = new Point(3, 39);
        _lblKPlusEquals.Name = "_lblKPlusEquals";
        _lblKPlusEquals.Size = new Size(239, 39);
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
        _btnCalcPlus.Location = new Point(259, 192);
        _btnCalcPlus.Margin = new Padding(14, 7, 14, 7);
        _btnCalcPlus.Name = "_btnCalcPlus";
        _btnCalcPlus.Size = new Size(218, 50);
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
        _lblMassPlusCap.Location = new Point(3, 146);
        _lblMassPlusCap.Name = "_lblMassPlusCap";
        _lblMassPlusCap.Size = new Size(239, 39);
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
        _btnCapPlus.Location = new Point(507, 112);
        _btnCapPlus.Margin = new Padding(16, 5, 16, 5);
        _btnCapPlus.Name = "_btnCapPlus";
        _btnCapPlus.Size = new Size(213, 29);
        _btnCapPlus.TabIndex = 8;
        _btnCapPlus.Text = "Захватить";
        _btnCapPlus.UseVisualStyleBackColor = false;
        _btnCapPlus.Click += BtnCapPlus_Click;
        // 
        // _txtMassPlus
        // 
        _txtMassPlus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtMassPlus.Font = new Font("Courier New", 9F);
        _txtMassPlus.Location = new Point(250, 153);
        _txtMassPlus.Margin = new Padding(5, 0, 5, 0);
        _txtMassPlus.Name = "_txtMassPlus";
        _txtMassPlus.Size = new Size(236, 24);
        _txtMassPlus.TabIndex = 10;
        // 
        // _txtCodePlus
        // 
        _txtCodePlus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtCodePlus.Font = new Font("Courier New", 9F);
        _txtCodePlus.Location = new Point(250, 114);
        _txtCodePlus.Margin = new Padding(5, 0, 5, 0);
        _txtCodePlus.Name = "_txtCodePlus";
        _txtCodePlus.Size = new Size(236, 24);
        _txtCodePlus.TabIndex = 7;
        // 
        // _lblCodePlusCap
        // 
        _lblCodePlusCap.AutoSize = true;
        _lblCodePlusCap.Dock = DockStyle.Fill;
        _lblCodePlusCap.Font = new Font("Segoe UI", 12F);
        _lblCodePlusCap.Location = new Point(3, 107);
        _lblCodePlusCap.Name = "_lblCodePlusCap";
        _lblCodePlusCap.Size = new Size(239, 39);
        _lblCodePlusCap.TabIndex = 6;
        _lblCodePlusCap.Text = "Код АЦП";
        _lblCodePlusCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblAutoCalcPlus
        // 
        _lblAutoCalcPlus.AutoSize = true;
        _lblAutoCalcPlus.Dock = DockStyle.Fill;
        _lblAutoCalcPlus.Font = new Font("Segoe UI", 12F);
        _lblAutoCalcPlus.Location = new Point(3, 78);
        _lblAutoCalcPlus.Name = "_lblAutoCalcPlus";
        _lblAutoCalcPlus.Size = new Size(239, 29);
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
        tlpCalibDMinus.Location = new Point(3, 258);
        tlpCalibDMinus.Name = "tlpCalibDMinus";
        tlpCalibDMinus.RowCount = 6;
        tlpCalibDMinus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDMinus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDMinus.RowStyles.Add(new RowStyle(SizeType.Percent, 12F));
        tlpCalibDMinus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDMinus.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
        tlpCalibDMinus.RowStyles.Add(new RowStyle(SizeType.Percent, 24F));
        tlpCalibDMinus.Size = new Size(736, 250);
        tlpCalibDMinus.TabIndex = 1;
        // 
        // _lblSecMinus_01
        // 
        _lblSecMinus_01.AutoSize = true;
        _lblSecMinus_01.Dock = DockStyle.Fill;
        _lblSecMinus_01.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecMinus_01.ForeColor = Color.FromArgb(46, 58, 70);
        _lblSecMinus_01.Location = new Point(248, 0);
        _lblSecMinus_01.Name = "_lblSecMinus_01";
        _lblSecMinus_01.Size = new Size(239, 40);
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
        _lblSecMinus_00.Size = new Size(239, 40);
        _lblSecMinus_00.TabIndex = 12;
        _lblSecMinus_00.Text = "──────────────────";
        _lblSecMinus_00.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _txtKMinus
        // 
        _txtKMinus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtKMinus.Font = new Font("Courier New", 10F);
        _txtKMinus.Location = new Point(250, 47);
        _txtKMinus.Margin = new Padding(5, 0, 5, 0);
        _txtKMinus.Name = "_txtKMinus";
        _txtKMinus.Size = new Size(235, 26);
        _txtKMinus.TabIndex = 14;
        // 
        // _lblSecMinus_02
        // 
        _lblSecMinus_02.AutoSize = true;
        _lblSecMinus_02.Dock = DockStyle.Fill;
        _lblSecMinus_02.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblSecMinus_02.ForeColor = Color.FromArgb(46, 58, 70);
        _lblSecMinus_02.Location = new Point(493, 0);
        _lblSecMinus_02.Name = "_lblSecMinus_02";
        _lblSecMinus_02.Size = new Size(240, 40);
        _lblSecMinus_02.TabIndex = 27;
        _lblSecMinus_02.Text = "──────────────────";
        _lblSecMinus_02.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblKMinusEquals
        // 
        _lblKMinusEquals.AutoSize = true;
        _lblKMinusEquals.Dock = DockStyle.Fill;
        _lblKMinusEquals.Font = new Font("Segoe UI", 10F);
        _lblKMinusEquals.Location = new Point(3, 40);
        _lblKMinusEquals.Name = "_lblKMinusEquals";
        _lblKMinusEquals.Size = new Size(239, 40);
        _lblKMinusEquals.TabIndex = 13;
        _lblKMinusEquals.Text = "K←  =";
        _lblKMinusEquals.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblAutoCalcMinus
        // 
        _lblAutoCalcMinus.AutoSize = true;
        _lblAutoCalcMinus.Dock = DockStyle.Fill;
        _lblAutoCalcMinus.Font = new Font("Segoe UI", 12F);
        _lblAutoCalcMinus.Location = new Point(3, 80);
        _lblAutoCalcMinus.Name = "_lblAutoCalcMinus";
        _lblAutoCalcMinus.Size = new Size(239, 30);
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
        _btnCalcMinus.Location = new Point(259, 197);
        _btnCalcMinus.Margin = new Padding(14, 7, 14, 7);
        _btnCalcMinus.Name = "_btnCalcMinus";
        _btnCalcMinus.Size = new Size(217, 46);
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
        _lblMassMinusCap.Location = new Point(3, 150);
        _lblMassMinusCap.Name = "_lblMassMinusCap";
        _lblMassMinusCap.Size = new Size(239, 40);
        _lblMassMinusCap.TabIndex = 19;
        _lblMassMinusCap.Text = "Эталон (т)";
        _lblMassMinusCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblCodeMinusCap
        // 
        _lblCodeMinusCap.AutoSize = true;
        _lblCodeMinusCap.Dock = DockStyle.Fill;
        _lblCodeMinusCap.Font = new Font("Segoe UI", 12F);
        _lblCodeMinusCap.Location = new Point(3, 110);
        _lblCodeMinusCap.Name = "_lblCodeMinusCap";
        _lblCodeMinusCap.Size = new Size(239, 40);
        _lblCodeMinusCap.TabIndex = 16;
        _lblCodeMinusCap.Text = "Код АЦП";
        _lblCodeMinusCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _txtMassMinus
        // 
        _txtMassMinus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtMassMinus.Font = new Font("Courier New", 9F);
        _txtMassMinus.Location = new Point(250, 158);
        _txtMassMinus.Margin = new Padding(5, 0, 5, 0);
        _txtMassMinus.Name = "_txtMassMinus";
        _txtMassMinus.Size = new Size(235, 24);
        _txtMassMinus.TabIndex = 20;
        // 
        // _txtCodeMinus
        // 
        _txtCodeMinus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtCodeMinus.Font = new Font("Courier New", 9F);
        _txtCodeMinus.Location = new Point(250, 118);
        _txtCodeMinus.Margin = new Padding(5, 0, 5, 0);
        _txtCodeMinus.Name = "_txtCodeMinus";
        _txtCodeMinus.Size = new Size(235, 24);
        _txtCodeMinus.TabIndex = 17;
        // 
        // _btnCapMinus
        // 
        _btnCapMinus.Dock = DockStyle.Fill;
        _btnCapMinus.FlatAppearance.BorderSize = 0;
        _btnCapMinus.FlatStyle = FlatStyle.Flat;
        _btnCapMinus.Font = new Font("Segoe UI", 8F);
        _btnCapMinus.Location = new Point(506, 115);
        _btnCapMinus.Margin = new Padding(16, 5, 16, 5);
        _btnCapMinus.Name = "_btnCapMinus";
        _btnCapMinus.Size = new Size(214, 30);
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
        dataGridViewCellStyle5.BackColor = Color.White;
        dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(220, 232, 247);
        _dgvDynCalib.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
        _dgvDynCalib.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dgvDynCalib.BackgroundColor = Color.White;
        dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle6.BackColor = Color.FromArgb(147, 112, 219);
        dataGridViewCellStyle6.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        dataGridViewCellStyle6.ForeColor = Color.FromArgb(240, 255, 240);
        dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(147, 112, 219);
        dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(240, 255, 240);
        dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
        _dgvDynCalib.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
        _dgvDynCalib.ColumnHeadersHeight = 34;
        _dgvDynCalib.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _dgvDynCalib.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8 });
        _dgvDynCalib.Dock = DockStyle.Fill;
        _dgvDynCalib.EditMode = DataGridViewEditMode.EditProgrammatically;
        _dgvDynCalib.EnableHeadersVisualStyles = false;
        _dgvDynCalib.Font = new Font("Segoe UI", 12F);
        _dgvDynCalib.GridColor = Color.FromArgb(212, 216, 222);
        _dgvDynCalib.Location = new Point(751, 4);
        _dgvDynCalib.Margin = new Padding(3, 4, 3, 4);
        _dgvDynCalib.MultiSelect = false;
        _dgvDynCalib.Name = "_dgvDynCalib";
        _dgvDynCalib.ReadOnly = true;
        _dgvDynCalib.RowHeadersVisible = false;
        _dgvDynCalib.RowHeadersWidth = 62;
        dataGridViewCellStyle7.BackColor = Color.White;
        dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(220, 232, 247);
        _dgvDynCalib.RowsDefaultCellStyle = dataGridViewCellStyle7;
        _dgvDynCalib.RowTemplate.Height = 28;
        _dgvDynCalib.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvDynCalib.Size = new Size(777, 509);
        _dgvDynCalib.TabIndex = 24;
        // 
        // dataGridViewTextBoxColumn4
        // 
        dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dataGridViewTextBoxColumn4.FillWeight = 18F;
        dataGridViewTextBoxColumn4.HeaderText = "Активный";
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
        _pnlCalibDBottom.Location = new Point(0, 618);
        _pnlCalibDBottom.Margin = new Padding(3, 4, 3, 4);
        _pnlCalibDBottom.Name = "_pnlCalibDBottom";
        _pnlCalibDBottom.Padding = new Padding(18, 12, 18, 12);
        _pnlCalibDBottom.Size = new Size(1533, 71);
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
        _btnCalibDynSave.Font = new Font("Segoe UI", 12F);
        _btnCalibDynSave.Location = new Point(283, 13);
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
        _tlpHeaders.Size = new Size(1531, 97);
        _tlpHeaders.TabIndex = 9;
        // 
        // _lblLiveWeightD
        // 
        _lblLiveWeightD.BackColor = Color.Transparent;
        _lblLiveWeightD.Dock = DockStyle.Fill;
        _lblLiveWeightD.Font = new Font("Courier New", 13F, FontStyle.Bold);
        _lblLiveWeightD.ForeColor = Color.FromArgb(192, 0, 192);
        _lblLiveWeightD.Location = new Point(1130, 47);
        _lblLiveWeightD.Name = "_lblLiveWeightD";
        _lblLiveWeightD.Size = new Size(397, 49);
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
        _lblLiveAdcD.Size = new Size(397, 45);
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
        _lblLiveAdcCapD.Location = new Point(772, 1);
        _lblLiveAdcCapD.Name = "_lblLiveAdcCapD";
        _lblLiveAdcCapD.Size = new Size(351, 45);
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
        _btnDynamicCalibPortRefresh.Location = new Point(281, 51);
        _btnDynamicCalibPortRefresh.Margin = new Padding(18, 3, 3, 3);
        _btnDynamicCalibPortRefresh.Name = "_btnDynamicCalibPortRefresh";
        _btnDynamicCalibPortRefresh.Size = new Size(137, 40);
        _btnDynamicCalibPortRefresh.TabIndex = 7;
        _btnDynamicCalibPortRefresh.Text = "↺ Обновить";
        _btnDynamicCalibPortRefresh.UseVisualStyleBackColor = false;
        _btnDynamicCalibPortRefresh.Click += BtnDynamicPortRefresh_Click;
        // 
        // _btnDynamicCalibConn
        // 
        _btnDynamicCalibConn.Dock = DockStyle.Fill;
        _btnDynamicCalibConn.FlatAppearance.BorderSize = 0;
        _btnDynamicCalibConn.FlatStyle = FlatStyle.Flat;
        _btnDynamicCalibConn.Font = new Font("Segoe UI", 12F);
        _btnDynamicCalibConn.Location = new Point(4, 50);
        _btnDynamicCalibConn.Name = "_btnDynamicCalibConn";
        _btnDynamicCalibConn.Size = new Size(255, 43);
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
        _lblDynamicCalibConn.Size = new Size(499, 45);
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
        _lblLiveWeightCapD.Location = new Point(772, 47);
        _lblLiveWeightCapD.Name = "_lblLiveWeightCapD";
        _lblLiveWeightCapD.Size = new Size(351, 49);
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
        _cmbDynamicCalibPort.Location = new Point(6, 6);
        _cmbDynamicCalibPort.Margin = new Padding(5, 0, 5, 4);
        _cmbDynamicCalibPort.Name = "_cmbDynamicCalibPort";
        _cmbDynamicCalibPort.Size = new Size(251, 31);
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
        _tabSett.Location = new Point(4, 32);
        _tabSett.Margin = new Padding(3, 4, 3, 4);
        _tabSett.Name = "_tabSett";
        _tabSett.Size = new Size(1533, 689);
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
        // ServiceForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(255, 192, 255);
        ClientSize = new Size(1541, 797);
        Controls.Add(_btnAdmin);
        Controls.Add(_tabs);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(727, 612);
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
        _pnlLogs.ResumeLayout(false);
        _pnlChannels.ResumeLayout(false);
        _tlpChannels.ResumeLayout(false);
        _pnlDynamicCh0.ResumeLayout(false);
        _tlpCh0.ResumeLayout(false);
        _tlpCh0.PerformLayout();
        _pnlDynamicCh1.ResumeLayout(false);
        _tlpCh1.ResumeLayout(false);
        _tlpCh1.PerformLayout();
        _pnlTop.ResumeLayout(false);
        _tlpTop.ResumeLayout(false);
        _tlpTop.PerformLayout();
        _tabCalibS.ResumeLayout(false);
        _pnlCalibS.ResumeLayout(false);
        _pnlCalibSBody.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_dgvCalib).EndInit();
        panel1.ResumeLayout(false);
        panel3.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        tableLayoutPanel2.PerformLayout();
        panel2.ResumeLayout(false);
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel3.PerformLayout();
        _pnlCalibSHead.ResumeLayout(false);
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
    private ListBox _lstDynamicLog;

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
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumnCalibActive;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumnCalibCreated;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumnCalibDeleted;
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
    private Panel _pnlTop;
    private TableLayoutPanel _tlpTop;
    private Panel _pnlChannels;
    private TableLayoutPanel _tlpChannels;
    private Panel _pnlLogs;
    private TableLayoutPanel _tlpCh0;
    private TableLayoutPanel _tlpCh1;
    private Label _lblStaticCalibMassCap;
    private Label _lblStaticCalibMass;
    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel tableLayoutPanel2;
    private Panel panel1;
    private Panel panel3;
    private Panel panel2;
    private TableLayoutPanel tableLayoutPanel3;
}
