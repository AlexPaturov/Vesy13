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
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        _pnlFilter = new Panel();
        _lblFrom = new Label();
        _dtpFrom = new DateTimePicker();
        _lblTo = new Label();
        _dtpTo = new DateTimePicker();
        _btnFind = new Button();
        _grid = new DataGridView();
        _pnlStatus = new Panel();
        tlpHead = new TableLayoutPanel();
        _pnlFilter.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        tlpHead.SuspendLayout();
        SuspendLayout();
        // 
        // _pnlFilter
        // 
        _pnlFilter.BackColor = Color.FromArgb(247, 249, 252);
        _pnlFilter.BorderStyle = BorderStyle.FixedSingle;
        _pnlFilter.Controls.Add(tlpHead);
        _pnlFilter.Dock = DockStyle.Top;
        _pnlFilter.Location = new Point(0, 0);
        _pnlFilter.Margin = new Padding(4, 5, 4, 5);
        _pnlFilter.Name = "_pnlFilter";
        _pnlFilter.Padding = new Padding(6, 0, 6, 0);
        _pnlFilter.Size = new Size(1806, 184);
        _pnlFilter.TabIndex = 1;
        // 
        // _lblFrom
        // 
        _lblFrom.Dock = DockStyle.Fill;
        _lblFrom.Font = new Font("Segoe UI", 12F);
        _lblFrom.Location = new Point(4, 0);
        _lblFrom.Margin = new Padding(4, 0, 4, 0);
        _lblFrom.Name = "_lblFrom";
        _lblFrom.Size = new Size(219, 150);
        _lblFrom.TabIndex = 0;
        _lblFrom.Text = "С:";
        _lblFrom.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _dtpFrom
        // 
        _dtpFrom.Anchor = AnchorStyles.None;
        _dtpFrom.CustomFormat = "dd.MM.yyyy HH:mm";
        _dtpFrom.Format = DateTimePickerFormat.Custom;
        _dtpFrom.Location = new Point(231, 59);
        _dtpFrom.Margin = new Padding(4, 5, 4, 5);
        _dtpFrom.Name = "_dtpFrom";
        _dtpFrom.Size = new Size(219, 31);
        _dtpFrom.TabIndex = 1;
        // 
        // _lblTo
        // 
        _lblTo.Dock = DockStyle.Fill;
        _lblTo.Font = new Font("Segoe UI", 12F);
        _lblTo.Location = new Point(458, 0);
        _lblTo.Margin = new Padding(4, 0, 4, 0);
        _lblTo.Name = "_lblTo";
        _lblTo.Size = new Size(81, 150);
        _lblTo.TabIndex = 2;
        _lblTo.Text = "—";
        _lblTo.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _dtpTo
        // 
        _dtpTo.Anchor = AnchorStyles.None;
        _dtpTo.CustomFormat = "dd.MM.yyyy HH:mm";
        _dtpTo.Format = DateTimePickerFormat.Custom;
        _dtpTo.Location = new Point(607, 59);
        _dtpTo.Margin = new Padding(4, 5, 4, 5);
        _dtpTo.Name = "_dtpTo";
        _dtpTo.Size = new Size(234, 31);
        _dtpTo.TabIndex = 3;
        // 
        // _btnFind
        // 
        _btnFind.Dock = DockStyle.Fill;
        _btnFind.FlatStyle = FlatStyle.Flat;
        _btnFind.Font = new Font("Segoe UI", 12F);
        _btnFind.Location = new Point(910, 5);
        _btnFind.Margin = new Padding(4, 5, 4, 5);
        _btnFind.Name = "_btnFind";
        _btnFind.Size = new Size(220, 140);
        _btnFind.TabIndex = 4;
        _btnFind.Text = "Найти";
        _btnFind.Click += BtnFind_Click;
        // 
        // _grid
        // 
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(240, 244, 248);
        _grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        _grid.BackgroundColor = Color.FromArgb(238, 241, 244);
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(221, 230, 240);
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        dataGridViewCellStyle2.ForeColor = Color.FromArgb(35, 49, 63);
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        _grid.ColumnHeadersHeight = 32;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = Color.FromArgb(238, 241, 244);
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F);
        dataGridViewCellStyle3.ForeColor = Color.FromArgb(46, 58, 70);
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(220, 232, 247);
        dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(35, 49, 63);
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        _grid.DefaultCellStyle = dataGridViewCellStyle3;
        _grid.EnableHeadersVisualStyles = false;
        _grid.GridColor = Color.FromArgb(200, 208, 218);
        _grid.Location = new Point(53, 293);
        _grid.Margin = new Padding(4, 5, 4, 5);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.ReadOnly = true;
        _grid.RowHeadersVisible = false;
        _grid.RowHeadersWidth = 62;
        _grid.RowTemplate.Height = 28;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.Size = new Size(343, 250);
        _grid.TabIndex = 0;
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
        // tlpHead
        // 
        tlpHead.ColumnCount = 5;
        tlpHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tlpHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tlpHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.8483243F));
        tlpHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32.010582F));
        tlpHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tlpHead.Controls.Add(_lblFrom, 0, 0);
        tlpHead.Controls.Add(_btnFind, 4, 0);
        tlpHead.Controls.Add(_dtpTo, 3, 0);
        tlpHead.Controls.Add(_lblTo, 2, 0);
        tlpHead.Controls.Add(_dtpFrom, 1, 0);
        tlpHead.Location = new Point(-1, 3);
        tlpHead.Name = "tlpHead";
        tlpHead.RowCount = 1;
        tlpHead.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlpHead.Size = new Size(1134, 150);
        tlpHead.TabIndex = 5;
        // 
        // LogsForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(238, 241, 244);
        ClientSize = new Size(1806, 1050);
        Controls.Add(_grid);
        Controls.Add(_pnlFilter);
        Controls.Add(_pnlStatus);
        Margin = new Padding(4, 5, 4, 5);
        Name = "LogsForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Просмотр логов";
        _pnlFilter.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        tlpHead.ResumeLayout(false);
        ResumeLayout(false);
    }

    private TableLayoutPanel tlpHead;
}
