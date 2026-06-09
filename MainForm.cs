using Vesy13.Services.Hardware;
using Vesy13.Services.Repositories;

namespace Vesy13;

/// <summary>
/// Главная форма — навигационный хаб. Открывает дочерние формы через OpenForm и
/// скрывается, пока открыта дочерняя.
/// </summary>
public partial class MainForm : Form
{
    private SimA04Reader    _sim = null!;
    private LocalRepository _ldb = null!;

    public MainForm()
    {
        InitializeComponent();
    }

    public MainForm(SimA04Reader sim, LocalRepository ldb)
    {
        _sim = sim;
        _ldb = ldb;
        InitializeComponent();
    }

    // ── Lifecycle ───────────────────────────────────────────────────────────

    private void ApplyTheme()
    {
        BackColor = Forms.UiColors.AppBackground;
        _pnlHeader.BackColor = Forms.UiColors.HeaderBar;
        _pnlStatus.BackColor = Forms.UiColors.StatusBar;

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
        _lblConn.Font        = Forms.UiFonts.Body;
        _lblConn.ForeColor   = Forms.UiColors.TextMuted;
        _btnConn.Font        = Forms.UiFonts.Small;
        _btnConn.BackColor   = Forms.UiColors.NavigationAction;
        _btnConn.ForeColor   = Forms.UiColors.TextOnDark;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ApplyTheme();
        if (DesignMode || _sim is null) return;
        _sim.ConnectionChanged += Adc_ConnectionChanged;
        UpdateConn(_sim.IsConnected);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (!DesignMode && _sim is not null)
        {
            _sim.ConnectionChanged -= Adc_ConnectionChanged;
            _sim.Close();
        }
        base.OnFormClosed(e);
    }

    // ── Connection ──────────────────────────────────────────────────────────

    private void Adc_ConnectionChanged(object? sender, bool connected) => UpdateConn(connected);

    private void BtnConn_Click(object? sender, EventArgs e)
    {
        if (_sim.IsConnected) { _sim.Close(); return; }
        try   { _sim.Open("COM1"); }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void UpdateConn(bool connected)
    {
        if (InvokeRequired) { BeginInvoke(() => UpdateConn(connected)); return; }
        _dotConn.BackColor = connected ? Forms.UiColors.PrimaryAction : Forms.UiColors.Disconnected;
        _lblConn.Text      = connected ? $"АЦП: {_sim.PortName}" : "АЦП: отключён";
        _btnConn.Text      = connected ? "Отключить" : "Подключить";
    }

    // ── Navigation ──────────────────────────────────────────────────────────

    private void BtnStatic_Click(object? sender, EventArgs e)      => OpenForm(new Forms.StaticWeighingForm(_sim, _ldb));
    private void BtnDynamic_Click(object? sender, EventArgs e)     => OpenForm(new Forms.DynamicWeighingForm(_sim, _ldb));
    private void BtnService_Click(object? sender, EventArgs e)     => OpenForm(new Forms.ServiceForm(_sim, _ldb));
    private void BtnCorrections_Click(object? sender, EventArgs e) => OpenForm(new Forms.CorrectionsForm(_ldb));
    private void BtnPrint_Click(object? sender, EventArgs e)       => OpenForm(new Forms.PrintForm(new FactoryRepository()));
    private void BtnLogs_Click(object? sender, EventArgs e)        => OpenForm(new Forms.LogsForm());

    private void OpenForm(Form form)
    {
        form.FormClosed += (_, _) => Show();
        Hide();
        form.Show();
    }
}
