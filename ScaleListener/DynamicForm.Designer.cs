namespace ScaleListener;

partial class DynamicForm
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
        _lblWeight = new Label();
        _numWeight = new NumericUpDown();
        _lblTolerance = new Label();
        _numTolerance = new NumericUpDown();
        _lblHz = new Label();
        _numHz = new NumericUpDown();
        _lblScenario = new Label();
        _cmbScenario = new ComboBox();
        _lblPacketLog = new Label();
        _cmbPacketLog = new ComboBox();
        _lblCode = new Label();
        _lblState = new Label();
        _log = new RichTextBox();
        _btnConnect = new Button();
        _btnStopStream = new Button();
        _btnClear = new Button();
        _btnFaults = new Button();
        _streamTimer = new System.Windows.Forms.Timer(components);
        _pnlTop = new Panel();
        _tlpTop = new TableLayoutPanel();
        _pnlBottom = new Panel();
        _tlpBottom = new TableLayoutPanel();
        ((System.ComponentModel.ISupportInitialize)_numWeight).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numTolerance).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numHz).BeginInit();
        _pnlTop.SuspendLayout();
        _tlpTop.SuspendLayout();
        _pnlBottom.SuspendLayout();
        _tlpBottom.SuspendLayout();
        SuspendLayout();
        //
        // _lblWeight
        //
        _lblWeight.Dock = DockStyle.Fill;
        _lblWeight.Location = new Point(4, 1);
        _lblWeight.Name = "_lblWeight";
        _lblWeight.Size = new Size(72, 48);
        _lblWeight.TabIndex = 0;
        _lblWeight.Text = "Вес, т";
        _lblWeight.TextAlign = ContentAlignment.MiddleRight;
        //
        // _numWeight
        //
        _numWeight.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numWeight.DecimalPlaces = 2;
        _numWeight.Font = new Font("Segoe UI", 12F);
        _numWeight.Increment = new decimal(new int[] { 10, 0, 0, 131072 });
        _numWeight.Location = new Point(84, 8);
        _numWeight.Margin = new Padding(4, 0, 4, 0);
        _numWeight.Maximum = new decimal(new int[] { 140, 0, 0, 0 });
        _numWeight.Name = "_numWeight";
        _numWeight.Size = new Size(102, 34);
        _numWeight.TabIndex = 1;
        _numWeight.ValueChanged += NumWeight_ValueChanged;
        //
        // _lblTolerance
        //
        _lblTolerance.Dock = DockStyle.Fill;
        _lblTolerance.Location = new Point(194, 1);
        _lblTolerance.Name = "_lblTolerance";
        _lblTolerance.Size = new Size(82, 48);
        _lblTolerance.TabIndex = 2;
        _lblTolerance.Text = "Шум, т";
        _lblTolerance.TextAlign = ContentAlignment.MiddleRight;
        //
        // _numTolerance
        //
        _numTolerance.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numTolerance.DecimalPlaces = 2;
        _numTolerance.Font = new Font("Segoe UI", 12F);
        _numTolerance.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
        _numTolerance.Location = new Point(284, 8);
        _numTolerance.Margin = new Padding(4, 0, 4, 0);
        _numTolerance.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        _numTolerance.Name = "_numTolerance";
        _numTolerance.Size = new Size(102, 34);
        _numTolerance.TabIndex = 3;
        _numTolerance.Value = new decimal(new int[] { 2, 0, 0, 131072 });
        _numTolerance.ValueChanged += NumTolerance_ValueChanged;
        //
        // _lblHz
        //
        _lblHz.Dock = DockStyle.Fill;
        _lblHz.Location = new Point(394, 1);
        _lblHz.Name = "_lblHz";
        _lblHz.Size = new Size(42, 48);
        _lblHz.TabIndex = 4;
        _lblHz.Text = "Hz";
        _lblHz.TextAlign = ContentAlignment.MiddleRight;
        //
        // _numHz
        //
        _numHz.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numHz.Font = new Font("Segoe UI", 12F);
        _numHz.Location = new Point(444, 8);
        _numHz.Margin = new Padding(4, 0, 4, 0);
        _numHz.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        _numHz.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        _numHz.Name = "_numHz";
        _numHz.Size = new Size(82, 34);
        _numHz.TabIndex = 5;
        _numHz.Value = new decimal(new int[] { 47, 0, 0, 0 });
        _numHz.ValueChanged += NumHz_ValueChanged;
        //
        // _lblScenario
        //
        _lblScenario.Dock = DockStyle.Fill;
        _lblScenario.Location = new Point(534, 1);
        _lblScenario.Name = "_lblScenario";
        _lblScenario.Size = new Size(102, 48);
        _lblScenario.TabIndex = 6;
        _lblScenario.Text = "Сценарий";
        _lblScenario.TextAlign = ContentAlignment.MiddleRight;
        //
        // _cmbScenario
        //
        _cmbScenario.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cmbScenario.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbScenario.Font = new Font("Segoe UI", 12F);
        _cmbScenario.Items.AddRange(new object[] { "Постоянная", "Две тележки" });
        _cmbScenario.Location = new Point(644, 7);
        _cmbScenario.Margin = new Padding(4, 0, 4, 0);
        _cmbScenario.Name = "_cmbScenario";
        _cmbScenario.Size = new Size(162, 36);
        _cmbScenario.TabIndex = 7;
        _cmbScenario.SelectedIndexChanged += CmbScenario_SelectedIndexChanged;
        //
        // _lblPacketLog
        //
        _lblPacketLog.Dock = DockStyle.Fill;
        _lblPacketLog.Location = new Point(814, 1);
        _lblPacketLog.Name = "_lblPacketLog";
        _lblPacketLog.Size = new Size(52, 48);
        _lblPacketLog.TabIndex = 8;
        _lblPacketLog.Text = "Лог";
        _lblPacketLog.TextAlign = ContentAlignment.MiddleRight;
        //
        // _cmbPacketLog
        //
        _cmbPacketLog.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cmbPacketLog.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbPacketLog.Font = new Font("Segoe UI", 12F);
        _cmbPacketLog.Items.AddRange(new object[] { "Все", "Каждый 10-й" });
        _cmbPacketLog.Location = new Point(874, 7);
        _cmbPacketLog.Margin = new Padding(4, 0, 4, 0);
        _cmbPacketLog.Name = "_cmbPacketLog";
        _cmbPacketLog.Size = new Size(90, 36);
        _cmbPacketLog.TabIndex = 9;
        _cmbPacketLog.SelectedIndexChanged += CmbPacketLog_SelectedIndexChanged;
        //
        // _lblCode
        //
        _lblCode.Dock = DockStyle.Fill;
        _lblCode.Location = new Point(4, 51);
        _lblCode.Name = "_lblCode";
        _lblCode.Size = new Size(960, 38);
        _lblCode.TabIndex = 10;
        _lblCode.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _tlpTop
        //
        _tlpTop.ColumnCount = 10;
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpTop.Controls.Add(_lblWeight, 0, 0);
        _tlpTop.Controls.Add(_numWeight, 1, 0);
        _tlpTop.Controls.Add(_lblTolerance, 2, 0);
        _tlpTop.Controls.Add(_numTolerance, 3, 0);
        _tlpTop.Controls.Add(_lblHz, 4, 0);
        _tlpTop.Controls.Add(_numHz, 5, 0);
        _tlpTop.Controls.Add(_lblScenario, 6, 0);
        _tlpTop.Controls.Add(_cmbScenario, 7, 0);
        _tlpTop.Controls.Add(_lblPacketLog, 8, 0);
        _tlpTop.Controls.Add(_cmbPacketLog, 9, 0);
        _tlpTop.Controls.Add(_lblCode, 0, 1);
        _tlpTop.SetColumnSpan(_lblCode, 10);
        _tlpTop.Dock = DockStyle.Fill;
        _tlpTop.Location = new Point(0, 0);
        _tlpTop.Name = "_tlpTop";
        _tlpTop.RowCount = 2;
        _tlpTop.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        _tlpTop.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        _tlpTop.Size = new Size(968, 90);
        _tlpTop.TabIndex = 0;
        //
        // _pnlTop
        //
        _pnlTop.Controls.Add(_tlpTop);
        _pnlTop.Dock = DockStyle.Top;
        _pnlTop.Location = new Point(0, 0);
        _pnlTop.Name = "_pnlTop";
        _pnlTop.Size = new Size(968, 90);
        _pnlTop.TabIndex = 0;
        //
        // _log
        //
        _log.BackColor = Color.LightGray;
        _log.DetectUrls = false;
        _log.Dock = DockStyle.Fill;
        _log.Font = new Font("Courier New", 12F);
        _log.Location = new Point(0, 90);
        _log.Name = "_log";
        _log.ReadOnly = true;
        _log.ScrollBars = RichTextBoxScrollBars.Vertical;
        _log.Size = new Size(968, 374);
        _log.TabIndex = 11;
        _log.Text = "";
        //
        // _btnConnect
        //
        _btnConnect.Dock = DockStyle.Fill;
        _btnConnect.FlatStyle = FlatStyle.Flat;
        _btnConnect.Location = new Point(7, 7);
        _btnConnect.Margin = new Padding(7);
        _btnConnect.Name = "_btnConnect";
        _btnConnect.Size = new Size(146, 42);
        _btnConnect.TabIndex = 12;
        _btnConnect.Text = "Connect";
        _btnConnect.Click += BtnConnect_Click;
        //
        // _lblState
        //
        _lblState.Dock = DockStyle.Fill;
        _lblState.Location = new Point(163, 0);
        _lblState.Name = "_lblState";
        _lblState.Size = new Size(345, 56);
        _lblState.TabIndex = 13;
        _lblState.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _btnStopStream
        //
        _btnStopStream.Dock = DockStyle.Fill;
        _btnStopStream.Enabled = false;
        _btnStopStream.FlatStyle = FlatStyle.Flat;
        _btnStopStream.Location = new Point(515, 7);
        _btnStopStream.Margin = new Padding(7);
        _btnStopStream.Name = "_btnStopStream";
        _btnStopStream.Size = new Size(156, 42);
        _btnStopStream.TabIndex = 14;
        _btnStopStream.Text = "Stop stream";
        _btnStopStream.Click += BtnStopStream_Click;
        //
        // _btnClear
        //
        _btnClear.Dock = DockStyle.Fill;
        _btnClear.FlatStyle = FlatStyle.Flat;
        _btnClear.Location = new Point(685, 7);
        _btnClear.Margin = new Padding(7);
        _btnClear.Name = "_btnClear";
        _btnClear.Size = new Size(116, 42);
        _btnClear.TabIndex = 15;
        _btnClear.Text = "Clear";
        _btnClear.Click += BtnClear_Click;
        //
        // _btnFaults
        //
        _btnFaults.BackColor = Color.FromArgb(255, 224, 178);
        _btnFaults.Dock = DockStyle.Fill;
        _btnFaults.FlatStyle = FlatStyle.Flat;
        _btnFaults.Location = new Point(815, 7);
        _btnFaults.Margin = new Padding(7);
        _btnFaults.Name = "_btnFaults";
        _btnFaults.Size = new Size(136, 42);
        _btnFaults.TabIndex = 16;
        _btnFaults.Text = "Сбои...";
        _btnFaults.Click += BtnFaults_Click;
        //
        // _tlpBottom
        //
        _tlpBottom.ColumnCount = 5;
        _tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
        _tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
        _tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
        _tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        _tlpBottom.Controls.Add(_btnConnect, 0, 0);
        _tlpBottom.Controls.Add(_lblState, 1, 0);
        _tlpBottom.Controls.Add(_btnStopStream, 2, 0);
        _tlpBottom.Controls.Add(_btnClear, 3, 0);
        _tlpBottom.Controls.Add(_btnFaults, 4, 0);
        _tlpBottom.Dock = DockStyle.Fill;
        _tlpBottom.Location = new Point(0, 0);
        _tlpBottom.Name = "_tlpBottom";
        _tlpBottom.RowCount = 1;
        _tlpBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpBottom.Size = new Size(968, 56);
        _tlpBottom.TabIndex = 0;
        //
        // _pnlBottom
        //
        _pnlBottom.Controls.Add(_tlpBottom);
        _pnlBottom.Dock = DockStyle.Bottom;
        _pnlBottom.Location = new Point(0, 464);
        _pnlBottom.Name = "_pnlBottom";
        _pnlBottom.Size = new Size(968, 56);
        _pnlBottom.TabIndex = 1;
        //
        // _streamTimer
        //
        _streamTimer.Tick += StreamTimer_Tick;
        //
        // DynamicForm
        //
        Font = new Font("Segoe UI", 12F);
        ClientSize = new Size(968, 520);
        Controls.Add(_log);
        Controls.Add(_pnlBottom);
        Controls.Add(_pnlTop);
        MinimumSize = new Size(900, 470);
        Name = "DynamicForm";
        Text = "Scale Listener - Динамика - COM4  4800/Even";
        ((System.ComponentModel.ISupportInitialize)_numWeight).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numTolerance).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numHz).EndInit();
        _pnlTop.ResumeLayout(false);
        _tlpTop.ResumeLayout(false);
        _pnlBottom.ResumeLayout(false);
        _tlpBottom.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Label _lblWeight;
    private NumericUpDown _numWeight;
    private Label _lblTolerance;
    private NumericUpDown _numTolerance;
    private Label _lblHz;
    private NumericUpDown _numHz;
    private Label _lblScenario;
    private ComboBox _cmbScenario;
    private Label _lblPacketLog;
    private ComboBox _cmbPacketLog;
    private Label _lblCode;
    private Label _lblState;
    private RichTextBox _log;
    private Button _btnConnect;
    private Button _btnStopStream;
    private Button _btnClear;
    private Button _btnFaults;
    private System.Windows.Forms.Timer _streamTimer;
    private Panel _pnlTop;
    private TableLayoutPanel _tlpTop;
    private Panel _pnlBottom;
    private TableLayoutPanel _tlpBottom;
}
