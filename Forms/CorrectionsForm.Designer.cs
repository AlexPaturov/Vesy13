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
        _pnlTopActionsLayout = new FlowLayoutPanel();
        _btnSave = new Button();
        _btnTransfer = new Button();
        _btnClear = new Button();
        _btnRefresh = new Button();
        _pnlTopActions = new Panel();
        tableLayoutPanel5 = new TableLayoutPanel();
        tableLayoutPanel7 = new TableLayoutPanel();
        label3 = new Label();
        lblpotr = new Label();
        tbCex = new TextBox();
        _tbPotr = new TextBox();
        tableLayoutPanel4 = new TableLayoutPanel();
        _rbGras = new RadioButton();
        _rbGpri = new RadioButton();
        tableLayoutPanel1 = new TableLayoutPanel();
        _txtGruz = new TextBox();
        _lblGruzCap = new Label();
        _tbPlat = new TextBox();
        label2 = new Label();
        tableLayoutPanel2 = new TableLayoutPanel();
        _txtNvag = new TextBox();
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
        _pnlTopActionsLayout.SuspendLayout();
        _pnlTopActions.SuspendLayout();
        tableLayoutPanel5.SuspendLayout();
        tableLayoutPanel7.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        tableLayoutPanel6.SuspendLayout();
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
        _pnlTop.Controls.Add(_pnlTopActionsLayout);
        _pnlTop.Controls.Add(_pnlTopActions);
        _pnlTop.Dock = DockStyle.Top;
        _pnlTop.Location = new Point(0, 0);
        _pnlTop.Margin = new Padding(4, 5, 4, 5);
        _pnlTop.Name = "_pnlTop";
        _pnlTop.Size = new Size(1806, 543);
        _pnlTop.TabIndex = 0;
        // 
        // _pnlTopActionsLayout
        // 
        _pnlTopActionsLayout.AutoSize = true;
        _pnlTopActionsLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _pnlTopActionsLayout.Controls.Add(_btnSave);
        _pnlTopActionsLayout.Controls.Add(_btnTransfer);
        _pnlTopActionsLayout.Controls.Add(_btnClear);
        _pnlTopActionsLayout.Controls.Add(_btnRefresh);
        _pnlTopActionsLayout.Location = new Point(146, 440);
        _pnlTopActionsLayout.Margin = new Padding(0);
        _pnlTopActionsLayout.Name = "_pnlTopActionsLayout";
        _pnlTopActionsLayout.Padding = new Padding(211, 7, 0, 7);
        _pnlTopActionsLayout.Size = new Size(1271, 67);
        _pnlTopActionsLayout.TabIndex = 0;
        _pnlTopActionsLayout.WrapContents = false;
        // 
        // _btnSave
        // 
        _btnSave.Enabled = false;
        _btnSave.FlatAppearance.BorderSize = 0;
        _btnSave.FlatStyle = FlatStyle.Flat;
        _btnSave.Location = new Point(211, 7);
        _btnSave.Margin = new Padding(0, 0, 20, 0);
        _btnSave.Name = "_btnSave";
        _btnSave.Size = new Size(343, 53);
        _btnSave.TabIndex = 36;
        _btnSave.Text = "Сохранить";
        _btnSave.UseVisualStyleBackColor = false;
        _btnSave.Visible = false;
        _btnSave.Click += BtnSave_Click;
        // 
        // _btnTransfer
        // 
        _btnTransfer.Enabled = false;
        _btnTransfer.FlatAppearance.BorderSize = 0;
        _btnTransfer.FlatStyle = FlatStyle.Flat;
        _btnTransfer.Location = new Point(574, 7);
        _btnTransfer.Margin = new Padding(0, 0, 20, 0);
        _btnTransfer.Name = "_btnTransfer";
        _btnTransfer.Size = new Size(343, 53);
        _btnTransfer.TabIndex = 26;
        _btnTransfer.Text = "Перенести";
        _btnTransfer.UseVisualStyleBackColor = false;
        _btnTransfer.Click += BtnTransfer_Click;
        // 
        // _btnClear
        // 
        _btnClear.FlatStyle = FlatStyle.Flat;
        _btnClear.Location = new Point(937, 7);
        _btnClear.Margin = new Padding(0, 0, 20, 0);
        _btnClear.Name = "_btnClear";
        _btnClear.Size = new Size(157, 53);
        _btnClear.TabIndex = 27;
        _btnClear.Text = "Очистить";
        _btnClear.UseVisualStyleBackColor = false;
        _btnClear.Click += BtnClear_Click;
        // 
        // _btnRefresh
        // 
        _btnRefresh.FlatStyle = FlatStyle.Flat;
        _btnRefresh.Location = new Point(1114, 7);
        _btnRefresh.Margin = new Padding(0);
        _btnRefresh.Name = "_btnRefresh";
        _btnRefresh.Size = new Size(157, 53);
        _btnRefresh.TabIndex = 28;
        _btnRefresh.Text = "Обновить";
        _btnRefresh.UseVisualStyleBackColor = false;
        _btnRefresh.Click += BtnRefresh_Click;
        // 
        // _pnlTopActions
        // 
        _pnlTopActions.Controls.Add(tableLayoutPanel5);
        _pnlTopActions.Dock = DockStyle.Top;
        _pnlTopActions.Location = new Point(0, 0);
        _pnlTopActions.Margin = new Padding(4, 5, 4, 5);
        _pnlTopActions.Name = "_pnlTopActions";
        _pnlTopActions.Size = new Size(1806, 305);
        _pnlTopActions.TabIndex = 2;
        // 
        // tableLayoutPanel5
        // 
        tableLayoutPanel5.ColumnCount = 3;
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.2757473F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 47.23145F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.43743F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tableLayoutPanel5.Controls.Add(tableLayoutPanel7, 2, 0);
        tableLayoutPanel5.Controls.Add(tableLayoutPanel4, 0, 0);
        tableLayoutPanel5.Controls.Add(tableLayoutPanel1, 2, 1);
        tableLayoutPanel5.Controls.Add(tableLayoutPanel2, 1, 0);
        tableLayoutPanel5.Controls.Add(tableLayoutPanel3, 0, 1);
        tableLayoutPanel5.Controls.Add(tableLayoutPanel6, 1, 1);
        tableLayoutPanel5.Dock = DockStyle.Top;
        tableLayoutPanel5.Location = new Point(0, 0);
        tableLayoutPanel5.Name = "tableLayoutPanel5";
        tableLayoutPanel5.RowCount = 2;
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 49.4071159F));
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50.5928841F));
        tableLayoutPanel5.Size = new Size(1806, 302);
        tableLayoutPanel5.TabIndex = 41;
        // 
        // tableLayoutPanel7
        // 
        tableLayoutPanel7.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel7.ColumnCount = 2;
        tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.462925F));
        tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68.53707F));
        tableLayoutPanel7.Controls.Add(label3, 0, 0);
        tableLayoutPanel7.Controls.Add(lblpotr, 0, 1);
        tableLayoutPanel7.Controls.Add(tbCex, 1, 0);
        tableLayoutPanel7.Controls.Add(_tbPotr, 1, 1);
        tableLayoutPanel7.Dock = DockStyle.Fill;
        tableLayoutPanel7.Location = new Point(1168, 3);
        tableLayoutPanel7.Name = "tableLayoutPanel7";
        tableLayoutPanel7.RowCount = 2;
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel7.Size = new Size(635, 143);
        tableLayoutPanel7.TabIndex = 43;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Dock = DockStyle.Fill;
        label3.Location = new Point(3, 1);
        label3.Margin = new Padding(2, 0, 2, 0);
        label3.Name = "label3";
        label3.Size = new Size(194, 70);
        label3.TabIndex = 34;
        label3.Text = "Цех";
        label3.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblpotr
        // 
        lblpotr.AutoSize = true;
        lblpotr.Dock = DockStyle.Fill;
        lblpotr.Location = new Point(1, 72);
        lblpotr.Margin = new Padding(0);
        lblpotr.Name = "lblpotr";
        lblpotr.Size = new Size(198, 70);
        lblpotr.TabIndex = 31;
        lblpotr.Text = "Потребитель";
        lblpotr.TextAlign = ContentAlignment.MiddleRight;
        // 
        // tbCex
        // 
        tbCex.Anchor = AnchorStyles.None;
        tbCex.Location = new Point(255, 20);
        tbCex.Margin = new Padding(4, 5, 4, 5);
        tbCex.MaxLength = 3;
        tbCex.Name = "tbCex";
        tbCex.Size = new Size(324, 31);
        tbCex.TabIndex = 33;
        // 
        // _tbPotr
        // 
        _tbPotr.Anchor = AnchorStyles.None;
        _tbPotr.Location = new Point(255, 91);
        _tbPotr.Margin = new Padding(4, 5, 4, 5);
        _tbPotr.Name = "_tbPotr";
        _tbPotr.Size = new Size(324, 31);
        _tbPotr.TabIndex = 29;
        _tbPotr.TextAlign = HorizontalAlignment.Center;
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
        tableLayoutPanel4.Location = new Point(3, 3);
        tableLayoutPanel4.Name = "tableLayoutPanel4";
        tableLayoutPanel4.RowCount = 1;
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel4.Size = new Size(306, 143);
        tableLayoutPanel4.TabIndex = 41;
        // 
        // _rbGras
        // 
        _rbGras.AutoSize = true;
        _rbGras.Dock = DockStyle.Fill;
        _rbGras.Location = new Point(168, 6);
        _rbGras.Margin = new Padding(15, 5, 4, 5);
        _rbGras.Name = "_rbGras";
        _rbGras.Size = new Size(133, 131);
        _rbGras.TabIndex = 2;
        _rbGras.Text = "Расход";
        // 
        // _rbGpri
        // 
        _rbGpri.AutoSize = true;
        _rbGpri.Checked = true;
        _rbGpri.Dock = DockStyle.Fill;
        _rbGpri.Location = new Point(16, 6);
        _rbGpri.Margin = new Padding(15, 5, 4, 5);
        _rbGpri.Name = "_rbGpri";
        _rbGpri.Size = new Size(132, 131);
        _rbGpri.TabIndex = 1;
        _rbGpri.TabStop = true;
        _rbGpri.Text = "Приход";
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29.5081959F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70.491806F));
        tableLayoutPanel1.Controls.Add(_txtGruz, 1, 1);
        tableLayoutPanel1.Controls.Add(_lblGruzCap, 0, 1);
        tableLayoutPanel1.Controls.Add(_tbPlat, 1, 0);
        tableLayoutPanel1.Controls.Add(label2, 0, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(1168, 152);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        tableLayoutPanel1.Size = new Size(635, 147);
        tableLayoutPanel1.TabIndex = 38;
        // 
        // _txtGruz
        // 
        _txtGruz.Anchor = AnchorStyles.None;
        _txtGruz.Location = new Point(239, 94);
        _txtGruz.Margin = new Padding(4, 5, 4, 5);
        _txtGruz.Name = "_txtGruz";
        _txtGruz.Size = new Size(344, 31);
        _txtGruz.TabIndex = 14;
        // 
        // _lblGruzCap
        // 
        _lblGruzCap.AutoSize = true;
        _lblGruzCap.Dock = DockStyle.Fill;
        _lblGruzCap.Location = new Point(5, 74);
        _lblGruzCap.Margin = new Padding(4, 0, 4, 0);
        _lblGruzCap.Name = "_lblGruzCap";
        _lblGruzCap.Size = new Size(178, 72);
        _lblGruzCap.TabIndex = 13;
        _lblGruzCap.Text = "Груз:";
        _lblGruzCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _tbPlat
        // 
        _tbPlat.Anchor = AnchorStyles.None;
        _tbPlat.Location = new Point(239, 21);
        _tbPlat.Margin = new Padding(4, 5, 4, 5);
        _tbPlat.Name = "_tbPlat";
        _tbPlat.Size = new Size(344, 31);
        _tbPlat.TabIndex = 30;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Dock = DockStyle.Fill;
        label2.Location = new Point(1, 1);
        label2.Margin = new Padding(0);
        label2.Name = "label2";
        label2.Size = new Size(186, 72);
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
        tableLayoutPanel2.Controls.Add(_txtNvag, 1, 2);
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
        tableLayoutPanel2.Location = new Point(315, 3);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 3;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel2.Size = new Size(847, 143);
        tableLayoutPanel2.TabIndex = 39;
        // 
        // _txtNvag
        // 
        _txtNvag.Dock = DockStyle.Fill;
        _txtNvag.Location = new Point(144, 100);
        _txtNvag.Margin = new Padding(4, 5, 4, 5);
        _txtNvag.Name = "_txtNvag";
        _txtNvag.Size = new Size(302, 31);
        _txtNvag.TabIndex = 10;
        _txtNvag.Leave += TxtNvag_Leave;
        // 
        // _lblNvagCap
        // 
        _lblNvagCap.AutoSize = true;
        _lblNvagCap.Dock = DockStyle.Fill;
        _lblNvagCap.Location = new Point(5, 95);
        _lblNvagCap.Margin = new Padding(4, 0, 4, 0);
        _lblNvagCap.Name = "_lblNvagCap";
        _lblNvagCap.Size = new Size(130, 47);
        _lblNvagCap.TabIndex = 9;
        _lblNvagCap.Text = "№ вагона";
        _lblNvagCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblMode
        // 
        _lblMode.Dock = DockStyle.Fill;
        _lblMode.Location = new Point(625, 48);
        _lblMode.Margin = new Padding(0);
        _lblMode.Name = "_lblMode";
        _lblMode.Size = new Size(221, 46);
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
        _lblDateCap.Size = new Size(138, 46);
        _lblDateCap.TabIndex = 1;
        _lblDateCap.Text = "Дата";
        _lblDateCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblModeCap
        // 
        _lblModeCap.Dock = DockStyle.Fill;
        _lblModeCap.Location = new Point(451, 48);
        _lblModeCap.Margin = new Padding(0);
        _lblModeCap.Name = "_lblModeCap";
        _lblModeCap.Size = new Size(173, 46);
        _lblModeCap.TabIndex = 7;
        _lblModeCap.Text = "Режим";
        _lblModeCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblVr
        // 
        _lblVr.Dock = DockStyle.Fill;
        _lblVr.Location = new Point(140, 48);
        _lblVr.Margin = new Padding(0);
        _lblVr.Name = "_lblVr";
        _lblVr.Size = new Size(310, 46);
        _lblVr.TabIndex = 4;
        _lblVr.Text = "—";
        _lblVr.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblNpp
        // 
        _lblNpp.Dock = DockStyle.Fill;
        _lblNpp.Location = new Point(625, 1);
        _lblNpp.Margin = new Padding(0);
        _lblNpp.Name = "_lblNpp";
        _lblNpp.Size = new Size(221, 46);
        _lblNpp.TabIndex = 6;
        _lblNpp.Text = "—";
        _lblNpp.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblDt
        // 
        _lblDt.Dock = DockStyle.Fill;
        _lblDt.Location = new Point(140, 1);
        _lblDt.Margin = new Padding(0);
        _lblDt.Name = "_lblDt";
        _lblDt.Padding = new Padding(0, 0, 0, 4);
        _lblDt.Size = new Size(310, 46);
        _lblDt.TabIndex = 2;
        _lblDt.Text = "—";
        _lblDt.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblNppCap
        // 
        _lblNppCap.Dock = DockStyle.Fill;
        _lblNppCap.Location = new Point(451, 1);
        _lblNppCap.Margin = new Padding(0);
        _lblNppCap.Name = "_lblNppCap";
        _lblNppCap.Size = new Size(173, 46);
        _lblNppCap.TabIndex = 5;
        _lblNppCap.Text = "№п/п";
        _lblNppCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblTimeCap
        // 
        _lblTimeCap.Dock = DockStyle.Fill;
        _lblTimeCap.Location = new Point(1, 48);
        _lblTimeCap.Margin = new Padding(0);
        _lblTimeCap.Name = "_lblTimeCap";
        _lblTimeCap.Size = new Size(138, 46);
        _lblTimeCap.TabIndex = 3;
        _lblTimeCap.Text = "Время";
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
        tableLayoutPanel3.Location = new Point(3, 152);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 1;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.Size = new Size(306, 147);
        tableLayoutPanel3.TabIndex = 40;
        // 
        // _rbTara
        // 
        _rbTara.AutoSize = true;
        _rbTara.Dock = DockStyle.Fill;
        _rbTara.Location = new Point(168, 6);
        _rbTara.Margin = new Padding(15, 5, 4, 5);
        _rbTara.Name = "_rbTara";
        _rbTara.Size = new Size(133, 135);
        _rbTara.TabIndex = 2;
        _rbTara.Text = "Тара";
        _rbTara.CheckedChanged += RbTara_CheckedChanged;
        // 
        // _rbBrutto
        // 
        _rbBrutto.AutoSize = true;
        _rbBrutto.Checked = true;
        _rbBrutto.Dock = DockStyle.Fill;
        _rbBrutto.Location = new Point(16, 6);
        _rbBrutto.Margin = new Padding(15, 5, 4, 5);
        _rbBrutto.Name = "_rbBrutto";
        _rbBrutto.Size = new Size(132, 135);
        _rbBrutto.TabIndex = 1;
        _rbBrutto.TabStop = true;
        _rbBrutto.Text = "Брутто";
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
        tableLayoutPanel6.Controls.Add(_cmbTar, 1, 1);
        tableLayoutPanel6.Dock = DockStyle.Fill;
        tableLayoutPanel6.Location = new Point(315, 152);
        tableLayoutPanel6.Name = "tableLayoutPanel6";
        tableLayoutPanel6.RowCount = 3;
        tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel6.Size = new Size(847, 147);
        tableLayoutPanel6.TabIndex = 42;
        // 
        // _lblNetto
        // 
        _lblNetto.AutoSize = true;
        _lblNetto.Dock = DockStyle.Fill;
        _lblNetto.Location = new Point(216, 97);
        _lblNetto.Margin = new Padding(4, 0, 4, 0);
        _lblNetto.Name = "_lblNetto";
        _lblNetto.Size = new Size(626, 49);
        _lblNetto.TabIndex = 24;
        _lblNetto.Text = "—";
        _lblNetto.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblNettoCap
        // 
        _lblNettoCap.AutoSize = true;
        _lblNettoCap.Dock = DockStyle.Fill;
        _lblNettoCap.Location = new Point(5, 97);
        _lblNettoCap.Margin = new Padding(4, 0, 4, 0);
        _lblNettoCap.Name = "_lblNettoCap";
        _lblNettoCap.Size = new Size(202, 49);
        _lblNettoCap.TabIndex = 23;
        _lblNettoCap.Text = "Нетто";
        _lblNettoCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblTarCap
        // 
        _lblTarCap.AutoSize = true;
        _lblTarCap.Dock = DockStyle.Fill;
        _lblTarCap.Location = new Point(5, 49);
        _lblTarCap.Margin = new Padding(4, 0, 4, 0);
        _lblTarCap.Name = "_lblTarCap";
        _lblTarCap.Size = new Size(202, 47);
        _lblTarCap.TabIndex = 20;
        _lblTarCap.Text = "Тара";
        _lblTarCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblBrutto
        // 
        _lblBrutto.AutoSize = true;
        _lblBrutto.Dock = DockStyle.Fill;
        _lblBrutto.Location = new Point(216, 1);
        _lblBrutto.Margin = new Padding(4, 0, 4, 0);
        _lblBrutto.Name = "_lblBrutto";
        _lblBrutto.Size = new Size(626, 47);
        _lblBrutto.TabIndex = 18;
        _lblBrutto.Text = "—";
        _lblBrutto.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblBruttoCap
        // 
        _lblBruttoCap.AutoSize = true;
        _lblBruttoCap.Dock = DockStyle.Fill;
        _lblBruttoCap.Location = new Point(5, 1);
        _lblBruttoCap.Margin = new Padding(4, 0, 4, 0);
        _lblBruttoCap.Name = "_lblBruttoCap";
        _lblBruttoCap.Size = new Size(202, 47);
        _lblBruttoCap.TabIndex = 17;
        _lblBruttoCap.Text = "Брутто";
        _lblBruttoCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _cmbTar
        // 
        _cmbTar.Dock = DockStyle.Fill;
        _cmbTar.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbTar.Location = new Point(216, 54);
        _cmbTar.Margin = new Padding(4, 5, 4, 5);
        _cmbTar.Name = "_cmbTar";
        _cmbTar.Size = new Size(626, 33);
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
        _split.Location = new Point(0, 543);
        _split.Margin = new Padding(4, 5, 4, 5);
        _split.Name = "_split";
        // 
        // _split.Panel1
        // 
        _split.Panel1.Controls.Add(_gridPend);
        _split.Panel1.Controls.Add(_lblHeaderPend);
        _split.Panel1MinSize = 563;
        // 
        // _split.Panel2
        // 
        _split.Panel2.Controls.Add(_gridDone);
        _split.Panel2.Controls.Add(_lblHeaderDone);
        _split.Panel2MinSize = 280;
        _split.Size = new Size(1806, 500);
        _split.SplitterDistance = 804;
        _split.SplitterWidth = 6;
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
        _gridPend.Location = new Point(0, 40);
        _gridPend.Margin = new Padding(4, 5, 4, 5);
        _gridPend.MultiSelect = false;
        _gridPend.Name = "_gridPend";
        _gridPend.ReadOnly = true;
        _gridPend.RowHeadersVisible = false;
        _gridPend.RowHeadersWidth = 51;
        _gridPend.RowTemplate.Height = 28;
        _gridPend.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gridPend.Size = new Size(804, 460);
        _gridPend.TabIndex = 0;
        _gridPend.SelectionChanged += GridPend_SelectionChanged;
        // 
        // _lblHeaderPend
        // 
        _lblHeaderPend.Dock = DockStyle.Top;
        _lblHeaderPend.Location = new Point(0, 0);
        _lblHeaderPend.Margin = new Padding(4, 0, 4, 0);
        _lblHeaderPend.Name = "_lblHeaderPend";
        _lblHeaderPend.Size = new Size(804, 40);
        _lblHeaderPend.TabIndex = 1;
        _lblHeaderPend.Text = "  Не перенесённые";
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
        _gridDone.Location = new Point(0, 40);
        _gridDone.Margin = new Padding(4, 5, 4, 5);
        _gridDone.MultiSelect = false;
        _gridDone.Name = "_gridDone";
        _gridDone.ReadOnly = true;
        _gridDone.RowHeadersVisible = false;
        _gridDone.RowHeadersWidth = 51;
        _gridDone.RowTemplate.Height = 28;
        _gridDone.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gridDone.Size = new Size(996, 460);
        _gridDone.TabIndex = 0;
        _gridDone.SelectionChanged += GridDone_SelectionChanged;
        // 
        // _lblHeaderDone
        // 
        _lblHeaderDone.Dock = DockStyle.Top;
        _lblHeaderDone.Location = new Point(0, 0);
        _lblHeaderDone.Margin = new Padding(4, 0, 4, 0);
        _lblHeaderDone.Name = "_lblHeaderDone";
        _lblHeaderDone.Size = new Size(996, 40);
        _lblHeaderDone.TabIndex = 1;
        _lblHeaderDone.Text = "  Перенесённые";
        _lblHeaderDone.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _pnlStatus
        // 
        _pnlStatus.Dock = DockStyle.Bottom;
        _pnlStatus.Location = new Point(0, 1043);
        _pnlStatus.Margin = new Padding(4, 5, 4, 5);
        _pnlStatus.Name = "_pnlStatus";
        _pnlStatus.Size = new Size(1806, 7);
        _pnlStatus.TabIndex = 2;
        // 
        // CorrectionsForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1806, 1050);
        Controls.Add(_split);
        Controls.Add(_pnlTop);
        Controls.Add(_pnlStatus);
        Margin = new Padding(4, 5, 4, 5);
        MinimumSize = new Size(1219, 896);
        Name = "CorrectionsForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Корректировки — перенос в учётную систему";
        _pnlTop.ResumeLayout(false);
        _pnlTop.PerformLayout();
        _pnlTopActionsLayout.ResumeLayout(false);
        _pnlTopActions.ResumeLayout(false);
        tableLayoutPanel5.ResumeLayout(false);
        tableLayoutPanel7.ResumeLayout(false);
        tableLayoutPanel7.PerformLayout();
        tableLayoutPanel4.ResumeLayout(false);
        tableLayoutPanel4.PerformLayout();
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        tableLayoutPanel2.ResumeLayout(false);
        tableLayoutPanel2.PerformLayout();
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel3.PerformLayout();
        tableLayoutPanel6.ResumeLayout(false);
        tableLayoutPanel6.PerformLayout();
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
    private FlowLayoutPanel _pnlTopActionsLayout;
    private Panel          _pnlStatus;
    private Label          _lblNvagCap;
    private TextBox        _txtNvag;
    private Label          _lblGruzCap;
    private TextBox        _txtGruz;
    private Label          _lblBruttoCap;
    private Label          _lblBrutto;
    private Label          _lblTarCap;
    private ComboBox       _cmbTar;
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
    private Label label2;
    private Label lblpotr;
    private TextBox _tbPlat;
    private TextBox tbCex;
    private TextBox _tbPotr;
    private Label label3;
    private TableLayoutPanel tableLayoutPanel6;
    private TableLayoutPanel tableLayoutPanel7;
}
