namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class BothPlacementLayoutStrategy : ToggleSwitchLayoutStrategyBase
    {
        public override Size Arrange(Size finalSize, IFrameworkRenderElementContext context)
        {
            Size size1;
            Size size3;
            FrameworkRenderElementContext context2 = base.FindSwitch(context);
            Size desiredSize = context2.DesiredSize;
            Rect finalRect = new Rect(new Point(Math.Max((double) 0.0, (double) ((finalSize.Width - context2.DesiredSize.Width) / 2.0)), Math.Max((double) 0.0, (double) ((finalSize.Height - desiredSize.Height) / 2.0))), context2.DesiredSize);
            context2.Arrange(finalRect);
            FrameworkRenderElementContext context3 = base.FindElement(context, "PART_UncheckedStateContent");
            if (base.HasUncheckedContent())
            {
                size1 = new Size(finalRect.Left, finalSize.Height);
            }
            else
            {
                desiredSize = new Size();
                size1 = desiredSize;
            }
            Size size = size1;
            context3.Arrange(new Rect(new Point(0.0, 0.0), size));
            FrameworkRenderElementContext context4 = base.FindElement(context, "PART_CheckedStateContent");
            if (base.HasCheckedContent())
            {
                size3 = new Size(Math.Max((double) 0.0, (double) (finalSize.Width - finalRect.Right)), finalSize.Height);
            }
            else
            {
                desiredSize = new Size();
                size3 = desiredSize;
            }
            size = size3;
            context4.Arrange(new Rect(new Point(finalRect.Right, 0.0), size));
            return finalSize;
        }

        public override unsafe Size Measure(Size availableSize, IFrameworkRenderElementContext context)
        {
            Size size = new Size();
            double num = 0.0;
            for (int i = 0; i < context.RenderChildrenCount; i++)
            {
                FrameworkRenderElementContext renderChild = context.GetRenderChild(i);
                renderChild.Measure(availableSize);
                if (renderChild.Name != "PART_SwitchPanel")
                {
                    num = Math.Max(num, renderChild.DesiredSize.Width);
                }
                else
                {
                    Size* sizePtr1 = &size;
                    sizePtr1.Width += renderChild.DesiredSize.Width;
                }
                size.Height = Math.Max(size.Height, renderChild.DesiredSize.Height);
            }
            Size* sizePtr2 = &size;
            sizePtr2.Width += num * 2.0;
            return size;
        }
    }
}

