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
        ((System.ComponentModel.ISupportInitialize)_numWeight).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numTolerance).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numHz).BeginInit();
        SuspendLayout();
        //
        // _lblWeight
        //
        _lblWeight.Location = new Point(8, 12);
        _lblWeight.Name = "_lblWeight";
        _lblWeight.Size = new Size(65, 24);
        _lblWeight.TabIndex = 0;
        _lblWeight.Text = "Вес, т:";
        _lblWeight.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _numWeight
        //
        _numWeight.DecimalPlaces = 2;
        _numWeight.Increment = new decimal(new int[] { 10, 0, 0, 131072 });
        _numWeight.Location = new Point(75, 10);
        _numWeight.Maximum = new decimal(new int[] { 140, 0, 0, 0 });
        _numWeight.Name = "_numWeight";
        _numWeight.Size = new Size(90, 24);
        _numWeight.TabIndex = 1;
        _numWeight.ValueChanged += NumWeight_ValueChanged;
        //
        // _lblTolerance
        //
        _lblTolerance.Location = new Point(178, 12);
        _lblTolerance.Name = "_lblTolerance";
        _lblTolerance.Size = new Size(70, 24);
        _lblTolerance.TabIndex = 2;
        _lblTolerance.Text = "Шум, т:";
        _lblTolerance.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _numTolerance
        //
        _numTolerance.DecimalPlaces = 2;
        _numTolerance.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
        _numTolerance.Location = new Point(250, 10);
        _numTolerance.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        _numTolerance.Name = "_numTolerance";
        _numTolerance.Size = new Size(80, 24);
        _numTolerance.TabIndex = 3;
        _numTolerance.Value = new decimal(new int[] { 2, 0, 0, 131072 });
        _numTolerance.ValueChanged += NumTolerance_ValueChanged;
        //
        // _lblHz
        //
        _lblHz.Location = new Point(342, 12);
        _lblHz.Name = "_lblHz";
        _lblHz.Size = new Size(32, 24);
        _lblHz.TabIndex = 4;
        _lblHz.Text = "Hz:";
        _lblHz.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _numHz
        //
        _numHz.Location = new Point(376, 10);
        _numHz.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        _numHz.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        _numHz.Name = "_numHz";
        _numHz.Size = new Size(64, 24);
        _numHz.TabIndex = 5;
        _numHz.Value = new decimal(new int[] { 47, 0, 0, 0 });
        _numHz.ValueChanged += NumHz_ValueChanged;
        //
        // _lblScenario
        //
        _lblScenario.Location = new Point(452, 12);
        _lblScenario.Name = "_lblScenario";
        _lblScenario.Size = new Size(76, 24);
        _lblScenario.TabIndex = 6;
        _lblScenario.Text = "Сценарий:";
        _lblScenario.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _cmbScenario
        //
        _cmbScenario.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbScenario.Items.AddRange(new object[] { "Постоянная", "Две тележки", "Битые сэмплы" });
        _cmbScenario.Location = new Point(532, 10);
        _cmbScenario.Name = "_cmbScenario";
        _cmbScenario.Size = new Size(150, 24);
        _cmbScenario.TabIndex = 7;
        _cmbScenario.SelectedIndexChanged += CmbScenario_SelectedIndexChanged;
        //
        // _lblPacketLog
        //
        _lblPacketLog.Location = new Point(690, 12);
        _lblPacketLog.Name = "_lblPacketLog";
        _lblPacketLog.Size = new Size(35, 24);
        _lblPacketLog.TabIndex = 8;
        _lblPacketLog.Text = "Лог:";
        _lblPacketLog.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _cmbPacketLog
        //
        _cmbPacketLog.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbPacketLog.Items.AddRange(new object[] { "Все", "Каждый 10-й" });
        _cmbPacketLog.Location = new Point(728, 10);
        _cmbPacketLog.Name = "_cmbPacketLog";
        _cmbPacketLog.Size = new Size(124, 24);
        _cmbPacketLog.TabIndex = 9;
        _cmbPacketLog.SelectedIndexChanged += CmbPacketLog_SelectedIndexChanged;
        //
        // _lblCode
        //
        _lblCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _lblCode.Location = new Point(8, 42);
        _lblCode.Name = "_lblCode";
        _lblCode.Size = new Size(844, 24);
        _lblCode.TabIndex = 10;
        _lblCode.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _lblState
        //
        _lblState.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _lblState.Location = new Point(118, 486);
        _lblState.Name = "_lblState";
        _lblState.Size = new Size(560, 24);
        _lblState.TabIndex = 13;
        _lblState.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _log
        //
        _log.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _log.BackColor = Color.LightGray;
        _log.DetectUrls = false;
        _log.Font = new Font("Courier New", 9.75F);
        _log.Location = new Point(8, 70);
        _log.Name = "_log";
        _log.ReadOnly = true;
        _log.ScrollBars = RichTextBoxScrollBars.Vertical;
        _log.Size = new Size(844, 406);
        _log.TabIndex = 11;
        _log.Text = "";
        //
        // _btnConnect
        //
        _btnConnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        _btnConnect.FlatStyle = FlatStyle.Flat;
        _btnConnect.Location = new Point(8, 486);
        _btnConnect.Name = "_btnConnect";
        _btnConnect.Size = new Size(100, 26);
        _btnConnect.TabIndex = 12;
        _btnConnect.Text = "Connect";
        _btnConnect.Click += BtnConnect_Click;
        //
        // _btnStopStream
        //
        _btnStopStream.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _btnStopStream.Enabled = false;
        _btnStopStream.FlatStyle = FlatStyle.Flat;
        _btnStopStream.Location = new Point(678, 486);
        _btnStopStream.Name = "_btnStopStream";
        _btnStopStream.Size = new Size(96, 26);
        _btnStopStream.TabIndex = 14;
        _btnStopStream.Text = "Stop stream";
        _btnStopStream.Click += BtnStopStream_Click;
        //
        // _btnClear
        //
        _btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _btnClear.FlatStyle = FlatStyle.Flat;
        _btnClear.Location = new Point(786, 486);
        _btnClear.Name = "_btnClear";
        _btnClear.Size = new Size(66, 26);
        _btnClear.TabIndex = 15;
        _btnClear.Text = "Clear";
        _btnClear.Click += BtnClear_Click;
        //
        // _btnFaults
        //
        _btnFaults.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _btnFaults.BackColor = Color.FromArgb(255, 224, 178);
        _btnFaults.FlatStyle = FlatStyle.Flat;
        _btnFaults.Location = new Point(860, 486);
        _btnFaults.Name = "_btnFaults";
        _btnFaults.Size = new Size(96, 26);
        _btnFaults.TabIndex = 16;
        _btnFaults.Text = "Сбои...";
        _btnFaults.Click += BtnFaults_Click;
        //
        // _streamTimer
        //
        _streamTimer.Tick += StreamTimer_Tick;
        //
        // DynamicForm
        //
        ClientSize = new Size(968, 520);
        Controls.Add(_lblWeight);
        Controls.Add(_numWeight);
        Controls.Add(_lblTolerance);
        Controls.Add(_numTolerance);
        Controls.Add(_lblHz);
        Controls.Add(_numHz);
        Controls.Add(_lblScenario);
        Controls.Add(_cmbScenario);
        Controls.Add(_lblPacketLog);
        Controls.Add(_cmbPacketLog);
        Controls.Add(_lblCode);
        Controls.Add(_log);
        Controls.Add(_btnConnect);
        Controls.Add(_lblState);
        Controls.Add(_btnStopStream);
        Controls.Add(_btnClear);
        Controls.Add(_btnFaults);
        MinimumSize = new Size(868, 430);
        Name = "DynamicForm";
        Text = "Scale Listener - Динамика - COM4  4800/Even";
        ((System.ComponentModel.ISupportInitialize)_numWeight).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numTolerance).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numHz).EndInit();
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
}
