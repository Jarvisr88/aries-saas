namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Reflection;

    [Wrapper, BindingFlags(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)]
    public interface IModuleDefinition
    {
        string Name { get; set; }

        IMonoCollection<IResource> Resources { get; }
    }
}

