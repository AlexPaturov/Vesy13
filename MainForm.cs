using Vesy13.Services;

namespace Vesy13;

public partial class MainForm : Form
{
    private AdcService         _adc   = null!;
    private CalibrationService _calib = null!;
    private DatabaseService    _db    = null!;

    public MainForm()
    {
        InitializeComponent();
    }

    public MainForm(AdcService adc, CalibrationService calib, DatabaseService db)
    {
        _adc   = adc;
        _calib = calib;
        _db    = db;
        InitializeComponent();
    }

    // ── Lifecycle ───────────────────────────────────────────────────────────

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (DesignMode || _adc is null) return;
        _adc.ConnectionChanged += Adc_ConnectionChanged;
        UpdateConn(_adc.IsConnected);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (!DesignMode && _adc is not null)
        {
            _adc.ConnectionChanged -= Adc_ConnectionChanged;
            _adc.Close();
        }
        base.OnFormClosed(e);
    }

    // ── Connection ──────────────────────────────────────────────────────────

    private void Adc_ConnectionChanged(object? sender, bool connected) => UpdateConn(connected);

    private void BtnConn_Click(object? sender, EventArgs e)
    {
        if (_adc.IsConnected) { _adc.Close(); return; }
        try   { _adc.Open("COM1"); }
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
        _lblConn.Text      = connected ? $"АЦП: {_adc.PortName}" : "АЦП: отключён";
        _btnConn.Text      = connected ? "Отключить" : "Подключить";
    }

    // ── Navigation ──────────────────────────────────────────────────────────

    private void BtnStatic_Click(object? sender, EventArgs e)      => OpenForm(new Forms.StaticWeighingForm(_adc, _calib, _db));
    private void BtnDynamic_Click(object? sender, EventArgs e)     => OpenForm(new Forms.DynamicWeighingForm(_adc, _calib, _db));
    private void BtnService_Click(object? sender, EventArgs e)     => OpenForm(new Forms.ServiceForm(_adc, _calib));
    private void BtnCorrections_Click(object? sender, EventArgs e) => OpenForm(new Forms.CorrectionsForm(_db));
    private void BtnPrint_Click(object? sender, EventArgs e)       => MessageBox.Show("В разработке", "Печать отвесной", MessageBoxButtons.OK, MessageBoxIcon.Information);
    private void BtnLogs_Click(object? sender, EventArgs e)        => MessageBox.Show("В разработке", "Просмотр логов",  MessageBoxButtons.OK, MessageBoxIcon.Information);

    private void OpenForm(Form form)
    {
        form.FormClosed += (_, _) => Show();
        Hide();
        form.Show();
    }
}
