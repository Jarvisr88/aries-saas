namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Markup;

    internal static class FrameworkElementExtensions
    {
        public static INameScope FindScope(DependencyObject d)
        {
            DependencyObject obj2;
            return FindScope(d, out obj2);
        }

        public static INameScope FindScope(DependencyObject d, out DependencyObject scopeOwner)
        {
            while (d != null)
            {
                INameScope nameScope = NameScope.GetNameScope(d);
                if (nameScope != null)
                {
                    scopeOwner = d;
                    return nameScope;
                }
                d = LogicalTreeHelper.GetParent(d);
            }
            scopeOwner = null;
            return null;
        }

        public static bool HasNameScope(DependencyObject d) => 
            FindScope(d) != null;
    }
}

