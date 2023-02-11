namespace JIL.CIM.Entities;

public record CivilStatus
{
    public short Id { get; set; }
    public required string Name { get; set; }
}