using JIL.Converters;
using Mapster;

namespace JIL.Authorization.Converters;

static class MapConverters
{
    public static UserIdConverter UserId => MapContext.Current.GetService<UserIdConverter>();
    public static RoleIdConverter RoleId => MapContext.Current.GetService<RoleIdConverter>();
    public static UserRoleIdConverter UserRoleId => MapContext.Current.GetService<UserRoleIdConverter>();
    public static PermissionIdConverter PermissionId => MapContext.Current.GetService<PermissionIdConverter>();
    public static UserPermissionIdConverter UserPermissionId => MapContext.Current.GetService<UserPermissionIdConverter>();
}

public sealed class RoleIdConverter : Int32HashIdConverterBase, IHashIdConverter<int>
{
    public RoleIdConverter(string salt, int minHashLength) : base(salt, minHashLength) { }
}

public sealed class UserRoleIdConverter : Int64HashIdConverterBase, IHashIdConverter<long>
{
    public UserRoleIdConverter(string salt, int minHashLength) : base(salt, minHashLength) { }
}

public sealed class PermissionIdConverter : Int32HashIdConverterBase, IHashIdConverter<int>
{
    public PermissionIdConverter(string salt, int minHashLength) : base(salt, minHashLength) { }
}

public sealed class UserPermissionIdConverter : Int64HashIdConverterBase, IHashIdConverter<long>
{
    public UserPermissionIdConverter(string salt, int minHashLength) : base(salt, minHashLength) { }
}
