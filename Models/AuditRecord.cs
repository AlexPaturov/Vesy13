namespace Vesy13.Models;

/// <summary>
/// Строка из таблицы audit_log. Все поля nullable: в момент записи любое из них может быть недоступно.
/// </summary>
public class AuditRecord
{
    public long      Id                { get; set; }
    public DateTime? TimeCreated       { get; set; }
    public int?      EventId           { get; set; }
    public string?   Keywords          { get; set; }
    public string?   Computer          { get; set; }
    public string?   SubjectUserSid    { get; set; }
    public string?   SubjectUserName   { get; set; }
    public string?   SubjectDomainName { get; set; }
    public string?   SubjectLogonId    { get; set; }
    public string?   ObjectServer      { get; set; }
    public string?   ObjectType        { get; set; }
    public string?   ObjectName        { get; set; }
    public string?   ObjectHandle      { get; set; }
    public int?      ProcessId         { get; set; }
    public string?   ProcessName       { get; set; }
    public string?   WorkstationName   { get; set; }
    public string?   IpAddress         { get; set; }
}
