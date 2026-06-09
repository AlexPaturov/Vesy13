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
        _layoutMain      = new TableLayoutPanel();
        _pnlTop          = new Panel();
        _pnlTopRow1      = new Panel();
        _pnlTopRow2      = new Panel();
        _pnlTopActions   = new Panel();
        _rbGpri          = new RadioButton();
        _rbGras          = new RadioButton();
        _lblDateFrom     = new Label();
        _dtpFrom         = new DateTimePicker();
        _lblDateTo       = new Label();
        _dtpTo           = new DateTimePicker();
        _lblGruz         = new Label();
        _txtGruz         = new TextBox();
        _lblNvag         = new Label();
        _txtNvag         = new TextBox();
        _lblPotr         = new Label();
        _txtPotr         = new TextBox();
        _lblNdok         = new Label();
        _txtNdok         = new TextBox();
        _chkPotr         = new CheckBox();
        _btnFind         = new Button();
        _btnClearFilters = new Button();
        _grid            = new DataGridView();
        _pnlStatus       = new Panel();
        _lblSlipNum      = new Label();
        _txtSlipNum      = new TextBox();
        _lblFrom         = new Label();
        _txtFrom         = new TextBox();
        _lblTo           = new Label();
        _txtTo           = new TextBox();
        _btnPreview      = new Button();

        _layoutMain.SuspendLayout();
        _pnlTop.SuspendLayout();
        _pnlTopRow1.SuspendLayout();
        _pnlTopRow2.SuspendLayout();
        _pnlTopActions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        _pnlStatus.SuspendLayout();
        SuspendLayout();

        // ── _layoutMain ─────────────────────────────────────────────────────
        _layoutMain.ColumnCount = 1;
        _layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _layoutMain.Dock = DockStyle.Fill;
        _layoutMain.Location = new Point(0, 0);
        _layoutMain.Margin = new Padding(0);
        _layoutMain.Name = "_layoutMain";
        _layoutMain.RowCount = 3;
        _layoutMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _layoutMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _layoutMain.Size = new Size(1264, 670);
        _layoutMain.TabIndex = 0;
        _layoutMain.Controls.Add(_pnlTop, 0, 0);
        _layoutMain.Controls.Add(_grid, 0, 1);
        _layoutMain.Controls.Add(_pnlStatus, 0, 2);

        // ── _pnlTop ──────────────────────────────────────────────────────────
        _pnlTop.Controls.Add(_pnlTopActions);
        _pnlTop.Controls.Add(_pnlTopRow2);
        _pnlTop.Controls.Add(_pnlTopRow1);
        _pnlTop.Dock = DockStyle.Top;
        _pnlTop.Location = new Point(0, 0);
        _pnlTop.Name = "_pnlTop";
        _pnlTop.Size = new Size(1264, 115);
        _pnlTop.TabIndex = 0;

        // ── _pnlTopRow1 ──────────────────────────────────────────────────────
        _pnlTopRow1.Controls.Add(_rbGpri);
        _pnlTopRow1.Controls.Add(_rbGras);
        _pnlTopRow1.Controls.Add(_lblDateFrom);
        _pnlTopRow1.Controls.Add(_dtpFrom);
        _pnlTopRow1.Controls.Add(_lblDateTo);
        _pnlTopRow1.Controls.Add(_dtpTo);
        _pnlTopRow1.Controls.Add(_lblGruz);
        _pnlTopRow1.Controls.Add(_txtGruz);
        _pnlTopRow1.Controls.Add(_lblNvag);
        _pnlTopRow1.Controls.Add(_txtNvag);
        _pnlTopRow1.Dock = DockStyle.Top;
        _pnlTopRow1.Location = new Point(0, 0);
        _pnlTopRow1.Name = "_pnlTopRow1";
        _pnlTopRow1.Size = new Size(1264, 40);
        _pnlTopRow1.TabIndex = 0;

        // ── _rbGpri ──────────────────────────────────────────────────────────
        _rbGpri.AutoSize = true;
        _rbGpri.Checked = true;
        _rbGpri.Location = new Point(10, 10);
        _rbGpri.Name = "_rbGpri";
        _rbGpri.Size = new Size(110, 24);
        _rbGpri.TabIndex = 0;
        _rbGpri.TabStop = true;
        _rbGpri.Text = "Приход (GPRI)";

        // ── _rbGras ──────────────────────────────────────────────────────────
        _rbGras.AutoSize = true;
        _rbGras.Location = new Point(135, 10);
        _rbGras.Name = "_rbGras";
        _rbGras.Size = new Size(110, 24);
        _rbGras.TabIndex = 1;
        _rbGras.Text = "Расход (GRAS)";

        // ── _lblDateFrom ─────────────────────────────────────────────────────
        _lblDateFrom.AutoSize = true;
        _lblDateFrom.Location = new Point(260, 14);
        _lblDateFrom.Name = "_lblDateFrom";
        _lblDateFrom.TabIndex = 2;
        _lblDateFrom.Text = "Дата с:";

        // ── _dtpFrom ─────────────────────────────────────────────────────────
        _dtpFrom.Format = DateTimePickerFormat.Short;
        _dtpFrom.Location = new Point(308, 9);
        _dtpFrom.Name = "_dtpFrom";
        _dtpFrom.Size = new Size(130, 27);
        _dtpFrom.TabIndex = 3;

        // ── _lblDateTo ───────────────────────────────────────────────────────
        _lblDateTo.AutoSize = true;
        _lblDateTo.Location = new Point(446, 14);
        _lblDateTo.Name = "_lblDateTo";
        _lblDateTo.TabIndex = 4;
        _lblDateTo.Text = "по:";

        // ── _dtpTo ───────────────────────────────────────────────────────────
        _dtpTo.Format = DateTimePickerFormat.Short;
        _dtpTo.Location = new Point(468, 9);
        _dtpTo.Name = "_dtpTo";
        _dtpTo.Size = new Size(130, 27);
        _dtpTo.TabIndex = 5;

        // ── _lblGruz ─────────────────────────────────────────────────────────
        _lblGruz.AutoSize = true;
        _lblGruz.Location = new Point(613, 14);
        _lblGruz.Name = "_lblGruz";
        _lblGruz.TabIndex = 6;
        _lblGruz.Text = "Груз:";

        // ── _txtGruz ─────────────────────────────────────────────────────────
        _txtGruz.Location = new Point(648, 9);
        _txtGruz.Name = "_txtGruz";
        _txtGruz.Size = new Size(210, 27);
        _txtGruz.TabIndex = 7;

        // ── _lblNvag ─────────────────────────────────────────────────────────
        _lblNvag.AutoSize = true;
        _lblNvag.Location = new Point(873, 14);
        _lblNvag.Name = "_lblNvag";
        _lblNvag.TabIndex = 8;
        _lblNvag.Text = "Вагон:";

        // ── _txtNvag ─────────────────────────────────────────────────────────
        _txtNvag.Location = new Point(920, 9);
        _txtNvag.Name = "_txtNvag";
        _txtNvag.Size = new Size(160, 27);
        _txtNvag.TabIndex = 9;

        // ── _pnlTopRow2 ──────────────────────────────────────────────────────
        _pnlTopRow2.Controls.Add(_lblPotr);
        _pnlTopRow2.Controls.Add(_txtPotr);
        _pnlTopRow2.Controls.Add(_lblNdok);
        _pnlTopRow2.Controls.Add(_txtNdok);
        _pnlTopRow2.Controls.Add(_chkPotr);
        _pnlTopRow2.Dock = DockStyle.Top;
        _pnlTopRow2.Location = new Point(0, 40);
        _pnlTopRow2.Name = "_pnlTopRow2";
        _pnlTopRow2.Size = new Size(1264, 34);
        _pnlTopRow2.TabIndex = 1;

        // ── _lblPotr ─────────────────────────────────────────────────────────
        _lblPotr.AutoSize = true;
        _lblPotr.Location = new Point(10, 8);
        _lblPotr.Name = "_lblPotr";
        _lblPotr.TabIndex = 10;
        _lblPotr.Text = "Получатель:";

        // ── _txtPotr ─────────────────────────────────────────────────────────
        _txtPotr.Location = new Point(98, 3);
        _txtPotr.Name = "_txtPotr";
        _txtPotr.Size = new Size(210, 27);
        _txtPotr.TabIndex = 11;

        // ── _lblNdok ─────────────────────────────────────────────────────────
        _lblNdok.AutoSize = true;
        _lblNdok.Location = new Point(320, 8);
        _lblNdok.Name = "_lblNdok";
        _lblNdok.TabIndex = 12;
        _lblNdok.Text = "№ отвесной:";

        // ── _txtNdok ─────────────────────────────────────────────────────────
        _txtNdok.Location = new Point(408, 3);
        _txtNdok.Name = "_txtNdok";
        _txtNdok.Size = new Size(130, 27);
        _txtNdok.TabIndex = 13;

        // ── _chkPotr ─────────────────────────────────────────────────────────
        _chkPotr.AutoSize = true;
        _chkPotr.Location = new Point(555, 6);
        _chkPotr.Name = "_chkPotr";
        _chkPotr.Size = new Size(220, 24);
        _chkPotr.TabIndex = 14;
        _chkPotr.Text = "Печатать грузополучателя";

        // ── _pnlTopActions ───────────────────────────────────────────────────
        _pnlTopActions.Controls.Add(_btnFind);
        _pnlTopActions.Controls.Add(_btnClearFilters);
        _pnlTopActions.Dock = DockStyle.Top;
        _pnlTopActions.Location = new Point(0, 74);
        _pnlTopActions.Name = "_pnlTopActions";
        _pnlTopActions.Size = new Size(1264, 41);
        _pnlTopActions.TabIndex = 2;

        // ── _btnFind ─────────────────────────────────────────────────────────
        _btnFind.FlatAppearance.BorderSize = 0;
        _btnFind.FlatStyle = FlatStyle.Flat;
        _btnFind.Location = new Point(10, 6);
        _btnFind.Name = "_btnFind";
        _btnFind.Size = new Size(100, 26);
        _btnFind.TabIndex = 15;
        _btnFind.Text = "Найти";
        _btnFind.UseVisualStyleBackColor = false;
        _btnFind.Click += BtnFind_Click;

        // ── _btnClearFilters ─────────────────────────────────────────────────
        _btnClearFilters.FlatAppearance.BorderSize = 0;
        _btnClearFilters.FlatStyle = FlatStyle.Flat;
        _btnClearFilters.Location = new Point(120, 6);
        _btnClearFilters.Name = "_btnClearFilters";
        _btnClearFilters.Size = new Size(150, 26);
        _btnClearFilters.TabIndex = 16;
        _btnClearFilters.Text = "Очистить фильтры";
        _btnClearFilters.UseVisualStyleBackColor = false;
        _btnClearFilters.Click += BtnClearFilters_Click;

        // ── _grid ─────────────────────────────────────────────────────────────
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AllowUserToResizeRows = false;
        _grid.BackgroundColor = UiColors.Surface;
        _grid.BorderStyle = BorderStyle.None;
        _grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = UiColors.GridHeaderBack;
        _grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = UiColors.GridHeaderText;
        _grid.ColumnHeadersHeight = 32;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.DefaultCellStyle.SelectionBackColor = UiColors.GridSelectionBack;
        _grid.DefaultCellStyle.SelectionForeColor = UiColors.GridSelectionText;
        _grid.EnableHeadersVisualStyles = false;
        _grid.GridColor = UiColors.GridLine;
        _grid.Dock = DockStyle.Fill;
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.ReadOnly = false;
        _grid.RowHeadersVisible = false;
        _grid.RowHeadersWidth = 51;
        _grid.RowTemplate.Height = 28;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.TabIndex = 1;

        // ── _pnlStatus ────────────────────────────────────────────────────────
        _pnlStatus.Controls.Add(_lblSlipNum);
        _pnlStatus.Controls.Add(_txtSlipNum);
        _pnlStatus.Controls.Add(_lblFrom);
        _pnlStatus.Controls.Add(_txtFrom);
        _pnlStatus.Controls.Add(_lblTo);
        _pnlStatus.Controls.Add(_txtTo);
        _pnlStatus.Controls.Add(_btnPreview);
        _pnlStatus.Dock = DockStyle.Bottom;
        _pnlStatus.Location = new Point(0, 626);
        _pnlStatus.Name = "_pnlStatus";
        _pnlStatus.Size = new Size(1264, 44);
        _pnlStatus.TabIndex = 2;

        // ── _lblSlipNum ──────────────────────────────────────────────────────
        _lblSlipNum.AutoSize = true;
        _lblSlipNum.Location = new Point(10, 13);
        _lblSlipNum.Name = "_lblSlipNum";
        _lblSlipNum.TabIndex = 0;
        _lblSlipNum.Text = "Номер отвесной:";

        // ── _txtSlipNum ──────────────────────────────────────────────────────
        _txtSlipNum.Location = new Point(128, 9);
        _txtSlipNum.Name = "_txtSlipNum";
        _txtSlipNum.Size = new Size(120, 27);
        _txtSlipNum.TabIndex = 1;

        // ── _lblFrom ─────────────────────────────────────────────────────────
        _lblFrom.AutoSize = true;
        _lblFrom.Location = new Point(260, 13);
        _lblFrom.Name = "_lblFrom";
        _lblFrom.TabIndex = 2;
        _lblFrom.Text = "Откуда:";

        // ── _txtFrom ─────────────────────────────────────────────────────────
        _txtFrom.Location = new Point(316, 9);
        _txtFrom.Name = "_txtFrom";
        _txtFrom.Size = new Size(250, 27);
        _txtFrom.TabIndex = 3;

        // ── _lblTo ───────────────────────────────────────────────────────────
        _lblTo.AutoSize = true;
        _lblTo.Location = new Point(578, 13);
        _lblTo.Name = "_lblTo";
        _lblTo.TabIndex = 4;
        _lblTo.Text = "Куда:";

        // ── _txtTo ───────────────────────────────────────────────────────────
        _txtTo.Location = new Point(618, 9);
        _txtTo.Name = "_txtTo";
        _txtTo.Size = new Size(250, 27);
        _txtTo.TabIndex = 5;

        // ── _btnPreview ──────────────────────────────────────────────────────
        _btnPreview.FlatAppearance.BorderSize = 0;
        _btnPreview.FlatStyle = FlatStyle.Flat;
        _btnPreview.Location = new Point(884, 9);
        _btnPreview.Name = "_btnPreview";
        _btnPreview.Size = new Size(170, 26);
        _btnPreview.TabIndex = 6;
        _btnPreview.Text = "Просмотр печати";
        _btnPreview.UseVisualStyleBackColor = false;
        _btnPreview.Click += BtnPreview_Click;


        // ── Form ──────────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = UiColors.AppBackground;
        ClientSize = new Size(1264, 670);
        Controls.Add(_layoutMain);
        MinimumSize = new Size(860, 560);
        Name = "PrintForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Печать отвесных — Весы 13";

        _pnlTopActions.ResumeLayout(false);
        _pnlTopRow2.ResumeLayout(false);
        _pnlTopRow2.PerformLayout();
        _pnlTopRow1.ResumeLayout(false);
        _pnlTopRow1.PerformLayout();
        _layoutMain.ResumeLayout(false);
        _pnlTop.ResumeLayout(false);
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
    private Label         _lblDateTo;
    private DateTimePicker _dtpTo;
    private Label         _lblGruz;
    private TextBox       _txtGruz;
    private Label         _lblNvag;
    private TextBox       _txtNvag;
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
}
