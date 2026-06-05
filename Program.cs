using QuestPDF.Infrastructure;
using Vesy13.Services;
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
        using var sim = new SimA04Reader();
        var       ldb = new LocalRepository();
        ldb.LoadAsync().GetAwaiter().GetResult();
        AuditLogger.Initialize();
        AuditLogger.Action(AuditLogger.AppStarted, "Application", "Vesy13");
        System.Windows.Forms.Application.Run(new MainForm(sim, ldb));
    }
}