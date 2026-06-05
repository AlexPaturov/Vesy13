namespace Vesy13.Models;

public class LocalWagon
{
    public int      Id        { get; set; }
    public int      Number    { get; set; }
    public DateTime TrainTime { get; set; }
    public DateTime WagonTime { get; set; }
    public double   Bogie1    { get; set; }
    public double   Bogie2    { get; set; }
    public string   Direction { get; set; } = "";
    public string   Mode      { get; set; } = "";
    public double   Total     => Bogie1 + Bogie2;
}
