namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DXVirtualizingStackPanel : VirtualizingStackPanel
    {
        private double maxMeasureWidth;
        private double firstMeasureWidth;

        public void BringIndexIntoView(int index)
        {
            base.BringIndexIntoView(index);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = base.MeasureOverride(constraint);
            if (this.AllowClipping && (this.firstMeasureWidth > 0.0))
            {
                size.Width = this.firstMeasureWidth;
            }
            else if (size.Width > this.maxMeasureWidth)
            {
                this.maxMeasureWidth = size.Width;
            }
            else
            {
                size.Width = this.maxMeasureWidth;
            }
            if ((Math.Abs(this.firstMeasureWidth) < double.Epsilon) && !double.IsInfinity(size.Width))
            {
                this.firstMeasureWidth = size.Width;
            }
            return size;
        }

        public bool AllowClipping { get; set; }
    }
}

