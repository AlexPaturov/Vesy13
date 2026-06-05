namespace Vesy13.Models;

/// <summary>
/// Запись взвешивания в таблицах Firebird: GPRI (приход) или GRAS (расход).
/// Поле Table указывает, в какой таблице хранится запись.
/// </summary>
public class GpriGras
{
    public int      Id     { get; set; }
    public string   Table  { get; set; } = "";
    public DateTime Dt     { get; set; }
    public TimeSpan Vr     { get; set; }
    public string   Nvag   { get; set; } = "";
    public long?    Ndok   { get; set; }
    public string   Gruz   { get; set; } = "";
    public string   Mode   { get; set; } = "";
    public decimal  Brutto { get; set; }
    public decimal? TarBrs { get; set; }
    public decimal? TarDok { get; set; }
    public decimal? Netto  { get; set; }
    public int      Npp    { get; set; }
    public string   Potr   { get; set; } = "";
    public string   Plat   { get; set; } = "";
    public int      Cex    { get; set; }
}
