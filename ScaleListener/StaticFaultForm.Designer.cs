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
        _chkSilenceEnabled = new CheckBox();
        _lblSilenceMode = new Label();
        _cmbSilenceMode = new ComboBox();
        _lblSilenceParam1 = new Label();
        _numSilenceParam1 = new NumericUpDown();
        _lblSilenceParam2 = new Label();
        _numSilenceParam2 = new NumericUpDown();
        _btnSilenceManual = new Button();

        _grpSpike = new GroupBox();
        _chkSpikeEnabled = new CheckBox();
        _lblSpikeMode = new Label();
        _cmbSpikeMode = new ComboBox();
        _lblSpikeParam1 = new Label();
        _numSpikeParam1 = new NumericUpDown();
        _lblSpikeMagnitude = new Label();
        _numSpikeMagnitude = new NumericUpDown();
        _btnSpikeManual = new Button();

        _grpDrift = new GroupBox();
        _chkDriftEnabled = new CheckBox();
        _lblDriftMode = new Label();
        _cmbDriftMode = new ComboBox();
        _lblDriftParam1 = new Label();
        _numDriftParam1 = new NumericUpDown();
        _lblDriftParam2 = new Label();
        _numDriftParam2 = new NumericUpDown();
        _lblDriftMagnitude = new Label();
        _numDriftMagnitude = new NumericUpDown();
        _btnDriftManual = new Button();

        _grpCorrupt = new GroupBox();
        _chkCorruptEnabled = new CheckBox();
        _lblCorruptMode = new Label();
        _cmbCorruptMode = new ComboBox();
        _lblCorruptParam1 = new Label();
        _numCorruptParam1 = new NumericUpDown();
        _lblCorruptMagnitude = new Label();
        _numCorruptMagnitude = new NumericUpDown();
        _btnCorruptManual = new Button();

        _grpStuck = new GroupBox();
        _chkStuckEnabled = new CheckBox();
        _lblStuckMode = new Label();
        _cmbStuckMode = new ComboBox();
        _lblStuckParam1 = new Label();
        _numStuckParam1 = new NumericUpDown();
        _lblStuckParam2 = new Label();
        _numStuckParam2 = new NumericUpDown();
        _lblStuckMagnitude = new Label();
        _numStuckMagnitude = new NumericUpDown();
        _btnStuckManual = new Button();

        _history = new FaultHistoryListBox();

        _grpSilence.SuspendLayout();
        _grpSpike.SuspendLayout();
        _grpDrift.SuspendLayout();
        _grpCorrupt.SuspendLayout();
        _grpStuck.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_numSilenceParam1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numSilenceParam2).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numSpikeParam1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numSpikeMagnitude).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftParam1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftParam2).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftMagnitude).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numCorruptParam1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numCorruptMagnitude).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckParam1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckParam2).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckMagnitude).BeginInit();
        SuspendLayout();
        //
        // _btnCycle
        //
        _btnCycle.FlatStyle = FlatStyle.Flat;
        _btnCycle.Location = new Point(11, 11);
        _btnCycle.Name = "_btnCycle";
        _btnCycle.Size = new Size(150, 26);
        _btnCycle.TabIndex = 0;
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
        //
        // _grpSilence
        //
        _grpSilence.BackColor = Color.FromArgb(255, 243, 224);
        _grpSilence.Controls.Add(_chkSilenceEnabled);
        _grpSilence.Controls.Add(_lblSilenceMode);
        _grpSilence.Controls.Add(_cmbSilenceMode);
        _grpSilence.Controls.Add(_lblSilenceParam1);
        _grpSilence.Controls.Add(_numSilenceParam1);
        _grpSilence.Controls.Add(_lblSilenceParam2);
        _grpSilence.Controls.Add(_numSilenceParam2);
        _grpSilence.Controls.Add(_btnSilenceManual);
        _grpSilence.Location = new Point(11, 47);
        _grpSilence.Name = "_grpSilence";
        _grpSilence.Padding = new Padding(8, 4, 8, 4);
        _grpSilence.Size = new Size(935, 96);
        _grpSilence.TabIndex = 2;
        _grpSilence.TabStop = false;
        _grpSilence.Text = "Тишина (Silence) — нет ответа на poll";
        //
        // _chkSilenceEnabled
        //
        _chkSilenceEnabled.Location = new Point(8, 20);
        _chkSilenceEnabled.Name = "_chkSilenceEnabled";
        _chkSilenceEnabled.Size = new Size(60, 22);
        _chkSilenceEnabled.TabIndex = 0;
        _chkSilenceEnabled.Text = "Вкл";
        //
        // _lblSilenceMode
        //
        _lblSilenceMode.Location = new Point(76, 22);
        _lblSilenceMode.Name = "_lblSilenceMode";
        _lblSilenceMode.Size = new Size(48, 20);
        _lblSilenceMode.TabIndex = 1;
        _lblSilenceMode.Text = "Режим:";
        //
        // _cmbSilenceMode
        //
        _cmbSilenceMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbSilenceMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно", "Вручную" });
        _cmbSilenceMode.Location = new Point(126, 19);
        _cmbSilenceMode.Name = "_cmbSilenceMode";
        _cmbSilenceMode.Size = new Size(110, 22);
        _cmbSilenceMode.TabIndex = 2;
        //
        // _lblSilenceParam1
        //
        _lblSilenceParam1.Location = new Point(8, 48);
        _lblSilenceParam1.Name = "_lblSilenceParam1";
        _lblSilenceParam1.Size = new Size(100, 20);
        _lblSilenceParam1.TabIndex = 3;
        //
        // _numSilenceParam1
        //
        _numSilenceParam1.DecimalPlaces = 1;
        _numSilenceParam1.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numSilenceParam1.Location = new Point(112, 46);
        _numSilenceParam1.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numSilenceParam1.Name = "_numSilenceParam1";
        _numSilenceParam1.Size = new Size(72, 22);
        _numSilenceParam1.TabIndex = 4;
        //
        // _lblSilenceParam2
        //
        _lblSilenceParam2.Location = new Point(192, 48);
        _lblSilenceParam2.Name = "_lblSilenceParam2";
        _lblSilenceParam2.Size = new Size(64, 20);
        _lblSilenceParam2.TabIndex = 5;
        //
        // _numSilenceParam2
        //
        _numSilenceParam2.DecimalPlaces = 1;
        _numSilenceParam2.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numSilenceParam2.Location = new Point(256, 46);
        _numSilenceParam2.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numSilenceParam2.Name = "_numSilenceParam2";
        _numSilenceParam2.Size = new Size(72, 22);
        _numSilenceParam2.TabIndex = 6;
        //
        // _btnSilenceManual
        //
        _btnSilenceManual.FlatStyle = FlatStyle.Flat;
        _btnSilenceManual.Location = new Point(8, 72);
        _btnSilenceManual.Name = "_btnSilenceManual";
        _btnSilenceManual.Size = new Size(150, 22);
        _btnSilenceManual.TabIndex = 7;
        _btnSilenceManual.Text = "Сработать сейчас";
        //
        // _grpSpike
        //
        _grpSpike.BackColor = Color.FromArgb(255, 243, 224);
        _grpSpike.Controls.Add(_chkSpikeEnabled);
        _grpSpike.Controls.Add(_lblSpikeMode);
        _grpSpike.Controls.Add(_cmbSpikeMode);
        _grpSpike.Controls.Add(_lblSpikeParam1);
        _grpSpike.Controls.Add(_numSpikeParam1);
        _grpSpike.Controls.Add(_lblSpikeMagnitude);
        _grpSpike.Controls.Add(_numSpikeMagnitude);
        _grpSpike.Controls.Add(_btnSpikeManual);
        _grpSpike.Location = new Point(11, 149);
        _grpSpike.Name = "_grpSpike";
        _grpSpike.Padding = new Padding(8, 4, 8, 4);
        _grpSpike.Size = new Size(536, 96);
        _grpSpike.TabIndex = 3;
        _grpSpike.TabStop = false;
        _grpSpike.Text = "Одиночный выброс (Spike)";
        //
        // _chkSpikeEnabled
        //
        _chkSpikeEnabled.Location = new Point(8, 20);
        _chkSpikeEnabled.Name = "_chkSpikeEnabled";
        _chkSpikeEnabled.Size = new Size(60, 22);
        _chkSpikeEnabled.TabIndex = 0;
        _chkSpikeEnabled.Text = "Вкл";
        //
        // _lblSpikeMode
        //
        _lblSpikeMode.Location = new Point(76, 22);
        _lblSpikeMode.Name = "_lblSpikeMode";
        _lblSpikeMode.Size = new Size(48, 20);
        _lblSpikeMode.TabIndex = 1;
        _lblSpikeMode.Text = "Режим:";
        //
        // _cmbSpikeMode
        //
        _cmbSpikeMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbSpikeMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно", "Вручную" });
        _cmbSpikeMode.Location = new Point(126, 19);
        _cmbSpikeMode.Name = "_cmbSpikeMode";
        _cmbSpikeMode.Size = new Size(110, 22);
        _cmbSpikeMode.TabIndex = 2;
        //
        // _lblSpikeParam1
        //
        _lblSpikeParam1.Location = new Point(8, 48);
        _lblSpikeParam1.Name = "_lblSpikeParam1";
        _lblSpikeParam1.Size = new Size(100, 20);
        _lblSpikeParam1.TabIndex = 3;
        //
        // _numSpikeParam1
        //
        _numSpikeParam1.DecimalPlaces = 1;
        _numSpikeParam1.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numSpikeParam1.Location = new Point(112, 46);
        _numSpikeParam1.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numSpikeParam1.Name = "_numSpikeParam1";
        _numSpikeParam1.Size = new Size(72, 22);
        _numSpikeParam1.TabIndex = 4;
        //
        // _lblSpikeMagnitude
        //
        _lblSpikeMagnitude.Location = new Point(336, 48);
        _lblSpikeMagnitude.Name = "_lblSpikeMagnitude";
        _lblSpikeMagnitude.Size = new Size(90, 20);
        _lblSpikeMagnitude.TabIndex = 5;
        _lblSpikeMagnitude.Text = "Ампл., т";
        //
        // _numSpikeMagnitude
        //
        _numSpikeMagnitude.DecimalPlaces = 2;
        _numSpikeMagnitude.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numSpikeMagnitude.Location = new Point(430, 46);
        _numSpikeMagnitude.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        _numSpikeMagnitude.Name = "_numSpikeMagnitude";
        _numSpikeMagnitude.Size = new Size(72, 22);
        _numSpikeMagnitude.TabIndex = 6;
        //
        // _btnSpikeManual
        //
        _btnSpikeManual.FlatStyle = FlatStyle.Flat;
        _btnSpikeManual.Location = new Point(8, 72);
        _btnSpikeManual.Name = "_btnSpikeManual";
        _btnSpikeManual.Size = new Size(150, 22);
        _btnSpikeManual.TabIndex = 7;
        _btnSpikeManual.Text = "Сработать сейчас";
        //
        // _grpDrift
        //
        _grpDrift.BackColor = Color.FromArgb(255, 243, 224);
        _grpDrift.Controls.Add(_chkDriftEnabled);
        _grpDrift.Controls.Add(_lblDriftMode);
        _grpDrift.Controls.Add(_cmbDriftMode);
        _grpDrift.Controls.Add(_lblDriftParam1);
        _grpDrift.Controls.Add(_numDriftParam1);
        _grpDrift.Controls.Add(_lblDriftParam2);
        _grpDrift.Controls.Add(_numDriftParam2);
        _grpDrift.Controls.Add(_lblDriftMagnitude);
        _grpDrift.Controls.Add(_numDriftMagnitude);
        _grpDrift.Controls.Add(_btnDriftManual);
        _grpDrift.Location = new Point(11, 251);
        _grpDrift.Name = "_grpDrift";
        _grpDrift.Padding = new Padding(8, 4, 8, 4);
        _grpDrift.Size = new Size(536, 96);
        _grpDrift.TabIndex = 4;
        _grpDrift.TabStop = false;
        _grpDrift.Text = "Дрейф/дребезг (Drift)";
        //
        // _chkDriftEnabled
        //
        _chkDriftEnabled.Location = new Point(8, 20);
        _chkDriftEnabled.Name = "_chkDriftEnabled";
        _chkDriftEnabled.Size = new Size(60, 22);
        _chkDriftEnabled.TabIndex = 0;
        _chkDriftEnabled.Text = "Вкл";
        //
        // _lblDriftMode
        //
        _lblDriftMode.Location = new Point(76, 22);
        _lblDriftMode.Name = "_lblDriftMode";
        _lblDriftMode.Size = new Size(48, 20);
        _lblDriftMode.TabIndex = 1;
        _lblDriftMode.Text = "Режим:";
        //
        // _cmbDriftMode
        //
        _cmbDriftMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbDriftMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно", "Вручную" });
        _cmbDriftMode.Location = new Point(126, 19);
        _cmbDriftMode.Name = "_cmbDriftMode";
        _cmbDriftMode.Size = new Size(110, 22);
        _cmbDriftMode.TabIndex = 2;
        //
        // _lblDriftParam1
        //
        _lblDriftParam1.Location = new Point(8, 48);
        _lblDriftParam1.Name = "_lblDriftParam1";
        _lblDriftParam1.Size = new Size(100, 20);
        _lblDriftParam1.TabIndex = 3;
        //
        // _numDriftParam1
        //
        _numDriftParam1.DecimalPlaces = 1;
        _numDriftParam1.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numDriftParam1.Location = new Point(112, 46);
        _numDriftParam1.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numDriftParam1.Name = "_numDriftParam1";
        _numDriftParam1.Size = new Size(72, 22);
        _numDriftParam1.TabIndex = 4;
        //
        // _lblDriftParam2
        //
        _lblDriftParam2.Location = new Point(192, 48);
        _lblDriftParam2.Name = "_lblDriftParam2";
        _lblDriftParam2.Size = new Size(64, 20);
        _lblDriftParam2.TabIndex = 5;
        //
        // _numDriftParam2
        //
        _numDriftParam2.DecimalPlaces = 1;
        _numDriftParam2.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numDriftParam2.Location = new Point(256, 46);
        _numDriftParam2.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numDriftParam2.Name = "_numDriftParam2";
        _numDriftParam2.Size = new Size(72, 22);
        _numDriftParam2.TabIndex = 6;
        //
        // _lblDriftMagnitude
        //
        _lblDriftMagnitude.Location = new Point(336, 48);
        _lblDriftMagnitude.Name = "_lblDriftMagnitude";
        _lblDriftMagnitude.Size = new Size(90, 20);
        _lblDriftMagnitude.TabIndex = 7;
        _lblDriftMagnitude.Text = "Ампл., т";
        //
        // _numDriftMagnitude
        //
        _numDriftMagnitude.DecimalPlaces = 2;
        _numDriftMagnitude.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numDriftMagnitude.Location = new Point(430, 46);
        _numDriftMagnitude.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        _numDriftMagnitude.Name = "_numDriftMagnitude";
        _numDriftMagnitude.Size = new Size(72, 22);
        _numDriftMagnitude.TabIndex = 8;
        //
        // _btnDriftManual
        //
        _btnDriftManual.FlatStyle = FlatStyle.Flat;
        _btnDriftManual.Location = new Point(8, 72);
        _btnDriftManual.Name = "_btnDriftManual";
        _btnDriftManual.Size = new Size(150, 22);
        _btnDriftManual.TabIndex = 9;
        _btnDriftManual.Text = "Сработать сейчас";
        //
        // _grpCorrupt
        //
        _grpCorrupt.BackColor = Color.FromArgb(255, 243, 224);
        _grpCorrupt.Controls.Add(_chkCorruptEnabled);
        _grpCorrupt.Controls.Add(_lblCorruptMode);
        _grpCorrupt.Controls.Add(_cmbCorruptMode);
        _grpCorrupt.Controls.Add(_lblCorruptParam1);
        _grpCorrupt.Controls.Add(_numCorruptParam1);
        _grpCorrupt.Controls.Add(_lblCorruptMagnitude);
        _grpCorrupt.Controls.Add(_numCorruptMagnitude);
        _grpCorrupt.Controls.Add(_btnCorruptManual);
        _grpCorrupt.Location = new Point(11, 353);
        _grpCorrupt.Name = "_grpCorrupt";
        _grpCorrupt.Padding = new Padding(8, 4, 8, 4);
        _grpCorrupt.Size = new Size(536, 96);
        _grpCorrupt.TabIndex = 5;
        _grpCorrupt.TabStop = false;
        _grpCorrupt.Text = "Порча байт (Corrupt)";
        //
        // _chkCorruptEnabled
        //
        _chkCorruptEnabled.Location = new Point(8, 20);
        _chkCorruptEnabled.Name = "_chkCorruptEnabled";
        _chkCorruptEnabled.Size = new Size(60, 22);
        _chkCorruptEnabled.TabIndex = 0;
        _chkCorruptEnabled.Text = "Вкл";
        //
        // _lblCorruptMode
        //
        _lblCorruptMode.Location = new Point(76, 22);
        _lblCorruptMode.Name = "_lblCorruptMode";
        _lblCorruptMode.Size = new Size(48, 20);
        _lblCorruptMode.TabIndex = 1;
        _lblCorruptMode.Text = "Режим:";
        //
        // _cmbCorruptMode
        //
        _cmbCorruptMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbCorruptMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно", "Вручную" });
        _cmbCorruptMode.Location = new Point(126, 19);
        _cmbCorruptMode.Name = "_cmbCorruptMode";
        _cmbCorruptMode.Size = new Size(110, 22);
        _cmbCorruptMode.TabIndex = 2;
        //
        // _lblCorruptParam1
        //
        _lblCorruptParam1.Location = new Point(8, 48);
        _lblCorruptParam1.Name = "_lblCorruptParam1";
        _lblCorruptParam1.Size = new Size(100, 20);
        _lblCorruptParam1.TabIndex = 3;
        //
        // _numCorruptParam1
        //
        _numCorruptParam1.DecimalPlaces = 1;
        _numCorruptParam1.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numCorruptParam1.Location = new Point(112, 46);
        _numCorruptParam1.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numCorruptParam1.Name = "_numCorruptParam1";
        _numCorruptParam1.Size = new Size(72, 22);
        _numCorruptParam1.TabIndex = 4;
        //
        // _lblCorruptMagnitude
        //
        _lblCorruptMagnitude.Location = new Point(336, 48);
        _lblCorruptMagnitude.Name = "_lblCorruptMagnitude";
        _lblCorruptMagnitude.Size = new Size(90, 20);
        _lblCorruptMagnitude.TabIndex = 5;
        _lblCorruptMagnitude.Text = "Байт мусора";
        //
        // _numCorruptMagnitude
        //
        _numCorruptMagnitude.DecimalPlaces = 2;
        _numCorruptMagnitude.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numCorruptMagnitude.Location = new Point(430, 46);
        _numCorruptMagnitude.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        _numCorruptMagnitude.Name = "_numCorruptMagnitude";
        _numCorruptMagnitude.Size = new Size(72, 22);
        _numCorruptMagnitude.TabIndex = 6;
        //
        // _btnCorruptManual
        //
        _btnCorruptManual.FlatStyle = FlatStyle.Flat;
        _btnCorruptManual.Location = new Point(8, 72);
        _btnCorruptManual.Name = "_btnCorruptManual";
        _btnCorruptManual.Size = new Size(150, 22);
        _btnCorruptManual.TabIndex = 7;
        _btnCorruptManual.Text = "Сработать сейчас";
        //
        // _grpStuck
        //
        _grpStuck.BackColor = Color.FromArgb(255, 243, 224);
        _grpStuck.Controls.Add(_chkStuckEnabled);
        _grpStuck.Controls.Add(_lblStuckMode);
        _grpStuck.Controls.Add(_cmbStuckMode);
        _grpStuck.Controls.Add(_lblStuckParam1);
        _grpStuck.Controls.Add(_numStuckParam1);
        _grpStuck.Controls.Add(_lblStuckParam2);
        _grpStuck.Controls.Add(_numStuckParam2);
        _grpStuck.Controls.Add(_lblStuckMagnitude);
        _grpStuck.Controls.Add(_numStuckMagnitude);
        _grpStuck.Controls.Add(_btnStuckManual);
        _grpStuck.Location = new Point(11, 455);
        _grpStuck.Name = "_grpStuck";
        _grpStuck.Padding = new Padding(8, 4, 8, 4);
        _grpStuck.Size = new Size(536, 96);
        _grpStuck.TabIndex = 6;
        _grpStuck.TabStop = false;
        _grpStuck.Text = "Застрявший датчик (Stuck)";
        //
        // _chkStuckEnabled
        //
        _chkStuckEnabled.Location = new Point(8, 20);
        _chkStuckEnabled.Name = "_chkStuckEnabled";
        _chkStuckEnabled.Size = new Size(60, 22);
        _chkStuckEnabled.TabIndex = 0;
        _chkStuckEnabled.Text = "Вкл";
        //
        // _lblStuckMode
        //
        _lblStuckMode.Location = new Point(76, 22);
        _lblStuckMode.Name = "_lblStuckMode";
        _lblStuckMode.Size = new Size(48, 20);
        _lblStuckMode.TabIndex = 1;
        _lblStuckMode.Text = "Режим:";
        //
        // _cmbStuckMode
        //
        _cmbStuckMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbStuckMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно", "Вручную" });
        _cmbStuckMode.Location = new Point(126, 19);
        _cmbStuckMode.Name = "_cmbStuckMode";
        _cmbStuckMode.Size = new Size(110, 22);
        _cmbStuckMode.TabIndex = 2;
        //
        // _lblStuckParam1
        //
        _lblStuckParam1.Location = new Point(8, 48);
        _lblStuckParam1.Name = "_lblStuckParam1";
        _lblStuckParam1.Size = new Size(100, 20);
        _lblStuckParam1.TabIndex = 3;
        //
        // _numStuckParam1
        //
        _numStuckParam1.DecimalPlaces = 1;
        _numStuckParam1.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numStuckParam1.Location = new Point(112, 46);
        _numStuckParam1.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numStuckParam1.Name = "_numStuckParam1";
        _numStuckParam1.Size = new Size(72, 22);
        _numStuckParam1.TabIndex = 4;
        //
        // _lblStuckParam2
        //
        _lblStuckParam2.Location = new Point(192, 48);
        _lblStuckParam2.Name = "_lblStuckParam2";
        _lblStuckParam2.Size = new Size(64, 20);
        _lblStuckParam2.TabIndex = 5;
        //
        // _numStuckParam2
        //
        _numStuckParam2.DecimalPlaces = 1;
        _numStuckParam2.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numStuckParam2.Location = new Point(256, 46);
        _numStuckParam2.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numStuckParam2.Name = "_numStuckParam2";
        _numStuckParam2.Size = new Size(72, 22);
        _numStuckParam2.TabIndex = 6;
        //
        // _lblStuckMagnitude
        //
        _lblStuckMagnitude.Location = new Point(336, 48);
        _lblStuckMagnitude.Name = "_lblStuckMagnitude";
        _lblStuckMagnitude.Size = new Size(90, 20);
        _lblStuckMagnitude.TabIndex = 7;
        _lblStuckMagnitude.Text = "Код АЦП";
        //
        // _numStuckMagnitude
        //
        _numStuckMagnitude.DecimalPlaces = 2;
        _numStuckMagnitude.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numStuckMagnitude.Location = new Point(430, 46);
        _numStuckMagnitude.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        _numStuckMagnitude.Name = "_numStuckMagnitude";
        _numStuckMagnitude.Size = new Size(72, 22);
        _numStuckMagnitude.TabIndex = 8;
        //
        // _btnStuckManual
        //
        _btnStuckManual.FlatStyle = FlatStyle.Flat;
        _btnStuckManual.Location = new Point(8, 72);
        _btnStuckManual.Name = "_btnStuckManual";
        _btnStuckManual.Size = new Size(150, 22);
        _btnStuckManual.TabIndex = 9;
        _btnStuckManual.Text = "Сработать сейчас";
        //
        // _history
        //
        _history.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _history.BackColor = Color.White;
        _history.BorderStyle = BorderStyle.FixedSingle;
        _history.DrawMode = DrawMode.OwnerDrawFixed;
        _history.Font = new Font("Courier New", 9.25F);
        _history.FormattingEnabled = true;
        _history.IntegralHeight = false;
        _history.Location = new Point(11, 560);
        _history.Name = "_history";
        _history.Size = new Size(1620, 208);
        _history.TabIndex = 7;
        //
        // StaticFaultForm
        //
        BackColor = Color.FromArgb(255, 250, 240);
        ClientSize = new Size(1642, 780);
        Controls.Add(_btnCycle);
        Controls.Add(_btnClearHistory);
        Controls.Add(_grpSilence);
        Controls.Add(_grpSpike);
        Controls.Add(_grpDrift);
        Controls.Add(_grpCorrupt);
        Controls.Add(_grpStuck);
        Controls.Add(_history);
        MinimumSize = new Size(560, 700);
        Name = "StaticFaultForm";
        Text = "Сбои — Статика";
        _grpSilence.ResumeLayout(false);
        _grpSpike.ResumeLayout(false);
        _grpDrift.ResumeLayout(false);
        _grpCorrupt.ResumeLayout(false);
        _grpStuck.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_numSilenceParam1).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numSilenceParam2).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numSpikeParam1).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numSpikeMagnitude).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftParam1).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftParam2).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numDriftMagnitude).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numCorruptParam1).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numCorruptMagnitude).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckParam1).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckParam2).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numStuckMagnitude).EndInit();
        ResumeLayout(false);
    }

    private Button _btnCycle;
    private Button _btnClearHistory;

    private GroupBox _grpSilence;
    private CheckBox _chkSilenceEnabled;
    private Label _lblSilenceMode;
    private ComboBox _cmbSilenceMode;
    private Label _lblSilenceParam1;
    private NumericUpDown _numSilenceParam1;
    private Label _lblSilenceParam2;
    private NumericUpDown _numSilenceParam2;
    private Button _btnSilenceManual;

    private GroupBox _grpSpike;
    private CheckBox _chkSpikeEnabled;
    private Label _lblSpikeMode;
    private ComboBox _cmbSpikeMode;
    private Label _lblSpikeParam1;
    private NumericUpDown _numSpikeParam1;
    private Label _lblSpikeMagnitude;
    private NumericUpDown _numSpikeMagnitude;
    private Button _btnSpikeManual;

    private GroupBox _grpDrift;
    private CheckBox _chkDriftEnabled;
    private Label _lblDriftMode;
    private ComboBox _cmbDriftMode;
    private Label _lblDriftParam1;
    private NumericUpDown _numDriftParam1;
    private Label _lblDriftParam2;
    private NumericUpDown _numDriftParam2;
    private Label _lblDriftMagnitude;
    private NumericUpDown _numDriftMagnitude;
    private Button _btnDriftManual;

    private GroupBox _grpCorrupt;
    private CheckBox _chkCorruptEnabled;
    private Label _lblCorruptMode;
    private ComboBox _cmbCorruptMode;
    private Label _lblCorruptParam1;
    private NumericUpDown _numCorruptParam1;
    private Label _lblCorruptMagnitude;
    private NumericUpDown _numCorruptMagnitude;
    private Button _btnCorruptManual;

    private GroupBox _grpStuck;
    private CheckBox _chkStuckEnabled;
    private Label _lblStuckMode;
    private ComboBox _cmbStuckMode;
    private Label _lblStuckParam1;
    private NumericUpDown _numStuckParam1;
    private Label _lblStuckParam2;
    private NumericUpDown _numStuckParam2;
    private Label _lblStuckMagnitude;
    private NumericUpDown _numStuckMagnitude;
    private Button _btnStuckManual;

    private FaultHistoryListBox _history;
}
