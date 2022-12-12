namespace JIL.Authorization.Contracts;

public sealed record AddUserPermissionCommand : IRequest
{
    public int UserId { get; init; }
    public int PermissionId { get; init; }

    public sealed record Response : IResponse
    {
        public long Id { get; init; }
        public required LoggedUser AddedBy { get; init; }
    }
}