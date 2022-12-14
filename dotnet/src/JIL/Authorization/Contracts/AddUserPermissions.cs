namespace JIL.Authorization.Contracts;

public sealed record AddUserPermissionsCommand : IRequest
{
    public int UserId { get; init; }
    public required IEnumerable<int> PermissionIds { get; init; }

    public sealed record Response : IResponse
    {
        public required IEnumerable<long> Ids { get; init; }
        public required LoggedUser AddedBy { get; init; }
    }
}