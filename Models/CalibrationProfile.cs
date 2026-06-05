namespace Vesy13.Models;

/// <summary>Пара (код АЦП → масса в тоннах) — опорная точка тарировочной характеристики.</summary>
public class CalibrationPoint
{
    public int    AdcCode    { get; set; }
    public double MassTonnes { get; set; }
}

/// <summary>Тарировка одного канала АЦП: линейная функция y = K·x + B и набор опорных точек для МНК.</summary>
public class ChannelCalib
{
    public double K { get; set; }
    public double B { get; set; }
    public List<CalibrationPoint> Points { get; set; } = new();

    public double Convert(int adcCode) => K * adcCode + B;
}

/// <summary>
/// Коэффициенты динамического взвешивания: разные для движения → и ←,
/// т.к. инерция платформы несимметрична.
/// </summary>
public class DynamicCalib
{
    public double KPlus  { get; set; }  // коэффициент для →
    public double KMinus { get; set; }  // коэффициент для ←
}

/// <summary>Полный профиль калибровки весов: два статических канала (CH0/CH1) и динамика.</summary>
public class CalibrationProfile
{
    public ChannelCalib Ch0     { get; set; } = new();
    public ChannelCalib Ch1     { get; set; } = new();
    public DynamicCalib Dynamic { get; set; } = new();
}
