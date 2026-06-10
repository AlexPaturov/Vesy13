using Dapper;
using Npgsql;
using Vesy13.Models;

namespace Vesy13.Services.Repositories;

/// <summary>
/// Репозиторий локальной PostgreSQL-базы (scale_db).
/// Хранит калибровочные точки и журнал взвешиваний вагонов.
/// </summary>
public class LocalRepository
{
    private const string ConnStr =
        "Host=localhost;Port=5432;Database=scale_db;Username=scale_user;Password=fluffyBunny";

    /// <summary>Кэш всех калибровочных точек. Обновляется после каждого сохранения.</summary>
    public IReadOnlyList<CalibPoint> CalibPoints { get; private set; } = [];

    /// <summary>Коэффициенты динамической калибровки.</summary>
    public DynamicCalib Dynamic { get; private set; } = new();

    // ── Load ───────────────────────────────────────────────────────────────

    public async Task LoadAsync()
    {
        try
        {
            await using var conn = new NpgsqlConnection(ConnStr);
            await conn.OpenAsync();

            var pts = await conn.QueryAsync<CalibPoint>(@"
                SELECT id, channel, adc_code AS AdcCode, CAST(mass AS float8) AS Mass, is_active AS IsActive
                FROM calibration_points
                ORDER BY channel, adc_code");
            CalibPoints = pts.ToList().AsReadOnly();

            var dyn = await conn.QueryFirstOrDefaultAsync<DynamicCalib>(@"
                SELECT k_plus AS KPlus, k_minus AS KMinus
                FROM calibration_dynamic WHERE id = 1");
            Dynamic = dyn ?? new DynamicCalib();
        }
        catch
        {
            CalibPoints = [];
            Dynamic     = new DynamicCalib();
        }
    }

    // ── Calibration points ─────────────────────────────────────────────────

    /// <summary>Возвращает все точки указанного канала из БД.</summary>
    public async Task<List<CalibPoint>> GetCalibPointsAsync(int channel)
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        var pts = await conn.QueryAsync<CalibPoint>(@"
            SELECT id, channel, adc_code AS AdcCode, CAST(mass AS float8) AS Mass, is_active AS IsActive
            FROM calibration_points
            WHERE channel = @channel
            ORDER BY adc_code",
            new { channel });
        return pts.ToList();
    }

    /// <summary>
    /// Заменяет все точки канала на переданный набор (DELETE + INSERT в транзакции).
    /// После коммита обновляет кэш <see cref="CalibPoints"/>.
    /// </summary>
    public async Task SaveCalibPointsAsync(int channel, IEnumerable<(int AdcCode, decimal Mass, bool IsActive)> points)
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        await using var tx = await conn.BeginTransactionAsync();

        await conn.ExecuteAsync(
            "DELETE FROM calibration_points WHERE channel = @channel",
            new { channel }, tx);

        foreach (var p in points)
            await conn.ExecuteAsync(@"
                INSERT INTO calibration_points (channel, adc_code, mass, is_active)
                VALUES (@channel, @AdcCode, @Mass, @IsActive)",
                new { channel, p.AdcCode, p.Mass, p.IsActive }, tx);

        await tx.CommitAsync();
        await ReloadCacheAsync(conn);
    }

    /// <summary>Переключает флаг активности одной точки и обновляет кэш.</summary>
    public async Task SetActiveAsync(int id, bool isActive)
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        await conn.ExecuteAsync(
            "UPDATE calibration_points SET is_active = @isActive WHERE id = @id",
            new { id, isActive });
        await ReloadCacheAsync(conn);
    }

    // ── Dynamic calibration ────────────────────────────────────────────────

    public async Task SaveDynamicCalibAsync(DynamicCalib calib)
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        await conn.ExecuteAsync(@"
            INSERT INTO calibration_dynamic (id, k_plus, k_minus, updated_at)
            VALUES (1, @KPlus, @KMinus, NOW())
            ON CONFLICT (id) DO UPDATE
            SET k_plus = EXCLUDED.k_plus, k_minus = EXCLUDED.k_minus, updated_at = NOW()",
            new { calib.KPlus, calib.KMinus });
        Dynamic = calib;
    }

    // ── Wagon weighing ─────────────────────────────────────────────────────

    public async Task SaveWagonAsync(LocalWagon record)
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        await conn.ExecuteAsync(@"
            INSERT INTO wagon_weighing (train_time, wagon_time, wagon_num, bogie1, bogie2, total, direction, mode)
            VALUES (@TrainTime, @WagonTime, @Number, @Bogie1, @Bogie2, @Total, @Direction, @Mode)",
            new
            {
                record.TrainTime,
                record.WagonTime,
                record.Number,
                Bogie1 = (decimal)record.Bogie1,
                Bogie2 = (decimal)record.Bogie2,
                Total  = (decimal)record.Total,
                record.Direction,
                record.Mode,
            });
    }

    public async Task<List<LocalWagon>> GetPendingAsync()
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        var rows = await conn.QueryAsync<LocalWagon>(@"
            SELECT id,
                   train_time              AS TrainTime,
                   wagon_time              AS WagonTime,
                   wagon_num               AS Number,
                   CAST(bogie1 AS float8)  AS Bogie1,
                   CAST(bogie2 AS float8)  AS Bogie2,
                   COALESCE(direction, '') AS Direction,
                   mode                    AS Mode
            FROM wagon_weighing
            WHERE transferred = false
            ORDER BY wagon_num ASC, wagon_time ASC, id ASC");
        return rows.ToList();
    }

    public async Task MarkTransferredAsync(int id)
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        await conn.ExecuteAsync(
            "UPDATE wagon_weighing SET transferred = true WHERE id = @id",
            new { id });
    }

    public async Task<List<LocalWagon>> GetTransferredAsync()
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        var rows = await conn.QueryAsync<LocalWagon>(@"
            SELECT id,
                   train_time              AS TrainTime,
                   wagon_time              AS WagonTime,
                   wagon_num               AS Number,
                   CAST(bogie1 AS float8)  AS Bogie1,
                   CAST(bogie2 AS float8)  AS Bogie2,
                   COALESCE(direction, '') AS Direction,
                   mode                    AS Mode
            FROM wagon_weighing
            WHERE transferred = true
            ORDER BY wagon_time DESC
            LIMIT 200");
        return rows.ToList();
    }

    // ── Helpers ────────────────────────────────────────────────────────────

    private async Task ReloadCacheAsync(NpgsqlConnection conn)
    {
        var pts = await conn.QueryAsync<CalibPoint>(@"
            SELECT id, channel, adc_code AS AdcCode, CAST(mass AS float8) AS Mass, is_active AS IsActive
            FROM calibration_points
            ORDER BY channel, adc_code");
        CalibPoints = pts.ToList().AsReadOnly();
    }
}
