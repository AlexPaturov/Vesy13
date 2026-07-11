namespace ScaleListener.FaultInjection;

/// <summary>Настройки и планировочное состояние одного типа сбоя.</summary>
public sealed class FaultState
{
    public FaultType Type { get; }
    public FaultKind Kind { get; }

    public bool Enabled { get; set; }
    public FaultMode Mode { get; set; } = FaultMode.Off;

    /// <summary>Discrete + Periodic: интервал между срабатываниями, сек.</summary>
    public double IntervalSeconds { get; set; } = 10;

    /// <summary>Discrete + Random: средняя частота срабатываний в минуту.</summary>
    public double RatePerMinute { get; set; } = 3;

    /// <summary>Continuous: длительность активного окна, сек (или среднее значение в Random).</summary>
    public double ActiveSeconds { get; set; } = 3;

    /// <summary>Continuous: пауза между окнами, сек (или среднее значение в Random).</summary>
    public double GapSeconds { get; set; } = 15;

    /// <summary>Специфичный для сбоя параметр силы эффекта (амплитуда скачка/дрейфа и т.п.), единицы - тонны.</summary>
    public double Magnitude { get; set; } = 5;

    internal bool IsActive { get; set; }
    internal DateTime NextTransitionAt { get; set; } = DateTime.MinValue;
    internal DateTime LastOpportunityAt { get; set; } = DateTime.MinValue;

    /// <summary>Discrete: взведено кнопкой "Сработать сейчас" - сработает на ближайшей возможности, вне зависимости от Mode.</summary>
    internal bool ManualArmed { get; set; }

    public FaultState(FaultType type, FaultKind kind)
    {
        Type = type;
        Kind = kind;
    }
}
