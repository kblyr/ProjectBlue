namespace JIL.Accounts.Entities;

public record User 
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Username { get; set; }
    public required string HashedPassword { get; set; }
    public required string PasswordSalt { get; set; }
    public short StatusId { get; set; }

    public required string FullName { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public required UserStatus Status { get; set; }
}

public record UserStatus
{
    public short Id { get; set; }
    public required string Name { get; set; }
}