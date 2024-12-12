namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class DefaultStyleKeyControlHelper : Control
    {
        public static object GetDefaultStyleKey(Control element) => 
            element.GetValue(FrameworkElement.DefaultStyleKeyProperty);
    }
}

