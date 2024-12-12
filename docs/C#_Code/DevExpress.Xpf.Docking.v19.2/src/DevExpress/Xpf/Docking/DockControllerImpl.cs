namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    internal class DockControllerImpl : DockControllerImplBase
    {
        public DockControllerImpl(DockLayoutManager container) : base(container)
        {
        }

        protected bool BoxAndDockInNewGroup(BaseLayoutItem item, BaseLayoutItem target, DockType type)
        {
            BaseLayoutItem[] items = new BaseLayoutItem[] { item, target };
            using (new LogicalTreeLocker(base.Container, items))
            {
                LayoutGroup parent = target.Parent;
                base.BoxGroupItemsInNewGroup(parent, type.ToOrientation(), base.Container);
                return this.InsertItemInGroup(parent, item, (type.ToInsertType() == InsertType.Before) ? 0 : parent.Items.Count, false);
            }
        }

        internal override FloatGroup BoxToFloatGroupCore(BaseLayoutItem item, Rect floatingBounds)
        {
            using (item.LockCanMerge())
            {
                if ((base.Container.FloatingDocumentContainer == FloatingDocumentContainer.DocumentHost) && (item is DocumentPanel))
                {
                    item = DockControllerHelper.BoxIntoDocumentHost(item, base.Container);
                }
                return base.BoxToFloatGroupCore(item, floatingBounds);
            }
        }

        private bool DockInExistingGroup(BaseLayoutItem item, LayoutGroup targetGroup, int targetPosition, Orientation neededOrientation)
        {
            BaseLayoutItem[] items = new BaseLayoutItem[] { item, targetGroup };
            using (new LogicalTreeLocker(base.Container, items))
            {
                if (item is DocumentPanel)
                {
                    item = DockControllerHelper.BoxIntoDocumentGroup(item, base.Container);
                }
                return ((targetGroup != null) && base.InsertItemInGroup(targetGroup, item, targetPosition, false));
            }
        }

        protected override bool DockToSideCore(BaseLayoutItem itemToDock, BaseLayoutItem target, DockType type)
        {
            bool flag;
            LayoutGroup parent = target as LayoutGroup;
            if ((parent != null) && !(target is TabbedGroup))
            {
                return base.DockInExistingGroup(itemToDock, parent, type);
            }
            parent = target.Parent;
            Orientation neededOrientation = type.ToOrientation();
            if (parent.GetIsDocumentHost() && (itemToDock.ItemType != LayoutItemType.DocumentPanelGroup))
            {
                flag = this.BoxAndDockInNewGroup(itemToDock, target, type);
            }
            else if (((!parent.IgnoreOrientation && (parent.Orientation != neededOrientation)) || target.IsFloatingRootItem) || parent.IsControlItemsHost)
            {
                flag = base.DockInNewGroup(itemToDock, target, type);
            }
            else
            {
                int index = parent.Items.IndexOf(target);
                int targetPosition = (type.ToInsertType() == InsertType.Before) ? index : (index + 1);
                if (ReferenceEquals(itemToDock.Parent, parent) && (itemToDock.Parent.Items.IndexOf(itemToDock) < targetPosition))
                {
                    targetPosition--;
                }
                flag = this.DockInExistingGroup(itemToDock, parent, targetPosition, neededOrientation);
            }
            return flag;
        }
    }
}

