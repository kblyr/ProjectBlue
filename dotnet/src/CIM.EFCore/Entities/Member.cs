namespace JIL.CIM.Entities;

public record Member
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
    public string? NameSuffix { get; set; }
    public DateTime BirthDate { get; set; }
    public bool Sex { get; set; }
    public short CivilStatusId { get; set; }
    public string? Occupation { get; set; }
    public string? LandlineNumber { get; set; }
    public string? MobileNumber { get; set; }
    public string? EmailAddress { get; set; }
    public string? HouseBuildingNumber { get; set; }
    public string? Street { get; set; }
    public int BarangayId { get; set; }
    public int CityId { get; set; }
    
    public required string FullName { get; set; }
    public required string FullAddress { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    
    public CivilStatus? CivilStatus { get; set; }
    public Barangay? Barangay { get; set; }
    public City? City { get; set; }
}