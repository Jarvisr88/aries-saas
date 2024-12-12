namespace DevExpress.Xpf.Printing.Parameters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public class AutoFitColumnGrid : Grid
    {
        public AutoFitColumnGrid()
        {
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
        }

        private double CalculateFirstColumnAvailableWidth()
        {
            double num = 0.0;
            for (int i = 1; i < base.ColumnDefinitions.Count; i++)
            {
                GridLength width = base.ColumnDefinitions[i].Width;
                double maxMinWidth = Math.Max(width.Value, base.ColumnDefinitions[i].MinWidth);
                this.GetColumnChildren(i).ForEach(delegate (FrameworkElement x) {
                    maxMinWidth = Math.Max(maxMinWidth, x.MinWidth);
                });
                num += maxMinWidth;
            }
            double num2 = base.ActualWidth - num;
            return ((num2 < 0.0) ? 0.0 : num2);
        }

        private List<FrameworkElement> GetColumnChildren(int columnIndex) => 
            (from x in base.Children.OfType<FrameworkElement>()
                where GetColumn(x) == columnIndex
                select x).ToList<FrameworkElement>();

        private double GetFirstColumnDesiredWidth()
        {
            double width = 0.0;
            this.GetColumnChildren(0).ForEach(delegate (FrameworkElement x) {
                x.Measure(new Size(double.PositiveInfinity, x.ActualHeight));
                width = Math.Max(width, x.DesiredSize.Width);
            });
            return (width + 1.0);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = base.MeasureOverride(constraint);
            return new Size(constraint.Width, size.Height);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (base.ColumnDefinitions.Count > 1)
            {
                double num = this.CalculateFirstColumnAvailableWidth();
                base.ColumnDefinitions[0].Width = new GridLength(Math.Min(num, this.GetFirstColumnDesiredWidth()), GridUnitType.Pixel);
            }
        }
    }
}

