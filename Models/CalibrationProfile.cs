namespace Vesy13.Models;

/// <summary>
/// Коэффициенты динамического взвешивания: разные для движения → и ←,
/// т.к. инерция платформы несимметрична.
/// </summary>
public class DynamicCalib
{
    public double KPlus  { get; set; }
    public double KMinus { get; set; }
}
