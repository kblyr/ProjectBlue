namespace JIL.Authorization.Contracts;

public sealed record AddUserRolesCommand : IRequest
{
    public int UserId { get; init; }
    public required IEnumerable<int> RoleIds { get; init; }

    public sealed record Response : IResponse
    {
        public required IEnumerable<long> Ids { get; init; }
        public required LoggedUser AddedBy { get; init; }
    }
}