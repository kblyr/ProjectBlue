namespace JIL.Accounts;

public sealed class AccountsDbContext : DbContextBase<AccountsDbContext>
{
    public AccountsDbContext(DbContextOptions<AccountsDbContext> options, IEntityConfigAssemblyProvider<AccountsDbContext>? entityConfigAssemblyProvider) : base(options, entityConfigAssemblyProvider)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserStatus> UserStatuses => Set<UserStatus>();
}
