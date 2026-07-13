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

        _grpSilence = new GroupBox();
        _lblSilenceMode = new Label();
        _cmbSilenceMode = new ComboBox();
        _lblSilenceActive = new Label();
        _numSilenceActive = new NumericUpDown();
        _lblSilenceGap = new Label();
        _numSilenceGap = new NumericUpDown();
        _btnSilenceManual = new Button();

        _grpSpike = new GroupBox();
        _lblSpikeMode = new Label();
        _cmbSpikeMode = new ComboBox();
        _lblSpikeInterval = new Label();
        _numSpikeInterval = new NumericUpDown();
        _lblSpikeRate = new Label();
        _numSpikeRate = new NumericUpDown();
        _lblSpikeMagnitude = new Label();
        _numSpikeMagnitude = new NumericUpDown();
        _btnSpikeManual = new Button();

        _grpDrift = new GroupBox();
        _lblDriftMode = new Label();
        _cmbDriftMode = new ComboBox();
        _lblDriftActive = new Label();
        _numDriftActive = new NumericUpDown();
        _lblDriftGap = new Label();
        _numDriftGap = new NumericUpDown();
        _lblDriftMagnitude = new Label();
        _numDriftMagnitude = new NumericUpDown();
        _btnDriftManual = new Button();

        _grpCorrupt = new GroupBox();
        _lblCorruptMode = new Label();
        _cmbCorruptMode = new ComboBox();
        _lblCorruptInterval = new Label();
        _numCorruptInterval = new NumericUpDown();
        _lblCorruptRate = new Label();
        _numCorruptRate = new NumericUpDown();
        _lblCorruptMagnitude = new Label();
        _numCorruptMagnitude = new NumericUpDown();
        _btnCorruptManual = new Button();

        _grpStuck = new GroupBox();
        _lblStuckMode = new Label();
        _cmbStuckMode = new ComboBox();
        _lblStuckActive = new Label();
        _numStuckActive = new NumericUpDown();
        _lblStuckGap = new Label();
        _numStuckGap = new NumericUpDown();
        _lblStuckMagnitude = new Label();
        _numStuckMagnitude = new NumericUpDown();
        _btnStuckManual = new Button();

        _history = new FaultHistoryListBox();

        _grpSilence.SuspendLayout();
        _grpSpike.SuspendLayout();
        _grpDrift.SuspendLayout();
        _grpCorrupt.SuspendLayout();
        _grpStuck.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_numSilenceActive).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numSilenceGap).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numSpikeInterval).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numSpikeRate).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numSpikeMagnitude).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftActive).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftGap).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftMagnitude).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numCorruptInterval).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numCorruptRate).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numCorruptMagnitude).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckActive).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckGap).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckMagnitude).BeginInit();
        SuspendLayout();
        //
        // _btnCycle
        //
        _btnCycle.BackColor = Color.White;
        _btnCycle.FlatStyle = FlatStyle.Flat;
        _btnCycle.Location = new Point(11, 11);
        _btnCycle.Name = "_btnCycle";
        _btnCycle.Size = new Size(150, 26);
        _btnCycle.TabIndex = 0;
        _btnCycle.Text = "Старт цикла";
        _btnCycle.UseVisualStyleBackColor = false;
        _btnCycle.Click += BtnCycle_Click;
        //
        // _btnClearHistory
        //
        _btnClearHistory.FlatStyle = FlatStyle.Flat;
        _btnClearHistory.Location = new Point(167, 11);
        _btnClearHistory.Name = "_btnClearHistory";
        _btnClearHistory.Size = new Size(140, 26);
        _btnClearHistory.TabIndex = 1;
        _btnClearHistory.Text = "Очистить историю";
        _btnClearHistory.Click += BtnClearHistory_Click;

        // ── Тишина (Silence) — непрерывный сбой: активность + пауза ──────────
        //
        // _grpSilence
        //
        _grpSilence.BackColor = Color.FromArgb(255, 243, 224);
        _grpSilence.Controls.Add(_lblSilenceMode);
        _grpSilence.Controls.Add(_cmbSilenceMode);
        _grpSilence.Controls.Add(_lblSilenceActive);
        _grpSilence.Controls.Add(_numSilenceActive);
        _grpSilence.Controls.Add(_lblSilenceGap);
        _grpSilence.Controls.Add(_numSilenceGap);
        _grpSilence.Controls.Add(_btnSilenceManual);
        _grpSilence.Location = new Point(11, 47);
        _grpSilence.Name = "_grpSilence";
        _grpSilence.Padding = new Padding(8, 4, 8, 4);
        _grpSilence.Size = new Size(700, 90);
        _grpSilence.TabIndex = 2;
        _grpSilence.TabStop = false;
        _grpSilence.Text = "Тишина (Silence) — нет ответа на poll";
        //
        // _lblSilenceMode
        //
        _lblSilenceMode.Location = new Point(76, 24);
        _lblSilenceMode.Name = "_lblSilenceMode";
        _lblSilenceMode.Size = new Size(48, 20);
        _lblSilenceMode.TabIndex = 1;
        _lblSilenceMode.Text = "Режим:";
        //
        // _cmbSilenceMode
        //
        _cmbSilenceMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbSilenceMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно" });
        _cmbSilenceMode.Location = new Point(126, 21);
        _cmbSilenceMode.Name = "_cmbSilenceMode";
        _cmbSilenceMode.Size = new Size(110, 24);
        _cmbSilenceMode.TabIndex = 2;
        _cmbSilenceMode.SelectedIndexChanged += CmbSilenceMode_SelectedIndexChanged;
        //
        // _btnSilenceManual
        //
        _btnSilenceManual.FlatStyle = FlatStyle.Flat;
        _btnSilenceManual.Location = new Point(530, 20);
        _btnSilenceManual.Name = "_btnSilenceManual";
        _btnSilenceManual.Size = new Size(160, 24);
        _btnSilenceManual.TabIndex = 3;
        _btnSilenceManual.Text = "Вкл/выкл сейчас";
        _btnSilenceManual.Click += BtnSilenceManual_Click;
        //
        // _lblSilenceActive
        //
        _lblSilenceActive.Location = new Point(8, 56);
        _lblSilenceActive.Name = "_lblSilenceActive";
        _lblSilenceActive.Size = new Size(100, 20);
        _lblSilenceActive.TabIndex = 4;
        _lblSilenceActive.Text = "Актив., с:";
        //
        // _numSilenceActive
        //
        _numSilenceActive.DecimalPlaces = 1;
        _numSilenceActive.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numSilenceActive.Location = new Point(112, 54);
        _numSilenceActive.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
        _numSilenceActive.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numSilenceActive.Name = "_numSilenceActive";
        _numSilenceActive.Size = new Size(72, 24);
        _numSilenceActive.TabIndex = 5;
        _numSilenceActive.Value = new decimal(new int[] { 3, 0, 0, 0 });
        _numSilenceActive.ValueChanged += NumSilenceActive_ValueChanged;
        //
        // _lblSilenceGap
        //
        _lblSilenceGap.Location = new Point(196, 56);
        _lblSilenceGap.Name = "_lblSilenceGap";
        _lblSilenceGap.Size = new Size(70, 20);
        _lblSilenceGap.TabIndex = 6;
        _lblSilenceGap.Text = "Пауза, с:";
        //
        // _numSilenceGap
        //
        _numSilenceGap.DecimalPlaces = 1;
        _numSilenceGap.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numSilenceGap.Location = new Point(270, 54);
        _numSilenceGap.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
        _numSilenceGap.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numSilenceGap.Name = "_numSilenceGap";
        _numSilenceGap.Size = new Size(72, 24);
        _numSilenceGap.TabIndex = 7;
        _numSilenceGap.Value = new decimal(new int[] { 15, 0, 0, 0 });
        _numSilenceGap.ValueChanged += NumSilenceGap_ValueChanged;

        // ── Одиночный выброс (Spike) — дискретный сбой: интервал / частота ────
        //
        // _grpSpike
        //
        _grpSpike.BackColor = Color.FromArgb(255, 243, 224);
        _grpSpike.Controls.Add(_lblSpikeMode);
        _grpSpike.Controls.Add(_cmbSpikeMode);
        _grpSpike.Controls.Add(_lblSpikeInterval);
        _grpSpike.Controls.Add(_numSpikeInterval);
        _grpSpike.Controls.Add(_lblSpikeRate);
        _grpSpike.Controls.Add(_numSpikeRate);
        _grpSpike.Controls.Add(_lblSpikeMagnitude);
        _grpSpike.Controls.Add(_numSpikeMagnitude);
        _grpSpike.Controls.Add(_btnSpikeManual);
        _grpSpike.Location = new Point(11, 147);
        _grpSpike.Name = "_grpSpike";
        _grpSpike.Padding = new Padding(8, 4, 8, 4);
        _grpSpike.Size = new Size(700, 90);
        _grpSpike.TabIndex = 3;
        _grpSpike.TabStop = false;
        _grpSpike.Text = "Одиночный выброс (Spike)";
        //
        // _lblSpikeMode
        //
        _lblSpikeMode.Location = new Point(76, 24);
        _lblSpikeMode.Name = "_lblSpikeMode";
        _lblSpikeMode.Size = new Size(48, 20);
        _lblSpikeMode.TabIndex = 1;
        _lblSpikeMode.Text = "Режим:";
        //
        // _cmbSpikeMode
        //
        _cmbSpikeMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbSpikeMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно" });
        _cmbSpikeMode.Location = new Point(126, 21);
        _cmbSpikeMode.Name = "_cmbSpikeMode";
        _cmbSpikeMode.Size = new Size(110, 24);
        _cmbSpikeMode.TabIndex = 2;
        _cmbSpikeMode.SelectedIndexChanged += CmbSpikeMode_SelectedIndexChanged;
        //
        // _btnSpikeManual
        //
        _btnSpikeManual.FlatStyle = FlatStyle.Flat;
        _btnSpikeManual.Location = new Point(530, 20);
        _btnSpikeManual.Name = "_btnSpikeManual";
        _btnSpikeManual.Size = new Size(160, 24);
        _btnSpikeManual.TabIndex = 3;
        _btnSpikeManual.Text = "Сработать сейчас";
        _btnSpikeManual.Click += BtnSpikeManual_Click;
        //
        // _lblSpikeInterval
        //
        _lblSpikeInterval.Location = new Point(8, 56);
        _lblSpikeInterval.Name = "_lblSpikeInterval";
        _lblSpikeInterval.Size = new Size(100, 20);
        _lblSpikeInterval.TabIndex = 4;
        _lblSpikeInterval.Text = "Интервал, с:";
        //
        // _numSpikeInterval
        //
        _numSpikeInterval.DecimalPlaces = 1;
        _numSpikeInterval.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numSpikeInterval.Location = new Point(112, 54);
        _numSpikeInterval.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
        _numSpikeInterval.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numSpikeInterval.Name = "_numSpikeInterval";
        _numSpikeInterval.Size = new Size(72, 24);
        _numSpikeInterval.TabIndex = 5;
        _numSpikeInterval.Value = new decimal(new int[] { 10, 0, 0, 0 });
        _numSpikeInterval.ValueChanged += NumSpikeInterval_ValueChanged;
        //
        // _lblSpikeRate
        //
        _lblSpikeRate.Location = new Point(196, 56);
        _lblSpikeRate.Name = "_lblSpikeRate";
        _lblSpikeRate.Size = new Size(100, 20);
        _lblSpikeRate.TabIndex = 6;
        _lblSpikeRate.Text = "Частота, /мин:";
        //
        // _numSpikeRate
        //
        _numSpikeRate.DecimalPlaces = 1;
        _numSpikeRate.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numSpikeRate.Location = new Point(300, 54);
        _numSpikeRate.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
        _numSpikeRate.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numSpikeRate.Name = "_numSpikeRate";
        _numSpikeRate.Size = new Size(72, 24);
        _numSpikeRate.TabIndex = 7;
        _numSpikeRate.Value = new decimal(new int[] { 3, 0, 0, 0 });
        _numSpikeRate.ValueChanged += NumSpikeRate_ValueChanged;
        //
        // _lblSpikeMagnitude
        //
        _lblSpikeMagnitude.Location = new Point(384, 56);
        _lblSpikeMagnitude.Name = "_lblSpikeMagnitude";
        _lblSpikeMagnitude.Size = new Size(80, 20);
        _lblSpikeMagnitude.TabIndex = 8;
        _lblSpikeMagnitude.Text = "Ампл., т:";
        //
        // _numSpikeMagnitude
        //
        _numSpikeMagnitude.DecimalPlaces = 2;
        _numSpikeMagnitude.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numSpikeMagnitude.Location = new Point(468, 54);
        _numSpikeMagnitude.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        _numSpikeMagnitude.Name = "_numSpikeMagnitude";
        _numSpikeMagnitude.Size = new Size(72, 24);
        _numSpikeMagnitude.TabIndex = 9;
        _numSpikeMagnitude.Value = new decimal(new int[] { 5, 0, 0, 0 });
        _numSpikeMagnitude.ValueChanged += NumSpikeMagnitude_ValueChanged;

        // ── Дрейф/дребезг (Drift) — непрерывный сбой ─────────────────────────
        //
        // _grpDrift
        //
        _grpDrift.BackColor = Color.FromArgb(255, 243, 224);
        _grpDrift.Controls.Add(_lblDriftMode);
        _grpDrift.Controls.Add(_cmbDriftMode);
        _grpDrift.Controls.Add(_lblDriftActive);
        _grpDrift.Controls.Add(_numDriftActive);
        _grpDrift.Controls.Add(_lblDriftGap);
        _grpDrift.Controls.Add(_numDriftGap);
        _grpDrift.Controls.Add(_lblDriftMagnitude);
        _grpDrift.Controls.Add(_numDriftMagnitude);
        _grpDrift.Controls.Add(_btnDriftManual);
        _grpDrift.Location = new Point(11, 247);
        _grpDrift.Name = "_grpDrift";
        _grpDrift.Padding = new Padding(8, 4, 8, 4);
        _grpDrift.Size = new Size(700, 90);
        _grpDrift.TabIndex = 4;
        _grpDrift.TabStop = false;
        _grpDrift.Text = "Дрейф/дребезг (Drift)";
        //
        // _lblDriftMode
        //
        _lblDriftMode.Location = new Point(76, 24);
        _lblDriftMode.Name = "_lblDriftMode";
        _lblDriftMode.Size = new Size(48, 20);
        _lblDriftMode.TabIndex = 1;
        _lblDriftMode.Text = "Режим:";
        //
        // _cmbDriftMode
        //
        _cmbDriftMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbDriftMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно" });
        _cmbDriftMode.Location = new Point(126, 21);
        _cmbDriftMode.Name = "_cmbDriftMode";
        _cmbDriftMode.Size = new Size(110, 24);
        _cmbDriftMode.TabIndex = 2;
        _cmbDriftMode.SelectedIndexChanged += CmbDriftMode_SelectedIndexChanged;
        //
        // _btnDriftManual
        //
        _btnDriftManual.FlatStyle = FlatStyle.Flat;
        _btnDriftManual.Location = new Point(530, 20);
        _btnDriftManual.Name = "_btnDriftManual";
        _btnDriftManual.Size = new Size(160, 24);
        _btnDriftManual.TabIndex = 3;
        _btnDriftManual.Text = "Вкл/выкл сейчас";
        _btnDriftManual.Click += BtnDriftManual_Click;
        //
        // _lblDriftActive
        //
        _lblDriftActive.Location = new Point(8, 56);
        _lblDriftActive.Name = "_lblDriftActive";
        _lblDriftActive.Size = new Size(100, 20);
        _lblDriftActive.TabIndex = 4;
        _lblDriftActive.Text = "Актив., с:";
        //
        // _numDriftActive
        //
        _numDriftActive.DecimalPlaces = 1;
        _numDriftActive.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numDriftActive.Location = new Point(112, 54);
        _numDriftActive.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
        _numDriftActive.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numDriftActive.Name = "_numDriftActive";
        _numDriftActive.Size = new Size(72, 24);
        _numDriftActive.TabIndex = 5;
        _numDriftActive.Value = new decimal(new int[] { 3, 0, 0, 0 });
        _numDriftActive.ValueChanged += NumDriftActive_ValueChanged;
        //
        // _lblDriftGap
        //
        _lblDriftGap.Location = new Point(196, 56);
        _lblDriftGap.Name = "_lblDriftGap";
        _lblDriftGap.Size = new Size(70, 20);
        _lblDriftGap.TabIndex = 6;
        _lblDriftGap.Text = "Пауза, с:";
        //
        // _numDriftGap
        //
        _numDriftGap.DecimalPlaces = 1;
        _numDriftGap.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numDriftGap.Location = new Point(270, 54);
        _numDriftGap.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
        _numDriftGap.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numDriftGap.Name = "_numDriftGap";
        _numDriftGap.Size = new Size(72, 24);
        _numDriftGap.TabIndex = 7;
        _numDriftGap.Value = new decimal(new int[] { 15, 0, 0, 0 });
        _numDriftGap.ValueChanged += NumDriftGap_ValueChanged;
        //
        // _lblDriftMagnitude
        //
        _lblDriftMagnitude.Location = new Point(384, 56);
        _lblDriftMagnitude.Name = "_lblDriftMagnitude";
        _lblDriftMagnitude.Size = new Size(80, 20);
        _lblDriftMagnitude.TabIndex = 8;
        _lblDriftMagnitude.Text = "Ампл., т:";
        //
        // _numDriftMagnitude
        //
        _numDriftMagnitude.DecimalPlaces = 2;
        _numDriftMagnitude.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numDriftMagnitude.Location = new Point(468, 54);
        _numDriftMagnitude.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        _numDriftMagnitude.Name = "_numDriftMagnitude";
        _numDriftMagnitude.Size = new Size(72, 24);
        _numDriftMagnitude.TabIndex = 9;
        _numDriftMagnitude.Value = new decimal(new int[] { 5, 0, 0, 0 });
        _numDriftMagnitude.ValueChanged += NumDriftMagnitude_ValueChanged;

        // ── Порча байт (Corrupt) — дискретный сбой ───────────────────────────
        //
        // _grpCorrupt
        //
        _grpCorrupt.BackColor = Color.FromArgb(255, 243, 224);
        _grpCorrupt.Controls.Add(_lblCorruptMode);
        _grpCorrupt.Controls.Add(_cmbCorruptMode);
        _grpCorrupt.Controls.Add(_lblCorruptInterval);
        _grpCorrupt.Controls.Add(_numCorruptInterval);
        _grpCorrupt.Controls.Add(_lblCorruptRate);
        _grpCorrupt.Controls.Add(_numCorruptRate);
        _grpCorrupt.Controls.Add(_lblCorruptMagnitude);
        _grpCorrupt.Controls.Add(_numCorruptMagnitude);
        _grpCorrupt.Controls.Add(_btnCorruptManual);
        _grpCorrupt.Location = new Point(11, 347);
        _grpCorrupt.Name = "_grpCorrupt";
        _grpCorrupt.Padding = new Padding(8, 4, 8, 4);
        _grpCorrupt.Size = new Size(700, 90);
        _grpCorrupt.TabIndex = 5;
        _grpCorrupt.TabStop = false;
        _grpCorrupt.Text = "Порча байт (Corrupt)";
        //
        // _lblCorruptMode
        //
        _lblCorruptMode.Location = new Point(76, 24);
        _lblCorruptMode.Name = "_lblCorruptMode";
        _lblCorruptMode.Size = new Size(48, 20);
        _lblCorruptMode.TabIndex = 1;
        _lblCorruptMode.Text = "Режим:";
        //
        // _cmbCorruptMode
        //
        _cmbCorruptMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbCorruptMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно" });
        _cmbCorruptMode.Location = new Point(126, 21);
        _cmbCorruptMode.Name = "_cmbCorruptMode";
        _cmbCorruptMode.Size = new Size(110, 24);
        _cmbCorruptMode.TabIndex = 2;
        _cmbCorruptMode.SelectedIndexChanged += CmbCorruptMode_SelectedIndexChanged;
        //
        // _btnCorruptManual
        //
        _btnCorruptManual.FlatStyle = FlatStyle.Flat;
        _btnCorruptManual.Location = new Point(530, 20);
        _btnCorruptManual.Name = "_btnCorruptManual";
        _btnCorruptManual.Size = new Size(160, 24);
        _btnCorruptManual.TabIndex = 3;
        _btnCorruptManual.Text = "Сработать сейчас";
        _btnCorruptManual.Click += BtnCorruptManual_Click;
        //
        // _lblCorruptInterval
        //
        _lblCorruptInterval.Location = new Point(8, 56);
        _lblCorruptInterval.Name = "_lblCorruptInterval";
        _lblCorruptInterval.Size = new Size(100, 20);
        _lblCorruptInterval.TabIndex = 4;
        _lblCorruptInterval.Text = "Интервал, с:";
        //
        // _numCorruptInterval
        //
        _numCorruptInterval.DecimalPlaces = 1;
        _numCorruptInterval.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numCorruptInterval.Location = new Point(112, 54);
        _numCorruptInterval.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
        _numCorruptInterval.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numCorruptInterval.Name = "_numCorruptInterval";
        _numCorruptInterval.Size = new Size(72, 24);
        _numCorruptInterval.TabIndex = 5;
        _numCorruptInterval.Value = new decimal(new int[] { 10, 0, 0, 0 });
        _numCorruptInterval.ValueChanged += NumCorruptInterval_ValueChanged;
        //
        // _lblCorruptRate
        //
        _lblCorruptRate.Location = new Point(196, 56);
        _lblCorruptRate.Name = "_lblCorruptRate";
        _lblCorruptRate.Size = new Size(100, 20);
        _lblCorruptRate.TabIndex = 6;
        _lblCorruptRate.Text = "Частота, /мин:";
        //
        // _numCorruptRate
        //
        _numCorruptRate.DecimalPlaces = 1;
        _numCorruptRate.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numCorruptRate.Location = new Point(300, 54);
        _numCorruptRate.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
        _numCorruptRate.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numCorruptRate.Name = "_numCorruptRate";
        _numCorruptRate.Size = new Size(72, 24);
        _numCorruptRate.TabIndex = 7;
        _numCorruptRate.Value = new decimal(new int[] { 3, 0, 0, 0 });
        _numCorruptRate.ValueChanged += NumCorruptRate_ValueChanged;
        //
        // _lblCorruptMagnitude
        //
        _lblCorruptMagnitude.Location = new Point(384, 56);
        _lblCorruptMagnitude.Name = "_lblCorruptMagnitude";
        _lblCorruptMagnitude.Size = new Size(80, 20);
        _lblCorruptMagnitude.TabIndex = 8;
        _lblCorruptMagnitude.Text = "Байт:";
        //
        // _numCorruptMagnitude
        //
        _numCorruptMagnitude.Increment = new decimal(new int[] { 1, 0, 0, 0 });
        _numCorruptMagnitude.Location = new Point(468, 54);
        _numCorruptMagnitude.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
        _numCorruptMagnitude.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        _numCorruptMagnitude.Name = "_numCorruptMagnitude";
        _numCorruptMagnitude.Size = new Size(72, 24);
        _numCorruptMagnitude.TabIndex = 9;
        _numCorruptMagnitude.Value = new decimal(new int[] { 1, 0, 0, 0 });
        _numCorruptMagnitude.ValueChanged += NumCorruptMagnitude_ValueChanged;

        // ── Застрявший датчик (Stuck) — непрерывный сбой ─────────────────────
        //
        // _grpStuck
        //
        _grpStuck.BackColor = Color.FromArgb(255, 243, 224);
        _grpStuck.Controls.Add(_lblStuckMode);
        _grpStuck.Controls.Add(_cmbStuckMode);
        _grpStuck.Controls.Add(_lblStuckActive);
        _grpStuck.Controls.Add(_numStuckActive);
        _grpStuck.Controls.Add(_lblStuckGap);
        _grpStuck.Controls.Add(_numStuckGap);
        _grpStuck.Controls.Add(_lblStuckMagnitude);
        _grpStuck.Controls.Add(_numStuckMagnitude);
        _grpStuck.Controls.Add(_btnStuckManual);
        _grpStuck.Location = new Point(11, 447);
        _grpStuck.Name = "_grpStuck";
        _grpStuck.Padding = new Padding(8, 4, 8, 4);
        _grpStuck.Size = new Size(700, 90);
        _grpStuck.TabIndex = 6;
        _grpStuck.TabStop = false;
        _grpStuck.Text = "Застрявший датчик (Stuck)";
        //
        // _lblStuckMode
        //
        _lblStuckMode.Location = new Point(76, 24);
        _lblStuckMode.Name = "_lblStuckMode";
        _lblStuckMode.Size = new Size(48, 20);
        _lblStuckMode.TabIndex = 1;
        _lblStuckMode.Text = "Режим:";
        //
        // _cmbStuckMode
        //
        _cmbStuckMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbStuckMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно" });
        _cmbStuckMode.Location = new Point(126, 21);
        _cmbStuckMode.Name = "_cmbStuckMode";
        _cmbStuckMode.Size = new Size(110, 24);
        _cmbStuckMode.TabIndex = 2;
        _cmbStuckMode.SelectedIndexChanged += CmbStuckMode_SelectedIndexChanged;
        //
        // _btnStuckManual
        //
        _btnStuckManual.FlatStyle = FlatStyle.Flat;
        _btnStuckManual.Location = new Point(530, 20);
        _btnStuckManual.Name = "_btnStuckManual";
        _btnStuckManual.Size = new Size(160, 24);
        _btnStuckManual.TabIndex = 3;
        _btnStuckManual.Text = "Вкл/выкл сейчас";
        _btnStuckManual.Click += BtnStuckManual_Click;
        //
        // _lblStuckActive
        //
        _lblStuckActive.Location = new Point(8, 56);
        _lblStuckActive.Name = "_lblStuckActive";
        _lblStuckActive.Size = new Size(100, 20);
        _lblStuckActive.TabIndex = 4;
        _lblStuckActive.Text = "Актив., с:";
        //
        // _numStuckActive
        //
        _numStuckActive.DecimalPlaces = 1;
        _numStuckActive.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numStuckActive.Location = new Point(112, 54);
        _numStuckActive.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
        _numStuckActive.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numStuckActive.Name = "_numStuckActive";
        _numStuckActive.Size = new Size(72, 24);
        _numStuckActive.TabIndex = 5;
        _numStuckActive.Value = new decimal(new int[] { 3, 0, 0, 0 });
        _numStuckActive.ValueChanged += NumStuckActive_ValueChanged;
        //
        // _lblStuckGap
        //
        _lblStuckGap.Location = new Point(196, 56);
        _lblStuckGap.Name = "_lblStuckGap";
        _lblStuckGap.Size = new Size(70, 20);
        _lblStuckGap.TabIndex = 6;
        _lblStuckGap.Text = "Пауза, с:";
        //
        // _numStuckGap
        //
        _numStuckGap.DecimalPlaces = 1;
        _numStuckGap.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numStuckGap.Location = new Point(270, 54);
        _numStuckGap.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
        _numStuckGap.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numStuckGap.Name = "_numStuckGap";
        _numStuckGap.Size = new Size(72, 24);
        _numStuckGap.TabIndex = 7;
        _numStuckGap.Value = new decimal(new int[] { 15, 0, 0, 0 });
        _numStuckGap.ValueChanged += NumStuckGap_ValueChanged;
        //
        // _lblStuckMagnitude
        //
        _lblStuckMagnitude.Location = new Point(384, 56);
        _lblStuckMagnitude.Name = "_lblStuckMagnitude";
        _lblStuckMagnitude.Size = new Size(80, 20);
        _lblStuckMagnitude.TabIndex = 8;
        _lblStuckMagnitude.Text = "Код АЦП:";
        //
        // _numStuckMagnitude
        //
        _numStuckMagnitude.Increment = new decimal(new int[] { 100, 0, 0, 0 });
        _numStuckMagnitude.Location = new Point(468, 54);
        _numStuckMagnitude.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        _numStuckMagnitude.Name = "_numStuckMagnitude";
        _numStuckMagnitude.Size = new Size(72, 24);
        _numStuckMagnitude.TabIndex = 9;
        _numStuckMagnitude.ValueChanged += NumStuckMagnitude_ValueChanged;
        //
        // _history
        //
        _history.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _history.BackColor = Color.White;
        _history.BorderStyle = BorderStyle.FixedSingle;
        _history.DrawMode = DrawMode.OwnerDrawFixed;
        _history.Font = new Font("Courier New", 12F);
        _history.FormattingEnabled = true;
        _history.IntegralHeight = false;
        _history.Location = new Point(11, 550);
        _history.Name = "_history";
        _history.Size = new Size(738, 218);
        _history.TabIndex = 7;
        //
        // StaticFaultForm
        //
        Font = new Font("Segoe UI", 12F);
        BackColor = Color.FromArgb(255, 250, 240);
        ClientSize = new Size(760, 780);
        Controls.Add(_btnCycle);
        Controls.Add(_btnClearHistory);
        Controls.Add(_grpSilence);
        Controls.Add(_grpSpike);
        Controls.Add(_grpDrift);
        Controls.Add(_grpCorrupt);
        Controls.Add(_grpStuck);
        Controls.Add(_history);
        MinimumSize = new Size(760, 700);
        Name = "StaticFaultForm";
        Text = "Сбои — Статика";
        _grpSilence.ResumeLayout(false);
        _grpSpike.ResumeLayout(false);
        _grpDrift.ResumeLayout(false);
        _grpCorrupt.ResumeLayout(false);
        _grpStuck.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_numSilenceActive).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numSilenceGap).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numSpikeInterval).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numSpikeRate).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numSpikeMagnitude).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftActive).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftGap).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftMagnitude).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numCorruptInterval).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numCorruptRate).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numCorruptMagnitude).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckActive).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckGap).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckMagnitude).EndInit();
        ResumeLayout(false);
    }

    private Button _btnCycle;
    private Button _btnClearHistory;

    private GroupBox _grpSilence;
    private Label _lblSilenceMode;
    private ComboBox _cmbSilenceMode;
    private Label _lblSilenceActive;
    private NumericUpDown _numSilenceActive;
    private Label _lblSilenceGap;
    private NumericUpDown _numSilenceGap;
    private Button _btnSilenceManual;

    private GroupBox _grpSpike;
    private Label _lblSpikeMode;
    private ComboBox _cmbSpikeMode;
    private Label _lblSpikeInterval;
    private NumericUpDown _numSpikeInterval;
    private Label _lblSpikeRate;
    private NumericUpDown _numSpikeRate;
    private Label _lblSpikeMagnitude;
    private NumericUpDown _numSpikeMagnitude;
    private Button _btnSpikeManual;

    private GroupBox _grpDrift;
    private Label _lblDriftMode;
    private ComboBox _cmbDriftMode;
    private Label _lblDriftActive;
    private NumericUpDown _numDriftActive;
    private Label _lblDriftGap;
    private NumericUpDown _numDriftGap;
    private Label _lblDriftMagnitude;
    private NumericUpDown _numDriftMagnitude;
    private Button _btnDriftManual;

    private GroupBox _grpCorrupt;
    private Label _lblCorruptMode;
    private ComboBox _cmbCorruptMode;
    private Label _lblCorruptInterval;
    private NumericUpDown _numCorruptInterval;
    private Label _lblCorruptRate;
    private NumericUpDown _numCorruptRate;
    private Label _lblCorruptMagnitude;
    private NumericUpDown _numCorruptMagnitude;
    private Button _btnCorruptManual;

    private GroupBox _grpStuck;
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
}
