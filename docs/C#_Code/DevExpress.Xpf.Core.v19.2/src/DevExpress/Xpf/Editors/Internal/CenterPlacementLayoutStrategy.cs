namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class CenterPlacementLayoutStrategy : ToggleSwitchLayoutStrategyBase
    {
        public override Size Arrange(Size finalSize, IFrameworkRenderElementContext context)
        {
            FrameworkRenderElementContext context2 = base.FindSwitch(context);
            if (context2 != null)
            {
                Size size = this.ShouldStrechToggleSwitch() ? finalSize : context2.DesiredSize;
                context2.Arrange(new Rect(new Point(0.0, 0.0), size));
                double thumbWidth = this.GetThumbWidth(context2);
                double num2 = Math.Max((double) 0.0, (double) (size.Width - thumbWidth));
                if (base.IsIndeterminateState)
                {
                    num2 /= 2.0;
                }
                for (int i = 0; i < context.RenderChildrenCount; i++)
                {
                    FrameworkRenderElementContext renderChild = context.GetRenderChild(i);
                    if (!ReferenceEquals(renderChild, context2))
                    {
                        Size size2 = new Size(Math.Min(renderChild.DesiredSize.Width, num2), renderChild.DesiredSize.Height);
                        double y = Math.Max((double) 0.0, (double) ((size.Height - size2.Height) / 2.0));
                        double x = 0.0;
                        if (base.IsIndeterminateState)
                        {
                            if (base.IsCheckedContent(renderChild))
                            {
                                x = (size.Width / 2.0) + (thumbWidth / 2.0);
                            }
                        }
                        else if (base.IsUncheckedContent(renderChild))
                        {
                            x = thumbWidth;
                        }
                        renderChild.Arrange(new Rect(x, y, num2, size2.Height));
                    }
                }
            }
            return finalSize;
        }

        private double GetThumbWidth(IFrameworkRenderElementContext Switch)
        {
            for (int i = 0; i < Switch.RenderChildrenCount; i++)
            {
                FrameworkRenderElementContext renderChild = Switch.GetRenderChild(i);
                if (renderChild is RenderToggleSwitchThumbContainerContext)
                {
                    return ((RenderToggleSwitchThumbContainerContext) renderChild).ThumbWidth;
                }
            }
            return 0.0;
        }

        public override Size Measure(Size availableSize, IFrameworkRenderElementContext context)
        {
            FrameworkRenderElementContext context2 = base.FindSwitch(context);
            if (context2 == null)
            {
                return new Size();
            }
            context2.Measure(availableSize);
            double width = Math.Max((double) 0.0, (double) (((!this.ShouldStrechToggleSwitch() || double.IsInfinity(availableSize.Width)) ? context2.DesiredSize.Width : availableSize.Width) - this.GetThumbWidth(context2)));
            if (base.IsIndeterminateState)
            {
                width /= 2.0;
            }
            for (int i = 0; i < context.RenderChildrenCount; i++)
            {
                FrameworkRenderElementContext renderChild = context.GetRenderChild(i);
                if (!ReferenceEquals(renderChild, context2))
                {
                    renderChild.Measure(new Size(width, availableSize.Height));
                }
            }
            return (this.ShouldStrechToggleSwitch() ? new Size(!double.IsInfinity(availableSize.Width) ? availableSize.Width : context2.DesiredSize.Width, !double.IsInfinity(availableSize.Height) ? availableSize.Height : context2.DesiredSize.Height) : context2.DesiredSize);
        }

        private bool ShouldStrechToggleSwitch() => 
            double.IsNaN(base.Owner.ToggleSwitchWidth) && (base.Owner.HorizontalAlignment == HorizontalAlignment.Stretch);
    }
}

