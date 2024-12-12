namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class CustomSettingsHeaderedContentControl : HeaderedContentControl
    {
        static CustomSettingsHeaderedContentControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomSettingsHeaderedContentControl), new FrameworkPropertyMetadata(typeof(CustomSettingsHeaderedContentControl)));
        }
    }
}

