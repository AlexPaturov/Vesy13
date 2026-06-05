using System.Text.Json;
using Npgsql;
using Vesy13.Models;

namespace Vesy13.Services.Repositories;

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

}
