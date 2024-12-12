namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class EditorRenderCheckBoxLayoutProvider : LayoutProvider
    {
        public override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context)
        {
            double x = 0.0;
            double y = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            int renderChildrenCount = context.RenderChildrenCount;
            int num6 = renderChildrenCount - (this.LastChildFill ? 1 : 0);
            int num7 = 0;
            for (int i = 0; i < renderChildrenCount; i++)
            {
                FrameworkRenderElementContext renderChild = context.GetRenderChild(i);
                Rect finalRect = new Rect(x, y, Math.Max((double) 0.0, (double) ((finalSize.Width - x) - num3)), Math.Max((double) 0.0, (double) ((finalSize.Height - y) - num4)));
                if (num7 < num6)
                {
                    Size desiredSize = renderChild.DesiredSize;
                    switch (this.GetDock(renderChild))
                    {
                        case Dock.Left:
                            x += desiredSize.Width;
                            finalRect.Width = desiredSize.Width;
                            break;

                        case Dock.Top:
                            y += desiredSize.Height;
                            finalRect.Height = desiredSize.Height;
                            break;

                        case Dock.Right:
                            num3 += desiredSize.Width;
                            finalRect.X = Math.Max((double) 0.0, (double) (finalSize.Width - num3));
                            finalRect.Width = desiredSize.Width;
                            break;

                        case Dock.Bottom:
                            num4 += desiredSize.Height;
                            finalRect.Y = Math.Max((double) 0.0, (double) (finalSize.Height - num4));
                            finalRect.Height = desiredSize.Height;
                            break;

                        default:
                            break;
                    }
                }
                renderChild.Arrange(finalRect);
                num7++;
            }
            return finalSize;
        }

        private Dock GetDock(FrameworkRenderElementContext element)
        {
            Dock? dock = element.Dock;
            dock = (dock != null) ? dock : element.Factory.Dock;
            return ((dock != null) ? dock.GetValueOrDefault() : Dock.Left);
        }

        public override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context)
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            for (int i = 0; i < context.RenderChildrenCount; i++)
            {
                FrameworkRenderElementContext renderChild = context.GetRenderChild(i);
                Size size = new Size(Math.Max((double) 0.0, (double) (availableSize.Width - num)), Math.Max((double) 0.0, (double) (availableSize.Height - num2)));
                renderChild.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                Size desiredSize = renderChild.DesiredSize;
                Dock dock = this.GetDock(renderChild);
                switch (dock)
                {
                    case Dock.Left:
                    case Dock.Right:
                        num4 = Math.Max(num4, num2 + desiredSize.Height);
                        num += desiredSize.Width;
                        break;

                    case Dock.Top:
                    case Dock.Bottom:
                        num3 = Math.Max(num3, num + desiredSize.Width);
                        num2 += desiredSize.Height;
                        break;

                    default:
                        break;
                }
            }
            return new Size(Math.Max(num3, num), Math.Max(num4, num2));
        }

        public bool LastChildFill { get; set; }
    }
}

