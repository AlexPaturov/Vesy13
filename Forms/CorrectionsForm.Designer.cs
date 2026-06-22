namespace Vesy13.Forms;

partial class CorrectionsForm
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
        _pnlTop = new Panel();
        _pnlTopActions = new Panel();
        _actionPanel = new Panel();
        tableLayoutPanel8 = new TableLayoutPanel();
        _btnClear = new Button();
        _btnTransfer = new Button();
        _btnSave = new Button();
        tableLayoutPanel9 = new TableLayoutPanel();
        _pnlTrainDateHost = new TableLayoutPanel();
        _dtpTrainDate = new DateTimePicker();
        _pnlTrainTimeHost = new TableLayoutPanel();
        _dtpTrainTime = new DateTimePicker();
        labTrainDate = new Label();
        label4 = new Label();
        tabPanTrainTimeDayMode = new TableLayoutPanel();
        rbTrainMode = new RadioButton();
        rbDayMode = new RadioButton();
        _btnRefresh = new Button();
        tableLayoutPanel5 = new TableLayoutPanel();
        tableLayoutPanel7 = new TableLayoutPanel();
        label3 = new Label();
        lblpotr = new Label();
        _tbCex = new TextBox();
        _tbPotr = new TextBox();
        tableLayoutPanel4 = new TableLayoutPanel();
        _rbGras = new RadioButton();
        _rbGpri = new RadioButton();
        tableLayoutPanel1 = new TableLayoutPanel();
        _pnlGruzHost = new TableLayoutPanel();
        _tbGruz = new TextBox();
        _lblGruzCap = new Label();
        _tbPlat = new TextBox();
        label2 = new Label();
        tableLayoutPanel2 = new TableLayoutPanel();
        _pnlNvagHost = new TableLayoutPanel();
        _tbNvag = new TextBox();
        _lblNvagCap = new Label();
        _lblMode = new Label();
        _lblDateCap = new Label();
        _lblModeCap = new Label();
        _lblVr = new Label();
        _lblNpp = new Label();
        _lblDt = new Label();
        _lblNppCap = new Label();
        _lblTimeCap = new Label();
        tableLayoutPanel3 = new TableLayoutPanel();
        _rbTara = new RadioButton();
        _rbBrutto = new RadioButton();
        tableLayoutPanel6 = new TableLayoutPanel();
        _lblNetto = new Label();
        _lblNettoCap = new Label();
        _lblTarCap = new Label();
        _lblBrutto = new Label();
        _lblBruttoCap = new Label();
        _pnlTarHost = new TableLayoutPanel();
        _cmbTar = new ComboBox();
        _lblVidVzv = new Label();
        _lblVidMode = new Label();
        _split = new SplitContainer();
        _gridPend = new DataGridView();
        _lblHeaderPend = new Label();
        _gridDone = new DataGridView();
        _lblHeaderDone = new Label();
        _pnlStatus = new Panel();
        _pnlTop.SuspendLayout();
        _pnlTopActions.SuspendLayout();
        _actionPanel.SuspendLayout();
        tableLayoutPanel8.SuspendLayout();
        tableLayoutPanel9.SuspendLayout();
        _pnlTrainDateHost.SuspendLayout();
        _pnlTrainTimeHost.SuspendLayout();
        tabPanTrainTimeDayMode.SuspendLayout();
        tableLayoutPanel5.SuspendLayout();
        tableLayoutPanel7.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        _pnlGruzHost.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        _pnlNvagHost.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        tableLayoutPanel6.SuspendLayout();
        _pnlTarHost.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_split).BeginInit();
        _split.Panel1.SuspendLayout();
        _split.Panel2.SuspendLayout();
        _split.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_gridPend).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_gridDone).BeginInit();
        SuspendLayout();
        // 
        // _pnlTop
        // 
        _pnlTop.Controls.Add(_pnlTopActions);
        _pnlTop.Dock = DockStyle.Top;
        _pnlTop.Location = new Point(0, 0);
        _pnlTop.Name = "_pnlTop";
        _pnlTop.Size = new Size(1264, 280);
        _pnlTop.TabIndex = 0;
        // 
        // _pnlTopActions
        // 
        _pnlTopActions.Controls.Add(_actionPanel);
        _pnlTopActions.Controls.Add(tableLayoutPanel5);
        _pnlTopActions.Dock = DockStyle.Top;
        _pnlTopActions.Location = new Point(0, 0);
        _pnlTopActions.Name = "_pnlTopActions";
        _pnlTopActions.Size = new Size(1264, 274);
        _pnlTopActions.TabIndex = 2;
        // 
        // _actionPanel
        // 
        _actionPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _actionPanel.Controls.Add(tableLayoutPanel8);
        _actionPanel.Location = new Point(2, 195);
        _actionPanel.Margin = new Padding(2);
        _actionPanel.Name = "_actionPanel";
        _actionPanel.Size = new Size(1260, 67);
        _actionPanel.TabIndex = 42;
        // 
        // tableLayoutPanel8
        // 
        tableLayoutPanel8.ColumnCount = 7;
        tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.611111F));
        tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.2777786F));
        tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.4444447F));
        tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.666667F));
        tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.11111F));
        tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.2222214F));
        tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.6111107F));
        tableLayoutPanel8.Controls.Add(_btnClear, 6, 0);
        tableLayoutPanel8.Controls.Add(_btnTransfer, 5, 0);
        tableLayoutPanel8.Controls.Add(_btnSave, 4, 0);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel9, 1, 0);
        tableLayoutPanel8.Controls.Add(tabPanTrainTimeDayMode, 0, 0);
        tableLayoutPanel8.Controls.Add(_btnRefresh, 2, 0);
        tableLayoutPanel8.Dock = DockStyle.Fill;
        tableLayoutPanel8.Location = new Point(0, 0);
        tableLayoutPanel8.Margin = new Padding(2);
        tableLayoutPanel8.Name = "tableLayoutPanel8";
        tableLayoutPanel8.RowCount = 1;
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel8.Size = new Size(1260, 67);
        tableLayoutPanel8.TabIndex = 0;
        // 
        // _btnClear
        // 
        _btnClear.Dock = DockStyle.Fill;
        _btnClear.FlatStyle = FlatStyle.Flat;
        _btnClear.Location = new Point(1138, 12);
        _btnClear.Margin = new Padding(14, 12, 14, 12);
        _btnClear.Name = "_btnClear";
        _btnClear.Size = new Size(108, 43);
        _btnClear.TabIndex = 27;
        _btnClear.Text = "Очистить";
        _btnClear.UseVisualStyleBackColor = false;
        _btnClear.Click += BtnClear_Click;
        // 
        // _btnTransfer
        // 
        _btnTransfer.Dock = DockStyle.Fill;
        _btnTransfer.Enabled = false;
        _btnTransfer.FlatAppearance.BorderSize = 0;
        _btnTransfer.FlatStyle = FlatStyle.Flat;
        _btnTransfer.Location = new Point(884, 12);
        _btnTransfer.Margin = new Padding(14, 12, 14, 12);
        _btnTransfer.Name = "_btnTransfer";
        _btnTransfer.Size = new Size(226, 43);
        _btnTransfer.TabIndex = 26;
        _btnTransfer.Text = "Обработать";
        _btnTransfer.UseVisualStyleBackColor = false;
        _btnTransfer.Click += BtnTransfer_Click;
        // 
        // _btnSave
        // 
        _btnSave.Dock = DockStyle.Fill;
        _btnSave.Enabled = false;
        _btnSave.FlatAppearance.BorderSize = 0;
        _btnSave.FlatStyle = FlatStyle.Flat;
        _btnSave.Location = new Point(618, 12);
        _btnSave.Margin = new Padding(14, 12, 14, 12);
        _btnSave.Name = "_btnSave";
        _btnSave.Size = new Size(238, 43);
        _btnSave.TabIndex = 36;
        _btnSave.Text = "Сохранить";
        _btnSave.UseVisualStyleBackColor = false;
        _btnSave.Visible = false;
        _btnSave.Click += BtnSave_Click;
        // 
        // tableLayoutPanel9
        // 
        tableLayoutPanel9.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel9.ColumnCount = 2;
        tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.69405F));
        tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64.30595F));
        tableLayoutPanel9.Controls.Add(_pnlTrainDateHost, 1, 0);
        tableLayoutPanel9.Controls.Add(_pnlTrainTimeHost, 1, 1);
        tableLayoutPanel9.Controls.Add(labTrainDate, 0, 0);
        tableLayoutPanel9.Controls.Add(label4, 0, 1);
        tableLayoutPanel9.Dock = DockStyle.Fill;
        tableLayoutPanel9.Location = new Point(110, 2);
        tableLayoutPanel9.Margin = new Padding(2);
        tableLayoutPanel9.Name = "tableLayoutPanel9";
        tableLayoutPanel9.RowCount = 2;
        tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel9.Size = new Size(239, 63);
        tableLayoutPanel9.TabIndex = 37;
        // 
        // _pnlTrainDateHost
        // 
        _pnlTrainDateHost.ColumnCount = 1;
        _pnlTrainDateHost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pnlTrainDateHost.Controls.Add(_dtpTrainDate, 0, 1);
        _pnlTrainDateHost.Dock = DockStyle.Fill;
        _pnlTrainDateHost.Location = new Point(86, 1);
        _pnlTrainDateHost.Margin = new Padding(0);
        _pnlTrainDateHost.Name = "_pnlTrainDateHost";
        _pnlTrainDateHost.RowCount = 3;
        _pnlTrainDateHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlTrainDateHost.RowStyles.Add(new RowStyle());
        _pnlTrainDateHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlTrainDateHost.Size = new Size(152, 30);
        _pnlTrainDateHost.TabIndex = 37;
        // 
        // _dtpTrainDate
        // 
        _dtpTrainDate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _dtpTrainDate.CustomFormat = "dd.MM.yyyy";
        _dtpTrainDate.Format = DateTimePickerFormat.Custom;
        _dtpTrainDate.Location = new Point(4, 3);
        _dtpTrainDate.Margin = new Padding(4, 0, 4, 0);
        _dtpTrainDate.Name = "_dtpTrainDate";
        _dtpTrainDate.Size = new Size(144, 23);
        _dtpTrainDate.TabIndex = 0;
        // 
        // _pnlTrainTimeHost
        // 
        _pnlTrainTimeHost.ColumnCount = 1;
        _pnlTrainTimeHost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pnlTrainTimeHost.Controls.Add(_dtpTrainTime, 0, 1);
        _pnlTrainTimeHost.Dock = DockStyle.Fill;
        _pnlTrainTimeHost.Location = new Point(86, 32);
        _pnlTrainTimeHost.Margin = new Padding(0);
        _pnlTrainTimeHost.Name = "_pnlTrainTimeHost";
        _pnlTrainTimeHost.RowCount = 3;
        _pnlTrainTimeHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlTrainTimeHost.RowStyles.Add(new RowStyle());
        _pnlTrainTimeHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlTrainTimeHost.Size = new Size(152, 30);
        _pnlTrainTimeHost.TabIndex = 38;
        // 
        // _dtpTrainTime
        // 
        _dtpTrainTime.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _dtpTrainTime.CustomFormat = "HH:mm:ss";
        _dtpTrainTime.Format = DateTimePickerFormat.Custom;
        _dtpTrainTime.Location = new Point(4, 3);
        _dtpTrainTime.Margin = new Padding(4, 0, 4, 0);
        _dtpTrainTime.Name = "_dtpTrainTime";
        _dtpTrainTime.ShowUpDown = true;
        _dtpTrainTime.Size = new Size(144, 23);
        _dtpTrainTime.TabIndex = 1;
        // 
        // labTrainDate
        // 
        labTrainDate.AutoSize = true;
        labTrainDate.Dock = DockStyle.Fill;
        labTrainDate.Location = new Point(4, 1);
        labTrainDate.Name = "labTrainDate";
        labTrainDate.Size = new Size(78, 30);
        labTrainDate.TabIndex = 2;
        labTrainDate.Text = "Дата сост.";
        labTrainDate.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Dock = DockStyle.Fill;
        label4.Location = new Point(4, 32);
        label4.Name = "label4";
        label4.Size = new Size(78, 30);
        label4.TabIndex = 3;
        label4.Text = "Время сост.";
        label4.TextAlign = ContentAlignment.MiddleRight;
        // 
        // tabPanTrainTimeDayMode
        // 
        tabPanTrainTimeDayMode.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tabPanTrainTimeDayMode.ColumnCount = 1;
        tabPanTrainTimeDayMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tabPanTrainTimeDayMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tabPanTrainTimeDayMode.Controls.Add(rbTrainMode, 0, 0);
        tabPanTrainTimeDayMode.Controls.Add(rbDayMode, 0, 1);
        tabPanTrainTimeDayMode.Dock = DockStyle.Fill;
        tabPanTrainTimeDayMode.Location = new Point(2, 2);
        tabPanTrainTimeDayMode.Margin = new Padding(2);
        tabPanTrainTimeDayMode.Name = "tabPanTrainTimeDayMode";
        tabPanTrainTimeDayMode.RowCount = 2;
        tabPanTrainTimeDayMode.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tabPanTrainTimeDayMode.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tabPanTrainTimeDayMode.Size = new Size(104, 63);
        tabPanTrainTimeDayMode.TabIndex = 38;
        // 
        // rbTrainMode
        // 
        rbTrainMode.AutoSize = true;
        rbTrainMode.Dock = DockStyle.Fill;
        rbTrainMode.Location = new Point(11, 3);
        rbTrainMode.Margin = new Padding(10, 2, 2, 2);
        rbTrainMode.Name = "rbTrainMode";
        rbTrainMode.Size = new Size(90, 26);
        rbTrainMode.TabIndex = 0;
        rbTrainMode.TabStop = true;
        rbTrainMode.Text = "Состав";
        rbTrainMode.UseVisualStyleBackColor = true;
        // 
        // rbDayMode
        // 
        rbDayMode.AutoSize = true;
        rbDayMode.Dock = DockStyle.Fill;
        rbDayMode.Location = new Point(11, 34);
        rbDayMode.Margin = new Padding(10, 2, 2, 2);
        rbDayMode.Name = "rbDayMode";
        rbDayMode.Size = new Size(90, 26);
        rbDayMode.TabIndex = 1;
        rbDayMode.TabStop = true;
        rbDayMode.Text = "Сутки";
        rbDayMode.UseVisualStyleBackColor = true;
        // 
        // _btnRefresh
        // 
        _btnRefresh.AutoSize = true;
        _btnRefresh.Dock = DockStyle.Fill;
        _btnRefresh.FlatStyle = FlatStyle.Flat;
        _btnRefresh.Location = new Point(365, 12);
        _btnRefresh.Margin = new Padding(14, 12, 14, 12);
        _btnRefresh.Name = "_btnRefresh";
        _btnRefresh.Size = new Size(116, 43);
        _btnRefresh.TabIndex = 28;
        _btnRefresh.Text = "Показать";
        _btnRefresh.UseVisualStyleBackColor = false;
        _btnRefresh.Click += BtnRefresh_Click;
        // 
        // tableLayoutPanel5
        // 
        tableLayoutPanel5.ColumnCount = 3;
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.2757473F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 47.23145F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.43743F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 14F));
        tableLayoutPanel5.Controls.Add(tableLayoutPanel7, 2, 0);
        tableLayoutPanel5.Controls.Add(tableLayoutPanel4, 0, 0);
        tableLayoutPanel5.Controls.Add(tableLayoutPanel1, 2, 1);
        tableLayoutPanel5.Controls.Add(tableLayoutPanel2, 1, 0);
        tableLayoutPanel5.Controls.Add(tableLayoutPanel3, 0, 1);
        tableLayoutPanel5.Controls.Add(tableLayoutPanel6, 1, 1);
        tableLayoutPanel5.Dock = DockStyle.Top;
        tableLayoutPanel5.Location = new Point(0, 0);
        tableLayoutPanel5.Margin = new Padding(2);
        tableLayoutPanel5.Name = "tableLayoutPanel5";
        tableLayoutPanel5.RowCount = 2;
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 49.4071159F));
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50.5928841F));
        tableLayoutPanel5.Size = new Size(1264, 181);
        tableLayoutPanel5.TabIndex = 41;
        // 
        // tableLayoutPanel7
        // 
        tableLayoutPanel7.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel7.ColumnCount = 2;
        tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29.33754F));
        tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70.66246F));
        tableLayoutPanel7.Controls.Add(label3, 0, 0);
        tableLayoutPanel7.Controls.Add(lblpotr, 0, 1);
        tableLayoutPanel7.Controls.Add(_tbCex, 1, 0);
        tableLayoutPanel7.Controls.Add(_tbPotr, 1, 1);
        tableLayoutPanel7.Dock = DockStyle.Fill;
        tableLayoutPanel7.Location = new Point(817, 2);
        tableLayoutPanel7.Margin = new Padding(2);
        tableLayoutPanel7.Name = "tableLayoutPanel7";
        tableLayoutPanel7.RowCount = 2;
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel7.Size = new Size(445, 85);
        tableLayoutPanel7.TabIndex = 43;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Dock = DockStyle.Fill;
        label3.Location = new Point(2, 1);
        label3.Margin = new Padding(1, 0, 1, 0);
        label3.Name = "label3";
        label3.Size = new Size(127, 41);
        label3.TabIndex = 34;
        label3.Text = "Цех";
        label3.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblpotr
        // 
        lblpotr.AutoSize = true;
        lblpotr.Dock = DockStyle.Fill;
        lblpotr.Location = new Point(1, 43);
        lblpotr.Margin = new Padding(0);
        lblpotr.Name = "lblpotr";
        lblpotr.Size = new Size(129, 41);
        lblpotr.TabIndex = 31;
        lblpotr.Text = "Потребитель";
        lblpotr.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _tbCex
        // 
        _tbCex.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _tbCex.Location = new Point(135, 10);
        _tbCex.Margin = new Padding(4, 0, 4, 0);
        _tbCex.MaxLength = 3;
        _tbCex.Name = "_tbCex";
        _tbCex.Size = new Size(305, 23);
        _tbCex.TabIndex = 33;
        // 
        // _tbPotr
        // 
        _tbPotr.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _tbPotr.Location = new Point(135, 52);
        _tbPotr.Margin = new Padding(4, 0, 4, 0);
        _tbPotr.MaxLength = 30;
        _tbPotr.Name = "_tbPotr";
        _tbPotr.Size = new Size(305, 23);
        _tbPotr.TabIndex = 29;
        // 
        // tableLayoutPanel4
        // 
        tableLayoutPanel4.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel4.ColumnCount = 2;
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel4.Controls.Add(_rbGras, 1, 0);
        tableLayoutPanel4.Controls.Add(_rbGpri, 0, 0);
        tableLayoutPanel4.Dock = DockStyle.Fill;
        tableLayoutPanel4.Location = new Point(2, 2);
        tableLayoutPanel4.Margin = new Padding(2);
        tableLayoutPanel4.Name = "tableLayoutPanel4";
        tableLayoutPanel4.RowCount = 1;
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel4.Size = new Size(214, 85);
        tableLayoutPanel4.TabIndex = 41;
        // 
        // _rbGras
        // 
        _rbGras.AutoSize = true;
        _rbGras.Dock = DockStyle.Fill;
        _rbGras.Location = new Point(117, 4);
        _rbGras.Margin = new Padding(10, 3, 3, 3);
        _rbGras.Name = "_rbGras";
        _rbGras.Size = new Size(93, 77);
        _rbGras.TabIndex = 2;
        _rbGras.Text = "Расход";
        // 
        // _rbGpri
        // 
        _rbGpri.AutoSize = true;
        _rbGpri.Checked = true;
        _rbGpri.Dock = DockStyle.Fill;
        _rbGpri.Location = new Point(11, 4);
        _rbGpri.Margin = new Padding(10, 3, 3, 3);
        _rbGpri.Name = "_rbGpri";
        _rbGpri.Size = new Size(92, 77);
        _rbGpri.TabIndex = 1;
        _rbGpri.TabStop = true;
        _rbGpri.Text = "Приход";
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29.1798115F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70.82019F));
        tableLayoutPanel1.Controls.Add(_pnlGruzHost, 1, 1);
        tableLayoutPanel1.Controls.Add(_lblGruzCap, 0, 1);
        tableLayoutPanel1.Controls.Add(_tbPlat, 1, 0);
        tableLayoutPanel1.Controls.Add(label2, 0, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(817, 91);
        tableLayoutPanel1.Margin = new Padding(2);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Size = new Size(445, 88);
        tableLayoutPanel1.TabIndex = 38;
        // 
        // _pnlGruzHost
        // 
        _pnlGruzHost.ColumnCount = 1;
        _pnlGruzHost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pnlGruzHost.Controls.Add(_tbGruz, 0, 1);
        _pnlGruzHost.Dock = DockStyle.Fill;
        _pnlGruzHost.Location = new Point(130, 44);
        _pnlGruzHost.Margin = new Padding(0);
        _pnlGruzHost.Name = "_pnlGruzHost";
        _pnlGruzHost.RowCount = 3;
        _pnlGruzHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlGruzHost.RowStyles.Add(new RowStyle());
        _pnlGruzHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlGruzHost.Size = new Size(314, 43);
        _pnlGruzHost.TabIndex = 15;
        // 
        // _tbGruz
        // 
        _tbGruz.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _tbGruz.Location = new Point(4, 10);
        _tbGruz.Margin = new Padding(4, 0, 4, 0);
        _tbGruz.MaxLength = 20;
        _tbGruz.Name = "_tbGruz";
        _tbGruz.Size = new Size(306, 23);
        _tbGruz.TabIndex = 14;
        // 
        // _lblGruzCap
        // 
        _lblGruzCap.AutoSize = true;
        _lblGruzCap.Dock = DockStyle.Fill;
        _lblGruzCap.Location = new Point(4, 44);
        _lblGruzCap.Name = "_lblGruzCap";
        _lblGruzCap.Size = new Size(122, 43);
        _lblGruzCap.TabIndex = 13;
        _lblGruzCap.Text = "Груз";
        _lblGruzCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _tbPlat
        // 
        _tbPlat.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _tbPlat.Location = new Point(134, 10);
        _tbPlat.Margin = new Padding(4, 0, 4, 0);
        _tbPlat.MaxLength = 30;
        _tbPlat.Name = "_tbPlat";
        _tbPlat.Size = new Size(306, 23);
        _tbPlat.TabIndex = 30;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Dock = DockStyle.Fill;
        label2.Location = new Point(1, 1);
        label2.Margin = new Padding(0);
        label2.Name = "label2";
        label2.Size = new Size(128, 42);
        label2.TabIndex = 32;
        label2.Text = "Плательщик";
        label2.TextAlign = ContentAlignment.MiddleRight;
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel2.ColumnCount = 4;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.4989948F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.9976349F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.68558F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.12293F));
        tableLayoutPanel2.Controls.Add(_pnlNvagHost, 1, 2);
        tableLayoutPanel2.Controls.Add(_lblNvagCap, 0, 2);
        tableLayoutPanel2.Controls.Add(_lblMode, 3, 1);
        tableLayoutPanel2.Controls.Add(_lblDateCap, 0, 0);
        tableLayoutPanel2.Controls.Add(_lblModeCap, 2, 1);
        tableLayoutPanel2.Controls.Add(_lblVr, 1, 1);
        tableLayoutPanel2.Controls.Add(_lblNpp, 3, 0);
        tableLayoutPanel2.Controls.Add(_lblDt, 1, 0);
        tableLayoutPanel2.Controls.Add(_lblNppCap, 2, 0);
        tableLayoutPanel2.Controls.Add(_lblTimeCap, 0, 1);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Location = new Point(220, 2);
        tableLayoutPanel2.Margin = new Padding(2);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 3;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
        tableLayoutPanel2.Size = new Size(593, 85);
        tableLayoutPanel2.TabIndex = 39;
        // 
        // _pnlNvagHost
        // 
        _pnlNvagHost.ColumnCount = 1;
        _pnlNvagHost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pnlNvagHost.Controls.Add(_tbNvag, 0, 1);
        _pnlNvagHost.Dock = DockStyle.Fill;
        _pnlNvagHost.Location = new Point(98, 53);
        _pnlNvagHost.Margin = new Padding(0);
        _pnlNvagHost.Name = "_pnlNvagHost";
        _pnlNvagHost.RowCount = 3;
        _pnlNvagHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlNvagHost.RowStyles.Add(new RowStyle());
        _pnlNvagHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlNvagHost.Size = new Size(216, 31);
        _pnlNvagHost.TabIndex = 11;
        // 
        // _tbNvag
        // 
        _tbNvag.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _tbNvag.Location = new Point(4, 4);
        _tbNvag.Margin = new Padding(4, 0, 4, 0);
        _tbNvag.MaxLength = 10;
        _tbNvag.Name = "_tbNvag";
        _tbNvag.Size = new Size(208, 23);
        _tbNvag.TabIndex = 10;
        _tbNvag.Leave += TxtNvag_Leave;
        // 
        // _lblNvagCap
        // 
        _lblNvagCap.AutoSize = true;
        _lblNvagCap.Dock = DockStyle.Fill;
        _lblNvagCap.Location = new Point(4, 53);
        _lblNvagCap.Name = "_lblNvagCap";
        _lblNvagCap.Size = new Size(90, 31);
        _lblNvagCap.TabIndex = 9;
        _lblNvagCap.Text = "№ вагона";
        _lblNvagCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblMode
        // 
        _lblMode.Dock = DockStyle.Fill;
        _lblMode.Location = new Point(437, 27);
        _lblMode.Margin = new Padding(0);
        _lblMode.Name = "_lblMode";
        _lblMode.Size = new Size(155, 25);
        _lblMode.TabIndex = 8;
        _lblMode.Text = "—";
        _lblMode.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblDateCap
        // 
        _lblDateCap.Dock = DockStyle.Fill;
        _lblDateCap.Location = new Point(1, 1);
        _lblDateCap.Margin = new Padding(0);
        _lblDateCap.Name = "_lblDateCap";
        _lblDateCap.Size = new Size(96, 25);
        _lblDateCap.TabIndex = 1;
        _lblDateCap.Text = "Дата сост.";
        _lblDateCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblModeCap
        // 
        _lblModeCap.Dock = DockStyle.Fill;
        _lblModeCap.Location = new Point(315, 27);
        _lblModeCap.Margin = new Padding(0);
        _lblModeCap.Name = "_lblModeCap";
        _lblModeCap.Size = new Size(121, 25);
        _lblModeCap.TabIndex = 7;
        _lblModeCap.Text = "Режим";
        _lblModeCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblVr
        // 
        _lblVr.Dock = DockStyle.Fill;
        _lblVr.Location = new Point(98, 27);
        _lblVr.Margin = new Padding(0);
        _lblVr.Name = "_lblVr";
        _lblVr.Size = new Size(216, 25);
        _lblVr.TabIndex = 4;
        _lblVr.Text = "—";
        _lblVr.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblNpp
        // 
        _lblNpp.Dock = DockStyle.Fill;
        _lblNpp.Location = new Point(437, 1);
        _lblNpp.Margin = new Padding(0);
        _lblNpp.Name = "_lblNpp";
        _lblNpp.Size = new Size(155, 25);
        _lblNpp.TabIndex = 6;
        _lblNpp.Text = "—";
        _lblNpp.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblDt
        // 
        _lblDt.Dock = DockStyle.Fill;
        _lblDt.Location = new Point(98, 1);
        _lblDt.Margin = new Padding(0);
        _lblDt.Name = "_lblDt";
        _lblDt.Padding = new Padding(0, 0, 0, 2);
        _lblDt.Size = new Size(216, 25);
        _lblDt.TabIndex = 2;
        _lblDt.Text = "—";
        _lblDt.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblNppCap
        // 
        _lblNppCap.Dock = DockStyle.Fill;
        _lblNppCap.Location = new Point(315, 1);
        _lblNppCap.Margin = new Padding(0);
        _lblNppCap.Name = "_lblNppCap";
        _lblNppCap.Size = new Size(121, 25);
        _lblNppCap.TabIndex = 5;
        _lblNppCap.Text = "№п/п";
        _lblNppCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblTimeCap
        // 
        _lblTimeCap.Dock = DockStyle.Fill;
        _lblTimeCap.Location = new Point(1, 27);
        _lblTimeCap.Margin = new Padding(0);
        _lblTimeCap.Name = "_lblTimeCap";
        _lblTimeCap.Size = new Size(96, 25);
        _lblTimeCap.TabIndex = 3;
        _lblTimeCap.Text = "Время сост.";
        _lblTimeCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel3.ColumnCount = 2;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.Controls.Add(_rbTara, 1, 0);
        tableLayoutPanel3.Controls.Add(_rbBrutto, 0, 0);
        tableLayoutPanel3.Dock = DockStyle.Fill;
        tableLayoutPanel3.Location = new Point(2, 91);
        tableLayoutPanel3.Margin = new Padding(2);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 1;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.Size = new Size(214, 88);
        tableLayoutPanel3.TabIndex = 40;
        // 
        // _rbTara
        // 
        _rbTara.AutoSize = true;
        _rbTara.Dock = DockStyle.Fill;
        _rbTara.Location = new Point(117, 4);
        _rbTara.Margin = new Padding(10, 3, 3, 3);
        _rbTara.Name = "_rbTara";
        _rbTara.Size = new Size(93, 80);
        _rbTara.TabIndex = 2;
        _rbTara.Text = "Тара";
        _rbTara.CheckedChanged += RbTara_CheckedChanged;
        // 
        // _rbBrutto
        // 
        _rbBrutto.AutoSize = true;
        _rbBrutto.Checked = true;
        _rbBrutto.Dock = DockStyle.Fill;
        _rbBrutto.Location = new Point(11, 4);
        _rbBrutto.Margin = new Padding(10, 3, 3, 3);
        _rbBrutto.Name = "_rbBrutto";
        _rbBrutto.Size = new Size(92, 80);
        _rbBrutto.TabIndex = 1;
        _rbBrutto.TabStop = true;
        _rbBrutto.Text = "Брутто";
        _rbBrutto.CheckedChanged += RbTara_CheckedChanged;
        // 
        // tableLayoutPanel6
        // 
        tableLayoutPanel6.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel6.ColumnCount = 2;
        tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.89083F));
        tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.10917F));
        tableLayoutPanel6.Controls.Add(_lblNetto, 1, 2);
        tableLayoutPanel6.Controls.Add(_lblNettoCap, 0, 2);
        tableLayoutPanel6.Controls.Add(_lblTarCap, 0, 1);
        tableLayoutPanel6.Controls.Add(_lblBrutto, 1, 0);
        tableLayoutPanel6.Controls.Add(_lblBruttoCap, 0, 0);
        tableLayoutPanel6.Controls.Add(_pnlTarHost, 1, 1);
        tableLayoutPanel6.Dock = DockStyle.Fill;
        tableLayoutPanel6.Location = new Point(220, 91);
        tableLayoutPanel6.Margin = new Padding(2);
        tableLayoutPanel6.Name = "tableLayoutPanel6";
        tableLayoutPanel6.RowCount = 3;
        tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel6.Size = new Size(593, 88);
        tableLayoutPanel6.TabIndex = 42;
        // 
        // _lblNetto
        // 
        _lblNetto.AutoSize = true;
        _lblNetto.Dock = DockStyle.Fill;
        _lblNetto.Location = new Point(151, 62);
        _lblNetto.Name = "_lblNetto";
        _lblNetto.Size = new Size(438, 25);
        _lblNetto.TabIndex = 24;
        _lblNetto.Text = "—";
        _lblNetto.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblNettoCap
        // 
        _lblNettoCap.AutoSize = true;
        _lblNettoCap.Dock = DockStyle.Fill;
        _lblNettoCap.Location = new Point(4, 62);
        _lblNettoCap.Name = "_lblNettoCap";
        _lblNettoCap.Size = new Size(140, 25);
        _lblNettoCap.TabIndex = 23;
        _lblNettoCap.Text = "Нетто";
        _lblNettoCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblTarCap
        // 
        _lblTarCap.AutoSize = true;
        _lblTarCap.Dock = DockStyle.Fill;
        _lblTarCap.Location = new Point(4, 27);
        _lblTarCap.Name = "_lblTarCap";
        _lblTarCap.Size = new Size(140, 34);
        _lblTarCap.TabIndex = 20;
        _lblTarCap.Text = "Тара";
        _lblTarCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblBrutto
        // 
        _lblBrutto.AutoSize = true;
        _lblBrutto.Dock = DockStyle.Fill;
        _lblBrutto.Location = new Point(151, 1);
        _lblBrutto.Name = "_lblBrutto";
        _lblBrutto.Size = new Size(438, 25);
        _lblBrutto.TabIndex = 18;
        _lblBrutto.Text = "—";
        _lblBrutto.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblBruttoCap
        // 
        _lblBruttoCap.AutoSize = true;
        _lblBruttoCap.Dock = DockStyle.Fill;
        _lblBruttoCap.Location = new Point(4, 1);
        _lblBruttoCap.Name = "_lblBruttoCap";
        _lblBruttoCap.Size = new Size(140, 25);
        _lblBruttoCap.TabIndex = 17;
        _lblBruttoCap.Text = "Брутто";
        _lblBruttoCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _pnlTarHost
        // 
        _pnlTarHost.ColumnCount = 1;
        _pnlTarHost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pnlTarHost.Controls.Add(_cmbTar, 0, 1);
        _pnlTarHost.Dock = DockStyle.Fill;
        _pnlTarHost.Location = new Point(148, 27);
        _pnlTarHost.Margin = new Padding(0);
        _pnlTarHost.Name = "_pnlTarHost";
        _pnlTarHost.RowCount = 3;
        _pnlTarHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlTarHost.RowStyles.Add(new RowStyle());
        _pnlTarHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlTarHost.Size = new Size(444, 34);
        _pnlTarHost.TabIndex = 22;
        // 
        // _cmbTar
        // 
        _cmbTar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cmbTar.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbTar.Location = new Point(4, 3);
        _cmbTar.Margin = new Padding(4, 0, 4, 4);
        _cmbTar.Name = "_cmbTar";
        _cmbTar.Size = new Size(436, 23);
        _cmbTar.TabIndex = 21;
        _cmbTar.SelectedIndexChanged += CmbTar_SelectedIndexChanged;
        // 
        // _lblVidVzv
        // 
        _lblVidVzv.Location = new Point(0, 0);
        _lblVidVzv.Name = "_lblVidVzv";
        _lblVidVzv.Size = new Size(100, 23);
        _lblVidVzv.TabIndex = 0;
        // 
        // _lblVidMode
        // 
        _lblVidMode.Location = new Point(0, 0);
        _lblVidMode.Name = "_lblVidMode";
        _lblVidMode.Size = new Size(100, 23);
        _lblVidMode.TabIndex = 0;
        // 
        // _split
        // 
        _split.Dock = DockStyle.Fill;
        _split.FixedPanel = FixedPanel.Panel1;
        _split.Location = new Point(0, 280);
        _split.Name = "_split";
        // 
        // _split.Panel1
        // 
        _split.Panel1.Controls.Add(_gridPend);
        _split.Panel1.Controls.Add(_lblHeaderPend);
        _split.Panel1MinSize = 488;
        // 
        // _split.Panel2
        // 
        _split.Panel2.Controls.Add(_gridDone);
        _split.Panel2.Controls.Add(_lblHeaderDone);
        _split.Panel2MinSize = 280;
        _split.Size = new Size(1264, 317);
        _split.SplitterDistance = 488;
        _split.TabIndex = 1;
        // 
        // _gridPend
        // 
        _gridPend.AllowUserToAddRows = false;
        _gridPend.AllowUserToDeleteRows = false;
        _gridPend.AllowUserToResizeRows = false;
        _gridPend.BackgroundColor = Color.FromArgb(245, 245, 247);
        _gridPend.BorderStyle = BorderStyle.None;
        _gridPend.ColumnHeadersHeight = 32;
        _gridPend.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _gridPend.Dock = DockStyle.Fill;
        _gridPend.EnableHeadersVisualStyles = false;
        _gridPend.GridColor = Color.FromArgb(212, 216, 222);
        _gridPend.Location = new Point(0, 24);
        _gridPend.MultiSelect = false;
        _gridPend.Name = "_gridPend";
        _gridPend.ReadOnly = true;
        _gridPend.RowHeadersVisible = false;
        _gridPend.RowHeadersWidth = 51;
        _gridPend.RowTemplate.Height = 28;
        _gridPend.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gridPend.Size = new Size(488, 293);
        _gridPend.TabIndex = 0;
        _gridPend.SelectionChanged += GridPend_SelectionChanged;
        // 
        // _lblHeaderPend
        // 
        _lblHeaderPend.Dock = DockStyle.Top;
        _lblHeaderPend.Location = new Point(0, 0);
        _lblHeaderPend.Name = "_lblHeaderPend";
        _lblHeaderPend.Size = new Size(488, 24);
        _lblHeaderPend.TabIndex = 1;
        _lblHeaderPend.Text = "  Не обработанные";
        _lblHeaderPend.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _gridDone
        // 
        _gridDone.AllowUserToAddRows = false;
        _gridDone.AllowUserToDeleteRows = false;
        _gridDone.AllowUserToResizeRows = false;
        _gridDone.BackgroundColor = Color.FromArgb(245, 245, 247);
        _gridDone.BorderStyle = BorderStyle.None;
        _gridDone.ColumnHeadersHeight = 32;
        _gridDone.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _gridDone.Dock = DockStyle.Fill;
        _gridDone.EnableHeadersVisualStyles = false;
        _gridDone.GridColor = Color.FromArgb(212, 216, 222);
        _gridDone.Location = new Point(0, 24);
        _gridDone.MultiSelect = false;
        _gridDone.Name = "_gridDone";
        _gridDone.ReadOnly = true;
        _gridDone.RowHeadersVisible = false;
        _gridDone.RowHeadersWidth = 51;
        _gridDone.RowTemplate.Height = 28;
        _gridDone.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gridDone.Size = new Size(772, 293);
        _gridDone.TabIndex = 0;
        _gridDone.SelectionChanged += GridDone_SelectionChanged;
        // 
        // _lblHeaderDone
        // 
        _lblHeaderDone.Dock = DockStyle.Top;
        _lblHeaderDone.Location = new Point(0, 0);
        _lblHeaderDone.Name = "_lblHeaderDone";
        _lblHeaderDone.Size = new Size(772, 24);
        _lblHeaderDone.TabIndex = 1;
        _lblHeaderDone.Text = "Обработанные";
        _lblHeaderDone.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _pnlStatus
        // 
        _pnlStatus.Dock = DockStyle.Bottom;
        _pnlStatus.Location = new Point(0, 597);
        _pnlStatus.Name = "_pnlStatus";
        _pnlStatus.Size = new Size(1264, 4);
        _pnlStatus.TabIndex = 2;
        // 
        // CorrectionsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1264, 601);
        Controls.Add(_split);
        Controls.Add(_pnlTop);
        Controls.Add(_pnlStatus);
        MinimumSize = new Size(852, 532);
        Name = "CorrectionsForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Корректировки — перенос в учётную систему";
        _pnlTop.ResumeLayout(false);
        _pnlTopActions.ResumeLayout(false);
        _actionPanel.ResumeLayout(false);
        tableLayoutPanel8.ResumeLayout(false);
        tableLayoutPanel8.PerformLayout();
        tableLayoutPanel9.ResumeLayout(false);
        tableLayoutPanel9.PerformLayout();
        _pnlTrainDateHost.ResumeLayout(false);
        _pnlTrainTimeHost.ResumeLayout(false);
        tabPanTrainTimeDayMode.ResumeLayout(false);
        tabPanTrainTimeDayMode.PerformLayout();
        tableLayoutPanel5.ResumeLayout(false);
        tableLayoutPanel7.ResumeLayout(false);
        tableLayoutPanel7.PerformLayout();
        tableLayoutPanel4.ResumeLayout(false);
        tableLayoutPanel4.PerformLayout();
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        _pnlGruzHost.ResumeLayout(false);
        _pnlGruzHost.PerformLayout();
        tableLayoutPanel2.ResumeLayout(false);
        tableLayoutPanel2.PerformLayout();
        _pnlNvagHost.ResumeLayout(false);
        _pnlNvagHost.PerformLayout();
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel3.PerformLayout();
        tableLayoutPanel6.ResumeLayout(false);
        tableLayoutPanel6.PerformLayout();
        _pnlTarHost.ResumeLayout(false);
        _split.Panel1.ResumeLayout(false);
        _split.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_split).EndInit();
        _split.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_gridPend).EndInit();
        ((System.ComponentModel.ISupportInitialize)_gridDone).EndInit();
        ResumeLayout(false);
    }

    private Panel          _pnlTop;
    private Panel          _pnlTopActions;
    private Panel          _pnlStatus;
    private Label          _lblNvagCap;
    private TextBox        _tbNvag;
    private TableLayoutPanel _pnlNvagHost;
    private Label          _lblGruzCap;
    private TextBox        _tbGruz;
    private Label          _lblBruttoCap;
    private Label          _lblBrutto;
    private Label          _lblTarCap;
    private ComboBox       _cmbTar;
    private TableLayoutPanel _pnlTarHost;
    private Label          _lblNettoCap;
    private Label          _lblNetto;
    private Button         _btnTransfer;
    private Button         _btnClear;
    private Button         _btnRefresh;
    private SplitContainer _split;
    private Label          _lblHeaderPend;
    private DataGridView   _gridPend;
    private Label          _lblHeaderDone;
    private DataGridView   _gridDone;
    private Label          _lblVidMode;
    private Label          _lblVidVzv;
    private Button         _btnSave;
    private TableLayoutPanel tableLayoutPanel3;
    private Label _lblMode;
    private Label _lblModeCap;
    private Label _lblNpp;
    private Label _lblNppCap;
    private TableLayoutPanel tableLayoutPanel4;
    private RadioButton _rbTara;
    private RadioButton _rbBrutto;
    private RadioButton _rbGras;
    private RadioButton _rbGpri;
    private TableLayoutPanel tableLayoutPanel2;
    private Label _lblDateCap;
    private Label _lblVr;
    private Label _lblDt;
    private Label _lblTimeCap;
    private TableLayoutPanel tableLayoutPanel5;
    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel _pnlGruzHost;
    private Label label2;
    private Label lblpotr;
    private TextBox _tbPlat;
    private TextBox _tbCex;
    private TextBox _tbPotr;
    private Label label3;
    private TableLayoutPanel tableLayoutPanel6;
    private TableLayoutPanel tableLayoutPanel7;
    private Panel _actionPanel;
    private TableLayoutPanel tableLayoutPanel8;
    private TableLayoutPanel tableLayoutPanel9;
    private TableLayoutPanel _pnlTrainDateHost;
    private TableLayoutPanel _pnlTrainTimeHost;
    private DateTimePicker _dtpTrainDate;
    private DateTimePicker _dtpTrainTime;
    private Label labTrainDate;
    private Label label4;
    private TableLayoutPanel tabPanTrainTimeDayMode;
    private RadioButton rbTrainMode;
    private RadioButton rbDayMode;
}
