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

    // ── Фильтры входного потока: статика (опрос 5 Гц) ─────────────────────────
    // Границы клэмпа зависят от конкретного тензодатчика и подбираются на месте:
    // ненагруженные весы дают ≈3589, верхняя граница соответствует НПВ. Поэтому
    // фильтры по умолчанию выключены, а параметры инертны — включает оператор
    // после настройки на живом железе.

    /// <summary>Отбраковывать кадры статики с кодом вне [StaticClampMinCode, StaticClampMaxCode].</summary>
    public bool StaticClampEnabled { get; set; }

    /// <summary>Нижняя граница допустимого кода АЦП в статике.</summary>
    public int StaticClampMinCode { get; set; }

    /// <summary>Верхняя граница допустимого кода АЦП в статике.</summary>
    public int StaticClampMaxCode { get; set; } = 65535;

    /// <summary>Отбраковывать кадры статики со скачком кода больше StaticDeltaMaxCodes.</summary>
    public bool StaticDeltaEnabled { get; set; }

    /// <summary>Максимальный допустимый скачок кода между соседними кадрами статики.</summary>
    public int StaticDeltaMaxCodes { get; set; } = 5000;

    /// <summary>Сглаживать код статики экспоненциальным средним.</summary>
    public bool StaticEmaEnabled { get; set; }

    /// <summary>Коэффициент EMA для статики: 1.0 — без сглаживания, ближе к 0 — сильнее.</summary>
    public double StaticEmaAlpha { get; set; } = 0.3;

    // ── Фильтры входного потока: динамика (поток 30 Гц) ───────────────────────

    /// <summary>Отбраковывать сэмплы динамики с кодом вне [DynamicClampMinCode, DynamicClampMaxCode].</summary>
    public bool DynamicClampEnabled { get; set; }

    /// <summary>Нижняя граница допустимого кода АЦП в динамике.</summary>
    public int DynamicClampMinCode { get; set; }

    /// <summary>Верхняя граница допустимого кода АЦП в динамике.</summary>
    public int DynamicClampMaxCode { get; set; } = 65535;

    /// <summary>Отбраковывать сэмплы динамики со скачком кода больше DynamicDeltaMaxCodes.</summary>
    public bool DynamicDeltaEnabled { get; set; }

    /// <summary>Максимальный допустимый скачок кода между соседними сэмплами динамики.</summary>
    public int DynamicDeltaMaxCodes { get; set; } = 5000;

    /// <summary>
    /// Отбраковывать сэмплы динамики при застрявшем активном канале.
    /// В статике детектора застревания нет: там вагон может законно стоять неподвижно
    /// (расцепка под башмак), и постоянный код — признак нормального взвешивания.
    /// </summary>
    public bool DynamicStuckEnabled { get; set; }

    /// <summary>Сколько строго равных кодов подряд считать застрявшим датчиком (30 Гц: 150 ≈ 5 с).</summary>
    public int DynamicStuckSamples { get; set; } = 150;

    /// <summary>Сглаживать код динамики экспоненциальным средним.</summary>
    public bool DynamicEmaEnabled { get; set; }

    /// <summary>Коэффициент EMA для динамики: 1.0 — без сглаживания, ближе к 0 — сильнее.</summary>
    public double DynamicEmaAlpha { get; set; } = 0.2;

    [JsonIgnore]
    public double OperatorZeroLimitTonnes => MaxCapacityTonnes * OperatorZeroLimitPercent / 100.0;
}
