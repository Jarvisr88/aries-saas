namespace DevExpress.Xpf.Docking.Internal
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class PlaceHolderHelper
    {
        private readonly ObservableCollection<object> itemsInternal = new ObservableCollection<object>();

        public PlaceHolderHelper(LayoutGroup owner)
        {
            this.Owner = owner;
        }

        public void Add(object item)
        {
            if (!this.itemsInternal.Contains(item))
            {
                this.itemsInternal.Add(item);
            }
        }

        internal static void AddFakePlaceHolderForItem(LayoutGroup parent, BaseLayoutItem item, PlaceHolderState dockState = 3)
        {
            AddFakePlaceHolderForItem(parent, item, 0, dockState);
        }

        internal static void AddFakePlaceHolderForItem(LayoutGroup parent, BaseLayoutItem item, int index, PlaceHolderState dockState = 3)
        {
            PlaceHolder holder1 = new PlaceHolder(item, parent);
            holder1.DockState = dockState;
            PlaceHolder placeHolder = holder1;
            if (!item.DockInfo.Contains(placeHolder))
            {
                item.DockInfo.Add(placeHolder);
            }
            if (parent != null)
            {
                parent.PlaceHolderHelper.Insert(index, placeHolder);
            }
        }

        public void AddPlaceHolderForItem(BaseLayoutItem item)
        {
            if (this.GetPlaceHolderForItem(item) == null)
            {
                LayoutGroup parent = item.Parent;
                PlaceHolder placeHolder = new PlaceHolder(item, parent);
                if (!item.DockInfo.Contains(placeHolder))
                {
                    item.DockInfo.Add(placeHolder);
                }
                this.Insert(this.IndexOf(item), placeHolder);
            }
        }

        internal static void AddPlaceHolderForItem(LayoutGroup parent, BaseLayoutItem item, PlaceHolder ph, int index)
        {
            if (!item.DockInfo.Contains(ph))
            {
                item.DockInfo.Add(ph);
            }
            if (parent != null)
            {
                parent.PlaceHolderHelper.Insert(index, ph);
            }
        }

        internal static bool CanRestoreLayoutHierarchy(BaseLayoutItem item, PlaceHolderState desiredState = 0)
        {
            if (item == null)
            {
                return false;
            }
            PlaceHolder holder = item.DockInfo.Find(di => di.DockState == desiredState);
            if (holder == null)
            {
                return false;
            }
            LayoutGroup parent = holder.Parent;
            return ((parent != null) && CanRestoreLayoutHierarchyCore(parent, desiredState));
        }

        private static bool CanRestoreLayoutHierarchyCore(LayoutGroup group, PlaceHolderState desiredState)
        {
            PlaceHolder holder = group.DockInfo.FirstOrDefault(x => x.DockState == desiredState);
            if (holder != null)
            {
                return CanRestoreLayoutHierarchyCore(holder.Parent, desiredState);
            }
            LayoutGroup root = group.GetRoot();
            return ((root is AutoHideGroup) || ((root is FloatGroup) || root.IsRootGroup));
        }

        public static void ClearPlaceHolder(BaseLayoutItem itemToDock)
        {
            Action<PlaceHolder> action = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Action<PlaceHolder> local1 = <>c.<>9__10_0;
                action = <>c.<>9__10_0 = delegate (PlaceHolder placeHolder) {
                    LayoutGroup parent = placeHolder.Parent;
                    if (parent != null)
                    {
                        parent.PlaceHolderHelper.Remove(placeHolder);
                    }
                };
            }
            itemToDock.DockInfo.ForeEach(action);
            itemToDock.DockInfo.Clear();
        }

        public static void ClearPlaceHolder(BaseLayoutItem itemToDock, BaseLayoutItem dockingRoot)
        {
            itemToDock.DockInfo.ToList().ForEach(delegate (PlaceHolder placeHolder) {
                LayoutGroup parent = placeHolder.Parent;
                if ((parent == null) || ReferenceEquals(GetPlaceHolderRoot(placeHolder).Parent, dockingRoot))
                {
                    itemToDock.DockInfo.Remove(placeHolder);
                }
                else
                {
                    parent.PlaceHolderHelper.Remove(placeHolder);
                    itemToDock.DockInfo.Remove(placeHolder);
                }
            });
        }

        public static void ClearPlaceHolder(BaseLayoutItem item, PlaceHolder placeHolder)
        {
            if (placeHolder != null)
            {
                LayoutGroup parent = placeHolder.Parent;
                item.DockInfo.Remove(placeHolder);
                if (parent != null)
                {
                    parent.PlaceHolderHelper.Remove(placeHolder);
                }
            }
        }

        public static void ClearPlaceHolder(BaseLayoutItem item, PlaceHolderState state)
        {
            PlaceHolder placeHolder = GetPlaceHolder(item, state);
            ClearPlaceHolder(item, placeHolder);
        }

        public bool Contains(object item) => 
            this.itemsInternal.Contains(item);

        internal static void DecomposeTo(BaseLayoutItem itemToDecompose, LayoutGroup newGroup)
        {
            IEnumerable<LayoutGroup> enumerable;
            DecomposeTo(itemToDecompose, newGroup, out enumerable);
        }

        internal static void DecomposeTo(BaseLayoutItem itemToDecompose, LayoutGroup newGroup, out IEnumerable<LayoutGroup> decomposedGroups)
        {
            List<LayoutGroup> list = new List<LayoutGroup>();
            DecomposeToCore(itemToDecompose, newGroup, list);
            decomposedGroups = list;
        }

        private static void DecomposeToCore(BaseLayoutItem itemToDecompose, LayoutGroup newGroup, List<LayoutGroup> decomposedGroups)
        {
            LayoutGroup group = itemToDecompose as LayoutGroup;
            if (group == null)
            {
                newGroup.Add(itemToDecompose);
                newGroup.PlaceHolderHelper.Add(itemToDecompose);
            }
            else
            {
                decomposedGroups.Add(group);
                object[] objArray = group.PlaceHolderHelper.itemsInternal.ToArray<object>();
                int index = 0;
                while (true)
                {
                    while (true)
                    {
                        if (index >= objArray.Length)
                        {
                            return;
                        }
                        object obj2 = objArray[index];
                        PlaceHolder holder = obj2 as PlaceHolder;
                        if (holder != null)
                        {
                            if (holder.Owner is LayoutGroup)
                            {
                                DecomposeToCore((LayoutGroup) holder.Owner, newGroup, decomposedGroups);
                                break;
                            }
                            holder.Parent.PlaceHolderHelper.Remove(holder);
                            holder.Parent = newGroup;
                        }
                        LayoutGroup group2 = obj2 as LayoutGroup;
                        if (group2 != null)
                        {
                            DecomposeToCore(group2, newGroup, decomposedGroups);
                        }
                        else
                        {
                            BaseLayoutItem item = obj2 as BaseLayoutItem;
                            if (item != null)
                            {
                                item.Parent.Remove((BaseLayoutItem) obj2);
                                newGroup.Add((BaseLayoutItem) obj2);
                            }
                            newGroup.PlaceHolderHelper.Add(obj2);
                        }
                        break;
                    }
                    index++;
                }
            }
        }

        public static LayoutGroup GetAffectedGroup(BaseLayoutItem panel, PlaceHolderState state = 0)
        {
            PlaceHolder holder = panel.DockInfo.Find(di => di.DockState == state);
            return holder?.Parent;
        }

        public static LayoutGroup[] GetAffectedGroups(BaseLayoutItem panel)
        {
            List<LayoutGroup> groups = new List<LayoutGroup>();
            panel.DockInfo.ForeEach(delegate (PlaceHolder ph) {
                groups.Add(ph.Parent);
            });
            return groups.ToArray();
        }

        public static int GetDockIndex(PlaceHolder placeHolder)
        {
            LayoutGroup parent = placeHolder.Parent;
            PlaceHolderHelper placeHolderHelper = parent.PlaceHolderHelper;
            int num = 0;
            int index = placeHolderHelper.IndexOf(placeHolder);
            if (index == -1)
            {
                index = placeHolder.GetDockIndex();
            }
            BaseLayoutItem item = null;
            int num3 = index;
            while (true)
            {
                if (num3 < placeHolderHelper.Count)
                {
                    if (!(placeHolderHelper[num3] is BaseLayoutItem))
                    {
                        num3++;
                        continue;
                    }
                    item = (BaseLayoutItem) placeHolderHelper[num3];
                    num = parent.Items.IndexOf(item);
                }
                if (item == null)
                {
                    for (int i = index - 1; i >= 0; i--)
                    {
                        if (placeHolderHelper[i] is BaseLayoutItem)
                        {
                            item = (BaseLayoutItem) placeHolderHelper[i];
                            num = parent.Items.IndexOf(item) + 1;
                            break;
                        }
                    }
                }
                return num;
            }
        }

        internal static int GetDockIndex(BaseLayoutItem itemToDock, PlaceHolder ph) => 
            GetDockIndex(ph);

        internal static IEnumerable<LayoutGroup> GetLayoutHierarchy(PlaceHolder placeHolder)
        {
            List<LayoutGroup> list = new List<LayoutGroup>();
            PlaceHolder holder = placeHolder;
            while (true)
            {
                if ((holder != null) && ((holder.Parent != null) && !holder.Parent.IsInTree()))
                {
                    list.Add(holder.Parent);
                    PlaceHolder holder2 = holder.Parent.DockInfo.LastOrDefault();
                    if (holder2 != null)
                    {
                        holder = holder2;
                        continue;
                    }
                }
                return list;
            }
        }

        public static PlaceHolder GetPlaceHolder(BaseLayoutItem itemToDock, PlaceHolderState state = 0) => 
            itemToDock.DockInfo.Find(di => di.DockState == state);

        public static int GetPlaceHolderCount(BaseLayoutItem item) => 
            item.DockInfo.Count;

        public static PlaceHolder GetPlaceHolderForDockOperation(BaseLayoutItem item, bool isRestore)
        {
            PlaceHolder holder = PlaceHolderToDock(item, isRestore);
            return (!isRestore ? (((holder == null) || !CanRestoreLayoutHierarchy(item, holder.DockState)) ? null : holder) : holder);
        }

        private PlaceHolder GetPlaceHolderForItem(BaseLayoutItem item) => 
            (PlaceHolder) this.itemsInternal.FirstOrDefault<object>(obj => ((obj is PlaceHolder) && ReferenceEquals(((PlaceHolder) obj).Owner, item)));

        internal static PlaceHolder GetPlaceHolderRoot(PlaceHolder placeHolder)
        {
            PlaceHolder holder = placeHolder;
            while (true)
            {
                if ((holder != null) && ((holder.Parent != null) && !holder.Parent.IsInTree()))
                {
                    PlaceHolder holder2 = holder.Parent.DockInfo.LastOrDefault();
                    if (holder2 != null)
                    {
                        holder = holder2;
                        continue;
                    }
                }
                return holder;
            }
        }

        internal static LayoutGroup GetPlaceHolderRoot(BaseLayoutItem item, PlaceHolderState desiredState)
        {
            PlaceHolder holder = item.DockInfo.FirstOrDefault(x => x.DockState == desiredState);
            LayoutGroup parent = null;
            while ((holder != null) && (holder.Parent != null))
            {
                parent = holder.Parent;
                holder = parent.DockInfo.FirstOrDefault(x => x.DockState == desiredState);
            }
            return parent;
        }

        public IEnumerable<PlaceHolder> GetPlaceHolders()
        {
            Func<object, bool> predicate = <>c.<>9__43_0;
            if (<>c.<>9__43_0 == null)
            {
                Func<object, bool> local1 = <>c.<>9__43_0;
                predicate = <>c.<>9__43_0 = x => x is PlaceHolder;
            }
            return this.itemsInternal.Where<object>(predicate).Cast<PlaceHolder>();
        }

        public static bool HasItems(BaseLayoutItem item) => 
            item.DockInfo.Count > 0;

        public int IndexOf(object item) => 
            this.itemsInternal.IndexOf(item);

        public void Insert(int index, object item)
        {
            if (!this.itemsInternal.Contains(item))
            {
                if ((index < 0) || (index >= this.Count))
                {
                    index = this.Count;
                }
                this.itemsInternal.Insert(index, item);
            }
        }

        public void InsertItem(int index, BaseLayoutItem item)
        {
            LayoutGroup owner = this.Owner;
            PlaceHolder placeHolderForItem = this.GetPlaceHolderForItem(item);
            if ((placeHolderForItem != null) && ReferenceEquals(placeHolderForItem.Parent, owner))
            {
                index = owner.PlaceHolderHelper.IndexOf(placeHolderForItem);
                owner.PlaceHolderHelper.Insert(index, item);
                owner.PlaceHolderHelper.Remove(placeHolderForItem);
            }
            else if (index >= (owner.Items.Count - 1))
            {
                owner.PlaceHolderHelper.Add(item);
            }
            else
            {
                BaseLayoutItem item2 = owner.Items[index + 1];
                index = owner.PlaceHolderHelper.IndexOf(item2);
                owner.PlaceHolderHelper.Insert(index, item);
            }
        }

        internal static void InsertItemToPosition(LayoutGroup parent, BaseLayoutItem oldItem, BaseLayoutItem item)
        {
            PlaceHolder placeHolderForItem = parent.PlaceHolderHelper.GetPlaceHolderForItem(oldItem);
            if (placeHolderForItem != null)
            {
                placeHolderForItem.Owner = item;
                int dockIndex = GetDockIndex(placeHolderForItem);
                parent.Insert(dockIndex, item);
                parent.PlaceHolderHelper.Remove(placeHolderForItem);
            }
        }

        public void MoveItem(int index, BaseLayoutItem item)
        {
            this.Remove(item);
            this.InsertItem(index, item);
        }

        internal void MoveItemsTo(LayoutGroup group)
        {
            foreach (PlaceHolder holder in this.GetPlaceHolders().ToList<PlaceHolder>())
            {
                this.Remove(holder);
                holder.Parent = group;
                group.PlaceHolderHelper.Insert(holder.Index, holder);
            }
        }

        private static PlaceHolder PlaceHolderToDock(BaseLayoutItem item, bool isRestore)
        {
            PlaceHolder holder = null;
            if (isRestore)
            {
                Func<PlaceHolder, bool> predicate = <>c.<>9__1_0;
                if (<>c.<>9__1_0 == null)
                {
                    Func<PlaceHolder, bool> local1 = <>c.<>9__1_0;
                    predicate = <>c.<>9__1_0 = d => !d.IsDocked;
                }
                holder = item.DockInfo.LastOrDefault(predicate);
            }
            if (holder == null)
            {
                Func<PlaceHolder, bool> predicate = <>c.<>9__1_1;
                if (<>c.<>9__1_1 == null)
                {
                    Func<PlaceHolder, bool> local2 = <>c.<>9__1_1;
                    predicate = <>c.<>9__1_1 = d => d.IsDocked;
                }
                holder = item.DockInfo.Find(predicate);
            }
            return holder;
        }

        public void Remove(object item)
        {
            this.itemsInternal.Remove(item);
        }

        internal static void ReplacePlaceHolderOwner(LayoutGroup parent, PlaceHolder oldPlaceHolder, BaseLayoutItem newItem)
        {
            oldPlaceHolder.Owner = newItem;
            oldPlaceHolder.Owner.DockInfo.Remove(oldPlaceHolder);
            newItem.DockInfo.Add(oldPlaceHolder);
        }

        internal static IEnumerable<LayoutGroup> RestoreLayoutHierarchy(DockLayoutManager container, PlaceHolder ph, out LayoutGroup targetGroup)
        {
            targetGroup = null;
            List<LayoutGroup> affectedGroups = new List<LayoutGroup>();
            if ((ph != null) && (ph.Parent != null))
            {
                LayoutGroup item = targetGroup = ph.Parent;
                if (item.IsInTree())
                {
                    return affectedGroups;
                }
                if (item.GetRoot().IsInTree())
                {
                    affectedGroups.Add(targetGroup);
                }
                else
                {
                    RestoreLayoutHierarchyCore(container, item, affectedGroups);
                }
            }
            return affectedGroups;
        }

        private static void RestoreLayoutHierarchyCore(DockLayoutManager container, LayoutGroup group, List<LayoutGroup> affectedGroups)
        {
            PlaceHolder placeHolder = group.DockInfo.LastOrDefault();
            if ((placeHolder != null) && (placeHolder.DockState != PlaceHolderState.Unset))
            {
                LayoutGroup parent = placeHolder.Parent;
                RestoreLayoutHierarchyCore(container, parent, affectedGroups);
                int dockIndex = GetDockIndex(placeHolder);
                DockControllerHelper.InsertItemInGroup(parent, group, dockIndex, false);
                ClearPlaceHolder(group);
            }
            group.IsUngroupped = false;
            container.DecomposedItems.Remove(group);
            affectedGroups.Add(group);
        }

        internal static void StorePositionAndRemove(LayoutGroup parent, BaseLayoutItem item)
        {
            PlaceHolder holder1 = new PlaceHolder(item, parent);
            holder1.DockState = PlaceHolderState.Unset;
            PlaceHolder holder = holder1;
            parent.PlaceHolderHelper.Insert(parent.PlaceHolderHelper.IndexOf(item), holder);
            parent.Remove(item);
        }

        public LayoutGroup Owner { get; private set; }

        public int Count =>
            this.itemsInternal.Count;

        public bool HasPlaceHolders
        {
            get
            {
                Func<object, bool> predicate = <>c.<>9__35_0;
                if (<>c.<>9__35_0 == null)
                {
                    Func<object, bool> local1 = <>c.<>9__35_0;
                    predicate = <>c.<>9__35_0 = fe => fe is PlaceHolder;
                }
                return (this.itemsInternal.FirstOrDefault<object>(predicate) != null);
            }
        }

        internal object this[int index] =>
            this.itemsInternal[index];

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PlaceHolderHelper.<>c <>9 = new PlaceHolderHelper.<>c();
            public static Func<PlaceHolder, bool> <>9__1_0;
            public static Func<PlaceHolder, bool> <>9__1_1;
            public static Action<PlaceHolder> <>9__10_0;
            public static Func<object, bool> <>9__35_0;
            public static Func<object, bool> <>9__43_0;

            internal void <ClearPlaceHolder>b__10_0(PlaceHolder placeHolder)
            {
                LayoutGroup parent = placeHolder.Parent;
                if (parent != null)
                {
                    parent.PlaceHolderHelper.Remove(placeHolder);
                }
            }

            internal bool <get_HasPlaceHolders>b__35_0(object fe) => 
                fe is PlaceHolder;

            internal bool <GetPlaceHolders>b__43_0(object x) => 
                x is PlaceHolder;

            internal bool <PlaceHolderToDock>b__1_0(PlaceHolder d) => 
                !d.IsDocked;

            internal bool <PlaceHolderToDock>b__1_1(PlaceHolder d) => 
                d.IsDocked;
        }
    }
}

