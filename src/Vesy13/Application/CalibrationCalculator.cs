using Vesy13.Models;
using Vesy13.Services.Hardware;

namespace Vesy13.Application;

/// <summary>
/// Пересчёт кода АЦП в тонны по активным калибровочным точкам текущего канала.
/// Ненулевые точки образуют кусочно-линейную зависимость массы от кода АЦП.
/// Для значений вне диапазона используется крайний отрезок.
/// Нулевая точка массы задаёт тару (смещение кода АЦП) и не является точкой масштаба.
/// Расчёт выполняется линейно между соседними точками.
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
        if (scalePoints.Count == 1)
        {
            var only = scalePoints[0];
            double tonnesCalculatSataic = (adcCode - zeroCode) * ((double)only.CalibrationValue / 65535d);
            return new StaticCalibrationResult(only, tonnesCalculatSataic, active.Count);
        }

        CalibPoint left;
        CalibPoint right;
        if (adcCode <= scalePoints[0].AdcCode)
        {
            left = scalePoints[0];
            right = scalePoints[1];
        }
        else if (adcCode >= scalePoints[^1].AdcCode)
        {
            left = scalePoints[^2];
            right = scalePoints[^1];
        }
        else
        {
            int rightIndex = scalePoints.FindIndex(p => p.AdcCode >= adcCode);
            left = scalePoints[rightIndex - 1];
            right = scalePoints[rightIndex];
        }

        double leftCode = left.AdcCode - zeroCode;
        double rightCode = right.AdcCode - zeroCode;
        double position = (adcCode - zeroCode - leftCode) / (rightCode - leftCode);
        double tonnes = (double)left.Mass + position * ((double)right.Mass - (double)left.Mass);
        var point = right;
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
