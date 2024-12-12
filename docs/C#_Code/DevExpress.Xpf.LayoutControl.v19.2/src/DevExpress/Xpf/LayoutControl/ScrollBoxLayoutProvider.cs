namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class ScrollBoxLayoutProvider : LayoutProviderBase
    {
        public ScrollBoxLayoutProvider(ILayoutControlBase control) : base(control)
        {
        }

        protected override Size OnArrange(FrameworkElements items, Rect bounds, Rect viewPortBounds)
        {
            foreach (FrameworkElement element in items)
            {
                Rect finalRect = RectHelper.New(element.DesiredSize);
                finalRect.X = bounds.Left + ScrollBox.GetLeft(element);
                finalRect.Y = bounds.Top + ScrollBox.GetTop(element);
                element.Arrange(finalRect);
            }
            return base.OnArrange(items, bounds, viewPortBounds);
        }

        protected override unsafe Size OnMeasure(FrameworkElements items, Size maxSize)
        {
            maxSize = SizeHelper.Infinite;
            Size size = new Size(0.0, 0.0);
            foreach (FrameworkElement element in items)
            {
                element.Measure(maxSize);
                Size desiredSize = element.DesiredSize;
                Size* sizePtr1 = &desiredSize;
                sizePtr1.Width += ScrollBox.GetLeft(element);
                Size* sizePtr2 = &desiredSize;
                sizePtr2.Height += ScrollBox.GetTop(element);
                SizeHelper.UpdateMaxSize(ref size, UIElementExtensions.GetRoundedSize(desiredSize));
            }
            return size;
        }

        public override void UpdateChildBounds(FrameworkElement child, ref Rect bounds)
        {
            base.UpdateChildBounds(child, ref bounds);
            RectHelper.IncLeft(ref bounds, -ScrollBox.GetLeft(child));
            RectHelper.IncTop(ref bounds, -ScrollBox.GetTop(child));
        }
    }
}

