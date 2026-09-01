using System.Globalization;
using System.Text;

namespace ScaleListener.CalibrationTesting;

public static class CalibrationCsvExporter
{
    public static void Save(string path, IEnumerable<CalibrationTestResult> results)
    {
        var lines = new List<string>
        {
            "Checkpoint;AdcCode;ExpectedMass;ActualMass;ErrorTonnes;SelectedCalibrationPointId;Status"
        };

        lines.AddRange(results.Select(result => string.Join(";",
            Escape(result.Checkpoint),
            result.AdcCode.ToString(CultureInfo.InvariantCulture),
            result.ExpectedMass.ToString("F5", CultureInfo.InvariantCulture),
            result.ActualMass?.ToString("F5", CultureInfo.InvariantCulture) ?? "",
            result.ErrorTonnes?.ToString("F5", CultureInfo.InvariantCulture) ?? "",
            result.SelectedCalibrationPointId?.ToString(CultureInfo.InvariantCulture) ?? "",
            result.Passed ? "PASS" : "FAIL")));

        File.WriteAllLines(path, lines, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
    }

    private static string Escape(string value) =>
        value.Contains(';') || value.Contains('"')
             ? $"\"{value.Replace("\"", "\"\"")}\""
            : value;
}
