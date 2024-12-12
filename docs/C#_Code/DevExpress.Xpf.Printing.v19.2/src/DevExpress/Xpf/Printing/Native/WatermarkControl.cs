namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class WatermarkControl : Control
    {
        static WatermarkControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkControl), new FrameworkPropertyMetadata(typeof(WatermarkControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}

