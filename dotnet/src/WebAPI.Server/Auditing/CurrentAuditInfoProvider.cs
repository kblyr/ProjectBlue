using System.Security.Principal;

namespace JIL.Auditing;

sealed class CurrentAuditInfoProvider : ICurrentAuditInfoProvider
{
    readonly IHttpContextAccessor _contextAccessor;

    public CurrentAuditInfoProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    AuditInfo? _current;
    public AuditInfo Current => (_current ??= GetCurrent()) with { Timestamp = DateTimeOffset.Now };

    AuditInfo GetCurrent()
    {
        var user = new AuditInfo.UserObj
        {
            Id = 0,
            Username = "",
            FullName = ""
        };

        if (_contextAccessor.HttpContext is not null && _contextAccessor.HttpContext.User.Identity is IIdentity identity && identity.IsAuthenticated)
        {
            user = user with
            {
                Id = _contextAccessor.HttpContext.User.TryGetClaimInt32(ClaimTypes.User.Id, out int id) ? id : 0,
                Username = _contextAccessor.HttpContext.User.TryGetClaim(ClaimTypes.User.Username, out string username) ? username : "",
                FullName = _contextAccessor.HttpContext.User.TryGetClaim(ClaimTypes.User.FullName, out string fullName) ? fullName : ""
            };
        }

        return new AuditInfo { User = user };
    }
}
