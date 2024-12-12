namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public class DefaultStyleKeyFrameworkElementHelper : FrameworkElement
    {
        public static object GetDefaultStyleKey(FrameworkElement element) => 
            element.GetValue(FrameworkElement.DefaultStyleKeyProperty);

        public static void SetDefaultStyleKey(FrameworkElement element, object value)
        {
            element.SetValue(FrameworkElement.DefaultStyleKeyProperty, value);
        }
    }
}

