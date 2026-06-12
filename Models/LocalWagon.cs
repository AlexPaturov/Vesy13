namespace Vesy13.Models;

/// <summary>
/// Запись о взвешивании вагона (потележечно, в тоннах) в локальной БД PostgreSQL.
/// Хранится до переноса в систему учёта предприятия (Firebird).
/// </summary>
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
    public bool     Transferred { get; set; }
    public double   Total     => Bogie1 + Bogie2;
}
