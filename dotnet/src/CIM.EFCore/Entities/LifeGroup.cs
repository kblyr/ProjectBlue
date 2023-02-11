namespace JIL.CIM.Entities;

public record LifeGroup
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? LeaderId { get; set; }
    public bool? Sex { get; set; }
    public short? MinAge { get; set; }
    public short? MaxAge { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public Member? Leader { get; set; }
}

public record LifeGroupMember
{
    public long Id { get; set; }
    public int LifeGroupId { get; set; }
    public int MemberId { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public LifeGroup? LifeGroup { get; set; }
    public Member? Member { get; set; }
}