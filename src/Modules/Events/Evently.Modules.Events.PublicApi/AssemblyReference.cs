using System.Reflection;

namespace Evently.Modules.Users.PublicApi;
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
