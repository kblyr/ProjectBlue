using System.Reflection;

namespace JIL.Accounts.EFCore;

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}