using Vesy13.Services.Hardware;
using Vesy13.Services.Repositories;

namespace Vesy13;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        using var adc   = new AdcService();
        var       calib = new CalibrationService();
        calib.LoadAsync().GetAwaiter().GetResult();
        var db = new LocalDbService();
        System.Windows.Forms.Application.Run(new MainForm(adc, calib, db));
    }
}