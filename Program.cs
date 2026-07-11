using QuestPDF.Infrastructure;
using Vesy13.Services.Configuration;
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
        var ldb = new LocalRepository();
        bool calibLoadedFromDb = ldb.LoadCalibrationFromDbAsync().GetAwaiter().GetResult();
        if (calibLoadedFromDb)
        {
            settings.UpdateCalibrationCache(ldb.CalibPoints, ldb.Dynamic);
            settings.Save();
        }
        else
        {
            ldb.RestoreLastKnownCalibration(settings.Current.CachedStaticPoints, settings.Current.CachedDynamicCalib);
        }
        AuditLogger.Initialize();
        AuditLogger.Action(AuditLogger.AppStarted, "Application", "Vesy13");
        if (!calibLoadedFromDb)
            AuditLogger.Action(AuditLogger.CalibrationFallback, "LocalRepository",
                $"БД недоступна на старте, калибровка восстановлена из локального кэша (обновлён {settings.Current.CalibCacheUpdatedAt:yyyy-MM-dd HH:mm:ss})");
        System.Windows.Forms.Application.Run(new MainForm(ldb, settings));
    }
}
