namespace JIL.Accounts;

sealed class ResponseTypeMapRegistration : IResponseTypeMapRegistration
{
    public void Register(IResponseTypeMapRegistry registry)
    {
        registry
            .RegisterCreated<CreateUserCommand.Response, CreateUser.Response>()
            .RegisterBadRequest<UsernameAlreadyExistsError, UsernameAlreadyExists.Response>()
            ;
    }
}
