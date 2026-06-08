namespace Vesy13.Forms;

partial class StaticWeighingForm
{
    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
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
        _btnBack = new Button();
        _dotConn = new Panel();
        _lblConn = new Label();
        _pnlDisplay.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        _pnlStatusBar.SuspendLayout();
        SuspendLayout();
        // 
        // _lblChannel
        // 
        _lblChannel.AutoSize = true;
        _lblChannel.Font = new Font("Segoe UI", 10F);
        _lblChannel.ForeColor = UiColors.TextMuted;
        _lblChannel.Location = new Point(8, 10);
        _lblChannel.Name = "_lblChannel";
        _lblChannel.Size = new Size(0, 19);
        _lblChannel.TabIndex = 0;
        // 
        // _pnlDisplay
        // 
        _pnlDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _pnlDisplay.BackColor = UiColors.DisplayBackground;
        _pnlDisplay.Controls.Add(_lblValue);
        _pnlDisplay.Controls.Add(_lblUnit);
        _pnlDisplay.Controls.Add(_lblStatus);
        _pnlDisplay.Location = new Point(8, 34);
        _pnlDisplay.Name = "_pnlDisplay";
        _pnlDisplay.Size = new Size(617, 158);
        _pnlDisplay.TabIndex = 1;
        // 
        // _lblValue
        // 
        _lblValue.Font = new Font("Courier New", 60F, FontStyle.Bold);
        _lblValue.ForeColor = UiColors.Disconnected;
        _lblValue.Location = new Point(8, 4);
        _lblValue.Name = "_lblValue";
        _lblValue.Size = new Size(450, 106);
        _lblValue.TabIndex = 0;
        _lblValue.Text = "—";
        _lblValue.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblUnit
        // 
        _lblUnit.Font = new Font("Segoe UI", 20F);
        _lblUnit.ForeColor = UiColors.Disconnected;
        _lblUnit.Location = new Point(462, 60);
        _lblUnit.Name = "_lblUnit";
        _lblUnit.Size = new Size(60, 62);
        _lblUnit.TabIndex = 1;
        _lblUnit.Text = "т";
        _lblUnit.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _lblStatus
        // 
        _lblStatus.Font = new Font("Segoe UI", 10F);
        _lblStatus.ForeColor = UiColors.TextOnDarkMuted;
        _lblStatus.Location = new Point(8, 118);
        _lblStatus.Name = "_lblStatus";
        _lblStatus.Size = new Size(528, 34);
        _lblStatus.TabIndex = 2;
        _lblStatus.Text = "Готов к взвешиванию  —  Тележка 1";
        _lblStatus.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _btnWeigh
        // 
        _btnWeigh.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _btnWeigh.BackColor = UiColors.PrimaryAction;
        _btnWeigh.FlatStyle = FlatStyle.Flat;
        _btnWeigh.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        _btnWeigh.ForeColor = UiColors.TextOnDark;
        _btnWeigh.Location = new Point(8, 198);
        _btnWeigh.Name = "_btnWeigh";
        _btnWeigh.Size = new Size(617, 54);
        _btnWeigh.TabIndex = 2;
        _btnWeigh.Text = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1";
        _btnWeigh.UseVisualStyleBackColor = false;
        _btnWeigh.Click += BtnWeigh_Click;
        // 
        // _btnZero
        // 
        _btnZero.FlatStyle = FlatStyle.Flat;
        _btnZero.Font = new Font("Segoe UI", 10F);
        _btnZero.Location = new Point(8, 256);
        _btnZero.Name = "_btnZero";
        _btnZero.Size = new Size(100, 32);
        _btnZero.TabIndex = 3;
        _btnZero.Text = "Ноль";
        _btnZero.Click += BtnZero_Click;
        // 
        // _btnFinish
        // 
        _btnFinish.BackColor = UiColors.DangerAction;
        _btnFinish.FlatStyle = FlatStyle.Flat;
        _btnFinish.Font = new Font("Segoe UI", 10F);
        _btnFinish.ForeColor = UiColors.TextOnDark;
        _btnFinish.Location = new Point(116, 256);
        _btnFinish.Name = "_btnFinish";
        _btnFinish.Size = new Size(244, 32);
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
        dataGridViewCellStyle1.BackColor = UiColors.GridAlternateRow;
        _grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        _grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _grid.BackgroundColor = UiColors.Surface;
        _grid.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle3.BackColor = UiColors.GridHeaderBack;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dataGridViewCellStyle3.ForeColor = UiColors.GridHeaderText;
        dataGridViewCellStyle3.SelectionBackColor = UiColors.GridHeaderBack;
        dataGridViewCellStyle3.SelectionForeColor = UiColors.GridHeaderText;
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
        _grid.ColumnHeadersHeight = 34;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.EnableHeadersVisualStyles = false;
        _grid.Font = new Font("Segoe UI", 9F);
        _grid.Location = new Point(8, 294);
        _grid.Name = "_grid";
        _grid.ReadOnly = true;
        _grid.RowHeadersVisible = false;
        dataGridViewCellStyle2.BackColor = UiColors.Surface;
        _grid.RowsDefaultCellStyle = dataGridViewCellStyle2;
        _grid.RowTemplate.Height = 22;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.Size = new Size(617, 295);
        _grid.TabIndex = 5;
        // 
        // _pnlStatusBar
        // 
        _pnlStatusBar.BackColor = UiColors.StatusBar;
        _pnlStatusBar.Controls.Add(_btnBack);
        _pnlStatusBar.Controls.Add(_dotConn);
        _pnlStatusBar.Controls.Add(_lblConn);
        _pnlStatusBar.Dock = DockStyle.Bottom;
        _pnlStatusBar.Location = new Point(0, 595);
        _pnlStatusBar.Name = "_pnlStatusBar";
        _pnlStatusBar.Size = new Size(633, 34);
        _pnlStatusBar.TabIndex = 6;
        // 
        // _btnBack
        // 
        _btnBack.BackColor = UiColors.NavigationAction;
        _btnBack.FlatAppearance.BorderSize = 0;
        _btnBack.FlatStyle = FlatStyle.Flat;
        _btnBack.Font = new Font("Segoe UI", 8F);
        _btnBack.ForeColor = UiColors.TextOnDark;
        _btnBack.Location = new Point(8, 6);
        _btnBack.Name = "_btnBack";
        _btnBack.Size = new Size(80, 22);
        _btnBack.TabIndex = 0;
        _btnBack.Text = "← Назад";
        _btnBack.UseVisualStyleBackColor = false;
        _btnBack.Click += BtnBack_Click;
        // 
        // _dotConn
        // 
        _dotConn.BackColor = UiColors.Disconnected;
        _dotConn.Location = new Point(100, 12);
        _dotConn.Name = "_dotConn";
        _dotConn.Size = new Size(10, 10);
        _dotConn.TabIndex = 1;
        // 
        // _lblConn
        // 
        _lblConn.AutoSize = true;
        _lblConn.Font = new Font("Segoe UI", 9F);
        _lblConn.ForeColor = UiColors.TextOnDarkMuted;
        _lblConn.Location = new Point(116, 8);
        _lblConn.Name = "_lblConn";
        _lblConn.Size = new Size(51, 15);
        _lblConn.TabIndex = 2;
        _lblConn.Text = "АЦП: —";
        // 
        // StaticWeighingForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = UiColors.AppBackground;
        ClientSize = new Size(633, 629);
        Controls.Add(_lblChannel);
        Controls.Add(_pnlDisplay);
        Controls.Add(_btnWeigh);
        Controls.Add(_btnZero);
        Controls.Add(_btnFinish);
        Controls.Add(_grid);
        Controls.Add(_pnlStatusBar);
        KeyPreview = true;
        MaximumSize = new Size(649, 10000);
        Name = "StaticWeighingForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Взвешивание — Статика";
        _pnlDisplay.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        _pnlStatusBar.ResumeLayout(false);
        _pnlStatusBar.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
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
