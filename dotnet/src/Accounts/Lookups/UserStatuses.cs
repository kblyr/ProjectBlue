namespace JIL.Accounts.Lookups;

public sealed record UserStatusesLookup
{
    public const string CONFIGKEY = "JIL:Accounts:UserStatuses";

    public short Pending { get; init; } = 1;
    public short Active { get; init; } = 2;
    public short Deactivated { get; init; } = 3;
    public short Locked { get; init; } = 4;
}