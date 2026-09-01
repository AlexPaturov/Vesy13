namespace Vesy13.Services.Configuration;

public static class WeightFormatter
{
    public static double RoundToDiscretization(double value, double discretization)
    {
        if (discretization <= 0) return value;
        return Math.Round(value / discretization, MidpointRounding.AwayFromZero) * discretization;
    }

    public static decimal RoundToDiscretization(decimal value, double discretization)
    {
        if (discretization <= 0) return value;
        decimal step = (decimal)discretization;
        return Math.Round(value / step, 0, MidpointRounding.AwayFromZero) * step;
    }
}
