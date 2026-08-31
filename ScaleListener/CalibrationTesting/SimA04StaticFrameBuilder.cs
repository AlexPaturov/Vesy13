namespace ScaleListener.CalibrationTesting;

public static class SimA04StaticFrameBuilder
{
    public static byte[] Build(int ch0, int ch1)
    {
        ch0 = Math.Clamp(ch0, 0, 65535);
        ch1 = Math.Clamp(ch1, 0, 65535);
        return new[]
        {
            (byte)(ch0 & 0xFF),
            (byte)((ch0 >> 8) & 0xFF),
            (byte)(ch1 & 0xFF),
            (byte)((ch1 >> 8) & 0xFF),
        };
    }
}
