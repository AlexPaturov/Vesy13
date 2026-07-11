using ScaleListener.FaultInjection;

namespace ScaleListener;

/// <summary>
/// Панель управления сбоями для статических ответов. Движок живёт в <see cref="StaticForm"/>
/// и продолжает работать, даже если эта панель закрыта.
/// </summary>
public sealed class StaticFaultForm : Form
{
    private readonly FaultEngine _engine;
    private FaultHistoryListBox _history = null!;
    private Button _btnCycle = null!;
    private Button _btnClearHistory = null!;

    public StaticFaultForm(FaultEngine engine)
    {
        _engine = engine;
        BuildUi();
        _engine.HistoryAppended += OnHistoryAppended;
        UpdateCycleButton();
    }

    private void BuildUi()
    {
        Text = "Сбои — Статика";
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
        panels.Controls.Add(FaultPanelBuilder.BuildPanel(_engine.Get(FaultType.Silence), "Тишина (Silence) — нет ответа на poll", "—",
            () => _engine.TriggerManual(FaultType.Silence)));
        panels.Controls.Add(FaultPanelBuilder.BuildPanel(_engine.Get(FaultType.Spike), "Одиночный выброс (Spike)", "Ампл., т",
            () => _engine.ArmManualDiscrete(FaultType.Spike)));
        panels.Controls.Add(FaultPanelBuilder.BuildPanel(_engine.Get(FaultType.Drift), "Дрейф/дребезг (Drift)", "Ампл., т",
            () => _engine.TriggerManual(FaultType.Drift)));
        panels.Controls.Add(FaultPanelBuilder.BuildPanel(_engine.Get(FaultType.Corrupt), "Порча байт (Corrupt)", "Байт мусора",
            () => _engine.ArmManualDiscrete(FaultType.Corrupt)));
        panels.Controls.Add(FaultPanelBuilder.BuildPanel(_engine.Get(FaultType.Stuck), "Застрявший датчик (Stuck)", "Код АЦП",
            () => _engine.TriggerManual(FaultType.Stuck)));
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
