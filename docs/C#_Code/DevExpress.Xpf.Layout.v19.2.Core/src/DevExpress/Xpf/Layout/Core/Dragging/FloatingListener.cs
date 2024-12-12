namespace DevExpress.Xpf.Layout.Core.Dragging
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class FloatingListener : BaseDragListener
    {
        protected override IView GetFloatingView(ILayoutElement element)
        {
            IView floatingView = base.GetFloatingView(element);
            if (floatingView != null)
            {
                floatingView.Adapter.DragService.DragItem = floatingView.LayoutRoot;
            }
            return floatingView;
        }

        public override void OnBegin(Point point, ILayoutElement element)
        {
            IView serviceProvider = (IView) base.ServiceProvider;
            if (serviceProvider.Type == HostType.AutoHide)
            {
                serviceProvider.Adapter.ActionService.Hide(serviceProvider, true);
            }
            this.GetFloatingView(element);
        }

        public override void OnEnter()
        {
            IView serviceProvider = base.ServiceProvider as IView;
            this.GetFloatingView(serviceProvider.Adapter.DragService.DragItem);
        }

        public sealed override DevExpress.Xpf.Layout.Core.OperationType OperationType =>
            DevExpress.Xpf.Layout.Core.OperationType.Floating;
    }
}

