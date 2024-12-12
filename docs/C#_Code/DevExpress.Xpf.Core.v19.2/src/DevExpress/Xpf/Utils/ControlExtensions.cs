namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class ControlExtensions
    {
        public static void SetDefaultStyleKey(this FrameworkElement control, Type value)
        {
            DependencyProperty property = DefaultStyleKeyHelper.GetProperty();
            DependencyProperty property2 = property;
            lock (property2)
            {
                if (((Type) property.GetMetadata(value).DefaultValue) != value)
                {
                    property.OverrideMetadata(value, new FrameworkPropertyMetadata(value));
                }
            }
        }

        private class DefaultStyleKeyHelper : FrameworkElement
        {
            public static DependencyProperty GetProperty() => 
                FrameworkElement.DefaultStyleKeyProperty;
        }
    }
}

