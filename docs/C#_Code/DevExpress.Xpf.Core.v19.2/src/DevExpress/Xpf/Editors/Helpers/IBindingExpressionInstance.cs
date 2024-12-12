namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Reflection;

    [Wrapper, BindingFlags(BindingFlags.NonPublic | BindingFlags.Instance)]
    public interface IBindingExpressionInstance
    {
        void Activate(object item);
        void Deactivate();

        object Value { get; }
    }
}

