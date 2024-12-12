namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Reflection;

    [Wrapper, BindingFlags(BindingFlags.NonPublic | BindingFlags.Instance)]
    public interface ITokenizerHelper
    {
        void LastTokenRequired();
        string NextTokenRequired();
    }
}

