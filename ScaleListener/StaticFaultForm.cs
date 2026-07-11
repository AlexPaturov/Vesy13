using ScaleListener.FaultInjection;

namespace ScaleListener;

/// <summary>
/// Панель управления сбоями для статических ответов. Движок живёт в <see cref="StaticForm"/>
/// и продолжает работать, даже если эта панель закрыта.
/// </summary>
public partial class StaticFaultForm : Form
{
    private readonly FaultEngine _engine;

    public StaticFaultForm(FaultEngine engine)
    {
        _engine = engine;
        InitializeComponent();

        _panelSilence.Bind(_engine.Get(FaultType.Silence), "Тишина (Silence) — нет ответа на poll", "—",
            () => _engine.TriggerManual(FaultType.Silence));
        _panelSpike.Bind(_engine.Get(FaultType.Spike), "Одиночный выброс (Spike)", "Ампл., т",
            () => _engine.ArmManualDiscrete(FaultType.Spike));
        _panelDrift.Bind(_engine.Get(FaultType.Drift), "Дрейф/дребезг (Drift)", "Ампл., т",
            () => _engine.TriggerManual(FaultType.Drift));
        _panelCorrupt.Bind(_engine.Get(FaultType.Corrupt), "Порча байт (Corrupt)", "Байт мусора",
            () => _engine.ArmManualDiscrete(FaultType.Corrupt));
        _panelStuck.Bind(_engine.Get(FaultType.Stuck), "Застрявший датчик (Stuck)", "Код АЦП",
            () => _engine.TriggerManual(FaultType.Stuck));

        _engine.HistoryAppended += OnHistoryAppended;
        UpdateCycleButton();
    }

    private void BtnCycle_Click(object? sender, EventArgs e)
    {
        if (_engine.IsRunning) _engine.StopCycle();
        else _engine.StartCycle();
        UpdateCycleButton();
    }

    private void BtnClearHistory_Click(object? sender, EventArgs e) => _history.ClearHistory();

    private void UpdateCycleButton()
    {
        _btnCycle.Text = _engine.IsRunning ? "Стоп цикла" : "Старт цикла";
        _btnCycle.BackColor = _engine.IsRunning ? Color.LightGreen : Color.LightGray;
    }

    private void OnHistoryAppended(FaultHistoryEntry entry)
    {
        if (InvokeRequired) { BeginInvoke(() => OnHistoryAppended(entry)); return; }
        _history.Append(entry);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _engine.HistoryAppended -= OnHistoryAppended;
        base.OnFormClosed(e);
    }
}
