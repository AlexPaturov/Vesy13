namespace Vesy13.Models;

/// <summary>Выбранная статическая точка и рассчитанный по ней базовый вес.</summary>
public sealed record StaticCalibrationResult(
    CalibPoint Point,
    double Tonnes,
    int ActivePointCount);
