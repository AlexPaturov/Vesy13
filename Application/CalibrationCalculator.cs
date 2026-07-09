using Vesy13.Models;
using Vesy13.Services.Hardware;

namespace Vesy13.Application;

/// <summary>
/// Пересчёт кода АЦП в тонны по активным калибровочным точкам текущего канала.
/// Каждая точка задаёт собственный коэффициент: масса_точки / код_АЦП_точки.
/// Точки сортируются по коду АЦП. Для расчёта выбирается последняя точка,
/// код которой меньше или равен текущему коду АЦП. Если текущий код ниже первой
/// точки, используется первая точка; если выше последней — последняя.
/// Вес считается без интерполяции: текущий_код_АЦП * коэффициент_выбранной_точки.
/// </summary>
public static class CalibrationCalculator
{
    /// <summary>
    /// Возвращает null, если для канала нет ни одной активной калибровочной точки —
    /// это отличает «калибровка не задана» от легитимного нулевого результата расчёта.
    /// </summary>
    public static double? Convert(IEnumerable<CalibPoint> points, int adcCode, ActiveChannel channel)
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

        return point.AdcCode == 0
            ? 0
            : adcCode * ((double)point.Mass / point.AdcCode);
    }

    public static (double K, double B) CalculateLsq(IEnumerable<(int AdcCode, decimal Mass, bool IsActive)> points)
    {
        var list = points.ToList();
        int n = list.Count;
        if (n < 2) return (0, 0);
        double sumX  = list.Sum(p => (double)p.AdcCode);
        double sumY  = list.Sum(p => (double)p.Mass);
        double sumXY = list.Sum(p => (double)p.AdcCode * (double)p.Mass);
        double sumX2 = list.Sum(p => (double)p.AdcCode * (double)p.AdcCode);
        double denom = n * sumX2 - sumX * sumX;
        if (denom == 0) return (0, 0);
        double k = (n * sumXY - sumX * sumY) / denom;
        double b = (sumY - k * sumX) / n;
        return (k, b);
    }

    public static double ConvertDynamic(DynamicCalib calib, int adcCode, string direction)
        => (direction.StartsWith("→") ? calib.KPlus : calib.KMinus) * adcCode;
}
