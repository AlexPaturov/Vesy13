namespace Vesy13.Forms;

partial class PrintForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        _layoutMain = new TableLayoutPanel();
        _pnlTop = new Panel();
        _pnlTopRow2 = new Panel();
        tableLayoutPanel2 = new TableLayoutPanel();
        _btnFind = new Button();
        _txtPotr = new TextBox();
        _btnClearFilters = new Button();
        _lblPotr = new Label();
        _pnlTopRow1 = new Panel();
        tableLayoutPanel1 = new TableLayoutPanel();
        _rbGpri = new RadioButton();
        _pnlNvagHost = new TableLayoutPanel();
        _txtNvag = new TextBox();
        _lblNvag = new Label();
        _pnlGruzHost = new TableLayoutPanel();
        _txtGruz = new TextBox();
        _lblGruz = new Label();
        _pnlDateToHost = new TableLayoutPanel();
        _dtpTo = new DateTimePicker();
        _lblDateTo = new Label();
        _pnlDateFromHost = new TableLayoutPanel();
        _dtpFrom = new DateTimePicker();
        _rbGras = new RadioButton();
        _lblDateFrom = new Label();
        _grid = new DataGridView();
        _pnlStatus = new Panel();
        tableLayoutPanel3 = new TableLayoutPanel();
        _lblSlipNum = new Label();
        _txtSlipNum = new TextBox();
        _lblFrom = new Label();
        _txtFrom = new TextBox();
        _lblTo = new Label();
        _txtTo = new TextBox();
        _btnPreview = new Button();
        _layoutMain.SuspendLayout();
        _pnlTop.SuspendLayout();
        _pnlTopRow2.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        _pnlTopRow1.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        _pnlNvagHost.SuspendLayout();
        _pnlGruzHost.SuspendLayout();
        _pnlDateToHost.SuspendLayout();
        _pnlDateFromHost.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        _pnlStatus.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        SuspendLayout();
        // 
        // _layoutMain
        // 
        _layoutMain.ColumnCount = 1;
        _layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _layoutMain.Controls.Add(_pnlTop, 0, 0);
        _layoutMain.Controls.Add(_grid, 0, 1);
        _layoutMain.Controls.Add(_pnlStatus, 0, 2);
        _layoutMain.Dock = DockStyle.Fill;
        _layoutMain.Location = new Point(0, 0);
        _layoutMain.Margin = new Padding(0);
        _layoutMain.Name = "_layoutMain";
        _layoutMain.RowCount = 3;
        _layoutMain.RowStyles.Add(new RowStyle());
        _layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _layoutMain.RowStyles.Add(new RowStyle());
        _layoutMain.Size = new Size(1264, 670);
        _layoutMain.TabIndex = 0;
        // 
        // _pnlTop
        // 
        _pnlTop.Controls.Add(_pnlTopRow2);
        _pnlTop.Controls.Add(_pnlTopRow1);
        _pnlTop.Dock = DockStyle.Top;
        _pnlTop.Location = new Point(3, 3);
        _pnlTop.Margin = new Padding(3, 3, 3, 0);
        _pnlTop.Name = "_pnlTop";
        _pnlTop.Size = new Size(1258, 97);
        _pnlTop.TabIndex = 0;
        // 
        // _pnlTopRow2
        // 
        _pnlTopRow2.Dock = DockStyle.Top;
        _pnlTopRow2.Controls.Add(tableLayoutPanel2);
        _pnlTopRow2.Location = new Point(0, 51);
        _pnlTopRow2.Name = "_pnlTopRow2";
        _pnlTopRow2.Size = new Size(1264, 44);
        _pnlTopRow2.TabIndex = 1;
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel2.ColumnCount = 5;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.702532F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.0601273F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.0664558F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.5854435F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.50633F));
        tableLayoutPanel2.Controls.Add(_btnFind, 3, 0);
        tableLayoutPanel2.Controls.Add(_txtPotr, 1, 0);
        tableLayoutPanel2.Controls.Add(_btnClearFilters, 4, 0);
        tableLayoutPanel2.Controls.Add(_lblPotr, 0, 0);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Location = new Point(0, 0);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 1;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel2.Size = new Size(1264, 44);
        tableLayoutPanel2.TabIndex = 15;
        // 
        // _btnFind
        // 
        _btnFind.Dock = DockStyle.Fill;
        _btnFind.FlatAppearance.BorderSize = 0;
        _btnFind.FlatStyle = FlatStyle.Flat;
        _btnFind.Location = new Point(879, 3);
        _btnFind.Margin = new Padding(10, 2, 10, 2);
        _btnFind.Name = "_btnFind";
        _btnFind.Size = new Size(176, 38);
        _btnFind.TabIndex = 15;
        _btnFind.Text = "Найти";
        _btnFind.UseVisualStyleBackColor = false;
        _btnFind.Click += BtnFind_Click;
        // 
        // _txtPotr
        // 
        _txtPotr.Anchor = AnchorStyles.None;
        _txtPotr.Location = new Point(126, 10);
        _txtPotr.Name = "_txtPotr";
        _txtPotr.Size = new Size(172, 23);
        _txtPotr.TabIndex = 11;
        // 
        // _btnClearFilters
        // 
        _btnClearFilters.Dock = DockStyle.Fill;
        _btnClearFilters.FlatAppearance.BorderSize = 0;
        _btnClearFilters.FlatStyle = FlatStyle.Flat;
        _btnClearFilters.Location = new Point(1076, 5);
        _btnClearFilters.Margin = new Padding(10, 4, 10, 4);
        _btnClearFilters.Name = "_btnClearFilters";
        _btnClearFilters.Size = new Size(177, 34);
        _btnClearFilters.TabIndex = 16;
        _btnClearFilters.Text = "Очистить фильтры";
        _btnClearFilters.UseVisualStyleBackColor = false;
        _btnClearFilters.Click += BtnClearFilters_Click;
        // 
        // _lblPotr
        // 
        _lblPotr.AutoSize = true;
        _lblPotr.Dock = DockStyle.Fill;
        _lblPotr.Location = new Point(4, 1);
        _lblPotr.Name = "_lblPotr";
        _lblPotr.Size = new Size(103, 42);
        _lblPotr.TabIndex = 10;
        _lblPotr.Text = "Получатель:";
        _lblPotr.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _pnlTopRow1
        // 
        _pnlTopRow1.Dock = DockStyle.Top;
        _pnlTopRow1.Controls.Add(tableLayoutPanel1);
        _pnlTopRow1.Location = new Point(0, 0);
        _pnlTopRow1.Name = "_pnlTopRow1";
        _pnlTopRow1.Size = new Size(1264, 49);
        _pnlTopRow1.TabIndex = 0;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel1.ColumnCount = 10;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.227848F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.278481F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.408228F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.1835442F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.775316F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.6582279F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.80379725F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.4841766F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.25F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
        tableLayoutPanel1.Controls.Add(_rbGpri, 0, 0);
        tableLayoutPanel1.Controls.Add(_pnlNvagHost, 9, 0);
        tableLayoutPanel1.Controls.Add(_lblNvag, 8, 0);
        tableLayoutPanel1.Controls.Add(_pnlGruzHost, 7, 0);
        tableLayoutPanel1.Controls.Add(_lblGruz, 6, 0);
        tableLayoutPanel1.Controls.Add(_pnlDateToHost, 5, 0);
        tableLayoutPanel1.Controls.Add(_lblDateTo, 4, 0);
        tableLayoutPanel1.Controls.Add(_pnlDateFromHost, 3, 0);
        tableLayoutPanel1.Controls.Add(_rbGras, 1, 0);
        tableLayoutPanel1.Controls.Add(_lblDateFrom, 2, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tableLayoutPanel1.Size = new Size(1264, 49);
        tableLayoutPanel1.TabIndex = 10;
        // 
        // _rbGpri
        // 
        _rbGpri.AutoSize = true;
        _rbGpri.Checked = true;
        _rbGpri.Dock = DockStyle.Fill;
        _rbGpri.Location = new Point(4, 4);
        _rbGpri.Name = "_rbGpri";
        _rbGpri.Size = new Size(97, 41);
        _rbGpri.TabIndex = 0;
        _rbGpri.TabStop = true;
        _rbGpri.Text = "Приход";
        // 
        // _pnlNvagHost
        // 
        _pnlNvagHost.ColumnCount = 1;
        _pnlNvagHost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pnlNvagHost.Controls.Add(_txtNvag, 0, 1);
        _pnlNvagHost.Dock = DockStyle.Fill;
        _pnlNvagHost.Location = new Point(1050, 1);
        _pnlNvagHost.Margin = new Padding(0);
        _pnlNvagHost.Name = "_pnlNvagHost";
        _pnlNvagHost.RowCount = 3;
        _pnlNvagHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlNvagHost.RowStyles.Add(new RowStyle());
        _pnlNvagHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlNvagHost.Size = new Size(213, 47);
        _pnlNvagHost.TabIndex = 9;
        // 
        // _txtNvag
        // 
        _txtNvag.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtNvag.Location = new Point(6, 12);
        _txtNvag.Margin = new Padding(6, 0, 6, 0);
        _txtNvag.Name = "_txtNvag";
        _txtNvag.Size = new Size(201, 23);
        _txtNvag.TabIndex = 9;
        // 
        // _lblNvag
        // 
        _lblNvag.AutoSize = true;
        _lblNvag.Dock = DockStyle.Fill;
        _lblNvag.Location = new Point(974, 1);
        _lblNvag.Name = "_lblNvag";
        _lblNvag.Size = new Size(72, 47);
        _lblNvag.TabIndex = 8;
        _lblNvag.Text = "Вагон:";
        _lblNvag.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _pnlGruzHost
        // 
        _pnlGruzHost.ColumnCount = 1;
        _pnlGruzHost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pnlGruzHost.Controls.Add(_txtGruz, 0, 1);
        _pnlGruzHost.Dock = DockStyle.Fill;
        _pnlGruzHost.Location = new Point(751, 1);
        _pnlGruzHost.Margin = new Padding(0);
        _pnlGruzHost.Name = "_pnlGruzHost";
        _pnlGruzHost.RowCount = 3;
        _pnlGruzHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlGruzHost.RowStyles.Add(new RowStyle());
        _pnlGruzHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlGruzHost.Size = new Size(219, 47);
        _pnlGruzHost.TabIndex = 7;
        // 
        // _txtGruz
        // 
        _txtGruz.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtGruz.Location = new Point(6, 12);
        _txtGruz.Margin = new Padding(6, 0, 6, 0);
        _txtGruz.Name = "_txtGruz";
        _txtGruz.Size = new Size(207, 23);
        _txtGruz.TabIndex = 7;
        // 
        // _lblGruz
        // 
        _lblGruz.AutoSize = true;
        _lblGruz.Dock = DockStyle.Fill;
        _lblGruz.Location = new Point(668, 1);
        _lblGruz.Name = "_lblGruz";
        _lblGruz.Size = new Size(79, 47);
        _lblGruz.TabIndex = 6;
        _lblGruz.Text = "Груз:";
        _lblGruz.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _pnlDateToHost
        // 
        _pnlDateToHost.ColumnCount = 1;
        _pnlDateToHost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pnlDateToHost.Controls.Add(_dtpTo, 0, 1);
        _pnlDateToHost.Dock = DockStyle.Fill;
        _pnlDateToHost.Location = new Point(505, 1);
        _pnlDateToHost.Margin = new Padding(0);
        _pnlDateToHost.Name = "_pnlDateToHost";
        _pnlDateToHost.RowCount = 3;
        _pnlDateToHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlDateToHost.RowStyles.Add(new RowStyle());
        _pnlDateToHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlDateToHost.Size = new Size(159, 47);
        _pnlDateToHost.TabIndex = 5;
        // 
        // _dtpTo
        // 
        _dtpTo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _dtpTo.Format = DateTimePickerFormat.Short;
        _dtpTo.Location = new Point(6, 12);
        _dtpTo.Margin = new Padding(6, 0, 6, 0);
        _dtpTo.Name = "_dtpTo";
        _dtpTo.Size = new Size(147, 23);
        _dtpTo.TabIndex = 5;
        // 
        // _lblDateTo
        // 
        _lblDateTo.AutoSize = true;
        _lblDateTo.Dock = DockStyle.Fill;
        _lblDateTo.Location = new Point(435, 1);
        _lblDateTo.Name = "_lblDateTo";
        _lblDateTo.Size = new Size(66, 47);
        _lblDateTo.TabIndex = 4;
        _lblDateTo.Text = "по:";
        _lblDateTo.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _pnlDateFromHost
        // 
        _pnlDateFromHost.ColumnCount = 1;
        _pnlDateFromHost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pnlDateFromHost.Controls.Add(_dtpFrom, 0, 1);
        _pnlDateFromHost.Dock = DockStyle.Fill;
        _pnlDateFromHost.Location = new Point(278, 1);
        _pnlDateFromHost.Margin = new Padding(0);
        _pnlDateFromHost.Name = "_pnlDateFromHost";
        _pnlDateFromHost.RowCount = 3;
        _pnlDateFromHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlDateFromHost.RowStyles.Add(new RowStyle());
        _pnlDateFromHost.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _pnlDateFromHost.Size = new Size(153, 47);
        _pnlDateFromHost.TabIndex = 3;
        // 
        // _dtpFrom
        // 
        _dtpFrom.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _dtpFrom.Format = DateTimePickerFormat.Short;
        _dtpFrom.Location = new Point(6, 12);
        _dtpFrom.Margin = new Padding(6, 0, 6, 0);
        _dtpFrom.Name = "_dtpFrom";
        _dtpFrom.Size = new Size(141, 23);
        _dtpFrom.TabIndex = 3;
        // 
        // _rbGras
        // 
        _rbGras.AutoSize = true;
        _rbGras.Dock = DockStyle.Fill;
        _rbGras.Location = new Point(108, 4);
        _rbGras.Name = "_rbGras";
        _rbGras.Size = new Size(85, 41);
        _rbGras.TabIndex = 1;
        _rbGras.Text = "Расход";
        // 
        // _lblDateFrom
        // 
        _lblDateFrom.AutoSize = true;
        _lblDateFrom.Dock = DockStyle.Fill;
        _lblDateFrom.Location = new Point(200, 1);
        _lblDateFrom.Name = "_lblDateFrom";
        _lblDateFrom.Size = new Size(74, 47);
        _lblDateFrom.TabIndex = 2;
        _lblDateFrom.Text = "Дата с:";
        _lblDateFrom.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _grid
        // 
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AllowUserToResizeRows = false;
        _grid.BackgroundColor = Color.FromArgb(247, 249, 252);
        _grid.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = SystemColors.Control;
        dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(221, 230, 240);
        dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(35, 49, 63);
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        _grid.ColumnHeadersHeight = 32;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = SystemColors.Window;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(220, 232, 247);
        dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(35, 49, 63);
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        _grid.DefaultCellStyle = dataGridViewCellStyle2;
        _grid.Dock = DockStyle.Fill;
        _grid.EnableHeadersVisualStyles = false;
        _grid.GridColor = Color.FromArgb(200, 208, 218);
        _grid.Location = new Point(3, 102);
        _grid.Margin = new Padding(3, 2, 3, 3);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.RowHeadersVisible = false;
        _grid.RowHeadersWidth = 51;
        _grid.RowTemplate.Height = 28;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.Size = new Size(1258, 515);
        _grid.TabIndex = 1;
        // 
        // _pnlStatus
        // 
        _pnlStatus.Controls.Add(tableLayoutPanel3);
        _pnlStatus.Dock = DockStyle.Bottom;
        _pnlStatus.Location = new Point(3, 623);
        _pnlStatus.Name = "_pnlStatus";
        _pnlStatus.Size = new Size(1258, 44);
        _pnlStatus.TabIndex = 2;
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tableLayoutPanel3.ColumnCount = 7;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tableLayoutPanel3.Controls.Add(_lblSlipNum, 0, 0);
        tableLayoutPanel3.Controls.Add(_txtSlipNum, 1, 0);
        tableLayoutPanel3.Controls.Add(_lblFrom, 2, 0);
        tableLayoutPanel3.Controls.Add(_txtFrom, 3, 0);
        tableLayoutPanel3.Controls.Add(_lblTo, 4, 0);
        tableLayoutPanel3.Controls.Add(_txtTo, 5, 0);
        tableLayoutPanel3.Controls.Add(_btnPreview, 6, 0);
        tableLayoutPanel3.Dock = DockStyle.Fill;
        tableLayoutPanel3.Location = new Point(0, 0);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 1;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel3.Size = new Size(1258, 44);
        tableLayoutPanel3.TabIndex = 0;
        // 
        // _lblSlipNum
        // 
        _lblSlipNum.AutoSize = true;
        _lblSlipNum.Dock = DockStyle.Fill;
        _lblSlipNum.Location = new Point(4, 1);
        _lblSlipNum.Name = "_lblSlipNum";
        _lblSlipNum.Size = new Size(118, 42);
        _lblSlipNum.TabIndex = 0;
        _lblSlipNum.Text = "Номер отвесной:";
        _lblSlipNum.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _txtSlipNum
        // 
        _txtSlipNum.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtSlipNum.Location = new Point(132, 10);
        _txtSlipNum.Margin = new Padding(6, 0, 6, 0);
        _txtSlipNum.Name = "_txtSlipNum";
        _txtSlipNum.Size = new Size(138, 23);
        _txtSlipNum.TabIndex = 1;
        // 
        // _lblFrom
        // 
        _lblFrom.AutoSize = true;
        _lblFrom.Dock = DockStyle.Fill;
        _lblFrom.Location = new Point(280, 1);
        _lblFrom.Name = "_lblFrom";
        _lblFrom.Size = new Size(81, 42);
        _lblFrom.TabIndex = 2;
        _lblFrom.Text = "Откуда:";
        _lblFrom.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _txtFrom
        // 
        _txtFrom.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtFrom.Location = new Point(371, 10);
        _txtFrom.Margin = new Padding(6, 0, 6, 0);
        _txtFrom.Name = "_txtFrom";
        _txtFrom.Size = new Size(263, 23);
        _txtFrom.TabIndex = 3;
        // 
        // _lblTo
        // 
        _lblTo.AutoSize = true;
        _lblTo.Dock = DockStyle.Fill;
        _lblTo.Location = new Point(644, 1);
        _lblTo.Name = "_lblTo";
        _lblTo.Size = new Size(81, 42);
        _lblTo.TabIndex = 4;
        _lblTo.Text = "Куда:";
        _lblTo.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _txtTo
        // 
        _txtTo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtTo.Location = new Point(735, 10);
        _txtTo.Margin = new Padding(6, 0, 6, 0);
        _txtTo.Name = "_txtTo";
        _txtTo.Size = new Size(263, 23);
        _txtTo.TabIndex = 5;
        // 
        // _btnPreview
        // 
        _btnPreview.Dock = DockStyle.Fill;
        _btnPreview.FlatAppearance.BorderSize = 0;
        _btnPreview.FlatStyle = FlatStyle.Flat;
        _btnPreview.Location = new Point(1015, 5);
        _btnPreview.Margin = new Padding(10, 4, 10, 4);
        _btnPreview.Name = "_btnPreview";
        _btnPreview.Size = new Size(232, 34);
        _btnPreview.TabIndex = 6;
        _btnPreview.Text = "Просмотр печати";
        _btnPreview.UseVisualStyleBackColor = false;
        _btnPreview.Click += BtnPreview_Click;
        // 
        // PrintForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(238, 241, 244);
        ClientSize = new Size(1264, 670);
        Controls.Add(_layoutMain);
        MinimumSize = new Size(860, 560);
        Name = "PrintForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Печать отвесных — Весы 13";
        _layoutMain.ResumeLayout(false);
        _pnlTop.ResumeLayout(false);
        _pnlTopRow2.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        tableLayoutPanel2.PerformLayout();
        _pnlTopRow1.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        _pnlNvagHost.ResumeLayout(false);
        _pnlNvagHost.PerformLayout();
        _pnlGruzHost.ResumeLayout(false);
        _pnlGruzHost.PerformLayout();
        _pnlDateToHost.ResumeLayout(false);
        _pnlDateFromHost.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel3.PerformLayout();
        _pnlStatus.ResumeLayout(false);
        ResumeLayout(false);
    }

    private TableLayoutPanel _layoutMain;
    private Panel         _pnlTop;
    private Panel         _pnlTopRow1;
    private Panel         _pnlTopRow2;
    private RadioButton   _rbGpri;
    private RadioButton   _rbGras;
    private Label         _lblDateFrom;
    private DateTimePicker _dtpFrom;
    private TableLayoutPanel _pnlDateFromHost;
    private Label         _lblDateTo;
    private DateTimePicker _dtpTo;
    private TableLayoutPanel _pnlDateToHost;
    private Label         _lblGruz;
    private TextBox       _txtGruz;
    private TableLayoutPanel _pnlGruzHost;
    private Label         _lblNvag;
    private TextBox       _txtNvag;
    private TableLayoutPanel _pnlNvagHost;
    private Label         _lblPotr;
    private TextBox       _txtPotr;
    private Button        _btnFind;
    private DataGridView  _grid;
    private Panel         _pnlStatus;
    private Label         _lblSlipNum;
    private TextBox       _txtSlipNum;
    private Label         _lblFrom;
    private TextBox       _txtFrom;
    private Label         _lblTo;
    private TextBox       _txtTo;
    private Button        _btnPreview;
    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel tableLayoutPanel2;
    private TableLayoutPanel tableLayoutPanel3;
    private Button _btnClearFilters;
}
