namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class SimplePanel : Panel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement element in base.Children)
            {
                element.Arrange(new Rect(0.0, 0.0, finalSize.Width, finalSize.Height));
            }
            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (base.Children.Count == 0)
            {
                return new Size(0.0, 0.0);
            }
            Size size = new Size(0.0, 0.0);
            foreach (UIElement element in base.Children)
            {
                element.Measure(availableSize);
                Size desiredSize = element.DesiredSize;
                size.Width = Math.Max(size.Width, desiredSize.Width);
                size.Height = Math.Max(size.Height, desiredSize.Height);
            }
            return size;
        }
    }
}

