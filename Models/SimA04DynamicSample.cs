namespace Vesy13.Models;

/// <summary>
/// Один 5-байтовый сэмпл динамического потока АЦП "СИМ А04".
/// </summary>
public readonly struct SimA04DynamicSample
{
    private const int AuxOffset = 28;

    public int Ch0 { get; }
    public int Ch1 { get; }
    public byte Aux { get; }
    public bool Valid { get; }

    private SimA04DynamicSample(int ch0, int ch1, byte aux)
    {
        Ch0 = ch0;
        Ch1 = ch1;
        Aux = aux;
        Valid = true;
    }

    public static SimA04DynamicSample Parse(byte[] data)
    {
        if (data.Length != 5) return default;

        var expectedAux = (data[0] + data[2] + AuxOffset) & 0xFF;
        if (data[4] != expectedAux) return default;

        return new SimA04DynamicSample(
            data[1] * 256 + data[0],
            data[3] * 256 + data[2],
            data[4]);
    }
}
