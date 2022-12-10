using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JIL.Accounts.Lookups;

public static class DependencyExtensions
{
    public static IServiceCollection ConfigureAccountsLookups(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<PermissionsLookup>(configuration.GetSection(PermissionsLookup.CONFIGKEY))
            .Configure<RolesLookup>(configuration.GetSection(RolesLookup.CONFIGKEY))
            .Configure<UserStatusesLookup>(configuration.GetSection(UserStatusesLookup.CONFIGKEY));
    }
}