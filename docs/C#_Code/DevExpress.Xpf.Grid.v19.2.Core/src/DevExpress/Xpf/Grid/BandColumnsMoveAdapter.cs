namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class BandColumnsMoveAdapter
    {
        private readonly Dictionary<ColumnNode, Position> positionCache;
        private readonly ColumnNodeCollection columnNodes;
        private CollectionModificationListener<MoveAdapterInsertInfo, ColumnNode> modificationListener;
        private bool hasExplicitVisibleIndexes;
        private bool allowMoveInsideCollectionCore;
        private HeaderPresenterType MoveFrom;
        private bool useLegacyColumnVisibleIndexes;

        public BandColumnsMoveAdapter(ColumnNodeCollection columnNodes, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes)
        {
            Func<MoveAdapterInsertInfo, ColumnNode> mapInsertion = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Func<MoveAdapterInsertInfo, ColumnNode> local1 = <>c.<>9__19_0;
                mapInsertion = <>c.<>9__19_0 = x => x.Source;
            }
            this.modificationListener = new CollectionModificationListener<MoveAdapterInsertInfo, ColumnNode>(mapInsertion);
            if (columnNodes == null)
            {
                throw new ArgumentNullException();
            }
            this.columnNodes = columnNodes;
            this.MoveFrom = moveFrom;
            this.useLegacyColumnVisibleIndexes = useLegacyColumnVisibleIndexes;
            this.positionCache = new Dictionary<ColumnNode, Position>();
            this.PopulatePositionCache();
        }

        public void Add(ColumnNode column)
        {
            if ((column != null) && !this.IsInCollection(column))
            {
                this.CollectionInsert(column, null, BandedViewDropPlace.None);
                this.PositionAdd(column);
            }
        }

        public void Apply()
        {
            this.ApplyCollectionModifications();
            this.ApplyPositionCache();
        }

        private void ApplyCollectionModifications()
        {
            foreach (ColumnNode node in this.modificationListener.RemovedItems)
            {
                node.VisibleIndex = -1;
                this.columnNodes.Remove(node);
            }
            foreach (MoveAdapterInsertInfo info in this.modificationListener.AddedItems)
            {
                ColumnNode source = info.Source;
                int collectionIndex = this.GetCachedPosition(source).CollectionIndex;
                this.columnNodes.Insert(collectionIndex, source);
            }
            this.modificationListener.Reset();
        }

        private void ApplyPositionCache()
        {
            foreach (ColumnNode node in this.columnNodes)
            {
                Position position = this.GetCachedPosition(node);
                if (position != null)
                {
                    node.Row = position.Row;
                    if (this.IndexedMode)
                    {
                        node.ForceSet(n => n.VisibleIndex = position.VisibleIndex);
                    }
                }
            }
        }

        private void CachePosition(ColumnNode node, int visibleIndex, int collectionIndex, int row)
        {
            this.positionCache[node] = new Position(visibleIndex, collectionIndex, row);
        }

        private int CalcInsertIndex(Position targetPosition, BandedViewDropPlace mode, Func<Position, int> indexSelector)
        {
            switch (mode)
            {
                case BandedViewDropPlace.Left:
                    return indexSelector(targetPosition);

                case BandedViewDropPlace.Top:
                    return this.GetPositions(targetPosition.Row).Min<Position>(indexSelector);

                case BandedViewDropPlace.Right:
                    return (indexSelector(targetPosition) + 1);

                case BandedViewDropPlace.Bottom:
                    return (this.GetPositions(targetPosition.Row).Max<Position>(indexSelector) + 1);
            }
            throw new InvalidOperationException();
        }

        private int CalcInsertRow(int targetRow, BandedViewDropPlace mode)
        {
            int num = targetRow;
            if (mode == BandedViewDropPlace.Bottom)
            {
                num++;
            }
            return num;
        }

        private void CollectionInsert(ColumnNode source, ColumnNode target, BandedViewDropPlace mode)
        {
            this.modificationListener.Add(new MoveAdapterInsertInfo(source, mode));
        }

        private void CollectionRemove(ColumnNode column)
        {
            this.modificationListener.Remove(column);
        }

        private bool GetAllowShift(ColumnNode column) => 
            this.useLegacyColumnVisibleIndexes ? (column.IsVisible && (this.MoveFrom != HeaderPresenterType.ColumnChooser)) : true;

        private Position GetCachedPosition(ColumnNode node)
        {
            Position position = null;
            this.positionCache.TryGetValue(node, out position);
            return position;
        }

        private IEnumerable<Position> GetPositions(int row) => 
            from x in this.positionCache.Values
                where x.Row == row
                select x;

        public void Insert(ColumnNode source, ColumnNode target, BandedViewDropPlace mode)
        {
            if ((source != null) && ((target != null) && (!this.IsInCollection(source) && this.IsInCollection(target))))
            {
                this.CollectionInsert(source, target, mode);
                this.PositionInsert(source, target, mode);
            }
        }

        private bool IsInCollection(ColumnNode node) => 
            this.positionCache.ContainsKey(node);

        private void OnAllowMoveInsideCollectionChanged()
        {
            this.modificationListener.EnableMinimization = this.IndexedMode;
        }

        private void PopulatePositionCache()
        {
            this.hasExplicitVisibleIndexes = this.columnNodes.HasExplicitIndexes();
            foreach (ColumnNode node in this.columnNodes)
            {
                this.CachePosition(node, node.VisibleIndex, node.CollectionIndex, node.Row);
            }
        }

        private void PositionAdd(ColumnNode column)
        {
            int visibleIndex = -1;
            int collectionIndex = -1;
            foreach (Position position in this.positionCache.Values)
            {
                if (visibleIndex < position.VisibleIndex)
                {
                    visibleIndex = position.VisibleIndex;
                }
                if (collectionIndex < position.CollectionIndex)
                {
                    collectionIndex = position.CollectionIndex;
                }
            }
            this.CachePosition(column, visibleIndex + 1, collectionIndex + 1, column.Row);
        }

        private void PositionInsert(ColumnNode source, ColumnNode target, BandedViewDropPlace mode)
        {
            Position cachedPosition = this.GetCachedPosition(target);
            Func<Position, int> indexSelector = <>c.<>9__24_0;
            if (<>c.<>9__24_0 == null)
            {
                Func<Position, int> local1 = <>c.<>9__24_0;
                indexSelector = <>c.<>9__24_0 = x => x.VisibleIndex;
            }
            int visibleIndex = this.CalcInsertIndex(cachedPosition, mode, indexSelector);
            Func<Position, int> func2 = <>c.<>9__24_1;
            if (<>c.<>9__24_1 == null)
            {
                Func<Position, int> local2 = <>c.<>9__24_1;
                func2 = <>c.<>9__24_1 = x => x.CollectionIndex;
            }
            int collectionIndex = this.CalcInsertIndex(cachedPosition, mode, func2);
            int rowIndex = this.CalcInsertRow(cachedPosition.Row, mode);
            this.ShiftIndexes(visibleIndex, collectionIndex, rowIndex, (mode == BandedViewDropPlace.Top) || (mode == BandedViewDropPlace.Bottom), true, ShiftMode.Right);
            this.CachePosition(source, visibleIndex, collectionIndex, rowIndex);
        }

        private void PositionRemove(ColumnNode column)
        {
            Position cachedPosition = this.GetCachedPosition(column);
            int visibleIndex = cachedPosition.VisibleIndex;
            int currentRow = cachedPosition.Row;
            this.positionCache.Remove(column);
            bool allowShiftRowIndex = !this.positionCache.Values.Any<Position>(x => (x.Row == currentRow));
            this.ShiftIndexes(visibleIndex, cachedPosition.CollectionIndex, currentRow, allowShiftRowIndex, this.GetAllowShift(column), ShiftMode.Left);
        }

        public void Remove(ColumnNode column)
        {
            if ((column != null) && this.IsInCollection(column))
            {
                this.CollectionRemove(column);
                this.PositionRemove(column);
            }
        }

        private void ShiftIndexes(ShiftIndex visible, ShiftIndex collection, ShiftIndex row)
        {
            foreach (Position position in this.positionCache.Values)
            {
                position.VisibleIndex = visible.CalcShiftedValue(position.VisibleIndex);
                position.CollectionIndex = collection.CalcShiftedValue(position.CollectionIndex);
                position.Row = row.CalcShiftedValue(position.Row);
            }
        }

        private void ShiftIndexes(int visibleIndex, int collectionIndex, int rowIndex, bool allowShiftRowIndex, bool allowShift, ShiftMode mode)
        {
            ShiftIndex visible = new ShiftIndex(visibleIndex, mode);
            visible.AllowShift = allowShift;
            ShiftIndex row = new ShiftIndex(rowIndex, mode);
            row.AllowShift = allowShiftRowIndex;
            this.ShiftIndexes(visible, new ShiftIndex(collectionIndex, mode), row);
        }

        public bool AllowMoveInsideCollection
        {
            get => 
                this.allowMoveInsideCollectionCore;
            set
            {
                if (this.allowMoveInsideCollectionCore != value)
                {
                    this.allowMoveInsideCollectionCore = value;
                    this.OnAllowMoveInsideCollectionChanged();
                }
            }
        }

        internal bool IndexedMode =>
            this.hasExplicitVisibleIndexes || !this.AllowMoveInsideCollection;

        internal ColumnNodeCollection ColumnNodes =>
            this.columnNodes;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BandColumnsMoveAdapter.<>c <>9 = new BandColumnsMoveAdapter.<>c();
            public static Func<BandColumnsMoveAdapter.MoveAdapterInsertInfo, ColumnNode> <>9__19_0;
            public static Func<BandColumnsMoveAdapter.Position, int> <>9__24_0;
            public static Func<BandColumnsMoveAdapter.Position, int> <>9__24_1;

            internal ColumnNode <.ctor>b__19_0(BandColumnsMoveAdapter.MoveAdapterInsertInfo x) => 
                x.Source;

            internal int <PositionInsert>b__24_0(BandColumnsMoveAdapter.Position x) => 
                x.VisibleIndex;

            internal int <PositionInsert>b__24_1(BandColumnsMoveAdapter.Position x) => 
                x.CollectionIndex;
        }

        private class MoveAdapterInsertInfo
        {
            public MoveAdapterInsertInfo(ColumnNode source, BandedViewDropPlace mode)
            {
                this.Source = source;
                this.InsertMode = mode;
            }

            public ColumnNode Source { get; private set; }

            public BandedViewDropPlace InsertMode { get; private set; }
        }

        private class Position
        {
            public Position(int visibleIndex, int collectionIndex, int row)
            {
                this.VisibleIndex = visibleIndex;
                this.CollectionIndex = collectionIndex;
                this.Row = row;
            }

            public int VisibleIndex { get; set; }

            public int CollectionIndex { get; set; }

            public int Row { get; set; }
        }

        private class ShiftIndex
        {
            private bool allowShiftCore = true;
            private int delta;
            private Func<int, int, bool> needShift;

            public ShiftIndex(int index, BandColumnsMoveAdapter.ShiftMode mode)
            {
                this.Index = index;
                this.InitShiftMode(mode);
            }

            public int CalcShiftedValue(int index) => 
                (!this.AllowShift || !this.needShift(index, this.Index)) ? index : (index + this.delta);

            private void InitShiftMode(BandColumnsMoveAdapter.ShiftMode mode)
            {
                if (mode == BandColumnsMoveAdapter.ShiftMode.Left)
                {
                    Func<int, int, bool> func2 = <>c.<>9__11_0;
                    if (<>c.<>9__11_0 == null)
                    {
                        Func<int, int, bool> local1 = <>c.<>9__11_0;
                        func2 = <>c.<>9__11_0 = (x, y) => x > y;
                    }
                    this.needShift = func2;
                    this.delta = -1;
                }
                else if (mode == BandColumnsMoveAdapter.ShiftMode.Right)
                {
                    Func<int, int, bool> func1 = <>c.<>9__11_1;
                    if (<>c.<>9__11_1 == null)
                    {
                        Func<int, int, bool> local2 = <>c.<>9__11_1;
                        func1 = <>c.<>9__11_1 = (x, y) => x >= y;
                    }
                    this.needShift = func1;
                    this.delta = 1;
                }
            }

            public int Index { get; set; }

            public bool AllowShift
            {
                get => 
                    this.allowShiftCore;
                set => 
                    this.allowShiftCore = value;
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly BandColumnsMoveAdapter.ShiftIndex.<>c <>9 = new BandColumnsMoveAdapter.ShiftIndex.<>c();
                public static Func<int, int, bool> <>9__11_0;
                public static Func<int, int, bool> <>9__11_1;

                internal bool <InitShiftMode>b__11_0(int x, int y) => 
                    x > y;

                internal bool <InitShiftMode>b__11_1(int x, int y) => 
                    x >= y;
            }
        }

        private enum ShiftMode
        {
            Left,
            Right
        }
    }
}

