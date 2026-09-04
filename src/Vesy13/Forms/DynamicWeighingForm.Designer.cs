namespace Vesy13.Forms;

partial class DynamicWeighingForm
{
    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
        _gbDir = new GroupBox();
        _rbRightFactor = new RadioButton();
        _rbLeftFactor = new RadioButton();
        _lblChannel = new Label();
        _layoutMain = new TableLayoutPanel();
        _pnlTop = new Panel();
        _pnlDisplay = new Panel();
        _lblBogie2Value = new Label();
        _lblBogie2Caption = new Label();
        _lblBogie1Value = new Label();
        _lblBogie1Caption = new Label();
        _lblValue = new Label();
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
        _gbDir.SuspendLayout();
        _layoutMain.SuspendLayout();
        _pnlTop.SuspendLayout();
        _pnlDisplay.SuspendLayout();
        _pnlActions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        _pnlStatusBar.SuspendLayout();
        _statusLayout.SuspendLayout();
        SuspendLayout();
        // 
        // _gbDir
        // 
        _gbDir.Controls.Add(_rbRightFactor);
        _gbDir.Controls.Add(_rbLeftFactor);
        _gbDir.Dock = DockStyle.Left;
        _gbDir.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        _gbDir.ForeColor = Color.FromArgb(46, 58, 70);
        _gbDir.Location = new Point(0, 0);
        _gbDir.Margin = new Padding(3, 4, 3, 4);
        _gbDir.Name = "_gbDir";
        _gbDir.Padding = new Padding(3, 4, 3, 4);
        _gbDir.Size = new Size(366, 62);
        _gbDir.TabIndex = 0;
        _gbDir.TabStop = false;
        _gbDir.Text = "Направление движения состава";
        // 
        // _rbRightFactor
        // 
        _rbRightFactor.AutoSize = true;
        _rbRightFactor.Checked = true;
        _rbRightFactor.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _rbRightFactor.ForeColor = Color.FromArgb(46, 58, 70);
        _rbRightFactor.Location = new Point(14, 30);
        _rbRightFactor.Margin = new Padding(3, 4, 3, 4);
        _rbRightFactor.Name = "_rbRightFactor";
        _rbRightFactor.Size = new Size(83, 27);
        _rbRightFactor.TabIndex = 0;
        _rbRightFactor.TabStop = true;
        _rbRightFactor.Text = "С лева";
        // 
        // _rbLeftFactor
        // 
        _rbLeftFactor.AutoSize = true;
        _rbLeftFactor.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _rbLeftFactor.ForeColor = Color.FromArgb(46, 58, 70);
        _rbLeftFactor.Location = new Point(149, 30);
        _rbLeftFactor.Margin = new Padding(3, 4, 3, 4);
        _rbLeftFactor.Name = "_rbLeftFactor";
        _rbLeftFactor.Size = new Size(94, 27);
        _rbLeftFactor.TabIndex = 1;
        _rbLeftFactor.Text = "С права";
        // 
        // _lblChannel
        // 
        _lblChannel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblChannel.AutoSize = true;
        _lblChannel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _lblChannel.ForeColor = Color.FromArgb(102, 112, 124);
        _lblChannel.Location = new Point(608, 30);
        _lblChannel.Margin = new Padding(0);
        _lblChannel.Name = "_lblChannel";
        _lblChannel.Size = new Size(0, 23);
        _lblChannel.TabIndex = 1;
        _lblChannel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _layoutMain
        // 
        _layoutMain.ColumnCount = 1;
        _layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _layoutMain.Controls.Add(_pnlTop, 0, 0);
        _layoutMain.Controls.Add(_pnlDisplay, 0, 1);
        _layoutMain.Controls.Add(_pnlActions, 0, 2);
        _layoutMain.Controls.Add(_grid, 0, 3);
        _layoutMain.Dock = DockStyle.Fill;
        _layoutMain.Location = new Point(0, 0);
        _layoutMain.Margin = new Padding(3, 4, 3, 4);
        _layoutMain.Name = "_layoutMain";
        _layoutMain.Padding = new Padding(9, 10, 9, 10);
        _layoutMain.RowCount = 4;
        _layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
        _layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 210F));
        _layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 135F));
        _layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _layoutMain.Size = new Size(993, 800);
        _layoutMain.TabIndex = 0;
        // 
        // _pnlTop
        // 
        _pnlTop.Controls.Add(_gbDir);
        _pnlTop.Controls.Add(_lblChannel);
        _pnlTop.Dock = DockStyle.Fill;
        _pnlTop.Location = new Point(9, 10);
        _pnlTop.Margin = new Padding(0, 0, 0, 10);
        _pnlTop.Name = "_pnlTop";
        _pnlTop.Size = new Size(975, 62);
        _pnlTop.TabIndex = 1;
        // 
        // _pnlDisplay
        // 
        _pnlDisplay.BackColor = Color.FromArgb(5, 8, 12);
        _pnlDisplay.Controls.Add(_lblBogie2Value);
        _pnlDisplay.Controls.Add(_lblBogie2Caption);
        _pnlDisplay.Controls.Add(_lblBogie1Value);
        _pnlDisplay.Controls.Add(_lblBogie1Caption);
        _pnlDisplay.Controls.Add(_lblValue);
        _pnlDisplay.Dock = DockStyle.Fill;
        _pnlDisplay.Location = new Point(9, 82);
        _pnlDisplay.Margin = new Padding(0, 0, 0, 10);
        _pnlDisplay.Name = "_pnlDisplay";
        _pnlDisplay.Size = new Size(975, 200);
        _pnlDisplay.TabIndex = 2;
        // 
        // _lblBogie2Value
        // 
        _lblBogie2Value.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblBogie2Value.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _lblBogie2Value.ForeColor = Color.FromArgb(215, 224, 234);
        _lblBogie2Value.Location = new Point(834, 118);
        _lblBogie2Value.Name = "_lblBogie2Value";
        _lblBogie2Value.Size = new Size(128, 34);
        _lblBogie2Value.TabIndex = 6;
        _lblBogie2Value.Text = "—";
        _lblBogie2Value.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblBogie2Caption
        // 
        _lblBogie2Caption.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblBogie2Caption.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        _lblBogie2Caption.ForeColor = Color.FromArgb(215, 224, 234);
        _lblBogie2Caption.Location = new Point(834, 90);
        _lblBogie2Caption.Name = "_lblBogie2Caption";
        _lblBogie2Caption.Size = new Size(128, 26);
        _lblBogie2Caption.TabIndex = 5;
        _lblBogie2Caption.Text = "Тележка 2";
        _lblBogie2Caption.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblBogie1Value
        // 
        _lblBogie1Value.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblBogie1Value.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _lblBogie1Value.ForeColor = Color.FromArgb(215, 224, 234);
        _lblBogie1Value.Location = new Point(834, 48);
        _lblBogie1Value.Name = "_lblBogie1Value";
        _lblBogie1Value.Size = new Size(128, 34);
        _lblBogie1Value.TabIndex = 4;
        _lblBogie1Value.Text = "—";
        _lblBogie1Value.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblBogie1Caption
        // 
        _lblBogie1Caption.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _lblBogie1Caption.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        _lblBogie1Caption.ForeColor = Color.FromArgb(215, 224, 234);
        _lblBogie1Caption.Location = new Point(834, 22);
        _lblBogie1Caption.Name = "_lblBogie1Caption";
        _lblBogie1Caption.Size = new Size(128, 26);
        _lblBogie1Caption.TabIndex = 3;
        _lblBogie1Caption.Text = "Тележка 1";
        _lblBogie1Caption.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _lblValue
        // 
        _lblValue.Font = new Font("Courier New", 60F, FontStyle.Bold, GraphicsUnit.Point);
        _lblValue.ForeColor = Color.FromArgb(215, 224, 234);
        _lblValue.Location = new Point(9, 6);
        _lblValue.Name = "_lblValue";
        _lblValue.Size = new Size(514, 142);
        _lblValue.TabIndex = 0;
        _lblValue.Text = "—";
        _lblValue.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _pnlActions
        // 
        _pnlActions.Controls.Add(_btnWeigh);
        _pnlActions.Controls.Add(_btnZero);
        _pnlActions.Controls.Add(_btnFinish);
        _pnlActions.Dock = DockStyle.Fill;
        _pnlActions.Location = new Point(9, 292);
        _pnlActions.Margin = new Padding(0, 0, 0, 10);
        _pnlActions.Name = "_pnlActions";
        _pnlActions.Size = new Size(975, 125);
        _pnlActions.TabIndex = 3;
        // 
        // _btnWeigh
        // 
        _btnWeigh.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _btnWeigh.BackColor = Color.FromArgb(47, 111, 237);
        _btnWeigh.FlatStyle = FlatStyle.Flat;
        _btnWeigh.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
        _btnWeigh.ForeColor = Color.FromArgb(255, 255, 255);
        _btnWeigh.Location = new Point(0, 0);
        _btnWeigh.Margin = new Padding(3, 4, 3, 4);
        _btnWeigh.Name = "_btnWeigh";
        _btnWeigh.Size = new Size(975, 72);
        _btnWeigh.TabIndex = 3;
        _btnWeigh.Text = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1";
        _btnWeigh.UseVisualStyleBackColor = false;
        _btnWeigh.Click += BtnWeigh_Click;
        // 
        // _btnZero
        // 
        _btnZero.BackColor = Color.FromArgb(217, 226, 236);
        _btnZero.FlatStyle = FlatStyle.Flat;
        _btnZero.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _btnZero.ForeColor = Color.FromArgb(46, 58, 70);
        _btnZero.Location = new Point(0, 78);
        _btnZero.Margin = new Padding(3, 4, 3, 4);
        _btnZero.Name = "_btnZero";
        _btnZero.Size = new Size(114, 42);
        _btnZero.TabIndex = 4;
        _btnZero.Text = "Ноль";
        _btnZero.UseVisualStyleBackColor = false;
        _btnZero.Click += BtnZero_Click;
        // 
        // _btnFinish
        // 
        _btnFinish.BackColor = Color.FromArgb(179, 58, 26);
        _btnFinish.FlatStyle = FlatStyle.Flat;
        _btnFinish.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _btnFinish.ForeColor = Color.FromArgb(255, 255, 255);
        _btnFinish.Location = new Point(123, 78);
        _btnFinish.Margin = new Padding(3, 4, 3, 4);
        _btnFinish.Name = "_btnFinish";
        _btnFinish.Size = new Size(279, 42);
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
        dataGridViewCellStyle4.BackColor = Color.FromArgb(240, 244, 248);
        dataGridViewCellStyle4.ForeColor = Color.FromArgb(46, 58, 70);
        dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(220, 232, 247);
        dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(35, 49, 63);
        _grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
        _grid.BackgroundColor = Color.FromArgb(247, 249, 252);
        dataGridViewCellStyle5.BackColor = Color.FromArgb(221, 230, 240);
        dataGridViewCellStyle5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
        dataGridViewCellStyle5.ForeColor = Color.FromArgb(35, 49, 63);
        dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(221, 230, 240);
        dataGridViewCellStyle5.SelectionForeColor = Color.FromArgb(35, 49, 63);
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
        _grid.ColumnHeadersHeight = 40;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.Dock = DockStyle.Fill;
        _grid.EnableHeadersVisualStyles = false;
        _grid.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        _grid.GridColor = Color.FromArgb(200, 208, 218);
        _grid.Location = new Point(12, 431);
        _grid.Margin = new Padding(3, 4, 3, 4);
        _grid.Name = "_grid";
        _grid.ReadOnly = true;
        _grid.RowHeadersVisible = false;
        _grid.RowHeadersWidth = 62;
        dataGridViewCellStyle6.BackColor = Color.FromArgb(240, 244, 248);
        dataGridViewCellStyle6.ForeColor = Color.FromArgb(46, 58, 70);
        dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(220, 232, 247);
        dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(35, 49, 63);
        _grid.RowsDefaultCellStyle = dataGridViewCellStyle6;
        _grid.RowTemplate.Height = 28;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.Size = new Size(969, 355);
        _grid.TabIndex = 6;
        // 
        // _pnlStatusBar
        // 
        _pnlStatusBar.BackColor = Color.FromArgb(217, 226, 236);
        _pnlStatusBar.Controls.Add(_statusLayout);
        _pnlStatusBar.Dock = DockStyle.Bottom;
        _pnlStatusBar.Location = new Point(0, 800);
        _pnlStatusBar.Margin = new Padding(3, 4, 3, 4);
        _pnlStatusBar.Name = "_pnlStatusBar";
        _pnlStatusBar.Size = new Size(993, 38);
        _pnlStatusBar.TabIndex = 7;
        // 
        // _statusLayout
        // 
        _statusLayout.ColumnCount = 3;
        _statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 21F));
        _statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
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
        _statusLayout.Size = new Size(993, 38);
        _statusLayout.TabIndex = 0;
        // 
        // _dotConn
        // 
        _dotConn.BackColor = Color.FromArgb(153, 153, 153);
        _dotConn.Dock = DockStyle.Fill;
        _dotConn.Location = new Point(5, 12);
        _dotConn.Margin = new Padding(5, 12, 5, 12);
        _dotConn.Name = "_dotConn";
        _dotConn.Size = new Size(11, 14);
        _dotConn.TabIndex = 1;
        // 
        // _lblConn
        // 
        _lblConn.AutoSize = true;
        _lblConn.Dock = DockStyle.Fill;
        _lblConn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        _lblConn.ForeColor = Color.FromArgb(102, 112, 124);
        _lblConn.Location = new Point(21, 4);
        _lblConn.Margin = new Padding(0, 4, 0, 0);
        _lblConn.Name = "_lblConn";
        _lblConn.Size = new Size(160, 34);
        _lblConn.TabIndex = 2;
        _lblConn.Text = "АЦП: —";
        // 
        // _lblStorage
        // 
        _lblStorage.AutoSize = true;
        _lblStorage.Dock = DockStyle.Fill;
        _lblStorage.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        _lblStorage.ForeColor = Color.Red;
        _lblStorage.Location = new Point(181, 4);
        _lblStorage.Margin = new Padding(0, 4, 0, 0);
        _lblStorage.Name = "_lblStorage";
        _lblStorage.Size = new Size(812, 34);
        _lblStorage.TabIndex = 3;
        // 
        // DynamicWeighingForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(238, 241, 244);
        ClientSize = new Size(993, 838);
        Controls.Add(_layoutMain);
        Controls.Add(_pnlStatusBar);
        KeyPreview = true;
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(784, 47);
        Name = "DynamicWeighingForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Взвешивание — Динамика";
        _gbDir.ResumeLayout(false);
        _gbDir.PerformLayout();
        _layoutMain.ResumeLayout(false);
        _pnlTop.ResumeLayout(false);
        _pnlTop.PerformLayout();
        _pnlDisplay.ResumeLayout(false);
        _pnlActions.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        _pnlStatusBar.ResumeLayout(false);
        _statusLayout.ResumeLayout(false);
        _statusLayout.PerformLayout();
        ResumeLayout(false);
    }

    private TableLayoutPanel _layoutMain;
    private Panel         _pnlTop;
    private GroupBox      _gbDir;
    private RadioButton   _rbRightFactor;
    private RadioButton   _rbLeftFactor;
    private Label         _lblChannel;
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
