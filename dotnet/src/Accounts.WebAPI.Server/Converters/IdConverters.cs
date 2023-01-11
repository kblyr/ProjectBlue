using JIL.Converters;
using Mapster;

namespace JIL.Accounts.Converters;

static class MapConverters
{
    public static UserIdConverter UserId => MapContext.Current.GetService<UserIdConverter>();
}