using JIL.Auditing;
using MapsterMapper;

namespace JIL;

public static class MapperExtensions
{
    public static LoggedUserResponse GetLoggedUser(this IMapper mapper, AuditInfo auditInfo)
    {
        return mapper.Map<AuditInfo.UserObj, LoggedUserResponse>(auditInfo.User);
    }
}