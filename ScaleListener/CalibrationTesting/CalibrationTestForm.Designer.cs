namespace ScaleListener.CalibrationTesting;

partial class CalibrationTestForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        _pnlTop = new Panel();
        _tlpTop = new TableLayoutPanel();
        _lblChannel = new Label();
        _cmbChannel = new ComboBox();
        _btnRun = new Button();
        _btnReset = new Button();
        _btnExport = new Button();
        _btnConnect = new Button();
        _lblActiveCode = new Label();
        _splitMain = new SplitContainer();
        _gridAnchors = new DataGridView();
        _colAnchorMass = new DataGridViewTextBoxColumn();
        _colAnchorCode = new DataGridViewTextBoxColumn();
        _lblAnchors = new Label();
        _lblAnchorsHint = new Label();
        _gridResults = new DataGridView();
        _colCheckpoint = new DataGridViewTextBoxColumn();
        _colAdcCode = new DataGridViewTextBoxColumn();
        _colExpected = new DataGridViewTextBoxColumn();
        _colActual = new DataGridViewTextBoxColumn();
        _colError = new DataGridViewTextBoxColumn();
        _colPoint = new DataGridViewTextBoxColumn();
        _colStatus = new DataGridViewTextBoxColumn();
        _lblResults = new Label();
        _pnlBottom = new Panel();
        _lblStatus = new Label();
        _saveCsvDialog = new SaveFileDialog();
        _pnlTop.SuspendLayout();
        _tlpTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_splitMain).BeginInit();
        _splitMain.Panel1.SuspendLayout();
        _splitMain.Panel2.SuspendLayout();
        _splitMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_gridAnchors).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_gridResults).BeginInit();
        _pnlBottom.SuspendLayout();
        SuspendLayout();
        //
        // _pnlTop
        //
        _pnlTop.Controls.Add(_tlpTop);
        _pnlTop.Dock = DockStyle.Top;
        _pnlTop.Location = new Point(0, 0);
        _pnlTop.Name = "_pnlTop";
        _pnlTop.Size = new Size(1184, 62);
        _pnlTop.TabIndex = 0;
        //
        // _tlpTop
        //
        _tlpTop.ColumnCount = 7;
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpTop.Controls.Add(_lblChannel, 0, 0);
        _tlpTop.Controls.Add(_cmbChannel, 1, 0);
        _tlpTop.Controls.Add(_btnRun, 2, 0);
        _tlpTop.Controls.Add(_btnReset, 3, 0);
        _tlpTop.Controls.Add(_btnExport, 4, 0);
        _tlpTop.Controls.Add(_btnConnect, 5, 0);
        _tlpTop.Controls.Add(_lblActiveCode, 6, 0);
        _tlpTop.Dock = DockStyle.Fill;
        _tlpTop.Location = new Point(0, 0);
        _tlpTop.Name = "_tlpTop";
        _tlpTop.RowCount = 1;
        _tlpTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpTop.Size = new Size(1184, 62);
        _tlpTop.TabIndex = 0;
        //
        // _lblChannel
        //
        _lblChannel.Dock = DockStyle.Fill;
        _lblChannel.Location = new Point(3, 0);
        _lblChannel.Name = "_lblChannel";
        _lblChannel.Size = new Size(84, 62);
        _lblChannel.TabIndex = 0;
        _lblChannel.Text = "Канал";
        _lblChannel.TextAlign = ContentAlignment.MiddleRight;
        //
        // _cmbChannel
        //
        _cmbChannel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cmbChannel.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbChannel.Items.AddRange(new object[] { "CH0", "CH1" });
        _cmbChannel.Location = new Point(94, 14);
        _cmbChannel.Margin = new Padding(4, 0, 4, 0);
        _cmbChannel.Name = "_cmbChannel";
        _cmbChannel.Size = new Size(102, 36);
        _cmbChannel.TabIndex = 1;
        _cmbChannel.SelectedIndexChanged += CmbChannel_SelectedIndexChanged;
        //
        // _btnRun
        //
        _btnRun.Dock = DockStyle.Fill;
        _btnRun.FlatStyle = FlatStyle.Flat;
        _btnRun.Location = new Point(207, 7);
        _btnRun.Margin = new Padding(7);
        _btnRun.Name = "_btnRun";
        _btnRun.Size = new Size(161, 48);
        _btnRun.TabIndex = 2;
        _btnRun.Text = "Выполнить тест";
        _btnRun.Click += BtnRun_Click;
        //
        // _btnReset
        //
        _btnReset.Dock = DockStyle.Fill;
        _btnReset.FlatStyle = FlatStyle.Flat;
        _btnReset.Location = new Point(382, 7);
        _btnReset.Margin = new Padding(7);
        _btnReset.Name = "_btnReset";
        _btnReset.Size = new Size(141, 48);
        _btnReset.TabIndex = 3;
        _btnReset.Text = "Пример";
        _btnReset.Click += BtnReset_Click;
        //
        // _btnExport
        //
        _btnExport.Dock = DockStyle.Fill;
        _btnExport.Enabled = false;
        _btnExport.FlatStyle = FlatStyle.Flat;
        _btnExport.Location = new Point(537, 7);
        _btnExport.Margin = new Padding(7);
        _btnExport.Name = "_btnExport";
        _btnExport.Size = new Size(131, 48);
        _btnExport.TabIndex = 4;
        _btnExport.Text = "Экспорт CSV";
        _btnExport.Click += BtnExport_Click;
        //
        // _btnConnect
        //
        _btnConnect.Dock = DockStyle.Fill;
        _btnConnect.FlatStyle = FlatStyle.Flat;
        _btnConnect.Location = new Point(682, 7);
        _btnConnect.Margin = new Padding(7);
        _btnConnect.Name = "_btnConnect";
        _btnConnect.Size = new Size(141, 48);
        _btnConnect.TabIndex = 5;
        _btnConnect.Text = "Connect";
        _btnConnect.Click += BtnConnect_Click;
        //
        // _lblActiveCode
        //
        _lblActiveCode.Dock = DockStyle.Fill;
        _lblActiveCode.Font = new Font("Courier New", 11F, FontStyle.Bold);
        _lblActiveCode.Location = new Point(833, 0);
        _lblActiveCode.Name = "_lblActiveCode";
        _lblActiveCode.Size = new Size(348, 62);
        _lblActiveCode.TabIndex = 6;
        _lblActiveCode.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _splitMain
        //
        _splitMain.Dock = DockStyle.Fill;
        _splitMain.FixedPanel = FixedPanel.Panel1;
        _splitMain.Location = new Point(0, 62);
        _splitMain.Name = "_splitMain";
        //
        // _splitMain.Panel1
        //
        _splitMain.Panel1.Controls.Add(_gridAnchors);
        _splitMain.Panel1.Controls.Add(_lblAnchorsHint);
        _splitMain.Panel1.Controls.Add(_lblAnchors);
        //
        // _splitMain.Panel2
        //
        _splitMain.Panel2.Controls.Add(_gridResults);
        _splitMain.Panel2.Controls.Add(_lblResults);
        _splitMain.Size = new Size(1184, 544);
        _splitMain.SplitterDistance = 330;
        _splitMain.TabIndex = 1;
        //
        // _gridAnchors
        //
        _gridAnchors.AllowUserToDeleteRows = true;
        _gridAnchors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _gridAnchors.BackgroundColor = Color.White;
        _gridAnchors.ColumnHeadersHeight = 34;
        _gridAnchors.Columns.AddRange(new DataGridViewColumn[] { _colAnchorMass, _colAnchorCode });
        _gridAnchors.Dock = DockStyle.Fill;
        _gridAnchors.EditMode = DataGridViewEditMode.EditOnEnter;
        _gridAnchors.Location = new Point(0, 42);
        _gridAnchors.Name = "_gridAnchors";
        _gridAnchors.RowHeadersVisible = false;
        _gridAnchors.RowTemplate.Height = 30;
        _gridAnchors.Size = new Size(330, 452);
        _gridAnchors.TabIndex = 1;
        //
        // _colAnchorMass
        //
        _colAnchorMass.HeaderText = "Масса, т";
        _colAnchorMass.Name = "_colAnchorMass";
        //
        // _colAnchorCode
        //
        _colAnchorCode.HeaderText = "Код АЦП";
        _colAnchorCode.Name = "_colAnchorCode";
        //
        // _lblAnchors
        //
        _lblAnchors.Dock = DockStyle.Top;
        _lblAnchors.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblAnchors.Location = new Point(0, 0);
        _lblAnchors.Name = "_lblAnchors";
        _lblAnchors.Size = new Size(330, 42);
        _lblAnchors.TabIndex = 0;
        _lblAnchors.Text = "Контрольные точки";
        _lblAnchors.TextAlign = ContentAlignment.MiddleCenter;
        //
        // _lblAnchorsHint
        //
        _lblAnchorsHint.Dock = DockStyle.Bottom;
        _lblAnchorsHint.Font = new Font("Segoe UI", 10F);
        _lblAnchorsHint.Location = new Point(0, 494);
        _lblAnchorsHint.Name = "_lblAnchorsHint";
        _lblAnchorsHint.Size = new Size(330, 50);
        _lblAnchorsHint.TabIndex = 2;
        _lblAnchorsHint.Text = "Ровно одна точка должна иметь массу 0 т.\nКоды должны возрастать вместе с массой.";
        _lblAnchorsHint.TextAlign = ContentAlignment.MiddleCenter;
        //
        // _gridResults
        //
        _gridResults.AllowUserToAddRows = false;
        _gridResults.AllowUserToDeleteRows = false;
        _gridResults.BackgroundColor = Color.White;
        _gridResults.ColumnHeadersHeight = 34;
        _gridResults.Columns.AddRange(new DataGridViewColumn[] { _colCheckpoint, _colAdcCode, _colExpected, _colActual, _colError, _colPoint, _colStatus });
        _gridResults.Dock = DockStyle.Fill;
        _gridResults.Location = new Point(0, 42);
        _gridResults.MultiSelect = false;
        _gridResults.Name = "_gridResults";
        _gridResults.ReadOnly = true;
        _gridResults.RowHeadersVisible = false;
        _gridResults.RowTemplate.Height = 30;
        _gridResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gridResults.Size = new Size(850, 502);
        _gridResults.TabIndex = 1;
        _gridResults.SelectionChanged += GridResults_SelectionChanged;
        //
        // _colCheckpoint
        //
        _colCheckpoint.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        _colCheckpoint.HeaderText = "Проверка";
        _colCheckpoint.Name = "_colCheckpoint";
        _colCheckpoint.ReadOnly = true;
        //
        // _colAdcCode
        //
        _colAdcCode.HeaderText = "ADC";
        _colAdcCode.Name = "_colAdcCode";
        _colAdcCode.ReadOnly = true;
        _colAdcCode.Width = 80;
        //
        // _colExpected
        //
        _colExpected.HeaderText = "Эталон, т";
        _colExpected.Name = "_colExpected";
        _colExpected.ReadOnly = true;
        _colExpected.Width = 110;
        //
        // _colActual
        //
        _colActual.HeaderText = "Текущий, т";
        _colActual.Name = "_colActual";
        _colActual.ReadOnly = true;
        _colActual.Width = 110;
        //
        // _colError
        //
        _colError.HeaderText = "Ошибка, т";
        _colError.Name = "_colError";
        _colError.ReadOnly = true;
        _colError.Width = 105;
        //
        // _colPoint
        //
        _colPoint.HeaderText = "ID точки";
        _colPoint.Name = "_colPoint";
        _colPoint.ReadOnly = true;
        _colPoint.Width = 90;
        //
        // _colStatus
        //
        _colStatus.HeaderText = "Статус";
        _colStatus.Name = "_colStatus";
        _colStatus.ReadOnly = true;
        _colStatus.Width = 80;
        //
        // _lblResults
        //
        _lblResults.Dock = DockStyle.Top;
        _lblResults.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblResults.Location = new Point(0, 0);
        _lblResults.Name = "_lblResults";
        _lblResults.Size = new Size(850, 42);
        _lblResults.TabIndex = 0;
        _lblResults.Text = "Текущий CalibrationCalculator и линейный эталон";
        _lblResults.TextAlign = ContentAlignment.MiddleCenter;
        //
        // _pnlBottom
        //
        _pnlBottom.Controls.Add(_lblStatus);
        _pnlBottom.Dock = DockStyle.Bottom;
        _pnlBottom.Location = new Point(0, 606);
        _pnlBottom.Name = "_pnlBottom";
        _pnlBottom.Size = new Size(1184, 45);
        _pnlBottom.TabIndex = 2;
        //
        // _lblStatus
        //
        _lblStatus.Dock = DockStyle.Fill;
        _lblStatus.Location = new Point(0, 0);
        _lblStatus.Name = "_lblStatus";
        _lblStatus.Padding = new Padding(8, 0, 8, 0);
        _lblStatus.Size = new Size(1184, 45);
        _lblStatus.TabIndex = 0;
        _lblStatus.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _saveCsvDialog
        //
        _saveCsvDialog.DefaultExt = "csv";
        _saveCsvDialog.FileName = "calibration-before.csv";
        _saveCsvDialog.Filter = "CSV (*.csv)|*.csv|Все файлы (*.*)|*.*";
        _saveCsvDialog.Title = "Сохранить результаты проверки калибровки";
        //
        // CalibrationTestForm
        //
        ClientSize = new Size(1184, 651);
        Controls.Add(_splitMain);
        Controls.Add(_pnlBottom);
        Controls.Add(_pnlTop);
        Font = new Font("Segoe UI", 12F);
        MinimumSize = new Size(1000, 600);
        Name = "CalibrationTestForm";
        Text = "Scale Listener - Проверка калибровки - COM4  4800/Even";
        _pnlTop.ResumeLayout(false);
        _tlpTop.ResumeLayout(false);
        _splitMain.Panel1.ResumeLayout(false);
        _splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_splitMain).EndInit();
        _splitMain.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_gridAnchors).EndInit();
        ((System.ComponentModel.ISupportInitialize)_gridResults).EndInit();
        _pnlBottom.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Panel _pnlTop;
    private TableLayoutPanel _tlpTop;
    private Label _lblChannel;
    private ComboBox _cmbChannel;
    private Button _btnRun;
    private Button _btnReset;
    private Button _btnExport;
    private Button _btnConnect;
    private Label _lblActiveCode;
    private SplitContainer _splitMain;
    private DataGridView _gridAnchors;
    private DataGridViewTextBoxColumn _colAnchorMass;
    private DataGridViewTextBoxColumn _colAnchorCode;
    private Label _lblAnchors;
    private Label _lblAnchorsHint;
    private DataGridView _gridResults;
    private DataGridViewTextBoxColumn _colCheckpoint;
    private DataGridViewTextBoxColumn _colAdcCode;
    private DataGridViewTextBoxColumn _colExpected;
    private DataGridViewTextBoxColumn _colActual;
    private DataGridViewTextBoxColumn _colError;
    private DataGridViewTextBoxColumn _colPoint;
    private DataGridViewTextBoxColumn _colStatus;
    private Label _lblResults;
    private Panel _pnlBottom;
    private Label _lblStatus;
    private SaveFileDialog _saveCsvDialog;
}
