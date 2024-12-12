namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class FloatingContainerHeaderPanel : Border
    {
        public static readonly DependencyProperty EnableLayoutCorrectionProperty = DependencyProperty.RegisterAttached("EnableLayoutCorrection", typeof(bool), typeof(FloatingContainerHeaderPanel), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetEnableLayoutCorrection(DependencyObject obj) => 
            (bool) obj.GetValue(EnableLayoutCorrectionProperty);

        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = base.MeasureOverride(availableSize);
            return (GetEnableLayoutCorrection(this) ? new Size(0.0, size.Height) : size);
        }

        public static void SetEnableLayoutCorrection(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableLayoutCorrectionProperty, value);
        }
    }
}

