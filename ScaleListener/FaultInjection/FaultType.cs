namespace ScaleListener.FaultInjection;

/// <summary>
/// Порядок объявления = порядок реализации/приоритет: от простого и связанного
/// с уже проверенной работой (Silence) до самого сложного (Corrupt).
/// </summary>
public enum FaultType
{
    /// <summary>Эмулятор перестаёт отвечать на время, порт не рвётся.</summary>
    Silence,

    /// <summary>Одиночный аномальный скачок значения на один ответ/сэмпл.</summary>
    Spike,

    /// <summary>Управляемый дрейф/колебание сигнала на протяжении окна (дребезг).</summary>
    Drift,

    /// <summary>Порча сырых байт / обрыв кадра на середине (рассинхронизация потока).</summary>
    Corrupt,

    /// <summary>Значение "застревает" на одном коде на протяжении окна.</summary>
    Stuck,
}

/// <summary>
/// Discrete - разовое срабатывание (одно событие в истории).
/// Continuous - активное окно с началом и концом (два события в истории на цикл).
/// </summary>
public enum FaultKind { Discrete, Continuous }

public enum FaultMode { Off, Periodic, Random, Manual }

public enum FaultEventKind { Fired, Started, Stopped }
