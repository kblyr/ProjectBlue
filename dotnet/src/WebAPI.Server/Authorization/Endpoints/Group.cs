namespace JIL.Authorization.Endpoints;

sealed class AuthorizationGroup : Group
{
    public AuthorizationGroup()
    {
        Configure("auth", ep => ep.AllowAnonymous());
    }
}