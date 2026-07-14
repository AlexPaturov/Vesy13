using System.Text.Json;
using Vesy13.Models;

namespace Vesy13.Services.Configuration;

public sealed class SettingsService
{
    private const string DefaultAdminPassword = "vesy13fuck";
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    private readonly string _path;
    private AppSettings _settings = new();

    public SettingsService(string? path = null)
    {
        _path = path ?? System.IO.Path.Combine(AppContext.BaseDirectory, "settings.json");
    }

    public AppSettings Current => _settings;
    public string Path => _path;

    public void LoadOrCreate()
    {
        if (!File.Exists(_path))
        {
            _settings = CreateDefault();
            Save();
            return;
        }

        try
        {
            string json = File.ReadAllText(_path);
            _settings = JsonSerializer.Deserialize<AppSettings>(json, JsonOptions) ?? CreateDefault();
        }
        catch
        {
            _settings = CreateDefault();
            Save();
            return;
        }

        if (EnsureDefaults(_settings))
            Save();
    }

    public void Save() => File.WriteAllText(_path, JsonSerializer.Serialize(_settings, JsonOptions));

    public bool VerifyAdminPassword(string password) =>
        PasswordHasher.Verify(password, _settings.AdminPasswordHash, _settings.AdminPasswordSalt);

    public void SetAdminPassword(string password)
    {
        var (hash, salt) = PasswordHasher.Create(password);
        _settings.AdminPasswordHash = hash;
        _settings.AdminPasswordSalt = salt;
    }

    /// <summary>Обновляет локальный fallback-кэш калибровки последним известным состоянием из БД. Не вызывает Save().</summary>
    public void UpdateCalibrationCache(IReadOnlyList<CalibPoint> staticPoints, DynamicCalib dynamicCalib)
    {
        _settings.CachedStaticPoints = staticPoints.ToList();
        _settings.CachedDynamicCalib = dynamicCalib;
        _settings.CalibCacheUpdatedAt = DateTime.Now;
    }

    private static AppSettings CreateDefault()
    {
        var settings = new AppSettings();
        var (hash, salt) = PasswordHasher.Create(DefaultAdminPassword);
        settings.AdminPasswordHash = hash;
        settings.AdminPasswordSalt = salt;
        return settings;
    }

    private static bool EnsureDefaults(AppSettings settings)
    {
        bool changed = false;

        if (string.IsNullOrWhiteSpace(settings.AdcPortName))
        {
            settings.AdcPortName = "COM1";
            changed = true;
        }

        if (settings.MaxCapacityTonnes <= 0)
        {
            settings.MaxCapacityTonnes = 140.0;
            changed = true;
        }

        if (settings.WeightDiscretizationTonnes <= 0)
        {
            settings.WeightDiscretizationTonnes = 0.05;
            changed = true;
        }

        if (settings.OperatorZeroLimitPercent <= 0)
        {
            settings.OperatorZeroLimitPercent = 2.0;
            changed = true;
        }

        if (settings.AdminZeroLimitPercent <= 0)
        {
            settings.AdminZeroLimitPercent = 100.0;
            changed = true;
        }

        if (string.IsNullOrWhiteSpace(settings.AdminPasswordHash) || string.IsNullOrWhiteSpace(settings.AdminPasswordSalt))
        {
            var (hash, salt) = PasswordHasher.Create(DefaultAdminPassword);
            settings.AdminPasswordHash = hash;
            settings.AdminPasswordSalt = salt;
            changed = true;
        }

        if (settings.StaticClampMaxCode <= settings.StaticClampMinCode)
        {
            settings.StaticClampMinCode = 0;
            settings.StaticClampMaxCode = 65535;
            changed = true;
        }

        if (settings.DynamicClampMaxCode <= settings.DynamicClampMinCode)
        {
            settings.DynamicClampMinCode = 0;
            settings.DynamicClampMaxCode = 65535;
            changed = true;
        }

        if (settings.StaticDeltaMaxCodes <= 0)
        {
            settings.StaticDeltaMaxCodes = 5000;
            changed = true;
        }

        if (settings.DynamicDeltaMaxCodes <= 0)
        {
            settings.DynamicDeltaMaxCodes = 5000;
            changed = true;
        }

        if (settings.DynamicStuckSamples < 2)
        {
            settings.DynamicStuckSamples = 150;
            changed = true;
        }

        if (settings.StaticEmaAlpha is <= 0 or > 1)
        {
            settings.StaticEmaAlpha = 0.3;
            changed = true;
        }

        if (settings.DynamicEmaAlpha is <= 0 or > 1)
        {
            settings.DynamicEmaAlpha = 0.2;
            changed = true;
        }

        return changed;
    }
}
