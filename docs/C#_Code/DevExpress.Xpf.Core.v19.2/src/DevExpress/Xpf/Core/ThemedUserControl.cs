namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    [Obsolete("use the standard UserControl instead")]
    public class ThemedUserControl : UserControl
    {
        static ThemedUserControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedUserControl), new FrameworkPropertyMetadata(typeof(ThemedUserControl)));
        }
    }
}

