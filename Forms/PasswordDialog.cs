namespace Vesy13.Forms;

public class PasswordDialog : Form
{
    private TextBox _txtPassword = null!;

    // TODO: заменить на сравнение с hash из settings.json
    private const string DefaultPassword = "1234";

    public PasswordDialog()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text            = "Сервисный режим";
        ClientSize      = new Size(300, 140);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox     = false;
        MinimizeBox     = false;
        StartPosition   = FormStartPosition.CenterParent;

        Controls.Add(new Label
        {
            Text     = "Введите пароль:",
            Location = new Point(20, 20),
            AutoSize = true,
            Font     = new Font("Segoe UI", 10),
        });

        _txtPassword = new TextBox
        {
            Location              = new Point(20, 46),
            Width                 = 260,
            UseSystemPasswordChar = true,
            Font                  = new Font("Segoe UI", 11),
        };
        _txtPassword.KeyDown += (_, e) => { if (e.KeyCode == Keys.Enter) TryAccept(); };
        Controls.Add(_txtPassword);

        var btnOk = new Button
        {
            Text      = "Войти",
            Location  = new Point(80, 90),
            Size      = new Size(80, 30),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(25, 45, 90),
            ForeColor = Color.White,
        };
        btnOk.Click += (_, _) => TryAccept();
        Controls.Add(btnOk);

        var btnCancel = new Button
        {
            Text         = "Отмена",
            Location     = new Point(170, 90),
            Size         = new Size(80, 30),
            FlatStyle    = FlatStyle.Flat,
            DialogResult = DialogResult.Cancel,
        };
        Controls.Add(btnCancel);
    }

    private void TryAccept()
    {
        if (_txtPassword.Text == DefaultPassword)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        else
        {
            MessageBox.Show("Неверный пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtPassword.Clear();
            _txtPassword.Focus();
        }
    }
}
