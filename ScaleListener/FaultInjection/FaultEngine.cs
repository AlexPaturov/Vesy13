namespace ScaleListener.FaultInjection;

/// <summary>
/// Планировщик управляемых сбоев. Протокол-агностичен: ничего не знает о формате кадра,
/// только решает "должен ли сбой X сработать/быть активным прямо сейчас". Вызывающая форма
/// дёргает <see cref="Pump"/> на каждую возможность отправки (тик потока у динамики, каждый
/// входящий poll-байт у статики), опрашивает <see cref="ShouldFireDiscrete"/>/<see cref="IsWindowActive"/>
/// и сама решает, как исказить свой конкретный кадр, а затем сама вызывает <see cref="AppendHistory"/>
/// с реальными значениями (правильным/неправильным) - движок значений не считает и не хранит.
/// Всё выполняется на UI-потоке (тики таймера/DataReceived через BeginInvoke), синхронизация не нужна.
/// </summary>
public sealed class FaultEngine
{
    private readonly Random _rng = new();
    private readonly Dictionary<FaultType, FaultState> _states = new();

    /// <summary>Непрерывный сбой сменил состояние (без значений - их добавляет вызывающая форма).</summary>
    public event Action<FaultType, bool>? WindowChanged;

    public event Action<FaultHistoryEntry>? HistoryAppended;

    public bool IsRunning { get; private set; }

    public FaultEngine()
    {
        _states[FaultType.Silence] = new FaultState(FaultType.Silence, FaultKind.Continuous);
        _states[FaultType.Spike]   = new FaultState(FaultType.Spike, FaultKind.Discrete);
        _states[FaultType.Drift]   = new FaultState(FaultType.Drift, FaultKind.Continuous);
        _states[FaultType.Corrupt] = new FaultState(FaultType.Corrupt, FaultKind.Discrete);
        _states[FaultType.Stuck]   = new FaultState(FaultType.Stuck, FaultKind.Continuous);
    }

    public FaultState Get(FaultType type) => _states[type];

    public IEnumerable<FaultState> All => _states.Values;

    public void StartCycle()
    {
        IsRunning = true;
        var now = DateTime.UtcNow;
        foreach (var state in _states.Values)
        {
            state.LastOpportunityAt = now;
            state.NextTransitionAt = DateTime.MinValue;
            state.IsActive = false;
        }
    }

    public void StopCycle()
    {
        IsRunning = false;
        foreach (var state in _states.Values)
        {
            if (state.IsActive)
            {
                state.IsActive = false;
                WindowChanged?.Invoke(state.Type, false);
            }
        }
    }

    /// <summary>
    /// Ручное переключение окна Continuous-сбоя (кнопка "Вкл/Выкл сейчас"), не зависит от
    /// общего Start/Stop цикла и от текущего Mode. Для Discrete-сбоев не применимо - разовое
    /// срабатывание форма делает сама напрямую, без участия движка (оно не завязано на
    /// внутреннее состояние планировщика).
    /// </summary>
    public void TriggerManual(FaultType type)
    {
        var state = _states[type];
        if (state.Kind == FaultKind.Discrete)
            return;

        state.IsActive = !state.IsActive;
        WindowChanged?.Invoke(type, state.IsActive);
    }

    /// <summary>
    /// Должен вызываться на каждую "возможность отправки". Продвигает состояние окон
    /// непрерывных сбоев. Дискретные сбои сами по себе ничего не продвигают здесь -
    /// решение принимается в <see cref="ShouldFireDiscrete"/>.
    /// </summary>
    public void Pump()
    {
        if (!IsRunning) return;
        var now = DateTime.UtcNow;

        foreach (var state in _states.Values)
        {
            if (state.Kind != FaultKind.Continuous || !state.Enabled ||
                state.Mode is FaultMode.Off or FaultMode.Manual)
                continue;

            if (state.NextTransitionAt == DateTime.MinValue)
                state.NextTransitionAt = now.AddSeconds(NextGapSeconds(state));

            if (now < state.NextTransitionAt) continue;

            state.IsActive = !state.IsActive;
            double delay = state.IsActive ? NextActiveSeconds(state) : NextGapSeconds(state);
            state.NextTransitionAt = now.AddSeconds(delay);
            WindowChanged?.Invoke(state.Type, state.IsActive);
        }
    }

    /// <summary>Взвести ручное срабатывание Discrete-сбоя (кнопка "Сработать сейчас") - сработает на ближайшем <see cref="ShouldFireDiscrete"/>, независимо от Mode и от Start/Stop цикла.</summary>
    public void ArmManualDiscrete(FaultType type)
    {
        var state = _states[type];
        if (state.Kind == FaultKind.Discrete)
            state.ManualArmed = true;
    }

    /// <summary>
    /// Для Discrete-сбоев (Spike/Corrupt) - решить, срабатывает ли сбой на этой конкретной
    /// возможности отправки. В режиме Random вероятность на вызов калибруется по фактическому
    /// интервалу с прошлого вызова - не завязана на предполагаемую частоту потока, поэтому
    /// не может "незаметно" разогнаться выше того, что реально настроено в RatePerMinute.
    /// </summary>
    public bool ShouldFireDiscrete(FaultType type)
    {
        var state = _states[type];
        if (state.Kind != FaultKind.Discrete) return false;

        if (state.ManualArmed)
        {
            state.ManualArmed = false;
            return true;
        }

        if (!IsRunning || !state.Enabled) return false;

        var now = DateTime.UtcNow;
        bool fire;

        switch (state.Mode)
        {
            case FaultMode.Periodic:
                if (state.NextTransitionAt == DateTime.MinValue)
                    state.NextTransitionAt = now.AddSeconds(state.IntervalSeconds);
                fire = now >= state.NextTransitionAt;
                if (fire)
                    state.NextTransitionAt = now.AddSeconds(state.IntervalSeconds);
                break;

            case FaultMode.Random:
                double elapsedSeconds = (now - state.LastOpportunityAt).TotalSeconds;
                if (state.LastOpportunityAt == DateTime.MinValue || elapsedSeconds <= 0)
                {
                    fire = false;
                }
                else
                {
                    double expectedPerSecond = state.RatePerMinute / 60.0;
                    double probability = Math.Min(1.0, expectedPerSecond * elapsedSeconds);
                    fire = _rng.NextDouble() < probability;
                }
                break;

            default:
                fire = false;
                break;
        }

        state.LastOpportunityAt = now;
        return fire;
    }

    public bool IsWindowActive(FaultType type)
    {
        var state = _states[type];
        return state.Kind == FaultKind.Continuous && state.Enabled && state.IsActive;
    }

    public void AppendHistory(FaultType type, FaultEventKind kind, string correctValue, string wrongValue)
        => HistoryAppended?.Invoke(new FaultHistoryEntry(DateTime.Now, type, kind, correctValue, wrongValue));

    private double NextActiveSeconds(FaultState state) =>
        state.Mode == FaultMode.Random ? RandomizedAround(state.ActiveSeconds) : state.ActiveSeconds;

    private double NextGapSeconds(FaultState state) =>
        state.Mode == FaultMode.Random ? RandomizedAround(state.GapSeconds) : state.GapSeconds;

    private double RandomizedAround(double avgSeconds)
    {
        double u = Math.Max(1e-9, _rng.NextDouble());
        return Math.Max(0.2, -Math.Log(u) * avgSeconds);
    }
}
