namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class BandsMover
    {
        private readonly DataControlBase grid;
        private readonly bool isInDesignMode;
        private BandsMoverHierarchy hierarchy;
        private IDesignTimeAdornerBase designTimeAdorner;
        private IndexSynchronizer synchronizerCore;

        public BandsMover(DataControlBase grid)
        {
            if (grid == null)
            {
                throw new ArgumentNullException();
            }
            this.grid = grid;
            this.isInDesignMode = DesignerProperties.GetIsInDesignMode(grid);
        }

        private void AfterColumnMove(ColumnNode source, HeaderPresenterType moveFrom)
        {
            if (moveFrom == HeaderPresenterType.ColumnChooser)
            {
                source.Prototype.Visible = true;
            }
            this.designTimeAdorner.SelectModelItem(this.hierarchy.FindNodeModel(source));
        }

        private void ApplyEditingScope(IModelEditingScope scope)
        {
            if (!this.isInDesignMode)
            {
                scope.Complete();
            }
            else
            {
                this.grid.BeginDesignColumnMoverApply();
                try
                {
                    scope.Complete();
                }
                finally
                {
                    this.grid.EndDesignColumnMoverApply();
                }
            }
        }

        private void BeforeColumnMove(ColumnNode source, HeaderPresenterType moveFrom)
        {
            this.grid.BeforeColumnMove(source.Prototype, moveFrom);
        }

        private Func<ColumnNodeCollection, HeaderPresenterType, bool, BandColumnsMoveAdapter> CreateAdapterFactory() => 
            (nds, moveFrom, useLegacyColumnVisibleIndexes) => this.CreateMoveAdapter(nds, moveFrom, useLegacyColumnVisibleIndexes);

        protected virtual IndexSynchronizer CreateIndexSynchronizer() => 
            new IndexSynchronizer();

        private BandColumnsMoveAdapter CreateMoveAdapter(ColumnNodeCollection nodes, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes) => 
            new BandColumnsMoveAdapter(nodes, moveFrom, useLegacyColumnVisibleIndexes) { AllowMoveInsideCollection = this.isInDesignMode };

        private ColumnNode FindNode(BaseColumn column)
        {
            if (column == null)
            {
                return null;
            }
            int index = this.IndexOf(column);
            if (index == -1)
            {
                return null;
            }
            int[] collection = new int[0];
            if (column.ParentBand != null)
            {
                collection = BandWalker.GetIndexes(column.ParentBand);
            }
            IColumnNodeOwner hierarchy = this.hierarchy;
            Stack<int> stack = new Stack<int>(collection);
            while (stack.Count > 0)
            {
                int num2 = stack.Pop();
                hierarchy = hierarchy.BandNodes[num2];
            }
            return this.GetChildren(hierarchy, column.IsBand ? ((BandedCollectionType) 0) : ((BandedCollectionType) 1))[index];
        }

        private ColumnNode FindRowSibling(ColumnNode source) => 
            this.GetChildren(source.Owner, source.CollectionType).FirstOrDefault<ColumnNode>(x => (x.Row == source.Row) && (!ReferenceEquals(x, source) && x.IsVisible));

        private ColumnNodeCollection GetChildren(IColumnNodeOwner owner, BandedCollectionType childrenType)
        {
            if (childrenType == BandedCollectionType.Bands)
            {
                return owner.BandNodes;
            }
            if (childrenType != BandedCollectionType.Columns)
            {
                throw new InvalidOperationException();
            }
            return owner.ColumnNodes;
        }

        private ColumnNode GetFirstChildColumn(ColumnNode band)
        {
            if ((band == null) || (band.ColumnNodes.Count == 0))
            {
                return null;
            }
            ColumnNode node = null;
            foreach (ColumnNode node2 in band.ColumnNodes)
            {
                if (node2.IsVisible)
                {
                    int row = node2.Row;
                    if ((node == null) || ((row < node.Row) || ((row == node.Row) && (node2.VisibleIndex < node.VisibleIndex))))
                    {
                        node = node2;
                    }
                }
            }
            return node;
        }

        private IColumnNodeOwner GetFirstLeafBand(IColumnNodeOwner root)
        {
            Func<ColumnNode, bool> predicate = <>c.<>9__20_0;
            if (<>c.<>9__20_0 == null)
            {
                Func<ColumnNode, bool> local1 = <>c.<>9__20_0;
                predicate = <>c.<>9__20_0 = x => x.IsVisible;
            }
            ColumnNode node = root.BandNodes.FirstOrDefault<ColumnNode>(predicate);
            return ((node != null) ? this.GetFirstLeafBand(node) : root);
        }

        private int IndexOf(BaseColumn column)
        {
            if (column != null)
            {
                if (column is BandBase)
                {
                    return ((column.ParentBand != null) ? column.ParentBand.BandsCore : this.grid.BandsCore).GetCachedIndex((BandBase) column);
                }
                if (column is ColumnBase)
                {
                    ISupportGetCachedIndex<ColumnBase> index2 = null;
                    index2 = (column.ParentBand == null) ? ((ISupportGetCachedIndex<ColumnBase>) this.grid.ColumnsCore) : ((ISupportGetCachedIndex<ColumnBase>) column.ParentBand.ColumnsCore);
                    return index2.GetCachedIndex((ColumnBase) column);
                }
            }
            return -1;
        }

        private void InvalidateHierarchy()
        {
            this.hierarchy = new BandsMoverHierarchy();
            this.hierarchy.AllowModelClear = this.isInDesignMode;
            this.hierarchy.UseColumnClones = this.isInDesignMode;
            this.hierarchy.Init(this.grid);
            this.PrepareHierarchy(this.hierarchy);
        }

        private void MapDragColumns(BaseColumn sourceCol, BaseColumn targetCol, out ColumnNode sourceNode, out ColumnNode targetNode)
        {
            this.InvalidateHierarchy();
            sourceNode = this.FindNode(sourceCol);
            if (sourceNode != null)
            {
                sourceNode.Prototype = sourceCol;
            }
            targetNode = this.FindNode(targetCol);
            if (targetNode != null)
            {
                targetNode.Prototype = targetCol;
            }
        }

        private void Move(ColumnNode source, IBandsMoverDropTarget target, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes)
        {
            if (source != null)
            {
                DataViewBase viewCore = this.grid.viewCore;
                if (viewCore != null)
                {
                    viewCore.BeginUpdateColumnsLayout();
                    viewCore.UpdateVisibleIndexesLocker.Lock();
                    this.designTimeAdorner = this.grid.DesignTimeAdorner;
                    try
                    {
                        using (IModelEditingScope scope = this.designTimeAdorner.DataControlModelItem.BeginEdit("DragDrop"))
                        {
                            this.BeforeColumnMove(source, moveFrom);
                            this.MoveCore(source, target, moveFrom, useLegacyColumnVisibleIndexes);
                            this.AfterColumnMove(source, moveFrom);
                            this.ApplyEditingScope(scope);
                            this.Synchronizer.Process();
                        }
                    }
                    finally
                    {
                        viewCore.UpdateVisibleIndexesLocker.Unlock();
                        viewCore.EndUpdateColumnsLayout();
                    }
                }
            }
        }

        public void MoveBand(BandBase source, BandBase target, BandedViewDropPlace dropPlace, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes)
        {
            if (dropPlace != BandedViewDropPlace.Top)
            {
                ColumnNode node;
                ColumnNode sourceNode;
                this.MapDragColumns(source, target, out sourceNode, out node);
                IBandsMoverDropTarget dropTarget = null;
                if (dropPlace == BandedViewDropPlace.Bottom)
                {
                    if (this.grid.BandsLayoutCore.AllowChangeBandParent || this.isInDesignMode)
                    {
                        BandsMoverDropTarget target1 = new BandsMoverDropTarget(node);
                        target1.NeedTransferChildren = true;
                        dropTarget = target1;
                    }
                }
                else if ((target == null) || !target.IsServiceColumn())
                {
                    AnchorBandsMoverDropTarget target3 = new AnchorBandsMoverDropTarget(node);
                    target3.DropPlace = dropPlace;
                    dropTarget = target3;
                }
                else if (dropPlace == BandedViewDropPlace.Right)
                {
                    BandBase column = this.grid.BandsLayoutCore.VisibleBands.FirstOrDefault<BandBase>(x => !ReferenceEquals(x, target));
                    if (column != null)
                    {
                        ColumnNode anchor = this.FindNode(column);
                        anchor.Prototype = column;
                        AnchorBandsMoverDropTarget target2 = new AnchorBandsMoverDropTarget(anchor);
                        target2.DropPlace = BandedViewDropPlace.Left;
                        dropTarget = target2;
                    }
                }
                if (this.grid == null)
                {
                    this.Move(sourceNode, dropTarget, moveFrom, useLegacyColumnVisibleIndexes);
                }
                else
                {
                    this.grid.BandsSourceSyncLocker.DoLockedAction(() => this.Move(sourceNode, dropTarget, moveFrom, useLegacyColumnVisibleIndexes));
                    foreach (object obj2 in this.grid.BandsCore)
                    {
                        BandBase base3 = obj2 as BandBase;
                        if (base3 != null)
                        {
                            base3.SyncBandsColectionWithSource(true);
                        }
                    }
                }
            }
        }

        public void MoveColumnToBand(ColumnBase source, BandBase target, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes)
        {
            ColumnNode node;
            ColumnNode node2;
            this.MapDragColumns(source, target, out node, out node2);
            IBandsMoverDropTarget target2 = null;
            ColumnNode firstChildColumn = this.GetFirstChildColumn(node2);
            if (firstChildColumn == null)
            {
                target2 = new BandsMoverDropTarget(node2);
            }
            else
            {
                ColumnNode objA = firstChildColumn;
                if (ReferenceEquals(objA, node))
                {
                    objA = this.FindRowSibling(node);
                }
                if (objA != null)
                {
                    AnchorBandsMoverDropTarget target1 = new AnchorBandsMoverDropTarget(objA);
                    target1.DropPlace = BandedViewDropPlace.Top;
                    target2 = target1;
                }
            }
            this.Move(node, target2, moveFrom, useLegacyColumnVisibleIndexes);
        }

        public void MoveColumnToColumn(ColumnBase source, ColumnBase target, BandedViewDropPlace dropPlace, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes)
        {
            ColumnNode node;
            ColumnNode node2;
            this.MapDragColumns(source, target, out node, out node2);
            IBandsMoverDropTarget target2 = null;
            if ((node != null) && (node2 != null))
            {
                ColumnNode anchor = null;
                if (!ReferenceEquals(node, node2))
                {
                    anchor = node2;
                }
                else if ((dropPlace == BandedViewDropPlace.Top) || (dropPlace == BandedViewDropPlace.Bottom))
                {
                    anchor = this.FindRowSibling(node);
                }
                if (anchor != null)
                {
                    AnchorBandsMoverDropTarget target1 = new AnchorBandsMoverDropTarget(anchor);
                    target1.DropPlace = dropPlace;
                    target2 = target1;
                }
            }
            this.Move(node, target2, moveFrom, useLegacyColumnVisibleIndexes);
        }

        private void MoveCore(ColumnNode source, IBandsMoverDropTarget target, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes)
        {
            if ((source != null) && (target != null))
            {
                IColumnNodeOwner nodeOwner = target.GetNodeOwner();
                BandedCollectionType collectionType = source.CollectionType;
                ColumnNodeCollection children = this.GetChildren(source.Owner, collectionType);
                ColumnNodeCollection objB = this.GetChildren(nodeOwner, collectionType);
                Func<ColumnNodeCollection, HeaderPresenterType, bool, BandColumnsMoveAdapter> func = this.CreateAdapterFactory();
                BandColumnsMoveAdapter adapter = func(children, moveFrom, useLegacyColumnVisibleIndexes);
                BandColumnsMoveAdapter adapter2 = ReferenceEquals(children, objB) ? adapter : func(objB, moveFrom, useLegacyColumnVisibleIndexes);
                this.PrepareMoveAdapter(adapter);
                this.PrepareMoveAdapter(adapter2);
                adapter.Remove(source);
                target.Drop(adapter2, source);
                adapter.Apply();
                adapter2.Apply();
                BandColumnsMoveAdapter[] adapters = new BandColumnsMoveAdapter[] { adapter, adapter2 };
                this.UpdateSyncRequests(adapters);
                if (target.NeedTransferChildren)
                {
                    this.TransferBandChildren(nodeOwner, source, moveFrom, useLegacyColumnVisibleIndexes);
                }
            }
        }

        protected virtual void PrepareHierarchy(BandsMoverHierarchy hierarchy)
        {
        }

        protected virtual void PrepareMoveAdapter(BandColumnsMoveAdapter adapter)
        {
        }

        private void TransferBandChildren(IColumnNodeOwner oldOwner, IColumnNodeOwner newOwner, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes)
        {
            if ((oldOwner != null) && (newOwner != null))
            {
                this.TransferBandChildren(oldOwner, newOwner, BandedCollectionType.Bands, moveFrom, useLegacyColumnVisibleIndexes);
                IColumnNodeOwner firstLeafBand = this.GetFirstLeafBand(newOwner);
                this.TransferBandChildren(newOwner, firstLeafBand, BandedCollectionType.Columns, moveFrom, useLegacyColumnVisibleIndexes);
                this.TransferBandChildren(oldOwner, firstLeafBand, BandedCollectionType.Columns, moveFrom, useLegacyColumnVisibleIndexes);
            }
        }

        private void TransferBandChildren(IColumnNodeOwner oldOwner, IColumnNodeOwner newOwner, BandedCollectionType childrenType, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes)
        {
            if (!ReferenceEquals(oldOwner, newOwner))
            {
                ColumnNodeCollection children = this.GetChildren(oldOwner, childrenType);
                ColumnNodeCollection nodes2 = this.GetChildren(newOwner, childrenType);
                Func<ColumnNode, int> keySelector = <>c.<>9__16_0;
                if (<>c.<>9__16_0 == null)
                {
                    Func<ColumnNode, int> local1 = <>c.<>9__16_0;
                    keySelector = <>c.<>9__16_0 = x => x.VisibleIndex;
                }
                ColumnNode[] nodeArray = children.OrderBy<ColumnNode, int>(keySelector).ToArray<ColumnNode>();
                if (nodeArray.Length != 0)
                {
                    Func<ColumnNodeCollection, HeaderPresenterType, bool, BandColumnsMoveAdapter> func = this.CreateAdapterFactory();
                    BandColumnsMoveAdapter adapter = func(children, moveFrom, useLegacyColumnVisibleIndexes);
                    BandColumnsMoveAdapter adapter2 = func(nodes2, moveFrom, useLegacyColumnVisibleIndexes);
                    foreach (ColumnNode node in nodeArray)
                    {
                        if (!ReferenceEquals(node, newOwner))
                        {
                            adapter.Remove(node);
                            adapter2.Add(node);
                        }
                    }
                    adapter.Apply();
                    adapter2.Apply();
                }
            }
        }

        private void UpdateSyncRequests(params BandColumnsMoveAdapter[] adapters)
        {
            foreach (BandColumnsMoveAdapter adapter in adapters.Distinct<BandColumnsMoveAdapter>())
            {
                this.Synchronizer.EnqueueSyncRequest(adapter);
            }
        }

        private IndexSynchronizer Synchronizer
        {
            get
            {
                this.synchronizerCore ??= this.CreateIndexSynchronizer();
                return this.synchronizerCore;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BandsMover.<>c <>9 = new BandsMover.<>c();
            public static Func<ColumnNode, int> <>9__16_0;
            public static Func<ColumnNode, bool> <>9__20_0;

            internal bool <GetFirstLeafBand>b__20_0(ColumnNode x) => 
                x.IsVisible;

            internal int <TransferBandChildren>b__16_0(ColumnNode x) => 
                x.VisibleIndex;
        }
    }
}

