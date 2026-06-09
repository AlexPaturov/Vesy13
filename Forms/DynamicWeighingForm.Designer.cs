namespace Vesy13.Forms;

partial class DynamicWeighingForm
{
    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        _gbDir = new GroupBox();
        _rbPlus = new RadioButton();
        _rbMinus = new RadioButton();
        _lblChannel = new Label();
        _pnlDisplay = new Panel();
        _lblValue = new Label();
        _lblUnit = new Label();
        _lblStatus = new Label();
        _btnWeigh = new Button();
        _btnZero = new Button();
        _btnFinish = new Button();
        _grid = new DataGridView();
        _pnlStatusBar = new Panel();
        _dotConn = new Panel();
        _lblConn = new Label();
        _gbDir.SuspendLayout();
        _pnlDisplay.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        _pnlStatusBar.SuspendLayout();
        SuspendLayout();
        // 
        // _gbDir
        // 
        _gbDir.Controls.Add(_rbPlus);
        _gbDir.Controls.Add(_rbMinus);
        _gbDir.Font = UiFonts.Body;
        _gbDir.ForeColor = Color.FromArgb(232, 237, 245);
        _gbDir.Location = new Point(8, 8);
        _gbDir.Name = "_gbDir";
        _gbDir.Size = new Size(320, 50);
        _gbDir.TabIndex = 0;
        _gbDir.TabStop = false;
        _gbDir.Text = "Направление движения состава";
        // 
        // _rbPlus
        // 
        _rbPlus.AutoSize = true;
        _rbPlus.Checked = true;
        _rbPlus.Font = UiFonts.Medium;
        _rbPlus.ForeColor = Color.FromArgb(232, 237, 245);
        _rbPlus.Location = new Point(12, 20);
        _rbPlus.Name = "_rbPlus";
        _rbPlus.Size = new Size(61, 23);
        _rbPlus.TabIndex = 0;
        _rbPlus.TabStop = true;
        _rbPlus.Text = "→ (+)";
        // 
        // _rbMinus
        // 
        _rbMinus.AutoSize = true;
        _rbMinus.Font = UiFonts.Medium;
        _rbMinus.ForeColor = Color.FromArgb(232, 237, 245);
        _rbMinus.Location = new Point(130, 20);
        _rbMinus.Name = "_rbMinus";
        _rbMinus.Size = new Size(58, 23);
        _rbMinus.TabIndex = 1;
        _rbMinus.Text = "← (–)";
        // 
        // _lblChannel
        // 
        _lblChannel.AutoSize = true;
        _lblChannel.Font = UiFonts.Medium;
        _lblChannel.ForeColor = Color.FromArgb(122, 138, 158);
        _lblChannel.Location = new Point(336, 22);
        _lblChannel.Name = "_lblChannel";
        _lblChannel.Size = new Size(0, 19);
        _lblChannel.TabIndex = 1;
        // 
        // _pnlDisplay
        // 
        _pnlDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _pnlDisplay.BackColor = Color.FromArgb(5, 8, 12);
        _pnlDisplay.Controls.Add(_lblValue);
        _pnlDisplay.Controls.Add(_lblUnit);
        _pnlDisplay.Controls.Add(_lblStatus);
        _pnlDisplay.Location = new Point(8, 66);
        _pnlDisplay.Name = "_pnlDisplay";
        _pnlDisplay.Size = new Size(655, 158);
        _pnlDisplay.TabIndex = 2;
        // 
        // _lblValue
        // 
        _lblValue.Font = UiFonts.Display;
        _lblValue.ForeColor = Color.FromArgb(90, 96, 112);
        _lblValue.Location = new Point(8, 4);
        _lblValue.Name = "_lblValue";
        _lblValue.Size = new Size(450, 106);
        _lblValue.TabIndex = 0;
        _lblValue.Text = "—";
        _lblValue.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblUnit
        // 
        _lblUnit.Font = UiFonts.UnitLabel;
        _lblUnit.ForeColor = Color.FromArgb(90, 96, 112);
        _lblUnit.Location = new Point(462, 60);
        _lblUnit.Name = "_lblUnit";
        _lblUnit.Size = new Size(60, 62);
        _lblUnit.TabIndex = 1;
        _lblUnit.Text = "т";
        _lblUnit.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _lblStatus
        // 
        _lblStatus.Font = UiFonts.Medium;
        _lblStatus.ForeColor = Color.FromArgb(138, 154, 181);
        _lblStatus.Location = new Point(8, 118);
        _lblStatus.Name = "_lblStatus";
        _lblStatus.Size = new Size(584, 34);
        _lblStatus.TabIndex = 2;
        _lblStatus.Text = "Готов к взвешиванию  —  Тележка 1";
        _lblStatus.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _btnWeigh
        // 
        _btnWeigh.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _btnWeigh.BackColor = Color.FromArgb(46, 155, 94);
        _btnWeigh.FlatStyle = FlatStyle.Flat;
        _btnWeigh.Font = UiFonts.WeighButton;
        _btnWeigh.ForeColor = Color.FromArgb(232, 237, 245);
        _btnWeigh.Location = new Point(8, 230);
        _btnWeigh.Name = "_btnWeigh";
        _btnWeigh.Size = new Size(655, 54);
        _btnWeigh.TabIndex = 3;
        _btnWeigh.Text = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1";
        _btnWeigh.UseVisualStyleBackColor = false;
        _btnWeigh.Click += BtnWeigh_Click;
        // 
        // _btnZero
        // 
        _btnZero.BackColor = Color.FromArgb(58, 74, 94);
        _btnZero.FlatStyle = FlatStyle.Flat;
        _btnZero.Font = UiFonts.Medium;
        _btnZero.ForeColor = Color.FromArgb(232, 237, 245);
        _btnZero.Location = new Point(8, 288);
        _btnZero.Name = "_btnZero";
        _btnZero.Size = new Size(100, 32);
        _btnZero.TabIndex = 4;
        _btnZero.Text = "Ноль";
        _btnZero.UseVisualStyleBackColor = false;
        _btnZero.Click += BtnZero_Click;
        // 
        // _btnFinish
        // 
        _btnFinish.BackColor = Color.FromArgb(179, 58, 26);
        _btnFinish.FlatStyle = FlatStyle.Flat;
        _btnFinish.Font = UiFonts.Medium;
        _btnFinish.ForeColor = Color.FromArgb(232, 237, 245);
        _btnFinish.Location = new Point(116, 288);
        _btnFinish.Name = "_btnFinish";
        _btnFinish.Size = new Size(244, 32);
        _btnFinish.TabIndex = 5;
        _btnFinish.Text = "Завершить состав";
        _btnFinish.UseVisualStyleBackColor = false;
        _btnFinish.Click += BtnFinish_Click;
        // 
        // _grid
        // 
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(42, 48, 64);
        dataGridViewCellStyle1.ForeColor = Color.FromArgb(232, 237, 245);
        dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(46, 82, 128);
        dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(232, 237, 245);
        _grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        _grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _grid.BackgroundColor = Color.FromArgb(37, 43, 53);
        _grid.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(26, 34, 53);
        dataGridViewCellStyle2.Font = UiFonts.GridHeader;
        dataGridViewCellStyle2.ForeColor = Color.FromArgb(232, 237, 245);
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(26, 34, 53);
        dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(232, 237, 245);
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        _grid.ColumnHeadersHeight = 40;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.EnableHeadersVisualStyles = false;
        _grid.Font = UiFonts.GridBody;
        _grid.GridColor = Color.FromArgb(58, 68, 88);
        _grid.Location = new Point(8, 326);
        _grid.Name = "_grid";
        _grid.ReadOnly = true;
        _grid.RowHeadersVisible = false;
        dataGridViewCellStyle3.BackColor = Color.FromArgb(37, 43, 53);
        dataGridViewCellStyle3.ForeColor = Color.FromArgb(232, 237, 245);
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(46, 82, 128);
        dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(232, 237, 245);
        _grid.RowsDefaultCellStyle = dataGridViewCellStyle3;
        _grid.RowTemplate.Height = 28;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.Size = new Size(655, 261);
        _grid.TabIndex = 6;
        // 
        // _pnlStatusBar
        // 
        _pnlStatusBar.BackColor = Color.FromArgb(26, 31, 40);
        _pnlStatusBar.Controls.Add(_dotConn);
        _pnlStatusBar.Controls.Add(_lblConn);
        _pnlStatusBar.Dock = DockStyle.Bottom;
        _pnlStatusBar.Location = new Point(0, 595);
        _pnlStatusBar.Name = "_pnlStatusBar";
        _pnlStatusBar.Size = new Size(673, 28);
        _pnlStatusBar.TabIndex = 7;
        // 
        // 
        // 
        // _dotConn
        // 
        _dotConn.BackColor = Color.FromArgb(90, 96, 112);
        _dotConn.Location = new Point(100, 12);
        _dotConn.Name = "_dotConn";
        _dotConn.Size = new Size(10, 10);
        _dotConn.TabIndex = 1;
        // 
        // _lblConn
        // 
        _lblConn.AutoSize = true;
        _lblConn.Font = UiFonts.Body;
        _lblConn.ForeColor = Color.FromArgb(138, 154, 181);
        _lblConn.Location = new Point(116, 8);
        _lblConn.Name = "_lblConn";
        _lblConn.Size = new Size(66, 21);
        _lblConn.TabIndex = 2;
        _lblConn.Text = "АЦП: —";
        // 
        // DynamicWeighingForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(27, 31, 36);
        ClientSize = new Size(673, 629);
        Controls.Add(_gbDir);
        Controls.Add(_lblChannel);
        Controls.Add(_pnlDisplay);
        Controls.Add(_btnWeigh);
        Controls.Add(_btnZero);
        Controls.Add(_btnFinish);
        Controls.Add(_grid);
        Controls.Add(_pnlStatusBar);
        KeyPreview = true;
        MaximumSize = new Size(689, 10000);
        Name = "DynamicWeighingForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Взвешивание — Динамика";
        _gbDir.ResumeLayout(false);
        _gbDir.PerformLayout();
        _pnlDisplay.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        _pnlStatusBar.ResumeLayout(false);
        _pnlStatusBar.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private GroupBox      _gbDir;
    private RadioButton   _rbPlus;
    private RadioButton   _rbMinus;
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
    private Panel         _dotConn;
    private Label         _lblConn;
}
