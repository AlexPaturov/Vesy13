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
        _pnlTopActions = new Panel();
        _btnFind = new Button();
        _btnClearFilters = new Button();
        _pnlTopRow2 = new Panel();
        _lblPotr = new Label();
        _txtPotr = new TextBox();
        _lblNdok = new Label();
        _txtNdok = new TextBox();
        _chkPotr = new CheckBox();
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
        _lblSlipNum = new Label();
        _txtSlipNum = new TextBox();
        _lblFrom = new Label();
        _txtFrom = new TextBox();
        _lblTo = new Label();
        _txtTo = new TextBox();
        _btnPreview = new Button();
        _layoutMain.SuspendLayout();
        _pnlTop.SuspendLayout();
        _pnlTopActions.SuspendLayout();
        _pnlTopRow2.SuspendLayout();
        _pnlTopRow1.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        _pnlNvagHost.SuspendLayout();
        _pnlGruzHost.SuspendLayout();
        _pnlDateToHost.SuspendLayout();
        _pnlDateFromHost.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        _pnlStatus.SuspendLayout();
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
        _pnlTop.Controls.Add(_pnlTopActions);
        _pnlTop.Controls.Add(_pnlTopRow2);
        _pnlTop.Controls.Add(_pnlTopRow1);
        _pnlTop.Dock = DockStyle.Top;
        _pnlTop.Location = new Point(3, 3);
        _pnlTop.Name = "_pnlTop";
        _pnlTop.Size = new Size(1258, 169);
        _pnlTop.TabIndex = 0;
        // 
        // _pnlTopActions
        // 
        _pnlTopActions.Controls.Add(_btnFind);
        _pnlTopActions.Controls.Add(_btnClearFilters);
        _pnlTopActions.Location = new Point(0, 122);
        _pnlTopActions.Name = "_pnlTopActions";
        _pnlTopActions.Size = new Size(1264, 41);
        _pnlTopActions.TabIndex = 2;
        // 
        // _btnFind
        // 
        _btnFind.FlatAppearance.BorderSize = 0;
        _btnFind.FlatStyle = FlatStyle.Flat;
        _btnFind.Location = new Point(10, 6);
        _btnFind.Name = "_btnFind";
        _btnFind.Size = new Size(100, 26);
        _btnFind.TabIndex = 15;
        _btnFind.Text = "Найти";
        _btnFind.UseVisualStyleBackColor = false;
        _btnFind.Click += BtnFind_Click;
        // 
        // _btnClearFilters
        // 
        _btnClearFilters.FlatAppearance.BorderSize = 0;
        _btnClearFilters.FlatStyle = FlatStyle.Flat;
        _btnClearFilters.Location = new Point(120, 6);
        _btnClearFilters.Name = "_btnClearFilters";
        _btnClearFilters.Size = new Size(150, 26);
        _btnClearFilters.TabIndex = 16;
        _btnClearFilters.Text = "Очистить фильтры";
        _btnClearFilters.UseVisualStyleBackColor = false;
        _btnClearFilters.Click += BtnClearFilters_Click;
        // 
        // _pnlTopRow2
        // 
        _pnlTopRow2.Controls.Add(_lblPotr);
        _pnlTopRow2.Controls.Add(_txtPotr);
        _pnlTopRow2.Controls.Add(_lblNdok);
        _pnlTopRow2.Controls.Add(_txtNdok);
        _pnlTopRow2.Controls.Add(_chkPotr);
        _pnlTopRow2.Location = new Point(0, 90);
        _pnlTopRow2.Name = "_pnlTopRow2";
        _pnlTopRow2.Size = new Size(1264, 34);
        _pnlTopRow2.TabIndex = 1;
        // 
        // _lblPotr
        // 
        _lblPotr.AutoSize = true;
        _lblPotr.Location = new Point(10, 8);
        _lblPotr.Name = "_lblPotr";
        _lblPotr.Size = new Size(76, 15);
        _lblPotr.TabIndex = 10;
        _lblPotr.Text = "Получатель:";
        // 
        // _txtPotr
        // 
        _txtPotr.Location = new Point(98, 3);
        _txtPotr.Name = "_txtPotr";
        _txtPotr.Size = new Size(210, 23);
        _txtPotr.TabIndex = 11;
        // 
        // _lblNdok
        // 
        _lblNdok.AutoSize = true;
        _lblNdok.Location = new Point(320, 8);
        _lblNdok.Name = "_lblNdok";
        _lblNdok.Size = new Size(77, 15);
        _lblNdok.TabIndex = 12;
        _lblNdok.Text = "№ отвесной:";
        // 
        // _txtNdok
        // 
        _txtNdok.Location = new Point(408, 3);
        _txtNdok.Name = "_txtNdok";
        _txtNdok.Size = new Size(130, 23);
        _txtNdok.TabIndex = 13;
        // 
        // _chkPotr
        // 
        _chkPotr.AutoSize = true;
        _chkPotr.Location = new Point(555, 6);
        _chkPotr.Name = "_chkPotr";
        _chkPotr.Size = new Size(173, 19);
        _chkPotr.TabIndex = 14;
        _chkPotr.Text = "Печатать грузополучателя";
        // 
        // _pnlTopRow1
        // 
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
        tableLayoutPanel1.Dock = DockStyle.Top;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(1264, 47);
        tableLayoutPanel1.TabIndex = 10;
        // 
        // _rbGpri
        // 
        _rbGpri.AutoSize = true;
        _rbGpri.Checked = true;
        _rbGpri.Dock = DockStyle.Fill;
        _rbGpri.Location = new Point(4, 4);
        _rbGpri.Name = "_rbGpri";
        _rbGpri.Size = new Size(97, 39);
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
        _pnlNvagHost.Size = new Size(213, 45);
        _pnlNvagHost.TabIndex = 9;
        // 
        // _txtNvag
        // 
        _txtNvag.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtNvag.Location = new Point(6, 11);
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
        _lblNvag.Size = new Size(72, 45);
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
        _pnlGruzHost.Size = new Size(219, 45);
        _pnlGruzHost.TabIndex = 7;
        // 
        // _txtGruz
        // 
        _txtGruz.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtGruz.Location = new Point(6, 11);
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
        _lblGruz.Size = new Size(79, 45);
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
        _pnlDateToHost.Size = new Size(159, 45);
        _pnlDateToHost.TabIndex = 5;
        // 
        // _dtpTo
        // 
        _dtpTo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _dtpTo.Format = DateTimePickerFormat.Short;
        _dtpTo.Location = new Point(6, 11);
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
        _lblDateTo.Size = new Size(66, 45);
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
        _pnlDateFromHost.Size = new Size(153, 45);
        _pnlDateFromHost.TabIndex = 3;
        // 
        // _dtpFrom
        // 
        _dtpFrom.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _dtpFrom.Format = DateTimePickerFormat.Short;
        _dtpFrom.Location = new Point(6, 11);
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
        _rbGras.Size = new Size(85, 39);
        _rbGras.TabIndex = 1;
        _rbGras.Text = "Расход";
        // 
        // _lblDateFrom
        // 
        _lblDateFrom.AutoSize = true;
        _lblDateFrom.Dock = DockStyle.Fill;
        _lblDateFrom.Location = new Point(200, 1);
        _lblDateFrom.Name = "_lblDateFrom";
        _lblDateFrom.Size = new Size(74, 45);
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
        _grid.Location = new Point(3, 178);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.RowHeadersVisible = false;
        _grid.RowHeadersWidth = 51;
        _grid.RowTemplate.Height = 28;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.Size = new Size(1258, 439);
        _grid.TabIndex = 1;
        // 
        // _pnlStatus
        // 
        _pnlStatus.Controls.Add(_lblSlipNum);
        _pnlStatus.Controls.Add(_txtSlipNum);
        _pnlStatus.Controls.Add(_lblFrom);
        _pnlStatus.Controls.Add(_txtFrom);
        _pnlStatus.Controls.Add(_lblTo);
        _pnlStatus.Controls.Add(_txtTo);
        _pnlStatus.Controls.Add(_btnPreview);
        _pnlStatus.Dock = DockStyle.Bottom;
        _pnlStatus.Location = new Point(3, 623);
        _pnlStatus.Name = "_pnlStatus";
        _pnlStatus.Size = new Size(1258, 44);
        _pnlStatus.TabIndex = 2;
        // 
        // _lblSlipNum
        // 
        _lblSlipNum.AutoSize = true;
        _lblSlipNum.Location = new Point(10, 13);
        _lblSlipNum.Name = "_lblSlipNum";
        _lblSlipNum.Size = new Size(102, 15);
        _lblSlipNum.TabIndex = 0;
        _lblSlipNum.Text = "Номер отвесной:";
        // 
        // _txtSlipNum
        // 
        _txtSlipNum.Location = new Point(128, 9);
        _txtSlipNum.Name = "_txtSlipNum";
        _txtSlipNum.Size = new Size(120, 23);
        _txtSlipNum.TabIndex = 1;
        // 
        // _lblFrom
        // 
        _lblFrom.AutoSize = true;
        _lblFrom.Location = new Point(260, 13);
        _lblFrom.Name = "_lblFrom";
        _lblFrom.Size = new Size(48, 15);
        _lblFrom.TabIndex = 2;
        _lblFrom.Text = "Откуда:";
        // 
        // _txtFrom
        // 
        _txtFrom.Location = new Point(316, 9);
        _txtFrom.Name = "_txtFrom";
        _txtFrom.Size = new Size(250, 23);
        _txtFrom.TabIndex = 3;
        // 
        // _lblTo
        // 
        _lblTo.AutoSize = true;
        _lblTo.Location = new Point(578, 13);
        _lblTo.Name = "_lblTo";
        _lblTo.Size = new Size(35, 15);
        _lblTo.TabIndex = 4;
        _lblTo.Text = "Куда:";
        // 
        // _txtTo
        // 
        _txtTo.Location = new Point(618, 9);
        _txtTo.Name = "_txtTo";
        _txtTo.Size = new Size(250, 23);
        _txtTo.TabIndex = 5;
        // 
        // _btnPreview
        // 
        _btnPreview.FlatAppearance.BorderSize = 0;
        _btnPreview.FlatStyle = FlatStyle.Flat;
        _btnPreview.Location = new Point(884, 9);
        _btnPreview.Name = "_btnPreview";
        _btnPreview.Size = new Size(170, 26);
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
        _pnlTopActions.ResumeLayout(false);
        _pnlTopRow2.ResumeLayout(false);
        _pnlTopRow2.PerformLayout();
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
        _pnlStatus.ResumeLayout(false);
        _pnlStatus.PerformLayout();
        ResumeLayout(false);
    }

    private TableLayoutPanel _layoutMain;
    private Panel         _pnlTop;
    private Panel         _pnlTopRow1;
    private Panel         _pnlTopRow2;
    private Panel         _pnlTopActions;
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
    private Label         _lblNdok;
    private TextBox       _txtNdok;
    private CheckBox      _chkPotr;
    private Button        _btnFind;
    private Button        _btnClearFilters;
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
}
