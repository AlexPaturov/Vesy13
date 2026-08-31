using Vesy13.Application;
using Vesy13.Models;
using Vesy13.Services.Hardware;

namespace ScaleListener.CalibrationTesting;

public static class CalibrationTestRunner
{
    private const double AllowedErrorTonnes = 0.001;

    public static IReadOnlyList<CalibrationTestResult> Run(
        IReadOnlyList<CalibrationAnchor> anchors,
        ActiveChannel channel)
    {
        Validate(anchors);

        int channelNumber = channel == ActiveChannel.Main ? 0 : 1;
        int zeroCode = anchors.Single(point => point.Mass == 0).AdcCode;
        var calibrationPoints = anchors
            .OrderBy(point => point.AdcCode)
            .Select(point => new CalibPoint
            {
                Id = point.Id,
                Channel = channelNumber,
                AdcCode = point.AdcCode,
                Mass = point.Mass,
                CalibrationValue = point.Mass == 0
                    ? 0
                    : decimal.Round(
                        point.Mass / (point.AdcCode - zeroCode) * 65535m,
                        3,
                        MidpointRounding.AwayFromZero),
                IsActive = true,
            })
            .ToList();

        return CalibrationScenarioGenerator.Generate(anchors)
            .Select(testCase =>
            {
                decimal expected = LinearCalibrationOracle.Calculate(anchors, testCase.AdcCode);
                StaticCalibrationResult? actual = CalibrationCalculator.CalculateStatic(
                    calibrationPoints,
                    testCase.AdcCode,
                    channel);
                double? error = actual is null
                    ? null
                    : actual.Tonnes - (double)expected;
                bool passed = error is not null && Math.Abs(error.Value) <= AllowedErrorTonnes;
                return new CalibrationTestResult(
                    testCase.Checkpoint,
                    testCase.AdcCode,
                    expected,
                    actual?.Tonnes,
                    error,
                    actual?.Point.Id,
                    passed);
            })
            .ToList();
    }

    private static void Validate(IReadOnlyList<CalibrationAnchor> anchors)
    {
        if (anchors.Count < 2)
            throw new InvalidOperationException("Добавьте минимум две калибровочные точки.");
        if (anchors.Count(point => point.Mass == 0) != 1)
            throw new InvalidOperationException("Должна быть ровно одна нулевая точка.");
        if (anchors.Any(point => point.AdcCode is < 0 or > 65535))
            throw new InvalidOperationException("Код АЦП должен находиться в диапазоне 0–65535.");
        if (anchors.GroupBy(point => point.AdcCode).Any(group => group.Count() > 1))
            throw new InvalidOperationException("Коды АЦП калибровочных точек не должны повторяться.");
        if (anchors.GroupBy(point => point.Mass).Any(group => group.Count() > 1))
            throw new InvalidOperationException("Массы калибровочных точек не должны повторяться.");

        var ordered = anchors.OrderBy(point => point.AdcCode).ToList();
        if (ordered.Zip(ordered.Skip(1), (left, right) => right.Mass > left.Mass).Any(increasing => !increasing))
            throw new InvalidOperationException("При росте кода АЦП масса должна строго возрастать.");

        int zeroCode = anchors.Single(point => point.Mass == 0).AdcCode;
        if (anchors.Any(point => point.Mass != 0 && point.AdcCode <= zeroCode))
            throw new InvalidOperationException("Коды нагруженных точек должны быть больше кода нулевой точки.");
    }
}
