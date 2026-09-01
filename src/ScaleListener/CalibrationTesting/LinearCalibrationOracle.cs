namespace ScaleListener.CalibrationTesting;

public static class LinearCalibrationOracle
{
    public static decimal Calculate(IReadOnlyList<CalibrationAnchor> anchors, int adcCode)
    {
        if (anchors.Count < 2)
            throw new InvalidOperationException("Для линейного эталона нужны минимум две точки.");

        var ordered = anchors.OrderBy(point => point.AdcCode).ToList();
        CalibrationAnchor left;
        CalibrationAnchor right;

        if (adcCode <= ordered[0].AdcCode)
        {
            left = ordered[0];
            right = ordered[1];
        }
        else if (adcCode >= ordered[^1].AdcCode)
        {
            left = ordered[^2];
            right = ordered[^1];
        }
        else
        {
            int rightIndex = ordered.FindIndex(point => point.AdcCode >= adcCode);
            left = ordered[rightIndex - 1];
            right = ordered[rightIndex];
        }

        decimal codeRange = right.AdcCode - left.AdcCode;
        decimal position = (adcCode - left.AdcCode) / codeRange;
        return left.Mass + position * (right.Mass - left.Mass);
    }
}
