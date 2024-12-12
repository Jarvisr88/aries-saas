namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public static class NamescopeHelper
    {
        public static readonly DependencyProperty NamescopeProperty;

        static NamescopeHelper();
        public static NamescopeInfo GetNamescope(DependencyObject d);
        public static void SetNamescope(DependencyObject d, NamescopeInfo name);
    }
}

