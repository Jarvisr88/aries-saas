namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class BestFitDecorator : Decorator
    {
        protected override Size ArrangeOverride(Size arrangeSize) => 
            new Size(0.0, 0.0);

        protected override Size MeasureOverride(Size constraint)
        {
            base.MeasureOverride(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return new Size(0.0, 0.0);
        }
    }
}

