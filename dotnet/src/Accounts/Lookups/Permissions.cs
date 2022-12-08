namespace JIL.Accounts.Lookups;

public sealed record PermissionsLookup
{
    public int CreateUser { get; init; } = 1;
    public int ApproveUser { get; init; } = 2;
}