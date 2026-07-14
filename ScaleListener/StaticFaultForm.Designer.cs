using ScaleListener.FaultInjection;

namespace ScaleListener;

partial class StaticFaultForm
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
        _btnCycle = new Button();
        _btnClearHistory = new Button();
        _lblSilenceMode = new Label();
        _lblSilenceActive = new Label();
        _numSilenceActive = new NumericUpDown();
        _lblSilenceGap = new Label();
        _numSilenceGap = new NumericUpDown();
        _pnlSpike = new Panel();
        _pnlSpikeBody = new Panel();
        _tlpSpikeParams = new TableLayoutPanel();
        _lblSpikeInterval = new Label();
        _numSpikeInterval = new NumericUpDown();
        _lblSpikeMode = new Label();
        _cmbSpikeMode = new ComboBox();
        _lblSpikeRate = new Label();
        _numSpikeRate = new NumericUpDown();
        _lblSpikeMagnitude = new Label();
        _numSpikeMagnitude = new NumericUpDown();
        _btnSpikeManual = new Button();
        _pnlSpikeHeader = new Panel();
        _tlpSpikeHeader = new TableLayoutPanel();
        _lblSpikeTitle = new Label();
        _pnlDrift = new Panel();
        _pnlDriftBody = new Panel();
        _tlpDriftParams = new TableLayoutPanel();
        _lblDriftActive = new Label();
        _numDriftActive = new NumericUpDown();
        _lblDriftMode = new Label();
        _lblDriftGap = new Label();
        _numDriftGap = new NumericUpDown();
        _lblDriftMagnitude = new Label();
        _numDriftMagnitude = new NumericUpDown();
        _btnDriftManual = new Button();
        _cmbDriftMode = new ComboBox();
        _pnlDriftHeader = new Panel();
        _tlpDriftHeader = new TableLayoutPanel();
        _lblDriftTitle = new Label();
        _pnlCorrupt = new Panel();
        _pnlCorruptBody = new Panel();
        _tlpCorruptParams = new TableLayoutPanel();
        _lblCorruptInterval = new Label();
        _numCorruptInterval = new NumericUpDown();
        _lblCorruptMode = new Label();
        _cmbCorruptMode = new ComboBox();
        _lblCorruptRate = new Label();
        _numCorruptRate = new NumericUpDown();
        _lblCorruptMagnitude = new Label();
        _numCorruptMagnitude = new NumericUpDown();
        _btnCorruptManual = new Button();
        _pnlCorruptHeader = new Panel();
        _tlpCorruptHeader = new TableLayoutPanel();
        _lblCorruptTitle = new Label();
        _pnlStuck = new Panel();
        _pnlStuckBody = new Panel();
        _tlpStuckParams = new TableLayoutPanel();
        _lblStuckActive = new Label();
        _numStuckActive = new NumericUpDown();
        _lblStuckMode = new Label();
        _cmbStuckMode = new ComboBox();
        _lblStuckGap = new Label();
        _numStuckGap = new NumericUpDown();
        _lblStuckMagnitude = new Label();
        _numStuckMagnitude = new NumericUpDown();
        _btnStuckManual = new Button();
        _pnlStuckHeader = new Panel();
        _tlpStuckHeader = new TableLayoutPanel();
        _lblStuckTitle = new Label();
        _history = new FaultHistoryListBox();
        _tlpSilenceParams = new TableLayoutPanel();
        _btnSilenceManual = new Button();
        _cmbSilenceMode = new ComboBox();
        _pnlSilence = new Panel();
        _pnlSilenceBody = new Panel();
        _pnlSilenceHeader = new Panel();
        _tlpSilenceHeader = new TableLayoutPanel();
        _lblSilenceTitle = new Label();
        _pnlMain = new Panel();
        _pnlMiddle = new Panel();
        _tlpContent = new TableLayoutPanel();
        _pnlHistory = new Panel();
        _pnlFaults = new Panel();
        _tlpFaults = new TableLayoutPanel();
        _pnlBottom = new Panel();
        ((System.ComponentModel.ISupportInitialize)_numSilenceActive).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numSilenceGap).BeginInit();
        _pnlSpike.SuspendLayout();
        _pnlSpikeBody.SuspendLayout();
        _tlpSpikeParams.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_numSpikeInterval).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numSpikeRate).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numSpikeMagnitude).BeginInit();
        _pnlSpikeHeader.SuspendLayout();
        _tlpSpikeHeader.SuspendLayout();
        _pnlDrift.SuspendLayout();
        _pnlDriftBody.SuspendLayout();
        _tlpDriftParams.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_numDriftActive).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftGap).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftMagnitude).BeginInit();
        _pnlDriftHeader.SuspendLayout();
        _tlpDriftHeader.SuspendLayout();
        _pnlCorrupt.SuspendLayout();
        _pnlCorruptBody.SuspendLayout();
        _tlpCorruptParams.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_numCorruptInterval).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numCorruptRate).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numCorruptMagnitude).BeginInit();
        _pnlCorruptHeader.SuspendLayout();
        _tlpCorruptHeader.SuspendLayout();
        _pnlStuck.SuspendLayout();
        _pnlStuckBody.SuspendLayout();
        _tlpStuckParams.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_numStuckActive).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckGap).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckMagnitude).BeginInit();
        _pnlStuckHeader.SuspendLayout();
        _tlpStuckHeader.SuspendLayout();
        _tlpSilenceParams.SuspendLayout();
        _pnlSilence.SuspendLayout();
        _pnlSilenceBody.SuspendLayout();
        _pnlSilenceHeader.SuspendLayout();
        _tlpSilenceHeader.SuspendLayout();
        _pnlMain.SuspendLayout();
        _pnlMiddle.SuspendLayout();
        _tlpContent.SuspendLayout();
        _pnlHistory.SuspendLayout();
        _pnlFaults.SuspendLayout();
        _tlpFaults.SuspendLayout();
        _pnlBottom.SuspendLayout();
        SuspendLayout();
        // 
        // _btnCycle
        // 
        _btnCycle.BackColor = Color.White;
        _btnCycle.FlatStyle = FlatStyle.Flat;
        _btnCycle.Location = new Point(9, 6);
        _btnCycle.Name = "_btnCycle";
        _btnCycle.Size = new Size(150, 46);
        _btnCycle.TabIndex = 0;
        _btnCycle.Text = "Старт цикла";
        _btnCycle.UseVisualStyleBackColor = false;
        _btnCycle.Click += BtnCycle_Click;
        // 
        // _btnClearHistory
        // 
        _btnClearHistory.FlatStyle = FlatStyle.Flat;
        _btnClearHistory.Location = new Point(230, 7);
        _btnClearHistory.Name = "_btnClearHistory";
        _btnClearHistory.Size = new Size(217, 46);
        _btnClearHistory.TabIndex = 1;
        _btnClearHistory.Text = "Очистить историю";
        _btnClearHistory.Click += BtnClearHistory_Click;
        // 
        // _lblSilenceMode
        // 
        _lblSilenceMode.Dock = DockStyle.Fill;
        _lblSilenceMode.Location = new Point(215, 1);
        _lblSilenceMode.Name = "_lblSilenceMode";
        _lblSilenceMode.Size = new Size(93, 50);
        _lblSilenceMode.TabIndex = 1;
        _lblSilenceMode.Text = "Режим";
        _lblSilenceMode.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblSilenceActive
        // 
        _lblSilenceActive.Dock = DockStyle.Fill;
        _lblSilenceActive.Location = new Point(4, 1);
        _lblSilenceActive.Name = "_lblSilenceActive";
        _lblSilenceActive.Size = new Size(111, 50);
        _lblSilenceActive.TabIndex = 4;
        _lblSilenceActive.Text = "Актив., с";
        _lblSilenceActive.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _numSilenceActive
        // 
        _numSilenceActive.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numSilenceActive.DecimalPlaces = 1;
        _numSilenceActive.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numSilenceActive.Location = new Point(123, 9);
        _numSilenceActive.Margin = new Padding(4, 0, 4, 0);
        _numSilenceActive.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
        _numSilenceActive.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numSilenceActive.Name = "_numSilenceActive";
        _numSilenceActive.Size = new Size(84, 34);
        _numSilenceActive.TabIndex = 5;
        _numSilenceActive.Value = new decimal(new int[] { 3, 0, 0, 0 });
        _numSilenceActive.ValueChanged += NumSilenceActive_ValueChanged;
        // 
        // _lblSilenceGap
        // 
        _lblSilenceGap.Dock = DockStyle.Fill;
        _lblSilenceGap.Location = new Point(4, 52);
        _lblSilenceGap.Name = "_lblSilenceGap";
        _lblSilenceGap.Size = new Size(111, 50);
        _lblSilenceGap.TabIndex = 6;
        _lblSilenceGap.Text = "Пауза, с";
        _lblSilenceGap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _numSilenceGap
        // 
        _numSilenceGap.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numSilenceGap.DecimalPlaces = 1;
        _numSilenceGap.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numSilenceGap.Location = new Point(123, 60);
        _numSilenceGap.Margin = new Padding(4, 0, 4, 0);
        _numSilenceGap.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
        _numSilenceGap.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numSilenceGap.Name = "_numSilenceGap";
        _numSilenceGap.Size = new Size(84, 34);
        _numSilenceGap.TabIndex = 7;
        _numSilenceGap.Value = new decimal(new int[] { 15, 0, 0, 0 });
        _numSilenceGap.ValueChanged += NumSilenceGap_ValueChanged;
        // 
        // _pnlSpike
        // 
        _pnlSpike.BackColor = Color.FromArgb(192, 192, 255);
        _pnlSpike.Controls.Add(_pnlSpikeBody);
        _pnlSpike.Controls.Add(_pnlSpikeHeader);
        _pnlSpike.Dock = DockStyle.Fill;
        _pnlSpike.Location = new Point(3, 3);
        _pnlSpike.Name = "_pnlSpike";
        _pnlSpike.Size = new Size(514, 180);
        _pnlSpike.TabIndex = 3;
        // 
        // _pnlSpikeBody
        // 
        _pnlSpikeBody.Controls.Add(_tlpSpikeParams);
        _pnlSpikeBody.Dock = DockStyle.Fill;
        _pnlSpikeBody.Location = new Point(0, 35);
        _pnlSpikeBody.Name = "_pnlSpikeBody";
        _pnlSpikeBody.Size = new Size(514, 145);
        _pnlSpikeBody.TabIndex = 1;
        // 
        // _tlpSpikeParams
        // 
        _tlpSpikeParams.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        _tlpSpikeParams.ColumnCount = 4;
        _tlpSpikeParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.96748F));
        _tlpSpikeParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.0894318F));
        _tlpSpikeParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.4495411F));
        _tlpSpikeParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.2660561F));
        _tlpSpikeParams.Controls.Add(_lblSpikeInterval, 0, 0);
        _tlpSpikeParams.Controls.Add(_numSpikeInterval, 1, 0);
        _tlpSpikeParams.Controls.Add(_lblSpikeMode, 2, 0);
        _tlpSpikeParams.Controls.Add(_cmbSpikeMode, 3, 0);
        _tlpSpikeParams.Controls.Add(_lblSpikeRate, 0, 1);
        _tlpSpikeParams.Controls.Add(_numSpikeRate, 1, 1);
        _tlpSpikeParams.Controls.Add(_lblSpikeMagnitude, 0, 2);
        _tlpSpikeParams.Controls.Add(_numSpikeMagnitude, 1, 2);
        _tlpSpikeParams.Controls.Add(_btnSpikeManual, 3, 2);
        _tlpSpikeParams.Dock = DockStyle.Fill;
        _tlpSpikeParams.Location = new Point(0, 0);
        _tlpSpikeParams.Name = "_tlpSpikeParams";
        _tlpSpikeParams.RowCount = 3;
        _tlpSpikeParams.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        _tlpSpikeParams.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        _tlpSpikeParams.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
        _tlpSpikeParams.Size = new Size(514, 145);
        _tlpSpikeParams.TabIndex = 0;
        // 
        // _lblSpikeInterval
        // 
        _lblSpikeInterval.Dock = DockStyle.Fill;
        _lblSpikeInterval.Location = new Point(4, 1);
        _lblSpikeInterval.Name = "_lblSpikeInterval";
        _lblSpikeInterval.Size = new Size(111, 46);
        _lblSpikeInterval.TabIndex = 0;
        _lblSpikeInterval.Text = "Интервал, с";
        _lblSpikeInterval.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _numSpikeInterval
        // 
        _numSpikeInterval.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numSpikeInterval.DecimalPlaces = 1;
        _numSpikeInterval.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numSpikeInterval.Location = new Point(123, 7);
        _numSpikeInterval.Margin = new Padding(4, 0, 4, 0);
        _numSpikeInterval.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
        _numSpikeInterval.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numSpikeInterval.Name = "_numSpikeInterval";
        _numSpikeInterval.Size = new Size(84, 34);
        _numSpikeInterval.TabIndex = 1;
        _numSpikeInterval.Value = new decimal(new int[] { 10, 0, 0, 0 });
        _numSpikeInterval.ValueChanged += NumSpikeInterval_ValueChanged;
        // 
        // _lblSpikeMode
        // 
        _lblSpikeMode.Dock = DockStyle.Fill;
        _lblSpikeMode.Location = new Point(215, 1);
        _lblSpikeMode.Name = "_lblSpikeMode";
        _lblSpikeMode.Size = new Size(93, 46);
        _lblSpikeMode.TabIndex = 2;
        _lblSpikeMode.Text = "Режим";
        _lblSpikeMode.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _cmbSpikeMode
        // 
        _cmbSpikeMode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cmbSpikeMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbSpikeMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно" });
        _cmbSpikeMode.Location = new Point(316, 10);
        _cmbSpikeMode.Margin = new Padding(4, 0, 4, 0);
        _cmbSpikeMode.Name = "_cmbSpikeMode";
        _cmbSpikeMode.Size = new Size(193, 36);
        _cmbSpikeMode.TabIndex = 3;
        _cmbSpikeMode.SelectedIndexChanged += CmbSpikeMode_SelectedIndexChanged;
        // 
        // _lblSpikeRate
        // 
        _lblSpikeRate.Dock = DockStyle.Fill;
        _lblSpikeRate.Location = new Point(4, 48);
        _lblSpikeRate.Name = "_lblSpikeRate";
        _lblSpikeRate.Size = new Size(111, 46);
        _lblSpikeRate.TabIndex = 4;
        _lblSpikeRate.Text = "Частота, /мин";
        _lblSpikeRate.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _numSpikeRate
        // 
        _numSpikeRate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numSpikeRate.DecimalPlaces = 1;
        _numSpikeRate.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numSpikeRate.Location = new Point(123, 54);
        _numSpikeRate.Margin = new Padding(4, 0, 4, 0);
        _numSpikeRate.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
        _numSpikeRate.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numSpikeRate.Name = "_numSpikeRate";
        _numSpikeRate.Size = new Size(84, 34);
        _numSpikeRate.TabIndex = 5;
        _numSpikeRate.Value = new decimal(new int[] { 3, 0, 0, 0 });
        _numSpikeRate.ValueChanged += NumSpikeRate_ValueChanged;
        // 
        // _lblSpikeMagnitude
        // 
        _lblSpikeMagnitude.Dock = DockStyle.Fill;
        _lblSpikeMagnitude.Location = new Point(4, 95);
        _lblSpikeMagnitude.Name = "_lblSpikeMagnitude";
        _lblSpikeMagnitude.Size = new Size(111, 49);
        _lblSpikeMagnitude.TabIndex = 6;
        _lblSpikeMagnitude.Text = "Ампл., т";
        _lblSpikeMagnitude.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _numSpikeMagnitude
        // 
        _numSpikeMagnitude.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numSpikeMagnitude.DecimalPlaces = 2;
        _numSpikeMagnitude.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numSpikeMagnitude.Location = new Point(123, 102);
        _numSpikeMagnitude.Margin = new Padding(4, 0, 4, 0);
        _numSpikeMagnitude.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        _numSpikeMagnitude.Name = "_numSpikeMagnitude";
        _numSpikeMagnitude.Size = new Size(84, 34);
        _numSpikeMagnitude.TabIndex = 7;
        _numSpikeMagnitude.Value = new decimal(new int[] { 5, 0, 0, 0 });
        _numSpikeMagnitude.ValueChanged += NumSpikeMagnitude_ValueChanged;
        // 
        // _btnSpikeManual
        // 
        _btnSpikeManual.Dock = DockStyle.Fill;
        _btnSpikeManual.FlatStyle = FlatStyle.Flat;
        _btnSpikeManual.Location = new Point(315, 98);
        _btnSpikeManual.Name = "_btnSpikeManual";
        _btnSpikeManual.Size = new Size(195, 43);
        _btnSpikeManual.TabIndex = 8;
        _btnSpikeManual.Text = "Сработать сейчас";
        _btnSpikeManual.Click += BtnSpikeManual_Click;
        // 
        // _pnlSpikeHeader
        // 
        _pnlSpikeHeader.BackColor = Color.FromArgb(255, 224, 192);
        _pnlSpikeHeader.Controls.Add(_tlpSpikeHeader);
        _pnlSpikeHeader.Dock = DockStyle.Top;
        _pnlSpikeHeader.Location = new Point(0, 0);
        _pnlSpikeHeader.Name = "_pnlSpikeHeader";
        _pnlSpikeHeader.Size = new Size(514, 35);
        _pnlSpikeHeader.TabIndex = 0;
        // 
        // _tlpSpikeHeader
        // 
        _tlpSpikeHeader.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        _tlpSpikeHeader.ColumnCount = 1;
        _tlpSpikeHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpSpikeHeader.Controls.Add(_lblSpikeTitle, 0, 0);
        _tlpSpikeHeader.Dock = DockStyle.Fill;
        _tlpSpikeHeader.Location = new Point(0, 0);
        _tlpSpikeHeader.Name = "_tlpSpikeHeader";
        _tlpSpikeHeader.RowCount = 1;
        _tlpSpikeHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpSpikeHeader.Size = new Size(514, 35);
        _tlpSpikeHeader.TabIndex = 0;
        // 
        // _lblSpikeTitle
        // 
        _lblSpikeTitle.AutoSize = true;
        _lblSpikeTitle.Dock = DockStyle.Fill;
        _lblSpikeTitle.Location = new Point(4, 1);
        _lblSpikeTitle.Name = "_lblSpikeTitle";
        _lblSpikeTitle.Size = new Size(506, 33);
        _lblSpikeTitle.TabIndex = 0;
        _lblSpikeTitle.Text = "Одиночный выброс (Spike)";
        _lblSpikeTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _pnlDrift
        // 
        _pnlDrift.BackColor = Color.FromArgb(192, 192, 255);
        _pnlDrift.Controls.Add(_pnlDriftBody);
        _pnlDrift.Controls.Add(_pnlDriftHeader);
        _pnlDrift.Dock = DockStyle.Fill;
        _pnlDrift.Location = new Point(3, 189);
        _pnlDrift.Name = "_pnlDrift";
        _pnlDrift.Size = new Size(514, 178);
        _pnlDrift.TabIndex = 4;
        // 
        // _pnlDriftBody
        // 
        _pnlDriftBody.Controls.Add(_tlpDriftParams);
        _pnlDriftBody.Dock = DockStyle.Fill;
        _pnlDriftBody.Location = new Point(0, 35);
        _pnlDriftBody.Name = "_pnlDriftBody";
        _pnlDriftBody.Size = new Size(514, 143);
        _pnlDriftBody.TabIndex = 1;
        // 
        // _tlpDriftParams
        // 
        _tlpDriftParams.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        _tlpDriftParams.ColumnCount = 4;
        _tlpDriftParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.96748F));
        _tlpDriftParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.0894318F));
        _tlpDriftParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.4495411F));
        _tlpDriftParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.2660561F));
        _tlpDriftParams.Controls.Add(_lblDriftActive, 0, 0);
        _tlpDriftParams.Controls.Add(_numDriftActive, 1, 0);
        _tlpDriftParams.Controls.Add(_lblDriftMode, 2, 0);
        _tlpDriftParams.Controls.Add(_lblDriftGap, 0, 1);
        _tlpDriftParams.Controls.Add(_numDriftGap, 1, 1);
        _tlpDriftParams.Controls.Add(_lblDriftMagnitude, 0, 2);
        _tlpDriftParams.Controls.Add(_numDriftMagnitude, 1, 2);
        _tlpDriftParams.Controls.Add(_btnDriftManual, 3, 2);
        _tlpDriftParams.Controls.Add(_cmbDriftMode, 3, 0);
        _tlpDriftParams.Dock = DockStyle.Fill;
        _tlpDriftParams.Location = new Point(0, 0);
        _tlpDriftParams.Name = "_tlpDriftParams";
        _tlpDriftParams.RowCount = 3;
        _tlpDriftParams.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        _tlpDriftParams.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        _tlpDriftParams.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
        _tlpDriftParams.Size = new Size(514, 143);
        _tlpDriftParams.TabIndex = 0;
        // 
        // _lblDriftActive
        // 
        _lblDriftActive.Dock = DockStyle.Fill;
        _lblDriftActive.Location = new Point(4, 1);
        _lblDriftActive.Name = "_lblDriftActive";
        _lblDriftActive.Size = new Size(111, 46);
        _lblDriftActive.TabIndex = 0;
        _lblDriftActive.Text = "Актив., с";
        _lblDriftActive.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _numDriftActive
        // 
        _numDriftActive.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numDriftActive.DecimalPlaces = 1;
        _numDriftActive.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numDriftActive.Location = new Point(123, 7);
        _numDriftActive.Margin = new Padding(4, 0, 4, 0);
        _numDriftActive.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
        _numDriftActive.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numDriftActive.Name = "_numDriftActive";
        _numDriftActive.Size = new Size(84, 34);
        _numDriftActive.TabIndex = 1;
        _numDriftActive.Value = new decimal(new int[] { 3, 0, 0, 0 });
        _numDriftActive.ValueChanged += NumDriftActive_ValueChanged;
        // 
        // _lblDriftMode
        // 
        _lblDriftMode.Dock = DockStyle.Fill;
        _lblDriftMode.Location = new Point(215, 1);
        _lblDriftMode.Name = "_lblDriftMode";
        _lblDriftMode.Size = new Size(93, 46);
        _lblDriftMode.TabIndex = 2;
        _lblDriftMode.Text = "Режим";
        _lblDriftMode.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _lblDriftGap
        // 
        _lblDriftGap.Dock = DockStyle.Fill;
        _lblDriftGap.Location = new Point(4, 48);
        _lblDriftGap.Name = "_lblDriftGap";
        _lblDriftGap.Size = new Size(111, 46);
        _lblDriftGap.TabIndex = 4;
        _lblDriftGap.Text = "Пауза, с";
        _lblDriftGap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _numDriftGap
        // 
        _numDriftGap.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numDriftGap.DecimalPlaces = 1;
        _numDriftGap.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numDriftGap.Location = new Point(123, 54);
        _numDriftGap.Margin = new Padding(4, 0, 4, 0);
        _numDriftGap.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
        _numDriftGap.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numDriftGap.Name = "_numDriftGap";
        _numDriftGap.Size = new Size(84, 34);
        _numDriftGap.TabIndex = 5;
        _numDriftGap.Value = new decimal(new int[] { 15, 0, 0, 0 });
        _numDriftGap.ValueChanged += NumDriftGap_ValueChanged;
        // 
        // _lblDriftMagnitude
        // 
        _lblDriftMagnitude.Dock = DockStyle.Fill;
        _lblDriftMagnitude.Location = new Point(4, 95);
        _lblDriftMagnitude.Name = "_lblDriftMagnitude";
        _lblDriftMagnitude.Size = new Size(111, 47);
        _lblDriftMagnitude.TabIndex = 6;
        _lblDriftMagnitude.Text = "Ампл., т";
        _lblDriftMagnitude.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _numDriftMagnitude
        // 
        _numDriftMagnitude.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numDriftMagnitude.DecimalPlaces = 2;
        _numDriftMagnitude.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numDriftMagnitude.Location = new Point(123, 101);
        _numDriftMagnitude.Margin = new Padding(4, 0, 4, 0);
        _numDriftMagnitude.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        _numDriftMagnitude.Name = "_numDriftMagnitude";
        _numDriftMagnitude.Size = new Size(84, 34);
        _numDriftMagnitude.TabIndex = 7;
        _numDriftMagnitude.Value = new decimal(new int[] { 5, 0, 0, 0 });
        _numDriftMagnitude.ValueChanged += NumDriftMagnitude_ValueChanged;
        // 
        // _btnDriftManual
        // 
        _btnDriftManual.Dock = DockStyle.Fill;
        _btnDriftManual.FlatStyle = FlatStyle.Flat;
        _btnDriftManual.Location = new Point(315, 98);
        _btnDriftManual.Name = "_btnDriftManual";
        _btnDriftManual.Size = new Size(195, 41);
        _btnDriftManual.TabIndex = 8;
        _btnDriftManual.Text = "Вкл/выкл сейчас";
        _btnDriftManual.Click += BtnDriftManual_Click;
        // 
        // _cmbDriftMode
        // 
        _cmbDriftMode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cmbDriftMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbDriftMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно" });
        _cmbDriftMode.Location = new Point(316, 6);
        _cmbDriftMode.Margin = new Padding(4, 0, 4, 0);
        _cmbDriftMode.Name = "_cmbDriftMode";
        _cmbDriftMode.Size = new Size(193, 36);
        _cmbDriftMode.TabIndex = 3;
        _cmbDriftMode.SelectedIndexChanged += CmbDriftMode_SelectedIndexChanged;
        // 
        // _pnlDriftHeader
        // 
        _pnlDriftHeader.BackColor = Color.FromArgb(255, 224, 192);
        _pnlDriftHeader.Controls.Add(_tlpDriftHeader);
        _pnlDriftHeader.Dock = DockStyle.Top;
        _pnlDriftHeader.Location = new Point(0, 0);
        _pnlDriftHeader.Name = "_pnlDriftHeader";
        _pnlDriftHeader.Size = new Size(514, 35);
        _pnlDriftHeader.TabIndex = 0;
        // 
        // _tlpDriftHeader
        // 
        _tlpDriftHeader.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        _tlpDriftHeader.ColumnCount = 1;
        _tlpDriftHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpDriftHeader.Controls.Add(_lblDriftTitle, 0, 0);
        _tlpDriftHeader.Dock = DockStyle.Fill;
        _tlpDriftHeader.Location = new Point(0, 0);
        _tlpDriftHeader.Name = "_tlpDriftHeader";
        _tlpDriftHeader.RowCount = 1;
        _tlpDriftHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpDriftHeader.Size = new Size(514, 35);
        _tlpDriftHeader.TabIndex = 0;
        // 
        // _lblDriftTitle
        // 
        _lblDriftTitle.AutoSize = true;
        _lblDriftTitle.Dock = DockStyle.Fill;
        _lblDriftTitle.Location = new Point(4, 1);
        _lblDriftTitle.Name = "_lblDriftTitle";
        _lblDriftTitle.Size = new Size(506, 33);
        _lblDriftTitle.TabIndex = 0;
        _lblDriftTitle.Text = "Дрейф/дребезг (Drift)";
        _lblDriftTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _pnlCorrupt
        // 
        _pnlCorrupt.BackColor = Color.FromArgb(192, 192, 255);
        _pnlCorrupt.Controls.Add(_pnlCorruptBody);
        _pnlCorrupt.Controls.Add(_pnlCorruptHeader);
        _pnlCorrupt.Dock = DockStyle.Fill;
        _pnlCorrupt.Location = new Point(3, 373);
        _pnlCorrupt.Name = "_pnlCorrupt";
        _pnlCorrupt.Size = new Size(514, 181);
        _pnlCorrupt.TabIndex = 4;
        // 
        // _pnlCorruptBody
        // 
        _pnlCorruptBody.Controls.Add(_tlpCorruptParams);
        _pnlCorruptBody.Dock = DockStyle.Fill;
        _pnlCorruptBody.Location = new Point(0, 35);
        _pnlCorruptBody.Name = "_pnlCorruptBody";
        _pnlCorruptBody.Size = new Size(514, 146);
        _pnlCorruptBody.TabIndex = 1;
        // 
        // _tlpCorruptParams
        // 
        _tlpCorruptParams.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        _tlpCorruptParams.ColumnCount = 4;
        _tlpCorruptParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.96748F));
        _tlpCorruptParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.0894318F));
        _tlpCorruptParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.4495411F));
        _tlpCorruptParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.2660561F));
        _tlpCorruptParams.Controls.Add(_lblCorruptInterval, 0, 0);
        _tlpCorruptParams.Controls.Add(_numCorruptInterval, 1, 0);
        _tlpCorruptParams.Controls.Add(_lblCorruptMode, 2, 0);
        _tlpCorruptParams.Controls.Add(_cmbCorruptMode, 3, 0);
        _tlpCorruptParams.Controls.Add(_lblCorruptRate, 0, 1);
        _tlpCorruptParams.Controls.Add(_numCorruptRate, 1, 1);
        _tlpCorruptParams.Controls.Add(_lblCorruptMagnitude, 0, 2);
        _tlpCorruptParams.Controls.Add(_numCorruptMagnitude, 1, 2);
        _tlpCorruptParams.Controls.Add(_btnCorruptManual, 3, 2);
        _tlpCorruptParams.Dock = DockStyle.Fill;
        _tlpCorruptParams.Location = new Point(0, 0);
        _tlpCorruptParams.Name = "_tlpCorruptParams";
        _tlpCorruptParams.RowCount = 3;
        _tlpCorruptParams.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        _tlpCorruptParams.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        _tlpCorruptParams.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
        _tlpCorruptParams.Size = new Size(514, 146);
        _tlpCorruptParams.TabIndex = 0;
        // 
        // _lblCorruptInterval
        // 
        _lblCorruptInterval.Dock = DockStyle.Fill;
        _lblCorruptInterval.Location = new Point(4, 1);
        _lblCorruptInterval.Name = "_lblCorruptInterval";
        _lblCorruptInterval.Size = new Size(111, 47);
        _lblCorruptInterval.TabIndex = 0;
        _lblCorruptInterval.Text = "Интервал, с";
        _lblCorruptInterval.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _numCorruptInterval
        // 
        _numCorruptInterval.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numCorruptInterval.DecimalPlaces = 1;
        _numCorruptInterval.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numCorruptInterval.Location = new Point(123, 7);
        _numCorruptInterval.Margin = new Padding(4, 0, 4, 0);
        _numCorruptInterval.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
        _numCorruptInterval.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numCorruptInterval.Name = "_numCorruptInterval";
        _numCorruptInterval.Size = new Size(84, 34);
        _numCorruptInterval.TabIndex = 1;
        _numCorruptInterval.Value = new decimal(new int[] { 10, 0, 0, 0 });
        _numCorruptInterval.ValueChanged += NumCorruptInterval_ValueChanged;
        // 
        // _lblCorruptMode
        // 
        _lblCorruptMode.Dock = DockStyle.Fill;
        _lblCorruptMode.Location = new Point(215, 1);
        _lblCorruptMode.Name = "_lblCorruptMode";
        _lblCorruptMode.Size = new Size(93, 47);
        _lblCorruptMode.TabIndex = 2;
        _lblCorruptMode.Text = "Режим";
        _lblCorruptMode.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _cmbCorruptMode
        // 
        _cmbCorruptMode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cmbCorruptMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbCorruptMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно" });
        _cmbCorruptMode.Location = new Point(316, 6);
        _cmbCorruptMode.Margin = new Padding(4, 0, 4, 0);
        _cmbCorruptMode.Name = "_cmbCorruptMode";
        _cmbCorruptMode.Size = new Size(193, 36);
        _cmbCorruptMode.TabIndex = 3;
        _cmbCorruptMode.SelectedIndexChanged += CmbCorruptMode_SelectedIndexChanged;
        // 
        // _lblCorruptRate
        // 
        _lblCorruptRate.Dock = DockStyle.Fill;
        _lblCorruptRate.Location = new Point(4, 49);
        _lblCorruptRate.Name = "_lblCorruptRate";
        _lblCorruptRate.Size = new Size(111, 47);
        _lblCorruptRate.TabIndex = 4;
        _lblCorruptRate.Text = "Частота, /мин";
        _lblCorruptRate.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _numCorruptRate
        // 
        _numCorruptRate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numCorruptRate.DecimalPlaces = 1;
        _numCorruptRate.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numCorruptRate.Location = new Point(123, 55);
        _numCorruptRate.Margin = new Padding(4, 0, 4, 0);
        _numCorruptRate.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
        _numCorruptRate.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numCorruptRate.Name = "_numCorruptRate";
        _numCorruptRate.Size = new Size(84, 34);
        _numCorruptRate.TabIndex = 5;
        _numCorruptRate.Value = new decimal(new int[] { 3, 0, 0, 0 });
        _numCorruptRate.ValueChanged += NumCorruptRate_ValueChanged;
        // 
        // _lblCorruptMagnitude
        // 
        _lblCorruptMagnitude.Dock = DockStyle.Fill;
        _lblCorruptMagnitude.Location = new Point(4, 97);
        _lblCorruptMagnitude.Name = "_lblCorruptMagnitude";
        _lblCorruptMagnitude.Size = new Size(111, 48);
        _lblCorruptMagnitude.TabIndex = 6;
        _lblCorruptMagnitude.Text = "Байт";
        _lblCorruptMagnitude.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _numCorruptMagnitude
        // 
        _numCorruptMagnitude.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numCorruptMagnitude.Location = new Point(123, 104);
        _numCorruptMagnitude.Margin = new Padding(4, 0, 4, 0);
        _numCorruptMagnitude.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
        _numCorruptMagnitude.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        _numCorruptMagnitude.Name = "_numCorruptMagnitude";
        _numCorruptMagnitude.Size = new Size(84, 34);
        _numCorruptMagnitude.TabIndex = 7;
        _numCorruptMagnitude.Value = new decimal(new int[] { 1, 0, 0, 0 });
        _numCorruptMagnitude.ValueChanged += NumCorruptMagnitude_ValueChanged;
        // 
        // _btnCorruptManual
        // 
        _btnCorruptManual.Dock = DockStyle.Fill;
        _btnCorruptManual.FlatStyle = FlatStyle.Flat;
        _btnCorruptManual.Location = new Point(315, 100);
        _btnCorruptManual.Name = "_btnCorruptManual";
        _btnCorruptManual.Size = new Size(195, 42);
        _btnCorruptManual.TabIndex = 8;
        _btnCorruptManual.Text = "Сработать сейчас";
        _btnCorruptManual.Click += BtnCorruptManual_Click;
        // 
        // _pnlCorruptHeader
        // 
        _pnlCorruptHeader.BackColor = Color.FromArgb(255, 224, 192);
        _pnlCorruptHeader.Controls.Add(_tlpCorruptHeader);
        _pnlCorruptHeader.Dock = DockStyle.Top;
        _pnlCorruptHeader.Location = new Point(0, 0);
        _pnlCorruptHeader.Name = "_pnlCorruptHeader";
        _pnlCorruptHeader.Size = new Size(514, 35);
        _pnlCorruptHeader.TabIndex = 0;
        // 
        // _tlpCorruptHeader
        // 
        _tlpCorruptHeader.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        _tlpCorruptHeader.ColumnCount = 1;
        _tlpCorruptHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpCorruptHeader.Controls.Add(_lblCorruptTitle, 0, 0);
        _tlpCorruptHeader.Dock = DockStyle.Fill;
        _tlpCorruptHeader.Location = new Point(0, 0);
        _tlpCorruptHeader.Name = "_tlpCorruptHeader";
        _tlpCorruptHeader.RowCount = 1;
        _tlpCorruptHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpCorruptHeader.Size = new Size(514, 35);
        _tlpCorruptHeader.TabIndex = 0;
        // 
        // _lblCorruptTitle
        // 
        _lblCorruptTitle.AutoSize = true;
        _lblCorruptTitle.Dock = DockStyle.Fill;
        _lblCorruptTitle.Location = new Point(4, 1);
        _lblCorruptTitle.Name = "_lblCorruptTitle";
        _lblCorruptTitle.Size = new Size(506, 33);
        _lblCorruptTitle.TabIndex = 0;
        _lblCorruptTitle.Text = "Порча байт (Corrupt)";
        _lblCorruptTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _pnlStuck
        // 
        _pnlStuck.BackColor = Color.FromArgb(192, 192, 255);
        _pnlStuck.Controls.Add(_pnlStuckBody);
        _pnlStuck.Controls.Add(_pnlStuckHeader);
        _pnlStuck.Dock = DockStyle.Fill;
        _pnlStuck.Location = new Point(3, 560);
        _pnlStuck.Name = "_pnlStuck";
        _pnlStuck.Size = new Size(514, 179);
        _pnlStuck.TabIndex = 4;
        // 
        // _pnlStuckBody
        // 
        _pnlStuckBody.Controls.Add(_tlpStuckParams);
        _pnlStuckBody.Dock = DockStyle.Fill;
        _pnlStuckBody.Location = new Point(0, 35);
        _pnlStuckBody.Name = "_pnlStuckBody";
        _pnlStuckBody.Size = new Size(514, 144);
        _pnlStuckBody.TabIndex = 1;
        // 
        // _tlpStuckParams
        // 
        _tlpStuckParams.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        _tlpStuckParams.ColumnCount = 4;
        _tlpStuckParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.96748F));
        _tlpStuckParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.0894318F));
        _tlpStuckParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.4495411F));
        _tlpStuckParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.2660561F));
        _tlpStuckParams.Controls.Add(_lblStuckActive, 0, 0);
        _tlpStuckParams.Controls.Add(_numStuckActive, 1, 0);
        _tlpStuckParams.Controls.Add(_lblStuckMode, 2, 0);
        _tlpStuckParams.Controls.Add(_cmbStuckMode, 3, 0);
        _tlpStuckParams.Controls.Add(_lblStuckGap, 0, 1);
        _tlpStuckParams.Controls.Add(_numStuckGap, 1, 1);
        _tlpStuckParams.Controls.Add(_lblStuckMagnitude, 0, 2);
        _tlpStuckParams.Controls.Add(_numStuckMagnitude, 1, 2);
        _tlpStuckParams.Controls.Add(_btnStuckManual, 3, 2);
        _tlpStuckParams.Dock = DockStyle.Fill;
        _tlpStuckParams.Location = new Point(0, 0);
        _tlpStuckParams.Name = "_tlpStuckParams";
        _tlpStuckParams.RowCount = 3;
        _tlpStuckParams.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        _tlpStuckParams.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        _tlpStuckParams.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
        _tlpStuckParams.Size = new Size(514, 144);
        _tlpStuckParams.TabIndex = 0;
        // 
        // _lblStuckActive
        // 
        _lblStuckActive.Dock = DockStyle.Fill;
        _lblStuckActive.Location = new Point(4, 1);
        _lblStuckActive.Name = "_lblStuckActive";
        _lblStuckActive.Size = new Size(111, 46);
        _lblStuckActive.TabIndex = 0;
        _lblStuckActive.Text = "Актив., с";
        _lblStuckActive.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _numStuckActive
        // 
        _numStuckActive.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numStuckActive.DecimalPlaces = 1;
        _numStuckActive.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numStuckActive.Location = new Point(123, 7);
        _numStuckActive.Margin = new Padding(4, 0, 4, 0);
        _numStuckActive.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
        _numStuckActive.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numStuckActive.Name = "_numStuckActive";
        _numStuckActive.Size = new Size(84, 34);
        _numStuckActive.TabIndex = 1;
        _numStuckActive.Value = new decimal(new int[] { 3, 0, 0, 0 });
        _numStuckActive.ValueChanged += NumStuckActive_ValueChanged;
        // 
        // _lblStuckMode
        // 
        _lblStuckMode.Dock = DockStyle.Fill;
        _lblStuckMode.Location = new Point(215, 1);
        _lblStuckMode.Name = "_lblStuckMode";
        _lblStuckMode.Size = new Size(93, 46);
        _lblStuckMode.TabIndex = 2;
        _lblStuckMode.Text = "Режим";
        _lblStuckMode.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _cmbStuckMode
        // 
        _cmbStuckMode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cmbStuckMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbStuckMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно" });
        _cmbStuckMode.Location = new Point(316, 6);
        _cmbStuckMode.Margin = new Padding(4, 0, 4, 0);
        _cmbStuckMode.Name = "_cmbStuckMode";
        _cmbStuckMode.Size = new Size(193, 36);
        _cmbStuckMode.TabIndex = 3;
        _cmbStuckMode.SelectedIndexChanged += CmbStuckMode_SelectedIndexChanged;
        // 
        // _lblStuckGap
        // 
        _lblStuckGap.Dock = DockStyle.Fill;
        _lblStuckGap.Location = new Point(4, 48);
        _lblStuckGap.Name = "_lblStuckGap";
        _lblStuckGap.Size = new Size(111, 46);
        _lblStuckGap.TabIndex = 4;
        _lblStuckGap.Text = "Пауза, с";
        _lblStuckGap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _numStuckGap
        // 
        _numStuckGap.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numStuckGap.DecimalPlaces = 1;
        _numStuckGap.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numStuckGap.Location = new Point(123, 54);
        _numStuckGap.Margin = new Padding(4, 0, 4, 0);
        _numStuckGap.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
        _numStuckGap.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numStuckGap.Name = "_numStuckGap";
        _numStuckGap.Size = new Size(84, 34);
        _numStuckGap.TabIndex = 5;
        _numStuckGap.Value = new decimal(new int[] { 15, 0, 0, 0 });
        _numStuckGap.ValueChanged += NumStuckGap_ValueChanged;
        // 
        // _lblStuckMagnitude
        // 
        _lblStuckMagnitude.Dock = DockStyle.Fill;
        _lblStuckMagnitude.Location = new Point(4, 95);
        _lblStuckMagnitude.Name = "_lblStuckMagnitude";
        _lblStuckMagnitude.Size = new Size(111, 48);
        _lblStuckMagnitude.TabIndex = 6;
        _lblStuckMagnitude.Text = "Код АЦП";
        _lblStuckMagnitude.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _numStuckMagnitude
        // 
        _numStuckMagnitude.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _numStuckMagnitude.Increment = new decimal(new int[] { 100, 0, 0, 0 });
        _numStuckMagnitude.Location = new Point(123, 102);
        _numStuckMagnitude.Margin = new Padding(4, 0, 4, 0);
        _numStuckMagnitude.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        _numStuckMagnitude.Name = "_numStuckMagnitude";
        _numStuckMagnitude.Size = new Size(84, 34);
        _numStuckMagnitude.TabIndex = 7;
        _numStuckMagnitude.ValueChanged += NumStuckMagnitude_ValueChanged;
        // 
        // _btnStuckManual
        // 
        _btnStuckManual.Dock = DockStyle.Fill;
        _btnStuckManual.FlatStyle = FlatStyle.Flat;
        _btnStuckManual.Location = new Point(315, 98);
        _btnStuckManual.Name = "_btnStuckManual";
        _btnStuckManual.Size = new Size(195, 42);
        _btnStuckManual.TabIndex = 8;
        _btnStuckManual.Text = "Вкл/выкл сейчас";
        _btnStuckManual.Click += BtnStuckManual_Click;
        // 
        // _pnlStuckHeader
        // 
        _pnlStuckHeader.BackColor = Color.FromArgb(255, 224, 192);
        _pnlStuckHeader.Controls.Add(_tlpStuckHeader);
        _pnlStuckHeader.Dock = DockStyle.Top;
        _pnlStuckHeader.Location = new Point(0, 0);
        _pnlStuckHeader.Name = "_pnlStuckHeader";
        _pnlStuckHeader.Size = new Size(514, 35);
        _pnlStuckHeader.TabIndex = 0;
        // 
        // _tlpStuckHeader
        // 
        _tlpStuckHeader.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        _tlpStuckHeader.ColumnCount = 1;
        _tlpStuckHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpStuckHeader.Controls.Add(_lblStuckTitle, 0, 0);
        _tlpStuckHeader.Dock = DockStyle.Fill;
        _tlpStuckHeader.Location = new Point(0, 0);
        _tlpStuckHeader.Name = "_tlpStuckHeader";
        _tlpStuckHeader.RowCount = 1;
        _tlpStuckHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpStuckHeader.Size = new Size(514, 35);
        _tlpStuckHeader.TabIndex = 0;
        // 
        // _lblStuckTitle
        // 
        _lblStuckTitle.AutoSize = true;
        _lblStuckTitle.Dock = DockStyle.Fill;
        _lblStuckTitle.Location = new Point(4, 1);
        _lblStuckTitle.Name = "_lblStuckTitle";
        _lblStuckTitle.Size = new Size(506, 33);
        _lblStuckTitle.TabIndex = 0;
        _lblStuckTitle.Text = "Застрявший датчик (Stuck)";
        _lblStuckTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _history
        // 
        _history.BackColor = Color.White;
        _history.BorderStyle = BorderStyle.FixedSingle;
        _history.Dock = DockStyle.Fill;
        _history.DrawMode = DrawMode.OwnerDrawFixed;
        _history.Font = new Font("Courier New", 12F);
        _history.FormattingEnabled = true;
        _history.IntegralHeight = false;
        _history.ItemHeight = 25;
        _history.Location = new Point(0, 0);
        _history.Margin = new Padding(4);
        _history.Name = "_history";
        _history.Size = new Size(752, 878);
        _history.TabIndex = 7;
        // 
        // _tlpSilenceParams
        // 
        _tlpSilenceParams.BackColor = Color.FromArgb(192, 255, 255);
        _tlpSilenceParams.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        _tlpSilenceParams.ColumnCount = 4;
        _tlpSilenceParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.96748F));
        _tlpSilenceParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.0894318F));
        _tlpSilenceParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.4495411F));
        _tlpSilenceParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.2660561F));
        _tlpSilenceParams.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        _tlpSilenceParams.Controls.Add(_lblSilenceActive, 0, 0);
        _tlpSilenceParams.Controls.Add(_lblSilenceGap, 0, 1);
        _tlpSilenceParams.Controls.Add(_numSilenceActive, 1, 0);
        _tlpSilenceParams.Controls.Add(_numSilenceGap, 1, 1);
        _tlpSilenceParams.Controls.Add(_lblSilenceMode, 2, 0);
        _tlpSilenceParams.Controls.Add(_btnSilenceManual, 3, 1);
        _tlpSilenceParams.Controls.Add(_cmbSilenceMode, 3, 0);
        _tlpSilenceParams.Dock = DockStyle.Fill;
        _tlpSilenceParams.Location = new Point(0, 0);
        _tlpSilenceParams.Name = "_tlpSilenceParams";
        _tlpSilenceParams.RowCount = 2;
        _tlpSilenceParams.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _tlpSilenceParams.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _tlpSilenceParams.Size = new Size(514, 103);
        _tlpSilenceParams.TabIndex = 8;
        // 
        // _btnSilenceManual
        // 
        _btnSilenceManual.Dock = DockStyle.Fill;
        _btnSilenceManual.FlatStyle = FlatStyle.Flat;
        _btnSilenceManual.Location = new Point(315, 55);
        _btnSilenceManual.Name = "_btnSilenceManual";
        _btnSilenceManual.Size = new Size(195, 44);
        _btnSilenceManual.TabIndex = 3;
        _btnSilenceManual.Text = "Вкл/выкл сейчас";
        _btnSilenceManual.Click += BtnSilenceManual_Click;
        // 
        // _cmbSilenceMode
        // 
        _cmbSilenceMode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cmbSilenceMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbSilenceMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно" });
        _cmbSilenceMode.Location = new Point(316, 12);
        _cmbSilenceMode.Margin = new Padding(4, 0, 4, 0);
        _cmbSilenceMode.Name = "_cmbSilenceMode";
        _cmbSilenceMode.Size = new Size(193, 36);
        _cmbSilenceMode.TabIndex = 2;
        _cmbSilenceMode.SelectedIndexChanged += CmbSilenceMode_SelectedIndexChanged;
        // 
        // _pnlSilence
        // 
        _pnlSilence.BackColor = Color.FromArgb(192, 192, 255);
        _pnlSilence.Controls.Add(_pnlSilenceBody);
        _pnlSilence.Controls.Add(_pnlSilenceHeader);
        _pnlSilence.Dock = DockStyle.Fill;
        _pnlSilence.Location = new Point(3, 745);
        _pnlSilence.Name = "_pnlSilence";
        _pnlSilence.Size = new Size(514, 138);
        _pnlSilence.TabIndex = 9;
        // 
        // _pnlSilenceBody
        // 
        _pnlSilenceBody.Controls.Add(_tlpSilenceParams);
        _pnlSilenceBody.Dock = DockStyle.Fill;
        _pnlSilenceBody.Location = new Point(0, 35);
        _pnlSilenceBody.Name = "_pnlSilenceBody";
        _pnlSilenceBody.Size = new Size(514, 103);
        _pnlSilenceBody.TabIndex = 10;
        // 
        // _pnlSilenceHeader
        // 
        _pnlSilenceHeader.BackColor = Color.FromArgb(255, 224, 192);
        _pnlSilenceHeader.Controls.Add(_tlpSilenceHeader);
        _pnlSilenceHeader.Dock = DockStyle.Top;
        _pnlSilenceHeader.Location = new Point(0, 0);
        _pnlSilenceHeader.Name = "_pnlSilenceHeader";
        _pnlSilenceHeader.Size = new Size(514, 35);
        _pnlSilenceHeader.TabIndex = 9;
        // 
        // _tlpSilenceHeader
        // 
        _tlpSilenceHeader.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        _tlpSilenceHeader.ColumnCount = 1;
        _tlpSilenceHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _tlpSilenceHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _tlpSilenceHeader.Controls.Add(_lblSilenceTitle, 0, 0);
        _tlpSilenceHeader.Dock = DockStyle.Fill;
        _tlpSilenceHeader.Location = new Point(0, 0);
        _tlpSilenceHeader.Name = "_tlpSilenceHeader";
        _tlpSilenceHeader.RowCount = 1;
        _tlpSilenceHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _tlpSilenceHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _tlpSilenceHeader.Size = new Size(514, 35);
        _tlpSilenceHeader.TabIndex = 0;
        // 
        // _lblSilenceTitle
        // 
        _lblSilenceTitle.AutoSize = true;
        _lblSilenceTitle.Dock = DockStyle.Fill;
        _lblSilenceTitle.Location = new Point(4, 1);
        _lblSilenceTitle.Name = "_lblSilenceTitle";
        _lblSilenceTitle.Size = new Size(506, 33);
        _lblSilenceTitle.TabIndex = 0;
        _lblSilenceTitle.Text = "Тишина (Silence) — нет ответа на poll";
        _lblSilenceTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _pnlMain
        // 
        _pnlMain.Controls.Add(_pnlMiddle);
        _pnlMain.Controls.Add(_pnlBottom);
        _pnlMain.Dock = DockStyle.Fill;
        _pnlMain.Location = new Point(0, 0);
        _pnlMain.Name = "_pnlMain";
        _pnlMain.Size = new Size(1292, 953);
        _pnlMain.TabIndex = 10;
        // 
        // _pnlMiddle
        // 
        _pnlMiddle.Controls.Add(_tlpContent);
        _pnlMiddle.Dock = DockStyle.Fill;
        _pnlMiddle.Location = new Point(0, 0);
        _pnlMiddle.Name = "_pnlMiddle";
        _pnlMiddle.Size = new Size(1292, 892);
        _pnlMiddle.TabIndex = 10;
        // 
        // _tlpContent
        // 
        _tlpContent.ColumnCount = 2;
        _tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40.78125F));
        _tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 59.21875F));
        _tlpContent.Controls.Add(_pnlHistory, 1, 0);
        _tlpContent.Controls.Add(_pnlFaults, 0, 0);
        _tlpContent.Dock = DockStyle.Fill;
        _tlpContent.Location = new Point(0, 0);
        _tlpContent.Name = "_tlpContent";
        _tlpContent.RowCount = 1;
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        _tlpContent.Size = new Size(1292, 892);
        _tlpContent.TabIndex = 0;
        // 
        // _pnlHistory
        // 
        _pnlHistory.BorderStyle = BorderStyle.FixedSingle;
        _pnlHistory.Controls.Add(_history);
        _pnlHistory.Dock = DockStyle.Fill;
        _pnlHistory.Location = new Point(532, 6);
        _pnlHistory.Margin = new Padding(6);
        _pnlHistory.Name = "_pnlHistory";
        _pnlHistory.Size = new Size(754, 880);
        _pnlHistory.TabIndex = 0;
        // 
        // _pnlFaults
        // 
        _pnlFaults.Controls.Add(_tlpFaults);
        _pnlFaults.Dock = DockStyle.Fill;
        _pnlFaults.Location = new Point(3, 3);
        _pnlFaults.Name = "_pnlFaults";
        _pnlFaults.Size = new Size(520, 886);
        _pnlFaults.TabIndex = 1;
        // 
        // _tlpFaults
        // 
        _tlpFaults.ColumnCount = 1;
        _tlpFaults.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpFaults.Controls.Add(_pnlSpike, 0, 0);
        _tlpFaults.Controls.Add(_pnlSilence, 0, 4);
        _tlpFaults.Controls.Add(_pnlDrift, 0, 1);
        _tlpFaults.Controls.Add(_pnlStuck, 0, 3);
        _tlpFaults.Controls.Add(_pnlCorrupt, 0, 2);
        _tlpFaults.Dock = DockStyle.Fill;
        _tlpFaults.Location = new Point(0, 0);
        _tlpFaults.Name = "_tlpFaults";
        _tlpFaults.RowCount = 5;
        _tlpFaults.RowStyles.Add(new RowStyle(SizeType.Percent, 21.0285721F));
        _tlpFaults.RowStyles.Add(new RowStyle(SizeType.Percent, 20.8F));
        _tlpFaults.RowStyles.Add(new RowStyle(SizeType.Percent, 21.1428566F));
        _tlpFaults.RowStyles.Add(new RowStyle(SizeType.Percent, 20.9142857F));
        _tlpFaults.RowStyles.Add(new RowStyle(SizeType.Percent, 16.1142864F));
        _tlpFaults.Size = new Size(520, 886);
        _tlpFaults.TabIndex = 0;
        // 
        // _pnlBottom
        // 
        _pnlBottom.Controls.Add(_btnCycle);
        _pnlBottom.Controls.Add(_btnClearHistory);
        _pnlBottom.Dock = DockStyle.Bottom;
        _pnlBottom.Location = new Point(0, 892);
        _pnlBottom.Name = "_pnlBottom";
        _pnlBottom.Size = new Size(1292, 61);
        _pnlBottom.TabIndex = 2;
        // 
        // StaticFaultForm
        // 
        BackColor = Color.FromArgb(255, 250, 240);
        ClientSize = new Size(1292, 953);
        Controls.Add(_pnlMain);
        Font = new Font("Segoe UI", 12F);
        MinimumSize = new Size(760, 700);
        Name = "StaticFaultForm";
        Text = "Сбои — Статика";
        ((System.ComponentModel.ISupportInitialize)_numSilenceActive).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numSilenceGap).EndInit();
        _pnlSpike.ResumeLayout(false);
        _pnlSpikeBody.ResumeLayout(false);
        _tlpSpikeParams.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_numSpikeInterval).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numSpikeRate).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numSpikeMagnitude).EndInit();
        _pnlSpikeHeader.ResumeLayout(false);
        _tlpSpikeHeader.ResumeLayout(false);
        _tlpSpikeHeader.PerformLayout();
        _pnlDrift.ResumeLayout(false);
        _pnlDriftBody.ResumeLayout(false);
        _tlpDriftParams.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_numDriftActive).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftGap).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftMagnitude).EndInit();
        _pnlDriftHeader.ResumeLayout(false);
        _tlpDriftHeader.ResumeLayout(false);
        _tlpDriftHeader.PerformLayout();
        _pnlCorrupt.ResumeLayout(false);
        _pnlCorruptBody.ResumeLayout(false);
        _tlpCorruptParams.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_numCorruptInterval).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numCorruptRate).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numCorruptMagnitude).EndInit();
        _pnlCorruptHeader.ResumeLayout(false);
        _tlpCorruptHeader.ResumeLayout(false);
        _tlpCorruptHeader.PerformLayout();
        _pnlStuck.ResumeLayout(false);
        _pnlStuckBody.ResumeLayout(false);
        _tlpStuckParams.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_numStuckActive).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckGap).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckMagnitude).EndInit();
        _pnlStuckHeader.ResumeLayout(false);
        _tlpStuckHeader.ResumeLayout(false);
        _tlpStuckHeader.PerformLayout();
        _tlpSilenceParams.ResumeLayout(false);
        _pnlSilence.ResumeLayout(false);
        _pnlSilenceBody.ResumeLayout(false);
        _pnlSilenceHeader.ResumeLayout(false);
        _tlpSilenceHeader.ResumeLayout(false);
        _tlpSilenceHeader.PerformLayout();
        _pnlMain.ResumeLayout(false);
        _pnlMiddle.ResumeLayout(false);
        _tlpContent.ResumeLayout(false);
        _pnlHistory.ResumeLayout(false);
        _pnlFaults.ResumeLayout(false);
        _tlpFaults.ResumeLayout(false);
        _pnlBottom.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Button _btnCycle;
    private Button _btnClearHistory;
    private Label _lblSilenceMode;
    private Label _lblSilenceActive;
    private NumericUpDown _numSilenceActive;
    private Label _lblSilenceGap;
    private NumericUpDown _numSilenceGap;

    private Panel _pnlSpike;
    private Panel _pnlSpikeHeader;
    private TableLayoutPanel _tlpSpikeHeader;
    private Label _lblSpikeTitle;
    private Panel _pnlSpikeBody;
    private TableLayoutPanel _tlpSpikeParams;
    private Label _lblSpikeMode;
    private ComboBox _cmbSpikeMode;
    private Label _lblSpikeInterval;
    private NumericUpDown _numSpikeInterval;
    private Label _lblSpikeRate;
    private NumericUpDown _numSpikeRate;
    private Label _lblSpikeMagnitude;
    private NumericUpDown _numSpikeMagnitude;
    private Button _btnSpikeManual;

    private Panel _pnlDrift;
    private Panel _pnlDriftHeader;
    private TableLayoutPanel _tlpDriftHeader;
    private Label _lblDriftTitle;
    private Panel _pnlDriftBody;
    private TableLayoutPanel _tlpDriftParams;
    private Label _lblDriftMode;
    private ComboBox _cmbDriftMode;
    private Label _lblDriftActive;
    private NumericUpDown _numDriftActive;
    private Label _lblDriftGap;
    private NumericUpDown _numDriftGap;
    private Label _lblDriftMagnitude;
    private NumericUpDown _numDriftMagnitude;
    private Button _btnDriftManual;

    private Panel _pnlCorrupt;
    private Panel _pnlCorruptHeader;
    private TableLayoutPanel _tlpCorruptHeader;
    private Label _lblCorruptTitle;
    private Panel _pnlCorruptBody;
    private TableLayoutPanel _tlpCorruptParams;
    private Label _lblCorruptMode;
    private ComboBox _cmbCorruptMode;
    private Label _lblCorruptInterval;
    private NumericUpDown _numCorruptInterval;
    private Label _lblCorruptRate;
    private NumericUpDown _numCorruptRate;
    private Label _lblCorruptMagnitude;
    private NumericUpDown _numCorruptMagnitude;
    private Button _btnCorruptManual;

    private Panel _pnlStuck;
    private Panel _pnlStuckHeader;
    private TableLayoutPanel _tlpStuckHeader;
    private Label _lblStuckTitle;
    private Panel _pnlStuckBody;
    private TableLayoutPanel _tlpStuckParams;
    private Label _lblStuckMode;
    private ComboBox _cmbStuckMode;
    private Label _lblStuckActive;
    private NumericUpDown _numStuckActive;
    private Label _lblStuckGap;
    private NumericUpDown _numStuckGap;
    private Label _lblStuckMagnitude;
    private NumericUpDown _numStuckMagnitude;
    private Button _btnStuckManual;

    private FaultHistoryListBox _history;
    private TableLayoutPanel _tlpSilenceParams;
    private Panel _pnlSilence;
    private Panel _pnlSilenceHeader;
    private TableLayoutPanel _tlpSilenceHeader;
    private Label _lblSilenceTitle;
    private Panel _pnlSilenceBody;
    private ComboBox _cmbSilenceMode;
    private Button _btnSilenceManual;
    private Panel _pnlMain;
    private Panel _pnlHistory;
    private Panel _pnlBottom;
    private Panel _pnlFaults;
    private Panel _pnlMiddle;
    private TableLayoutPanel _tlpContent;
    private TableLayoutPanel _tlpFaults;
}
