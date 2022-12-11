using System.Reflection;

namespace JIL.EFCore.PostgreSQL;

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}