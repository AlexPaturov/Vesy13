using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Vesy13.Models;

namespace Vesy13.Services.Repositories;

/// <summary>
/// Репозиторий заводской базы Firebird (DC_OPER.FDB).
/// Обеспечивает перенос и корректировку взвешиваний в таблицах GPRI и GRAS.
/// </summary>
public class FactoryRepository
{
    private const string ConnStr =
        @"User=sysdba;Password=masterkey;Database=127.0.0.1:C:\Work\DC_OPER.FDB;Dialect=3;Charset=UTF8;WireCrypt=Enabled;Role=;Connection Lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;PacketSize=8192;ServerType=0";

    /// <summary>
    /// Вставляет новую запись взвешивания в таблицу GPRI или GRAS.
    /// Таблица определяется <see cref="GpriGras.Table"/>.
    /// Все поля (BRUTTO, TAR_BRS, NETTO, GRUZ) должны быть вычислены вызывающей стороной.
    /// </summary>
    public async Task InsertAsync(GpriGras record)
    {
        await using var conn = new FbConnection(ConnStr);
        await conn.OpenAsync();
        await conn.ExecuteAsync($@"
            INSERT INTO {record.Table}
                (DT, VR, VR_PRV, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, VESY, NPP, REJVZVESH, POTR, PLAT, CEX)
            VALUES
                (@dt, @vr, @vrprv, @nvag, @ndok, @gruz, @brutto, @tarbrs, @tardok, @netto, 13, @npp, @rejvzvesh, @potr, @plat, @cex)",
            new {
                dt        = record.Dt,
                vr        = record.Vr,
                vrprv     = record.VR_PRV,
                nvag      = record.Nvag,
                ndok      = record.Ndok,
                gruz      = record.Gruz,
                brutto    = record.Brutto,
                tarbrs    = record.TarBrs,
                tardok    = record.TarDok,
                netto     = record.Netto,
                npp       = (short)record.Npp,
                rejvzvesh = record.Mode,
                potr      = record.Potr,
                plat      = record.Plat,
                cex       = record.Cex,
            });
    }

    /// <summary>
    /// Обновляет существующую запись в GPRI или GRAS.
    /// Таблица и ID определяются полями <see cref="GpriGras.Table"/> и <see cref="GpriGras.Id"/>.
    /// </summary>
    public async Task UpdateAsync(GpriGras record)
    {
        await using var conn = new FbConnection(ConnStr);
        await conn.OpenAsync();
        await conn.ExecuteAsync($@"
            UPDATE {record.Table} SET
                NVAG=@nvag, NDOK=@ndok, GRUZ=@gruz,
                BRUTTO=@brutto, TAR_BRS=@tarbrs, TAR_DOK=@tardok, NETTO=@netto,
                POTR=@potr, PLAT=@plat, CEX=@cex
            WHERE ID=@id",
            new {
                id     = record.Id,
                nvag   = record.Nvag,
                ndok   = record.Ndok,
                gruz   = record.Gruz,
                brutto = record.Brutto,
                tarbrs = record.TarBrs,
                tardok = record.TarDok,
                netto  = record.Netto,
                potr   = record.Potr,
                plat   = record.Plat,
                cex    = record.Cex,
            });
    }

    /// <summary>
    /// Возвращает последние 200 записей из GPRI и GRAS за указанное количество дней.
    /// Результат содержит поле <c>Table</c> («GPRI» или «GRAS») для последующего UPDATE.
    /// </summary>
    /// <param name="days">Глубина выборки в днях от сегодня. По умолчанию 7.</param>
    public async Task<List<GpriGras>> GetRecentAsync(int days = 7)
    {
        var result = new List<GpriGras>();
        var cutoff = DateTime.Today.AddDays(-days);

        await using var conn = new FbConnection(ConnStr);
        await conn.OpenAsync();

        const string sql = @"
            SELECT ID, SRC, DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, NPP, POTR, PLAT, CEX, VR_PRV
            FROM (
                SELECT ID, 'GPRI' AS SRC, DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, NPP, POTR, PLAT, CEX, VR_PRV FROM GPRI WHERE DT >= @cutoff
                UNION ALL
                SELECT ID, 'GRAS' AS SRC, DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, NPP, POTR, PLAT, CEX, VR_PRV FROM GRAS WHERE DT >= @cutoff
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
            var vrPrvRaw = rdr.IsDBNull(15) ? TimeSpan.Zero : rdr.GetValue(15) switch
            {
                TimeSpan ts => ts,
                DateTime dv => dv.TimeOfDay,
                _           => TimeSpan.Zero,
            };
            result.Add(new GpriGras
            {
                Id     = Convert.ToInt32(rdr.GetValue(0)),
                Table  = rdr.GetString(1),
                Dt     = dt.Date,
                Vr     = vrRaw,
                Nvag   = rdr.IsDBNull(4)  ? "" : rdr.GetString(4).Trim(),
                Ndok   = rdr.IsDBNull(5)  ? null : Convert.ToInt64(rdr.GetValue(5)),
                Gruz   = rdr.IsDBNull(6)  ? "" : rdr.GetString(6).Trim(),
                Brutto = rdr.GetDecimal(7),
                TarBrs = rdr.IsDBNull(8)  ? null : rdr.GetDecimal(8),
                TarDok = rdr.IsDBNull(9)  ? null : rdr.GetDecimal(9),
                Netto  = rdr.IsDBNull(10) ? null : rdr.GetDecimal(10),
                Npp    = rdr.IsDBNull(11) ? 0   : Convert.ToInt32(rdr.GetValue(11)),
                Potr   = rdr.IsDBNull(12) ? "" : rdr.GetString(12).Trim(),
                Plat   = rdr.IsDBNull(13) ? "" : rdr.GetString(13).Trim(),
                Cex    = rdr.IsDBNull(14) ? 0  : Convert.ToInt32(rdr.GetValue(14)),
                VR_PRV = vrPrvRaw,
            });
        }
        return result;
    }


    public async Task<List<GpriGras>> GetByTrainTimeAsync(DateTime trainTime)
    {
        var result = new List<GpriGras>();
        var date = trainTime.Date;
        var time = trainTime.TimeOfDay;

        await using var conn = new FbConnection(ConnStr);
        await conn.OpenAsync();

        const string sql = @"
            SELECT ID, SRC, DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, NPP, POTR, PLAT, CEX, VR_PRV
            FROM (
                SELECT ID, 'GPRI' AS SRC, DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, NPP, POTR, PLAT, CEX, VR_PRV
                FROM GPRI
                WHERE DT = @date AND CAST(VR AS TIME) = CAST(@time AS TIME)
                UNION ALL
                SELECT ID, 'GRAS' AS SRC, DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, NPP, POTR, PLAT, CEX, VR_PRV
                FROM GRAS
                WHERE DT = @date AND CAST(VR AS TIME) = CAST(@time AS TIME)
            ) t
            ORDER BY NPP, ID";

        await using var cmd = new FbCommand(sql, conn);
        cmd.Parameters.AddWithValue("@date", date.Date);
        cmd.Parameters.AddWithValue("@time", time);

        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
        {
            var dt = rdr.GetDateTime(2);
            var vrRaw = rdr.IsDBNull(3) ? TimeSpan.Zero : rdr.GetValue(3) switch
            {
                TimeSpan ts => ts,
                DateTime dv => dv.TimeOfDay,
                _ => TimeSpan.Zero,
            };
            var vrPrvRaw = rdr.IsDBNull(15) ? TimeSpan.Zero : rdr.GetValue(15) switch
            {
                TimeSpan ts => ts,
                DateTime dv => dv.TimeOfDay,
                _ => TimeSpan.Zero,
            };
            result.Add(new GpriGras
            {
                Id = Convert.ToInt32(rdr.GetValue(0)),
                Table = rdr.GetString(1),
                Dt = dt.Date,
                Vr = vrRaw,
                Nvag = rdr.IsDBNull(4) ? "" : rdr.GetString(4).Trim(),
                Ndok = rdr.IsDBNull(5) ? null : Convert.ToInt64(rdr.GetValue(5)),
                Gruz = rdr.IsDBNull(6) ? "" : rdr.GetString(6).Trim(),
                Brutto = rdr.GetDecimal(7),
                TarBrs = rdr.IsDBNull(8) ? null : rdr.GetDecimal(8),
                TarDok = rdr.IsDBNull(9) ? null : rdr.GetDecimal(9),
                Netto = rdr.IsDBNull(10) ? null : rdr.GetDecimal(10),
                Npp = rdr.IsDBNull(11) ? 0 : Convert.ToInt32(rdr.GetValue(11)),
                Potr = rdr.IsDBNull(12) ? "" : rdr.GetString(12).Trim(),
                Plat = rdr.IsDBNull(13) ? "" : rdr.GetString(13).Trim(),
                Cex = rdr.IsDBNull(14) ? 0 : Convert.ToInt32(rdr.GetValue(14)),
                VR_PRV = vrPrvRaw,
            });
        }
        return result;
    }

    public async Task<List<GpriGras>> GetByDateAsync(DateTime date)
    {
        var result = new List<GpriGras>();

        await using var conn = new FbConnection(ConnStr);
        await conn.OpenAsync();

        const string sql = @"
            SELECT ID, SRC, DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, NPP, POTR, PLAT, CEX, VR_PRV
            FROM (
                SELECT ID, 'GPRI' AS SRC, DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, NPP, POTR, PLAT, CEX, VR_PRV
                FROM GPRI
                WHERE DT = @date
                UNION ALL
                SELECT ID, 'GRAS' AS SRC, DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, NPP, POTR, PLAT, CEX, VR_PRV
                FROM GRAS
                WHERE DT = @date
            ) t
            ORDER BY DT DESC, VR DESC, NPP, ID";

        await using var cmd = new FbCommand(sql, conn);
        cmd.Parameters.AddWithValue("@date", date.Date);

        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
        {
            var dt = rdr.GetDateTime(2);
            var vrRaw = rdr.IsDBNull(3) ? TimeSpan.Zero : rdr.GetValue(3) switch
            {
                TimeSpan ts => ts,
                DateTime dv => dv.TimeOfDay,
                _ => TimeSpan.Zero,
            };
            var vrPrvRaw = rdr.IsDBNull(15) ? TimeSpan.Zero : rdr.GetValue(15) switch
            {
                TimeSpan ts => ts,
                DateTime dv => dv.TimeOfDay,
                _ => TimeSpan.Zero,
            };
            result.Add(new GpriGras
            {
                Id = Convert.ToInt32(rdr.GetValue(0)),
                Table = rdr.GetString(1),
                Dt = dt.Date,
                Vr = vrRaw,
                Nvag = rdr.IsDBNull(4) ? "" : rdr.GetString(4).Trim(),
                Ndok = rdr.IsDBNull(5) ? null : Convert.ToInt64(rdr.GetValue(5)),
                Gruz = rdr.IsDBNull(6) ? "" : rdr.GetString(6).Trim(),
                Brutto = rdr.GetDecimal(7),
                TarBrs = rdr.IsDBNull(8) ? null : rdr.GetDecimal(8),
                TarDok = rdr.IsDBNull(9) ? null : rdr.GetDecimal(9),
                Netto = rdr.IsDBNull(10) ? null : rdr.GetDecimal(10),
                Npp = rdr.IsDBNull(11) ? 0 : Convert.ToInt32(rdr.GetValue(11)),
                Potr = rdr.IsDBNull(12) ? "" : rdr.GetString(12).Trim(),
                Plat = rdr.IsDBNull(13) ? "" : rdr.GetString(13).Trim(),
                Cex = rdr.IsDBNull(14) ? 0 : Convert.ToInt32(rdr.GetValue(14)),
                VR_PRV = vrPrvRaw,
            });
        }
        return result;
    }

    /// <summary>
    /// Возвращает записи из GPRI или GRAS по заданным фильтрам для печати отвесных.
    /// </summary>
    public async Task<List<GpriGras>> GetForPrintAsync(

        string table,
        DateTime dateFrom, DateTime dateTo,
        string? nvag, string? gruz, string? potr, long? ndok)
    {
        var result     = new List<GpriGras>();
        var conditions = new List<string> { "DT BETWEEN @datefrom AND @dateto" };

        if (!string.IsNullOrWhiteSpace(nvag)) conditions.Add("NVAG = @nvag");
        if (!string.IsNullOrWhiteSpace(gruz)) conditions.Add("GRUZ CONTAINING @gruz");
        if (!string.IsNullOrWhiteSpace(potr)) conditions.Add("POTR CONTAINING @potr");
        if (ndok.HasValue)                    conditions.Add("NDOK = @ndok");

        var sql = $@"SELECT ID, DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, NPP, POTR, PLAT, CEX, REJVZVESH, VR_PRV
                     FROM {table}
                     WHERE {string.Join(" AND ", conditions)}
                     ORDER BY DT, VR";

        await using var conn = new FbConnection(ConnStr);
        await conn.OpenAsync();

        await using var cmd = new FbCommand(sql, conn);
        cmd.Parameters.AddWithValue("@datefrom", dateFrom.Date);
        cmd.Parameters.AddWithValue("@dateto",   dateTo.Date);
        if (!string.IsNullOrWhiteSpace(nvag)) cmd.Parameters.AddWithValue("@nvag", nvag);
        if (!string.IsNullOrWhiteSpace(gruz)) cmd.Parameters.AddWithValue("@gruz", gruz);
        if (!string.IsNullOrWhiteSpace(potr)) cmd.Parameters.AddWithValue("@potr", potr);
        if (ndok.HasValue)                    cmd.Parameters.AddWithValue("@ndok", ndok.Value);

        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
        {
            var dt    = rdr.GetDateTime(1);
            var vrRaw = rdr.IsDBNull(2) ? TimeSpan.Zero : rdr.GetValue(2) switch
            {
                TimeSpan ts => ts,
                DateTime dv => dv.TimeOfDay,
                _           => TimeSpan.Zero,
            };
            var vrPrvRaw = rdr.IsDBNull(15) ? TimeSpan.Zero : rdr.GetValue(15) switch
            {
                TimeSpan ts => ts,
                DateTime dv => dv.TimeOfDay,
                _           => TimeSpan.Zero,
            };
            result.Add(new GpriGras
            {
                Id     = Convert.ToInt32(rdr.GetValue(0)),
                Table  = table,
                Dt     = dt.Date,
                Vr     = vrRaw,
                Nvag   = rdr.IsDBNull(3)  ? "" : rdr.GetString(3).Trim(),
                Ndok   = rdr.IsDBNull(4)  ? null : Convert.ToInt64(rdr.GetValue(4)),
                Gruz   = rdr.IsDBNull(5)  ? "" : rdr.GetString(5).Trim(),
                Brutto = rdr.GetDecimal(6),
                TarBrs = rdr.IsDBNull(7)  ? null : rdr.GetDecimal(7),
                TarDok = rdr.IsDBNull(8)  ? null : rdr.GetDecimal(8),
                Netto  = rdr.IsDBNull(9)  ? null : rdr.GetDecimal(9),
                Npp    = rdr.IsDBNull(10) ? 0    : Convert.ToInt32(rdr.GetValue(10)),
                Potr   = rdr.IsDBNull(11) ? ""   : rdr.GetString(11).Trim(),
                Plat   = rdr.IsDBNull(12) ? ""   : rdr.GetString(12).Trim(),
                Cex    = rdr.IsDBNull(13) ? 0    : Convert.ToInt32(rdr.GetValue(13)),
                Mode   = rdr.IsDBNull(14) ? ""   : rdr.GetString(14).Trim(),
                VR_PRV = vrPrvRaw,
            });
        }
        return result;
    }

    /// <summary>
    /// Возвращает варианты тары для вагона из GPRI и GRAS за последний год.
    /// Используется для заполнения выпадающего списка тары в CorrectionsForm.
    /// </summary>
    /// <param name="nvag">Номер вагона.</param>
    public async Task<List<TaraOption>> GetTaraOptionsAsync(string nvag)
    {
        var result = new List<TaraOption>();
        var cutoff = DateTime.Today.AddYears(-1);

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
            if (rdr.IsDBNull(2))
                continue;

            var dt    = rdr.GetDateTime(0);
            var vrRaw = rdr.IsDBNull(1) ? TimeSpan.Zero : rdr.GetValue(1) switch
            {
                TimeSpan ts => ts,
                DateTime dv => dv.TimeOfDay,
                _           => TimeSpan.Zero,
            };
            result.Add(new TaraOption(dt.Date.Add(vrRaw), rdr.GetDecimal(2)));
        }
        return result;
    }
}
