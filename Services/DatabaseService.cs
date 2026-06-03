using Npgsql;
using Vesy13.Models;

namespace Vesy13.Services;

public class DatabaseService
{
    private const string ConnStr =
        "Host=localhost;Port=5432;Database=scale_db;Username=scale_user;Password=fluffyBunny";

    public async Task SaveWagonAsync(WagonRecord record, string mode)
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

    public async Task<List<WagonWeighingRow>> GetPendingAsync()
    {
        var rows = new List<WagonWeighingRow>();
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();

        const string sql = @"
SELECT id, train_time, wagon_time, wagon_num, bogie1, bogie2, total, direction, mode
FROM wagon_weighing
WHERE transferred = false
ORDER BY wagon_time DESC";

        await using var cmd = new NpgsqlCommand(sql, conn);
        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
        {
            rows.Add(new WagonWeighingRow
            {
                Id        = rdr.GetInt32(0),
                TrainTime = rdr.GetDateTime(1),
                WagonTime = rdr.GetDateTime(2),
                WagonNum  = rdr.GetInt32(3),
                Bogie1    = rdr.GetDecimal(4),
                Bogie2    = rdr.GetDecimal(5),
                Total     = rdr.GetDecimal(6),
                Direction = rdr.IsDBNull(7) ? "" : rdr.GetString(7),
                Mode      = rdr.GetString(8),
            });
        }
        return rows;
    }

    public async Task MarkTransferredAsync(int id)
    {
        await using var conn = new NpgsqlConnection(ConnStr);
        await conn.OpenAsync();
        const string sql = "UPDATE wagon_weighing SET transferred = true WHERE id = @id";
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", id);
        await cmd.ExecuteNonQueryAsync();
    }
}
