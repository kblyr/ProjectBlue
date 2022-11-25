using System.Reflection;

namespace JIL;

public interface IEntityConfigAssemblyProvider<T> where T : DbContext
{
    Assembly Assembly { get; }
}

sealed class EntityConfigAssemblyProvider<T> : IEntityConfigAssemblyProvider<T>
    where T : DbContext
{
    public Assembly Assembly { get; }

    public EntityConfigAssemblyProvider(Assembly assembly)
    {
        Assembly = assembly;
    }
}