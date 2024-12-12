namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    internal class ColumnNode : IColumnNodeOwner
    {
        private readonly BandedCollectionType collectionTypeCore;
        private ColumnNodeCollection bandNodes;
        private ColumnNodeCollection columnNodes;
        private DXPropertyIdentifier visibleIndexPropertyId;
        private DXPropertyIdentifier rowPropertyId;
        private int visibleIndexCore;
        private int rowCore;
        private bool forceSetModelProperties;
        private bool isVisibleCore;

        public ColumnNode(BandedCollectionType collectionType) : this(collectionType, null)
        {
        }

        public ColumnNode(BandedCollectionType collectionType, ColumnNodeCollectionFactory collectionFactory)
        {
            this.visibleIndexPropertyId = new DXPropertyIdentifier(BaseColumn.VisibleIndexProperty.OwnerType, BaseColumn.VisibleIndexProperty.Name);
            this.rowPropertyId = new DXPropertyIdentifier(BandBase.GridRowProperty.OwnerType, BandBase.GridRowProperty.Name);
            this.isVisibleCore = true;
            this.collectionTypeCore = collectionType;
            collectionFactory ??= this.CreateDefaultCollectionFactory();
            this.bandNodes = collectionFactory.CreateBandCollection(this);
            this.columnNodes = collectionFactory.CreateColumnCollection(this);
        }

        private ColumnNodeCollectionFactory CreateDefaultCollectionFactory() => 
            new ColumnNodeCollectionFactory();

        BandsMoverHierarchy IColumnNodeOwner.GetRoot()
        {
            Func<IColumnNodeOwner, BandsMoverHierarchy> evaluator = <>c.<>9__48_0;
            if (<>c.<>9__48_0 == null)
            {
                Func<IColumnNodeOwner, BandsMoverHierarchy> local1 = <>c.<>9__48_0;
                evaluator = <>c.<>9__48_0 = x => x.GetRoot();
            }
            return this.Owner.With<IColumnNodeOwner, BandsMoverHierarchy>(evaluator);
        }

        void IColumnNodeOwner.OnNodeChanging(NodeChangingInfo change)
        {
            this.OnNodeChanging(change);
        }

        void IColumnNodeOwner.OnNodeCollectionChanged(ColumnNodeCollection source, NotifyCollectionChangedEventArgs e)
        {
            this.Owner.Do<IColumnNodeOwner>(o => o.OnNodeCollectionChanged(source, e));
        }

        public void ForceSet(Action<ColumnNode> setAction)
        {
            if (setAction != null)
            {
                this.forceSetModelProperties = true;
                try
                {
                    setAction(this);
                }
                finally
                {
                    this.forceSetModelProperties = false;
                }
            }
        }

        private void OnModelPropertyChanging(object oldValue, object newValue, object defaultValue, DXPropertyIdentifier propId)
        {
            if (!this.IsInitializing && (!Equals(oldValue, newValue) || this.forceSetModelProperties))
            {
                NodeChangingInfo change = new NodeChangingInfo();
                change.Node = this;
                change.NewValue = newValue;
                change.OldValue = oldValue;
                change.DefaultValue = defaultValue;
                change.PropertyID = propId;
                this.OnNodeChanging(change);
            }
        }

        private void OnNodeChanging(NodeChangingInfo change)
        {
            this.Owner.Do<IColumnNodeOwner>(o => o.OnNodeChanging(change));
        }

        public BandedCollectionType CollectionType =>
            this.collectionTypeCore;

        private bool IsInitializing
        {
            get
            {
                Func<IColumnNodeOwner, bool> evaluator = <>c.<>9__8_0;
                if (<>c.<>9__8_0 == null)
                {
                    Func<IColumnNodeOwner, bool> local1 = <>c.<>9__8_0;
                    evaluator = <>c.<>9__8_0 = o => o.IsInitializing;
                }
                return this.Owner.Return<IColumnNodeOwner, bool>(evaluator, (<>c.<>9__8_1 ??= () => false));
            }
        }

        public int VisibleIndex
        {
            get => 
                this.visibleIndexCore;
            set
            {
                this.OnModelPropertyChanging(this.visibleIndexCore, value, -1, this.visibleIndexPropertyId);
                this.visibleIndexCore = value;
            }
        }

        public int Row
        {
            get => 
                this.rowCore;
            set
            {
                this.OnModelPropertyChanging(this.rowCore, value, 0, this.rowPropertyId);
                this.rowCore = value;
            }
        }

        public int CollectionIndex { get; set; }

        public BaseColumn Prototype { get; set; }

        public bool IsVisible
        {
            get => 
                this.isVisibleCore;
            set => 
                this.isVisibleCore = value;
        }

        public IColumnNodeOwner Owner { get; set; }

        public ColumnNodeCollection BandNodes =>
            this.bandNodes;

        public ColumnNodeCollection ColumnNodes =>
            this.columnNodes;

        bool IColumnNodeOwner.IsInitializing =>
            this.IsInitializing;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ColumnNode.<>c <>9 = new ColumnNode.<>c();
            public static Func<IColumnNodeOwner, bool> <>9__8_0;
            public static Func<bool> <>9__8_1;
            public static Func<IColumnNodeOwner, BandsMoverHierarchy> <>9__48_0;

            internal BandsMoverHierarchy <DevExpress.Xpf.Grid.IColumnNodeOwner.GetRoot>b__48_0(IColumnNodeOwner x) => 
                x.GetRoot();

            internal bool <get_IsInitializing>b__8_0(IColumnNodeOwner o) => 
                o.IsInitializing;

            internal bool <get_IsInitializing>b__8_1() => 
                false;
        }
    }
}

