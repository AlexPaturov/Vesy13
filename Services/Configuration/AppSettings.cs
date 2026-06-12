using System.Text.Json.Serialization;

namespace Vesy13.Services.Configuration;

public sealed class AppSettings
{
    public string AdcPortName { get; set; } = "COM1";
    public double MaxCapacityTonnes { get; set; } = 140.0;
    public double WeightDiscretizationTonnes { get; set; } = 0.05;
    public double OperatorZeroLimitPercent { get; set; } = 2.0;
    public double AdminZeroLimitPercent { get; set; } = 100.0;
    public string AdminPasswordHash { get; set; } = "";
    public string AdminPasswordSalt { get; set; } = "";

    [JsonIgnore]
    public double OperatorZeroLimitTonnes => MaxCapacityTonnes * OperatorZeroLimitPercent / 100.0;
}
