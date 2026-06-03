using System.Globalization;
using System.Text.Json;
using Vesy13.Models;

namespace Vesy13.Services;

public class CalibrationService
{
    private static readonly string FilePath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory, "calibration.json");

    private static readonly JsonSerializerOptions JsonOpts =
        new() { WriteIndented = true };

    public CalibrationProfile Profile { get; private set; }

    public CalibrationService() => Profile = Load();

    public double Convert(int adcCode, ActiveChannel channel)
        => channel == ActiveChannel.Main
            ? Profile.Ch0.Convert(adcCode)
            : Profile.Ch1.Convert(adcCode);

    public double ConvertDynamic(int adcCode, string direction)
        => (direction == "→" ? Profile.Dynamic.KPlus : Profile.Dynamic.KMinus) * adcCode;

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

    public void Save() =>
        File.WriteAllText(FilePath, JsonSerializer.Serialize(Profile, JsonOpts));

    private static CalibrationProfile Load()
    {
        if (!File.Exists(FilePath)) return new CalibrationProfile();
        try
        {
            return JsonSerializer.Deserialize<CalibrationProfile>(
                       File.ReadAllText(FilePath)) ?? new CalibrationProfile();
        }
        catch { return new CalibrationProfile(); }
    }
}