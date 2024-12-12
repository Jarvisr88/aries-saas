namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ButtonsPanel : Panel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            Size desiredSize;
            double width = 0.0;
            for (int i = 0; i < base.InternalChildren.Count; i++)
            {
                desiredSize = base.InternalChildren[i].DesiredSize;
                width += desiredSize.Width;
            }
            Size size = new Size(width, finalSize.Height);
            if (width.AreClose(0.0))
            {
                return size;
            }
            double x = 0.0;
            for (int j = 0; j < base.InternalChildren.Count; j++)
            {
                if (!base.InternalChildren[j].DesiredSize.IsEmpty)
                {
                    desiredSize = base.InternalChildren[j].DesiredSize;
                    double num6 = (desiredSize.Width / size.Width) * finalSize.Width;
                    base.InternalChildren[j].Arrange(new Rect(new Point(x, 0.0), new Size(num6, finalSize.Height)));
                    x += num6;
                }
            }
            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            double width = 0.0;
            double num2 = 0.0;
            foreach (UIElement element in base.InternalChildren)
            {
                element.Measure(availableSize);
                width += element.DesiredSize.Width;
                Size desiredSize = element.DesiredSize;
                num2 = Math.Max(desiredSize.Height, num2);
            }
            return new Size(width, num2);
        }
    }
}

