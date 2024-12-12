namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class MinSizeArranger : Decorator
    {
        public static readonly DependencyProperty MinChildWidthProperty = DependencyProperty.Register("MinChildWidth", typeof(double), typeof(MinSizeArranger), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange));
        public static readonly DependencyProperty MinChildHeightProperty = DependencyProperty.Register("MinChildHeight", typeof(double), typeof(MinSizeArranger), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange));

        protected override Size ArrangeOverride(Size arrangeSize) => 
            this.ArrangeOverride(((arrangeSize.Width < this.MinChildWidth) || (arrangeSize.Height < this.MinChildHeight)) ? new Size(0.0, 0.0) : arrangeSize);

        protected override Size MeasureOverride(Size constraint)
        {
            base.MeasureOverride(constraint);
            return new Size(0.0, 0.0);
        }

        public double MinChildWidth
        {
            get => 
                (double) base.GetValue(MinChildWidthProperty);
            set => 
                base.SetValue(MinChildWidthProperty, value);
        }

        public double MinChildHeight
        {
            get => 
                (double) base.GetValue(MinChildHeightProperty);
            set => 
                base.SetValue(MinChildHeightProperty, value);
        }
    }
}

