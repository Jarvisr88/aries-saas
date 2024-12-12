namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Customization;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    public class LayoutController : ILayoutController, IActiveItemOwner, IDisposable
    {
        private bool isDisposingCore;
        private DockLayoutManager containerCore;
        private DevExpress.Xpf.Docking.Selection selectionCore;
        private ActiveItemHelper ActivationHelper;
        private BaseLayoutItem activeItemCore;
        private int lockActivateCounter;
        private HiddenItemsCollection hiddenItemsCore;
        private ReadOnlyCollection<BaseLayoutItem> fixedItemsCore;

        public LayoutController(DockLayoutManager container)
        {
            this.containerCore = container;
            this.ActivationHelper = new ActiveItemHelper(this);
            this.selectionCore = this.CreateSelection();
            this.hiddenItemsCore = this.CreateHiddenItemsCollection();
            this.Container.LayoutItemSelectionChanged += new LayoutItemSelectionChangedEventHandler(this.OnItemSelectionChanged);
        }

        public void Activate(BaseLayoutItem item)
        {
            this.Activate(item, true);
        }

        public void Activate(BaseLayoutItem item, bool focus)
        {
            if (((this.lockActivateCounter <= 0) && ((item != null) && (item.AllowActivate && !item.IsClosed))) && (!(item is LayoutGroup) || !((LayoutGroup) item).IsUngroupped))
            {
                this.lockActivateCounter++;
                try
                {
                    if (this.RaiseItemCancelEvent(item, DockLayoutManager.LayoutItemActivatingEvent))
                    {
                        item.InvokeCancelActivation(this.ActiveItem);
                    }
                    else
                    {
                        this.ActivationHelper.ActivateItem(item, focus);
                    }
                }
                finally
                {
                    this.lockActivateCounter--;
                }
            }
        }

        public bool CancelRenaming() => 
            this.Container.RenameHelper.CancelRenamingAndResetClickedState();

        private bool CanMove(BaseLayoutItem item, BaseLayoutItem target, MoveType type) => 
            (item != null) && ((target != null) && (!ReferenceEquals(item, target) && ((type != MoveType.None) && (item.AllowMove && ((!item.IsHidden || item.AllowRestore) && (((type != MoveType.InsideGroup) || ((target is LayoutGroup) && (!ReferenceEquals(item.Parent, target) || item.IsHidden))) ? (((type == MoveType.InsideGroup) || (target.Parent != null)) ? (!(item is LayoutGroup) || ((!LayoutItemsHelper.IsParent(target, item) && !((LayoutGroup) item).IsLayoutRoot) || item.IsHidden)) : false) : false))))));

        public bool ChangeGroupOrientation(LayoutGroup group, Orientation orientation)
        {
            if (group == null)
            {
                return false;
            }
            group.Orientation = orientation;
            this.Container.Update();
            return true;
        }

        public T CreateCommand<T>(BaseLayoutItem[] items) where T: LayoutControllerCommand, new()
        {
            T local1 = Activator.CreateInstance<T>();
            local1.Controller = this;
            local1.Items = items;
            return local1;
        }

        protected virtual ReadOnlyCollection<BaseLayoutItem> CreateFixedItemsCollection()
        {
            BaseLayoutItem[] list = new BaseLayoutItem[] { FixedItemFactory.CreateFixedItem(LayoutItemType.EmptySpaceItem), FixedItemFactory.CreateFixedItem(LayoutItemType.Label), FixedItemFactory.CreateFixedItem(LayoutItemType.Separator), FixedItemFactory.CreateFixedItem(LayoutItemType.LayoutSplitter) };
            return new ReadOnlyCollection<BaseLayoutItem>(list);
        }

        protected virtual HiddenItemsCollection CreateHiddenItemsCollection() => 
            new HiddenItemsCollection(this.Container, null);

        protected virtual DevExpress.Xpf.Docking.Selection CreateSelection() => 
            new DevExpress.Xpf.Docking.Selection();

        public bool EndRenaming() => 
            this.Container.RenameHelper.EndRenaming();

        public bool Group(BaseLayoutItem[] items)
        {
            if (!LayoutItemsHelper.AreInSameGroup(items))
            {
                return false;
            }
            using (new UpdateBatch(this.Container))
            {
                using (new NotificationBatch(this.Container))
                {
                    LayoutGroup parent = items[0].Parent;
                    int index = parent.Items.IndexOf(items[0]);
                    BaseLayoutItem[] itemArray = items;
                    int num2 = 0;
                    while (true)
                    {
                        if (num2 >= itemArray.Length)
                        {
                            using (new LogicalTreeLocker(this.Container, items))
                            {
                                BaseLayoutItem[] itemArray2 = items;
                                int num3 = 0;
                                while (true)
                                {
                                    if (num3 >= itemArray2.Length)
                                    {
                                        LayoutGroup group2 = DockControllerHelper.BoxIntoGroup(items, parent.Orientation, this.Container);
                                        group2.Caption = DockingLocalizer.GetString(DockingStringId.NewGroupCaption);
                                        group2.ShowCaption = true;
                                        parent.Insert(index, group2);
                                        this.Activate(group2);
                                        break;
                                    }
                                    BaseLayoutItem item2 = itemArray2[num3];
                                    item2.SetSelected(this.Container, false);
                                    parent.Remove(item2);
                                    num3++;
                                }
                            }
                            this.Container.Update();
                            NotificationBatch.Action(this.Container, parent.GetRoot(), null);
                            break;
                        }
                        BaseLayoutItem item = itemArray[num2];
                        index = Math.Min(index, parent.Items.IndexOf(item));
                        num2++;
                    }
                }
                return true;
            }
        }

        public bool Hide(BaseLayoutItem item)
        {
            if (item == null)
            {
                return false;
            }
            if (item.IsHidden && item.IsSelected)
            {
                item.SetSelected(this.Container, false);
            }
            if (item.IsHidden || (!item.AllowHide || (item.Parent == null)))
            {
                return false;
            }
            if (item.IsSelected)
            {
                item.SetSelected(this.Container, false);
            }
            bool flag = false;
            LayoutGroup root = item.GetRoot();
            using (new UpdateBatch(this.Container))
            {
                if (root != null)
                {
                    LayoutGroup parent = item.Parent;
                    BaseLayoutItem nextItem = LayoutItemsHelper.GetNextItem(item);
                    if (parent.Remove(item))
                    {
                        BaseLayoutItem nestedItem = null;
                        int index = -1;
                        LayoutGroup group3 = this.TryUngroupGroup(parent, ref nestedItem, ref index);
                        this.HiddenItems.Add(item, root);
                        if ((this.ActiveItem != null) && this.ActiveItem.IsHidden)
                        {
                            BaseLayoutItem item1 = nextItem;
                            if (nextItem == null)
                            {
                                BaseLayoutItem local1 = nextItem;
                                item1 = group3;
                            }
                            this.Activate(item1);
                        }
                        this.Container.CustomizationController.ClearSelection();
                        this.Container.RaiseEvent(new LayoutItemHiddenEventArgs(item));
                        this.Container.Update();
                        flag = true;
                    }
                }
                return flag;
            }
        }

        public void HideSelectedItems()
        {
            foreach (BaseLayoutItem item in this.Selection.ToArray())
            {
                this.Hide(item);
            }
        }

        public bool Move(BaseLayoutItem item, BaseLayoutItem target, MoveType type) => 
            this.Move(item, target, type, -1);

        public bool Move(BaseLayoutItem item, BaseLayoutItem target, MoveType type, int insertIndex)
        {
            if (!this.CanMove(item, target, type))
            {
                return false;
            }
            this.Container.CustomizationController.ClearSelection();
            LayoutGroup parent = item.Parent;
            bool isHidden = item.IsHidden;
            using (new NotificationBatch(this.Container))
            {
                using (new UpdateBatch(this.Container))
                {
                    bool flag2;
                    NotificationBatch.Action(this.Container, item.GetRoot(), null);
                    if (parent != null)
                    {
                        parent.Remove(item);
                        BaseLayoutItem nestedItem = null;
                        int index = -1;
                        LayoutGroup objA = this.TryUngroupGroup(parent, ref nestedItem, ref index);
                        if ((target is LayoutGroup) && (((LayoutGroup) target).IsUngroupped && !ReferenceEquals(objA, parent)))
                        {
                            if (nestedItem == null)
                            {
                                if (index != -1)
                                {
                                    this.MoveCore(item, objA, index);
                                }
                                else
                                {
                                    this.MoveCore(item, objA);
                                }
                                this.OnMoveComplete(item, target, type, isHidden);
                                return true;
                            }
                            else
                            {
                                target = nestedItem;
                            }
                        }
                    }
                    if (type != MoveType.InsideGroup)
                    {
                        LayoutGroup group3 = target.Parent;
                        if (group3 != null)
                        {
                            Orientation orientation = type.ToOrientation();
                            int index = group3.Items.IndexOf(target);
                            if (group3.IgnoreOrientation || (group3.Orientation == orientation))
                            {
                                index += (type.ToInsertType() == InsertType.Before) ? 0 : 1;
                            }
                            else
                            {
                                group3.Remove(target);
                                LayoutGroup group4 = DockControllerHelper.BoxIntoGroup(target, orientation, this.Container);
                                if (!target.ItemHeight.IsAuto && !item.ItemHeight.IsAuto)
                                {
                                    target.ItemHeight = item.ItemHeight;
                                }
                                this.MoveCore(item, group4, (type.ToInsertType() == InsertType.Before) ? 0 : 1);
                                item = group4;
                            }
                            this.MoveCore(item, group3, index);
                            goto TR_0008;
                        }
                        else
                        {
                            flag2 = false;
                        }
                    }
                    else
                    {
                        this.MoveCore(item, (LayoutGroup) target, insertIndex);
                        goto TR_0008;
                    }
                    return flag2;
                TR_0008:
                    this.OnMoveComplete(item, target, type, isHidden);
                    return true;
                }
            }
        }

        private void MoveCore(BaseLayoutItem item, LayoutGroup target)
        {
            this.MoveCore(item, target, -1);
        }

        private void MoveCore(BaseLayoutItem item, LayoutGroup target, int index)
        {
            if (item.IsHidden)
            {
                this.HiddenItems.Remove(item);
            }
            if (index != -1)
            {
                target.Insert(index, item);
            }
            else
            {
                target.Add(item);
            }
        }

        protected void OnDisposing()
        {
            Ref.Dispose<DevExpress.Xpf.Docking.Selection>(ref this.selectionCore);
            Ref.Dispose<HiddenItemsCollection>(ref this.hiddenItemsCore);
            Ref.Dispose<ActiveItemHelper>(ref this.ActivationHelper);
            this.activeItemCore = null;
            this.Container.LayoutItemSelectionChanged -= new LayoutItemSelectionChangedEventHandler(this.OnItemSelectionChanged);
            this.containerCore = null;
        }

        private void OnItemSelectionChanged(object sender, LayoutItemSelectionChangedEventArgs e)
        {
            this.Selection.ProcessSelectionChanged(e.Item, e.Selected);
        }

        private void OnMoveComplete(BaseLayoutItem item, BaseLayoutItem target, MoveType type, bool isHidden)
        {
            NotificationBatch.Action(this.Container, item.GetRoot(), null);
            this.Container.Update();
            this.RaiseItemMovedEvent(item, target, type, isHidden);
        }

        private void RaiseActiveItemChanged(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            LayoutItemActivatedEventArgs e = new LayoutItemActivatedEventArgs(item, oldItem);
            e.Source = this.Container;
            this.Container.RaiseEvent(e);
        }

        private bool RaiseItemCancelEvent(BaseLayoutItem item, RoutedEvent routedEvent) => 
            this.Container.RaiseItemCancelEvent(item, routedEvent);

        private void RaiseItemMovedEvent(BaseLayoutItem item, BaseLayoutItem target, MoveType type, bool isHidden)
        {
            if (isHidden)
            {
                this.Container.RaiseEvent(new LayoutItemRestoredEventArgs(item));
            }
            else
            {
                this.Container.RaiseEvent(new LayoutItemMovedEventArgs(item, target, type));
            }
        }

        public bool Rename(BaseLayoutItem item) => 
            this.Container.RenameHelper.Rename(item);

        public bool Restore(BaseLayoutItem item)
        {
            bool flag;
            LayoutGroup restoreRoot = this.GetRestoreRoot(item);
            if (restoreRoot == null)
            {
                return false;
            }
            using (new UpdateBatch(this.Container))
            {
                if (!this.HiddenItems.Remove(item))
                {
                    flag = false;
                }
                else
                {
                    restoreRoot.Add(item);
                    this.Container.RaiseEvent(new LayoutItemRestoredEventArgs(item));
                    this.Container.Update();
                    flag = true;
                }
            }
            return flag;
        }

        private void SetActive(bool value)
        {
            if (this.ActiveItem != null)
            {
                this.ActiveItem.SetActive(value);
                if (value && (this.lockActivateCounter == 0))
                {
                    this.ActivationHelper.SelectInGroup(this.ActiveItem);
                }
            }
        }

        private void SetActiveItemCore(BaseLayoutItem value)
        {
            this.SetActive(false);
            BaseLayoutItem activeItemCore = this.activeItemCore;
            this.activeItemCore = value;
            this.SetActive(true);
            DockLayoutManager container = this.Container;
            container.isLayoutItemActivation++;
            this.Container.ActiveLayoutItem = this.ActiveItem;
            DockLayoutManager manager2 = this.Container;
            manager2.isLayoutItemActivation--;
            this.RaiseActiveItemChanged(value, activeItemCore);
        }

        public bool SetGroupBorderStyle(LayoutGroup group, GroupBorderStyle style)
        {
            if (group == null)
            {
                return false;
            }
            group.GroupBorderStyle = style;
            this.Container.Update();
            return true;
        }

        void IDisposable.Dispose()
        {
            if (!this.IsDisposing)
            {
                this.isDisposingCore = true;
                this.OnDisposing();
            }
            GC.SuppressFinalize(this);
        }

        private LayoutGroup TryUngroupGroup(LayoutGroup group, ref BaseLayoutItem nestedItem, ref int index)
        {
            if (group == null)
            {
                return null;
            }
            if (group.DestroyOnClosingChildren)
            {
                LayoutGroup parent = group.Parent;
                int count = group.Items.Count;
                if (count != 0)
                {
                    if (count == 1)
                    {
                        nestedItem = group.Items[0];
                        this.TryUngroupGroup(nestedItem as LayoutGroup, ref nestedItem, ref index);
                        if (parent != null)
                        {
                            index = parent.Items.IndexOf(group);
                        }
                        return DockControllerHelper.Unbox(this.Container, group);
                    }
                }
                else
                {
                    nestedItem = null;
                    if (parent != null)
                    {
                        index = parent.Items.IndexOf(group);
                        if (parent.Items.Remove(group))
                        {
                            group.IsUngroupped = true;
                            return this.TryUngroupGroup(parent, ref nestedItem, ref index);
                        }
                    }
                }
            }
            return group;
        }

        public bool Ungroup(LayoutGroup group)
        {
            bool flag;
            using (new UpdateBatch(this.Container))
            {
                if ((group == null) || (group.Parent == null))
                {
                    flag = false;
                }
                else
                {
                    group.SetSelected(this.Container, false);
                    LayoutGroup parent = group.Parent;
                    int index = parent.Items.IndexOf(group);
                    BaseLayoutItem[] array = new BaseLayoutItem[group.Items.Count];
                    group.Items.CopyTo(array, 0);
                    BaseLayoutItem[] itemArray2 = array;
                    int num2 = 0;
                    while (true)
                    {
                        if (num2 >= itemArray2.Length)
                        {
                            parent.Remove(group);
                            this.Container.Update();
                            flag = true;
                            break;
                        }
                        BaseLayoutItem item = itemArray2[num2];
                        group.Remove(item);
                        parent.Insert(index++, item);
                        num2++;
                    }
                }
            }
            return flag;
        }

        protected bool IsDisposing =>
            this.isDisposingCore;

        [Description("Gets the DockLayoutManager container whose dock functionality is controlled by the current LayoutController.")]
        public DockLayoutManager Container =>
            this.containerCore;

        [Description("Gets the active layout item.")]
        public BaseLayoutItem ActiveItem
        {
            get => 
                this.activeItemCore;
            set
            {
                if (!ReferenceEquals(this.ActiveItem, value))
                {
                    this.SetActiveItemCore(value);
                }
            }
        }

        [Description("Gets whether Customization Mode is enabled.")]
        public bool IsCustomization =>
            this.Container.CustomizationController.IsCustomization;

        [Description("Gets a list of items that are selected in Customization Mode.")]
        public DevExpress.Xpf.Docking.Selection Selection =>
            this.selectionCore;

        [Description("Gets the collection of hidden items.")]
        public HiddenItemsCollection HiddenItems =>
            this.hiddenItemsCore;

        [Description("Gets a collection of the items that are always available in the Customization Window.")]
        public IEnumerable<BaseLayoutItem> FixedItems
        {
            get
            {
                if ((this.fixedItemsCore == null) && !this.IsDisposing)
                {
                    this.fixedItemsCore = this.CreateFixedItemsCollection();
                }
                return this.fixedItemsCore;
            }
        }
    }
}

