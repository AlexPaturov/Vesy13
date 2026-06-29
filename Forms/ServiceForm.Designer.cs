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
        tableLayoutPanel1 = new TableLayoutPanel();
        _rbCh1Calib = new RadioButton();
        _lblLiveAdc = new Label();
        _lblLiveAdcCap = new Label();
        _rbCh0Calib = new RadioButton();
        _btnCapture = new Button();
        _tabCalibD = new TabPage();
        _pnlCalibD = new Panel();
        _pnlCalibDBody = new Panel();
        _lblSecPlus = new Label();
        _lblKPlusEquals = new Label();
        _txtKPlus = new TextBox();
        _lblAutoCalcPlus = new Label();
        _lblCodePlusCap = new Label();
        _txtCodePlus = new TextBox();
        _btnCapPlus = new Button();
        _lblMassPlusCap = new Label();
        _txtMassPlus = new TextBox();
        _btnCalcPlus = new Button();
        _lblSecMinus = new Label();
        _lblKMinusEquals = new Label();
        _txtKMinus = new TextBox();
        _lblAutoCalcMinus = new Label();
        _lblCodeMinusCap = new Label();
        _txtCodeMinus = new TextBox();
        _btnCapMinus = new Button();
        _lblMassMinusCap = new Label();
        _txtMassMinus = new TextBox();
        _btnCalcMinus = new Button();
        _lblFormulaD = new Label();
        _btnCalibDynSave = new Button();
        _dgvDynCalib = new DataGridView();
        _pnlCalibDHead = new Panel();
        _lblLiveAdcCapD = new Label();
        _lblLiveAdcD = new Label();
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
        _tabCalibS.SuspendLayout();
        _pnlCalibS.SuspendLayout();
        _pnlCalibSBody.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvCalib).BeginInit();
        _pnlCalibSHead.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        _tabCalibD.SuspendLayout();
        _pnlCalibD.SuspendLayout();
        _pnlCalibDBody.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvDynCalib).BeginInit();
        _pnlCalibDHead.SuspendLayout();
        _tabSett.SuspendLayout();
        SuspendLayout();
        // 
        // _btnAdmin
        // 
        _btnAdmin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnAdmin.FlatAppearance.BorderSize = 0;
        _btnAdmin.FlatStyle = FlatStyle.Flat;
        _btnAdmin.Location = new Point(443, 8);
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
        _tabs.Controls.Add(_tabCalibS);
        _tabs.Controls.Add(_tabCalibD);
        _tabs.Controls.Add(_tabSett);
        _tabs.Location = new Point(0, 44);
        _tabs.Name = "_tabs";
        _tabs.SelectedIndex = 0;
        _tabs.Size = new Size(723, 379);
        _tabs.TabIndex = 1;
        // 
        // _tabChannel
        // 
        _tabChannel.Controls.Add(_lblChannelTitle);
        _tabChannel.Controls.Add(_rbMain);
        _tabChannel.Controls.Add(_rbBackup);
        _tabChannel.Controls.Add(_lblChannelNote);
        _tabChannel.Location = new Point(4, 24);
        _tabChannel.Name = "_tabChannel";
        _tabChannel.Size = new Size(715, 351);
        _tabChannel.TabIndex = 0;
        _tabChannel.Text = "Канал";
        // 
        // _lblChannelTitle
        // 
        _lblChannelTitle.AutoSize = true;
        _lblChannelTitle.Location = new Point(20, 16);
        _lblChannelTitle.Name = "_lblChannelTitle";
        _lblChannelTitle.Size = new Size(144, 15);
        _lblChannelTitle.TabIndex = 0;
        _lblChannelTitle.Text = "Выбор активного канала";
        // 
        // _rbMain
        // 
        _rbMain.AutoSize = true;
        _rbMain.Checked = true;
        _rbMain.Location = new Point(20, 52);
        _rbMain.Name = "_rbMain";
        _rbMain.Size = new Size(128, 19);
        _rbMain.TabIndex = 1;
        _rbMain.TabStop = true;
        _rbMain.Text = "Канал: Основной (CH0)";
        _rbMain.CheckedChanged += RbMain_CheckedChanged;
        // 
        // _rbBackup
        // 
        _rbBackup.AutoSize = true;
        _rbBackup.Location = new Point(20, 90);
        _rbBackup.Name = "_rbBackup";
        _rbBackup.Size = new Size(132, 19);
        _rbBackup.TabIndex = 2;
        _rbBackup.Text = "Канал: Резервный (CH1)";
        _rbBackup.CheckedChanged += RbBackup_CheckedChanged;
        // 
        // _lblChannelNote
        // 
        _lblChannelNote.AutoSize = true;
        _lblChannelNote.Location = new Point(20, 136);
        _lblChannelNote.Name = "_lblChannelNote";
        _lblChannelNote.Size = new Size(373, 15);
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
        _tabMonitor.Location = new Point(4, 24);
        _tabMonitor.Name = "_tabMonitor";
        _tabMonitor.Size = new Size(715, 351);
        _tabMonitor.TabIndex = 1;
        _tabMonitor.Text = "Сервисный режим Статика";
        // 
        // _cmbPort
        // 
        _cmbPort.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbPort.Location = new Point(10, 12);
        _cmbPort.Name = "_cmbPort";
        _cmbPort.Size = new Size(90, 23);
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
        _btnConn.Location = new Point(124, 8);
        _btnConn.Name = "_btnConn";
        _btnConn.Size = new Size(110, 28);
        _btnConn.TabIndex = 2;
        _btnConn.Text = "Подключить";
        _btnConn.UseVisualStyleBackColor = false;
        _btnConn.Click += BtnMonConn_Click;
        // 
        // _btnPortRefresh
        // 
        _btnPortRefresh.FlatStyle = FlatStyle.Flat;
        _btnPortRefresh.Location = new Point(242, 8);
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
        _lblConn.Location = new Point(280, 14);
        _lblConn.Name = "_lblConn";
        _lblConn.Size = new Size(106, 15);
        _lblConn.TabIndex = 4;
        _lblConn.Text = "Нет подключения";
        // 
        // _lblRate
        // 
        _lblRate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblRate.AutoSize = true;
        _lblRate.Location = new Point(560, 14);
        _lblRate.Name = "_lblRate";
        _lblRate.Size = new Size(49, 15);
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
        _lblCh0Cap.Location = new Point(8, 6);
        _lblCh0Cap.Name = "_lblCh0Cap";
        _lblCh0Cap.Size = new Size(104, 15);
        _lblCh0Cap.TabIndex = 0;
        _lblCh0Cap.Text = "Канал: Основной (CH0)";
        // 
        // _lblCh0
        // 
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
        _lblCh1Cap.Location = new Point(8, 6);
        _lblCh1Cap.Name = "_lblCh1Cap";
        _lblCh1Cap.Size = new Size(108, 15);
        _lblCh1Cap.TabIndex = 0;
        _lblCh1Cap.Text = "Канал: Резервный (CH1)";
        // 
        // _lblCh1
        // 
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
        _chkLog.Location = new Point(10, 198);
        _chkLog.Name = "_chkLog";
        _chkLog.Size = new Size(92, 19);
        _chkLog.TabIndex = 8;
        _chkLog.Text = "Лог активен";
        // 
        // _btnClearLog
        // 
        _btnClearLog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnClearLog.FlatStyle = FlatStyle.Flat;
        _btnClearLog.Location = new Point(598, 198);
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
        _rtbLog.Location = new Point(10, 239);
        _rtbLog.Name = "_rtbLog";
        _rtbLog.ReadOnly = true;
        _rtbLog.ScrollBars = RichTextBoxScrollBars.Vertical;
        _rtbLog.Size = new Size(690, 99);
        _rtbLog.TabIndex = 10;
        _rtbLog.Text = "";
        _rtbLog.WordWrap = false;
        // 
        // _tabCalibS
        // 
        _tabCalibS.Controls.Add(_pnlCalibS);
        _tabCalibS.Location = new Point(4, 24);
        _tabCalibS.Name = "_tabCalibS";
        _tabCalibS.Size = new Size(715, 351);
        _tabCalibS.TabIndex = 2;
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
        _pnlCalibS.Size = new Size(715, 351);
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
        _pnlCalibSBody.Location = new Point(0, 57);
        _pnlCalibSBody.Name = "_pnlCalibSBody";
        _pnlCalibSBody.Padding = new Padding(16, 12, 16, 12);
        _pnlCalibSBody.Size = new Size(715, 294);
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
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(220, 232, 247);
        dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(26, 26, 26);
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        _dgvCalib.DefaultCellStyle = dataGridViewCellStyle2;
        _dgvCalib.EditMode = DataGridViewEditMode.EditOnEnter;
        _dgvCalib.EnableHeadersVisualStyles = false;
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
        _lblKEquals.Location = new Point(494, 61);
        _lblKEquals.Name = "_lblKEquals";
        _lblKEquals.Size = new Size(27, 15);
        _lblKEquals.TabIndex = 8;
        _lblKEquals.Text = "k  =";
        // 
        // _txtK
        // 
        _txtK.Location = new Point(530, 56);
        _txtK.Name = "_txtK";
        _txtK.Size = new Size(130, 23);
        _txtK.TabIndex = 9;
        _txtK.Text = "0";
        // 
        // _lblBEquals
        // 
        _lblBEquals.AutoSize = true;
        _lblBEquals.Location = new Point(494, 97);
        _lblBEquals.Name = "_lblBEquals";
        _lblBEquals.Size = new Size(28, 15);
        _lblBEquals.TabIndex = 10;
        _lblBEquals.Text = "b  =";
        // 
        // _txtB
        // 
        _txtB.Location = new Point(530, 93);
        _txtB.Name = "_txtB";
        _txtB.Size = new Size(130, 23);
        _txtB.TabIndex = 11;
        _txtB.Text = "0";
        // 
        // _lblFormula
        // 
        _lblFormula.AutoSize = true;
        _lblFormula.Location = new Point(519, 24);
        _lblFormula.Name = "_lblFormula";
        _lblFormula.Size = new Size(117, 15);
        _lblFormula.TabIndex = 12;
        _lblFormula.Text = "Масса = k × Код + b";
        // 
        // _btnLsq
        // 
        _btnLsq.FlatAppearance.BorderSize = 0;
        _btnLsq.FlatStyle = FlatStyle.Flat;
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
        _pnlCalibSHead.Controls.Add(tableLayoutPanel1);
        _pnlCalibSHead.Dock = DockStyle.Top;
        _pnlCalibSHead.Location = new Point(0, 0);
        _pnlCalibSHead.Name = "_pnlCalibSHead";
        _pnlCalibSHead.Size = new Size(715, 57);
        _pnlCalibSHead.TabIndex = 0;
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
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Margin = new Padding(2);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Size = new Size(713, 55);
        tableLayoutPanel1.TabIndex = 5;
        // 
        // _rbCh1Calib
        // 
        _rbCh1Calib.AutoSize = true;
        _rbCh1Calib.Dock = DockStyle.Fill;
        _rbCh1Calib.Location = new Point(527, 3);
        _rbCh1Calib.Margin = new Padding(7, 3, 3, 3);
        _rbCh1Calib.Name = "_rbCh1Calib";
        _rbCh1Calib.Size = new Size(183, 21);
        _rbCh1Calib.TabIndex = 1;
        _rbCh1Calib.Text = "Канал: Резервный (CH1)";
        _rbCh1Calib.CheckedChanged += RbCh1Calib_CheckedChanged;
        // 
        // _lblLiveAdc
        // 
        _lblLiveAdc.AutoSize = true;
        _lblLiveAdc.Dock = DockStyle.Fill;
        _lblLiveAdc.Location = new Point(145, 0);
        _lblLiveAdc.Name = "_lblLiveAdc";
        _lblLiveAdc.Size = new Size(136, 27);
        _lblLiveAdc.TabIndex = 3;
        _lblLiveAdc.Text = "—";
        _lblLiveAdc.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblLiveAdcCap
        // 
        _lblLiveAdcCap.AutoSize = true;
        _lblLiveAdcCap.Dock = DockStyle.Fill;
        _lblLiveAdcCap.Location = new Point(3, 0);
        _lblLiveAdcCap.Name = "_lblLiveAdcCap";
        _lblLiveAdcCap.Size = new Size(136, 27);
        _lblLiveAdcCap.TabIndex = 2;
        _lblLiveAdcCap.Text = "Текущий код АЦП:";
        _lblLiveAdcCap.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _rbCh0Calib
        // 
        _rbCh0Calib.AutoSize = true;
        _rbCh0Calib.Checked = true;
        _rbCh0Calib.Dock = DockStyle.Fill;
        _rbCh0Calib.Location = new Point(527, 30);
        _rbCh0Calib.Margin = new Padding(7, 3, 3, 3);
        _rbCh0Calib.Name = "_rbCh0Calib";
        _rbCh0Calib.Size = new Size(183, 22);
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
        _btnCapture.Location = new Point(291, 2);
        _btnCapture.Margin = new Padding(7, 2, 7, 0);
        _btnCapture.Name = "_btnCapture";
        _btnCapture.Size = new Size(105, 25);
        _btnCapture.TabIndex = 4;
        _btnCapture.Text = "Захватить";
        _btnCapture.UseVisualStyleBackColor = false;
        _btnCapture.Click += BtnCapture_Click;
        // 
        // _tabCalibD
        // 
        _tabCalibD.Controls.Add(_pnlCalibD);
        _tabCalibD.Location = new Point(4, 24);
        _tabCalibD.Name = "_tabCalibD";
        _tabCalibD.Size = new Size(715, 351);
        _tabCalibD.TabIndex = 3;
        _tabCalibD.Text = "Калибровка Динамика";
        // 
        // _pnlCalibD
        // 
        _pnlCalibD.Controls.Add(_pnlCalibDBody);
        _pnlCalibD.Controls.Add(_pnlCalibDHead);
        _pnlCalibD.Dock = DockStyle.Fill;
        _pnlCalibD.Location = new Point(0, 0);
        _pnlCalibD.Name = "_pnlCalibD";
        _pnlCalibD.Size = new Size(715, 351);
        _pnlCalibD.TabIndex = 0;
        // 
        // _pnlCalibDBody
        // 
        _pnlCalibDBody.AutoScroll = true;
        _pnlCalibDBody.BorderStyle = BorderStyle.FixedSingle;
        _pnlCalibDBody.Controls.Add(_dgvDynCalib);
        _pnlCalibDBody.Controls.Add(_lblSecPlus);
        _pnlCalibDBody.Controls.Add(_lblKPlusEquals);
        _pnlCalibDBody.Controls.Add(_txtKPlus);
        _pnlCalibDBody.Controls.Add(_lblAutoCalcPlus);
        _pnlCalibDBody.Controls.Add(_lblCodePlusCap);
        _pnlCalibDBody.Controls.Add(_txtCodePlus);
        _pnlCalibDBody.Controls.Add(_btnCapPlus);
        _pnlCalibDBody.Controls.Add(_lblMassPlusCap);
        _pnlCalibDBody.Controls.Add(_txtMassPlus);
        _pnlCalibDBody.Controls.Add(_btnCalcPlus);
        _pnlCalibDBody.Controls.Add(_lblSecMinus);
        _pnlCalibDBody.Controls.Add(_lblKMinusEquals);
        _pnlCalibDBody.Controls.Add(_txtKMinus);
        _pnlCalibDBody.Controls.Add(_lblAutoCalcMinus);
        _pnlCalibDBody.Controls.Add(_lblCodeMinusCap);
        _pnlCalibDBody.Controls.Add(_txtCodeMinus);
        _pnlCalibDBody.Controls.Add(_btnCapMinus);
        _pnlCalibDBody.Controls.Add(_lblMassMinusCap);
        _pnlCalibDBody.Controls.Add(_txtMassMinus);
        _pnlCalibDBody.Controls.Add(_btnCalcMinus);
        _pnlCalibDBody.Controls.Add(_lblFormulaD);
        _pnlCalibDBody.Controls.Add(_btnCalibDynSave);
        _pnlCalibDBody.Dock = DockStyle.Fill;
        _pnlCalibDBody.Location = new Point(0, 40);
        _pnlCalibDBody.Name = "_pnlCalibDBody";
        _pnlCalibDBody.Padding = new Padding(16, 12, 16, 12);
        _pnlCalibDBody.Size = new Size(715, 311);
        _pnlCalibDBody.TabIndex = 1;
        // 
        // _lblSecPlus
        // 
        _lblSecPlus.AutoSize = true;
        _lblSecPlus.Location = new Point(16, 8);
        _lblSecPlus.Name = "_lblSecPlus";
        _lblSecPlus.Size = new Size(226, 15);
        _lblSecPlus.TabIndex = 2;
        _lblSecPlus.Text = "── Направление  →  ──────────────────";
        // 
        // _lblKPlusEquals
        // 
        _lblKPlusEquals.AutoSize = true;
        _lblKPlusEquals.Location = new Point(16, 36);
        _lblKPlusEquals.Name = "_lblKPlusEquals";
        _lblKPlusEquals.Size = new Size(38, 15);
        _lblKPlusEquals.TabIndex = 3;
        _lblKPlusEquals.Text = "K→  =";
        // 
        // _txtKPlus
        // 
        _txtKPlus.Location = new Point(68, 32);
        _txtKPlus.Name = "_txtKPlus";
        _txtKPlus.Size = new Size(160, 23);
        _txtKPlus.TabIndex = 4;
        // 
        // _lblAutoCalcPlus
        // 
        _lblAutoCalcPlus.AutoSize = true;
        _lblAutoCalcPlus.Location = new Point(16, 64);
        _lblAutoCalcPlus.Name = "_lblAutoCalcPlus";
        _lblAutoCalcPlus.Size = new Size(73, 15);
        _lblAutoCalcPlus.TabIndex = 5;
        _lblAutoCalcPlus.Text = "Авторасчёт:";
        // 
        // _lblCodePlusCap
        // 
        _lblCodePlusCap.AutoSize = true;
        _lblCodePlusCap.Location = new Point(32, 86);
        _lblCodePlusCap.Name = "_lblCodePlusCap";
        _lblCodePlusCap.Size = new Size(59, 15);
        _lblCodePlusCap.TabIndex = 6;
        _lblCodePlusCap.Text = "Код АЦП:";
        // 
        // _txtCodePlus
        // 
        _txtCodePlus.Location = new Point(156, 82);
        _txtCodePlus.Name = "_txtCodePlus";
        _txtCodePlus.Size = new Size(120, 23);
        _txtCodePlus.TabIndex = 7;
        // 
        // _btnCapPlus
        // 
        _btnCapPlus.FlatAppearance.BorderSize = 0;
        _btnCapPlus.FlatStyle = FlatStyle.Flat;
        _btnCapPlus.Location = new Point(284, 80);
        _btnCapPlus.Name = "_btnCapPlus";
        _btnCapPlus.Size = new Size(100, 26);
        _btnCapPlus.TabIndex = 8;
        _btnCapPlus.Text = "Захватить";
        _btnCapPlus.UseVisualStyleBackColor = false;
        _btnCapPlus.Click += BtnCapPlus_Click;
        // 
        // _lblMassPlusCap
        // 
        _lblMassPlusCap.AutoSize = true;
        _lblMassPlusCap.Location = new Point(32, 114);
        _lblMassPlusCap.Name = "_lblMassPlusCap";
        _lblMassPlusCap.Size = new Size(65, 15);
        _lblMassPlusCap.TabIndex = 9;
        _lblMassPlusCap.Text = "Эталон (т):";
        // 
        // _txtMassPlus
        // 
        _txtMassPlus.Location = new Point(156, 110);
        _txtMassPlus.Name = "_txtMassPlus";
        _txtMassPlus.Size = new Size(120, 23);
        _txtMassPlus.TabIndex = 10;
        // 
        // _btnCalcPlus
        // 
        _btnCalcPlus.FlatAppearance.BorderSize = 0;
        _btnCalcPlus.FlatStyle = FlatStyle.Flat;
        _btnCalcPlus.Location = new Point(32, 142);
        _btnCalcPlus.Name = "_btnCalcPlus";
        _btnCalcPlus.Size = new Size(180, 30);
        _btnCalcPlus.TabIndex = 11;
        _btnCalcPlus.Text = "Рассчитать K→";
        _btnCalcPlus.UseVisualStyleBackColor = false;
        _btnCalcPlus.Click += BtnCalcPlus_Click;
        // 
        // _lblSecMinus
        // 
        _lblSecMinus.AutoSize = true;
        _lblSecMinus.Location = new Point(16, 184);
        _lblSecMinus.Name = "_lblSecMinus";
        _lblSecMinus.Size = new Size(226, 15);
        _lblSecMinus.TabIndex = 12;
        _lblSecMinus.Text = "── Направление  ←  ──────────────────";
        // 
        // _lblKMinusEquals
        // 
        _lblKMinusEquals.AutoSize = true;
        _lblKMinusEquals.Location = new Point(16, 212);
        _lblKMinusEquals.Name = "_lblKMinusEquals";
        _lblKMinusEquals.Size = new Size(38, 15);
        _lblKMinusEquals.TabIndex = 13;
        _lblKMinusEquals.Text = "K←  =";
        // 
        // _txtKMinus
        // 
        _txtKMinus.Location = new Point(68, 208);
        _txtKMinus.Name = "_txtKMinus";
        _txtKMinus.Size = new Size(160, 23);
        _txtKMinus.TabIndex = 14;
        // 
        // _lblAutoCalcMinus
        // 
        _lblAutoCalcMinus.AutoSize = true;
        _lblAutoCalcMinus.Location = new Point(16, 244);
        _lblAutoCalcMinus.Name = "_lblAutoCalcMinus";
        _lblAutoCalcMinus.Size = new Size(73, 15);
        _lblAutoCalcMinus.TabIndex = 15;
        _lblAutoCalcMinus.Text = "Авторасчёт:";
        // 
        // _lblCodeMinusCap
        // 
        _lblCodeMinusCap.AutoSize = true;
        _lblCodeMinusCap.Location = new Point(32, 266);
        _lblCodeMinusCap.Name = "_lblCodeMinusCap";
        _lblCodeMinusCap.Size = new Size(59, 15);
        _lblCodeMinusCap.TabIndex = 16;
        _lblCodeMinusCap.Text = "Код АЦП:";
        // 
        // _txtCodeMinus
        // 
        _txtCodeMinus.Location = new Point(156, 262);
        _txtCodeMinus.Name = "_txtCodeMinus";
        _txtCodeMinus.Size = new Size(120, 23);
        _txtCodeMinus.TabIndex = 17;
        // 
        // _btnCapMinus
        // 
        _btnCapMinus.FlatAppearance.BorderSize = 0;
        _btnCapMinus.FlatStyle = FlatStyle.Flat;
        _btnCapMinus.Location = new Point(284, 260);
        _btnCapMinus.Name = "_btnCapMinus";
        _btnCapMinus.Size = new Size(100, 26);
        _btnCapMinus.TabIndex = 18;
        _btnCapMinus.Text = "Захватить";
        _btnCapMinus.UseVisualStyleBackColor = false;
        _btnCapMinus.Click += BtnCapMinus_Click;
        // 
        // _lblMassMinusCap
        // 
        _lblMassMinusCap.AutoSize = true;
        _lblMassMinusCap.Location = new Point(32, 294);
        _lblMassMinusCap.Name = "_lblMassMinusCap";
        _lblMassMinusCap.Size = new Size(65, 15);
        _lblMassMinusCap.TabIndex = 19;
        _lblMassMinusCap.Text = "Эталон (т):";
        // 
        // _txtMassMinus
        // 
        _txtMassMinus.Location = new Point(156, 290);
        _txtMassMinus.Name = "_txtMassMinus";
        _txtMassMinus.Size = new Size(120, 23);
        _txtMassMinus.TabIndex = 20;
        // 
        // _btnCalcMinus
        // 
        _btnCalcMinus.FlatAppearance.BorderSize = 0;
        _btnCalcMinus.FlatStyle = FlatStyle.Flat;
        _btnCalcMinus.Location = new Point(32, 322);
        _btnCalcMinus.Name = "_btnCalcMinus";
        _btnCalcMinus.Size = new Size(180, 30);
        _btnCalcMinus.TabIndex = 21;
        _btnCalcMinus.Text = "Рассчитать K←";
        _btnCalcMinus.UseVisualStyleBackColor = false;
        _btnCalcMinus.Click += BtnCalcMinus_Click;
        // 
        // _lblFormulaD
        // 
        _lblFormulaD.AutoSize = true;
        _lblFormulaD.Location = new Point(16, 360);
        _lblFormulaD.Name = "_lblFormulaD";
        _lblFormulaD.Size = new Size(126, 15);
        _lblFormulaD.TabIndex = 22;
        _lblFormulaD.Text = "Масса = K × Код АЦП";
        // 
        // _btnCalibDynSave
        // 
        _btnCalibDynSave.FlatAppearance.BorderSize = 0;
        _btnCalibDynSave.FlatStyle = FlatStyle.Flat;
        _btnCalibDynSave.Location = new Point(16, 390);
        _btnCalibDynSave.Name = "_btnCalibDynSave";
        _btnCalibDynSave.Size = new Size(220, 34);
        _btnCalibDynSave.TabIndex = 23;
        _btnCalibDynSave.Text = "Применить и сохранить";
        _btnCalibDynSave.UseVisualStyleBackColor = false;
        _btnCalibDynSave.Click += BtnCalibDynSave_Click;
        // 
        // _dgvDynCalib
        //
        _dgvDynCalib.AllowUserToAddRows = false;
        _dgvDynCalib.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _dgvDynCalib.AllowUserToDeleteRows = false;
        _dgvDynCalib.AllowUserToResizeRows = false;
        _dgvDynCalib.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dgvDynCalib.BackgroundColor = Color.FromArgb(245, 245, 247);
        _dgvDynCalib.ColumnHeadersHeight = 34;
        _dgvDynCalib.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _dgvDynCalib.EditMode = DataGridViewEditMode.EditProgrammatically;
        _dgvDynCalib.EnableHeadersVisualStyles = false;
        _dgvDynCalib.GridColor = Color.FromArgb(212, 216, 222);
        _dgvDynCalib.Location = new Point(410, 32);
        _dgvDynCalib.MultiSelect = false;
        _dgvDynCalib.Name = "_dgvDynCalib";
        _dgvDynCalib.ReadOnly = true;
        _dgvDynCalib.RowHeadersVisible = false;
        _dgvDynCalib.RowHeadersWidth = 62;
        _dgvDynCalib.RowTemplate.Height = 28;
        _dgvDynCalib.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvDynCalib.Size = new Size(280, 260);
        _dgvDynCalib.TabIndex = 24;
        //
        // _pnlCalibDHead
        // 
        _pnlCalibDHead.BorderStyle = BorderStyle.FixedSingle;
        _pnlCalibDHead.Controls.Add(_lblLiveAdcCapD);
        _pnlCalibDHead.Controls.Add(_lblLiveAdcD);
        _pnlCalibDHead.Dock = DockStyle.Top;
        _pnlCalibDHead.Location = new Point(0, 0);
        _pnlCalibDHead.Name = "_pnlCalibDHead";
        _pnlCalibDHead.Padding = new Padding(16, 12, 16, 12);
        _pnlCalibDHead.Size = new Size(715, 40);
        _pnlCalibDHead.TabIndex = 0;
        // 
        // _lblLiveAdcCapD
        // 
        _lblLiveAdcCapD.AutoSize = true;
        _lblLiveAdcCapD.Location = new Point(20, 16);
        _lblLiveAdcCapD.Name = "_lblLiveAdcCapD";
        _lblLiveAdcCapD.Size = new Size(110, 15);
        _lblLiveAdcCapD.TabIndex = 0;
        _lblLiveAdcCapD.Text = "Текущий код АЦП:";
        // 
        // _lblLiveAdcD
        // 
        _lblLiveAdcD.AutoSize = true;
        _lblLiveAdcD.Location = new Point(165, 12);
        _lblLiveAdcD.Name = "_lblLiveAdcD";
        _lblLiveAdcD.Size = new Size(19, 15);
        _lblLiveAdcD.TabIndex = 1;
        _lblLiveAdcD.Text = "—";
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
        _tabSett.Location = new Point(4, 24);
        _tabSett.Name = "_tabSett";
        _tabSett.Size = new Size(715, 351);
        _tabSett.TabIndex = 4;
        _tabSett.Text = "Настройки";
        // 
        // _lblPortCap
        // 
        _lblPortCap.AutoSize = true;
        _lblPortCap.Location = new Point(20, 20);
        _lblPortCap.Name = "_lblPortCap";
        _lblPortCap.Size = new Size(69, 15);
        _lblPortCap.TabIndex = 0;
        _lblPortCap.Text = "COM-порт:";
        // 
        // _cmbSettPort
        // 
        _cmbSettPort.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbSettPort.Location = new Point(220, 16);
        _cmbSettPort.Name = "_cmbSettPort";
        _cmbSettPort.Size = new Size(200, 23);
        _cmbSettPort.TabIndex = 1;
        // 
        // _lblNpvCap
        // 
        _lblNpvCap.AutoSize = true;
        _lblNpvCap.Location = new Point(20, 56);
        _lblNpvCap.Name = "_lblNpvCap";
        _lblNpvCap.Size = new Size(51, 15);
        _lblNpvCap.TabIndex = 2;
        _lblNpvCap.Text = "НПВ (т):";
        // 
        // _txtNpv
        // 
        _txtNpv.Location = new Point(220, 52);
        _txtNpv.Name = "_txtNpv";
        _txtNpv.Size = new Size(200, 23);
        _txtNpv.TabIndex = 3;
        _txtNpv.Text = "150";
        // 
        // _lblDiscCap
        // 
        _lblDiscCap.AutoSize = true;
        _lblDiscCap.Location = new Point(20, 92);
        _lblDiscCap.Name = "_lblDiscCap";
        _lblDiscCap.Size = new Size(86, 15);
        _lblDiscCap.TabIndex = 4;
        _lblDiscCap.Text = "Дискретность:";
        // 
        // _cmbDisc
        // 
        _cmbDisc.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbDisc.Items.AddRange(new object[] { "0.1 т", "0.05 т", "0.01 т" });
        _cmbDisc.Location = new Point(220, 88);
        _cmbDisc.Name = "_cmbDisc";
        _cmbDisc.Size = new Size(200, 23);
        _cmbDisc.TabIndex = 5;
        // 
        // _lblZeroCap
        // 
        _lblZeroCap.AutoSize = true;
        _lblZeroCap.Location = new Point(20, 128);
        _lblZeroCap.Name = "_lblZeroCap";
        _lblZeroCap.Size = new Size(124, 15);
        _lblZeroCap.TabIndex = 6;
        _lblZeroCap.Text = "Лимит нуля (% НПВ):";
        // 
        // _txtZeroLimit
        // 
        _txtZeroLimit.Location = new Point(220, 124);
        _txtZeroLimit.Name = "_txtZeroLimit";
        _txtZeroLimit.Size = new Size(200, 23);
        _txtZeroLimit.TabIndex = 7;
        _txtZeroLimit.Text = "2";
        // 
        // _lblPasswordCap
        // 
        _lblPasswordCap.AutoSize = true;
        _lblPasswordCap.Location = new Point(20, 164);
        _lblPasswordCap.Name = "_lblPasswordCap";
        _lblPasswordCap.Size = new Size(91, 15);
        _lblPasswordCap.TabIndex = 8;
        _lblPasswordCap.Text = "Новый пароль:";
        // 
        // _txtNewPassword
        // 
        _txtNewPassword.Location = new Point(220, 160);
        _txtNewPassword.Name = "_txtNewPassword";
        _txtNewPassword.Size = new Size(200, 23);
        _txtNewPassword.TabIndex = 9;
        _txtNewPassword.UseSystemPasswordChar = true;
        // 
        // _btnSaveSettings
        // 
        _btnSaveSettings.FlatStyle = FlatStyle.Flat;
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
        ClientSize = new Size(723, 434);
        Controls.Add(_btnAdmin);
        Controls.Add(_tabs);
        MinimumSize = new Size(638, 473);
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
        _tabCalibS.ResumeLayout(false);
        _pnlCalibS.ResumeLayout(false);
        _pnlCalibSBody.ResumeLayout(false);
        _pnlCalibSBody.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvCalib).EndInit();
        _pnlCalibSHead.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        _tabCalibD.ResumeLayout(false);
        _pnlCalibD.ResumeLayout(false);
        _pnlCalibDBody.ResumeLayout(false);
        _pnlCalibDBody.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvDynCalib).EndInit();
        _pnlCalibDHead.ResumeLayout(false);
        _pnlCalibDHead.PerformLayout();
        _tabSett.ResumeLayout(false);
        _tabSett.PerformLayout();
        ResumeLayout(false);
    }

    private Button      _btnAdmin;
    private TabControl  _tabs;
    private TabPage     _tabChannel;
    private TabPage     _tabMonitor;
    private TabPage     _tabCalibS;
    private Panel       _pnlCalibS;
    private Panel       _pnlCalibSHead;
    private Panel       _pnlCalibSBody;
    private TabPage     _tabCalibD;
    private Panel       _pnlCalibD;
    private Panel       _pnlCalibDHead;
    private Panel       _pnlCalibDBody;
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
    private Label   _lblAutoCalcPlus;
    private Label   _lblCodePlusCap;
    private TextBox _txtCodePlus;
    private Button  _btnCapPlus;
    private Label   _lblMassPlusCap;
    private TextBox _txtMassPlus;
    private Button  _btnCalcPlus;
    private Label   _lblSecMinus;
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
}
