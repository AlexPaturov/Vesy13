using Vesy13.Models;
using Vesy13.Services.Hardware;

namespace Vesy13.Application;

public static class CalibrationCalculator
{
    public static double Convert(CalibrationProfile profile, int adcCode, ActiveChannel channel)
        => channel == ActiveChannel.Main
            ? profile.Ch0.Convert(adcCode)
            : profile.Ch1.Convert(adcCode);

    public static double ConvertDynamic(CalibrationProfile profile, int adcCode, string direction)
        => (direction.StartsWith("→") ? profile.Dynamic.KPlus : profile.Dynamic.KMinus) * adcCode;

    public static (double k, double b) CalculateLsq(IList<CalibrationPoint> points)
    {
        int n = points.Count;
        if (n < 2) return (0, 0);

        double sumX = 0, sumY = 0, sumXY = 0, sumXX = 0;
        foreach (var p in points)
        {
            sumX  += p.AdcCode;
            sumY  += p.MassTonnes;
            sumXY += (double)p.AdcCode * p.MassTonnes;
            sumXX += (double)p.AdcCode * p.AdcCode;
        }
        double denom = n * sumXX - sumX * sumX;
        double k     = denom != 0 ? (n * sumXY - sumX * sumY) / denom : 0;
        double b     = (sumY - k * sumX) / n;
        return (k, b);
    }
}
