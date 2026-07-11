using ScaleListener.FaultInjection;

namespace ScaleListener;

/// <summary>
/// Панель управления сбоями для динамического потока. Сам движок (<see cref="FaultEngine"/>)
/// живёт в <see cref="DynamicForm"/> и продолжает работать, даже если эта панель закрыта -
/// форма только показывает/редактирует его состояние и историю.
/// </summary>
public sealed class DynamicFaultForm : Form
{
    private readonly FaultEngine _engine;
    private FaultHistoryListBox _history = null!;
    private Button _btnCycle = null!;
    private Button _btnClearHistory = null!;

    public DynamicFaultForm(FaultEngine engine)
    {
        _engine = engine;
        BuildUi();
        _engine.HistoryAppended += OnHistoryAppended;
        UpdateCycleButton();
    }

    private void BuildUi()
    {
        Text = "Сбои — Динамика";
        ClientSize = new Size(560, 780);
        MinimumSize = new Size(560, 700);
        BackColor = Color.FromArgb(255, 250, 240);

        var top = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 40,
            Padding = new Padding(8),
        };

        _btnCycle = new Button { Size = new Size(150, 26), FlatStyle = FlatStyle.Flat };
        _btnCycle.Click += (_, _) => ToggleCycle();

        _btnClearHistory = new Button { Text = "Очистить историю", Size = new Size(140, 26), FlatStyle = FlatStyle.Flat };
        _btnClearHistory.Click += (_, _) => _history.ClearHistory();

        top.Controls.AddRange(new Control[] { _btnCycle, _btnClearHistory });

        var panels = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Height = 5 * 104,
            Padding = new Padding(4),
            AutoScroll = false,
        };
        panels.Controls.Add(FaultPanelBuilder.BuildPanel(_engine.Get(FaultType.Silence), "Тишина (Silence) — эмулятор не отвечает", "—",
            () => TriggerSilenceManual()));
        panels.Controls.Add(FaultPanelBuilder.BuildPanel(_engine.Get(FaultType.Spike), "Одиночный выброс (Spike)", "Ампл., т",
            () => TriggerSpikeManual()));
        panels.Controls.Add(FaultPanelBuilder.BuildPanel(_engine.Get(FaultType.Drift), "Дрейф/дребезг (Drift)", "Ампл., т",
            () => TriggerDriftManual()));
        panels.Controls.Add(FaultPanelBuilder.BuildPanel(_engine.Get(FaultType.Corrupt), "Порча байт (Corrupt)", "Байт мусора",
            () => TriggerCorruptManual()));
        panels.Controls.Add(FaultPanelBuilder.BuildPanel(_engine.Get(FaultType.Stuck), "Застрявший датчик (Stuck)", "Код АЦП",
            () => TriggerStuckManual()));
        foreach (Control c in panels.Controls)
            c.Width = 536;
        panels.Width = 552;

        _history = new FaultHistoryListBox
        {
            Dock = DockStyle.Fill,
        };

        Controls.Add(_history);
        Controls.Add(panels);
        Controls.Add(top);
    }

    private void ToggleCycle()
    {
        if (_engine.IsRunning) _engine.StopCycle();
        else _engine.StartCycle();
        UpdateCycleButton();
    }

    private void UpdateCycleButton()
    {
        _btnCycle.Text = _engine.IsRunning ? "Стоп цикла" : "Старт цикла";
        _btnCycle.BackColor = _engine.IsRunning ? Color.LightGreen : Color.LightGray;
    }

    // Continuous (Silence/Drift/Stuck): переключаем окно прямо в движке.
    // Discrete (Spike/Corrupt): взводим флаг, реальную порчу на ближайшей отправке
    // применит и запишет в историю DynamicForm - у панели нет доступа к текущему сэмплу.
    private void TriggerSilenceManual() => _engine.TriggerManual(FaultType.Silence);
    private void TriggerDriftManual() => _engine.TriggerManual(FaultType.Drift);
    private void TriggerStuckManual() => _engine.TriggerManual(FaultType.Stuck);
    private void TriggerSpikeManual() => _engine.ArmManualDiscrete(FaultType.Spike);
    private void TriggerCorruptManual() => _engine.ArmManualDiscrete(FaultType.Corrupt);

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
