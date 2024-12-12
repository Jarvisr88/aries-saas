namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class DockCommand : DockControllerCommand
    {
        protected bool CanDock(DockLayoutManager container, BaseLayoutItem item)
        {
            DockSituation lastDockSituation = item.GetLastDockSituation();
            DockType dockTypeInContainer = DockControllerHelper.GetDockTypeInContainer(container, item);
            BaseLayoutItem layoutRoot = container.LayoutRoot;
            if ((lastDockSituation != null) && (lastDockSituation.DockTarget != null))
            {
                LayoutGroup root = lastDockSituation.DockTarget.GetRoot();
                bool flag = ReferenceEquals(lastDockSituation.Root, root) && container.IsViewCreated(root);
                if (!ReferenceEquals(root, item.GetRoot()) & flag)
                {
                    dockTypeInContainer = lastDockSituation.Type;
                    layoutRoot = lastDockSituation.DockTarget;
                }
            }
            return !container.RaiseItemDockingEvent(DockLayoutManager.DockItemStartDockingEvent, item, CoordinateHelper.ZeroPoint, layoutRoot, dockTypeInContainer, false);
        }

        protected override bool CanExecuteCore(BaseLayoutItem item) => 
            item.IsFloating || (item.IsAutoHidden && item.AllowDock);

        protected override void ExecuteCore(IDockController controller, BaseLayoutItem item)
        {
            if (this.CanDock(controller.Container, item))
            {
                controller.Dock(item);
            }
        }
    }
}

