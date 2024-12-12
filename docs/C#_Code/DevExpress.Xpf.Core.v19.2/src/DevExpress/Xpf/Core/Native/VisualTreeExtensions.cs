namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class VisualTreeExtensions
    {
        public static IEnumerable<DependencyObject> VisualChildren(this DependencyObject rootElement, bool includeRootElement = false);
        public static IEnumerable<DependencyObject> VisualParents(this DependencyObject rootElement, bool includeRootElement = false);
    }
}

