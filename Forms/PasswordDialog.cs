using Vesy13.Services.Configuration;

namespace Vesy13.Forms;

public partial class PasswordDialog : Form
{
    private SettingsService? _settings;

    public PasswordDialog()
    {
        InitializeComponent();
    }

    public PasswordDialog(SettingsService settings)
    {
        _settings = settings;
        InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ApplyTheme();
    }

    private void ApplyTheme()
    {
        BackColor = UiColors.AppBackground;
        _lblPrompt.Font = UiFonts.Medium;
        _lblPrompt.ForeColor = UiColors.TextPrimary;
        _txtPassword.Font = UiFonts.SubHeader;
        _txtPassword.BackColor = UiColors.InputBack;
        _txtPassword.ForeColor = UiColors.InputFore;
        _btnOk.Font = UiFonts.BodyBold;
        _btnOk.BackColor = UiColors.PrimaryAction;
        _btnOk.ForeColor = UiColors.TextOnDark;
        _btnCancel.Font = UiFonts.Body;
        _btnCancel.BackColor = UiColors.NeutralAction;
        _btnCancel.ForeColor = UiColors.TextPrimary;
    }

    private void TxtPassword_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) TryAccept();
    }

    private void BtnOk_Click(object? sender, EventArgs e) => TryAccept();

    private void TryAccept()
    {
        if (_settings is not null && _settings.VerifyAdminPassword(_txtPassword.Text))
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
