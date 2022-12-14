namespace JIL.Authorization.Contracts;

public sealed record RemoveUserPermissionCommand : IRequest
{
    public int UserId { get; init; }
    public int PermissionId { get; init; }

    public sealed record Response : IResponse
    {
        public long Id { get; init; }
        public required LoggedUser RemovedBy { get; init; }
    }
}