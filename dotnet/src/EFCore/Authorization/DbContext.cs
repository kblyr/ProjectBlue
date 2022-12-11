using JIL.Authorization.Entities;

namespace JIL.Authorization;

public sealed class AuthorizationDbContext : DbContextBase<AuthorizationDbContext>
{
    public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options, IEntityConfigAssemblyProvider<AuthorizationDbContext>? entityConfigAssemblyProvider) : base(options, entityConfigAssemblyProvider) { }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<UserPermission> UserPermissions => Set<UserPermission>();
}
