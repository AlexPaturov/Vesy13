namespace Vesy13.Models;

public record WagonRecord(
    int      Number,
    DateTime TrainTime,
    DateTime WagonTime,
    int      Bogie1,
    int      Bogie2,
    string   Direction)
{
    public int Total => Bogie1 + Bogie2;
}
