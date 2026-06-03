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
}
