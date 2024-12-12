namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class SuperTipPanel : Panel
    {
        protected override unsafe Size ArrangeOverride(Size finalSize)
        {
            Point location = new Point();
            for (int i = 0; i < base.Children.Count; i++)
            {
                if (base.Children[i].Visibility != Visibility.Collapsed)
                {
                    Size desiredSize = base.Children[i].DesiredSize;
                    if (base.Children[i] is SuperTipItemControlSeparator)
                    {
                        desiredSize.Width = finalSize.Width;
                    }
                    base.Children[i].Arrange(new Rect(location, desiredSize));
                    Point* pointPtr1 = &location;
                    pointPtr1.Y += base.Children[i].DesiredSize.Height;
                }
            }
            return finalSize;
        }

        protected override unsafe Size MeasureOverride(Size availableSize)
        {
            Size size = new Size();
            for (int i = 0; i < base.Children.Count; i++)
            {
                base.Children[i].Measure(availableSize);
                if (base.Children[i].Visibility != Visibility.Collapsed)
                {
                    size.Width = Math.Max(size.Width, base.Children[i].DesiredSize.Width);
                    Size* sizePtr1 = &size;
                    sizePtr1.Height += base.Children[i].DesiredSize.Height;
                }
            }
            return size;
        }
    }
}

