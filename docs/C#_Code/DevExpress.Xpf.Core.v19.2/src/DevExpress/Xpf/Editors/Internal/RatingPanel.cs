namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq;
    using System.Windows;

    public class RatingPanel : DXPanelBase
    {
        static RatingPanel()
        {
            DependencyPropertyRegistrator<RatingPanel>.New().OverrideMetadata(UIElement.ClipToBoundsProperty, true, null, FrameworkPropertyMetadataOptions.None);
        }

        protected override Size ArrangeOverrideCore(Rect avRect)
        {
            double x = avRect.X;
            int index = 0;
            foreach (FrameworkElement element in base.VisibleChildren)
            {
                Size minSize = base.GetMinSize(element, true);
                double height = Math.Max(avRect.Height, minSize.Height);
                Size size = new Size(this.GetChildWidth(element, index, avRect.Width), height);
                base.ArrangeChild(element, new Rect(new Point(x, avRect.Y), size));
                x += size.Width;
                index++;
            }
            return avRect.Size;
        }

        private double GetChildWidth(FrameworkElement child, int index, double avW)
        {
            Size minSize = base.GetMinSize(child, true);
            double d = avW;
            if (base.VisibleChildren.Count<FrameworkElement>() == 1)
            {
                return d;
            }
            d /= (double) base.VisibleChildren.Count<FrameworkElement>();
            d = Math.Floor(d);
            if (((d - Math.Floor(d)) * base.VisibleChildren.Count<FrameworkElement>()) >= (base.VisibleChildren.ToList<FrameworkElement>().IndexOf(child) + 1))
            {
                d++;
            }
            return Math.Max(d, minSize.Width);
        }

        protected override Size MeasureOverrideCore(Size avSize)
        {
            if (base.VisibleChildren.Count<FrameworkElement>() == 0)
            {
                return new Size();
            }
            double num = 0.0;
            double num2 = 0.0;
            foreach (FrameworkElement element in base.VisibleChildren)
            {
                Size minSize = base.GetMinSize(element, true);
                base.MeasureChild(element, minSize);
                num += minSize.Width;
                num2 = Math.Max(num2, minSize.Height);
            }
            if (!double.IsPositiveInfinity(avSize.Width))
            {
                num = Math.Min(num, avSize.Width);
            }
            if (!double.IsPositiveInfinity(avSize.Height))
            {
                num2 = Math.Min(num2, avSize.Height);
            }
            return new Size(num, num2);
        }
    }
}

