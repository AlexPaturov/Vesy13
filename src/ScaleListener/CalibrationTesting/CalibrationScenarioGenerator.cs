namespace ScaleListener.CalibrationTesting;

public static class CalibrationScenarioGenerator
{
    public static IReadOnlyList<CalibrationTestCase> Generate(IReadOnlyList<CalibrationAnchor> anchors)
    {
        var ordered = anchors.OrderBy(point => point.AdcCode).ToList();
        var cases = new SortedDictionary<int, string>();

        foreach (var point in ordered)
        {
            Add(cases, point.AdcCode, $"Точка {point.Mass:G} т");
            Add(cases, point.AdcCode - 1, $"Перед точкой {point.Mass:G} т");
            Add(cases, point.AdcCode + 1, $"После точки {point.Mass:G} т");
        }

        for (int index = 0; index < ordered.Count - 1; index++)
        {
            var left = ordered[index];
            var right = ordered[index + 1];
            AddFraction(cases, left, right, 0.25m, "25%");
            AddFraction(cases, left, right, 0.50m, "50%");
            AddFraction(cases, left, right, 0.75m, "75%");
        }

        return cases
            .Where(item => item.Key is >= 0 and <= 65535)
            .Select(item => new CalibrationTestCase(item.Value, item.Key))
            .ToList();
    }

    private static void AddFraction(
        IDictionary<int, string> cases,
        CalibrationAnchor left,
        CalibrationAnchor right,
        decimal fraction,
        string fractionText)
    {
        int code = left.AdcCode + (int)decimal.Round(
            (right.AdcCode - left.AdcCode) * fraction,
            0,
            MidpointRounding.AwayFromZero);
        Add(cases, code, $"{fractionText}: {left.Mass:G}–{right.Mass:G} т");
    }

    private static void Add(IDictionary<int, string> cases, int code, string checkpoint)
    {
        if (code is >= 0 and <= 65535 && !cases.ContainsKey(code))
            cases.Add(code, checkpoint);
    }
}
