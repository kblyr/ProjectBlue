namespace JIL.Authorization.Contracts;

public sealed record RemoveUserPermissionsCommand : IRequest
{
    public int UserId { get; init; }
    public required IEnumerable<int> PermissionIds { get; init; }

    public sealed record Response : IResponse
    {
        public required IEnumerable<long> Ids { get; init; }
        public required LoggedUser RemovedBy { get; init; }
    }
}