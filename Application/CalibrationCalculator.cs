using Vesy13.Models;
using Vesy13.Services.Hardware;

namespace Vesy13.Application;

/// <summary>
/// Пересчёт кода АЦП в тонны по активным калибровочным точкам текущего канала.
/// Каждая точка задаёт собственное калибровочное число.
/// Точки сортируются по коду АЦП. Для расчёта выбирается последняя точка,
/// код которой меньше или равен текущему коду АЦП. Если текущий код ниже первой
/// точки, используется первая точка; если выше последней — последняя.
/// Вес считается без интерполяции: текущий_код_АЦП * калибровочное_число / 65535.
/// </summary>
public static class CalibrationCalculator
{
    /// <summary>
    /// Возвращает null, если для канала нет ни одной активной калибровочной точки —
    /// это отличает «калибровка не задана» от легитимного нулевого результата расчёта.
    /// </summary>
    public static StaticCalibrationResult? CalculateStatic(IEnumerable<CalibPoint> points, int adcCode, ActiveChannel channel)
    {
        int ch = channel == ActiveChannel.Main ? 0 : 1;
        var active = points
            .Where(p => p.Channel == ch && p.IsActive)
            .OrderBy(p => p.AdcCode)
            .ToList();

        if (active.Count == 0) return null;
        var point = active[0];

        foreach (var p in active)
        {
            if (adcCode >= p.AdcCode)
                point = p;
            else
                break;
        }

        double tonnes = adcCode * ((double)point.CalibrationValue / 65535d);
        return new StaticCalibrationResult(point, tonnes, active.Count);
    }

    /// <summary>Возвращает только вес для существующих вызовов.</summary>
    public static double? Convert(IEnumerable<CalibPoint> points, int adcCode, ActiveChannel channel)
        => CalculateStatic(points, adcCode, channel)?.Tonnes;

    public static double ApplyDirectionCorrection(double staticTonnes, DirectionCorrectionProfile profile, Direction direction)
        => staticTonnes * (direction == Direction.Right
            ? profile.RightDirectionCorrectionFactor
            : profile.LeftDirectionCorrectionFactor);
}
