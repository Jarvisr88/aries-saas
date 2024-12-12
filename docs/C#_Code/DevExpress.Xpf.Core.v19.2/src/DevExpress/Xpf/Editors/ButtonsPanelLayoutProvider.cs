namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class ButtonsPanelLayoutProvider : LayoutProvider
    {
        public static readonly LayoutProvider ButtonsPanelInstance = new ButtonsPanelLayoutProvider();

        public override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context)
        {
            Size desiredSize;
            double width = 0.0;
            for (int i = 0; i < context.RenderChildrenCount; i++)
            {
                desiredSize = context.GetRenderChild(i).DesiredSize;
                width += desiredSize.Width;
            }
            Size size = new Size(width, finalSize.Height);
            if (width.AreClose(0.0))
            {
                return size;
            }
            double x = 0.0;
            for (int j = 0; j < context.RenderChildrenCount; j++)
            {
                FrameworkRenderElementContext renderChild = context.GetRenderChild(j);
                if (!renderChild.DesiredSize.IsEmpty)
                {
                    desiredSize = renderChild.DesiredSize;
                    double num6 = (desiredSize.Width / size.Width) * finalSize.Width;
                    renderChild.Arrange(new Rect(new Point(x, 0.0), new Size(num6, finalSize.Height)));
                    x += num6;
                }
            }
            return finalSize;
        }

        public override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context)
        {
            double width = 0.0;
            double num2 = 0.0;
            for (int i = 0; i < context.RenderChildrenCount; i++)
            {
                FrameworkRenderElementContext renderChild = context.GetRenderChild(i);
                renderChild.Measure(availableSize);
                width += renderChild.DesiredSize.Width;
                Size desiredSize = renderChild.DesiredSize;
                num2 = Math.Max(desiredSize.Height, num2);
            }
            return new Size(width, num2);
        }
    }
}

