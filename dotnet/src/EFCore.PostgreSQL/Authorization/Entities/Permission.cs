using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JIL.Authorization.Entities;

sealed class PermissionETC : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permission", "Authorization");
    }
}

sealed class UserPermissionETC : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.ToTable("UserPermission", "Authorization");

        builder.HasOne(_ => _.Permission)
            .WithMany(_ => _.UserPermissions)
            .HasForeignKey(_ => _.PermissionId);
    }
}
