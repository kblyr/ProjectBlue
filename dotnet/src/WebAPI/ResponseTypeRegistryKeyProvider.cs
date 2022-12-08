namespace JIL;

public interface IApiResponseTypeRegistryKeyProvider
{
    string Get(Type responseType);
}

sealed class ApiResponseTypeRegistryKeyProvider : IApiResponseTypeRegistryKeyProvider
{
    static readonly Type t_schemaIdAttr = typeof(SchemaIdAttribute);

    readonly Dictionary<Type, string> _registryKeys = new();

    public string Get(Type responseType)
    {
        if (_registryKeys.ContainsKey(responseType))
        {
            return _registryKeys[responseType];
        }

        var key = default(string?);
        var schemaIdAttr = responseType.GetCustomAttributes(t_schemaIdAttr, false).FirstOrDefault() as SchemaIdAttribute;
        key = schemaIdAttr?.SchemaId ?? responseType.FullName;
        _registryKeys.Add(responseType, key ?? throw new FailedToGetApiResponseTypeKeyException(responseType));
        return key;

    }
}