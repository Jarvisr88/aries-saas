namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Reflection;

    [Wrapper]
    public interface IFrameworkElementInstance
    {
        [BindingFlags(BindingFlags.NonPublic | BindingFlags.Instance)]
        string GetPlainText();
    }
}

