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
                SELECT id, channel, adc_code AS AdcCode, CAST(mass AS float8) AS Mass, is_active AS IsActive, deleted_at AS DeletedAt
                FROM calibration_points
                ORDER BY channel, adc_code");
            CalibPoints = pts.ToList().AsReadOnly();

            Dynamic = await LoadActiveDynamicCalibAsync(conn);
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
            SELECT
                id,
                channel,
                adc_code AS AdcCode,
                CAST(mass AS float8) AS Mass,
                is_active AS IsActive,
                deleted_at AS DeletedAt
            FROM calibration_points
            WHERE channel = @channel
            ORDER BY adc_code",
            new { channel });
        return pts.ToList();
    }

    /// <summary>
    /// Сохраняет точки канала без физического удаления: существующие обновляются, новые добавляются.
    /// Неактивные точки получают deleted_at как отметку мягкого удаления.
    /// </summary>
    public async Task SaveCalibPointsAsync(int channel, IEnumerable<CalibPoint> points)
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        await using var tx = await conn.BeginTransactionAsync();

        foreach (var p in points)
        {
            DateTime? deletedAt = p.IsActive ? null : p.DeletedAt ?? DateTime.Now;
            if (p.Id > 0)
            {
                await conn.ExecuteAsync(@"
                    UPDATE calibration_points
                    SET adc_code = @AdcCode,
                        mass = @Mass,
                        is_active = CASE WHEN deleted_at IS NOT NULL THEN FALSE ELSE @IsActive END,
                        deleted_at = CASE
                            WHEN deleted_at IS NOT NULL THEN deleted_at
                            ELSE @DeletedAt
                        END
                    WHERE id = @Id AND channel = @channel",
                    new { p.Id, channel, p.AdcCode, p.Mass, p.IsActive, DeletedAt = deletedAt }, tx);
            }
            else
            {
                await conn.ExecuteAsync(@"
                    INSERT INTO calibration_points (channel, adc_code, mass, is_active, deleted_at)
                    VALUES (@channel, @AdcCode, @Mass, @IsActive, @DeletedAt)",
                    new { channel, p.AdcCode, p.Mass, p.IsActive, DeletedAt = deletedAt }, tx);
            }
        }

        await tx.CommitAsync();
        await ReloadCacheAsync(conn);
    }

    /// <summary>Переключает флаг активности одной точки и обновляет кэш.</summary>
    public async Task SetActiveAsync(int id, bool isActive)
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        await conn.ExecuteAsync(@"
            UPDATE calibration_points
            SET is_active = CASE WHEN deleted_at IS NOT NULL THEN FALSE ELSE @isActive END,
                deleted_at = CASE
                    WHEN deleted_at IS NOT NULL THEN deleted_at
                    WHEN @isActive THEN NULL
                    ELSE NOW()
                END
            WHERE id = @id",
            new { id, isActive });
        await ReloadCacheAsync(conn);
    }

    // ── Dynamic calibration ────────────────────────────────────────────────

    public async Task<List<DynamicCalib>> GetDynamicCalibsAsync()
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        var rows = await conn.QueryAsync<DynamicCalib>(@"
            SELECT id,
                   k_plus     AS KPlus,
                   k_minus    AS KMinus,
                   is_active  AS IsActive,
                   created_at AS CreatedAt,
                   deleted_at AS DeletedAt
            FROM calibration_dynamic
            ORDER BY is_active DESC, created_at DESC, id DESC");
        return rows.ToList();
    }

    public async Task SaveDynamicCalibAsync(DynamicCalib calib)
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        await using var tx = await conn.BeginTransactionAsync();

        await conn.ExecuteAsync(@"
            UPDATE calibration_dynamic
            SET is_active = FALSE,
                deleted_at = COALESCE(deleted_at, NOW()),
                updated_at = NOW()
            WHERE is_active = TRUE AND deleted_at IS NULL", transaction: tx);

        int id = await conn.ExecuteScalarAsync<int>(@"
            INSERT INTO calibration_dynamic (k_plus, k_minus, is_active, created_at, deleted_at, updated_at)
            VALUES (@KPlus, @KMinus, TRUE, NOW(), NULL, NOW())
            RETURNING id",
            new { calib.KPlus, calib.KMinus }, tx);

        await tx.CommitAsync();
        Dynamic = await LoadActiveDynamicCalibAsync(conn);
        calib.Id = id;
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
                   mode                    AS Mode,
                   transferred             AS Transferred
            FROM wagon_weighing
            WHERE transferred = false
            ORDER BY train_time ASC, wagon_num ASC");
        return rows.ToList();
    }


    public async Task<List<LocalWagon>> GetAllByTrainTimeAsync(DateTime trainTime)
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
                   mode                    AS Mode,
                   transferred             AS Transferred
            FROM wagon_weighing
            WHERE date_trunc('second', train_time) = date_trunc('second', @trainTime)
            ORDER BY train_time ASC, wagon_num ASC",
            new { trainTime });
        return rows.ToList();
    }

    public async Task<List<LocalWagon>> GetAllByDateAsync(DateTime date)
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
                   mode                    AS Mode,
                   transferred             AS Transferred
            FROM wagon_weighing
            WHERE train_time::date = @date
            ORDER BY train_time ASC, wagon_num ASC",
            new { date = date.Date });
        return rows.ToList();
    }

    public async Task<List<LocalWagon>> GetPendingByTrainTimeAsync(DateTime trainTime)
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
                   mode                    AS Mode,
                   transferred             AS Transferred
            FROM wagon_weighing
            WHERE transferred = false
              AND date_trunc('second', train_time) = date_trunc('second', @trainTime)
            ORDER BY train_time ASC, wagon_num ASC",
            new { trainTime });
        return rows.ToList();
    }

    public async Task<List<LocalWagon>> GetPendingByDateAsync(DateTime date)
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
                   mode                    AS Mode,
                   transferred             AS Transferred
            FROM wagon_weighing
            WHERE transferred = false
              AND train_time::date = @date
            ORDER BY train_time ASC, wagon_num ASC",
            new { date = date.Date });
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
                   mode                    AS Mode,
                   transferred             AS Transferred
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
            SELECT id, channel, adc_code AS AdcCode, CAST(mass AS float8) AS Mass, is_active AS IsActive, deleted_at AS DeletedAt
            FROM calibration_points
            ORDER BY channel, adc_code");
        CalibPoints = pts.ToList().AsReadOnly();
        Dynamic = await LoadActiveDynamicCalibAsync(conn);
    }

    private static async Task<DynamicCalib> LoadActiveDynamicCalibAsync(NpgsqlConnection conn)
    {
        var dyn = await conn.QueryFirstOrDefaultAsync<DynamicCalib>(@"
            SELECT id,
                   k_plus     AS KPlus,
                   k_minus    AS KMinus,
                   is_active  AS IsActive,
                   created_at AS CreatedAt,
                   deleted_at AS DeletedAt
            FROM calibration_dynamic
            WHERE is_active = TRUE AND deleted_at IS NULL
            ORDER BY created_at DESC, id DESC
            LIMIT 1");
        return dyn ?? new DynamicCalib();
    }
}
