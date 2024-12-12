namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking.Internal;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public class BaseLayoutItemCollection : BaseItemsCollection<BaseLayoutItem>, ILockable
    {
        protected readonly LayoutGroup Owner;
        private readonly List<NotifyCollectionChangedEventArgs> pendingChanges = new List<NotifyCollectionChangedEventArgs>();
        private int lockCount;
        private List<BaseLayoutItem> lockedItems;
        private readonly OrderedDictionary collectionHash = new OrderedDictionary();
        private readonly Locker addToItemsSourceLocker = new Locker();
        private readonly Locker removeFromItemsSourceLocker = new Locker();

        public BaseLayoutItemCollection(LayoutGroup owner)
        {
            this.Owner = owner;
        }

        internal bool AllowDockToDocumentGroup()
        {
            bool flag;
            using (IEnumerator<BaseLayoutItem> enumerator = base.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseLayoutItem current = enumerator.Current;
                        if ((current is LayoutPanel) && !((LayoutPanel) current).AllowDockToDocumentGroup)
                        {
                            flag = false;
                        }
                        else
                        {
                            if (current.ItemType != LayoutItemType.Group)
                            {
                                continue;
                            }
                            LayoutGroup group = current as LayoutGroup;
                            if ((group.Items.Count == 0) || group.Items.AllowDockToDocumentGroup())
                            {
                                continue;
                            }
                            flag = false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        protected virtual void BeforeItemAdded(BaseLayoutItem item)
        {
            this.CheckItemRules(item);
            this.Owner.BeforeItemAdded(item);
        }

        public virtual void BeginUpdate()
        {
            this.lockCount++;
            this.lockedItems = this.ToList<BaseLayoutItem>();
        }

        protected internal void CancelUpdate()
        {
            this.lockCount--;
        }

        private bool CanDockToDocumentGroup(BaseLayoutItem item) => 
            (item is LayoutPanel) && ((LayoutPanel) item).AllowDockToDocumentGroup;

        private void CheckIsUsingItemsSource()
        {
            if (base.IsUsingItemsSource && (!this.removeFromItemsSourceLocker && (DockLayoutManagerParameters.CheckLayoutGroupIsUsingItemsSource && !this.addToItemsSourceLocker)))
            {
                throw new InvalidOperationException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.ItemsSourceInUse));
            }
        }

        protected override bool CheckItemCollectionMustBeEmpty() => 
            base.CheckItemCollectionMustBeEmpty() && DockLayoutManagerParameters.CheckLayoutGroupIsUsingItemsSource;

        protected virtual void CheckItemRules(BaseLayoutItem item)
        {
            if ((item.ItemType == LayoutItemType.ControlItem) && (this.Owner.ItemType != LayoutItemType.Group))
            {
                throw new NotSupportedException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.ItemCanNotBeHosted));
            }
            if (this.Owner.IsControlItemsHost)
            {
                if ((item.ItemType == LayoutItemType.Panel) || (item.ItemType == LayoutItemType.Document))
                {
                    throw new NotSupportedException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.InconsistentLayout));
                }
                if ((item.ItemType == LayoutItemType.DocumentPanelGroup) || (item.ItemType == LayoutItemType.TabPanelGroup))
                {
                    throw new NotSupportedException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.InconsistentLayout));
                }
            }
            if ((item.ItemType != LayoutItemType.Panel) && (this.Owner.ItemType == LayoutItemType.TabPanelGroup))
            {
                throw new NotSupportedException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.WrongPanel));
            }
            if ((this.Owner.ItemType == LayoutItemType.DocumentPanelGroup) && !this.CanDockToDocumentGroup(item))
            {
                throw new NotSupportedException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.WrongDocument));
            }
            if (item.ItemType == LayoutItemType.FloatGroup)
            {
                throw new NotSupportedException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.FloatGroupsCollection));
            }
            if (item.ItemType == LayoutItemType.AutoHideGroup)
            {
                throw new NotSupportedException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.AutoHideGroupsCollection));
            }
            if (!(item is LayoutPanel) && (this.Owner.ItemType == LayoutItemType.AutoHideGroup))
            {
                throw new NotSupportedException(DockLayoutManagerHelper.GetRule(DockLayoutManagerRule.WrongAutoHiddenPanel));
            }
        }

        protected override void ClearItems()
        {
            this.CheckIsUsingItemsSource();
            this.ClearItemsCore();
        }

        private void ClearItemsCore()
        {
            BaseLayoutItem[] itemArray = base.Items.ToArray<BaseLayoutItem>();
            base.ClearItems();
            for (int i = 0; i < itemArray.Length; i++)
            {
                this.OnItemRemoved(itemArray[i]);
            }
        }

        internal void CollectConstraints(out Size[] minSizes, out Size[] maxSizes)
        {
            minSizes = new Size[base.Items.Count];
            maxSizes = new Size[base.Items.Count];
            for (int i = 0; i < minSizes.Length; i++)
            {
                if (base.Items[i].IsVisibleCore)
                {
                    minSizes[i] = base.Items[i].ActualMinSize;
                    maxSizes[i] = base.Items[i].ActualMaxSize;
                }
            }
        }

        internal bool ContainsLayoutControlItem()
        {
            bool flag;
            using (IEnumerator<BaseLayoutItem> enumerator = base.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseLayoutItem current = enumerator.Current;
                        if (!LayoutItemsHelper.IsLayoutItem(current))
                        {
                            continue;
                        }
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

        internal bool ContainsLayoutControlItemOrGroup()
        {
            bool flag;
            using (IEnumerator<BaseLayoutItem> enumerator = base.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseLayoutItem current = enumerator.Current;
                        if (LayoutItemsHelper.IsLayoutItem(current))
                        {
                            flag = true;
                        }
                        else
                        {
                            LayoutGroup group = current as LayoutGroup;
                            if ((group == null) || ((group.Items.Count <= 0) || !group.Items.ContainsLayoutControlItemOrGroup()))
                            {
                                continue;
                            }
                            flag = true;
                        }
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

        internal bool ContainsNestedControlItemHostItems()
        {
            bool flag;
            using (IEnumerator<BaseLayoutItem> enumerator = base.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseLayoutItem current = enumerator.Current;
                        if (current.IsControlItemsHost)
                        {
                            flag = true;
                        }
                        else
                        {
                            LayoutGroup group = current as LayoutGroup;
                            if ((group == null) || ((group.Items.Count <= 0) || !group.Items.ContainsNestedControlItemHostItems()))
                            {
                                continue;
                            }
                            flag = true;
                        }
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

        internal bool ContainsNonEmptyDocumentGroups()
        {
            bool flag;
            using (IEnumerator<BaseLayoutItem> enumerator = base.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseLayoutItem current = enumerator.Current;
                        if (current.Parent is DocumentGroup)
                        {
                            flag = true;
                        }
                        else
                        {
                            if (!(current is LayoutGroup) || !((LayoutGroup) current).Items.ContainsNonEmptyDocumentGroups())
                            {
                                continue;
                            }
                            flag = true;
                        }
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

        internal bool ContainsOnlyControlItemsOrItsHosts()
        {
            bool flag;
            using (IEnumerator<BaseLayoutItem> enumerator = base.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseLayoutItem current = enumerator.Current;
                        if (LayoutItemsHelper.IsLayoutItem(current))
                        {
                            continue;
                        }
                        if (current.ItemType == LayoutItemType.Panel)
                        {
                            flag = false;
                        }
                        else if (current.ItemType == LayoutItemType.Document)
                        {
                            flag = false;
                        }
                        else
                        {
                            if (current.IsControlItemsHost)
                            {
                                continue;
                            }
                            if (current.ItemType == LayoutItemType.Group)
                            {
                                LayoutGroup group = current as LayoutGroup;
                                if (group.Items.Count == 0)
                                {
                                    continue;
                                }
                                if (group.Items.ContainsOnlyControlItemsOrItsHosts())
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            flag = false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        internal bool ContainsOnlyLayoutControlItemOrGroup()
        {
            bool flag;
            using (IEnumerator<BaseLayoutItem> enumerator = base.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseLayoutItem current = enumerator.Current;
                        if (LayoutItemsHelper.IsLayoutItem(current) || (current.ItemType == LayoutItemType.Group))
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        public virtual void EndUpdate()
        {
            this.CancelUpdate();
            if (!this.IsLockUpdate)
            {
                if ((this.lockedItems != null) && !this.lockedItems.SequenceEqual<BaseLayoutItem>(this))
                {
                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
                this.lockedItems = null;
            }
        }

        public static BaseLayoutItem FindItem(IList<BaseLayoutItem> items, string name)
        {
            BaseLayoutItem item2;
            using (IEnumerator<BaseLayoutItem> enumerator = items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseLayoutItem current = enumerator.Current;
                        if (current.Name != name)
                        {
                            continue;
                        }
                        item2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return item2;
        }

        private int GetActualStartingIndex(int startingIndex)
        {
            int num = -1;
            List<object> list = this.collectionHash.Values.OfType<object>().ToList<object>();
            int num2 = startingIndex - 1;
            while (true)
            {
                if (num2 >= 0)
                {
                    if (!base.Contains((BaseLayoutItem) list[num2]))
                    {
                        num2--;
                        continue;
                    }
                    num = base.IndexOf((BaseLayoutItem) list[num2]) + 1;
                }
                if ((num == -1) && !this.Owner.IsInTree())
                {
                    for (int i = startingIndex - 1; i >= 0; i--)
                    {
                        BaseLayoutItem item = (BaseLayoutItem) list[i];
                        PlaceHolder holder = this.Owner.PlaceHolderHelper.GetPlaceHolders().FirstOrDefault<PlaceHolder>(x => ReferenceEquals(x.Owner, item));
                        if (holder != null)
                        {
                            num = this.Owner.PlaceHolderHelper.IndexOf(holder) + 1;
                            break;
                        }
                    }
                }
                return Math.Max(num, 0);
            }
        }

        internal double GetIntervals()
        {
            double num = 0.0;
            for (int i = 1; i < base.Items.Count; i++)
            {
                num += (double) this.Owner.GetValue(IntervalHelper.GetTargetProperty(base.Items[i - 1], base.Items[i]));
            }
            return num;
        }

        private DockLayoutManager GetManager() => 
            (this.Owner != null) ? this.Owner.GetRoot().Manager : null;

        internal bool HasVisibleStarItems()
        {
            bool isHorizontal = this.Owner.Orientation == Orientation.Horizontal;
            return (this.Count<BaseLayoutItem>(x => (LayoutItemsHelper.IsActuallyVisibleInTree(x) && (isHorizontal ? x.ItemWidth.IsStar : x.ItemHeight.IsStar))) != 0);
        }

        protected override void InsertItem(int index, BaseLayoutItem item)
        {
            this.CheckIsUsingItemsSource();
            this.InsertItemCore(index, item);
        }

        private void InsertItemCore(int index, BaseLayoutItem item)
        {
            using (new UpdateBatch(this.GetManager()))
            {
                using (item.ParentLockHelper.Lock())
                {
                    this.BeforeItemAdded(item);
                    base.InsertItem(index, item);
                    this.NotifyItemInserted(item, index);
                }
            }
        }

        protected override void InvalidateItemsSource()
        {
            foreach (DictionaryEntry entry in this.collectionHash)
            {
                BaseLayoutItem container = entry.Value as BaseLayoutItem;
                object key = entry.Key;
                using (this.removeFromItemsSourceLocker.Lock())
                {
                    this.Owner.ClearContainerForItem(container, key);
                    this.OnItemRemoved(container);
                }
            }
            this.collectionHash.Clear();
        }

        internal bool IsDocumentHost()
        {
            bool flag2;
            bool flag = false;
            using (IEnumerator<BaseLayoutItem> enumerator = base.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseLayoutItem current = enumerator.Current;
                        if (!(current is LayoutPanel))
                        {
                            if (!(current is DocumentGroup))
                            {
                                continue;
                            }
                            flag = true;
                            continue;
                        }
                        flag2 = false;
                    }
                    else
                    {
                        return flag;
                    }
                    break;
                }
            }
            return flag2;
        }

        internal bool IsDocumentHost(bool ignoreLayoutPanels)
        {
            bool flag = false;
            if (!ignoreLayoutPanels)
            {
                flag = this.IsDocumentHost();
            }
            else
            {
                foreach (BaseLayoutItem item in base.Items)
                {
                    if (item is DocumentGroup)
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            if (this.Owner != null)
            {
                this.Owner.moveItemLock++;
            }
            base.MoveItem(oldIndex, newIndex);
            if (this.Owner != null)
            {
                this.Owner.moveItemLock--;
            }
        }

        protected virtual void NotifyItemInserted(BaseLayoutItem item, int index)
        {
            this.OnItemAdded(item);
            if (this.Owner != null)
            {
                this.Owner.AfterItemAdded(index, item);
            }
        }

        protected virtual void NotifyItemRemoved(BaseLayoutItem item)
        {
            if (this.Owner != null)
            {
                this.Owner.AfterItemRemoved(item);
            }
        }

        protected override void OnAddToItemsSource(IEnumerable newItems, int startingIndex = 0)
        {
            if (newItems != null)
            {
                using (this.addToItemsSourceLocker.Lock())
                {
                    int actualStartingIndex = this.GetActualStartingIndex(startingIndex);
                    using (new UpdateBatch(this.Owner.GetDockLayoutManager()))
                    {
                        foreach (object obj2 in newItems)
                        {
                            DependencyObject obj3 = ((IGeneratorHost) this.Owner).GenerateContainerForItem(obj2, actualStartingIndex++, null, null);
                            this.collectionHash.Add(obj2, obj3);
                        }
                    }
                }
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.IsLockUpdate)
            {
                this.pendingChanges.Add(e);
            }
            else
            {
                base.OnCollectionChanged(e);
            }
        }

        protected override void OnCurrentChanged(object sender)
        {
        }

        protected virtual void OnItemAdded(BaseLayoutItem item)
        {
            item.Parent = this.Owner;
        }

        protected override void OnItemMovedInItemsSource(int oldStartingIndex, int newStartingIndex)
        {
            base.Move(oldStartingIndex, newStartingIndex);
        }

        protected virtual void OnItemRemoved(BaseLayoutItem item)
        {
            item.Parent = null;
            this.Owner.AfterItemRemoved(item);
        }

        protected override void OnItemReplacedInItemsSource(IList oldItems, IList newItems, int newStartingIndex)
        {
            this.OnRemoveFromItemsSource(oldItems);
            this.OnAddToItemsSource(newItems, newStartingIndex);
        }

        protected override void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            using (new UpdateBatch(this.Owner.Manager))
            {
                this.Owner.BeginUpdate();
                base.OnItemsSourceCollectionChanged(sender, e);
                this.Owner.EndUpdate();
            }
        }

        protected override void OnRemoveFromItemsSource(IEnumerable oldItems)
        {
            foreach (object obj2 in oldItems)
            {
                if (this.collectionHash.Contains(obj2))
                {
                    BaseLayoutItem container = this.collectionHash[obj2] as BaseLayoutItem;
                    using (this.removeFromItemsSourceLocker.Lock())
                    {
                        this.Owner.ClearContainerForItem(container, obj2);
                    }
                    this.RemoveItemCore(container);
                    this.collectionHash.Remove(obj2);
                }
            }
        }

        protected override void OnResetItemsSource()
        {
            this.InvalidateItemsSource();
            this.OnAddToItemsSource(base.ItemsSource, 0);
        }

        internal void ProcessRemovedItems()
        {
            if ((this.lockedItems != null) && !this.lockedItems.SequenceEqual<BaseLayoutItem>(this))
            {
                Func<NotifyCollectionChangedEventArgs, bool> predicate = <>c.<>9__24_0;
                if (<>c.<>9__24_0 == null)
                {
                    Func<NotifyCollectionChangedEventArgs, bool> local1 = <>c.<>9__24_0;
                    predicate = <>c.<>9__24_0 = x => x.Action == NotifyCollectionChangedAction.Remove;
                }
                foreach (NotifyCollectionChangedEventArgs args in this.pendingChanges.Where<NotifyCollectionChangedEventArgs>(predicate).ToList<NotifyCollectionChangedEventArgs>())
                {
                    base.OnCollectionChanged(args);
                }
            }
            this.pendingChanges.Clear();
        }

        protected override void RemoveItem(int index)
        {
            this.CheckIsUsingItemsSource();
            this.RemoveItemAt(index);
        }

        private void RemoveItemAt(int index)
        {
            BaseLayoutItem item = ((index < 0) || (index >= base.Count)) ? null : base[index];
            if (item != null)
            {
                item.ParentLockHelper.Lock();
                item.LockInLogicalTree();
            }
            base.RemoveItem(index);
            if (item != null)
            {
                this.OnItemRemoved(item);
                this.NotifyItemRemoved(item);
                item.UnlockItemInLogicalTree();
                item.ParentLockHelper.Unlock();
            }
        }

        private void RemoveItemCore(BaseLayoutItem item)
        {
            int index = base.IndexOf(item);
            if (index != -1)
            {
                this.RemoveItemAt(index);
            }
        }

        internal void ResetItemsSource()
        {
            this.OnResetItemsSource();
        }

        public bool IsLockUpdate =>
            this.lockCount != 0;

        public BaseLayoutItem this[string name] =>
            FindItem(base.Items, name);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseLayoutItemCollection.<>c <>9 = new BaseLayoutItemCollection.<>c();
            public static Func<NotifyCollectionChangedEventArgs, bool> <>9__24_0;

            internal bool <ProcessRemovedItems>b__24_0(NotifyCollectionChangedEventArgs x) => 
                x.Action == NotifyCollectionChangedAction.Remove;
        }
    }
}

