namespace Vesy13.Forms;

partial class StaticWeighingForm
{
    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        _lblChannel = new Label();
        _layoutMain = new TableLayoutPanel();
        _pnlDisplay = new Panel();
        _lblValue = new Label();
        _lblBogie1Caption = new Label();
        _lblBogie1Value = new Label();
        _lblBogie2Caption = new Label();
        _lblBogie2Value = new Label();
        _pnlActions = new Panel();
        _btnWeigh = new Button();
        _btnZero = new Button();
        _btnFinish = new Button();
        _grid = new DataGridView();
        _pnlStatusBar = new Panel();
        _statusLayout = new TableLayoutPanel();
        _dotConn = new Panel();
        _lblConn = new Label();
        _lblStorage = new Label();
        _layoutMain.SuspendLayout();
        _pnlDisplay.SuspendLayout();
        _pnlActions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        _pnlStatusBar.SuspendLayout();
        _statusLayout.SuspendLayout();
        SuspendLayout();
        // 
        // _lblChannel
        // 
        _lblChannel.AutoSize = true;
        _lblChannel.Dock = DockStyle.Fill;
        _lblChannel.Font = new Font("Segoe UI", 10F);
        _lblChannel.ForeColor = Color.FromArgb(102, 112, 124);
        _lblChannel.Location = new Point(9, 11);
        _lblChannel.Margin = new Padding(0, 0, 0, 8);
        _lblChannel.Name = "_lblChannel";
        _lblChannel.Size = new Size(900, 23);
        _lblChannel.TabIndex = 0;
        // 
        // _layoutMain
        // 
        _layoutMain.ColumnCount = 1;
        _layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _layoutMain.Controls.Add(_lblChannel, 0, 0);
        _layoutMain.Controls.Add(_pnlDisplay, 0, 1);
        _layoutMain.Controls.Add(_pnlActions, 0, 2);
        _layoutMain.Controls.Add(_grid, 0, 3);
        _layoutMain.Dock = DockStyle.Fill;
        _layoutMain.Location = new Point(0, 0);
        _layoutMain.Margin = new Padding(3, 4, 3, 4);
        _layoutMain.Name = "_layoutMain";
        _layoutMain.Padding = new Padding(9, 11, 9, 11);
        _layoutMain.RowCount = 4;
        _layoutMain.RowStyles.Add(new RowStyle());
        _layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 211F));
        _layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 131F));
        _layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _layoutMain.Size = new Size(918, 822);
        _layoutMain.TabIndex = 0;
        // 
        // _pnlDisplay
        // 
        _pnlDisplay.BackColor = Color.FromArgb(5, 8, 12);
        _pnlDisplay.Controls.Add(_lblValue);
        _pnlDisplay.Controls.Add(_lblBogie1Caption);
        _pnlDisplay.Controls.Add(_lblBogie1Value);
        _pnlDisplay.Controls.Add(_lblBogie2Caption);
        _pnlDisplay.Controls.Add(_lblBogie2Value);
        _pnlDisplay.Dock = DockStyle.Fill;
        _pnlDisplay.Location = new Point(9, 42);
        _pnlDisplay.Margin = new Padding(0, 0, 0, 11);
        _pnlDisplay.Name = "_pnlDisplay";
        _pnlDisplay.Size = new Size(900, 200);
        _pnlDisplay.TabIndex = 1;
        // 
        // _lblValue
        // 
        _lblValue.Font = new Font("Courier New", 60F, FontStyle.Bold);
        _lblValue.ForeColor = Color.FromArgb(215, 224, 234);
        _lblValue.Location = new Point(0, 11);
        _lblValue.Name = "_lblValue";
        _lblValue.Size = new Size(455, 176);
        _lblValue.TabIndex = 0;
        _lblValue.Text = "—";
        _lblValue.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblBogie1Caption
        // 
        _lblBogie1Caption.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblBogie1Caption.Font = new Font("Segoe UI", 12F);
        _lblBogie1Caption.ForeColor = Color.FromArgb(215, 224, 234);
        _lblBogie1Caption.Location = new Point(677, 24);
        _lblBogie1Caption.Name = "_lblBogie1Caption";
        _lblBogie1Caption.Size = new Size(199, 27);
        _lblBogie1Caption.TabIndex = 2;
        _lblBogie1Caption.Text = "Тележка 1";
        _lblBogie1Caption.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblBogie1Value
        // 
        _lblBogie1Value.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblBogie1Value.Font = new Font("Segoe UI", 10F);
        _lblBogie1Value.ForeColor = Color.FromArgb(215, 224, 234);
        _lblBogie1Value.Location = new Point(677, 52);
        _lblBogie1Value.Name = "_lblBogie1Value";
        _lblBogie1Value.Size = new Size(199, 48);
        _lblBogie1Value.TabIndex = 3;
        _lblBogie1Value.Text = "—";
        _lblBogie1Value.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblBogie2Caption
        // 
        _lblBogie2Caption.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblBogie2Caption.Font = new Font("Segoe UI", 12F);
        _lblBogie2Caption.ForeColor = Color.FromArgb(215, 224, 234);
        _lblBogie2Caption.Location = new Point(677, 112);
        _lblBogie2Caption.Name = "_lblBogie2Caption";
        _lblBogie2Caption.Size = new Size(199, 27);
        _lblBogie2Caption.TabIndex = 4;
        _lblBogie2Caption.Text = "Тележка 2";
        _lblBogie2Caption.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblBogie2Value
        // 
        _lblBogie2Value.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblBogie2Value.Font = new Font("Segoe UI", 10F);
        _lblBogie2Value.ForeColor = Color.FromArgb(215, 224, 234);
        _lblBogie2Value.Location = new Point(677, 140);
        _lblBogie2Value.Name = "_lblBogie2Value";
        _lblBogie2Value.Size = new Size(199, 48);
        _lblBogie2Value.TabIndex = 5;
        _lblBogie2Value.Text = "—";
        _lblBogie2Value.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _pnlActions
        // 
        _pnlActions.Controls.Add(_btnWeigh);
        _pnlActions.Controls.Add(_btnZero);
        _pnlActions.Controls.Add(_btnFinish);
        _pnlActions.Dock = DockStyle.Fill;
        _pnlActions.Location = new Point(9, 253);
        _pnlActions.Margin = new Padding(0, 0, 0, 5);
        _pnlActions.Name = "_pnlActions";
        _pnlActions.Size = new Size(900, 126);
        _pnlActions.TabIndex = 2;
        // 
        // _btnWeigh
        // 
        _btnWeigh.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _btnWeigh.BackColor = Color.FromArgb(47, 111, 237);
        _btnWeigh.FlatStyle = FlatStyle.Flat;
        _btnWeigh.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        _btnWeigh.ForeColor = Color.FromArgb(255, 255, 255);
        _btnWeigh.Location = new Point(0, 0);
        _btnWeigh.Margin = new Padding(3, 4, 3, 4);
        _btnWeigh.Name = "_btnWeigh";
        _btnWeigh.Size = new Size(900, 72);
        _btnWeigh.TabIndex = 2;
        _btnWeigh.Text = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1";
        _btnWeigh.UseVisualStyleBackColor = false;
        _btnWeigh.Click += BtnWeigh_Click;
        // 
        // _btnZero
        // 
        _btnZero.BackColor = Color.FromArgb(217, 226, 236);
        _btnZero.FlatStyle = FlatStyle.Flat;
        _btnZero.Font = new Font("Segoe UI", 10F);
        _btnZero.ForeColor = Color.FromArgb(46, 58, 70);
        _btnZero.Location = new Point(6, 77);
        _btnZero.Margin = new Padding(3, 4, 3, 4);
        _btnZero.Name = "_btnZero";
        _btnZero.Size = new Size(114, 43);
        _btnZero.TabIndex = 3;
        _btnZero.Text = "Ноль";
        _btnZero.UseVisualStyleBackColor = false;
        _btnZero.Click += BtnZero_Click;
        // 
        // _btnFinish
        // 
        _btnFinish.BackColor = Color.FromArgb(179, 58, 26);
        _btnFinish.FlatStyle = FlatStyle.Flat;
        _btnFinish.Font = new Font("Segoe UI", 10F);
        _btnFinish.ForeColor = Color.FromArgb(255, 255, 255);
        _btnFinish.Location = new Point(145, 77);
        _btnFinish.Margin = new Padding(3, 4, 3, 4);
        _btnFinish.Name = "_btnFinish";
        _btnFinish.Size = new Size(279, 43);
        _btnFinish.TabIndex = 4;
        _btnFinish.Text = "Завершить состав";
        _btnFinish.UseVisualStyleBackColor = false;
        _btnFinish.Click += BtnFinish_Click;
        // 
        // _grid
        // 
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(240, 244, 248);
        dataGridViewCellStyle1.ForeColor = Color.FromArgb(46, 58, 70);
        dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(220, 232, 247);
        dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(35, 49, 63);
        _grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        _grid.BackgroundColor = Color.FromArgb(247, 249, 252);
        _grid.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(221, 230, 240);
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        dataGridViewCellStyle2.ForeColor = Color.FromArgb(35, 49, 63);
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(221, 230, 240);
        dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(35, 49, 63);
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        _grid.ColumnHeadersHeight = 40;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.Dock = DockStyle.Fill;
        _grid.EnableHeadersVisualStyles = false;
        _grid.Font = new Font("Segoe UI", 12F);
        _grid.GridColor = Color.FromArgb(200, 208, 218);
        _grid.Location = new Point(11, 387);
        _grid.Margin = new Padding(2, 3, 2, 3);
        _grid.Name = "_grid";
        _grid.ReadOnly = true;
        _grid.RowHeadersVisible = false;
        _grid.RowHeadersWidth = 51;
        dataGridViewCellStyle3.BackColor = Color.FromArgb(240, 244, 248);
        dataGridViewCellStyle3.ForeColor = Color.FromArgb(46, 58, 70);
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(220, 232, 247);
        dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(35, 49, 63);
        _grid.RowsDefaultCellStyle = dataGridViewCellStyle3;
        _grid.RowTemplate.Height = 28;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.Size = new Size(896, 421);
        _grid.TabIndex = 5;
        // 
        // _pnlStatusBar
        // 
        _pnlStatusBar.BackColor = Color.FromArgb(217, 226, 236);
        _pnlStatusBar.Controls.Add(_statusLayout);
        _pnlStatusBar.Dock = DockStyle.Bottom;
        _pnlStatusBar.Location = new Point(0, 822);
        _pnlStatusBar.Margin = new Padding(3, 4, 3, 4);
        _pnlStatusBar.Name = "_pnlStatusBar";
        _pnlStatusBar.Size = new Size(918, 37);
        _pnlStatusBar.TabIndex = 6;
        // 
        // _statusLayout
        // 
        _statusLayout.ColumnCount = 3;
        _statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 21F));
        _statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 171F));
        _statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _statusLayout.Controls.Add(_dotConn, 0, 0);
        _statusLayout.Controls.Add(_lblConn, 1, 0);
        _statusLayout.Controls.Add(_lblStorage, 2, 0);
        _statusLayout.Dock = DockStyle.Fill;
        _statusLayout.Location = new Point(0, 0);
        _statusLayout.Margin = new Padding(3, 4, 3, 4);
        _statusLayout.Name = "_statusLayout";
        _statusLayout.RowCount = 1;
        _statusLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _statusLayout.Size = new Size(918, 37);
        _statusLayout.TabIndex = 0;
        // 
        // _dotConn
        // 
        _dotConn.BackColor = Color.FromArgb(153, 153, 153);
        _dotConn.Dock = DockStyle.Fill;
        _dotConn.Location = new Point(5, 12);
        _dotConn.Margin = new Padding(5, 12, 5, 12);
        _dotConn.Name = "_dotConn";
        _dotConn.Size = new Size(11, 13);
        _dotConn.TabIndex = 1;
        // 
        // _lblConn
        // 
        _lblConn.AutoSize = true;
        _lblConn.Dock = DockStyle.Fill;
        _lblConn.Font = new Font("Segoe UI", 12F);
        _lblConn.ForeColor = Color.FromArgb(102, 112, 124);
        _lblConn.Location = new Point(21, 4);
        _lblConn.Margin = new Padding(0, 4, 0, 0);
        _lblConn.Name = "_lblConn";
        _lblConn.Size = new Size(171, 33);
        _lblConn.TabIndex = 2;
        _lblConn.Text = "АЦП: —";
        // 
        // _lblStorage
        // 
        _lblStorage.AutoSize = true;
        _lblStorage.Dock = DockStyle.Fill;
        _lblStorage.Font = new Font("Segoe UI", 12F);
        _lblStorage.ForeColor = Color.Red;
        _lblStorage.Location = new Point(192, 4);
        _lblStorage.Margin = new Padding(0, 4, 0, 0);
        _lblStorage.Name = "_lblStorage";
        _lblStorage.Size = new Size(726, 33);
        _lblStorage.TabIndex = 3;
        // 
        // StaticWeighingForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(238, 241, 244);
        ClientSize = new Size(918, 859);
        Controls.Add(_layoutMain);
        Controls.Add(_pnlStatusBar);
        KeyPreview = true;
        Margin = new Padding(3, 4, 3, 4);
        Name = "StaticWeighingForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Взвешивание — Статика";
        _layoutMain.ResumeLayout(false);
        _layoutMain.PerformLayout();
        _pnlDisplay.ResumeLayout(false);
        _pnlActions.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        _pnlStatusBar.ResumeLayout(false);
        _statusLayout.ResumeLayout(false);
        _statusLayout.PerformLayout();
        ResumeLayout(false);
    }

    private Label         _lblChannel;
    private TableLayoutPanel _layoutMain;
    private Panel         _pnlDisplay;
    private Label         _lblValue;
    private Label         _lblBogie1Caption;
    private Label         _lblBogie1Value;
    private Label         _lblBogie2Caption;
    private Label         _lblBogie2Value;
    private Button        _btnWeigh;
    private Button        _btnZero;
    private Button        _btnFinish;
    private DataGridView  _grid;
    private Panel         _pnlActions;
    private Panel         _pnlStatusBar;
    private TableLayoutPanel _statusLayout;
    private Panel         _dotConn;
    private Label         _lblConn;
    private Label         _lblStorage;
}
