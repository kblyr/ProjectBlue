namespace JIL.CIM.Entities;

public record Ministry
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? ShortName { get; set; }
    public int? HeadId { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public Member? Head { get; set; }
}

public record MinistryMember
{
    public long Id { get; set; }
    public int MinistryId { get; set; }
    public int MemberId { get; set; }
    public string? Role { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public Ministry? Ministry { get; set; }
    public Member? Member { get; set; }
}