namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class DXContentControl : ContentControl
    {
        static DXContentControl()
        {
            Control.IsTabStopProperty.OverrideMetadata(typeof(DXContentControl), new FrameworkPropertyMetadata(false));
            UIElement.FocusableProperty.OverrideMetadata(typeof(DXContentControl), new FrameworkPropertyMetadata(false));
        }
    }
}

