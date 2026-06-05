namespace Vesy13.Models;

public record LocalWagon(
    int      Number,
    DateTime TrainTime,
    DateTime WagonTime,
    double   Bogie1,
    double   Bogie2,
    string   Direction)
{
    public int    Id   { get; init; } = 0;
    public string Mode { get; init; } = "";
    public double Total => Bogie1 + Bogie2;
}
