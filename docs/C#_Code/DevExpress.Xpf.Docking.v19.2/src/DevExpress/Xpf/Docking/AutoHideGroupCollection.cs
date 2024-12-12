namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class AutoHideGroupCollection : BaseLockableCollection<AutoHideGroup>, IDisposable
    {
        private readonly CompositeCollection mergedBottomItems;
        private readonly CompositeCollection mergedRightItems;
        private readonly CompositeCollection mergedTopItems;
        private ObservableCollection<AutoHideGroup> bottomItemsCore;
        private bool isDisposing;
        private ObservableCollection<AutoHideGroup> leftItemsCore;
        private CompositeCollection mergedLeftItems;
        private ObservableCollection<AutoHideGroup> rightItemsCore;
        private ObservableCollection<AutoHideGroup> topItemsCore;
        private Dictionary<object, ahCollectionContainer> containers = new Dictionary<object, ahCollectionContainer>();

        public AutoHideGroupCollection()
        {
            this.leftItemsCore = this.CreateTarget();
            this.rightItemsCore = this.CreateTarget();
            this.topItemsCore = this.CreateTarget();
            this.bottomItemsCore = this.CreateTarget();
            this.mergedLeftItems = this.CreateMergedTarget(this.leftItemsCore);
            this.mergedRightItems = this.CreateMergedTarget(this.rightItemsCore);
            this.mergedTopItems = this.CreateMergedTarget(this.topItemsCore);
            this.mergedBottomItems = this.CreateMergedTarget(this.bottomItemsCore);
        }

        public void AddRange(AutoHideGroup[] items)
        {
            Array.ForEach<AutoHideGroup>(items, new Action<AutoHideGroup>(this.Add));
        }

        private static void Clear(ref ObservableCollection<AutoHideGroup> collection)
        {
            if (collection != null)
            {
                collection.Clear();
            }
            collection = null;
        }

        protected override void ClearItems()
        {
            AutoHideGroup[] groupArray = this.ToArray();
            base.ClearItems();
            for (int i = 0; i < groupArray.Length; i++)
            {
                this.OnItemRemoved(groupArray[i]);
            }
        }

        private CompositeCollection CreateMergedTarget(ObservableCollection<AutoHideGroup> items)
        {
            CompositeCollection composites = new CompositeCollection();
            CollectionContainer newItem = new CollectionContainer();
            newItem.Collection = items;
            composites.Add(newItem);
            return composites;
        }

        protected virtual ObservableCollection<AutoHideGroup> CreateTarget() => 
            new ObservableCollection<AutoHideGroup>();

        public void Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                this.OnDisposing();
            }
            GC.SuppressFinalize(this);
        }

        private ahCollectionContainer GetContainer(object key)
        {
            ahCollectionContainer container = null;
            if (!this.containers.TryGetValue(key, out container))
            {
                container = new ahCollectionContainer();
                this.containers[key] = container;
                this.mergedBottomItems.Add(container.GetContainer(Dock.Bottom));
                this.mergedLeftItems.Add(container.GetContainer(Dock.Left));
                this.mergedRightItems.Add(container.GetContainer(Dock.Right));
                this.mergedTopItems.Add(container.GetContainer(Dock.Top));
            }
            return container;
        }

        protected CompositeCollection GetMergedTarget(Dock type)
        {
            CompositeCollection leftItems = this.LeftItems;
            switch (type)
            {
                case Dock.Top:
                    leftItems = this.TopItems;
                    break;

                case Dock.Right:
                    leftItems = this.RightItems;
                    break;

                case Dock.Bottom:
                    leftItems = this.BottomItems;
                    break;

                default:
                    break;
            }
            return leftItems;
        }

        protected ObservableCollection<AutoHideGroup> GetTarget(Dock type)
        {
            ObservableCollection<AutoHideGroup> leftItemsCore = this.leftItemsCore;
            switch (type)
            {
                case Dock.Top:
                    leftItemsCore = this.topItemsCore;
                    break;

                case Dock.Right:
                    leftItemsCore = this.rightItemsCore;
                    break;

                case Dock.Bottom:
                    leftItemsCore = this.bottomItemsCore;
                    break;

                default:
                    break;
            }
            return leftItemsCore;
        }

        internal ObservableCollection<AutoHideGroup> GetXtraTarget(Dock type)
        {
            ObservableCollection<AutoHideGroup> observables = new ObservableCollection<AutoHideGroup>();
            foreach (ahCollectionContainer container in this.containers.Values)
            {
                container.GetItems(type).ForEach<AutoHideGroup>(new Action<AutoHideGroup>(observables.Add));
            }
            return observables;
        }

        protected override void InsertItem(int index, AutoHideGroup item)
        {
            base.InsertItem(index, item);
            this.OnItemAdded(item);
        }

        internal void Merge(object key, AutoHideGroupCollection extraItems)
        {
            extraItems.OnMerge();
            ahCollectionContainer container = this.GetContainer(key);
            container.GetItems(Dock.Bottom).Clear();
            container.GetItems(Dock.Left).Clear();
            container.GetItems(Dock.Right).Clear();
            container.GetItems(Dock.Top).Clear();
            foreach (AutoHideGroup group in extraItems)
            {
                container.GetItems(group.DockType).Add(group);
            }
        }

        protected virtual void NotifyItemAdded(AutoHideGroup item)
        {
            if (item != null)
            {
                if (this.Owner != null)
                {
                    item.Manager ??= this.Owner;
                }
                item.OnOwnerCollectionChanged();
            }
        }

        protected virtual void NotifyItemRemoved(AutoHideGroup item)
        {
            if (item != null)
            {
                item.OnOwnerCollectionChanged();
            }
        }

        protected virtual void OnDisposing()
        {
            this.ClearItems();
            Clear(ref this.leftItemsCore);
            Clear(ref this.rightItemsCore);
            Clear(ref this.topItemsCore);
            Clear(ref this.bottomItemsCore);
        }

        protected virtual void OnItemAdded(AutoHideGroup item)
        {
            this.GetTarget(item.DockType).Add(item);
            this.Unsubscribe(item);
            this.Subscribe(item);
            item.IsRootGroup = true;
            item.DockLayoutManagerCore = this.Owner;
            this.NotifyItemAdded(item);
        }

        private void OnItemDockTypeChanged(object sender, DockTypeChangedEventArgs e)
        {
            AutoHideGroup item = sender as AutoHideGroup;
            this.GetTarget(e.PrevValue).Remove(item);
            this.GetTarget(e.Value).Add(item);
        }

        protected virtual void OnItemRemoved(AutoHideGroup item)
        {
            this.Unsubscribe(item);
            this.GetTarget(item.DockType).Remove(item);
            item.IsRootGroup = false;
            item.DockLayoutManagerCore = null;
            this.NotifyItemRemoved(item);
        }

        private void OnMerge()
        {
            this.LeftItems.Clear();
            this.RightItems.Clear();
            this.TopItems.Clear();
            this.BottomItems.Clear();
        }

        private void OnUnmerge()
        {
            CollectionContainer newItem = new CollectionContainer();
            newItem.Collection = this.leftItemsCore;
            this.mergedLeftItems.Add(newItem);
            CollectionContainer container2 = new CollectionContainer();
            container2.Collection = this.rightItemsCore;
            this.mergedRightItems.Add(container2);
            CollectionContainer container3 = new CollectionContainer();
            container3.Collection = this.topItemsCore;
            this.mergedTopItems.Add(container3);
            CollectionContainer container4 = new CollectionContainer();
            container4.Collection = this.bottomItemsCore;
            this.mergedBottomItems.Add(container4);
        }

        protected override void RemoveItem(int index)
        {
            AutoHideGroup item = base.Items[index];
            base.RemoveItem(index);
            this.OnItemRemoved(item);
        }

        protected override void SetItem(int index, AutoHideGroup item)
        {
            AutoHideGroup group = base[index];
            base.SetItem(index, item);
            this.OnItemRemoved(group);
            this.OnItemAdded(item);
        }

        private void Subscribe(AutoHideGroup item)
        {
            item.DockTypeChanged += new DockTypeChangedEventHandler(this.OnItemDockTypeChanged);
        }

        public AutoHideGroup[] ToArray()
        {
            AutoHideGroup[] array = new AutoHideGroup[base.Count];
            base.Items.CopyTo(array, 0);
            return array;
        }

        internal void Unmerge(object key, AutoHideGroupCollection extraItems)
        {
            ahCollectionContainer container = null;
            if (!this.containers.TryGetValue(key, out container))
            {
                container = new ahCollectionContainer();
                this.containers[key] = container;
            }
            container.GetItems(Dock.Bottom).Clear();
            container.GetItems(Dock.Left).Clear();
            container.GetItems(Dock.Right).Clear();
            container.GetItems(Dock.Top).Clear();
            extraItems.OnUnmerge();
        }

        private void Unsubscribe(AutoHideGroup item)
        {
            item.DockTypeChanged -= new DockTypeChangedEventHandler(this.OnItemDockTypeChanged);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public CompositeCollection BottomItems =>
            this.mergedBottomItems;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public CompositeCollection LeftItems =>
            this.mergedLeftItems;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public CompositeCollection RightItems =>
            this.mergedRightItems;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public CompositeCollection TopItems =>
            this.mergedTopItems;

        internal DockLayoutManager Owner { get; set; }

        public BaseLayoutItem this[string name] =>
            Array.Find<BaseLayoutItem>(this.GetItems(), item => item.Name == name);

        private class ahCollectionContainer
        {
            private CollectionContainer bottomContainer;
            private ObservableCollection<AutoHideGroup> extraBottomItems;
            private ObservableCollection<AutoHideGroup> extraLeftItems;
            private ObservableCollection<AutoHideGroup> extraRightItems;
            private ObservableCollection<AutoHideGroup> extraTopItems;
            private CollectionContainer leftContainer;
            private CollectionContainer rightContainer;
            private CollectionContainer topContainer;

            public ahCollectionContainer()
            {
                this.extraLeftItems = this.CreateTarget();
                this.extraRightItems = this.CreateTarget();
                this.extraTopItems = this.CreateTarget();
                this.extraBottomItems = this.CreateTarget();
                CollectionContainer container1 = new CollectionContainer();
                container1.Collection = this.extraBottomItems;
                this.bottomContainer = container1;
                CollectionContainer container2 = new CollectionContainer();
                container2.Collection = this.extraLeftItems;
                this.leftContainer = container2;
                CollectionContainer container3 = new CollectionContainer();
                container3.Collection = this.extraRightItems;
                this.rightContainer = container3;
                CollectionContainer container4 = new CollectionContainer();
                container4.Collection = this.extraTopItems;
                this.topContainer = container4;
            }

            protected ObservableCollection<AutoHideGroup> CreateTarget() => 
                new ObservableCollection<AutoHideGroup>();

            public CollectionContainer GetContainer(Dock dock)
            {
                CollectionContainer leftContainer = this.leftContainer;
                switch (dock)
                {
                    case Dock.Top:
                        leftContainer = this.topContainer;
                        break;

                    case Dock.Right:
                        leftContainer = this.rightContainer;
                        break;

                    case Dock.Bottom:
                        leftContainer = this.bottomContainer;
                        break;

                    default:
                        break;
                }
                return leftContainer;
            }

            public ObservableCollection<AutoHideGroup> GetItems(Dock dock)
            {
                ObservableCollection<AutoHideGroup> extraLeftItems = this.extraLeftItems;
                switch (dock)
                {
                    case Dock.Top:
                        extraLeftItems = this.extraTopItems;
                        break;

                    case Dock.Right:
                        extraLeftItems = this.extraRightItems;
                        break;

                    case Dock.Bottom:
                        extraLeftItems = this.extraBottomItems;
                        break;

                    default:
                        break;
                }
                return extraLeftItems;
            }
        }
    }
}

