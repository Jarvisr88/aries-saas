namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ThemedMessageBoxContent : ContentControl
    {
        static ThemedMessageBoxContent()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedMessageBoxContent), new FrameworkPropertyMetadata(typeof(ThemedMessageBoxContent)));
        }
    }
}

