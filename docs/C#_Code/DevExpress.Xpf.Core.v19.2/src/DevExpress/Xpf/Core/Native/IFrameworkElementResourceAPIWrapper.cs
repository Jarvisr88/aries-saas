namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Windows;

    public interface IFrameworkElementResourceAPIWrapper
    {
        [BindingFlags(BindingFlags.NonPublic | BindingFlags.Static)]
        object FindTemplateResourceInTree(DependencyObject target, ArrayList keys, int exactMatch, ref int bestMatch);
    }
}

