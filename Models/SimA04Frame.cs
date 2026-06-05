namespace Vesy13.Models;

public readonly struct SimA04Frame
{
    public int  Ch0   { get; }
    public int  Ch1   { get; }
    public bool Valid { get; }

    private SimA04Frame(int ch0, int ch1) { Ch0 = ch0; Ch1 = ch1; Valid = true; }

    public static SimA04Frame Parse(byte[] data)
    {
        if (data.Length != 4) return default;
        return new SimA04Frame(data[1] * 256 + data[0], data[3] * 256 + data[2]);
    }
}