namespace Vesy13.Forms;

public partial class PasswordDialog : Form
{
    private const string DefaultPassword = "1234";

    public PasswordDialog()
    {
        InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        _lblPrompt.Font   = UiFonts.Medium;
        _txtPassword.Font = UiFonts.SubHeader;
    }

    private void TxtPassword_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) TryAccept();
    }

    private void BtnOk_Click(object? sender, EventArgs e) => TryAccept();

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
