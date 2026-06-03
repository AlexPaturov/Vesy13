namespace Vesy13.Models;

public record WagonRecord(
    int      Number,
    DateTime TrainTime,
    DateTime WagonTime,
    double   Bogie1,
    double   Bogie2,
    string   Direction)
{
    public double Total => Bogie1 + Bogie2;
}
