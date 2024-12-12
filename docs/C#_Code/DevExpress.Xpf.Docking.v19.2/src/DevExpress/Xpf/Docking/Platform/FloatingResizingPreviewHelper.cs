namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    internal class FloatingResizingPreviewHelper : BaseResizingPreviewHelper
    {
        public FloatingResizingPreviewHelper(LayoutView view) : base(view)
        {
        }

        protected override Rect GetAdornerBounds(Point change)
        {
            Rect rect = base.BoundsHelper.CalcBounds(base.View.ClientToScreen(change));
            RectHelper.SetLocation(ref rect, base.View.ScreenToClient(rect.TopLeft()));
            return rect;
        }

        protected override Rect GetInitialAdornerBounds()
        {
            Rect rect = base.BoundsHelper.CalcBounds(base.StartPoint);
            RectHelper.SetLocation(ref rect, base.View.ScreenToClient(rect.TopLeft()));
            return rect;
        }

        protected override Rect GetInitialWindowBounds()
        {
            Rect rect = base.BoundsHelper.CalcBounds(base.StartPoint);
            return base.CorrectBounds(rect);
        }

        protected override UIElement GetUIElement() => 
            new FloatingResizePointer();

        protected override Rect GetWindowBounds(Point change)
        {
            Point screenPoint = base.View.ClientToScreen(change);
            return base.CorrectBounds(base.BoundsHelper.CalcBounds(screenPoint));
        }
    }
}

