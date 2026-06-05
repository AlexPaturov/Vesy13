namespace Vesy13.Forms;

partial class StaticWeighingForm
{
    private void InitializeComponent()
    {
        _lblChannel   = new Label();
        _pnlDisplay   = new Panel();
        _lblValue     = new Label();
        _lblUnit      = new Label();
        _lblStatus    = new Label();
        _btnWeigh     = new Button();
        _btnZero      = new Button();
        _btnFinish    = new Button();
        _grid         = new DataGridView();
        _pnlStatusBar = new Panel();
        _btnBack      = new Button();
        _dotConn      = new Panel();
        _lblConn      = new Label();

        _pnlDisplay.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        _pnlStatusBar.SuspendLayout();
        SuspendLayout();

        // ── _lblChannel (top area 0–34) ───────────────────────────────────────
        _lblChannel.Location  = new Point(8, 10);
        _lblChannel.AutoSize  = true;
        _lblChannel.Font      = new Font("Segoe UI", 10F);
        _lblChannel.ForeColor = Color.FromArgb(80, 80, 80);

        // ── Display panel (topY = 34) ─────────────────────────────────────────
        _lblValue.Text      = "—";
        _lblValue.Font      = new Font("Courier New", 60F, FontStyle.Bold);
        _lblValue.ForeColor = Color.DimGray;
        _lblValue.TextAlign = ContentAlignment.MiddleRight;
        _lblValue.Bounds    = new Rectangle(8, 4, 450, 106);
        _lblValue.AutoSize  = false;

        _lblUnit.Text      = "т";
        _lblUnit.Font      = new Font("Segoe UI", 20F);
        _lblUnit.ForeColor = Color.Gray;
        _lblUnit.TextAlign = ContentAlignment.BottomLeft;
        _lblUnit.Bounds    = new Rectangle(462, 60, 60, 62);
        _lblUnit.AutoSize  = false;

        _lblStatus.Text      = "Готов к взвешиванию  —  Тележка 1";
        _lblStatus.Font      = new Font("Segoe UI", 10F);
        _lblStatus.ForeColor = Color.Silver;
        _lblStatus.TextAlign = ContentAlignment.MiddleCenter;
        _lblStatus.Bounds    = new Rectangle(8, 118, 528, 34);
        _lblStatus.AutoSize  = false;

        _pnlDisplay.Location  = new Point(8, 34);
        _pnlDisplay.Size      = new Size(544, 158);
        _pnlDisplay.BackColor = Color.Black;
        _pnlDisplay.Controls.Add(_lblValue);
        _pnlDisplay.Controls.Add(_lblUnit);
        _pnlDisplay.Controls.Add(_lblStatus);

        // ── _btnWeigh (y = 34+158+6 = 198) ───────────────────────────────────
        _btnWeigh.Text      = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1";
        _btnWeigh.Location  = new Point(8, 198);
        _btnWeigh.Size      = new Size(544, 54);
        _btnWeigh.Font      = new Font("Segoe UI", 14F, FontStyle.Bold);
        _btnWeigh.FlatStyle = FlatStyle.Flat;
        _btnWeigh.BackColor = Color.FromArgb(0, 130, 0);
        _btnWeigh.ForeColor = Color.White;
        _btnWeigh.Click    += BtnWeigh_Click;

        // ── y = 198+54+4 = 256 ────────────────────────────────────────────────
        _btnZero.Text      = "Ноль";
        _btnZero.Location  = new Point(8, 256);
        _btnZero.Size      = new Size(100, 32);
        _btnZero.Font      = new Font("Segoe UI", 10F);
        _btnZero.FlatStyle = FlatStyle.Flat;
        _btnZero.Click    += BtnZero_Click;

        _btnFinish.Text      = "Завершить состав";
        _btnFinish.Location  = new Point(116, 256);
        _btnFinish.Size      = new Size(244, 32);
        _btnFinish.Font      = new Font("Segoe UI", 10F);
        _btnFinish.FlatStyle = FlatStyle.Flat;
        _btnFinish.BackColor = Color.FromArgb(120, 40, 0);
        _btnFinish.ForeColor = Color.White;
        _btnFinish.Click    += BtnFinish_Click;

        // ── _grid (y = 256+32+6 = 294) ────────────────────────────────────────
        _grid.Location                              = new Point(8, 294);
        _grid.Size                                  = new Size(544, 248);
        _grid.ReadOnly                              = true;
        _grid.AllowUserToAddRows                    = false;
        _grid.AllowUserToDeleteRows                 = false;
        _grid.AllowUserToResizeRows                 = false;
        _grid.RowHeadersVisible                     = false;
        _grid.SelectionMode                         = DataGridViewSelectionMode.FullRowSelect;
        _grid.BackgroundColor                       = Color.White;
        _grid.BorderStyle                           = BorderStyle.None;
        _grid.Font                                  = new Font("Segoe UI", 9F);
        _grid.ColumnHeadersHeightSizeMode           = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.ColumnHeadersHeight                   = 28;
        _grid.RowTemplate.Height                    = 22;
        _grid.RowsDefaultCellStyle.BackColor            = Color.White;
        _grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 245, 255);

        // ── Status bar ────────────────────────────────────────────────────────
        _btnBack.Text      = "← Назад";
        _btnBack.Location  = new Point(8, 6);
        _btnBack.Size      = new Size(80, 22);
        _btnBack.FlatStyle = FlatStyle.Flat;
        _btnBack.Font      = new Font("Segoe UI", 8F);
        _btnBack.BackColor = Color.FromArgb(40, 70, 130);
        _btnBack.ForeColor = Color.White;
        _btnBack.FlatAppearance.BorderSize = 0;
        _btnBack.Click    += BtnBack_Click;

        _dotConn.Size      = new Size(10, 10);
        _dotConn.Location  = new Point(100, 12);
        _dotConn.BackColor = Color.Gray;

        _lblConn.Text      = "АЦП: —";
        _lblConn.Font      = new Font("Segoe UI", 9F);
        _lblConn.ForeColor = Color.Silver;
        _lblConn.Location  = new Point(116, 8);
        _lblConn.AutoSize  = true;

        _pnlStatusBar.Dock      = DockStyle.Bottom;
        _pnlStatusBar.Height    = 34;
        _pnlStatusBar.BackColor = Color.FromArgb(18, 32, 65);
        _pnlStatusBar.Controls.Add(_btnBack);
        _pnlStatusBar.Controls.Add(_dotConn);
        _pnlStatusBar.Controls.Add(_lblConn);

        // ── Form ──────────────────────────────────────────────────────────────
        Text          = "Взвешивание — Статика";
        ClientSize    = new Size(560, 584);
        StartPosition = FormStartPosition.CenterScreen;
        KeyPreview    = true;
        BackColor     = Color.FromArgb(240, 242, 245);
        Controls.Add(_lblChannel);
        Controls.Add(_pnlDisplay);
        Controls.Add(_btnWeigh);
        Controls.Add(_btnZero);
        Controls.Add(_btnFinish);
        Controls.Add(_grid);
        Controls.Add(_pnlStatusBar);

        _pnlDisplay.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        _pnlStatusBar.ResumeLayout(false);
        _pnlStatusBar.PerformLayout();
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ResumeLayout(false);
    }

    private Label         _lblChannel;
    private Panel         _pnlDisplay;
    private Label         _lblValue;
    private Label         _lblUnit;
    private Label         _lblStatus;
    private Button        _btnWeigh;
    private Button        _btnZero;
    private Button        _btnFinish;
    private DataGridView  _grid;
    private Panel         _pnlStatusBar;
    private Button        _btnBack;
    private Panel         _dotConn;
    private Label         _lblConn;
}
