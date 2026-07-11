namespace ScaleListener;

partial class MainForm
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
        _lblTitle = new Label();
        _btnStatic = new Button();
        _btnDynamic = new Button();
        _lblHint = new Label();
        SuspendLayout();
        //
        // _lblTitle
        //
        _lblTitle.AutoSize = false;
        _lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblTitle.Location = new Point(16, 16);
        _lblTitle.Name = "_lblTitle";
        _lblTitle.Size = new Size(328, 32);
        _lblTitle.TabIndex = 0;
        _lblTitle.Text = "Эмулятор АЦП СИМ А04";
        _lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        //
        // _btnStatic
        //
        _btnStatic.FlatStyle = FlatStyle.Flat;
        _btnStatic.Location = new Point(32, 68);
        _btnStatic.Name = "_btnStatic";
        _btnStatic.Size = new Size(130, 52);
        _btnStatic.TabIndex = 1;
        _btnStatic.Text = "Статика";
        _btnStatic.Click += BtnStatic_Click;
        //
        // _btnDynamic
        //
        _btnDynamic.FlatStyle = FlatStyle.Flat;
        _btnDynamic.Location = new Point(198, 68);
        _btnDynamic.Name = "_btnDynamic";
        _btnDynamic.Size = new Size(130, 52);
        _btnDynamic.TabIndex = 2;
        _btnDynamic.Text = "Динамика";
        _btnDynamic.Click += BtnDynamic_Click;
        //
        // _lblHint
        //
        _lblHint.AutoSize = false;
        _lblHint.Location = new Point(16, 136);
        _lblHint.Name = "_lblHint";
        _lblHint.Size = new Size(328, 24);
        _lblHint.TabIndex = 3;
        _lblHint.Text = "Открывайте один режим за раз: оба используют COM4.";
        _lblHint.TextAlign = ContentAlignment.MiddleCenter;
        //
        // MainForm
        //
        ClientSize = new Size(360, 180);
        Controls.Add(_lblTitle);
        Controls.Add(_btnStatic);
        Controls.Add(_btnDynamic);
        Controls.Add(_lblHint);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimumSize = new Size(360, 180);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Scale Listener - главное меню";
        ResumeLayout(false);
    }

    private Label _lblTitle;
    private Button _btnStatic;
    private Button _btnDynamic;
    private Label _lblHint;
}
