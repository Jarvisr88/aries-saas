namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    internal class AutoWidthColumnBehavior : Behavior<LinesGrid>
    {
        public static readonly DependencyProperty AutoWidthColumnIndexProperty = DependencyProperty.Register("AutoWidthColumnIndex", typeof(int), typeof(AutoWidthColumnBehavior), new PropertyMetadata(-1));

        private double CalculateAvailableWidth()
        {
            double num = 0.0;
            foreach (ColumnDefinition definition in (IEnumerable<ColumnDefinition>) base.AssociatedObject.ColumnDefinitions)
            {
                if (base.AssociatedObject.ColumnDefinitions.IndexOf(definition) != this.AutoWidthColumnIndex)
                {
                    num += Math.Min(definition.ActualWidth, definition.MinWidth);
                }
            }
            double num2 = base.AssociatedObject.ActualWidth - num;
            return ((num2 < 0.0) ? 0.0 : num2);
        }

        private double CalculateColumnChildrenWidth()
        {
            double num = 0.0;
            foreach (UIElement element in this.GetColumnChildren())
            {
                element.Measure(new Size(double.PositiveInfinity, element.DesiredSize.Height));
                Size desiredSize = element.DesiredSize;
                num = Math.Max(num, desiredSize.Width);
            }
            return (num + 1.0);
        }

        private IEnumerable<UIElement> GetColumnChildren() => 
            (IEnumerable<UIElement>) (from c in base.AssociatedObject.Children.Cast<FrameworkElement>()
                where Grid.GetColumn(c) == this.AutoWidthColumnIndex
                select c);

        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            base.AssociatedObject.SizeChanged -= new SizeChangedEventHandler(this.OnSizeChanged);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if ((base.AssociatedObject.ColumnDefinitions.Count > 1) && ((this.AutoWidthColumnIndex >= 0) && (this.AutoWidthColumnIndex < base.AssociatedObject.ColumnDefinitions.Count)))
            {
                double num = this.CalculateAvailableWidth();
                base.AssociatedObject.ColumnDefinitions[this.AutoWidthColumnIndex].Width = new GridLength(Math.Min(num, this.CalculateColumnChildrenWidth()), GridUnitType.Pixel);
            }
        }

        public int AutoWidthColumnIndex
        {
            get => 
                (int) base.GetValue(AutoWidthColumnIndexProperty);
            set => 
                base.SetValue(AutoWidthColumnIndexProperty, value);
        }
    }
}

