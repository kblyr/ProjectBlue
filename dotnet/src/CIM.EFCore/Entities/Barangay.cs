namespace JIL.CIM.Entities;

public record Barangay
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int CityId { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    
    public City? City { get; set; }
}