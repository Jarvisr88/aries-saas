namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.IO;
    using System.Reflection;

    [Wrapper, BindingFlags(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)]
    public interface IAssemblyDefinitionStatic
    {
        IAssemblyDefinition ReadAssembly(Stream stream);
        IAssemblyDefinition ReadAssembly(string path);
    }
}

