namespace JIL.Authorization.Contracts;

public sealed record RemoveUserRoleCommand : IRequest
{
    public int UserId { get; init; }
    public int RoleId { get; init; }

    public sealed record Response : IResponse
    {
        public long Id { get; init; }
        public required LoggedUserResponse RemovedBy { get; init; }
    }
}