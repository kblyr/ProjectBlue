namespace JIL.Accounts.Lookups;

public sealed record RolesLookup
{
    public const string CONFIGKEY = "JIL:Accounts:Roles";

    public int Administrator { get; init; } = 1;
}