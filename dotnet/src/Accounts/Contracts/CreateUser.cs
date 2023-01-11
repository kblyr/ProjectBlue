using JIL.Accounts.Utilities;

namespace JIL.Accounts.Contracts;

public sealed record CreateUserCommand : IRequest, IUserFullNameSource
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Username { get; init; }
    public required string CipherPassword { get; init; }

    public sealed record Response : IResponse
    {
        public int Id { get; init; }
    }
}

public sealed record UserCreatedEvent : MediatR.INotification
{
    public int Id { get; init; }
    public required string FullName { get; init; }
    public required string Username { get; init; }
    public short StatusId { get; init; }
    public required LoggedUserResponse CreatedBy { get; init; }
}