namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Customization;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Dragging;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LayoutViewClientDraggingListener : ClientDraggingListener
    {
        public override bool CanDrag(Point point, ILayoutElement element)
        {
            bool flag = base.CanDrag(point, element);
            return ((((IDockLayoutElement) element).Item is FloatGroup) | flag);
        }

        public override bool CanDrop(Point point, ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            return (this.View.Container.ShowContentWhenDragging ? (!item.IsItemWithRestrictedFloating() ? new MovingHelper(this.View).CanMove(point, element) : new DockingHelper(this.View).CanDrop(point, element)) : (item.AllowFloat || item.AllowMove));
        }

        protected override IFloatingHelper CreateFloatingHelper() => 
            (this.View.Adapter.DragService.DragItem is AutoHidePaneHeaderItemElement) ? new AutoHideFloatingHelper(this.View) : new FloatingHelper(this.View);

        private void InitFloating(ILayoutElement element, Point point)
        {
            FloatingView floatingView = this.GetFloatingView(element) as FloatingView;
            if (floatingView != null)
            {
                Point point2 = this.View.ClientToScreen(point);
                floatingView.FloatGroup.FloatLocation = point2;
            }
        }

        public override void OnBegin(Point point, ILayoutElement element)
        {
            IDockLayoutElement dockLayoutElement = (IDockLayoutElement) element;
            BaseLayoutItem item = dockLayoutElement.Item;
            if (item.IsItemWithRestrictedFloating())
            {
                item.DragCursorBounds = FloatingHelper.GetDragCursorBounds(this.View, dockLayoutElement);
            }
            Point point2 = this.View.ClientToScreen(point);
            this.View.Container.CustomizationController.ShowDragCursor(point2, item);
            this.View.Adapter.SelectionService.ClearSelection(this.View);
            this.View.AdornerHelper.TryShowAdornerWindow(true);
        }

        public override void OnCancel()
        {
            this.View.Adapter.SelectionService.ClearSelection(this.View);
            this.ResetVisualization();
        }

        public override void OnDragging(Point point, ILayoutElement element)
        {
            if (!((IDockLayoutElement) element).Item.IsItemWithRestrictedFloating())
            {
                this.UpdateDragCursor(element, point);
                DragInfo info = DockLayoutElementDragInfo.CalcDragInfo(this.View, point, element);
                this.View.Container.CustomizationController.UpdateDragInfo(info);
            }
            else
            {
                bool flag1;
                DockHintHitInfo hitInfo = null;
                Point screenPoint = this.View.ClientToScreen(point);
                LayoutView behindView = this.View.Adapter.GetBehindView(this.View, screenPoint) as LayoutView;
                if ((behindView != null) && !ReferenceEquals(behindView, this.View))
                {
                    DockLayoutElementDragInfo info2 = new DockLayoutElementDragInfo(behindView, this.View.Adapter.GetBehindViewPoint(this.View, behindView, screenPoint), element);
                    hitInfo = behindView.AdornerHelper.GetHitInfo(info2.Point);
                }
                else
                {
                    DockLayoutElementDragInfo info3 = new DockLayoutElementDragInfo(this.View, point, element);
                    hitInfo = this.View.AdornerHelper.GetHitInfo(info3.Point);
                }
                if (this.View.Container.ShowContentWhenDragging)
                {
                    flag1 = false;
                }
                else
                {
                    Func<DockHintHitInfo, bool> func1 = <>c.<>9__3_0;
                    if (<>c.<>9__3_0 == null)
                    {
                        Func<DockHintHitInfo, bool> local1 = <>c.<>9__3_0;
                        func1 = <>c.<>9__3_0 = x => x.DockType != DockType.None;
                    }
                    flag1 = (((DockHintHitInfo) func1).Return<DockHintHitInfo, bool>(((Func<DockHintHitInfo, bool>) (<>c.<>9__3_1 ??= () => false)), (<>c.<>9__3_1 ??= () => false)) || this.View.AdornerHelper.IsTabHeadersHintEnabled) || ((behindView != null) ? behindView.AdornerHelper.IsTabHeadersHintEnabled : false);
                }
                if (flag1)
                {
                    this.View.Container.CustomizationController.HideDragCursor();
                }
                else
                {
                    this.UpdateDragCursor(element, point);
                }
            }
        }

        public override unsafe void OnDrop(Point point, ILayoutElement element)
        {
            this.ResetVisualization();
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            if (!item.IsItemWithRestrictedFloating())
            {
                new MovingHelper(this.View).Move(point, element);
            }
            else
            {
                if (((IDockLayoutElement) element).CheckDragElement().Item.AllowFloat)
                {
                    this.RaiseDockOperationCompleted(element);
                }
                Vector vector = new Vector(item.DragCursorBounds.Location.X, item.DragCursorBounds.Location.Y);
                Point screenPoint = this.View.ClientToScreen(point);
                LayoutView behindView = this.View.Adapter.GetBehindView(this.View, screenPoint) as LayoutView;
                FloatGroup root = item.GetRoot() as FloatGroup;
                bool flag = item.IsFloatingRootItem || (item is FloatGroup);
                if (flag && root.IsMaximized)
                {
                    root.ResetMaximized(screenPoint - vector);
                }
                bool flag2 = false;
                if (behindView != null)
                {
                    Point point4 = this.View.Adapter.GetBehindViewPoint(this.View, behindView, screenPoint);
                    flag2 = new DockingHelper(behindView).CanDrop(point4, element);
                    if (flag2)
                    {
                        flag2 = new DockingHelper(behindView).Drop(point4, element);
                    }
                }
                if (!this.View.IsDisposing && (!flag2 && !(this.View.AdornerHelper.IsTabHeadersHintEnabled || ((behindView != null) ? behindView.AdornerHelper.IsTabHeadersHintEnabled : false))))
                {
                    Thickness borderMargin = item.GetBorderMargin();
                    Vector* vectorPtr1 = &vector;
                    vectorPtr1.X += borderMargin.Left;
                    Vector* vectorPtr2 = &vector;
                    vectorPtr2.Y += borderMargin.Top;
                    if (flag)
                    {
                        root.FloatLocation = screenPoint - vector;
                    }
                    else
                    {
                        this.InitFloating(element, point - vector);
                    }
                }
            }
        }

        public override void OnEnter()
        {
            this.View.AdornerHelper.TryShowAdornerWindow(true);
        }

        private void RaiseDockOperationCompleted(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).CheckDragElement().Item;
            this.View.Container.RaiseDockOperationCompletedEvent(DockOperation.Move, item);
        }

        protected void ResetVisualization()
        {
            this.View.Container.CustomizationController.HideDragCursor();
            this.View.Container.CustomizationController.UpdateDragInfo(null);
        }

        protected void UpdateDragCursor(ILayoutElement element, Point point)
        {
            ICustomizationController customizationController = this.View.Container.CustomizationController;
            Point point2 = this.View.ClientToScreen(point);
            if (!customizationController.IsDragCursorVisible)
            {
                customizationController.ShowDragCursor(point2, ((IDockLayoutElement) element).Item);
            }
            else
            {
                customizationController.SetDragCursorPosition(point2);
            }
        }

        public LayoutView View =>
            base.ServiceProvider as LayoutView;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutViewClientDraggingListener.<>c <>9 = new LayoutViewClientDraggingListener.<>c();
            public static Func<DockHintHitInfo, bool> <>9__3_0;
            public static Func<bool> <>9__3_1;

            internal bool <OnDragging>b__3_0(DockHintHitInfo x) => 
                x.DockType != DockType.None;

            internal bool <OnDragging>b__3_1() => 
                false;
        }
    }
}

