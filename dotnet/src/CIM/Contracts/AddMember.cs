namespace JIL.CIM.Contracts;

public sealed record AddMemberCommand : IRequest
{
    public required string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public required string LastName { get; init; }
    public string? NameSuffix { get; init; }
    public DateTime BirthDate { get; init; }
    public bool Sex { get; init; }
    public short CivilStatusId { get; init; }
    public string? Occupation { get; init; }
    public string? LandlineNumber { get; init; }
    public string? MobileNumber { get; init; }
    public string? EmailAddress { get; init; }
    public string? HouseBuildingNumber { get; init; }
    public string? Street { get; init; }
    public int BarangayId { get; init; }
    public int CityId { get; init; }

    public sealed record Response : IResponse
    {
        public int Id { get; init; }
    }
}