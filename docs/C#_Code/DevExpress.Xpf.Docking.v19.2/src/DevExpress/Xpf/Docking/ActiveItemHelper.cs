namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Linq;

    public class ActiveItemHelper : IDisposable
    {
        private IActiveItemOwner Owner;
        private bool isDisposing;

        public ActiveItemHelper(IActiveItemOwner owner)
        {
            this.Owner = owner;
        }

        public void ActivateItem(BaseLayoutItem item, bool focus)
        {
            if (item is LayoutGroup)
            {
                LayoutGroup group = (LayoutGroup) item;
                item = ((this.Owner.ActiveItem == null) || !group.GetNestedItems().Contains<BaseLayoutItem>(this.Owner.ActiveItem)) ? LayoutItemsHelper.GetFirstItemInGroup(group) : this.Owner.ActiveItem;
            }
            this.Owner.ActiveItem = item;
            if (PopupMenuManager.TopPopup != null)
            {
                focus = false;
            }
            this.SelectInGroup(item, item.Parent);
            if (focus)
            {
                this.Owner.Container.FocusItem(item, false);
            }
        }

        public void Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                this.Owner = null;
            }
            GC.SuppressFinalize(this);
        }

        internal void RestoreKeyboardFocus(BaseLayoutItem item)
        {
            KeyboardFocusHelper.RestoreKeyboardFocus(item);
        }

        public void SelectInGroup(BaseLayoutItem item)
        {
            if (item.IsActive)
            {
                if (item is LayoutGroup)
                {
                    item = LayoutItemsHelper.GetFirstItemInGroup((LayoutGroup) item);
                }
                if (item != null)
                {
                    this.SelectInGroup(item, item.Parent);
                }
            }
        }

        public void SelectInGroup(BaseLayoutItem item, LayoutGroup group)
        {
            if (group != null)
            {
                LayoutGroup parent = group.Parent;
                BaseLayoutItem[] itemArray1 = new BaseLayoutItem[1];
                DockLayoutManager container = this.Owner?.Container;
                if (parent == null)
                {
                    DockLayoutManager local1 = this.Owner?.Container;
                    container = (DockLayoutManager) group;
                }
                new BaseLayoutItem[1][new BaseLayoutItem[1]] = (BaseLayoutItem) container;
                using (new LogicalTreeLocker((DockLayoutManager) parent, 0))
                {
                    this.TrySelectTabPage(item, group);
                    this.TrySelectAutoHidePage(item, group);
                }
            }
        }

        private void TrySelectAutoHidePage(BaseLayoutItem item, LayoutGroup group)
        {
            AutoHideGroup group2 = group as AutoHideGroup;
            if (group2 != null)
            {
                int index = group2.Items.IndexOf(item);
                if (index != -1)
                {
                    AutoHideTrayHeadersGroup visualChild = LayoutItemsHelper.GetVisualChild<AutoHideTrayHeadersGroup>(group);
                    AutoHideTray tray = visualChild?.Tray;
                    using (tray?.LockExpanding())
                    {
                        if (group2.SelectedTabIndex != index)
                        {
                            group2.SelectedTabIndex = index;
                        }
                        if ((tray != null) && (!tray.IsExpanded || !ReferenceEquals(tray.HotItem, item)))
                        {
                            tray.DoExpand(item);
                        }
                    }
                }
            }
        }

        private void TrySelectTabPage(BaseLayoutItem item, LayoutGroup group)
        {
            int index = group.Items.IndexOf(item);
            if (group.IsTabHost && ((index != -1) && (group.SelectedTabIndex != index)))
            {
                group.SelectedTabIndex = index;
            }
            while (group != null)
            {
                if (group.GroupBorderStyle == GroupBorderStyle.Tabbed)
                {
                    group.SelectedTabIndex = group.Items.IndexOf(item);
                }
                item = group;
                group = group.Parent;
            }
        }
    }
}

