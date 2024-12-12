namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public static class DefaultStyleKeyHelper
    {
        public static object ClearDefaultStyleKey(this FrameworkContentElement element) => 
            DefaultStyleKeyFrameworkContentElementHelper.GetDefaultStyleKey(element);

        public static object ClearDefaultStyleKey(this FrameworkElement element) => 
            DefaultStyleKeyFrameworkElementHelper.GetDefaultStyleKey(element);

        public static Type GetControlDefaultStyleKey(Control control) => 
            (control != null) ? ((Type) DefaultStyleKeyControlHelper.GetDefaultStyleKey(control)) : null;

        public static object GetDefaultStyleKey(this FrameworkContentElement element) => 
            DefaultStyleKeyFrameworkContentElementHelper.GetDefaultStyleKey(element);

        public static object GetDefaultStyleKey(this FrameworkElement element) => 
            DefaultStyleKeyFrameworkElementHelper.GetDefaultStyleKey(element);

        public static void SetDefaultStyleKey(this FrameworkContentElement element, object value)
        {
            DefaultStyleKeyFrameworkContentElementHelper.SetDefaultStyleKey(element, value);
        }

        public static void SetDefaultStyleKey(this FrameworkElement element, object value)
        {
            DefaultStyleKeyFrameworkElementHelper.SetDefaultStyleKey(element, value);
        }
    }
}

