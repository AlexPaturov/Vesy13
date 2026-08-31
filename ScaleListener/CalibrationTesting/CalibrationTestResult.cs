namespace ScaleListener.CalibrationTesting;

public sealed record CalibrationTestResult(
    string Checkpoint,
    int AdcCode,
    decimal ExpectedMass,
    double? ActualMass,
    double? ErrorTonnes,
    int? SelectedCalibrationPointId,
    bool Passed);
