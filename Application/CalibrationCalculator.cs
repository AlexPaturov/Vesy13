using Vesy13.Models;
using Vesy13.Services.Hardware;

namespace Vesy13.Application;

/// <summary>
/// Пересчёт кода АЦП в тонны по активным калибровочным точкам текущего канала.
/// Каждая точка задаёт собственное калибровочное число.
/// Точки сортируются по коду АЦП. Для расчёта выбирается последняя точка,
/// код которой меньше или равен текущему коду АЦП. Если текущий код ниже первой
/// точки, используется первая точка; если выше последней — последняя.
/// Нулевая точка массы задаёт тару (смещение кода АЦП) и не является точкой масштаба.
/// Вес считается без интерполяции: (текущий_код_АЦП - код_тары) * калибровочное_число / 65535.
/// Результат может быть отрицательным, если текущий код ниже кода тары.
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

        var scalePoints = active.Where(p => p.Mass != 0).ToList();
        if (scalePoints.Count == 0) return null;

        int zeroCode = active.FirstOrDefault(p => p.Mass == 0)?.AdcCode ?? 0;
        var point = scalePoints[0];

        foreach (var p in scalePoints)
        {
            if (adcCode >= p.AdcCode)
                point = p;
            else
                break;
        }

        int correctedCode = adcCode - zeroCode;
        double tonnes = correctedCode * ((double)point.CalibrationValue / 65535d);
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
