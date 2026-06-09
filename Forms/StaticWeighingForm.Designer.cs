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
        _lblUnit = new Label();
        _lblStatus = new Label();
        _btnWeigh = new Button();
        _btnZero = new Button();
        _btnFinish = new Button();
        _grid = new DataGridView();
        _pnlActions = new Panel();
        _pnlStatusBar = new Panel();
        _statusLayout = new TableLayoutPanel();
        _dotConn = new Panel();
        _lblConn = new Label();
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
        _lblChannel.Font = UiFonts.Medium;
        _lblChannel.ForeColor = UiColors.TextMuted;
        _lblChannel.Margin = new Padding(0, 0, 0, 6);
        _lblChannel.Name = "_lblChannel";
        _lblChannel.Size = new Size(657, 19);
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
        _layoutMain.Name = "_layoutMain";
        _layoutMain.Padding = new Padding(8, 8, 8, 8);
        _layoutMain.RowCount = 4;
        _layoutMain.RowStyles.Add(new RowStyle());
        _layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 158F));
        _layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
        _layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _layoutMain.Size = new Size(673, 629);
        _layoutMain.TabIndex = 0;
        // 
        // _pnlDisplay
        // 
        _pnlDisplay.BackColor = UiColors.DisplayBackground;
        _pnlDisplay.Controls.Add(_lblValue);
        _pnlDisplay.Controls.Add(_lblUnit);
        _pnlDisplay.Controls.Add(_lblStatus);
        _pnlDisplay.Dock = DockStyle.Fill;
        _pnlDisplay.Margin = new Padding(0, 0, 0, 8);
        _pnlDisplay.Name = "_pnlDisplay";
        _pnlDisplay.Size = new Size(657, 158);
        _pnlDisplay.TabIndex = 1;
        // 
        // _lblValue
        // 
        _lblValue.Font = UiFonts.Display;
        _lblValue.ForeColor = UiColors.TextOnDarkMuted;
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
        _lblUnit.ForeColor = UiColors.TextOnDarkMuted;
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
        _lblStatus.ForeColor = UiColors.TextOnDarkMuted;
        _lblStatus.Location = new Point(8, 118);
        _lblStatus.Name = "_lblStatus";
        _lblStatus.Size = new Size(584, 34);
        _lblStatus.TabIndex = 2;
        _lblStatus.Text = "Готов к взвешиванию  —  Тележка 1";
        _lblStatus.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _pnlActions
        // 
        _pnlActions.Controls.Add(_btnWeigh);
        _pnlActions.Controls.Add(_btnZero);
        _pnlActions.Controls.Add(_btnFinish);
        _pnlActions.Dock = DockStyle.Fill;
        _pnlActions.Margin = new Padding(0, 0, 0, 8);
        _pnlActions.Name = "_pnlActions";
        _pnlActions.Size = new Size(657, 96);
        _pnlActions.TabIndex = 2;
        // 
        // _btnWeigh
        // 
        _btnWeigh.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _btnWeigh.BackColor = UiColors.PrimaryAction;
        _btnWeigh.FlatStyle = FlatStyle.Flat;
        _btnWeigh.Font = UiFonts.WeighButton;
        _btnWeigh.ForeColor = UiColors.TextOnDark;
        _btnWeigh.Location = new Point(0, 0);
        _btnWeigh.Name = "_btnWeigh";
        _btnWeigh.Size = new Size(657, 54);
        _btnWeigh.TabIndex = 2;
        _btnWeigh.Text = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1";
        _btnWeigh.UseVisualStyleBackColor = false;
        _btnWeigh.Click += BtnWeigh_Click;
        // 
        // _btnZero
        // 
        _btnZero.BackColor = UiColors.NeutralAction;
        _btnZero.FlatStyle = FlatStyle.Flat;
        _btnZero.Font = UiFonts.Medium;
        _btnZero.ForeColor = UiColors.TextPrimary;
        _btnZero.Location = new Point(0, 58);
        _btnZero.Name = "_btnZero";
        _btnZero.Size = new Size(100, 32);
        _btnZero.TabIndex = 3;
        _btnZero.Text = "Ноль";
        _btnZero.UseVisualStyleBackColor = false;
        _btnZero.Click += BtnZero_Click;
        // 
        // _btnFinish
        // 
        _btnFinish.BackColor = UiColors.DangerAction;
        _btnFinish.FlatStyle = FlatStyle.Flat;
        _btnFinish.Font = UiFonts.Medium;
        _btnFinish.ForeColor = UiColors.TextOnDark;
        _btnFinish.Location = new Point(108, 58);
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
        dataGridViewCellStyle1.ForeColor = UiColors.TextPrimary;
        dataGridViewCellStyle1.SelectionBackColor = UiColors.GridSelectionBack;
        dataGridViewCellStyle1.SelectionForeColor = UiColors.GridSelectionText;
        _grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        _grid.BackgroundColor = UiColors.Surface;
        _grid.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle2.BackColor = UiColors.GridHeaderBack;
        dataGridViewCellStyle2.Font = UiFonts.GridHeader;
        dataGridViewCellStyle2.ForeColor = UiColors.GridHeaderText;
        dataGridViewCellStyle2.SelectionBackColor = UiColors.GridHeaderBack;
        dataGridViewCellStyle2.SelectionForeColor = UiColors.GridHeaderText;
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        _grid.ColumnHeadersHeight = 40;
        _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _grid.Dock = DockStyle.Fill;
        _grid.EnableHeadersVisualStyles = false;
        _grid.Font = UiFonts.GridBody;
        _grid.GridColor = UiColors.GridLine;
        _grid.Location = new Point(8, 270);
        _grid.Name = "_grid";
        _grid.ReadOnly = true;
        _grid.RowHeadersVisible = false;
        dataGridViewCellStyle3.BackColor = UiColors.GridAlternateRow;
        dataGridViewCellStyle3.ForeColor = UiColors.TextPrimary;
        dataGridViewCellStyle3.SelectionBackColor = UiColors.GridSelectionBack;
        dataGridViewCellStyle3.SelectionForeColor = UiColors.GridSelectionText;
        _grid.RowsDefaultCellStyle = dataGridViewCellStyle3;
        _grid.RowTemplate.Height = 28;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.Size = new Size(657, 351);
        _grid.TabIndex = 5;
        // 
        // _pnlStatusBar
        // 
        _pnlStatusBar.BackColor = UiColors.StatusBar;
        _pnlStatusBar.Controls.Add(_statusLayout);
        _pnlStatusBar.Dock = DockStyle.Bottom;
        _pnlStatusBar.Location = new Point(0, 595);
        _pnlStatusBar.Name = "_pnlStatusBar";
        _pnlStatusBar.Size = new Size(673, 28);
        _pnlStatusBar.TabIndex = 6;
        // 
        // _statusLayout
        // 
        _statusLayout.ColumnCount = 2;
        _statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 18F));
        _statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _statusLayout.Dock = DockStyle.Fill;
        _statusLayout.Location = new Point(0, 0);
        _statusLayout.Name = "_statusLayout";
        _statusLayout.RowCount = 1;
        _statusLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _statusLayout.Size = new Size(673, 28);
        _statusLayout.TabIndex = 0;
        _statusLayout.Controls.Add(_dotConn, 0, 0);
        _statusLayout.Controls.Add(_lblConn, 1, 0);
        // 
        // _dotConn
        // 
        _dotConn.BackColor = UiColors.Disconnected;
        _dotConn.Dock = DockStyle.Fill;
        _dotConn.Margin = new Padding(4, 9, 4, 9);
        _dotConn.Name = "_dotConn";
        _dotConn.Size = new Size(10, 10);
        _dotConn.TabIndex = 1;
        // 
        // _lblConn
        // 
        _lblConn.AutoSize = true;
        _lblConn.Font = UiFonts.Body;
        _lblConn.ForeColor = UiColors.TextMuted;
        _lblConn.Dock = DockStyle.Fill;
        _lblConn.Margin = new Padding(0, 3, 0, 0);
        _lblConn.Name = "_lblConn";
        _lblConn.Size = new Size(655, 25);
        _lblConn.TabIndex = 2;
        _lblConn.Text = "АЦП: —";
        // 
        // StaticWeighingForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = UiColors.AppBackground;
        ClientSize = new Size(673, 629);
        Controls.Add(_layoutMain);
        Controls.Add(_pnlStatusBar);
        KeyPreview = true;
        MaximumSize = new Size(689, 10000);
        Name = "StaticWeighingForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Взвешивание — Статика";
        _layoutMain.ResumeLayout(false);
        _layoutMain.PerformLayout();
        _pnlDisplay.ResumeLayout(false);
        _pnlActions.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        _statusLayout.ResumeLayout(false);
        _pnlStatusBar.ResumeLayout(false);
        _pnlStatusBar.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label         _lblChannel;
    private TableLayoutPanel _layoutMain;
    private Panel         _pnlDisplay;
    private Label         _lblValue;
    private Label         _lblUnit;
    private Label         _lblStatus;
    private Button        _btnWeigh;
    private Button        _btnZero;
    private Button        _btnFinish;
    private DataGridView  _grid;
    private Panel         _pnlActions;
    private Panel         _pnlStatusBar;
    private TableLayoutPanel _statusLayout;
    private Panel         _dotConn;
    private Label         _lblConn;
}
