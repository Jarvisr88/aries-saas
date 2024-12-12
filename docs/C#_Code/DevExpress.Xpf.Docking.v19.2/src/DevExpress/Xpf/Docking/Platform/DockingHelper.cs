namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    internal class DockingHelper
    {
        public DockingHelper(LayoutView view)
        {
            this.View = view;
        }

        public bool CanDrop(Point point, ILayoutElement element)
        {
            DockLayoutElementDragInfo info = new DockLayoutElementDragInfo(this.View, point, element);
            DockingHintAdornerBase dockingHintAdorner = this.View.AdornerHelper.GetDockingHintAdorner();
            if ((dockingHintAdorner == null) || (info.Item == null))
            {
                return false;
            }
            DockHintHitInfo hitInfo = dockingHintAdorner.HitTest(point);
            return (hitInfo.InButton && info.AcceptDocking(hitInfo));
        }

        protected bool DockItemAsTabCore(BaseLayoutItem item, Point pt, BaseLayoutItem target, DockType type) => 
            !this.View.Container.RaiseItemDockingEvent(DockLayoutManager.DockItemEndDockingEvent, item, pt, target, type, false) && this.View.Container.DockController.DockAsDocument(item, target, type);

        protected bool DockItemCore(BaseLayoutItem item, Point pt, BaseLayoutItem target, DockType type) => 
            !this.View.Container.RaiseItemDockingEvent(DockLayoutManager.DockItemEndDockingEvent, item, pt, target, type, false) && ((this.View.Container != null) && this.View.Container.DockController.Dock(item, target, type));

        public bool Drop(Point point, ILayoutElement element)
        {
            bool flag = true;
            DockLayoutElementDragInfo info = new DockLayoutElementDragInfo(this.View, point, element);
            DockHintHitInfo info2 = this.View.AdornerHelper.GetDockingHintAdorner().HitTest(point);
            if (info2.IsCenter)
            {
                if (info2.IsTabButton)
                {
                    this.DockItemAsTabCore(info.Item, info.Point, info.Target, info2.DockType);
                }
                else
                {
                    flag = this.DockItemCore(info.Item, info.Point, info.Target, info2.DockType);
                }
            }
            else
            {
                bool flag2 = (this.View.Type == HostType.AutoHide) && (info.DropTarget is AutoHideTrayElement);
                if (info2.IsHideButton | flag2)
                {
                    this.HideItemCore(info.Item, info.Point, flag2 ? (info.DropTarget as AutoHideTrayElement).Tray.DockType : info2.Dock);
                }
                else
                {
                    flag = this.DockItemCore(info.Item, info.Point, info.Target.GetRoot(), info2.DockType);
                }
            }
            if (!this.View.IsDisposing)
            {
                this.View.AdornerHelper.ResetDockingHints();
                this.View.AdornerHelper.TryHideAdornerWindow();
            }
            return flag;
        }

        protected bool HideItemCore(BaseLayoutItem item, Point pt, Dock dock) => 
            !this.View.Container.RaiseItemDockingEvent(DockLayoutManager.DockItemEndDockingEvent, item, pt, null, dock.ToDockType(), true) && this.View.Container.DockController.Hide(item, dock);

        public LayoutView View { get; private set; }
    }
}

