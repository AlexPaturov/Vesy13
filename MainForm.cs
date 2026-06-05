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

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
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
        _dotConn.BackColor = connected ? Color.LimeGreen : Color.Gray;
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
