namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class HideCommand : DockControllerCommand
    {
        protected override bool CanExecuteCore(BaseLayoutItem item) => 
            !item.IsAutoHidden && item.AllowHide;

        protected override void ExecuteCore(IDockController controller, BaseLayoutItem item)
        {
            if (!item.IsFloating || !controller.Container.RaiseItemDockingEvent(DockLayoutManager.DockItemStartDockingEvent, item, CoordinateHelper.ZeroPoint, null, DockType.None, true))
            {
                controller.Hide(item);
            }
        }
    }
}

