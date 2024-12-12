namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FlowLayoutMaximizedElementDragAndDropController : DragAndDropController
    {
        public FlowLayoutMaximizedElementDragAndDropController(DevExpress.Xpf.Core.Controller controller, Point startDragPoint) : base(controller, startDragPoint)
        {
        }

        protected override FrameworkElement CreateDragImage()
        {
            MaximizedElementPositionIndicator indicator1 = new MaximizedElementPositionIndicator();
            indicator1.SelectedPosition = this.MaximizedElementPosition;
            indicator1.Style = this.Controller.ILayoutControl.MaximizedElementPositionIndicatorStyle;
            return indicator1;
        }

        public override void DragAndDrop(Point p)
        {
            base.DragAndDrop(p);
            DevExpress.Xpf.LayoutControl.MaximizedElementPosition? maximizedElementPosition = this.GetMaximizedElementPosition(p);
            this.MaximizedElementPosition = (maximizedElementPosition != null) ? maximizedElementPosition.GetValueOrDefault() : this.MaximizedElementPosition;
        }

        public override void EndDragAndDrop(bool accept)
        {
            base.EndDragAndDrop(accept);
            if (!accept)
            {
                this.MaximizedElementPosition = this.OriginalMaximizedElementPosition;
            }
        }

        protected override unsafe Point GetDragImagePosition(Point p)
        {
            Point startDragPoint = base.StartDragPoint;
            if (this.DragImage.ActualWidth == 0.0)
            {
                this.DragImage.UpdateLayout();
            }
            Point* pointPtr1 = &startDragPoint;
            pointPtr1.X -= Math.Round((double) (this.DragImage.ActualWidth / 2.0));
            Point* pointPtr2 = &startDragPoint;
            pointPtr2.Y -= Math.Round((double) (this.DragImage.ActualHeight / 2.0));
            return startDragPoint;
        }

        protected DevExpress.Xpf.LayoutControl.MaximizedElementPosition? GetMaximizedElementPosition(Point p)
        {
            Point point = PointHelper.Subtract(p, base.StartDragPoint);
            double num = this.DragImage.NoChangeAreaSize / 2.0;
            if ((Math.Abs(point.X) > num) || (Math.Abs(point.Y) > num))
            {
                return ((Math.Abs(point.X) <= Math.Abs(point.Y)) ? new DevExpress.Xpf.LayoutControl.MaximizedElementPosition?((point.Y < 0.0) ? DevExpress.Xpf.LayoutControl.MaximizedElementPosition.Top : DevExpress.Xpf.LayoutControl.MaximizedElementPosition.Bottom) : new DevExpress.Xpf.LayoutControl.MaximizedElementPosition?((point.X < 0.0) ? DevExpress.Xpf.LayoutControl.MaximizedElementPosition.Left : DevExpress.Xpf.LayoutControl.MaximizedElementPosition.Right));
            }
            return null;
        }

        public override void StartDragAndDrop(Point p)
        {
            this.OriginalMaximizedElementPosition = this.MaximizedElementPosition;
            base.StartDragAndDrop(p);
        }

        protected FlowLayoutController Controller =>
            (FlowLayoutController) base.Controller;

        protected DevExpress.Xpf.LayoutControl.MaximizedElementPosition MaximizedElementPosition
        {
            get => 
                this.Controller.ILayoutControl.MaximizedElementPosition;
            set
            {
                if (this.MaximizedElementPosition != value)
                {
                    this.Controller.ILayoutControl.MaximizedElementPosition = value;
                    if (this.DragImage != null)
                    {
                        this.DragImage.SelectedPosition = value;
                    }
                }
            }
        }

        protected DevExpress.Xpf.LayoutControl.MaximizedElementPosition OriginalMaximizedElementPosition { get; private set; }

        protected MaximizedElementPositionIndicator DragImage =>
            (MaximizedElementPositionIndicator) base.DragImage;
    }
}

