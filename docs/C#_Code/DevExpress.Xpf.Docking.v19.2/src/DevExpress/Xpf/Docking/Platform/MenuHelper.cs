namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    internal class MenuHelper
    {
        private readonly DockLayoutManager _manager;

        private MenuHelper(DockLayoutManager manager)
        {
            this._manager = manager;
        }

        private void ShowItemContextMenu(BaseLayoutItem item)
        {
            this._manager.CustomizationController.ShowContextMenu(item);
        }

        private void ShowItemsSelectorMenu(UIElement source, BaseLayoutItem[] items)
        {
            this._manager.CustomizationController.ShowItemSelectorMenu(source, items);
        }

        private void ShowLayoutControlItemContextMenu(BaseLayoutItem item)
        {
            this._manager.CustomizationController.ShowControlItemContextMenu(item);
        }

        public bool ShowMenu(BaseLayoutItem item, UIElement placementTarget)
        {
            bool flag;
            try
            {
                this._manager.CustomizationController.MenuSource = placementTarget;
                if ((item is LayoutControlItem) || (item is FixedItem))
                {
                    this.ShowLayoutControlItemContextMenu(item);
                    flag = true;
                }
                else
                {
                    LayoutGroup group = item as LayoutGroup;
                    if (group != null)
                    {
                        this.ShowItemsSelectorMenu(group.GetUIElement<IUIElement>() as UIElement, group.GetItems());
                    }
                    else
                    {
                        this.ShowItemContextMenu(item);
                    }
                    return true;
                }
            }
            finally
            {
                this._manager.CustomizationController.MenuSource = null;
            }
            return flag;
        }

        public static bool ShowMenu(DockLayoutManager manager, BaseLayoutItem item, UIElement placementTarget = null, bool forceShow = false) => 
            (manager != null) && (!manager.IsInDesignTime && ((item != null) && ((item.AllowContextMenu || forceShow) && new MenuHelper(manager).ShowMenu(item, placementTarget))));
    }
}

