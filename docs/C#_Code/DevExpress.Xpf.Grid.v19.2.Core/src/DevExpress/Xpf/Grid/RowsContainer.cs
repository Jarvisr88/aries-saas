namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Hierarchy;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class RowsContainer : DependencyObject, IItemsContainer
    {
        protected static readonly DependencyPropertyKey ItemsPropertyKey;
        public static readonly DependencyProperty ItemsProperty;
        protected static readonly DependencyPropertyKey RowsLocationPropertyKey;
        public static readonly DependencyProperty AnimationProgressProperty;
        private RowDataBase ownerRowData;

        static RowsContainer()
        {
            Type ownerType = typeof(RowsContainer);
            ItemsPropertyKey = DependencyPropertyManager.RegisterReadOnly("Items", typeof(ObservableCollection<RowDataBase>), ownerType, new PropertyMetadata(null));
            ItemsProperty = ItemsPropertyKey.DependencyProperty;
            AnimationProgressProperty = DependencyPropertyManager.Register("AnimationProgress", typeof(double), ownerType, new PropertyMetadata(0.0, (d, e) => ((RowsContainer) d).OnAnimationProgressChanged()));
        }

        protected RowsContainer()
        {
        }

        internal virtual bool BaseSyncronize(NodeContainer nodeContainer) => 
            false;

        internal void Clear()
        {
            foreach (RowDataBase base2 in this.Items)
            {
                HierarchyPanel.DetachItem(base2);
                if (base2.RowsContainer != null)
                {
                    base2.RowsContainer.Clear();
                }
            }
            this.Items.Clear();
        }

        internal virtual RowsContainerSyncronizerBase CreateRowsContainerSyncronizer() => 
            new RowsContainerSyncronizer(this);

        private static FreeRowDataInfo FindRowDataInfo(LinkedList<FreeRowDataInfo> list, RowDataBase rowData)
        {
            FreeRowDataInfo info2;
            using (LinkedList<FreeRowDataInfo>.Enumerator enumerator = list.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        FreeRowDataInfo current = enumerator.Current;
                        if (!ReferenceEquals(current.RowData, rowData))
                        {
                            continue;
                        }
                        info2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return info2;
        }

        internal virtual Guid GetCacheVersion() => 
            Guid.Empty;

        [IteratorStateMachine(typeof(<GetEnumerable>d__32))]
        internal IEnumerable GetEnumerable()
        {
            <GetEnumerable>d__32 d__1 = new <GetEnumerable>d__32(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        internal void InitItemsCollection()
        {
            this.Items = new RowsContainerItemsCollection(this, this.GetCacheVersion());
        }

        private void OnAnimationProgressChanged()
        {
            this.MasterRootRowsContainer.RaiseHierarchyChanged(HierarchyChangedEventArgs.Default);
        }

        public void RaiseItemsRemoved(IEnumerable items)
        {
            if (items != null)
            {
                foreach (IItem item in items)
                {
                    this.MasterRootRowsContainer.RaiseHierarchyChanged(new HierarchyChangedEventArgs(HierarchyChangeType.ItemRemoved, item));
                    this.RaiseItemsRemoved(item.ItemsContainer.Items);
                }
            }
        }

        internal void SetOwnerRowData(RowDataBase value)
        {
            if (!ReferenceEquals(this.ownerRowData, value))
            {
                if ((this.ownerRowData != null) && ReferenceEquals(this.ownerRowData.RowsContainer, this))
                {
                    this.ownerRowData.RowsContainer = null;
                }
                this.ownerRowData = value;
            }
        }

        internal virtual void StoreFreeData()
        {
            if (this.Items != null)
            {
                foreach (RowDataBase base2 in this.Items)
                {
                    base2.StoreAsFreeData(this);
                }
            }
        }

        public void StoreFreeRowData(RowNode node, RowDataBase rowData)
        {
            this.VerifyVisualRootTreeBuilder();
            FreeRowDataInfo info = FindRowDataInfo(node.GetFreeRowDataQueue(this.SynchronizationQueues), rowData);
            if (info != null)
            {
                info.DataContainer = this;
            }
            else
            {
                node.GetFreeRowDataQueue(this.SynchronizationQueues).AddLast(new FreeRowDataInfo(this, rowData));
            }
        }

        internal virtual void Synchronize(NodeContainer nodeContainer)
        {
            this.MasterRootRowsContainer.TreeBuilder.Synchronize(this, nodeContainer);
        }

        public void UnstoreFreeRowData(RowNode node, RowDataBase rowData)
        {
            this.VerifyVisualRootTreeBuilder();
            FreeRowDataInfo info = FindRowDataInfo(node.GetFreeRowDataQueue(this.SynchronizationQueues), rowData);
            if (info != null)
            {
                node.GetFreeRowDataQueue(this.SynchronizationQueues).Remove(info);
            }
        }

        private void VerifyVisualRootTreeBuilder()
        {
            if (!(this.MasterRootRowsContainer.TreeBuilder is VisualDataTreeBuilder))
            {
                throw new InvalidOperationException("This method is valid only when used inside VisualTreeBuilder");
            }
        }

        internal abstract MasterRowsContainer MasterRootRowsContainer { get; }

        internal abstract DevExpress.Xpf.Grid.Native.SynchronizationQueues SynchronizationQueues { get; }

        public double AnimationProgress
        {
            get => 
                (double) base.GetValue(AnimationProgressProperty);
            set => 
                base.SetValue(AnimationProgressProperty, value);
        }

        public ObservableCollection<RowDataBase> Items
        {
            get => 
                (ObservableCollection<RowDataBase>) base.GetValue(ItemsProperty);
            internal set => 
                base.SetValue(ItemsPropertyKey, value);
        }

        IList<IItem> IItemsContainer.Items
        {
            get
            {
                Func<RowDataBase, IItem> cast = <>c.<>9__34_0;
                if (<>c.<>9__34_0 == null)
                {
                    Func<RowDataBase, IItem> local1 = <>c.<>9__34_0;
                    cast = <>c.<>9__34_0 = rowData => rowData;
                }
                return new SimpleBridgeList<IItem, RowDataBase>(this.Items, cast, null);
            }
        }

        Size IItemsContainer.DesiredSize { get; set; }

        Size IItemsContainer.RenderSize { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RowsContainer.<>c <>9 = new RowsContainer.<>c();
            public static Func<RowDataBase, IItem> <>9__34_0;

            internal void <.cctor>b__4_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((RowsContainer) d).OnAnimationProgressChanged();
            }

            internal IItem <DevExpress.Xpf.Grid.Hierarchy.IItemsContainer.get_Items>b__34_0(RowDataBase rowData) => 
                rowData;
        }

        [CompilerGenerated]
        private sealed class <GetEnumerable>d__32 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            public RowsContainer <>4__this;
            private IEnumerator<RowDataBase> <>7__wrap1;
            private List<LinkedList<FreeRowDataInfo>>.Enumerator <>7__wrap2;
            private LinkedList<FreeRowDataInfo>.Enumerator <>7__wrap3;

            [DebuggerHidden]
            public <GetEnumerable>d__32(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private void <>m__Finally2()
            {
                this.<>1__state = -1;
                this.<>7__wrap2.Dispose();
            }

            private void <>m__Finally3()
            {
                this.<>1__state = -4;
                this.<>7__wrap3.Dispose();
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    switch (this.<>1__state)
                    {
                        case 0:
                            this.<>1__state = -1;
                            this.<>7__wrap1 = this.<>4__this.Items.GetEnumerator();
                            this.<>1__state = -3;
                            break;

                        case 1:
                            this.<>1__state = -3;
                            break;

                        case 2:
                            this.<>1__state = -5;
                            goto TR_0006;

                        default:
                            return false;
                    }
                    if (this.<>7__wrap1.MoveNext())
                    {
                        RowDataBase current = this.<>7__wrap1.Current;
                        this.<>2__current = current;
                        this.<>1__state = 1;
                        return true;
                    }
                    else
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        this.<>7__wrap2 = this.<>4__this.SynchronizationQueues.AllFreeQueues.GetEnumerator();
                        this.<>1__state = -4;
                    }
                    goto TR_0009;
                TR_0006:
                    if (this.<>7__wrap3.MoveNext())
                    {
                        FreeRowDataInfo current = this.<>7__wrap3.Current;
                        this.<>2__current = current.RowData;
                        this.<>1__state = 2;
                        flag = true;
                    }
                    else
                    {
                        this.<>m__Finally3();
                        this.<>7__wrap3 = new LinkedList<FreeRowDataInfo>.Enumerator();
                        goto TR_0009;
                    }
                    return flag;
                TR_0009:
                    while (true)
                    {
                        if (this.<>7__wrap2.MoveNext())
                        {
                            this.<>7__wrap3 = this.<>7__wrap2.Current.GetEnumerator();
                            this.<>1__state = -5;
                        }
                        else
                        {
                            this.<>m__Finally2();
                            this.<>7__wrap2 = new List<LinkedList<FreeRowDataInfo>>.Enumerator();
                            return false;
                        }
                        break;
                    }
                    goto TR_0006;
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                RowsContainer.<GetEnumerable>d__32 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new RowsContainer.<GetEnumerable>d__32(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                switch (num)
                {
                    case -5:
                    case -4:
                    case 2:
                        try
                        {
                            if ((num == -5) || (num == 2))
                            {
                                try
                                {
                                }
                                finally
                                {
                                    this.<>m__Finally3();
                                }
                            }
                        }
                        finally
                        {
                            this.<>m__Finally2();
                        }
                        break;

                    case -3:
                    case 1:
                        try
                        {
                        }
                        finally
                        {
                            this.<>m__Finally1();
                        }
                        break;

                    case -2:
                    case -1:
                    case 0:
                        break;

                    default:
                        return;
                }
            }

            object IEnumerator<object>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        private class RowsContainerItemsCollection : VersionedObservableCollection<RowDataBase>
        {
            private readonly RowsContainer dataContainerItem;

            public RowsContainerItemsCollection(RowsContainer dataContainerItem, Guid cacheVersion) : base(cacheVersion)
            {
                this.dataContainerItem = dataContainerItem;
            }

            protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
            {
                base.OnCollectionChanged(e);
                HierarchyChangedEventArgs eventArgs = (e.Action != NotifyCollectionChangedAction.Remove) ? HierarchyChangedEventArgs.Default : new HierarchyChangedEventArgs(HierarchyChangeType.ItemRemoved, (IItem) e.OldItems[0]);
                this.dataContainerItem.MasterRootRowsContainer.RaiseHierarchyChanged(eventArgs);
            }
        }
    }
}

