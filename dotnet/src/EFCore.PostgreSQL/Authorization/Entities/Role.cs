using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JIL.Authorization.Entities;

sealed class RoleETC : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role", "Authorization");
    }
}

sealed class UserRoleETC : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRole", "Authorization");

        builder.HasOne(_ => _.Role)
            .WithMany(_ => _.UserRoles)
            .HasForeignKey(_ => _.RoleId);
    }
}
