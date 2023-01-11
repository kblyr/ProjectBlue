namespace JIL.Accounts.Lookups;

public sealed record PermissionsLookup
{
    public const string CONFIGKEY = "JIL:Accounts:Permissions";

    public int CreateUser { get; init; } = 1;
    public int ApproveUser { get; init; } = 2;
}