namespace ScaleListener.FaultInjection;

/// <summary>Одна строка истории: когда, какой сбой, правильное/неправильное значение.</summary>
public sealed record FaultHistoryEntry(DateTime Time, FaultType Type, FaultEventKind Kind, string CorrectValue, string WrongValue)
{
    public string Format()
    {
        string kindLabel = Kind switch
        {
            FaultEventKind.Fired => "сработал",
            FaultEventKind.Started => "начался",
            FaultEventKind.Stopped => "закончился",
            _ => Kind.ToString(),
        };
        return $"{Time:HH:mm:ss.fff}  {Type,-8}  {kindLabel,-10}  верно={CorrectValue,-12} неверно={WrongValue}";
    }
}
