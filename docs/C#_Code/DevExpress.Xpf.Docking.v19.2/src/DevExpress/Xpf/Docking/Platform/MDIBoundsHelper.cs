namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Input;

    internal class MDIBoundsHelper
    {
        private Rect startRect;
        private Point startPoint;
        private Size MinSize;
        private Size MaxSize;
        private Point startLocation;
        private bool rightToLeft;

        public MDIBoundsHelper(IView view, IDockLayoutElement element, Size minSize)
        {
            this.MinSize = minSize;
            this.rightToLeft = ((LayoutView) view).Container.FlowDirection == FlowDirection.RightToLeft;
            this.startLocation = DocumentPanel.GetMDILocation(element.Item);
            this.startRect = ElementHelper.GetScreenRect(view, element);
            this.startPoint = view.Adapter.DragService.DragOrigin;
        }

        public MDIBoundsHelper(IView view, IDockLayoutElement element, Size minSize, Size maxSize) : this(view, element, minSize)
        {
            this.MaxSize = maxSize;
        }

        public Rect CalcBounds(Point screenPoint)
        {
            Rect rect = ResizeHelper.CalcResizing(this.startRect, this.startPoint, screenPoint, this.MinSize, this.MaxSize);
            double num = rect.X - this.startRect.X;
            double num2 = rect.Y - this.startRect.Y;
            return new Rect(new Point(this.startLocation.X + num, this.startLocation.Y + num2), rect.Size());
        }

        public Cursor GetCursor() => 
            ResizeHelper.GetResizeCursor(this.startRect, this.startPoint, this.rightToLeft);
    }
}

