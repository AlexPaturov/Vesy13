namespace Vesy13.Models;

/// <summary>
/// Коэффициенты динамического взвешивания: разные для движения → и ←.
/// Активная строка определяется через IsActive и DeletedAt; нулевой коэффициент валиден.
/// </summary>
public class DynamicCalib
{
    public int       Id        { get; set; }
    public double    KPlus     { get; set; }
    public double    KMinus    { get; set; }
    public bool      IsActive  { get; set; }
    public DateTime  CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
