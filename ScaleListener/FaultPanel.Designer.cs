namespace ScaleListener;

partial class FaultPanel
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
        _group = new GroupBox();
        _chkEnabled = new CheckBox();
        _lblMode = new Label();
        _cmbMode = new ComboBox();
        _lblParam1 = new Label();
        _numParam1 = new NumericUpDown();
        _lblParam2 = new Label();
        _numParam2 = new NumericUpDown();
        _lblMagnitude = new Label();
        _numMagnitude = new NumericUpDown();
        _btnManual = new Button();
        _group.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_numParam1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numParam2).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_numMagnitude).BeginInit();
        SuspendLayout();
        //
        // _group
        //
        _group.BackColor = Color.FromArgb(255, 243, 224);
        _group.Controls.Add(_chkEnabled);
        _group.Controls.Add(_lblMode);
        _group.Controls.Add(_cmbMode);
        _group.Controls.Add(_lblParam1);
        _group.Controls.Add(_numParam1);
        _group.Controls.Add(_lblParam2);
        _group.Controls.Add(_numParam2);
        _group.Controls.Add(_lblMagnitude);
        _group.Controls.Add(_numMagnitude);
        _group.Controls.Add(_btnManual);
        _group.Dock = DockStyle.Fill;
        _group.Location = new Point(0, 0);
        _group.Name = "_group";
        _group.Padding = new Padding(8, 4, 8, 4);
        _group.Size = new Size(536, 96);
        _group.TabIndex = 0;
        _group.TabStop = false;
        //
        // _chkEnabled
        //
        _chkEnabled.Location = new Point(8, 20);
        _chkEnabled.Name = "_chkEnabled";
        _chkEnabled.Size = new Size(60, 22);
        _chkEnabled.TabIndex = 0;
        _chkEnabled.Text = "Вкл";
        _chkEnabled.CheckedChanged += ChkEnabled_CheckedChanged;
        //
        // _lblMode
        //
        _lblMode.Location = new Point(76, 22);
        _lblMode.Name = "_lblMode";
        _lblMode.Size = new Size(48, 20);
        _lblMode.TabIndex = 1;
        _lblMode.Text = "Режим:";
        //
        // _cmbMode
        //
        _cmbMode.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbMode.Items.AddRange(new object[] { "Выкл", "Периодично", "Случайно", "Вручную" });
        _cmbMode.Location = new Point(126, 19);
        _cmbMode.Name = "_cmbMode";
        _cmbMode.Size = new Size(110, 22);
        _cmbMode.TabIndex = 2;
        _cmbMode.SelectedIndexChanged += CmbMode_SelectedIndexChanged;
        //
        // _lblParam1
        //
        _lblParam1.Location = new Point(8, 48);
        _lblParam1.Name = "_lblParam1";
        _lblParam1.Size = new Size(100, 20);
        _lblParam1.TabIndex = 3;
        //
        // _numParam1
        //
        _numParam1.DecimalPlaces = 1;
        _numParam1.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numParam1.Location = new Point(112, 46);
        _numParam1.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numParam1.Name = "_numParam1";
        _numParam1.Size = new Size(72, 22);
        _numParam1.TabIndex = 4;
        _numParam1.ValueChanged += NumParam1_ValueChanged;
        //
        // _lblParam2
        //
        _lblParam2.Location = new Point(192, 48);
        _lblParam2.Name = "_lblParam2";
        _lblParam2.Size = new Size(64, 20);
        _lblParam2.TabIndex = 5;
        //
        // _numParam2
        //
        _numParam2.DecimalPlaces = 1;
        _numParam2.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numParam2.Location = new Point(256, 46);
        _numParam2.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
        _numParam2.Name = "_numParam2";
        _numParam2.Size = new Size(72, 22);
        _numParam2.TabIndex = 6;
        _numParam2.ValueChanged += NumParam2_ValueChanged;
        //
        // _lblMagnitude
        //
        _lblMagnitude.Location = new Point(336, 48);
        _lblMagnitude.Name = "_lblMagnitude";
        _lblMagnitude.Size = new Size(90, 20);
        _lblMagnitude.TabIndex = 7;
        //
        // _numMagnitude
        //
        _numMagnitude.DecimalPlaces = 2;
        _numMagnitude.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        _numMagnitude.Location = new Point(430, 46);
        _numMagnitude.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        _numMagnitude.Name = "_numMagnitude";
        _numMagnitude.Size = new Size(72, 22);
        _numMagnitude.TabIndex = 8;
        _numMagnitude.ValueChanged += NumMagnitude_ValueChanged;
        //
        // _btnManual
        //
        _btnManual.FlatStyle = FlatStyle.Flat;
        _btnManual.Location = new Point(8, 72);
        _btnManual.Name = "_btnManual";
        _btnManual.Size = new Size(150, 22);
        _btnManual.TabIndex = 9;
        _btnManual.Text = "Сработать сейчас";
        _btnManual.Click += BtnManual_Click;
        //
        // FaultPanel
        //
        Controls.Add(_group);
        Name = "FaultPanel";
        Size = new Size(536, 96);
        _group.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_numParam1).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numParam2).EndInit();
        ((System.ComponentModel.ISupportInitialize)_numMagnitude).EndInit();
        ResumeLayout(false);
    }

    private GroupBox _group;
    private CheckBox _chkEnabled;
    private Label _lblMode;
    private ComboBox _cmbMode;
    private Label _lblParam1;
    private NumericUpDown _numParam1;
    private Label _lblParam2;
    private NumericUpDown _numParam2;
    private Label _lblMagnitude;
    private NumericUpDown _numMagnitude;
    private Button _btnManual;
}
