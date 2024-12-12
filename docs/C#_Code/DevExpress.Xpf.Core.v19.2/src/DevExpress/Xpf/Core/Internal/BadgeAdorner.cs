namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class BadgeAdorner : AdornerContainer
    {
        public BadgeAdorner(UIElement adornedElement, UIElement child) : base(adornedElement, child)
        {
            base.IsHitTestVisible = false;
            base.Focusable = false;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (base.Child != null)
            {
                base.Child.InvalidateArrange();
                base.Child.Arrange(new Rect(new Point(0.0, 0.0), base.AdornedElement.RenderSize));
            }
            return finalSize;
        }

        public void Destroy()
        {
            base.RemoveVisualChild(base.Child);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (base.Child == null)
            {
                return base.MeasureOverride(constraint);
            }
            base.Child.Measure(constraint);
            return base.Child.DesiredSize;
        }
    }
}

