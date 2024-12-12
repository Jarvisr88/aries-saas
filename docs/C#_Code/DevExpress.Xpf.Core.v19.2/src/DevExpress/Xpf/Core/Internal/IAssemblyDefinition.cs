namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.IO;
    using System.Reflection;

    [Wrapper, BindingFlags(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)]
    public interface IAssemblyDefinition
    {
        void Write(Stream stream);
        void Write(string path);

        IAssemblyNameDefinition Name { get; }

        string FullName { get; }

        IModuleDefinition MainModule { get; }
    }
}

