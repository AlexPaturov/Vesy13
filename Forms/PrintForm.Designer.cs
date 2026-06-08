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

        _pnlTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        _pnlStatus.SuspendLayout();
        SuspendLayout();

        // ── _pnlTop ──────────────────────────────────────────────────────────
        _pnlTop.BackColor = UiColors.FilterBar;
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
        _rbGpri.Font = UiFonts.BodyBold;
        _rbGpri.ForeColor = UiColors.TextPrimary;
        _rbGpri.Location = new Point(10, 10);
        _rbGpri.Name = "_rbGpri";
        _rbGpri.Size = new Size(110, 24);
        _rbGpri.TabIndex = 0;
        _rbGpri.TabStop = true;
        _rbGpri.Text = "Приход (GPRI)";

        // ── _rbGras ──────────────────────────────────────────────────────────
        _rbGras.AutoSize = true;
        _rbGras.Font = UiFonts.BodyBold;
        _rbGras.ForeColor = UiColors.TextPrimary;
        _rbGras.Location = new Point(135, 10);
        _rbGras.Name = "_rbGras";
        _rbGras.Size = new Size(110, 24);
        _rbGras.TabIndex = 1;
        _rbGras.Text = "Расход (GRAS)";

        // ── _lblDateFrom ─────────────────────────────────────────────────────
        _lblDateFrom.AutoSize = true;
        _lblDateFrom.Font = UiFonts.Body;
        _lblDateFrom.ForeColor = UiColors.TextMuted;
        _lblDateFrom.Location = new Point(260, 14);
        _lblDateFrom.Name = "_lblDateFrom";
        _lblDateFrom.TabIndex = 2;
        _lblDateFrom.Text = "Дата с:";

        // ── _dtpFrom ─────────────────────────────────────────────────────────
        _dtpFrom.Font = UiFonts.Body;
        _dtpFrom.Format = DateTimePickerFormat.Short;
        _dtpFrom.Location = new Point(308, 9);
        _dtpFrom.Name = "_dtpFrom";
        _dtpFrom.Size = new Size(130, 27);
        _dtpFrom.TabIndex = 3;

        // ── _lblDateTo ───────────────────────────────────────────────────────
        _lblDateTo.AutoSize = true;
        _lblDateTo.Font = UiFonts.Body;
        _lblDateTo.ForeColor = UiColors.TextMuted;
        _lblDateTo.Location = new Point(446, 14);
        _lblDateTo.Name = "_lblDateTo";
        _lblDateTo.TabIndex = 4;
        _lblDateTo.Text = "по:";

        // ── _dtpTo ───────────────────────────────────────────────────────────
        _dtpTo.Font = UiFonts.Body;
        _dtpTo.Format = DateTimePickerFormat.Short;
        _dtpTo.Location = new Point(468, 9);
        _dtpTo.Name = "_dtpTo";
        _dtpTo.Size = new Size(130, 27);
        _dtpTo.TabIndex = 5;

        // ── _lblGruz ─────────────────────────────────────────────────────────
        _lblGruz.AutoSize = true;
        _lblGruz.Font = UiFonts.Body;
        _lblGruz.ForeColor = UiColors.TextMuted;
        _lblGruz.Location = new Point(613, 14);
        _lblGruz.Name = "_lblGruz";
        _lblGruz.TabIndex = 6;
        _lblGruz.Text = "Груз:";

        // ── _txtGruz ─────────────────────────────────────────────────────────
        _txtGruz.Font = UiFonts.Body;
        _txtGruz.BackColor = UiColors.InputBack;
        _txtGruz.ForeColor = UiColors.InputFore;
        _txtGruz.Location = new Point(648, 9);
        _txtGruz.Name = "_txtGruz";
        _txtGruz.Size = new Size(210, 27);
        _txtGruz.TabIndex = 7;

        // ── _lblNvag ─────────────────────────────────────────────────────────
        _lblNvag.AutoSize = true;
        _lblNvag.Font = UiFonts.Body;
        _lblNvag.ForeColor = UiColors.TextMuted;
        _lblNvag.Location = new Point(873, 14);
        _lblNvag.Name = "_lblNvag";
        _lblNvag.TabIndex = 8;
        _lblNvag.Text = "Вагон:";

        // ── _txtNvag ─────────────────────────────────────────────────────────
        _txtNvag.Font = UiFonts.Body;
        _txtNvag.BackColor = UiColors.InputBack;
        _txtNvag.ForeColor = UiColors.InputFore;
        _txtNvag.Location = new Point(920, 9);
        _txtNvag.Name = "_txtNvag";
        _txtNvag.Size = new Size(160, 27);
        _txtNvag.TabIndex = 9;

        // ── _lblPotr ─────────────────────────────────────────────────────────
        _lblPotr.AutoSize = true;
        _lblPotr.Font = UiFonts.Body;
        _lblPotr.ForeColor = UiColors.TextMuted;
        _lblPotr.Location = new Point(10, 52);
        _lblPotr.Name = "_lblPotr";
        _lblPotr.TabIndex = 10;
        _lblPotr.Text = "Получатель:";

        // ── _txtPotr ─────────────────────────────────────────────────────────
        _txtPotr.Font = UiFonts.Body;
        _txtPotr.BackColor = UiColors.InputBack;
        _txtPotr.ForeColor = UiColors.InputFore;
        _txtPotr.Location = new Point(98, 47);
        _txtPotr.Name = "_txtPotr";
        _txtPotr.Size = new Size(210, 27);
        _txtPotr.TabIndex = 11;

        // ── _lblNdok ─────────────────────────────────────────────────────────
        _lblNdok.AutoSize = true;
        _lblNdok.Font = UiFonts.Body;
        _lblNdok.ForeColor = UiColors.TextMuted;
        _lblNdok.Location = new Point(320, 52);
        _lblNdok.Name = "_lblNdok";
        _lblNdok.TabIndex = 12;
        _lblNdok.Text = "№ отвесной:";

        // ── _txtNdok ─────────────────────────────────────────────────────────
        _txtNdok.Font = UiFonts.Body;
        _txtNdok.BackColor = UiColors.InputBack;
        _txtNdok.ForeColor = UiColors.InputFore;
        _txtNdok.Location = new Point(408, 47);
        _txtNdok.Name = "_txtNdok";
        _txtNdok.Size = new Size(130, 27);
        _txtNdok.TabIndex = 13;

        // ── _chkPotr ─────────────────────────────────────────────────────────
        _chkPotr.AutoSize = true;
        _chkPotr.Font = UiFonts.Body;
        _chkPotr.ForeColor = UiColors.TextPrimary;
        _chkPotr.Location = new Point(555, 50);
        _chkPotr.Name = "_chkPotr";
        _chkPotr.Size = new Size(220, 24);
        _chkPotr.TabIndex = 14;
        _chkPotr.Text = "Печатать грузополучателя";

        // ── _btnFind ─────────────────────────────────────────────────────────
        _btnFind.BackColor = UiColors.PrimaryAction;
        _btnFind.FlatAppearance.BorderSize = 0;
        _btnFind.FlatStyle = FlatStyle.Flat;
        _btnFind.Font = UiFonts.BodyBold;
        _btnFind.ForeColor = UiColors.TextOnDark;
        _btnFind.Location = new Point(10, 82);
        _btnFind.Name = "_btnFind";
        _btnFind.Size = new Size(100, 26);
        _btnFind.TabIndex = 15;
        _btnFind.Text = "Найти";
        _btnFind.UseVisualStyleBackColor = false;
        _btnFind.Click += BtnFind_Click;

        // ── _btnClearFilters ─────────────────────────────────────────────────
        _btnClearFilters.BackColor = UiColors.NeutralAction;
        _btnClearFilters.FlatAppearance.BorderSize = 0;
        _btnClearFilters.FlatStyle = FlatStyle.Flat;
        _btnClearFilters.Font = UiFonts.Body;
        _btnClearFilters.ForeColor = UiColors.TextPrimary;
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
        _grid.BackgroundColor = UiColors.Surface;
        _grid.BorderStyle = BorderStyle.None;
        _grid.ColumnHeadersDefaultCellStyle.BackColor = UiColors.GridHeaderBack;
        _grid.ColumnHeadersDefaultCellStyle.ForeColor = UiColors.GridHeaderText;
        _grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = UiColors.GridHeaderBack;
        _grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = UiColors.GridHeaderText;
        _grid.ColumnHeadersHeight = 32;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
        _grid.DefaultCellStyle.SelectionBackColor = UiColors.GridSelectionBack;
        _grid.DefaultCellStyle.SelectionForeColor = UiColors.GridSelectionText;
        _grid.EnableHeadersVisualStyles = false;
        _grid.GridColor = UiColors.GridLine;
        _grid.Dock = DockStyle.Fill;
        _grid.Font = UiFonts.GridBody;
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.ReadOnly = false;
        _grid.RowHeadersVisible = false;
        _grid.RowHeadersWidth = 51;
        _grid.RowTemplate.Height = 28;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.TabIndex = 1;

        // ── _pnlStatus ────────────────────────────────────────────────────────
        _pnlStatus.BackColor = UiColors.StatusBar;
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
        _lblSlipNum.Font = UiFonts.Body;
        _lblSlipNum.ForeColor = UiColors.TextOnDarkMuted;
        _lblSlipNum.Location = new Point(10, 13);
        _lblSlipNum.Name = "_lblSlipNum";
        _lblSlipNum.TabIndex = 0;
        _lblSlipNum.Text = "Номер отвесной:";

        // ── _txtSlipNum ──────────────────────────────────────────────────────
        _txtSlipNum.Font = UiFonts.Body;
        _txtSlipNum.BackColor = UiColors.InputBack;
        _txtSlipNum.ForeColor = UiColors.InputFore;
        _txtSlipNum.Location = new Point(128, 9);
        _txtSlipNum.Name = "_txtSlipNum";
        _txtSlipNum.Size = new Size(120, 27);
        _txtSlipNum.TabIndex = 1;

        // ── _lblFrom ─────────────────────────────────────────────────────────
        _lblFrom.AutoSize = true;
        _lblFrom.Font = UiFonts.Body;
        _lblFrom.ForeColor = UiColors.TextOnDarkMuted;
        _lblFrom.Location = new Point(260, 13);
        _lblFrom.Name = "_lblFrom";
        _lblFrom.TabIndex = 2;
        _lblFrom.Text = "Откуда:";

        // ── _txtFrom ─────────────────────────────────────────────────────────
        _txtFrom.Font = UiFonts.Body;
        _txtFrom.BackColor = UiColors.InputBack;
        _txtFrom.ForeColor = UiColors.InputFore;
        _txtFrom.Location = new Point(316, 9);
        _txtFrom.Name = "_txtFrom";
        _txtFrom.Size = new Size(250, 27);
        _txtFrom.TabIndex = 3;

        // ── _lblTo ───────────────────────────────────────────────────────────
        _lblTo.AutoSize = true;
        _lblTo.Font = UiFonts.Body;
        _lblTo.ForeColor = UiColors.TextOnDarkMuted;
        _lblTo.Location = new Point(578, 13);
        _lblTo.Name = "_lblTo";
        _lblTo.TabIndex = 4;
        _lblTo.Text = "Куда:";

        // ── _txtTo ───────────────────────────────────────────────────────────
        _txtTo.Font = UiFonts.Body;
        _txtTo.BackColor = UiColors.InputBack;
        _txtTo.ForeColor = UiColors.InputFore;
        _txtTo.Location = new Point(618, 9);
        _txtTo.Name = "_txtTo";
        _txtTo.Size = new Size(250, 27);
        _txtTo.TabIndex = 5;

        // ── _btnPreview ──────────────────────────────────────────────────────
        _btnPreview.BackColor = UiColors.InfoAction;
        _btnPreview.FlatAppearance.BorderSize = 0;
        _btnPreview.FlatStyle = FlatStyle.Flat;
        _btnPreview.Font = UiFonts.BodyBold;
        _btnPreview.ForeColor = UiColors.TextOnDark;
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
}
