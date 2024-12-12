namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public class DefaultStyleKeyFrameworkContentElementHelper : FrameworkContentElement
    {
        public static object GetDefaultStyleKey(FrameworkContentElement element) => 
            element.GetValue(FrameworkContentElement.DefaultStyleKeyProperty);

        public static void SetDefaultStyleKey(FrameworkContentElement element, object value)
        {
            element.SetValue(FrameworkContentElement.DefaultStyleKeyProperty, value);
        }
    }
}

