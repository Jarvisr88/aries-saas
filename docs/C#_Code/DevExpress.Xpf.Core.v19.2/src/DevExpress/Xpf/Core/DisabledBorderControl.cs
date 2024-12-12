namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class DisabledBorderControl : ContentControl
    {
        static DisabledBorderControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DisabledBorderControl), new FrameworkPropertyMetadata(typeof(DisabledBorderControl)));
        }
    }
}

