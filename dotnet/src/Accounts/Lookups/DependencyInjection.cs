using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JIL.Accounts.Lookups;

public static class DependencyExtensions
{
    public static Accounts.DependencyInjector ConfigureLookups(this Accounts.DependencyInjector injector, IConfiguration configuration)
    {
        injector.Services
            .Configure<PermissionsLookup>(configuration.GetSection(PermissionsLookup.CONFIGKEY))
            .Configure<RolesLookup>(configuration.GetSection(RolesLookup.CONFIGKEY))
            .Configure<UserStatusesLookup>(configuration.GetSection(UserStatusesLookup.CONFIGKEY));
        return injector;
    }
}