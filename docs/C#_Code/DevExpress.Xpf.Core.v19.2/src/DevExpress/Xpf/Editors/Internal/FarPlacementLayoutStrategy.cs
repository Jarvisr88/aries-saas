namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class FarPlacementLayoutStrategy : ToggleSwitchLayoutStrategyBase
    {
        public override Size Arrange(Size finalSize, IFrameworkRenderElementContext context)
        {
            FrameworkRenderElementContext context2 = base.FindSwitch(context);
            if (context2 != null)
            {
                double width = context2.DesiredSize.Width;
                context2.Arrange(new Rect(new Point(0.0, Math.Max((double) 0.0, (double) ((finalSize.Height - context2.DesiredSize.Height) / 2.0))), context2.DesiredSize));
                double num3 = Math.Max((double) 0.0, (double) (finalSize.Width - context2.DesiredSize.Width));
                for (int i = 0; i < context.RenderChildrenCount; i++)
                {
                    FrameworkRenderElementContext renderChild = context.GetRenderChild(i);
                    if (!base.IsSwitch(renderChild))
                    {
                        Size size = new Size();
                        if ((base.IsUncheckedContent(renderChild) && base.HasUncheckedContent()) || (base.IsCheckedContent(renderChild) && base.HasCheckedContent()))
                        {
                            size = new Size(num3, finalSize.Height);
                        }
                        renderChild.Arrange(new Rect(new Point(width, 0.0), size));
                    }
                }
            }
            return finalSize;
        }

        public override Size Measure(Size availableSize, IFrameworkRenderElementContext context) => 
            base.MeasureBase(availableSize, context);
    }
}

