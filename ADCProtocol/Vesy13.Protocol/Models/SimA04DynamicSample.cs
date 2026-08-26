namespace Vesy13.Protocol.Models;

/// <summary>
/// Один 5-байтовый сэмпл динамического потока АЦП "СИМ А04".
/// </summary>
public readonly struct SimA04DynamicSample
{
    public int Ch0 { get; }
    public int Ch1 { get; }
    public byte Aux { get; }
    public const int FrameSize = 5;

    public static bool IsValid(ReadOnlySpan<byte> data)
    {
        return data.Length == 5 & data[4] == ((data[0] + data[1] + data[2] + data[3]) & 0xFF);
    }

    public static bool TryParse(ReadOnlySpan<byte> data, out SimA04DynamicSample sample)
    {


    }
}
