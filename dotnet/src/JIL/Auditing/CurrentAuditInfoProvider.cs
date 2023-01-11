namespace JIL.Auditing;

public interface ICurrentAuditInfoProvider
{
    AuditInfo Current { get; }
}