using ScaleListener.FaultInjection;

namespace ScaleListener;

/// <summary>
/// Панель управления сбоями для динамического потока. Сам движок (<see cref="FaultEngine"/>)
/// живёт в <see cref="DynamicForm"/> и продолжает работать, даже если эта панель закрыта.
///
/// Разделение ответственности: вся презентация (тексты, цвета, лимиты, состав панелей,
/// начальные значения) принадлежит Designer.cs и правится в дизайнере; этот файл только
/// переносит значения контролов в состояние сбоя и показывает историю. Код не трогает
/// ни Text, ни Visible, ни Maximum контролов.
/// </summary>
public partial class DynamicFaultForm : Form
{
    private readonly FaultEngine _engine;

    /// <summary>Только для дизайнера VS: ему нужен конструктор без параметров, чтобы создать design-time экземпляр формы.</summary>
    public DynamicFaultForm() : this(new FaultEngine()) { }

    public DynamicFaultForm(FaultEngine engine)
    {
        _engine = engine;
        InitializeComponent();

        PushSilenceToState();
        PushSpikeToState();
        PushDriftToState();
        PushCorruptToState();
        PushStuckToState();

        _engine.HistoryAppended += OnHistoryAppended;
    }

    // ── Начальная синхронизация: дизайнер - источник истины для стартовых значений ──

    private void PushSilenceToState()
    {
        var state = _engine.Get(FaultType.Silence);
        state.Mode = (FaultMode)_cmbSilenceMode.SelectedIndex;
        state.ActiveSeconds = (double)_numSilenceActive.Value;
        state.GapSeconds = (double)_numSilenceGap.Value;
    }

    private void PushSpikeToState()
    {
        var state = _engine.Get(FaultType.Spike);
        state.Mode = (FaultMode)_cmbSpikeMode.SelectedIndex;
        state.IntervalSeconds = (double)_numSpikeInterval.Value;
        state.RatePerMinute = (double)_numSpikeRate.Value;
        state.Magnitude = (double)_numSpikeMagnitude.Value;
    }

    private void PushDriftToState()
    {
        var state = _engine.Get(FaultType.Drift);
        state.Mode = (FaultMode)_cmbDriftMode.SelectedIndex;
        state.ActiveSeconds = (double)_numDriftActive.Value;
        state.GapSeconds = (double)_numDriftGap.Value;
        state.Magnitude = (double)_numDriftMagnitude.Value;
    }

    private void PushCorruptToState()
    {
        var state = _engine.Get(FaultType.Corrupt);
        state.Mode = (FaultMode)_cmbCorruptMode.SelectedIndex;
        state.IntervalSeconds = (double)_numCorruptInterval.Value;
        state.RatePerMinute = (double)_numCorruptRate.Value;
        state.Magnitude = (double)_numCorruptMagnitude.Value;
    }

    private void PushStuckToState()
    {
        var state = _engine.Get(FaultType.Stuck);
        state.Mode = (FaultMode)_cmbStuckMode.SelectedIndex;
        state.ActiveSeconds = (double)_numStuckActive.Value;
        state.GapSeconds = (double)_numStuckGap.Value;
        state.Magnitude = (double)_numStuckMagnitude.Value;
    }

    // ── Тишина (Silence) ────────────────────────────────────────────────────

    private void CmbSilenceMode_SelectedIndexChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Silence).Mode = (FaultMode)_cmbSilenceMode.SelectedIndex;

    private void NumSilenceActive_ValueChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Silence).ActiveSeconds = (double)_numSilenceActive.Value;

    private void NumSilenceGap_ValueChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Silence).GapSeconds = (double)_numSilenceGap.Value;

    private void BtnSilenceManual_Click(object? sender, EventArgs e)
        => _engine.TriggerManual(FaultType.Silence);

    // ── Одиночный выброс (Spike) ────────────────────────────────────────────

    private void CmbSpikeMode_SelectedIndexChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Spike).Mode = (FaultMode)_cmbSpikeMode.SelectedIndex;

    private void NumSpikeInterval_ValueChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Spike).IntervalSeconds = (double)_numSpikeInterval.Value;

    private void NumSpikeRate_ValueChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Spike).RatePerMinute = (double)_numSpikeRate.Value;

    private void NumSpikeMagnitude_ValueChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Spike).Magnitude = (double)_numSpikeMagnitude.Value;

    private void BtnSpikeManual_Click(object? sender, EventArgs e)
        => _engine.ArmManualDiscrete(FaultType.Spike);

    // ── Дрейф/дребезг (Drift) ───────────────────────────────────────────────

    private void CmbDriftMode_SelectedIndexChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Drift).Mode = (FaultMode)_cmbDriftMode.SelectedIndex;

    private void NumDriftActive_ValueChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Drift).ActiveSeconds = (double)_numDriftActive.Value;

    private void NumDriftGap_ValueChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Drift).GapSeconds = (double)_numDriftGap.Value;

    private void NumDriftMagnitude_ValueChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Drift).Magnitude = (double)_numDriftMagnitude.Value;

    private void BtnDriftManual_Click(object? sender, EventArgs e)
        => _engine.TriggerManual(FaultType.Drift);

    // ── Порча байт (Corrupt) ────────────────────────────────────────────────

    private void CmbCorruptMode_SelectedIndexChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Corrupt).Mode = (FaultMode)_cmbCorruptMode.SelectedIndex;

    private void NumCorruptInterval_ValueChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Corrupt).IntervalSeconds = (double)_numCorruptInterval.Value;

    private void NumCorruptRate_ValueChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Corrupt).RatePerMinute = (double)_numCorruptRate.Value;

    private void NumCorruptMagnitude_ValueChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Corrupt).Magnitude = (double)_numCorruptMagnitude.Value;

    private void BtnCorruptManual_Click(object? sender, EventArgs e)
        => _engine.ArmManualDiscrete(FaultType.Corrupt);

    // ── Застрявший датчик (Stuck) ───────────────────────────────────────────

    private void CmbStuckMode_SelectedIndexChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Stuck).Mode = (FaultMode)_cmbStuckMode.SelectedIndex;

    private void NumStuckActive_ValueChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Stuck).ActiveSeconds = (double)_numStuckActive.Value;

    private void NumStuckGap_ValueChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Stuck).GapSeconds = (double)_numStuckGap.Value;

    private void NumStuckMagnitude_ValueChanged(object? sender, EventArgs e)
        => _engine.Get(FaultType.Stuck).Magnitude = (double)_numStuckMagnitude.Value;

    private void BtnStuckManual_Click(object? sender, EventArgs e)
        => _engine.TriggerManual(FaultType.Stuck);

    // ── Цикл и история ──────────────────────────────────────────────────────

    private void BtnCycle_Click(object? sender, EventArgs e)
    {
        if (_engine.IsRunning) _engine.StopCycle();
        else _engine.StartCycle();

        _btnCycle.Text = _engine.IsRunning ? "Стоп цикла" : "Старт цикла";
    }

    private void BtnClearHistory_Click(object? sender, EventArgs e) => _history.ClearHistory();

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
