namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Reflection;

    [BindingFlags(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), Wrapper]
    public interface IAssemblyNameDefinition
    {
        string Name { get; set; }

        System.Version Version { get; set; }
    }
}

