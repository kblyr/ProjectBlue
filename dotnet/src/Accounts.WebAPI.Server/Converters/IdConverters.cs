using JIL;
using Mapster;

namespace JIL.Accounts.Converters;

static class MapConverters
{
    public static UserIdConverter UserId => MapContext.Current.GetService<UserIdConverter>();
}

public sealed class UserIdConverter : Int32HashIdConverterBase, IHashIdConverter<int>
{
    public UserIdConverter(string salt, int minHashLength) : base(salt, minHashLength)
    {
    }
}
