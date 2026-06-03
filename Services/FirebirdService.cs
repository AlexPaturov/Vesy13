using FirebirdSql.Data.FirebirdClient;
using Vesy13.Models;

namespace Vesy13.Services;

public class FirebirdService
{
    private const string ConnStr =
        @"DataSource=localhost;Database=C:\Work\dc_oper.fdb;User ID=sysdba;Password=masterkey;Charset=WIN1251";

    public async Task InsertAsync(
        string          table,
        WagonWeighingRow row,
        string          nvag,
        long?           ndok,
        string?         gruz,
        decimal?        tarDok)
    {
        decimal? netto = tarDok.HasValue ? row.Total - tarDok.Value : null;

        await using var conn = new FbConnection(ConnStr);
        await conn.OpenAsync();

        string sql = $@"
INSERT INTO {table}
    (DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_DOK, NETTO, VESY, NPP, V12, V34, REJVZVESH, WHEN_INSERT)
VALUES
    (@dt, @vr, @nvag, @ndok, @gruz, @brutto, @tardok, @netto, 13, @npp, @v12, @v34, @rejvzvesh, CURRENT_TIMESTAMP)";

        await using var cmd = new FbCommand(sql, conn);
        cmd.Parameters.AddWithValue("@dt",        row.WagonTime.Date);
        cmd.Parameters.AddWithValue("@vr",        row.WagonTime.TimeOfDay);
        cmd.Parameters.AddWithValue("@nvag",      nvag);
        cmd.Parameters.AddWithValue("@ndok",      ndok.HasValue ? (object)ndok.Value : DBNull.Value);
        cmd.Parameters.AddWithValue("@gruz",      (object?)gruz ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@brutto",    row.Total);
        cmd.Parameters.AddWithValue("@tardok",    tarDok.HasValue ? (object)tarDok.Value : DBNull.Value);
        cmd.Parameters.AddWithValue("@netto",     netto.HasValue  ? (object)netto.Value  : DBNull.Value);
        cmd.Parameters.AddWithValue("@npp",       (short)row.WagonNum);
        cmd.Parameters.AddWithValue("@v12",       row.Bogie1);
        cmd.Parameters.AddWithValue("@v34",       row.Bogie2);
        cmd.Parameters.AddWithValue("@rejvzvesh", row.Mode);
        await cmd.ExecuteNonQueryAsync();
    }
}