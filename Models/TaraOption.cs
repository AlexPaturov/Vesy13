namespace Vesy13.Models;

public record TaraOption(DateTime WeighTime, decimal Brutto)
{
    public override string ToString() =>
        $"{WeighTime:dd.MM.yyyy  HH:mm:ss}  —  {Brutto:F2} т";
}
