namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System.IO;
    using System.Reflection;

    [BindingFlags(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), Wrapper]
    public interface IEmbeddedResource : IResource
    {
        Stream GetResourceStream();
    }
}

