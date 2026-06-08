namespace Vesy13.Forms;

partial class LogsForm
{
    private System.ComponentModel.IContainer components = null!;

    private Panel               _pnlFilter = null!;
    private Label               _lblFrom   = null!;
    private DateTimePicker      _dtpFrom   = null!;
    private Label               _lblTo     = null!;
    private DateTimePicker      _dtpTo     = null!;
    private Button              _btnFind   = null!;
    private DataGridView        _grid      = null!;
    private Panel               _pnlStatus = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        _pnlFilter = new Panel();
        _lblFrom   = new Label();
        _dtpFrom   = new DateTimePicker();
        _lblTo     = new Label();
        _dtpTo     = new DateTimePicker();
        _btnFind   = new Button();
        _grid      = new DataGridView();
        _pnlStatus = new Panel();

        SuspendLayout();
        _pnlFilter.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        _pnlStatus.SuspendLayout();

        // ── Filter panel ──────────────────────────────────────────────────────
        _pnlFilter.Dock        = DockStyle.Top;
        _pnlFilter.Height      = 50;
        _pnlFilter.BackColor   = UiColors.HeaderBar;
        _pnlFilter.Padding     = new Padding(4, 0, 4, 0);

        _lblFrom.AutoSize  = false;
        _lblFrom.Width     = 20;
        _lblFrom.Height    = 20;
        _lblFrom.Location  = new Point(10, 15);
        _lblFrom.Text      = "С:";
        _lblFrom.ForeColor = UiColors.TextPrimary;
        _lblFrom.Font      = UiFonts.Body;
        _lblFrom.TextAlign = ContentAlignment.MiddleLeft;

        _dtpFrom.Location     = new Point(32, 12);
        _dtpFrom.Width        = 165;
        _dtpFrom.Format       = DateTimePickerFormat.Custom;
        _dtpFrom.CustomFormat = "dd.MM.yyyy HH:mm";
        _dtpFrom.ShowUpDown   = false;

        _lblTo.AutoSize  = false;
        _lblTo.Width     = 12;
        _lblTo.Height    = 20;
        _lblTo.Location  = new Point(203, 15);
        _lblTo.Text      = "—";
        _lblTo.ForeColor = UiColors.TextPrimary;
        _lblTo.Font      = UiFonts.Body;
        _lblTo.TextAlign = ContentAlignment.MiddleCenter;

        _dtpTo.Location     = new Point(218, 12);
        _dtpTo.Width        = 165;
        _dtpTo.Format       = DateTimePickerFormat.Custom;
        _dtpTo.CustomFormat = "dd.MM.yyyy HH:mm";
        _dtpTo.ShowUpDown   = false;

        _btnFind.Location  = new Point(394, 11);
        _btnFind.Size      = new Size(90, 26);
        _btnFind.Text      = "Найти";
        _btnFind.FlatStyle = FlatStyle.Flat;
        _btnFind.BackColor = UiColors.InfoAction;
        _btnFind.ForeColor = UiColors.TextOnDark;
        _btnFind.UseVisualStyleBackColor = false;
        _btnFind.Font      = UiFonts.Body;
        _btnFind.Click    += BtnFind_Click;

        _pnlFilter.Controls.Add(_lblFrom);
        _pnlFilter.Controls.Add(_dtpFrom);
        _pnlFilter.Controls.Add(_lblTo);
        _pnlFilter.Controls.Add(_dtpTo);
        _pnlFilter.Controls.Add(_btnFind);

        // ── Grid ──────────────────────────────────────────────────────────────
        _grid.Dock                    = DockStyle.Fill;
        _grid.ReadOnly                = true;
        _grid.AllowUserToAddRows      = false;
        _grid.AllowUserToDeleteRows   = false;
        _grid.AllowUserToResizeRows   = false;
        _grid.RowHeadersVisible       = false;
        _grid.MultiSelect             = false;
        _grid.SelectionMode           = DataGridViewSelectionMode.FullRowSelect;
        _grid.AutoSizeColumnsMode     = DataGridViewAutoSizeColumnsMode.None;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.ColumnHeadersHeight     = 32;
        _grid.RowTemplate.Height      = 28;
        _grid.BackgroundColor         = UiColors.AppBackground;
        _grid.GridColor               = UiColors.GridLine;
        _grid.BorderStyle             = BorderStyle.None;
        _grid.ColumnHeadersDefaultCellStyle.BackColor          = UiColors.GridHeaderBack;
        _grid.ColumnHeadersDefaultCellStyle.ForeColor          = UiColors.GridHeaderText;
        _grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = UiColors.GridHeaderBack;
        _grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = UiColors.GridHeaderText;
        _grid.ColumnHeadersDefaultCellStyle.Font               = UiFonts.GridHeader;
        _grid.ColumnHeadersDefaultCellStyle.Alignment          = DataGridViewContentAlignment.MiddleLeft;
        _grid.DefaultCellStyle.BackColor                       = UiColors.AppBackground;
        _grid.DefaultCellStyle.Font                            = UiFonts.GridBody;
        _grid.DefaultCellStyle.ForeColor                       = UiColors.TextPrimary;
        _grid.DefaultCellStyle.SelectionBackColor              = UiColors.GridSelectionBack;
        _grid.DefaultCellStyle.SelectionForeColor              = UiColors.GridSelectionText;
        _grid.AlternatingRowsDefaultCellStyle.BackColor        = UiColors.GridAlternateRow;
        _grid.EnableHeadersVisualStyles                        = false;

        // ── Status panel ──────────────────────────────────────────────────────
        _pnlStatus.Dock      = DockStyle.Bottom;
        _pnlStatus.Height    = 4;
        _pnlStatus.BackColor = UiColors.StatusBar;



        // ── Form ──────────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7f, 15f);
        AutoScaleMode       = AutoScaleMode.Font;
        BackColor           = UiColors.AppBackground;
        ClientSize          = new Size(1264, 670);
        Text                = "Просмотр логов";
        StartPosition       = FormStartPosition.CenterScreen;

        Controls.Add(_grid);
        Controls.Add(_pnlFilter);
        Controls.Add(_pnlStatus);

        _pnlStatus.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        _pnlFilter.ResumeLayout(false);
        ResumeLayout(false);
    }
}
