using System.Text.Json;
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

    /// <summary>
    /// Сохраняет текущий <see cref="Profile"/> в таблицу <c>calibration_config</c>.
    /// Использует INSERT … ON CONFLICT UPDATE, поэтому запись всегда одна (id = 1).
    /// </summary>
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

    /// <summary>
    /// Сохраняет взвешивание вагона в таблицу <c>wagon_weighing</c>.
    /// </summary>
    /// <param name="record">Данные вагона: тележки, время, номер в составе.</param>
    /// <param name="mode">Режим взвешивания: «СТАТИКА» или «ДИНАМИКА».</param>
    public async Task SaveWagonAsync(LocalWagon record, string mode)
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();

        const string sql = @"
INSERT INTO wagon_weighing (train_time, wagon_time, wagon_num, bogie1, bogie2, total, direction, mode)
VALUES (@trainTime, @wagonTime, @wagonNum, @bogie1, @bogie2, @total, @direction, @mode)";

        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("trainTime", record.TrainTime);
        cmd.Parameters.AddWithValue("wagonTime",  record.WagonTime);
        cmd.Parameters.AddWithValue("wagonNum",   record.Number);
        cmd.Parameters.AddWithValue("bogie1",     (decimal)record.Bogie1);
        cmd.Parameters.AddWithValue("bogie2",     (decimal)record.Bogie2);
        cmd.Parameters.AddWithValue("total",      (decimal)record.Total);
        cmd.Parameters.AddWithValue("direction",  record.Direction);
        cmd.Parameters.AddWithValue("mode",       mode);
        await cmd.ExecuteNonQueryAsync();
    }

    /// <summary>
    /// Возвращает записи из <c>wagon_weighing</c>, ещё не перенесённые в Firebird
    /// (<c>transferred = false</c>), отсортированные по убыванию времени взвешивания.
    /// </summary>
    public async Task<List<LocalWagon>> GetPendingAsync()
    {
        var rows = new List<LocalWagon>();
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();

        const string sql = @"
SELECT id, train_time, wagon_time, wagon_num, bogie1, bogie2, direction, mode
FROM wagon_weighing
WHERE transferred = false
ORDER BY wagon_time DESC";

        await using var cmd = new NpgsqlCommand(sql, conn);
        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
        {
            rows.Add(new LocalWagon(
                Number:    rdr.GetInt32(3),
                TrainTime: rdr.GetDateTime(1),
                WagonTime: rdr.GetDateTime(2),
                Bogie1:    (double)rdr.GetDecimal(4),
                Bogie2:    (double)rdr.GetDecimal(5),
                Direction: rdr.IsDBNull(6) ? "" : rdr.GetString(6))
            {
                Id   = rdr.GetInt32(0),
                Mode = rdr.GetString(7),
            });
        }
        return rows;
    }

    /// <summary>
    /// Отмечает запись как перенесённую в Firebird (<c>transferred = true</c>).
    /// </summary>
    /// <param name="id">Первичный ключ записи в <c>wagon_weighing</c>.</param>
    public async Task MarkTransferredAsync(int id)
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        const string sql = "UPDATE wagon_weighing SET transferred = true WHERE id = @id";
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", id);
        await cmd.ExecuteNonQueryAsync();
    }

    /// <summary>
    /// Возвращает последние 200 записей из <c>wagon_weighing</c> с флагом
    /// <c>transferred = true</c>, отсортированных по убыванию времени взвешивания.
    /// </summary>
    public async Task<List<LocalWagon>> GetTransferredAsync()
    {
        var rows = new List<LocalWagon>();
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();

        const string sql = @"
SELECT id, train_time, wagon_time, wagon_num, bogie1, bogie2, direction, mode
FROM wagon_weighing
WHERE transferred = true
ORDER BY wagon_time DESC
LIMIT 200";

        await using var cmd = new NpgsqlCommand(sql, conn);
        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
        {
            rows.Add(new LocalWagon(
                Number:    rdr.GetInt32(3),
                TrainTime: rdr.GetDateTime(1),
                WagonTime: rdr.GetDateTime(2),
                Bogie1:    (double)rdr.GetDecimal(4),
                Bogie2:    (double)rdr.GetDecimal(5),
                Direction: rdr.IsDBNull(6) ? "" : rdr.GetString(6))
            {
                Id   = rdr.GetInt32(0),
                Mode = rdr.GetString(7),
            });
        }
        return rows;
    }
}
