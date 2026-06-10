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
        tableLayoutPanel5 = new TableLayoutPanel();
        tableLayoutPanel7 = new TableLayoutPanel();
        label3 = new Label();
        lblpotr = new Label();
        tbCex = new TextBox();
        _tbPotr = new TextBox();
        tableLayoutPanel4 = new TableLayoutPanel();
        _rbTara = new RadioButton();
        _rbBrutto = new RadioButton();
        _rbGras = new RadioButton();
        _rbGpri = new RadioButton();
        tableLayoutPanel1 = new TableLayoutPanel();
        _txtGruz = new TextBox();
        _lblGruzCap = new Label();
        _tbPlat = new TextBox();
        label2 = new Label();
        tableLayoutPanel2 = new TableLayoutPanel();
        _lblDateCap = new Label();
        _lblVr = new Label();
        _lblDt = new Label();
        _lblTimeCap = new Label();
        tableLayoutPanel3 = new TableLayoutPanel();
        _lblMode = new Label();
        _lblModeCap = new Label();
        _lblNpp = new Label();
        _lblNppCap = new Label();
        tableLayoutPanel6 = new TableLayoutPanel();
        _pnlTopData = new Panel();
        _pnlTopDataRow3 = new Panel();
        _pnlTopDataRow3Layout = new Panel();
        _lblBruttoCap = new Label();
        _lblBrutto = new Label();
        _lblTarCap = new Label();
        _cmbTar = new ComboBox();
        _lblNettoCap = new Label();
        _lblNetto = new Label();
        _lblNettoUnit = new Label();
        _pnlTopDataRow2 = new Panel();
        _pnlTopDataRow2Layout = new Panel();
        _lblNdokCap = new Label();
        _txtNdok = new TextBox();
        _pnlTopDataRow1 = new Panel();
        _pnlTopDataRow1Layout = new Panel();
        _lblNvagCap = new Label();
        _txtNvag = new TextBox();
        _lblDirCap = new Label();
        _lblDir = new Label();
        _pnlTopActionsLayout = new FlowLayoutPanel();
        _btnSave = new Button();
        _btnTransfer = new Button();
        _btnClear = new Button();
        _btnRefresh = new Button();
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
        tableLayoutPanel5.SuspendLayout();
        tableLayoutPanel7.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        tableLayoutPanel6.SuspendLayout();
        _pnlTopData.SuspendLayout();
        _pnlTopDataRow3.SuspendLayout();
        _pnlTopDataRow3Layout.SuspendLayout();
        _pnlTopDataRow2.SuspendLayout();
        _pnlTopDataRow2Layout.SuspendLayout();
        _pnlTopDataRow1.SuspendLayout();
        _pnlTopDataRow1Layout.SuspendLayout();
        _pnlTopActionsLayout.SuspendLayout();
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
        _pnlTop.Controls.Add(_pnlTopData);
        _pnlTop.Dock = DockStyle.Top;
        _pnlTop.Location = new Point(0, 0);
        _pnlTop.Margin = new Padding(4, 5, 4, 5);
        _pnlTop.Name = "_pnlTop";
        _pnlTop.Size = new Size(1806, 553);
        _pnlTop.TabIndex = 0;
        // 
        // _pnlTopActions
        // 
        _pnlTopActions.Controls.Add(tableLayoutPanel5);
        _pnlTopActions.Dock = DockStyle.Top;
        _pnlTopActions.Location = new Point(0, 0);
        _pnlTopActions.Margin = new Padding(4, 5, 4, 5);
        _pnlTopActions.Name = "_pnlTopActions";
        _pnlTopActions.Size = new Size(1806, 259);
        _pnlTopActions.TabIndex = 2;
        // 
        // tableLayoutPanel5
        // 
        tableLayoutPanel5.ColumnCount = 4;
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.9663868F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28.0112076F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28.0112076F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28.0112076F));
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
        tableLayoutPanel5.Size = new Size(1806, 253);
        tableLayoutPanel5.TabIndex = 41;
        // 
        // tableLayoutPanel7
        // 
        tableLayoutPanel7.ColumnCount = 2;
        tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.462925F));
        tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68.53707F));
        tableLayoutPanel7.Controls.Add(label3, 0, 0);
        tableLayoutPanel7.Controls.Add(lblpotr, 0, 1);
        tableLayoutPanel7.Controls.Add(tbCex, 1, 0);
        tableLayoutPanel7.Controls.Add(_tbPotr, 1, 1);
        tableLayoutPanel7.Dock = DockStyle.Fill;
        tableLayoutPanel7.Location = new Point(796, 3);
        tableLayoutPanel7.Name = "tableLayoutPanel7";
        tableLayoutPanel7.RowCount = 2;
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel7.Size = new Size(499, 119);
        tableLayoutPanel7.TabIndex = 43;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Dock = DockStyle.Fill;
        label3.Location = new Point(2, 0);
        label3.Margin = new Padding(2, 0, 2, 0);
        label3.Name = "label3";
        label3.Size = new Size(153, 59);
        label3.TabIndex = 34;
        label3.Text = "Цех";
        label3.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblpotr
        // 
        lblpotr.AutoSize = true;
        lblpotr.Dock = DockStyle.Fill;
        lblpotr.Location = new Point(0, 59);
        lblpotr.Margin = new Padding(0);
        lblpotr.Name = "lblpotr";
        lblpotr.Size = new Size(157, 60);
        lblpotr.TabIndex = 31;
        lblpotr.Text = "Потребитель";
        lblpotr.TextAlign = ContentAlignment.MiddleRight;
        // 
        // tbCex
        // 
        tbCex.Anchor = AnchorStyles.None;
        tbCex.Location = new Point(166, 14);
        tbCex.Margin = new Padding(4, 5, 4, 5);
        tbCex.MaxLength = 3;
        tbCex.Name = "tbCex";
        tbCex.Size = new Size(324, 31);
        tbCex.TabIndex = 33;
        // 
        // _tbPotr
        // 
        _tbPotr.Anchor = AnchorStyles.None;
        _tbPotr.Location = new Point(166, 73);
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
        tableLayoutPanel4.Controls.Add(_rbTara, 1, 1);
        tableLayoutPanel4.Controls.Add(_rbBrutto, 0, 1);
        tableLayoutPanel4.Controls.Add(_rbGras, 1, 0);
        tableLayoutPanel4.Controls.Add(_rbGpri, 0, 0);
        tableLayoutPanel4.Dock = DockStyle.Fill;
        tableLayoutPanel4.Location = new Point(3, 3);
        tableLayoutPanel4.Name = "tableLayoutPanel4";
        tableLayoutPanel4.RowCount = 2;
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel4.Size = new Size(282, 119);
        tableLayoutPanel4.TabIndex = 41;
        // 
        // _rbTara
        // 
        _rbTara.AutoSize = true;
        _rbTara.Dock = DockStyle.Fill;
        _rbTara.Location = new Point(156, 65);
        _rbTara.Margin = new Padding(15, 5, 4, 5);
        _rbTara.Name = "_rbTara";
        _rbTara.Size = new Size(121, 48);
        _rbTara.TabIndex = 2;
        _rbTara.Text = "Тара";
        _rbTara.CheckedChanged += RbTara_CheckedChanged;
        // 
        // _rbBrutto
        // 
        _rbBrutto.AutoSize = true;
        _rbBrutto.Checked = true;
        _rbBrutto.Dock = DockStyle.Fill;
        _rbBrutto.Location = new Point(16, 65);
        _rbBrutto.Margin = new Padding(15, 5, 4, 5);
        _rbBrutto.Name = "_rbBrutto";
        _rbBrutto.Size = new Size(120, 48);
        _rbBrutto.TabIndex = 1;
        _rbBrutto.TabStop = true;
        _rbBrutto.Text = "Брутто";
        // 
        // _rbGras
        // 
        _rbGras.AutoSize = true;
        _rbGras.Dock = DockStyle.Fill;
        _rbGras.Location = new Point(156, 6);
        _rbGras.Margin = new Padding(15, 5, 4, 5);
        _rbGras.Name = "_rbGras";
        _rbGras.Size = new Size(121, 48);
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
        _rbGpri.Size = new Size(120, 48);
        _rbGpri.TabIndex = 1;
        _rbGpri.TabStop = true;
        _rbGpri.Text = "Приход";
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29.5081959F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70.491806F));
        tableLayoutPanel1.Controls.Add(_txtGruz, 1, 1);
        tableLayoutPanel1.Controls.Add(_lblGruzCap, 0, 1);
        tableLayoutPanel1.Controls.Add(_tbPlat, 1, 0);
        tableLayoutPanel1.Controls.Add(label2, 0, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(796, 128);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        tableLayoutPanel1.Size = new Size(499, 122);
        tableLayoutPanel1.TabIndex = 38;
        // 
        // _txtGruz
        // 
        _txtGruz.Anchor = AnchorStyles.None;
        _txtGruz.Location = new Point(151, 76);
        _txtGruz.Margin = new Padding(4, 5, 4, 5);
        _txtGruz.Name = "_txtGruz";
        _txtGruz.Size = new Size(344, 31);
        _txtGruz.TabIndex = 14;
        // 
        // _lblGruzCap
        // 
        _lblGruzCap.AutoSize = true;
        _lblGruzCap.Dock = DockStyle.Fill;
        _lblGruzCap.Location = new Point(4, 61);
        _lblGruzCap.Margin = new Padding(4, 0, 4, 0);
        _lblGruzCap.Name = "_lblGruzCap";
        _lblGruzCap.Size = new Size(139, 61);
        _lblGruzCap.TabIndex = 13;
        _lblGruzCap.Text = "Груз:";
        _lblGruzCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _tbPlat
        // 
        _tbPlat.Anchor = AnchorStyles.None;
        _tbPlat.Location = new Point(151, 15);
        _tbPlat.Margin = new Padding(4, 5, 4, 5);
        _tbPlat.Name = "_tbPlat";
        _tbPlat.Size = new Size(344, 31);
        _tbPlat.TabIndex = 30;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Dock = DockStyle.Fill;
        label2.Location = new Point(0, 0);
        label2.Margin = new Padding(0);
        label2.Name = "label2";
        label2.Size = new Size(147, 61);
        label2.TabIndex = 32;
        label2.Text = "Плательщик";
        label2.TextAlign = ContentAlignment.MiddleRight;
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel2.ColumnCount = 2;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32.93173F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67.0682755F));
        tableLayoutPanel2.Controls.Add(_lblDateCap, 0, 0);
        tableLayoutPanel2.Controls.Add(_lblVr, 1, 1);
        tableLayoutPanel2.Controls.Add(_lblDt, 1, 0);
        tableLayoutPanel2.Controls.Add(_lblTimeCap, 0, 1);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Location = new Point(291, 3);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 2;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel2.Size = new Size(499, 119);
        tableLayoutPanel2.TabIndex = 39;
        // 
        // _lblDateCap
        // 
        _lblDateCap.AutoSize = true;
        _lblDateCap.Dock = DockStyle.Fill;
        _lblDateCap.Location = new Point(1, 19);
        _lblDateCap.Margin = new Padding(0, 18, 0, 0);
        _lblDateCap.Name = "_lblDateCap";
        _lblDateCap.Size = new Size(163, 40);
        _lblDateCap.TabIndex = 1;
        _lblDateCap.Text = "Дата:";
        _lblDateCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblVr
        // 
        _lblVr.AutoSize = true;
        _lblVr.Dock = DockStyle.Fill;
        _lblVr.Location = new Point(165, 78);
        _lblVr.Margin = new Padding(0, 18, 0, 0);
        _lblVr.Name = "_lblVr";
        _lblVr.Size = new Size(333, 40);
        _lblVr.TabIndex = 4;
        _lblVr.Text = "—";
        _lblVr.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblDt
        // 
        _lblDt.AutoSize = true;
        _lblDt.Dock = DockStyle.Fill;
        _lblDt.Location = new Point(165, 19);
        _lblDt.Margin = new Padding(0, 18, 0, 0);
        _lblDt.Name = "_lblDt";
        _lblDt.Size = new Size(333, 40);
        _lblDt.TabIndex = 2;
        _lblDt.Text = "—";
        _lblDt.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblTimeCap
        // 
        _lblTimeCap.AutoSize = true;
        _lblTimeCap.Dock = DockStyle.Fill;
        _lblTimeCap.Location = new Point(1, 78);
        _lblTimeCap.Margin = new Padding(0, 18, 0, 0);
        _lblTimeCap.Name = "_lblTimeCap";
        _lblTimeCap.Size = new Size(163, 40);
        _lblTimeCap.TabIndex = 3;
        _lblTimeCap.Text = "Время:";
        _lblTimeCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel3.ColumnCount = 2;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.Controls.Add(_lblMode, 1, 1);
        tableLayoutPanel3.Controls.Add(_lblModeCap, 0, 1);
        tableLayoutPanel3.Controls.Add(_lblNpp, 1, 0);
        tableLayoutPanel3.Controls.Add(_lblNppCap, 0, 0);
        tableLayoutPanel3.Location = new Point(3, 128);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 2;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.Size = new Size(282, 119);
        tableLayoutPanel3.TabIndex = 40;
        // 
        // _lblMode
        // 
        _lblMode.AutoSize = true;
        _lblMode.Location = new Point(145, 60);
        _lblMode.Margin = new Padding(4, 0, 4, 0);
        _lblMode.Name = "_lblMode";
        _lblMode.Size = new Size(30, 25);
        _lblMode.TabIndex = 8;
        _lblMode.Text = "—";
        // 
        // _lblModeCap
        // 
        _lblModeCap.AutoSize = true;
        _lblModeCap.Location = new Point(5, 60);
        _lblModeCap.Margin = new Padding(4, 0, 4, 0);
        _lblModeCap.Name = "_lblModeCap";
        _lblModeCap.Size = new Size(71, 25);
        _lblModeCap.TabIndex = 7;
        _lblModeCap.Text = "Режим:";
        // 
        // _lblNpp
        // 
        _lblNpp.AutoSize = true;
        _lblNpp.Location = new Point(145, 1);
        _lblNpp.Margin = new Padding(4, 0, 4, 0);
        _lblNpp.Name = "_lblNpp";
        _lblNpp.Size = new Size(30, 25);
        _lblNpp.TabIndex = 6;
        _lblNpp.Text = "—";
        // 
        // _lblNppCap
        // 
        _lblNppCap.AutoSize = true;
        _lblNppCap.Location = new Point(5, 1);
        _lblNppCap.Margin = new Padding(4, 0, 4, 0);
        _lblNppCap.Name = "_lblNppCap";
        _lblNppCap.Size = new Size(63, 25);
        _lblNppCap.TabIndex = 5;
        _lblNppCap.Text = "№п/п:";
        // 
        // tableLayoutPanel6
        // 
        tableLayoutPanel6.ColumnCount = 2;
        tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.67134F));
        tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64.32866F));
        tableLayoutPanel6.Controls.Add(_lblNetto, 1, 2);
        tableLayoutPanel6.Controls.Add(_lblNettoCap, 0, 2);
        tableLayoutPanel6.Controls.Add(_cmbTar, 1, 1);
        tableLayoutPanel6.Controls.Add(_lblTarCap, 0, 1);
        tableLayoutPanel6.Controls.Add(_lblBrutto, 1, 0);
        tableLayoutPanel6.Controls.Add(_lblBruttoCap, 0, 0);
        tableLayoutPanel6.Dock = DockStyle.Fill;
        tableLayoutPanel6.Location = new Point(291, 128);
        tableLayoutPanel6.Name = "tableLayoutPanel6";
        tableLayoutPanel6.RowCount = 3;
        tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        tableLayoutPanel6.Size = new Size(499, 122);
        tableLayoutPanel6.TabIndex = 42;
        // 
        // _pnlTopData
        // 
        _pnlTopData.Controls.Add(_pnlTopDataRow3);
        _pnlTopData.Controls.Add(_pnlTopDataRow2);
        _pnlTopData.Controls.Add(_pnlTopDataRow1);
        _pnlTopData.Location = new Point(0, 316);
        _pnlTopData.Margin = new Padding(4, 5, 4, 5);
        _pnlTopData.Name = "_pnlTopData";
        _pnlTopData.Size = new Size(1806, 207);
        _pnlTopData.TabIndex = 1;
        // 
        // _pnlTopDataRow3
        // 
        _pnlTopDataRow3.Controls.Add(_pnlTopDataRow3Layout);
        _pnlTopDataRow3.Dock = DockStyle.Top;
        _pnlTopDataRow3.Location = new Point(0, 57);
        _pnlTopDataRow3.Margin = new Padding(4, 5, 4, 5);
        _pnlTopDataRow3.Name = "_pnlTopDataRow3";
        _pnlTopDataRow3.Size = new Size(1806, 103);
        _pnlTopDataRow3.TabIndex = 2;
        // 
        // _pnlTopDataRow3Layout
        // 
        _pnlTopDataRow3Layout.Controls.Add(_lblNettoUnit);
        _pnlTopDataRow3Layout.Location = new Point(480, 0);
        _pnlTopDataRow3Layout.Margin = new Padding(0);
        _pnlTopDataRow3Layout.Name = "_pnlTopDataRow3Layout";
        _pnlTopDataRow3Layout.Size = new Size(1326, 103);
        _pnlTopDataRow3Layout.TabIndex = 0;
        // 
        // _lblBruttoCap
        // 
        _lblBruttoCap.AutoSize = true;
        _lblBruttoCap.Location = new Point(4, 0);
        _lblBruttoCap.Margin = new Padding(4, 0, 4, 0);
        _lblBruttoCap.Name = "_lblBruttoCap";
        _lblBruttoCap.Size = new Size(71, 25);
        _lblBruttoCap.TabIndex = 17;
        _lblBruttoCap.Text = "Брутто:";
        // 
        // _lblBrutto
        // 
        _lblBrutto.AutoSize = true;
        _lblBrutto.Location = new Point(182, 0);
        _lblBrutto.Margin = new Padding(4, 0, 4, 0);
        _lblBrutto.Name = "_lblBrutto";
        _lblBrutto.Size = new Size(30, 25);
        _lblBrutto.TabIndex = 18;
        _lblBrutto.Text = "—";
        // 
        // _lblTarCap
        // 
        _lblTarCap.AutoSize = true;
        _lblTarCap.Location = new Point(4, 40);
        _lblTarCap.Margin = new Padding(4, 0, 4, 0);
        _lblTarCap.Name = "_lblTarCap";
        _lblTarCap.Size = new Size(54, 25);
        _lblTarCap.TabIndex = 20;
        _lblTarCap.Text = "Тара:";
        // 
        // _cmbTar
        // 
        _cmbTar.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbTar.Location = new Point(182, 45);
        _cmbTar.Margin = new Padding(4, 5, 4, 5);
        _cmbTar.Name = "_cmbTar";
        _cmbTar.Size = new Size(313, 33);
        _cmbTar.TabIndex = 21;
        _cmbTar.SelectedIndexChanged += CmbTar_SelectedIndexChanged;
        // 
        // _lblNettoCap
        // 
        _lblNettoCap.AutoSize = true;
        _lblNettoCap.Location = new Point(4, 80);
        _lblNettoCap.Margin = new Padding(4, 0, 4, 0);
        _lblNettoCap.Name = "_lblNettoCap";
        _lblNettoCap.Size = new Size(63, 25);
        _lblNettoCap.TabIndex = 23;
        _lblNettoCap.Text = "Нетто:";
        // 
        // _lblNetto
        // 
        _lblNetto.AutoSize = true;
        _lblNetto.Location = new Point(182, 80);
        _lblNetto.Margin = new Padding(4, 0, 4, 0);
        _lblNetto.Name = "_lblNetto";
        _lblNetto.Size = new Size(30, 25);
        _lblNetto.TabIndex = 24;
        _lblNetto.Text = "—";
        // 
        // _lblNettoUnit
        // 
        _lblNettoUnit.AutoSize = true;
        _lblNettoUnit.Location = new Point(1041, 0);
        _lblNettoUnit.Margin = new Padding(4, 0, 4, 0);
        _lblNettoUnit.Name = "_lblNettoUnit";
        _lblNettoUnit.Size = new Size(19, 25);
        _lblNettoUnit.TabIndex = 25;
        _lblNettoUnit.Text = "т";
        // 
        // _pnlTopDataRow2
        // 
        _pnlTopDataRow2.Controls.Add(_pnlTopDataRow2Layout);
        _pnlTopDataRow2.Dock = DockStyle.Top;
        _pnlTopDataRow2.Location = new Point(0, 0);
        _pnlTopDataRow2.Margin = new Padding(4, 5, 4, 5);
        _pnlTopDataRow2.Name = "_pnlTopDataRow2";
        _pnlTopDataRow2.Size = new Size(1806, 57);
        _pnlTopDataRow2.TabIndex = 1;
        // 
        // _pnlTopDataRow2Layout
        // 
        _pnlTopDataRow2Layout.Controls.Add(_lblNdokCap);
        _pnlTopDataRow2Layout.Controls.Add(_txtNdok);
        _pnlTopDataRow2Layout.Location = new Point(947, 0);
        _pnlTopDataRow2Layout.Margin = new Padding(0);
        _pnlTopDataRow2Layout.Name = "_pnlTopDataRow2Layout";
        _pnlTopDataRow2Layout.Size = new Size(859, 57);
        _pnlTopDataRow2Layout.TabIndex = 0;
        // 
        // _lblNdokCap
        // 
        _lblNdokCap.AutoSize = true;
        _lblNdokCap.Location = new Point(661, 0);
        _lblNdokCap.Margin = new Padding(4, 0, 4, 0);
        _lblNdokCap.Name = "_lblNdokCap";
        _lblNdokCap.Size = new Size(128, 25);
        _lblNdokCap.TabIndex = 15;
        _lblNdokCap.Text = "№ документа:";
        // 
        // _txtNdok
        // 
        _txtNdok.Location = new Point(1555, 5);
        _txtNdok.Margin = new Padding(4, 5, 4, 5);
        _txtNdok.Name = "_txtNdok";
        _txtNdok.Size = new Size(184, 31);
        _txtNdok.TabIndex = 16;
        // 
        // _pnlTopDataRow1
        // 
        _pnlTopDataRow1.Controls.Add(_pnlTopDataRow1Layout);
        _pnlTopDataRow1.Location = new Point(0, 0);
        _pnlTopDataRow1.Margin = new Padding(4, 5, 4, 5);
        _pnlTopDataRow1.Name = "_pnlTopDataRow1";
        _pnlTopDataRow1.Size = new Size(1806, 47);
        _pnlTopDataRow1.TabIndex = 0;
        // 
        // _pnlTopDataRow1Layout
        // 
        _pnlTopDataRow1Layout.Controls.Add(_lblNvagCap);
        _pnlTopDataRow1Layout.Controls.Add(_txtNvag);
        _pnlTopDataRow1Layout.Controls.Add(_lblDirCap);
        _pnlTopDataRow1Layout.Controls.Add(_lblDir);
        _pnlTopDataRow1Layout.Location = new Point(0, 24);
        _pnlTopDataRow1Layout.Margin = new Padding(0);
        _pnlTopDataRow1Layout.Name = "_pnlTopDataRow1Layout";
        _pnlTopDataRow1Layout.Size = new Size(1806, 23);
        _pnlTopDataRow1Layout.TabIndex = 0;
        // 
        // _lblNvagCap
        // 
        _lblNvagCap.AutoSize = true;
        _lblNvagCap.Location = new Point(4, 0);
        _lblNvagCap.Margin = new Padding(4, 0, 4, 0);
        _lblNvagCap.Name = "_lblNvagCap";
        _lblNvagCap.Size = new Size(97, 25);
        _lblNvagCap.TabIndex = 9;
        _lblNvagCap.Text = "№ вагона:";
        // 
        // _txtNvag
        // 
        _txtNvag.Location = new Point(220, 5);
        _txtNvag.Margin = new Padding(4, 5, 4, 5);
        _txtNvag.Name = "_txtNvag";
        _txtNvag.Size = new Size(233, 31);
        _txtNvag.TabIndex = 10;
        _txtNvag.Leave += TxtNvag_Leave;
        // 
        // _lblDirCap
        // 
        _lblDirCap.AutoSize = true;
        _lblDirCap.Location = new Point(463, 0);
        _lblDirCap.Margin = new Padding(4, 0, 4, 0);
        _lblDirCap.Name = "_lblDirCap";
        _lblDirCap.Size = new Size(125, 25);
        _lblDirCap.TabIndex = 11;
        _lblDirCap.Text = "Направление:";
        // 
        // _lblDir
        // 
        _lblDir.AutoSize = true;
        _lblDir.Location = new Point(1356, 0);
        _lblDir.Margin = new Padding(4, 0, 4, 0);
        _lblDir.Name = "_lblDir";
        _lblDir.Size = new Size(30, 25);
        _lblDir.TabIndex = 12;
        _lblDir.Text = "—";
        // 
        // _pnlTopActionsLayout
        // 
        _pnlTopActionsLayout.AutoSize = true;
        _pnlTopActionsLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _pnlTopActionsLayout.Controls.Add(_btnSave);
        _pnlTopActionsLayout.Controls.Add(_btnTransfer);
        _pnlTopActionsLayout.Controls.Add(_btnClear);
        _pnlTopActionsLayout.Controls.Add(_btnRefresh);
        _pnlTopActionsLayout.Location = new Point(156, 5);
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
        _split.Location = new Point(0, 553);
        _split.Margin = new Padding(4, 5, 4, 5);
        _split.Name = "_split";
        // 
        // _split.Panel1
        // 
        _split.Panel1.Controls.Add(_pnlTopActionsLayout);
        _split.Panel1.Controls.Add(_gridPend);
        _split.Panel1.Controls.Add(_lblHeaderPend);
        _split.Panel1MinSize = 563;
        // 
        // _split.Panel2
        // 
        _split.Panel2.Controls.Add(_gridDone);
        _split.Panel2.Controls.Add(_lblHeaderDone);
        _split.Panel2MinSize = 280;
        _split.Size = new Size(1806, 490);
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
        _gridPend.Size = new Size(804, 450);
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
        _gridDone.Size = new Size(996, 450);
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
        _pnlTopData.ResumeLayout(false);
        _pnlTopDataRow3.ResumeLayout(false);
        _pnlTopDataRow3Layout.ResumeLayout(false);
        _pnlTopDataRow3Layout.PerformLayout();
        _pnlTopDataRow2.ResumeLayout(false);
        _pnlTopDataRow2Layout.ResumeLayout(false);
        _pnlTopDataRow2Layout.PerformLayout();
        _pnlTopDataRow1.ResumeLayout(false);
        _pnlTopDataRow1Layout.ResumeLayout(false);
        _pnlTopDataRow1Layout.PerformLayout();
        _pnlTopActionsLayout.ResumeLayout(false);
        _split.Panel1.ResumeLayout(false);
        _split.Panel1.PerformLayout();
        _split.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_split).EndInit();
        _split.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_gridPend).EndInit();
        ((System.ComponentModel.ISupportInitialize)_gridDone).EndInit();
        ResumeLayout(false);
    }

    private Panel          _pnlTop;
    private Panel          _pnlTopData;
    private Panel          _pnlTopDataRow1;
    private Panel          _pnlTopDataRow1Layout;
    private Panel          _pnlTopDataRow2;
    private Panel          _pnlTopDataRow2Layout;
    private Panel          _pnlTopDataRow3;
    private Panel          _pnlTopDataRow3Layout;
    private Panel          _pnlTopActions;
    private FlowLayoutPanel _pnlTopActionsLayout;
    private Panel          _pnlStatus;
    private Label          _lblNvagCap;
    private TextBox        _txtNvag;
    private Label          _lblDirCap;
    private Label          _lblDir;
    private Label          _lblGruzCap;
    private TextBox        _txtGruz;
    private Label          _lblNdokCap;
    private TextBox        _txtNdok;
    private Label          _lblBruttoCap;
    private Label          _lblBrutto;
    private Label          _lblTarCap;
    private ComboBox       _cmbTar;
    private Label          _lblNettoCap;
    private Label          _lblNetto;
    private Label          _lblNettoUnit;
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
