namespace JIL.Authorization.Entities;

public record Permission
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public required IEnumerable<UserPermission> UserPermissions { get; set; }
}

public record UserPermission
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public int PermissionId { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public Permission? Permission { get; set; }
}