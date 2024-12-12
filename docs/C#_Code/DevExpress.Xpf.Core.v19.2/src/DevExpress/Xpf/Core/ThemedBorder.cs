namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ThemedBorder : Border
    {
        static ThemedBorder()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedBorder), new FrameworkPropertyMetadata(typeof(ThemedBorder)));
        }
    }
}

