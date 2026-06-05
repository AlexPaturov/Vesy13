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
        _pnlTop          = new Panel();
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
        _btnBack         = new Button();

        _pnlTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        _pnlStatus.SuspendLayout();
        SuspendLayout();

        // ── _pnlTop ──────────────────────────────────────────────────────────
        _pnlTop.BackColor = Color.FromArgb(215, 225, 248);
        _pnlTop.Controls.Add(_rbGpri);
        _pnlTop.Controls.Add(_rbGras);
        _pnlTop.Controls.Add(_lblDateFrom);
        _pnlTop.Controls.Add(_dtpFrom);
        _pnlTop.Controls.Add(_lblDateTo);
        _pnlTop.Controls.Add(_dtpTo);
        _pnlTop.Controls.Add(_lblGruz);
        _pnlTop.Controls.Add(_txtGruz);
        _pnlTop.Controls.Add(_lblNvag);
        _pnlTop.Controls.Add(_txtNvag);
        _pnlTop.Controls.Add(_lblPotr);
        _pnlTop.Controls.Add(_txtPotr);
        _pnlTop.Controls.Add(_lblNdok);
        _pnlTop.Controls.Add(_txtNdok);
        _pnlTop.Controls.Add(_chkPotr);
        _pnlTop.Controls.Add(_btnFind);
        _pnlTop.Controls.Add(_btnClearFilters);
        _pnlTop.Dock = DockStyle.Top;
        _pnlTop.Location = new Point(0, 0);
        _pnlTop.Name = "_pnlTop";
        _pnlTop.Size = new Size(1264, 115);
        _pnlTop.TabIndex = 0;

        // ── _rbGpri ──────────────────────────────────────────────────────────
        _rbGpri.AutoSize = true;
        _rbGpri.Checked = true;
        _rbGpri.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _rbGpri.Location = new Point(10, 10);
        _rbGpri.Name = "_rbGpri";
        _rbGpri.Size = new Size(110, 24);
        _rbGpri.TabIndex = 0;
        _rbGpri.TabStop = true;
        _rbGpri.Text = "Приход (GPRI)";

        // ── _rbGras ──────────────────────────────────────────────────────────
        _rbGras.AutoSize = true;
        _rbGras.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _rbGras.Location = new Point(135, 10);
        _rbGras.Name = "_rbGras";
        _rbGras.Size = new Size(110, 24);
        _rbGras.TabIndex = 1;
        _rbGras.Text = "Расход (GRAS)";

        // ── _lblDateFrom ─────────────────────────────────────────────────────
        _lblDateFrom.AutoSize = true;
        _lblDateFrom.Font = new Font("Segoe UI", 9F);
        _lblDateFrom.ForeColor = Color.FromArgb(60, 60, 80);
        _lblDateFrom.Location = new Point(260, 14);
        _lblDateFrom.Name = "_lblDateFrom";
        _lblDateFrom.TabIndex = 2;
        _lblDateFrom.Text = "Дата с:";

        // ── _dtpFrom ─────────────────────────────────────────────────────────
        _dtpFrom.Font = new Font("Segoe UI", 9F);
        _dtpFrom.Format = DateTimePickerFormat.Short;
        _dtpFrom.Location = new Point(308, 9);
        _dtpFrom.Name = "_dtpFrom";
        _dtpFrom.Size = new Size(130, 27);
        _dtpFrom.TabIndex = 3;

        // ── _lblDateTo ───────────────────────────────────────────────────────
        _lblDateTo.AutoSize = true;
        _lblDateTo.Font = new Font("Segoe UI", 9F);
        _lblDateTo.ForeColor = Color.FromArgb(60, 60, 80);
        _lblDateTo.Location = new Point(446, 14);
        _lblDateTo.Name = "_lblDateTo";
        _lblDateTo.TabIndex = 4;
        _lblDateTo.Text = "по:";

        // ── _dtpTo ───────────────────────────────────────────────────────────
        _dtpTo.Font = new Font("Segoe UI", 9F);
        _dtpTo.Format = DateTimePickerFormat.Short;
        _dtpTo.Location = new Point(468, 9);
        _dtpTo.Name = "_dtpTo";
        _dtpTo.Size = new Size(130, 27);
        _dtpTo.TabIndex = 5;

        // ── _lblGruz ─────────────────────────────────────────────────────────
        _lblGruz.AutoSize = true;
        _lblGruz.Font = new Font("Segoe UI", 9F);
        _lblGruz.ForeColor = Color.FromArgb(60, 60, 80);
        _lblGruz.Location = new Point(613, 14);
        _lblGruz.Name = "_lblGruz";
        _lblGruz.TabIndex = 6;
        _lblGruz.Text = "Груз:";

        // ── _txtGruz ─────────────────────────────────────────────────────────
        _txtGruz.Font = new Font("Segoe UI", 9F);
        _txtGruz.Location = new Point(648, 9);
        _txtGruz.Name = "_txtGruz";
        _txtGruz.Size = new Size(210, 27);
        _txtGruz.TabIndex = 7;

        // ── _lblNvag ─────────────────────────────────────────────────────────
        _lblNvag.AutoSize = true;
        _lblNvag.Font = new Font("Segoe UI", 9F);
        _lblNvag.ForeColor = Color.FromArgb(60, 60, 80);
        _lblNvag.Location = new Point(873, 14);
        _lblNvag.Name = "_lblNvag";
        _lblNvag.TabIndex = 8;
        _lblNvag.Text = "Вагон:";

        // ── _txtNvag ─────────────────────────────────────────────────────────
        _txtNvag.Font = new Font("Segoe UI", 9F);
        _txtNvag.Location = new Point(920, 9);
        _txtNvag.Name = "_txtNvag";
        _txtNvag.Size = new Size(160, 27);
        _txtNvag.TabIndex = 9;

        // ── _lblPotr ─────────────────────────────────────────────────────────
        _lblPotr.AutoSize = true;
        _lblPotr.Font = new Font("Segoe UI", 9F);
        _lblPotr.ForeColor = Color.FromArgb(60, 60, 80);
        _lblPotr.Location = new Point(10, 52);
        _lblPotr.Name = "_lblPotr";
        _lblPotr.TabIndex = 10;
        _lblPotr.Text = "Получатель:";

        // ── _txtPotr ─────────────────────────────────────────────────────────
        _txtPotr.Font = new Font("Segoe UI", 9F);
        _txtPotr.Location = new Point(98, 47);
        _txtPotr.Name = "_txtPotr";
        _txtPotr.Size = new Size(210, 27);
        _txtPotr.TabIndex = 11;

        // ── _lblNdok ─────────────────────────────────────────────────────────
        _lblNdok.AutoSize = true;
        _lblNdok.Font = new Font("Segoe UI", 9F);
        _lblNdok.ForeColor = Color.FromArgb(60, 60, 80);
        _lblNdok.Location = new Point(320, 52);
        _lblNdok.Name = "_lblNdok";
        _lblNdok.TabIndex = 12;
        _lblNdok.Text = "№ отвесной:";

        // ── _txtNdok ─────────────────────────────────────────────────────────
        _txtNdok.Font = new Font("Segoe UI", 9F);
        _txtNdok.Location = new Point(408, 47);
        _txtNdok.Name = "_txtNdok";
        _txtNdok.Size = new Size(130, 27);
        _txtNdok.TabIndex = 13;

        // ── _chkPotr ─────────────────────────────────────────────────────────
        _chkPotr.AutoSize = true;
        _chkPotr.Font = new Font("Segoe UI", 9F);
        _chkPotr.ForeColor = Color.FromArgb(40, 40, 70);
        _chkPotr.Location = new Point(555, 50);
        _chkPotr.Name = "_chkPotr";
        _chkPotr.Size = new Size(220, 24);
        _chkPotr.TabIndex = 14;
        _chkPotr.Text = "Печатать грузополучателя";

        // ── _btnFind ─────────────────────────────────────────────────────────
        _btnFind.BackColor = Color.FromArgb(0, 100, 60);
        _btnFind.FlatAppearance.BorderSize = 0;
        _btnFind.FlatStyle = FlatStyle.Flat;
        _btnFind.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _btnFind.ForeColor = Color.White;
        _btnFind.Location = new Point(10, 82);
        _btnFind.Name = "_btnFind";
        _btnFind.Size = new Size(100, 26);
        _btnFind.TabIndex = 15;
        _btnFind.Text = "Найти";
        _btnFind.UseVisualStyleBackColor = false;
        _btnFind.Click += BtnFind_Click;

        // ── _btnClearFilters ─────────────────────────────────────────────────
        _btnClearFilters.BackColor = Color.FromArgb(100, 100, 110);
        _btnClearFilters.FlatAppearance.BorderSize = 0;
        _btnClearFilters.FlatStyle = FlatStyle.Flat;
        _btnClearFilters.Font = new Font("Segoe UI", 9F);
        _btnClearFilters.ForeColor = Color.White;
        _btnClearFilters.Location = new Point(120, 82);
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
        _grid.BackgroundColor = Color.White;
        _grid.BorderStyle = BorderStyle.None;
        _grid.ColumnHeadersHeight = 26;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.Dock = DockStyle.Fill;
        _grid.Font = new Font("Segoe UI", 9F);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.ReadOnly = false;
        _grid.RowHeadersVisible = false;
        _grid.RowHeadersWidth = 51;
        _grid.RowTemplate.Height = 22;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.TabIndex = 1;

        // ── _pnlStatus ────────────────────────────────────────────────────────
        _pnlStatus.BackColor = Color.FromArgb(18, 32, 65);
        _pnlStatus.Controls.Add(_lblSlipNum);
        _pnlStatus.Controls.Add(_txtSlipNum);
        _pnlStatus.Controls.Add(_lblFrom);
        _pnlStatus.Controls.Add(_txtFrom);
        _pnlStatus.Controls.Add(_lblTo);
        _pnlStatus.Controls.Add(_txtTo);
        _pnlStatus.Controls.Add(_btnPreview);
        _pnlStatus.Controls.Add(_btnBack);
        _pnlStatus.Dock = DockStyle.Bottom;
        _pnlStatus.Location = new Point(0, 626);
        _pnlStatus.Name = "_pnlStatus";
        _pnlStatus.Size = new Size(1264, 44);
        _pnlStatus.TabIndex = 2;

        // ── _lblSlipNum ──────────────────────────────────────────────────────
        _lblSlipNum.AutoSize = true;
        _lblSlipNum.Font = new Font("Segoe UI", 9F);
        _lblSlipNum.ForeColor = Color.FromArgb(200, 210, 230);
        _lblSlipNum.Location = new Point(10, 13);
        _lblSlipNum.Name = "_lblSlipNum";
        _lblSlipNum.TabIndex = 0;
        _lblSlipNum.Text = "Номер отвесной:";

        // ── _txtSlipNum ──────────────────────────────────────────────────────
        _txtSlipNum.Font = new Font("Segoe UI", 9F);
        _txtSlipNum.Location = new Point(128, 9);
        _txtSlipNum.Name = "_txtSlipNum";
        _txtSlipNum.Size = new Size(120, 27);
        _txtSlipNum.TabIndex = 1;

        // ── _lblFrom ─────────────────────────────────────────────────────────
        _lblFrom.AutoSize = true;
        _lblFrom.Font = new Font("Segoe UI", 9F);
        _lblFrom.ForeColor = Color.FromArgb(200, 210, 230);
        _lblFrom.Location = new Point(260, 13);
        _lblFrom.Name = "_lblFrom";
        _lblFrom.TabIndex = 2;
        _lblFrom.Text = "Откуда:";

        // ── _txtFrom ─────────────────────────────────────────────────────────
        _txtFrom.Font = new Font("Segoe UI", 9F);
        _txtFrom.Location = new Point(316, 9);
        _txtFrom.Name = "_txtFrom";
        _txtFrom.Size = new Size(250, 27);
        _txtFrom.TabIndex = 3;

        // ── _lblTo ───────────────────────────────────────────────────────────
        _lblTo.AutoSize = true;
        _lblTo.Font = new Font("Segoe UI", 9F);
        _lblTo.ForeColor = Color.FromArgb(200, 210, 230);
        _lblTo.Location = new Point(578, 13);
        _lblTo.Name = "_lblTo";
        _lblTo.TabIndex = 4;
        _lblTo.Text = "Куда:";

        // ── _txtTo ───────────────────────────────────────────────────────────
        _txtTo.Font = new Font("Segoe UI", 9F);
        _txtTo.Location = new Point(618, 9);
        _txtTo.Name = "_txtTo";
        _txtTo.Size = new Size(250, 27);
        _txtTo.TabIndex = 5;

        // ── _btnPreview ──────────────────────────────────────────────────────
        _btnPreview.BackColor = Color.FromArgb(0, 90, 160);
        _btnPreview.FlatAppearance.BorderSize = 0;
        _btnPreview.FlatStyle = FlatStyle.Flat;
        _btnPreview.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _btnPreview.ForeColor = Color.White;
        _btnPreview.Location = new Point(884, 9);
        _btnPreview.Name = "_btnPreview";
        _btnPreview.Size = new Size(170, 26);
        _btnPreview.TabIndex = 6;
        _btnPreview.Text = "Просмотр печати";
        _btnPreview.UseVisualStyleBackColor = false;
        _btnPreview.Click += BtnPreview_Click;

        // ── _btnBack ─────────────────────────────────────────────────────────
        _btnBack.BackColor = Color.FromArgb(40, 70, 130);
        _btnBack.FlatAppearance.BorderSize = 0;
        _btnBack.FlatStyle = FlatStyle.Flat;
        _btnBack.Font = new Font("Segoe UI", 8F);
        _btnBack.ForeColor = Color.White;
        _btnBack.Location = new Point(1064, 9);
        _btnBack.Name = "_btnBack";
        _btnBack.Size = new Size(90, 26);
        _btnBack.TabIndex = 7;
        _btnBack.Text = "← Назад";
        _btnBack.UseVisualStyleBackColor = false;
        _btnBack.Click += BtnBack_Click;

        // ── Form ──────────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(240, 242, 245);
        ClientSize = new Size(1264, 670);
        Controls.Add(_grid);
        Controls.Add(_pnlTop);
        Controls.Add(_pnlStatus);
        MinimumSize = new Size(860, 560);
        Name = "PrintForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Печать отвесных — Весы 13";

        _pnlTop.ResumeLayout(false);
        _pnlTop.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        _pnlStatus.ResumeLayout(false);
        _pnlStatus.PerformLayout();
        ResumeLayout(false);
    }

    private Panel         _pnlTop;
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
    private Button        _btnBack;
}
