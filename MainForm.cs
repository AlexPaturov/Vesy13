using Vesy13.Services.Configuration;
using Vesy13.Services.Hardware;
using Vesy13.Services.Repositories;

namespace Vesy13;

/// <summary>
/// Главная форма — навигационный хаб. Открывает дочерние формы через OpenForm и
/// скрывается, пока открыта дочерняя.
/// </summary>
public partial class MainForm : Form
{
    private SimA04ReaderStatic    _sim = null!;
    private LocalRepository _ldb = null!;
    private SettingsService _settings = null!;

    public MainForm()
    {
        InitializeComponent();
    }

    public MainForm(SimA04ReaderStatic sim, LocalRepository ldb, SettingsService settings)
    {
        _sim = sim;
        _ldb = ldb;
        _settings = settings;
        InitializeComponent();
    }

    // ── Lifecycle ───────────────────────────────────────────────────────────

    private void ApplyTheme()
    {
        BackColor = Forms.UiColors.AppBackground;
        _pnlHeader.BackColor = Forms.UiColors.HeaderBar;

        _lblTitle.Font       = Forms.UiFonts.Title;
        _lblTitle.ForeColor  = Forms.UiColors.TextPrimary;
        _btnStatic.Font      = Forms.UiFonts.NavButton;
        _btnStatic.BackColor = Forms.UiColors.NavigationAction;
        _btnStatic.ForeColor = Forms.UiColors.TextOnDark;
        _btnDynamic.Font     = Forms.UiFonts.NavButton;
        _btnDynamic.BackColor = Forms.UiColors.NavigationAction;
        _btnDynamic.ForeColor = Forms.UiColors.TextOnDark;
        _btnService.Font     = Forms.UiFonts.NavButton;
        _btnService.BackColor = Forms.UiColors.NavigationAction;
        _btnService.ForeColor = Forms.UiColors.TextOnDark;
        _btnCorrections.Font = Forms.UiFonts.NavButton;
        _btnCorrections.BackColor = Forms.UiColors.NavigationAction;
        _btnCorrections.ForeColor = Forms.UiColors.TextOnDark;
        _btnPrint.Font       = Forms.UiFonts.NavButton;
        _btnPrint.BackColor = Forms.UiColors.NavigationAction;
        _btnPrint.ForeColor = Forms.UiColors.TextOnDark;
        _btnLogs.Font        = Forms.UiFonts.NavButton;
        _btnLogs.BackColor = Forms.UiColors.NavigationAction;
        _btnLogs.ForeColor = Forms.UiColors.TextOnDark;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ApplyTheme();
        if (DesignMode || _sim is null) return;
    }
    // ── Navigation ──────────────────────────────────────────────────────────

    private void BtnStatic_Click(object? sender, EventArgs e)      => OpenForm(new Forms.StaticWeighingForm(_sim, _ldb, _settings));
    private void BtnDynamic_Click(object? sender, EventArgs e)     => OpenForm(new Forms.DynamicWeighingForm(_sim, _ldb, _settings));
    private void BtnService_Click(object? sender, EventArgs e)     => OpenForm(new Forms.ServiceForm(_sim, _ldb, _settings));
    private void BtnCorrections_Click(object? sender, EventArgs e) => OpenForm(new Forms.CorrectionsForm(_ldb, _settings));
    private void BtnPrint_Click(object? sender, EventArgs e)       => OpenForm(new Forms.PrintForm(new FactoryRepository()));
    private void BtnLogs_Click(object? sender, EventArgs e)        => OpenForm(new Forms.LogsForm());
    private void OpenForm(Form form)
    {
        form.FormClosed += (_, _) => Show();
        Hide();
        form.Show();
    }
}
