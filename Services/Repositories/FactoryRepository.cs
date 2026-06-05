using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Vesy13.Models;

namespace Vesty13.Services.Repositories;

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
    /// </summary>
    /// <param name="table">Целевая таблица: «GPRI» (приход) или «GRAS» (расход).</param>
    /// <param name="row">Исходная строка из wagon_weighing.</param>
    /// <param name="nvag">Номер вагона.</param>
    /// <param name="ndok">Номер документа (может отсутствовать).</param>
    /// <param name="gruz">Наименование груза. Игнорируется при <paramref name="isTara"/> = true.</param>
    /// <param name="tarDok">Тара по документу (т). При наличии вычисляется нетто.</param>
    /// <param name="potr">Поставщик.</param>
    /// <param name="plat">Плательщик.</param>
    /// <param name="isTara">
    /// Режим тары: BRUTTO = 0, TAR_BRS = вес с весов, NETTO = NULL, GRUZ = «Тара».
    /// </param>
    public async Task InsertAsync(
        string           table,
        LocalWagon row,
        string           nvag,
        long?            ndok,
        string?          gruz,
        decimal?         tarDok,
        string?          potr,
        string?          plat,
        bool             isTara = false)
    {
        decimal? brutto    = isTara ? 0m                  : (decimal)row.Total;
        decimal? tarBrs    = isTara ? (decimal)row.Total : null;
        decimal? netto     = isTara ? null                : (tarDok.HasValue ? (decimal)row.Total - tarDok.Value : null);
        string   gruzFinal = isTara ? "Тара"   : (gruz ?? "");

        await using var conn = new FbConnection(ConnStr);
        await conn.OpenAsync();
        await conn.ExecuteAsync($@"
INSERT INTO {table}
    (DT, VR, NVAG, NDOK, GRUZ, BRUTTO, TAR_BRS, TAR_DOK, NETTO, VESY, NPP, REJVZVESH, POTR, PLAT)
VALUES
    (@dt, @vr, @nvag, @ndok, @gruz, @brutto, @tarbrs, @tardok, @netto, 13, @npp, @rejvzvesh, @potr, @plat)",
            new {
                dt        = row.WagonTime.Date,
                vr        = row.WagonTime.TimeOfDay,
                nvag,
                ndok,
                gruz      = gruzFinal,
                brutto,
                tarbrs    = tarBrs,
                tardok    = tarDok,
                netto,
                npp       = (short)row.Number,
                rejvzvesh = row.Mode,
                potr,
                plat,
            });
    }

    /// <summary>
    /// Обновляет существующую запись в GPRI или GRAS по первичному ключу.
    /// </summary>
    /// <param name="table">Целевая таблица: «GPRI» или «GRAS».</param>
    /// <param name="id">Первичный ключ записи (поле ID).</param>
    /// <param name="nvag">Номер вагона.</param>
    /// <param name="ndok">Номер документа.</param>
    /// <param name="gruz">Наименование груза.</param>
    /// <param name="tarBrs">Тара фактическая (т).</param>
    /// <param name="tarDok">Тара по документу (т).</param>
    /// <param name="netto">Нетто (т).</param>
    /// <param name="potr">Поставщик.</param>
    /// <param name="plat">Плательщик.</param>
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
        await conn.ExecuteAsync($@"
UPDATE {table} SET
    NVAG=@nvag, NDOK=@ndok, GRUZ=@gruz,
    TAR_BRS=@tarbrs, TAR_DOK=@tardok, NETTO=@netto,
    POTR=@potr, PLAT=@plat
WHERE ID=@id",
            new { id, nvag, ndok, gruz, tarbrs = tarBrs, tardok = tarDok, netto, potr, plat });
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
            result.Add(new GpriGras
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
