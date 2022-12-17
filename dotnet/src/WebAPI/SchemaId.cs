namespace JIL;

[AttributeUsage(AttributeTargets.Class)]
public class SchemaIdAttribute : Attribute
{
    public string SchemaId { get; }

    public SchemaIdAttribute(string schemaId)
    {
        SchemaId = schemaId;
    }

    public const string ValidationFailure = "res://jil/validation-failure";
}