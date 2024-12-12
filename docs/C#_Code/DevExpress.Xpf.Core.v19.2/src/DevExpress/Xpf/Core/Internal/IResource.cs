namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Reflection;

    [BindingFlags(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), Wrapper]
    public interface IResource
    {
        string Name { get; set; }

        object Attributes { get; }
    }
}

