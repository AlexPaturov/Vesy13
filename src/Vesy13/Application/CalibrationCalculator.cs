using Vesy13.Models;
using Vesy13.Services.Hardware;

namespace Vesy13.Application;

/// <summary>
/// Пересчёт кода АЦП в тонны по активным калибровочным точкам текущего канала.
/// Ненулевые точки образуют кусочно-линейную зависимость массы от кода АЦП.
/// Для значений вне диапазона используется крайний отрезок.
/// Нулевая точка массы задаёт тару (смещение кода АЦП) и участвует в интерполяции как точка 0 т.
/// Расчёт выполняется линейно между соседними точками.
/// Результат может быть отрицательным, если текущий код ниже кода тары.
/// </summary>
public static class CalibrationCalculator
{
    /// <summary>
    /// Возвращает null, если для канала нет активных ненулевых калибровочных точек.
    /// zeroCode — ADC-код активной точки с Mass == 0; если нулевой точки нет, используется 0.
    /// Из adcCode вычитается только это смещение. Отрицательный результат не ограничивается.
    /// </summary>
    public static StaticCalibrationResult? CalculateStatic(IEnumerable<CalibPoint> points, int adcCode, ActiveChannel channel)
    {
        int ch = channel == ActiveChannel.Main ? 0 : 1;
        var active = points
            .Where(p => p.Channel == ch && p.IsActive)
            .OrderBy(p => p.AdcCode)
            .ToList();

        var nonZeroPoints = active.Where(p => p.Mass != 0).ToList();
        if (nonZeroPoints.Count == 0) return null;

        // Нулевая точка задаёт смещение ADC и участвует в первом интервале.
        int zeroCode = active.FirstOrDefault(p => p.Mass == 0)?.AdcCode ?? 0;
        var scalePoints = active.Any(p => p.Mass == 0) ? active : nonZeroPoints;
        if (nonZeroPoints.Count == 1 && !active.Any(p => p.Mass == 0))
        {
            var only = nonZeroPoints[0];
            double tonnesCalculatSataic = (adcCode - zeroCode) * ((double)only.CalibrationValue / 65535d);
            return new StaticCalibrationResult(only, tonnesCalculatSataic, active.Count);
        }

        CalibPoint lowerPoint;
        CalibPoint upperPoint;
        if (adcCode <= scalePoints[0].AdcCode)
        {
            lowerPoint = scalePoints[0];
            upperPoint = scalePoints[1];
        }
        else if (adcCode >= scalePoints[^1].AdcCode)
        {
            lowerPoint = scalePoints[^2];
            upperPoint = scalePoints[^1];
        }
        else
        {
            int upperIndex = scalePoints.FindIndex(p => p.AdcCode >= adcCode);
            lowerPoint = scalePoints[upperIndex - 1];
            upperPoint = scalePoints[upperIndex];
        }

        double lowerCode = lowerPoint.AdcCode - zeroCode;
        double upperCode = upperPoint.AdcCode - zeroCode;
        double position = (adcCode - zeroCode - lowerCode) / (upperCode - lowerCode);
        double tonnes = (double)lowerPoint.Mass + position * ((double)upperPoint.Mass - (double)lowerPoint.Mass);
        var point = upperPoint;
        return new StaticCalibrationResult(point, tonnes, active.Count);
    }

    /*
     * LEGACY ALGORITHM FOR COMPARISON
     *
     * To enable it: comment out the current CalculateStatic above and
     * uncomment this method. Both methods intentionally have the same
     * signature; enabling both must produce a duplicate-member compile error.
     */
    /*
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
     */

    /// <summary>Возвращает только вес для существующих вызовов.</summary>
    public static double? Convert(IEnumerable<CalibPoint> points, int adcCode, ActiveChannel channel)
        => CalculateStatic(points, adcCode, channel)?.Tonnes;

    public static double ApplyDirectionCorrection(double staticTonnes, DirectionCorrectionProfile profile, Direction direction)
        => staticTonnes * (direction == Direction.Right
            ? profile.RightDirectionCorrectionFactor
            : profile.LeftDirectionCorrectionFactor);
}
