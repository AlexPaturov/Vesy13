namespace Vesy13.Models;

/// <summary>
/// Поправочные коэффициенты направления: разные для движения вправо и влево.
/// Активная строка определяется через IsActive и DeletedAt; нулевой коэффициент валиден.
/// </summary>
public class DirectionCorrectionProfile
{
    public int       Id                             { get; set; }
    public double    RightDirectionCorrectionFactor { get; set; }
    public double    LeftDirectionCorrectionFactor  { get; set; }
    public bool      IsActive                       { get; set; }
    public DateTime  CreatedAt                      { get; set; }
    public DateTime? DeletedAt                      { get; set; }
}
