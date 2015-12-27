using System.Reflection;

[assembly: AssemblyTitle("NeverNull")]
[assembly: AssemblyProduct("NeverNull")]
[assembly:
    AssemblyDescription(
        "A Option type that prevents using null or 'magic values' (NullObject, exit code -1, index out of range, etc.) in your code."
        )]
[assembly: AssemblyVersion("3.2.0")]
[assembly: AssemblyFileVersion("3.2.0")]

namespace System {
    static class AssemblyVersionInformation {
        internal const string Version = "3.2.0";
    }
}