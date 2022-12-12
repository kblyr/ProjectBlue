using JIL.Auditing;
using MapsterMapper;

namespace JIL;

public static class MapperExtensions
{
    public static LoggedUser GetLoggedUser(this IMapper mapper, AuditInfo auditInfo)
    {
        return mapper.Map<AuditInfo.UserObj, LoggedUser>(auditInfo.User);
    }
}