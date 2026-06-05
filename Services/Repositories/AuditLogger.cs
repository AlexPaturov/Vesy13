using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Dapper;
using Microsoft.ApplicationInsights.Extensibility;
using Npgsql;
using Serilog;
using Serilog.Sinks.ApplicationInsights.TelemetryConverters;
using Vesy13.Models;

namespace Vesy13.Services.Repositories;

public static class AuditLogger
{
    // ── Event IDs ─────────────────────────────────────────────────────────────
    public const int FormOpened        = 1001;
    public const int WeighingSaved     = 1002;
    public const int RecordTransferred = 1003;
    public const int SlipGenerated     = 1004;
    public const int RecordUpdated     = 1005;
    public const int TrainFinished     = 1006;
    public const int CalibrationSaved  = 1007;
    public const int AdcConnected      = 2001;
    public const int AdcDisconnected   = 2002;
    public const int AdminLogin        = 2003;
    public const int AppStarted        = 2004;
    public const int ErrorDb           = 9001;
    public const int ErrorAdc          = 9002;
    public const int ErrorPdf          = 9003;
    public const int ErrorGeneral      = 9099;

    private const string ConnStr =
        "Host=localhost;Port=5432;Database=scale_db;Username=scale_user;Password=fluffyBunny";

    private const string AppInsightsConnStr =
        "InstrumentationKey=0dfea935-2abc-4952-b8c8-24d409bae62d;" +
        "IngestionEndpoint=https://polandcentral-0.in.applicationinsights.azure.com/;" +
        "LiveEndpoint=https://polandcentral.livediagnostics.monitor.azure.com/;" +
        "ApplicationId=67cf9bc0-5a58-4811-aff8-b74030a23a54";

    private static ILogger _log = new LoggerConfiguration().CreateLogger();

    private static string? _sid;
    private static string? _userName;
    private static string? _domainName;
    private static string? _logonId;
    private static string? _computer;
    private static string? _ipAddress;
    private static int?    _pid;
    private static string? _pname;

    // ── P/Invoke ──────────────────────────────────────────────────────────────

    [StructLayout(LayoutKind.Sequential)]
    private struct Luid { public uint LowPart; public int HighPart; }

    [StructLayout(LayoutKind.Sequential)]
    private struct TokenStatistics
    {
        public Luid  TokenId;
        public Luid  AuthenticationId;
        public long  ExpirationTime;
        public int   TokenType;
        public int   ImpersonationLevel;
        public uint  DynamicCharged;
        public uint  DynamicAvailable;
        public uint  GroupCount;
        public uint  PrivilegeCount;
        public Luid  ModifiedId;
    }

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool GetTokenInformation(
        IntPtr TokenHandle, int TokenInformationClass,
        ref TokenStatistics TokenInformation,
        uint TokenInformationLength, out uint ReturnLength);

    // ── Initialization ────────────────────────────────────────────────────────

    public static void Initialize()
    {
        CollectContext();

        try
        {
            var aiConfig = new TelemetryConfiguration { ConnectionString = AppInsightsConnStr };
            _log = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.WithProperty("Application",       "Vesy13")
                .Enrich.WithProperty("Computer",          _computer)
                .Enrich.WithProperty("SubjectUserSid",    _sid)
                .Enrich.WithProperty("SubjectUserName",   _userName)
                .Enrich.WithProperty("SubjectDomainName", _domainName)
                .Enrich.WithProperty("SubjectLogonId",    _logonId)
                .Enrich.WithProperty("ProcessId",         _pid)
                .Enrich.WithProperty("ProcessName",       _pname)
                .Enrich.WithProperty("WorkstationName",   _computer)
                .Enrich.WithProperty("IpAddress",         _ipAddress)
                .WriteTo.ApplicationInsights(aiConfig, new TraceTelemetryConverter())
                .CreateLogger();
        }
        catch { /* AI unavailable — logger stays as no-op */ }

        _ = EnsureTableAsync();
    }

    // ── Public API ────────────────────────────────────────────────────────────

    public static void Action(int eventId, string objectType, string objectName,
        string objectServer = "Vesy13", string? objectHandle = null)
        => Write(eventId, "Audit Success", objectType, objectName, objectServer, objectHandle);

    public static void Error(int eventId, string objectType, string objectName,
        string objectServer = "Vesy13", string? objectHandle = null)
        => Write(eventId, "Audit Failure", objectType, objectName, objectServer, objectHandle);

    public static async Task<List<AuditRecord>> GetLogsAsync(DateTime from, DateTime to)
    {
        try
        {
            await using var conn = new NpgsqlConnection(ConnStr);
            await conn.OpenAsync();
            var rows = await conn.QueryAsync<AuditRecord>(@"
                SELECT id,
                       time_created        AS TimeCreated,
                       event_id            AS EventId,
                       keywords            AS Keywords,
                       computer            AS Computer,
                       subject_user_sid    AS SubjectUserSid,
                       subject_user_name   AS SubjectUserName,
                       subject_domain_name AS SubjectDomainName,
                       subject_logon_id    AS SubjectLogonId,
                       object_server       AS ObjectServer,
                       object_type         AS ObjectType,
                       object_name         AS ObjectName,
                       object_handle       AS ObjectHandle,
                       process_id          AS ProcessId,
                       process_name        AS ProcessName,
                       workstation_name    AS WorkstationName,
                       ip_address          AS IpAddress
                FROM audit_log
                WHERE time_created >= @from AND time_created < @to
                ORDER BY time_created DESC
                LIMIT 5000",
                new { from = from.ToUniversalTime(), to = to.ToUniversalTime() });
            return rows.ToList();
        }
        catch { return []; }
    }

    // ── Private ───────────────────────────────────────────────────────────────

    private static void Write(int eventId, string keywords,
        string? objectType, string? objectName, string? objectServer, string? objectHandle)
    {
        var now = DateTime.UtcNow;

        try
        {
            _log.ForContext("EventId",      eventId)
                .ForContext("Keywords",     keywords)
                .ForContext("ObjectServer", objectServer)
                .ForContext("ObjectType",   objectType)
                .ForContext("ObjectName",   objectName)
                .ForContext("ObjectHandle", objectHandle)
                .Information("[{EventId}] {Keywords} | {ObjectType} | {ObjectName}",
                    eventId, keywords, objectType, objectName);
        }
        catch { }

        _ = WriteToDbAsync(now, eventId, keywords, objectServer, objectType, objectName, objectHandle);
    }

    private static async Task WriteToDbAsync(
        DateTime timeCreated, int eventId, string keywords,
        string? objectServer, string? objectType, string? objectName, string? objectHandle)
    {
        try
        {
            await using var conn = new NpgsqlConnection(ConnStr);
            await conn.OpenAsync();
            await conn.ExecuteAsync(@"
                INSERT INTO audit_log (
                    time_created, event_id, keywords, computer,
                    subject_user_sid, subject_user_name, subject_domain_name, subject_logon_id,
                    object_server, object_type, object_name, object_handle,
                    process_id, process_name, workstation_name, ip_address)
                VALUES (
                    @timeCreated, @eventId, @keywords, @computer,
                    @sid, @userName, @domainName, @logonId,
                    @objectServer, @objectType, @objectName, @objectHandle,
                    @processId, @processName, @workstation, @ipAddress)",
                new {
                    timeCreated  = timeCreated,
                    eventId      = eventId,
                    keywords     = keywords,
                    computer     = _computer,
                    sid          = _sid,
                    userName     = _userName,
                    domainName   = _domainName,
                    logonId      = _logonId,
                    objectServer = objectServer,
                    objectType   = objectType,
                    objectName   = objectName,
                    objectHandle = objectHandle,
                    processId    = _pid,
                    processName  = _pname,
                    workstation  = _computer,
                    ipAddress    = _ipAddress,
                });
        }
        catch { /* never throw from logger */ }
    }

    private static async Task EnsureTableAsync()
    {
        try
        {
            await using var conn = new NpgsqlConnection(ConnStr);
            await conn.OpenAsync();
            await conn.ExecuteAsync(@"
                CREATE TABLE IF NOT EXISTS audit_log (
                    id                  BIGSERIAL       PRIMARY KEY,
                    time_created        TIMESTAMPTZ,
                    event_id            INTEGER,
                    keywords            VARCHAR(20),
                    computer            VARCHAR(100),
                    subject_user_sid    VARCHAR(200),
                    subject_user_name   VARCHAR(200),
                    subject_domain_name VARCHAR(200),
                    subject_logon_id    VARCHAR(100),
                    object_server       VARCHAR(200),
                    object_type         VARCHAR(100),
                    object_name         TEXT,
                    object_handle       VARCHAR(200),
                    process_id          INTEGER,
                    process_name        TEXT,
                    workstation_name    VARCHAR(100),
                    ip_address          VARCHAR(50)
                )");
        }
        catch { }
    }

    private static void CollectContext()
    {
        try
        {
            using var identity = WindowsIdentity.GetCurrent();
            _sid     = identity.User?.Value;
            var name = identity.Name ?? "";
            var slash = name.IndexOf('\\');
            _domainName = slash > 0 ? name[..slash] : Environment.UserDomainName;
            _userName   = slash > 0 ? name[(slash + 1)..] : name;
            _logonId    = GetLogonId(identity.Token);
        }
        catch { }

        try { _computer = Environment.MachineName; } catch { }

        try
        {
            _ipAddress = Dns.GetHostAddresses(Dns.GetHostName())
                .FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)
                ?.ToString();
        }
        catch { }

        try
        {
            var proc = Process.GetCurrentProcess();
            _pid   = proc.Id;
            _pname = proc.MainModule?.FileName;
        }
        catch { }
    }

    private static string? GetLogonId(IntPtr token)
    {
        try
        {
            var stats = new TokenStatistics();
            if (GetTokenInformation(token, 10,
                ref stats, (uint)Marshal.SizeOf<TokenStatistics>(), out _))
                return $"{stats.AuthenticationId.HighPart:X8}-{stats.AuthenticationId.LowPart:X8}";
        }
        catch { }
        return null;
    }
}
