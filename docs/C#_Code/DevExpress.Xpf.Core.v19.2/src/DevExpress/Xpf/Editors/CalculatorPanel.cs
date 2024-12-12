namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class CalculatorPanel : Panel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (base.Children.Count != 2)
            {
                return base.ArrangeOverride(finalSize);
            }
            finalSize.Width = Math.Max(finalSize.Width, base.Children[1].DesiredSize.Width);
            finalSize.Height = Math.Max(finalSize.Height, base.Children[0].DesiredSize.Height + base.Children[1].DesiredSize.Height);
            base.Children[0].Arrange(new Rect(0.0, 0.0, finalSize.Width, base.Children[0].DesiredSize.Height));
            base.Children[1].Arrange(new Rect(0.0, base.Children[0].DesiredSize.Height, finalSize.Width, finalSize.Height - base.Children[0].DesiredSize.Height));
            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (base.Children.Count != 2)
            {
                return base.MeasureOverride(availableSize);
            }
            Calculator calculator = LayoutHelper.FindParentObject<Calculator>(this);
            base.Children[1].Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            Size size = new Size {
                Width = base.Children[1].DesiredSize.Width
            };
            base.Children[0].Measure(new Size(size.Width, double.PositiveInfinity));
            size.Height = base.Children[0].DesiredSize.Height + base.Children[1].DesiredSize.Height;
            return size;
        }
    }
}

