using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JIL.Accounts.Entities;

sealed class UserETC : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User", "Accounts");

        builder.HasOne(_ => _.Status)
            .WithMany()
            .HasForeignKey(_ => _.StatusId);
    }
}

sealed class UserStatusETC : IEntityTypeConfiguration<UserStatus>
{
    public void Configure(EntityTypeBuilder<UserStatus> builder)
    {
        builder.ToTable("UserStatus", "Accounts");
    }
}
