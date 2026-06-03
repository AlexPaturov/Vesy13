using Vesy13.Services;

namespace Vesy13;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        using var adc   = new AdcService();
        var       calib = new CalibrationService();
        var       db    = new DatabaseService();
        Application.Run(new MainForm(adc, calib, db));
    }
}