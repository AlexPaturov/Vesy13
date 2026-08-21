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

    private static async Task RunScheduledDatabaseCleanupAsync(LocalRepository ldb, SettingsService settings)
    {
        DateTime today = DateTime.Today;
        DateTime periodStartedOn = settings.Current.DatabaseCleanupPeriodStartedOn ?? today;
        if (today < periodStartedOn.AddDays(60) || settings.Current.DatabaseCleanupLastAttemptedOn?.Date == today)
            return;

        settings.Current.DatabaseCleanupLastAttemptedOn = today;
        try
        {
            settings.Save();
        }
        catch (Exception ex)
        {
            AuditLogger.Exception(AuditLogger.ErrorDb, "DatabaseCleanup",
                $"Не удалось сохранить дату попытки очистки; periodStartedOn={periodStartedOn:yyyy-MM-dd}", ex, "Settings");
            return;
        }

        try
        {
            DatabaseCleanupResult result = await ldb.CleanupDataOlderThan30DaysAsync();
            settings.Current.DatabaseCleanupPeriodStartedOn = today;
            settings.Current.DatabaseCleanupLastAttemptedOn = today;
            settings.Save();
            AuditLogger.Action(AuditLogger.DatabaseCleanup, "DatabaseCleanup",
                $"deletedWagonWeighings={result.DeletedWagonWeighings}; deletedAuditRecords={result.DeletedAuditRecords}", "PostgreSQL");
        }
        catch (Exception ex)
        {
            AuditLogger.Exception(AuditLogger.ErrorDb, "DatabaseCleanup",
                $"Не удалось очистить БД; periodStartedOn={periodStartedOn:yyyy-MM-dd}; attemptOn={today:yyyy-MM-dd}", ex, "PostgreSQL");
        }
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
        var mainForm = new MainForm(ldb, settings);
        mainForm.Shown += async (_, _) => await RunScheduledDatabaseCleanupAsync(ldb, settings);
        System.Windows.Forms.Application.Run(mainForm);
    }
}
