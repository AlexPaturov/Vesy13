using FirebirdSql.Data.FirebirdClient;
using Vesy13.Models;

namespace Vesy13.Services;

public class FirebirdService
{
    private const string ConnStr =
        @"User=sysdba;Password=masterkey;Database=127.0.0.1:C:\Work\DC_OPER.FDB;Dialect=3;Charset=UTF8;WireCrypt=Enabled;Role=;Connection Lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;PacketSize=8192;ServerType=0";

    public async Task InsertAsync(
        string           table,
        WagonWeighingRow row,
        string           nvag,
        long?            ndok,
        string?          gruz,
        decimal?         tarDok,
        string?          potr,
        string?          plat)
    {
        decimal? netto = tarDok.HasValue ? row.Total - tarDok.Value : null;

        await using var conn = new FbConnection(ConnStr);
        await conn.OpenAsync();

        string sql = $@"
INSERT INTO {table}
    (DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_DOK, NETTO, VESY, NPP, V12, V34, REJVZVESH, POTR, PLAT, WHEN_INSERT)
VALUES
    (@dt, @vr, @nvag, @ndok, @gruz, @brutto, @tardok, @netto, 13, @npp, @v12, @v34, @rejvzvesh, @potr, @plat, CURRENT_TIMESTAMP)";

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
        cmd.Parameters.AddWithValue("@potr",      (object?)potr ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@plat",      (object?)plat ?? DBNull.Value);
        await cmd.ExecuteNonQueryAsync();
    }
}