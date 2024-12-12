namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class DockControllerExtensions
    {
        internal static bool CloseEx(this IDockController controller, BaseLayoutItem item) => 
            (item != null) ? (item.ExecuteCloseCommand() || controller.Close(item)) : false;

        internal static bool DockAsDocument(this IDockController controller, BaseLayoutItem item, BaseLayoutItem target, DockType type) => 
            (controller is IDockController2010) ? ((IDockController2010) controller).DockAsDocument(item, target, type) : controller.Dock(item, target, type);

        internal static bool DockAsDocument(this IDockController2010 controller, BaseLayoutItem item, BaseLayoutItem target, DockType type) => 
            controller.DockAsDocument(item, target, type);

        public static FloatGroup Float(this IDockController controller, BaseLayoutItem item, Rect floatBounds)
        {
            using (new UpdateBatch(controller.Container))
            {
                item.InitialFloatBounds = new Rect?(floatBounds);
                FloatGroup group = controller.Float(item);
                item.InitialFloatBounds = null;
                return group;
            }
        }

        internal static FloatGroup Float(this IDockController controller, BaseLayoutItem item, Size floatSize) => 
            controller.Float(item, new Rect(floatSize));

        internal static bool Insert(this IDockController controller, LayoutGroup group, BaseLayoutItem item, int index, bool isReordering)
        {
            LayoutPanel target = group.ItemFromIndex(index) as LayoutPanel;
            if (isReordering && ((item.Parent.IndexFromItem(item) == index) || ((index < 0) || !LayoutItemsHelper.CanReorder(item, target))))
            {
                return false;
            }
            DockOperation dockOperation = isReordering ? DockOperation.Reorder : DockOperation.Dock;
            if (controller.Container.RaiseDockOperationStartingEvent(dockOperation, item, group))
            {
                return false;
            }
            if (!isReordering && ((target != null) && target.IsPinnedTab))
            {
                index = (target.TabPinLocation != TabHeaderPinLocation.Far) ? 0 : target.Parent.Items.Count;
            }
            bool flag = controller.Insert(group, item, index);
            if (flag)
            {
                controller.Container.RaiseDockOperationCompletedEvent(dockOperation, item);
            }
            return flag;
        }

        internal static bool ToggleItemPinStatus(this IDockController controller, BaseLayoutItem item)
        {
            bool flag = item.IsTabDocument && item.ToggleTabPinStatus();
            if (flag)
            {
                controller.Container.Update(false);
            }
            return flag;
        }
    }
}

