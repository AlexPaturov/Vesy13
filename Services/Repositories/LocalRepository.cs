using System.Text.Json;
using Dapper;
using Npgsql;
using Vesy13.Models;

namespace Vesy13.Services.Repositories;

/// <summary>
/// Репозиторий локальной PostgreSQL-базы (scale_db).
/// Хранит профиль калибровки и журнал взвешиваний вагонов.
/// </summary>
public class LocalRepository
{
    private const string ConnStr =
        "Host=localhost;Port=5432;Database=scale_db;Username=scale_user;Password=fluffyBunny";

    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = false };

    /// <summary>Текущий профиль калибровки. Заполняется после <see cref="LoadAsync"/>.</summary>
    public CalibrationProfile Profile { get; private set; } = new();

    /// <summary>
    /// Загружает профиль калибровки из таблицы <c>calibration_config</c>.
    /// При отсутствии записи или ошибке подключения оставляет профиль по умолчанию.
    /// </summary>
    public async Task LoadAsync()
    {
        try
        {
            await using var conn = new NpgsqlConnection(ConnStr);
            await conn.OpenAsync();
            var json = await conn.QueryFirstOrDefaultAsync<string>(
                "SELECT profile FROM calibration_config WHERE id = 1");
            if (!string.IsNullOrWhiteSpace(json) && json != "{}")
                Profile = JsonSerializer.Deserialize<CalibrationProfile>(json) ?? new CalibrationProfile();
        }
        catch
        {
            Profile = new CalibrationProfile();
        }
    }

    /// <summary>
    /// Сохраняет текущий <see cref="Profile"/> в таблицу <c>calibration_config</c>.
    /// Использует INSERT … ON CONFLICT UPDATE, поэтому запись всегда одна (id = 1).
    /// </summary>
    public async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(Profile, JsonOpts);
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        await conn.ExecuteAsync(@"
            INSERT INTO calibration_config (id, profile, updated_at)
            VALUES (1, @profile::jsonb, NOW())
            ON CONFLICT (id) DO UPDATE SET profile = EXCLUDED.profile, updated_at = NOW()",
            new { profile = json });
    }

    /// <summary>
    /// Сохраняет взвешивание вагона в таблицу <c>wagon_weighing</c>.
    /// </summary>
    /// <param name="record">Данные вагона: тележки, время, номер в составе.</param>
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

    /// <summary>
    /// Возвращает записи из <c>wagon_weighing</c>, ещё не перенесённые в Firebird
    /// (<c>transferred = false</c>), отсортированные по убыванию времени взвешивания.
    /// </summary>
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
            ORDER BY wagon_time DESC");
        return rows.ToList();
    }

    /// <summary>
    /// Отмечает запись как перенесённую в Firebird (<c>transferred = true</c>).
    /// </summary>
    /// <param name="id">Первичный ключ записи в <c>wagon_weighing</c>.</param>
    public async Task MarkTransferredAsync(int id)
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        await conn.ExecuteAsync(
            "UPDATE wagon_weighing SET transferred = true WHERE id = @id",
            new { id });
    }

    /// <summary>
    /// Возвращает последние 200 записей из <c>wagon_weighing</c> с флагом
    /// <c>transferred = true</c>, отсортированных по убыванию времени взвешивания.
    /// </summary>
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
}
