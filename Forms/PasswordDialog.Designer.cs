namespace Vesy13.Forms;

partial class PasswordDialog
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        _lblPrompt   = new Label();
        _txtPassword = new TextBox();
        _btnOk       = new Button();
        _btnCancel   = new Button();

        SuspendLayout();

        _lblPrompt.Text     = "Введите пароль:";
        _lblPrompt.Location = new Point(20, 20);
        _lblPrompt.AutoSize = true;
        _lblPrompt.Font     = new Font("Segoe UI", 10F);

        _txtPassword.Location              = new Point(20, 46);
        _txtPassword.Width                 = 260;
        _txtPassword.UseSystemPasswordChar = true;
        _txtPassword.Font                  = new Font("Segoe UI", 11F);
        _txtPassword.KeyDown              += TxtPassword_KeyDown;

        _btnOk.Text      = "Войти";
        _btnOk.Location  = new Point(80, 90);
        _btnOk.Size      = new Size(80, 30);
        _btnOk.FlatStyle = FlatStyle.Flat;
        _btnOk.BackColor = UiColors.HeaderBar;
        _btnOk.ForeColor = UiColors.TextOnDark;
        _btnOk.Click    += BtnOk_Click;

        _btnCancel.Text         = "Отмена";
        _btnCancel.Location     = new Point(170, 90);
        _btnCancel.Size         = new Size(80, 30);
        _btnCancel.FlatStyle    = FlatStyle.Flat;
        _btnCancel.DialogResult = DialogResult.Cancel;

        Text            = "Сервисный режим";
        ClientSize      = new Size(300, 140);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox     = false;
        MinimizeBox     = false;
        StartPosition   = FormStartPosition.CenterParent;
        Controls.Add(_lblPrompt);
        Controls.Add(_txtPassword);
        Controls.Add(_btnOk);
        Controls.Add(_btnCancel);

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ResumeLayout(false);
        PerformLayout();
    }

    private Label   _lblPrompt;
    private TextBox _txtPassword;
    private Button  _btnOk;
    private Button  _btnCancel;
}
