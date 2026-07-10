using QuestPDF.Infrastructure;
using Vesy13.Services.Configuration;
using Vesy13.Services.Hardware;
using Vesy13.Services.Repositories;

namespace Vesy13;

static class Program
{
    [STAThread]
    static void Main()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        ApplicationConfiguration.Initialize();
        var settings = new SettingsService();
        settings.LoadOrCreate();
        using var staticSim = new SimA04ReaderStatic();
        using var dynamicSim = new SimA04ReaderDynamic();
        var       ldb = new LocalRepository();
        bool calibLoaded = ldb.LoadAsync().GetAwaiter().GetResult();
        if (calibLoaded)
        {
            settings.UpdateCalibrationCache(ldb.CalibPoints, ldb.Dynamic);
            settings.Save();
        }
        AuditLogger.Initialize();
        AuditLogger.Action(AuditLogger.AppStarted, "Application", "Vesy13");
        System.Windows.Forms.Application.Run(new MainForm(staticSim, dynamicSim, ldb, settings));
    }
}
