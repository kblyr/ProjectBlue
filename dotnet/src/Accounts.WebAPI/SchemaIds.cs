namespace JIL.Accounts;

static class RequestSchemaIds
{
    public const string CreateUser = "req://accounts.jil/create-user";
}

static class ResponseSchemaIds
{
    public const string CreateUser = "res://accounts.jil/create-user";
    public const string UsernameAlreadyExists = "res://accounts.jil/username-already-exists";
}