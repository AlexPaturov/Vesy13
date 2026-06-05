namespace Vesy13.Models;

public class WagonWeighingRow
{
    public int      Id          { get; set; }
    public DateTime TrainTime   { get; set; }
    public DateTime WagonTime   { get; set; }
    public int      WagonNum    { get; set; }
    public decimal  Bogie1      { get; set; }
    public decimal  Bogie2      { get; set; }
    public decimal  Total       { get; set; }
    public string   Direction   { get; set; } = "";
    public string   Mode        { get; set; } = "";
    public string   Potr        { get; set; } = "";
    public string   Plat        { get; set; } = "";
    public int      Cex         { get; set; }
}