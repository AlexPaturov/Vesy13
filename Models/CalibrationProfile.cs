namespace Vesy13.Models;

public class CalibrationPoint
{
    public int    AdcCode    { get; set; }
    public double MassTonnes { get; set; }
}

public class ChannelCalib
{
    public double K { get; set; }
    public double B { get; set; }
    public List<CalibrationPoint> Points { get; set; } = new();

    public double Convert(int adcCode) => K * adcCode + B;
}

public class CalibrationProfile
{
    public ChannelCalib Ch0 { get; set; } = new();
    public ChannelCalib Ch1 { get; set; } = new();
}
