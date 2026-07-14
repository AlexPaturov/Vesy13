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
        _pnlTop = new Panel();
        _tlpTop = new TableLayoutPanel();
        _pnlBottom = new Panel();
        _tlpBottom = new TableLayoutPanel();
        ((System.ComponentModel.ISupportInitialize)_numWeight).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numTolerance).BeginInit();
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
        _lblWeight.Size = new Size(82, 48);
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
        _numWeight.Location = new Point(94, 8);
        _numWeight.Margin = new Padding(4, 0, 4, 0);
        _numWeight.Maximum = new decimal(new int[] { 140, 0, 0, 0 });
        _numWeight.Name = "_numWeight";
        _numWeight.Size = new Size(112, 34);
        _numWeight.TabIndex = 1;
        _numWeight.ValueChanged += NumWeight_ValueChanged;
        //
        // _lblTolerance
        //
        _lblTolerance.Dock = DockStyle.Fill;
        _lblTolerance.Location = new Point(214, 1);
        _lblTolerance.Name = "_lblTolerance";
        _lblTolerance.Size = new Size(112, 48);
        _lblTolerance.TabIndex = 2;
        _lblTolerance.Text = "Погр., т";
        _lblTolerance.TextAlign = ContentAlignment.MiddleRight;
        //
        // _numTolerance
        //
        _numTolerance.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numTolerance.DecimalPlaces = 2;
        _numTolerance.Font = new Font("Segoe UI", 12F);
        _numTolerance.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
        _numTolerance.Location = new Point(334, 8);
        _numTolerance.Margin = new Padding(4, 0, 4, 0);
        _numTolerance.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        _numTolerance.Name = "_numTolerance";
        _numTolerance.Size = new Size(102, 34);
        _numTolerance.TabIndex = 3;
        _numTolerance.Value = new decimal(new int[] { 2, 0, 0, 131072 });
        _numTolerance.ValueChanged += NumTolerance_ValueChanged;
        //
        // _lblCode
        //
        _lblCode.Dock = DockStyle.Fill;
        _lblCode.Location = new Point(444, 1);
        _lblCode.Name = "_lblCode";
        _lblCode.Size = new Size(282, 48);
        _lblCode.TabIndex = 4;
        _lblCode.TextAlign = ContentAlignment.MiddleLeft;
        //
        // _tlpTop
        //
        _tlpTop.ColumnCount = 5;
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        _tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpTop.Controls.Add(_lblWeight, 0, 0);
        _tlpTop.Controls.Add(_numWeight, 1, 0);
        _tlpTop.Controls.Add(_lblTolerance, 2, 0);
        _tlpTop.Controls.Add(_numTolerance, 3, 0);
        _tlpTop.Controls.Add(_lblCode, 4, 0);
        _tlpTop.Dock = DockStyle.Fill;
        _tlpTop.Location = new Point(0, 0);
        _tlpTop.Name = "_tlpTop";
        _tlpTop.RowCount = 1;
        _tlpTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpTop.Size = new Size(730, 50);
        _tlpTop.TabIndex = 0;
        //
        // _pnlTop
        //
        _pnlTop.Controls.Add(_tlpTop);
        _pnlTop.Dock = DockStyle.Top;
        _pnlTop.Location = new Point(0, 0);
        _pnlTop.Name = "_pnlTop";
        _pnlTop.Size = new Size(730, 50);
        _pnlTop.TabIndex = 0;
        //
        // _log
        //
        _log.BackColor = Color.LightGray;
        _log.DetectUrls = false;
        _log.Dock = DockStyle.Fill;
        _log.Font = new Font("Courier New", 12F);
        _log.Location = new Point(0, 50);
        _log.Name = "_log";
        _log.ReadOnly = true;
        _log.ScrollBars = RichTextBoxScrollBars.Vertical;
        _log.Size = new Size(730, 324);
        _log.TabIndex = 5;
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
        _btnConnect.TabIndex = 6;
        _btnConnect.Text = "Connect";
        _btnConnect.Click += BtnConnect_Click;
        //
        // _btnFaults
        //
        _btnFaults.BackColor = Color.FromArgb(255, 224, 178);
        _btnFaults.Dock = DockStyle.Fill;
        _btnFaults.FlatStyle = FlatStyle.Flat;
        _btnFaults.Location = new Point(447, 7);
        _btnFaults.Margin = new Padding(7);
        _btnFaults.Name = "_btnFaults";
        _btnFaults.Size = new Size(136, 42);
        _btnFaults.TabIndex = 7;
        _btnFaults.Text = "Сбои...";
        _btnFaults.Click += BtnFaults_Click;
        //
        // _btnClear
        //
        _btnClear.Dock = DockStyle.Fill;
        _btnClear.FlatStyle = FlatStyle.Flat;
        _btnClear.Location = new Point(597, 7);
        _btnClear.Margin = new Padding(7);
        _btnClear.Name = "_btnClear";
        _btnClear.Size = new Size(126, 42);
        _btnClear.TabIndex = 8;
        _btnClear.Text = "Clear";
        _btnClear.Click += BtnClear_Click;
        //
        // _tlpBottom
        //
        _tlpBottom.ColumnCount = 4;
        _tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
        _tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        _tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
        _tlpBottom.Controls.Add(_btnConnect, 0, 0);
        _tlpBottom.Controls.Add(_btnFaults, 2, 0);
        _tlpBottom.Controls.Add(_btnClear, 3, 0);
        _tlpBottom.Dock = DockStyle.Fill;
        _tlpBottom.Location = new Point(0, 0);
        _tlpBottom.Name = "_tlpBottom";
        _tlpBottom.RowCount = 1;
        _tlpBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpBottom.Size = new Size(730, 56);
        _tlpBottom.TabIndex = 0;
        //
        // _pnlBottom
        //
        _pnlBottom.Controls.Add(_tlpBottom);
        _pnlBottom.Dock = DockStyle.Bottom;
        _pnlBottom.Location = new Point(0, 374);
        _pnlBottom.Name = "_pnlBottom";
        _pnlBottom.Size = new Size(730, 56);
        _pnlBottom.TabIndex = 1;
        //
        // StaticForm
        //
        Font = new Font("Segoe UI", 12F);
        ClientSize = new Size(730, 430);
        Controls.Add(_log);
        Controls.Add(_pnlBottom);
        Controls.Add(_pnlTop);
        MinimumSize = new Size(700, 400);
        Name = "StaticForm";
        Text = "Scale Listener - Статика - COM4  4800/Even";
        ((System.ComponentModel.ISupportInitialize)_numWeight).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numTolerance).EndInit();
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
    private Label _lblCode;
    private RichTextBox _log;
    private Button _btnConnect;
    private Button _btnFaults;
    private Button _btnClear;
    private Panel _pnlTop;
    private TableLayoutPanel _tlpTop;
    private Panel _pnlBottom;
    private TableLayoutPanel _tlpBottom;
}
