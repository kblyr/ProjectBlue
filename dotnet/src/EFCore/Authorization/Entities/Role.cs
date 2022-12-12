namespace JIL.Authorization.Entities;

public record Role
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public required IEnumerable<UserRole> UserRoles { get; set; }
}

public record UserRole
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public Role? Role { get; set; }
}