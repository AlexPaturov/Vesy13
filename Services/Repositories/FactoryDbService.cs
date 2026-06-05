using FirebirdSql.Data.FirebirdClient;
using System.Data;
using Vesy13.Models;

namespace Vesy13.Services.Repositories;

public class FactoryDbService
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
        string?          plat,
        bool             isTara = false)
    {
        decimal? brutto = isTara ? 0m        : row.Total;
        decimal? tarBrs = isTara ? row.Total : null;
        decimal? netto  = isTara ? null      : (tarDok.HasValue ? row.Total - tarDok.Value : null);
        string   gruzFinal = isTara ? "Тара" : (gruz ?? "");

        await using var conn = new FbConnection(ConnStr);
        await conn.OpenAsync();

        string sql = $@"
INSERT INTO {table}
    (DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, VESY, NPP, REJVZVESH, POTR, PLAT)
VALUES
    (@dt, @vr, @nvag, @ndok, @gruz, @brutto, @tarbrs, @tardok, @netto, 13, @npp, @rejvzvesh, @potr, @plat)";

        await using var cmd = new FbCommand(sql, conn);
        cmd.Parameters.AddWithValue("@dt",        row.WagonTime.Date);
        cmd.Parameters.AddWithValue("@vr",        row.WagonTime.TimeOfDay);
        cmd.Parameters.AddWithValue("@nvag",      nvag);
        cmd.Parameters.AddWithValue("@ndok",      ndok.HasValue ? (object)ndok.Value : DBNull.Value);
        cmd.Parameters.AddWithValue("@gruz",      gruzFinal);
        cmd.Parameters.AddWithValue("@brutto",    brutto);
        cmd.Parameters.AddWithValue("@tarbrs",    tarBrs.HasValue ? (object)tarBrs.Value : DBNull.Value);
        cmd.Parameters.AddWithValue("@tardok",    tarDok.HasValue ? (object)tarDok.Value : DBNull.Value);
        cmd.Parameters.AddWithValue("@netto",     netto.HasValue  ? (object)netto.Value  : DBNull.Value);
        cmd.Parameters.AddWithValue("@npp",       (short)row.WagonNum);
        cmd.Parameters.AddWithValue("@rejvzvesh", row.Mode);
        cmd.Parameters.AddWithValue("@potr",      (object?)potr ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@plat",      (object?)plat ?? DBNull.Value);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(
        string   table,
        int      id,
        string   nvag,
        long?    ndok,
        string?  gruz,
        decimal? tarBrs,
        decimal? tarDok,
        decimal? netto,
        string?  potr,
        string?  plat)
    {
        await using var conn = new FbConnection(ConnStr);
        await conn.OpenAsync();

        string sql = $@"
UPDATE {table} SET
    NVAG=@nvag, NDOK=@ndok, GRUZ=@gruz,
    TAR_BRS=@tarbrs, TAR_DOK=@tardok, NETTO=@netto,
    POTR=@potr, PLAT=@plat
WHERE ID=@id";

        await using var cmd = new FbCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id",     id);
        cmd.Parameters.AddWithValue("@nvag",   nvag);
        cmd.Parameters.AddWithValue("@ndok",   ndok.HasValue ? (object)ndok.Value : DBNull.Value);
        cmd.Parameters.AddWithValue("@gruz",   (object?)gruz   ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@tarbrs", tarBrs.HasValue ? (object)tarBrs.Value : DBNull.Value);
        cmd.Parameters.AddWithValue("@tardok", tarDok.HasValue ? (object)tarDok.Value : DBNull.Value);
        cmd.Parameters.AddWithValue("@netto",  netto.HasValue  ? (object)netto.Value  : DBNull.Value);
        cmd.Parameters.AddWithValue("@potr",   (object?)potr   ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@plat",   (object?)plat   ?? DBNull.Value);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<FbRecord>> GetRecentAsync(int days = 7)
    {
        var result = new List<FbRecord>();
        var cutoff = DateTime.Today.AddDays(-days);

        await using var conn = new FbConnection(ConnStr);
        await conn.OpenAsync();

        const string sql = @"
SELECT FIRST 200 ID, SRC, DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, NPP, POTR, PLAT, CEX
FROM (
    SELECT ID, 'GPRI' AS SRC, DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, NPP, POTR, PLAT, CEX FROM GPRI WHERE DT >= @cutoff
    UNION ALL
    SELECT ID, 'GRAS' AS SRC, DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, NPP, POTR, PLAT, CEX FROM GRAS WHERE DT >= @cutoff
) t
ORDER BY DT DESC, VR DESC";

        await using var cmd = new FbCommand(sql, conn);
        cmd.Parameters.AddWithValue("@cutoff", cutoff.Date);

        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
        {
            var dt    = rdr.GetDateTime(2);
            var vrRaw = rdr.IsDBNull(3) ? TimeSpan.Zero : rdr.GetValue(3) switch
            {
                TimeSpan ts => ts,
                DateTime dv => dv.TimeOfDay,
                _           => TimeSpan.Zero,
            };
            result.Add(new FbRecord
            {
                Id     = Convert.ToInt32(rdr.GetValue(0)),
                Table  = rdr.GetString(1),
                Dt     = dt.Date,
                Vr     = vrRaw,
                Nvag   = rdr.IsDBNull(4)  ? "" : rdr.GetString(4).Trim(),
                Ndok   = rdr.IsDBNull(5)  ? "" : rdr.GetValue(5).ToString()!,
                Gruz   = rdr.IsDBNull(6)  ? "" : rdr.GetString(6).Trim(),
                Brutto = rdr.GetDecimal(7),
                TarBrs = rdr.IsDBNull(8)  ? null : rdr.GetDecimal(8),
                TarDok = rdr.IsDBNull(9)  ? null : rdr.GetDecimal(9),
                Netto  = rdr.IsDBNull(10) ? null : rdr.GetDecimal(10),
                Npp    = rdr.IsDBNull(11) ? 0   : Convert.ToInt32(rdr.GetValue(11)),
                Potr   = rdr.IsDBNull(12) ? "" : rdr.GetString(12).Trim(),
                Plat   = rdr.IsDBNull(13) ? "" : rdr.GetString(13).Trim(),
                Cex    = rdr.IsDBNull(14) ? 0  : Convert.ToInt32(rdr.GetValue(14)),
            });
        }
        return result;
    }

    public async Task<List<TaraOption>> GetTaraOptionsAsync(string nvag)
    {
        var result  = new List<TaraOption>();
        var cutoff  = DateTime.Today.AddYears(-1);

        await using var conn = new FbConnection(ConnStr);
        await conn.OpenAsync();

        const string sql = @"
SELECT DT, VR, tar_brs FROM GPRI WHERE NVAG = @nvag AND DT >= @cutoff
UNION ALL
SELECT DT, VR, tar_brs FROM GRAS WHERE NVAG = @nvag AND DT >= @cutoff
ORDER BY 1 DESC, 2 DESC";

        await using var cmd = new FbCommand(sql, conn);
        cmd.Parameters.AddWithValue("@nvag",   nvag);
        cmd.Parameters.AddWithValue("@cutoff", cutoff.Date);

        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
        {
            var dt      = rdr.GetDateTime(0);
            var vrRaw   = rdr.IsDBNull(1) ? TimeSpan.Zero : rdr.GetValue(1) switch
            {
                TimeSpan ts => ts,
                DateTime dv => dv.TimeOfDay,
                _           => TimeSpan.Zero,
            };
            var tara_brs  = rdr.GetDecimal(2);
            result.Add(new TaraOption(dt.Date.Add(vrRaw), tara_brs));
        }
        return result;
    }
}