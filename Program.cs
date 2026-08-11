using QuestPDF.Infrastructure;
using Vesy13.Services.Configuration;
using Vesy13.Services.Repositories;

namespace Vesy13;

static class Program
{
    private static void ConfigureUnhandledExceptionLogging()
    {
        System.Windows.Forms.Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        System.Windows.Forms.Application.ThreadException += (_, args) =>
        {
            AuditLogger.UnhandledException(args.Exception, "Application.ThreadException");
            MessageBox.Show("Произошла критическая ошибка. Подробности сохранены в журнале.", "Vesy13", MessageBoxButtons.OK, MessageBoxIcon.Error);
            System.Windows.Forms.Application.Exit();
        };
        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
        {
            var exception = args.ExceptionObject as Exception ?? new InvalidOperationException(args.ExceptionObject?.ToString());
            AuditLogger.UnhandledException(exception, "AppDomain.UnhandledException");
        };
        TaskScheduler.UnobservedTaskException += (_, args) =>
        {
            AuditLogger.UnhandledException(args.Exception, "TaskScheduler.UnobservedTaskException");
            args.SetObserved();
        };
    }

    [STAThread]
    static void Main()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        ApplicationConfiguration.Initialize();
        ConfigureUnhandledExceptionLogging();
        var settings = new SettingsService();
        settings.LoadOrCreate();
        var ldb = new LocalRepository();
        bool calibLoadedFromDb = ldb.LoadCalibrationFromDbAsync().GetAwaiter().GetResult();
        if (calibLoadedFromDb)
        {
            settings.UpdateCalibrationCache(ldb.CalibPoints, ldb.ActiveDirectionCorrectionProfile);
            settings.Save();
        }
        else
        {
            ldb.RestoreLastKnownCalibration(settings.Current.CachedStaticPoints, settings.Current.CachedDirectionCorrectionProfile);
        }
        AuditLogger.Initialize();
        AuditLogger.Action(AuditLogger.AppStarted, "Application", "Vesy13");
        if (!calibLoadedFromDb)
            AuditLogger.Action(AuditLogger.CalibrationFallback, "LocalRepository",
                $"БД недоступна на старте, настройки взвешивания восстановлены из локального кэша (обновлён {settings.Current.CalibCacheUpdatedAt:yyyy-MM-dd HH:mm:ss})");
        System.Windows.Forms.Application.Run(new MainForm(ldb, settings));
    }
}
