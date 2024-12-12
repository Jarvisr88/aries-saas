namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Dragging;
    using System;
    using System.Windows;

    public class FloatingViewFloatingMovingListener : FloatingMovingListener
    {
        private LocationHelper helper;

        public override bool CanDrop(Point point, ILayoutElement element) => 
            true;

        public override void OnBegin(Point point, ILayoutElement element)
        {
            Point dragOffset;
            if (this.View.FloatGroup.IsMaximized)
            {
                this.View.FloatGroup.ResetMaximized(new MarginHelper(this.View).Correct(point));
            }
            if (this.View.IsFloating)
            {
                dragOffset = this.View.Container.GetDragOffset();
            }
            else
            {
                dragOffset = new Point();
            }
            this.helper = new LocationHelper(this.View, ElementHelper.GetRoot(element), dragOffset);
            this.View.EndFloating();
        }

        public override void OnCancel()
        {
            this.View.EndFloating();
            base.OnCancel();
        }

        public override void OnDragging(Point point, ILayoutElement element)
        {
            Point screenPoint = this.View.ClientToScreen(point);
            this.View.SetFloatLocation(this.helper.CalcLocation(screenPoint));
            NotificationBatch.Action(this.View.Container, this.View.FloatGroup, FloatGroup.FloatLocationProperty);
        }

        public override void OnDrop(Point point, ILayoutElement element)
        {
            this.RaiseDockOperationCompleted(element);
        }

        private void RaiseDockOperationCompleted(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).CheckDragElement().Item;
            this.View.Container.RaiseDockOperationCompletedEvent(DockOperation.Move, item);
        }

        public FloatingView View =>
            base.ServiceProvider as FloatingView;
    }
}

