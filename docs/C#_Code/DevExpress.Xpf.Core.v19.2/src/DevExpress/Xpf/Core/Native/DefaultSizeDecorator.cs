namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    [Browsable(false), DXToolboxBrowsable(false)]
    public class DefaultSizeDecorator : Decorator
    {
        public static readonly DependencyProperty DefaultChildWidthProperty;
        public static readonly DependencyProperty DefaultChildHeightProperty;

        static DefaultSizeDecorator();
        protected override Size ArrangeOverride(Size finalSize);
        private static double GetCorrectValue(double available, double def);
        protected override Size MeasureOverride(Size availableSize);

        public double DefaultChildWidth { get; set; }

        public double DefaultChildHeight { get; set; }
    }
}

