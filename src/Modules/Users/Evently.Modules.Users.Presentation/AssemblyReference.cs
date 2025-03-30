using System.Reflection;

namespace Evently.Modules.Users.Presentation;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
