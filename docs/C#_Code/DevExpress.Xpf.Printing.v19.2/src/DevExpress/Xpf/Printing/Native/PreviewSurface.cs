namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class PreviewSurface : Control
    {
        static PreviewSurface()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PreviewSurface), new FrameworkPropertyMetadata(typeof(PreviewSurface)));
        }

        public PreviewSurface()
        {
            base.FocusVisualStyle = null;
            base.Focusable = false;
        }
    }
}

