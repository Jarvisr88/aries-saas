namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Input;

    internal class BoundsHelper
    {
        internal Rect startRect;
        internal Point startPoint;
        private Rect effectiveRect;
        private Thickness margin;
        private Size MinSize;
        private Size MaxSize;
        private bool rightToLeft;
        private SizingAction sizingAction;

        public unsafe BoundsHelper(IView view, ILayoutElement element, Size minSize)
        {
            this.MinSize = minSize;
            this.rightToLeft = ((LayoutView) view).Container.FlowDirection == FlowDirection.RightToLeft;
            this.startRect = ElementHelper.GetScreenRect(view, element);
            this.startPoint = view.Adapter.DragService.DragOrigin;
            this.effectiveRect = GetEffectiveRect(element, this.startRect);
            this.margin = GetMargin(this.effectiveRect, this.startRect);
            Size* sizePtr1 = &this.MinSize;
            sizePtr1.Width -= this.margin.Left + this.margin.Right;
            Size* sizePtr2 = &this.MinSize;
            sizePtr2.Height -= this.margin.Top + this.margin.Bottom;
        }

        public unsafe BoundsHelper(IView view, ILayoutElement element, Size minSize, Size maxSize) : this(view, element, minSize)
        {
            this.MaxSize = maxSize;
            Size* sizePtr1 = &this.MaxSize;
            sizePtr1.Width -= this.margin.Left + this.margin.Right;
            Size* sizePtr2 = &this.MaxSize;
            sizePtr2.Height -= this.margin.Top + this.margin.Bottom;
        }

        public BoundsHelper(IView view, ILayoutElement element, Size minSize, Size maxSize, SizingAction sizingAction) : this(view, element, minSize, maxSize)
        {
            this.sizingAction = sizingAction;
        }

        public Rect CalcBounds(Point screenPoint) => 
            GetRealRect(ResizeHelper.CalcResizing(this.effectiveRect, this.startPoint, screenPoint, this.MinSize, this.MaxSize, this.sizingAction), this.margin);

        public Cursor GetCursor() => 
            (this.sizingAction != SizingAction.None) ? this.sizingAction.ToCursor() : ResizeHelper.GetResizeCursor(this.effectiveRect, this.startPoint, this.rightToLeft);

        private static Rect GetEffectiveRect(ILayoutElement element, Rect real)
        {
            if (element is FloatPanePresenterElement)
            {
                RectHelper.Inflate(ref real, (double) -5.0, -5.0);
            }
            return real;
        }

        private static Thickness GetMargin(Rect effective, Rect real) => 
            new Thickness(effective.Left - real.Left, effective.Top - real.Top, real.Right - effective.Right, real.Bottom - effective.Bottom);

        private static Rect GetRealRect(Rect effective, Thickness margin) => 
            new Rect(effective.Left - margin.Left, effective.Top - margin.Top, effective.Width + (margin.Left + margin.Right), effective.Height + (margin.Top + margin.Bottom));

        public SizingAction GetSizingAction() => 
            ResizeHelper.GetSizingAction(this.effectiveRect, this.startPoint, this.rightToLeft);
    }
}

