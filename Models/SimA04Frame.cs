namespace Vesy13.Models;

/// <summary>
/// Один пакет данных от АЦП «СИМ А04» — результат одного цикла опроса.
/// Содержит показания двух тензодатчиков в сырых кодах АЦП.
/// </summary>
public readonly struct SimA04Frame
{
    /// <summary>Канал 0 — тележка 1. Сырой код АЦП (0…65535).</summary>
    public int  Ch0   { get; }

    /// <summary>Канал 1 — тележка 2. Сырой код АЦП (0…65535).</summary>
    public int  Ch1   { get; }

    /// <summary>True если пакет корректен (длина ровно 4 байта).</summary>
    public bool Valid { get; }

    private SimA04Frame(int ch0, int ch1) { Ch0 = ch0; Ch1 = ch1; Valid = true; }

    /// <summary>
    /// Разбирает 4-байтовый ответ прибора.
    /// Протокол: CH0 = byte[1]*256 + byte[0], CH1 = byte[3]*256 + byte[2].
    /// При длине не равной 4 возвращает невалидный фрейм (<see cref="Valid"/> = false).
    /// </summary>
    public static SimA04Frame Parse(byte[] data)
    {
        if (data.Length != 4) return default;
        return new SimA04Frame(data[1] * 256 + data[0], data[3] * 256 + data[2]);
    }
}
