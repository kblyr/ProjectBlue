namespace JIL;

public static class HashIdConverterExtensions
{
    public static IEnumerable<string> Convert<T>(this IHashIdConverter<T> converter, IEnumerable<T> ids)
    {
        return ids.Select(id => converter.Convert(id));
    }

    public static IEnumerable<T> Convert<T>(this IHashIdConverter<T> converter, IEnumerable<string> hashIds)
    {
        return hashIds.Select(hashId => converter.Convert(hashId));
    }
}