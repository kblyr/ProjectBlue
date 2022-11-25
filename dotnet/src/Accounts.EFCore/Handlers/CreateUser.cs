using JIL.Contracts;

namespace JIL.Accounts.Handlers;

sealed class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
    public Task<IResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
