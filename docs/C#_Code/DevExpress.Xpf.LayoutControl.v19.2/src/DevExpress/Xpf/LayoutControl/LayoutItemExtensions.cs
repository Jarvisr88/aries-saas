namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class LayoutItemExtensions
    {
        public static LayoutItem GetLayoutItem(this FrameworkElement content, FrameworkElement root = null, bool explicitOnly = true)
        {
            LayoutItem item = content.FindElementByTypeInParents<LayoutItem>(root);
            return (((item == null) || (explicitOnly && !ReferenceEquals(item.Content, content))) ? null : item);
        }
    }
}

