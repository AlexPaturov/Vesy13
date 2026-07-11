using System.Text.Json.Serialization;
using Vesy13.Models;
using Vesy13.Services.Hardware;

namespace Vesy13.Services.Configuration;

public sealed class AppSettings
{
    public string AdcPortName { get; set; } = "COM3";

    /// <summary>Активный канал АЦП (CH0/CH1), общий для статики и динамики. Переключается на вкладке «Канал» сервисной формы; переживает перезапуск программы.</summary>
    public ActiveChannel ActiveChannel { get; set; } = ActiveChannel.Main;
    public double MaxCapacityTonnes { get; set; } = 140.0;
    public double WeightDiscretizationTonnes { get; set; } = 0.05;
    public double OperatorZeroLimitPercent { get; set; } = 2.0;
    public double AdminZeroLimitPercent { get; set; } = 100.0;
    public string AdminPasswordHash { get; set; } = "";
    public string AdminPasswordSalt { get; set; } = "";

    /// <summary>Последний успешно прочитанный/сохранённый снимок точек статической калибровки (fallback при недоступной БД).</summary>
    public List<CalibPoint> CachedStaticPoints { get; set; } = new();

    /// <summary>Последняя известная активная строка динамической калибровки (fallback при недоступной БД).</summary>
    public DynamicCalib CachedDynamicCalib { get; set; } = new();

    /// <summary>Время последнего обновления кэша калибровки выше.</summary>
    public DateTime? CalibCacheUpdatedAt { get; set; }

    [JsonIgnore]
    public double OperatorZeroLimitTonnes => MaxCapacityTonnes * OperatorZeroLimitPercent / 100.0;
}
