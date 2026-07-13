namespace ScaleListener;

partial class StaticForm
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
        _lblWeight = new Label();
        _numWeight = new NumericUpDown();
        _lblTolerance = new Label();
        _numTolerance = new NumericUpDown();
        _lblCode = new Label();
        _log = new RichTextBox();
        _btnConnect = new Button();
        _btnFaults = new Button();
        _btnClear = new Button();
        ((System.ComponentModel.ISupportInitialize)_numWeight).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numTolerance).BeginInit();
        SuspendLayout();
        //
        // _lblWeight
        //
        _lblWeight.Location = new Point(8, 12);
        _lblWeight.Name = "_lblWeight";
        _lblWeight.Size = new Size(85, 24);
        _lblWeight.TabIndex = 0;
        _lblWeight.Text = "Вес, т:";
        _lblWeight.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _numWeight
        //
        _numWeight.DecimalPlaces = 2;
        _numWeight.Increment = new decimal(new int[] { 10, 0, 0, 131072 });
        _numWeight.Location = new Point(95, 10);
        _numWeight.Maximum = new decimal(new int[] { 140, 0, 0, 0 });
        _numWeight.Name = "_numWeight";
        _numWeight.Size = new Size(100, 24);
        _numWeight.TabIndex = 1;
        _numWeight.ValueChanged += NumWeight_ValueChanged;
        //
        // _lblTolerance
        //
        _lblTolerance.Location = new Point(215, 12);
        _lblTolerance.Name = "_lblTolerance";
        _lblTolerance.Size = new Size(105, 24);
        _lblTolerance.TabIndex = 2;
        _lblTolerance.Text = "Погр., т:";
        _lblTolerance.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _numTolerance
        //
        _numTolerance.DecimalPlaces = 2;
        _numTolerance.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
        _numTolerance.Location = new Point(322, 10);
        _numTolerance.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        _numTolerance.Name = "_numTolerance";
        _numTolerance.Size = new Size(90, 24);
        _numTolerance.TabIndex = 3;
        _numTolerance.Value = new decimal(new int[] { 2, 0, 0, 131072 });
        _numTolerance.ValueChanged += NumTolerance_ValueChanged;
        //
        // _lblCode
        //
        _lblCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _lblCode.Location = new Point(430, 12);
        _lblCode.Name = "_lblCode";
        _lblCode.Size = new Size(292, 24);
        _lblCode.TabIndex = 4;
        _lblCode.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _log
        //
        _log.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _log.BackColor = Color.LightGray;
        _log.DetectUrls = false;
        _log.Font = new Font("Courier New", 12F);
        _log.Location = new Point(8, 46);
        _log.Name = "_log";
        _log.ReadOnly = true;
        _log.ScrollBars = RichTextBoxScrollBars.Vertical;
        _log.Size = new Size(714, 336);
        _log.TabIndex = 5;
        _log.Text = "";
        //
        // _btnConnect
        //
        _btnConnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        _btnConnect.FlatStyle = FlatStyle.Flat;
        _btnConnect.Location = new Point(8, 394);
        _btnConnect.Name = "_btnConnect";
        _btnConnect.Size = new Size(100, 26);
        _btnConnect.TabIndex = 6;
        _btnConnect.Text = "Connect";
        _btnConnect.Click += BtnConnect_Click;
        //
        // _btnFaults
        //
        _btnFaults.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _btnFaults.BackColor = Color.FromArgb(255, 224, 178);
        _btnFaults.FlatStyle = FlatStyle.Flat;
        _btnFaults.Location = new Point(550, 394);
        _btnFaults.Name = "_btnFaults";
        _btnFaults.Size = new Size(90, 26);
        _btnFaults.TabIndex = 7;
        _btnFaults.Text = "Сбои...";
        _btnFaults.Click += BtnFaults_Click;
        //
        // _btnClear
        //
        _btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _btnClear.FlatStyle = FlatStyle.Flat;
        _btnClear.Location = new Point(648, 394);
        _btnClear.Name = "_btnClear";
        _btnClear.Size = new Size(74, 26);
        _btnClear.TabIndex = 8;
        _btnClear.Text = "Clear";
        _btnClear.Click += BtnClear_Click;
        //
        // StaticForm
        //
        Font = new Font("Segoe UI", 12F);
        ClientSize = new Size(730, 430);
        Controls.Add(_lblWeight);
        Controls.Add(_numWeight);
        Controls.Add(_lblTolerance);
        Controls.Add(_numTolerance);
        Controls.Add(_lblCode);
        Controls.Add(_log);
        Controls.Add(_btnConnect);
        Controls.Add(_btnFaults);
        Controls.Add(_btnClear);
        MinimumSize = new Size(650, 340);
        Name = "StaticForm";
        Text = "Scale Listener - Статика - COM4  4800/Even";
        ((System.ComponentModel.ISupportInitialize)_numWeight).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numTolerance).EndInit();
        ResumeLayout(false);
    }

    private Label _lblWeight;
    private NumericUpDown _numWeight;
    private Label _lblTolerance;
    private NumericUpDown _numTolerance;
    private Label _lblCode;
    private RichTextBox _log;
    private Button _btnConnect;
    private Button _btnFaults;
    private Button _btnClear;
}
