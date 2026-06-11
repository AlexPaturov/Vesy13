namespace Vesy13.Models;

public record TaraOption(DateTime WeighTime, decimal Brutto, bool WeightOnly = false)
{
    public override string ToString() =>
        WeightOnly
            ? $"{Brutto:F2} т"
            : $"{WeighTime:dd.MM.yyyy  HH:mm:ss}  —  {Brutto:F2} т";
}
