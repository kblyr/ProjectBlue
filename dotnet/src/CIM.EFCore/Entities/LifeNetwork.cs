namespace JIL.CIM.Entities;

public record LifeNetwork
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? ShortName { get; set; }
    public bool? Sex { get; set; }
    public short? MinAge { get; set; }
    public short? MaxAge { get; set; }
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