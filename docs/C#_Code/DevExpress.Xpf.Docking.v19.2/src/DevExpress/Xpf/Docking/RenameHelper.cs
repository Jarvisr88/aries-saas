namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;

    public class RenameHelper : IDisposable
    {
        private DockLayoutManager Manager;
        private BaseLayoutItem lastClickedItem;

        public RenameHelper(DockLayoutManager manager)
        {
            this.Manager = manager;
        }

        public bool CancelRenaming() => 
            this.Manager.IsRenaming ? this.TryCancelRenaming() : false;

        public bool CancelRenamingAndResetClickedState()
        {
            this.ResetClickedState();
            return this.CancelRenaming();
        }

        public bool CanRename(BaseLayoutItem item)
        {
            if (this.Manager.IsInDesignTime)
            {
                return false;
            }
            if ((item == null) || !item.AllowRename)
            {
                return false;
            }
            bool? nullable = LayoutItemsHelper.IsDockItem(item) ? this.Manager.AllowDockItemRename : this.Manager.AllowLayoutItemRename;
            return ((nullable != null) ? nullable.Value : this.Manager.IsCustomization);
        }

        public void Dispose()
        {
            this.lastClickedItem = null;
            this.Manager = null;
            GC.SuppressFinalize(this);
        }

        public bool EndRenaming()
        {
            if (!this.Manager.IsRenaming)
            {
                return false;
            }
            this.ResetClickedState();
            return this.TryEndRenaming();
        }

        public bool Rename(BaseLayoutItem item)
        {
            IUIElement uIElement = item.GetUIElement<ITabHeader>() as IUIElement;
            IDockLayoutElement element = null;
            if (uIElement != null)
            {
                element = this.Manager.GetViewElement(uIElement) as IDockLayoutElement;
            }
            element ??= (this.Manager.GetViewElement(item) as IDockLayoutElement);
            return this.Rename(element);
        }

        public bool Rename(IDockLayoutElement element)
        {
            if ((element != null) && (element.Item != null))
            {
                BaseLayoutItem item = element.Item;
                this.ResetClickedState();
                if (this.Manager.RaiseItemCancelEvent(item, DockLayoutManager.LayoutItemStartRenamingEvent))
                {
                    return false;
                }
                LayoutView view = this.Manager.GetView(item.GetRoot()) as LayoutView;
                if (view != null)
                {
                    view.AdornerHelper.TryShowAdornerWindow(true);
                    DockingHintAdornerBase selectionAdorner = view.AdornerHelper.GetSelectionAdorner();
                    if ((selectionAdorner != null) && this.CanRename(item))
                    {
                        selectionAdorner.Update(true);
                        selectionAdorner.RenameController.StartRenaming(element);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RenameByClick(IDockLayoutElement layoutElement)
        {
            BaseLayoutItem item = layoutElement.Item;
            if (this.CanRename(item))
            {
                if (ReferenceEquals(item, this.lastClickedItem))
                {
                    this.Rename(layoutElement);
                    return true;
                }
                this.lastClickedItem = item;
            }
            return false;
        }

        public void ResetClickedState()
        {
            this.lastClickedItem = null;
        }

        private bool TryCancelRenaming()
        {
            bool flag;
            using (IEnumerator<IView> enumerator = this.Manager.ViewAdapter.Views.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        LayoutView current = (LayoutView) enumerator.Current;
                        RenameController renameController = current.AdornerHelper.GetRenameController();
                        if ((renameController == null) || !renameController.IsRenamingStarted)
                        {
                            continue;
                        }
                        renameController.CancelRenaming();
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private bool TryEndRenaming()
        {
            bool flag;
            using (IEnumerator<IView> enumerator = this.Manager.ViewAdapter.Views.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        LayoutView current = (LayoutView) enumerator.Current;
                        RenameController renameController = current.AdornerHelper.GetRenameController();
                        if ((renameController == null) || !renameController.IsRenamingStarted)
                        {
                            continue;
                        }
                        renameController.EndRenaming();
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }
    }
}

