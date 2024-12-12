namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Dragging;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LayoutViewRegularDragListener : RegularListener
    {
        private Point? lastDraggingPoint;
        private Point? lastCanDragPoint;

        public override bool CanDrag(Point point, ILayoutElement element)
        {
            this.lastCanDragPoint = new Point?(point);
            return base.CanDrag(point, element);
        }

        public override bool CanDrop(Point point, ILayoutElement element) => 
            (this.View.Adapter.DragService.OperationType != DevExpress.Xpf.Layout.Core.OperationType.ClientDragging) && new DockingHelper(this.View).CanDrop(point, element);

        private bool CanShowDockHints(ILayoutElement element) => 
            (this.View.Adapter.DragService.OperationType != DevExpress.Xpf.Layout.Core.OperationType.ClientDragging) || IsLayoutPanelWithRestrictedFloating(element);

        private static bool IsLayoutPanelWithRestrictedFloating(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            return (((item is LayoutPanel) || ((item is TabbedGroup) || (item is FloatGroup))) ? (!item.AllowFloat || !item.Manager.ShowContentWhenDragging) : false);
        }

        public override void OnCancel()
        {
            this.View.AdornerHelper.ResetDockingHints();
            this.View.AdornerHelper.TryHideAdornerWindow();
        }

        public override void OnDragging(Point point, ILayoutElement element)
        {
            if (!KeyHelper.IsCtrlPressed && this.CanShowDockHints(element))
            {
                DockLayoutElementDragInfo dragInfo = new DockLayoutElementDragInfo(this.View, point, element);
                this.View.AdornerHelper.UpdateDockingHints(dragInfo, false);
            }
            this.lastDraggingPoint = new Point?(point);
        }

        public override void OnDrop(Point point, ILayoutElement element)
        {
            new DockingHelper(this.View).Drop(point, element);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            if (!KeyHelper.IsCtrlPressed)
            {
                ILayoutElement element = this.View.Adapter.DragService.DragItem ?? this.View.Container.LinkedDragService.Return<IDragService, ILayoutElement>((<>c.<>9__2_0 ??= x => x.DragItem), (<>c.<>9__2_1 ??= ((Func<ILayoutElement>) (() => null))));
                Point? lastDraggingPoint = this.lastDraggingPoint;
                Point? nullable = (lastDraggingPoint != null) ? lastDraggingPoint : this.lastCanDragPoint;
                if (this.CanShowDockHints(element) && (nullable != null))
                {
                    DockLayoutElementDragInfo dragInfo = new DockLayoutElementDragInfo(this.View, nullable.Value, element);
                    this.View.AdornerHelper.UpdateDockingHints(dragInfo, true);
                }
            }
        }

        public override void OnLeave()
        {
            this.View.AdornerHelper.BeginHideAdornerWindowAndResetDockingHints();
            this.View.AdornerHelper.BeginHideAdornerWindowAndResetTabHeadersHints();
        }

        public LayoutView View =>
            base.ServiceProvider as LayoutView;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutViewRegularDragListener.<>c <>9 = new LayoutViewRegularDragListener.<>c();
            public static Func<IDragService, ILayoutElement> <>9__2_0;
            public static Func<ILayoutElement> <>9__2_1;

            internal ILayoutElement <OnEnter>b__2_0(IDragService x) => 
                x.DragItem;

            internal ILayoutElement <OnEnter>b__2_1() => 
                null;
        }
    }
}

