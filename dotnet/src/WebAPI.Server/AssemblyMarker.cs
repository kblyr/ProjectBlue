using System.Reflection;

namespace JIL.WebAPI.Server;

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}