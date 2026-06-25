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
        using var sim = new SimA04ReaderStatic();
        var       ldb = new LocalRepository();
        ldb.LoadAsync().GetAwaiter().GetResult();
        AuditLogger.Initialize();
        AuditLogger.Action(AuditLogger.AppStarted, "Application", "Vesy13");
        System.Windows.Forms.Application.Run(new MainForm(sim, ldb, settings));
    }
}