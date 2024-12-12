namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class BandsMoverHierarchy : IColumnNodeOwner
    {
        private const int RootCollectionIndex = -1;
        private const string BandsPropertyName = "Bands";
        private const string ColumnsPropertyName = "Columns";
        private ColumnNodeCollection bandNodes;
        private ColumnNodeCollection columnNodes;
        private Locker initLocker = new Locker();
        private IDesignTimeAdornerBase adorner;
        private DataControlBase grid;
        private ColumnNodeCollectionFactory collectionFactoryCore;
        private NodeModelCache modelBufferCore = new NodeModelCache();

        public BandsMoverHierarchy()
        {
            this.ResetNodes();
        }

        private void AddNodeToBuffer(ColumnNode node, IModelItem model)
        {
            IModelItem original = model;
            if (this.UseColumnClones && (node.CollectionType == BandedCollectionType.Columns))
            {
                original = new ModelItemCloner().Clone(original);
            }
            if (original != null)
            {
                this.ModelBuffer.Add(node, original);
            }
        }

        private ColumnNodeCollectionFactory CreateDefaultCollectionFactory() => 
            new ColumnNodeCollectionFactory();

        BandsMoverHierarchy IColumnNodeOwner.GetRoot() => 
            this;

        void IColumnNodeOwner.OnNodeChanging(NodeChangingInfo change)
        {
            if (change != null)
            {
                IModelItem item = this.FindNodeModel(change.Node);
                if ((item != null) && (item.Properties != null))
                {
                    DXPropertyIdentifier propertyID = change.PropertyID;
                    IModelProperty property = item.Properties[change.PropertyID];
                    if (property != null)
                    {
                        if (this.AllowModelClear && Equals(change.NewValue, change.DefaultValue))
                        {
                            property.ClearValue();
                        }
                        else
                        {
                            property.SetValue(change.NewValue);
                        }
                    }
                }
            }
        }

        void IColumnNodeOwner.OnNodeCollectionChanged(ColumnNodeCollection source, NotifyCollectionChangedEventArgs e)
        {
            IModelItemCollection items = this.FindCollectionModel(source);
            if (items != null)
            {
                if (e.OldItems != null)
                {
                    foreach (ColumnNode node in e.OldItems)
                    {
                        int collectionIndex = node.CollectionIndex;
                        IModelItem model = items[collectionIndex];
                        this.AddNodeToBuffer(node, model);
                        items.RemoveAt(collectionIndex);
                    }
                }
                if (e.NewItems != null)
                {
                    foreach (ColumnNode node2 in e.NewItems)
                    {
                        int collectionIndex = node2.CollectionIndex;
                        IModelItem item2 = null;
                        if (this.ModelBuffer.TryGetValue(node2, out item2))
                        {
                            items.Insert(collectionIndex, item2);
                            continue;
                        }
                        if (node2.Prototype != null)
                        {
                            items.Insert(collectionIndex, node2.Prototype);
                        }
                    }
                }
            }
        }

        public virtual IModelItemCollection FindCollectionModel(ColumnNodeCollection nodes)
        {
            if ((this.adorner == null) || ((this.grid == null) || (nodes == null)))
            {
                return null;
            }
            IModelItem dataControlModelItem = this.adorner.DataControlModelItem;
            if (dataControlModelItem == null)
            {
                return null;
            }
            Tuple<Stack<int>, BandedCollectionType> nodePath = this.GetNodePath(nodes);
            return ((nodePath != null) ? this.FindCollectionModel(dataControlModelItem, nodePath.Item1, nodePath.Item2) : null);
        }

        private IModelItemCollection FindCollectionModel(IModelItem root, Stack<int> indexStack, BandedCollectionType collectionType)
        {
            IModelItemCollection modelCollection = this.GetModelCollection(root, (indexStack.Count == 0) ? collectionType : BandedCollectionType.Bands);
            return (((indexStack.Count == 0) || (modelCollection == null)) ? modelCollection : this.FindCollectionModel(modelCollection[indexStack.Pop()], indexStack, collectionType));
        }

        public virtual IList<BaseColumn> FindColumns(ColumnNodeCollection nodes)
        {
            if ((this.grid == null) || (nodes == null))
            {
                return null;
            }
            Tuple<Stack<int>, BandedCollectionType> nodePath = this.GetNodePath(nodes);
            if (nodePath == null)
            {
                return null;
            }
            BandedCollectionType collectionType = nodePath.Item2;
            Stack<int> indexStack = nodePath.Item1;
            Func<IList> collectionAccessor = null;
            collectionAccessor = ((collectionType == BandedCollectionType.Bands) || (indexStack.Count > 0)) ? ((Func<IList>) (() => this.grid.BandsCore)) : ((Func<IList>) (() => this.grid.ColumnsCore));
            return this.FindColumns(collectionAccessor, indexStack, collectionType);
        }

        private IList<BaseColumn> FindColumns(Func<IList> collectionAccessor, Stack<int> indexStack, BandedCollectionType collectionType)
        {
            IList source = collectionAccessor();
            if (source != null)
            {
                if (indexStack.Count == 0)
                {
                    return source.Cast<BaseColumn>().ToList<BaseColumn>();
                }
                object obj2 = source[indexStack.Pop()];
                if ((collectionType != BandedCollectionType.Bands) && (indexStack.Count <= 0))
                {
                    BandBase band = obj2 as BandBase;
                    if (band != null)
                    {
                        return this.FindColumns(() => band.ColumnsCore, indexStack, collectionType);
                    }
                }
                else
                {
                    IBandsOwner bandsOwner = obj2 as IBandsOwner;
                    if (bandsOwner != null)
                    {
                        return this.FindColumns(() => bandsOwner.BandsCore, indexStack, collectionType);
                    }
                }
            }
            return null;
        }

        public virtual IModelItem FindNodeModel(ColumnNode node)
        {
            if ((this.adorner == null) || ((this.grid == null) || ((node == null) || (node.Owner == null))))
            {
                return null;
            }
            ColumnNodeCollection collection = GetCollection(node.Owner, node.CollectionType);
            IModelItemCollection items = this.FindCollectionModel(collection);
            return items?[node.CollectionIndex];
        }

        private static ColumnNodeCollection GetCollection(IColumnNodeOwner owner, BandedCollectionType collectionType)
        {
            if (collectionType == BandedCollectionType.Bands)
            {
                return owner.BandNodes;
            }
            if (collectionType != BandedCollectionType.Columns)
            {
                throw new InvalidOperationException();
            }
            return owner.ColumnNodes;
        }

        private string GetCollectionName(BandedCollectionType collectionType)
        {
            if (collectionType == BandedCollectionType.Bands)
            {
                return "Bands";
            }
            if (collectionType != BandedCollectionType.Columns)
            {
                throw new InvalidOperationException();
            }
            return "Columns";
        }

        private IModelItemCollection GetModelCollection(IModelItem parent, BandedCollectionType collectionType)
        {
            string collectionName = this.GetCollectionName(collectionType);
            Func<IModelItem, IModelPropertyCollection> evaluator = <>c.<>9__37_0;
            if (<>c.<>9__37_0 == null)
            {
                Func<IModelItem, IModelPropertyCollection> local1 = <>c.<>9__37_0;
                evaluator = <>c.<>9__37_0 = p => p.Properties;
            }
            Func<IModelProperty, IModelItemCollection> func2 = <>c.<>9__37_2;
            if (<>c.<>9__37_2 == null)
            {
                Func<IModelProperty, IModelItemCollection> local2 = <>c.<>9__37_2;
                func2 = <>c.<>9__37_2 = y => y.Collection;
            }
            return parent.With<IModelItem, IModelPropertyCollection>(evaluator).With<IModelPropertyCollection, IModelProperty>(x => x[collectionName]).With<IModelProperty, IModelItemCollection>(func2);
        }

        private Tuple<Stack<int>, BandedCollectionType> GetNodePath(ColumnNodeCollection nodes)
        {
            Stack<int> stack = new Stack<int>();
            IColumnNodeOwner objA = nodes.Owner;
            BandedCollectionType type = ReferenceEquals(nodes, objA.ColumnNodes) ? BandedCollectionType.Columns : BandedCollectionType.Bands;
            while (!ReferenceEquals(objA, this) && (objA != null))
            {
                stack.Push(objA.CollectionIndex);
                objA = objA.Owner;
            }
            return ((objA != null) ? new Tuple<Stack<int>, BandedCollectionType>(stack, type) : null);
        }

        public void Init(DataControlBase grid)
        {
            this.adorner = grid.DesignTimeAdorner;
            this.grid = grid;
            this.ResetNodes();
            IList bands = grid.BandsCore;
            IList columns = null;
            columns = (bands.Count <= 0) ? ((IList) grid.ColumnsCore) : ((IList) new ColumnBase[0]);
            this.initLocker.DoLockedAction(() => this.PopulateNodes(bands, columns, this));
        }

        private bool IsColumnActualVisible(BaseColumn column)
        {
            if (!column.Visible)
            {
                return false;
            }
            Func<bool> fallback = <>c.<>9__31_1;
            if (<>c.<>9__31_1 == null)
            {
                Func<bool> local1 = <>c.<>9__31_1;
                fallback = <>c.<>9__31_1 = () => true;
            }
            return this.grid.viewCore.Return<DataViewBase, bool>(x => x.IsColumnVisibleInHeaders(column), fallback);
        }

        private void OnBandNodeCreated(BandBase band, ColumnNode node)
        {
            this.PopulateNodes(band.BandsCore, band.ColumnsCore, node);
        }

        private void OnColumnNodeCreated(ColumnBase column, ColumnNode node)
        {
            node.Row = BandBase.GetGridRow(column);
        }

        private void PopulateNodes(IList bands, IList columns, IColumnNodeOwner owner)
        {
            this.PopulateNodes<BandBase>(bands, BandedCollectionType.Bands, owner, new Action<BandBase, ColumnNode>(this.OnBandNodeCreated));
            this.PopulateNodes<ColumnBase>(columns, BandedCollectionType.Columns, owner, new Action<ColumnBase, ColumnNode>(this.OnColumnNodeCreated));
        }

        private void PopulateNodes<T>(IList source, BandedCollectionType collectionType, IColumnNodeOwner owner, Action<T, ColumnNode> onNodeCreated = null) where T: BaseColumn
        {
            ColumnNodeCollection collection = GetCollection(owner, collectionType);
            if (source.Count != 0)
            {
                collection.BeginUpdate();
                for (int i = 0; i < source.Count; i++)
                {
                    T column = (T) source[i];
                    ColumnNode item = new ColumnNode(collectionType, this.CollectionFactory) {
                        Owner = owner,
                        VisibleIndex = column.VisibleIndex,
                        IsVisible = this.IsColumnActualVisible(column),
                        CollectionIndex = i
                    };
                    collection.Add(item);
                    if (onNodeCreated != null)
                    {
                        onNodeCreated(column, item);
                    }
                }
                collection.EndUpdate();
            }
        }

        private void ResetNodes()
        {
            Action<ColumnNodeCollection> action = <>c.<>9__26_0;
            if (<>c.<>9__26_0 == null)
            {
                Action<ColumnNodeCollection> local1 = <>c.<>9__26_0;
                action = <>c.<>9__26_0 = x => x.Owner = null;
            }
            this.bandNodes.Do<ColumnNodeCollection>(action);
            Action<ColumnNodeCollection> action2 = <>c.<>9__26_1;
            if (<>c.<>9__26_1 == null)
            {
                Action<ColumnNodeCollection> local2 = <>c.<>9__26_1;
                action2 = <>c.<>9__26_1 = x => x.Owner = null;
            }
            this.columnNodes.Do<ColumnNodeCollection>(action2);
            this.bandNodes = this.CollectionFactory.CreateBandCollection(this);
            this.columnNodes = this.CollectionFactory.CreateColumnCollection(this);
        }

        public ColumnNodeCollectionFactory CollectionFactory
        {
            get
            {
                this.collectionFactoryCore ??= this.CreateDefaultCollectionFactory();
                return this.collectionFactoryCore;
            }
            set => 
                this.collectionFactoryCore = value;
        }

        public bool AllowModelClear { get; set; }

        public bool UseColumnClones { get; set; }

        public NodeModelCache ModelBuffer =>
            this.modelBufferCore;

        public ColumnNodeCollection BandNodes =>
            this.bandNodes;

        public ColumnNodeCollection ColumnNodes =>
            this.columnNodes;

        bool IColumnNodeOwner.IsInitializing =>
            this.initLocker.IsLocked;

        int IColumnNodeOwner.CollectionIndex =>
            -1;

        IColumnNodeOwner IColumnNodeOwner.Owner =>
            null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BandsMoverHierarchy.<>c <>9 = new BandsMoverHierarchy.<>c();
            public static Action<ColumnNodeCollection> <>9__26_0;
            public static Action<ColumnNodeCollection> <>9__26_1;
            public static Func<bool> <>9__31_1;
            public static Func<IModelItem, IModelPropertyCollection> <>9__37_0;
            public static Func<IModelProperty, IModelItemCollection> <>9__37_2;

            internal IModelPropertyCollection <GetModelCollection>b__37_0(IModelItem p) => 
                p.Properties;

            internal IModelItemCollection <GetModelCollection>b__37_2(IModelProperty y) => 
                y.Collection;

            internal bool <IsColumnActualVisible>b__31_1() => 
                true;

            internal void <ResetNodes>b__26_0(ColumnNodeCollection x)
            {
                x.Owner = null;
            }

            internal void <ResetNodes>b__26_1(ColumnNodeCollection x)
            {
                x.Owner = null;
            }
        }
    }
}

