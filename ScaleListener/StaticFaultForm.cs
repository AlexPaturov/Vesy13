using ScaleListener.FaultInjection;

namespace ScaleListener;

/// <summary>
/// Панель управления сбоями для статических ответов. Движок живёт в <see cref="StaticForm"/>
/// и продолжает работать, даже если эта панель закрыта.
/// Разметка каждой панели сбоя лежит в Designer.cs и настраивается независимо от остальных;
/// привязка контролов к состоянию сбоя - через <see cref="FaultPanelBinder"/>.
/// </summary>
public partial class StaticFaultForm : Form
{
    private readonly FaultEngine _engine;

    /// <summary>Только для дизайнера VS: ему нужен конструктор без параметров, чтобы создать design-time экземпляр формы.</summary>
    public StaticFaultForm() : this(new FaultEngine()) { }

    public StaticFaultForm(FaultEngine engine)
    {
        _engine = engine;
        InitializeComponent();

        _ = new FaultPanelBinder(_engine.Get(FaultType.Silence),
            _chkSilenceEnabled, _cmbSilenceMode,
            _lblSilenceParam1, _numSilenceParam1, _lblSilenceParam2, _numSilenceParam2,
            null, _btnSilenceManual,
            () => _engine.TriggerManual(FaultType.Silence));

        _ = new FaultPanelBinder(_engine.Get(FaultType.Spike),
            _chkSpikeEnabled, _cmbSpikeMode,
            _lblSpikeParam1, _numSpikeParam1, null, null,
            _numSpikeMagnitude, _btnSpikeManual,
            () => _engine.ArmManualDiscrete(FaultType.Spike));

        _ = new FaultPanelBinder(_engine.Get(FaultType.Drift),
            _chkDriftEnabled, _cmbDriftMode,
            _lblDriftParam1, _numDriftParam1, _lblDriftParam2, _numDriftParam2,
            _numDriftMagnitude, _btnDriftManual,
            () => _engine.TriggerManual(FaultType.Drift));

        _ = new FaultPanelBinder(_engine.Get(FaultType.Corrupt),
            _chkCorruptEnabled, _cmbCorruptMode,
            _lblCorruptParam1, _numCorruptParam1, null, null,
            _numCorruptMagnitude, _btnCorruptManual,
            () => _engine.ArmManualDiscrete(FaultType.Corrupt));

        _ = new FaultPanelBinder(_engine.Get(FaultType.Stuck),
            _chkStuckEnabled, _cmbStuckMode,
            _lblStuckParam1, _numStuckParam1, _lblStuckParam2, _numStuckParam2,
            _numStuckMagnitude, _btnStuckManual,
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
