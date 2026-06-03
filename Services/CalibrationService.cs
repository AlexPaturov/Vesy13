using System.Globalization;
using System.Text.Json;
using Npgsql;
using Vesy13.Models;

namespace Vesy13.Services;

public class CalibrationService
{
    private const string ConnStr =
        "Host=localhost;Port=5432;Database=scale_db;Username=scale_user;Password=fluffyBunny";

    private static readonly JsonSerializerOptions JsonOpts =
        new() { WriteIndented = false };

    public CalibrationProfile Profile { get; private set; } = new();

    public async Task LoadAsync()
    {
        try
        {
            await using var conn = new NpgsqlConnection(ConnStr);
            await conn.OpenAsync();
            const string sql = "SELECT profile FROM calibration_config WHERE id = 1";
            await using var cmd = new NpgsqlCommand(sql, conn);
            var json = (string?)await cmd.ExecuteScalarAsync();
            if (!string.IsNullOrWhiteSpace(json) && json != "{}")
                Profile = JsonSerializer.Deserialize<CalibrationProfile>(json) ?? new CalibrationProfile();
        }
        catch
        {
            Profile = new CalibrationProfile();
        }
    }

    public async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(Profile, JsonOpts);
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        const string sql = @"
INSERT INTO calibration_config (id, profile, updated_at)
VALUES (1, @profile::jsonb, NOW())
ON CONFLICT (id) DO UPDATE SET profile = EXCLUDED.profile, updated_at = NOW()";
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("profile", json);
        await cmd.ExecuteNonQueryAsync();
    }

    public double Convert(int adcCode, ActiveChannel channel)
        => channel == ActiveChannel.Main
            ? Profile.Ch0.Convert(adcCode)
            : Profile.Ch1.Convert(adcCode);

    public double ConvertDynamic(int adcCode, string direction)
        => (direction.StartsWith("→") ? Profile.Dynamic.KPlus : Profile.Dynamic.KMinus) * adcCode;

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
