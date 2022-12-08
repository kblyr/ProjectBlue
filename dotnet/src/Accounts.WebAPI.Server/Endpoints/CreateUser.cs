namespace JIL.Accounts.Endpoints;

sealed class CreateUserEndpoint : ApiEndpoint<CreateUser.Request, CreateUserCommand>
{
    public override void Configure()
    {
        Post("user");
    }
}