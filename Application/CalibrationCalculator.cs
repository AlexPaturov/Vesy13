using Vesy13.Models;
using Vesy13.Services.Hardware;

namespace Vesy13.Application;

/// <summary>
/// Пересчёт кода АЦП в тонны по набору калибровочных точек.
/// Использует кусочно-линейную интерполяцию по активным точкам канала,
/// отсортированным по возрастанию кода АЦП.
/// </summary>
public static class CalibrationCalculator
{
    public static double Convert(IEnumerable<CalibPoint> points, int adcCode, ActiveChannel channel)
    {
        int ch = channel == ActiveChannel.Main ? 0 : 1;
        var active = points
            .Where(p => p.Channel == ch && p.IsActive)
            .OrderBy(p => p.AdcCode)
            .ToList();

        if (active.Count == 0) return 0;
        if (active.Count == 1) return (double)active[0].Mass;

        if (adcCode <= active[0].AdcCode)
            return Interp(active[0], active[1], adcCode);
        if (adcCode >= active[^1].AdcCode)
            return Interp(active[^2], active[^1], adcCode);

        for (int i = 0; i < active.Count - 1; i++)
            if (adcCode >= active[i].AdcCode && adcCode <= active[i + 1].AdcCode)
                return Interp(active[i], active[i + 1], adcCode);

        return 0;
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

    private static double Interp(CalibPoint p1, CalibPoint p2, int x)
    {
        if (p2.AdcCode == p1.AdcCode) return (double)p1.Mass;
        double t = (double)(x - p1.AdcCode) / (p2.AdcCode - p1.AdcCode);
        return (double)p1.Mass + t * ((double)p2.Mass - (double)p1.Mass);
    }
}
