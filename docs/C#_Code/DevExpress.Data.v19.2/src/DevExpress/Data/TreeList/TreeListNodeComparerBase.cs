namespace DevExpress.Data.TreeList
{
    using DevExpress.Data;
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class TreeListNodeComparerBase : Comparer<TreeListNodeBase>
    {
        protected ITreeListNodeCollection nodes;

        protected TreeListNodeComparerBase(TreeListDataControllerBase controller);
        private object CheckDBNull(object value);
        protected Comparison<TreeListNodeBase> CreateComparision(TreeListDataColumnSortInfo sortInfo);
        protected virtual ExpressiveSortInfo.Cell CreateSortCell(TreeListDataColumnSortInfo sortInfo);
        protected Delegate CreateValueCacher(TreeListDataColumnSortInfo sortInfo, Type columnType, Delegate valueGetter);
        private Type GetActualColumnType(TreeListDataColumnSortInfo sortInfo);
        protected Delegate GetRowValueDelegate(DataColumnInfo columnInfo, Type expectedReturnType);
        public virtual void Init(ITreeListNodeCollection nodes);

        protected TreeListDataControllerBase Controller { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TreeListNodeComparerBase.<>c <>9;
            public static Func<TreeListNodeBase, object> <>9__14_0;

            static <>c();
            internal object <GetRowValueDelegate>b__14_0(TreeListNodeBase node);
        }

        public abstract class TreeListSortCellComparerBuilder : GenericInvoker<Func<Delegate, Delegate, bool, Comparison<TreeListNodeBase>>, TreeListNodeComparerBase.TreeListSortCellComparerBuilder.Impl<object>>
        {
            protected TreeListSortCellComparerBuilder();

            public class Impl<T> : TreeListNodeComparerBase.TreeListSortCellComparerBuilder
            {
                private static Comparison<TreeListNodeBase> Build(Func<TreeListNodeBase, T> valueGetter, Func<T, T, int> comparer);
                private static Comparison<TreeListNodeBase> Build(Func<TreeListNodeBase, T> valueGetter, Func<TreeListNodeBase, TreeListNodeBase, T, T, int> comparer);
                protected override Func<Delegate, Delegate, bool, Comparison<TreeListNodeBase>> CreateInvoker();

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly TreeListNodeComparerBase.TreeListSortCellComparerBuilder.Impl<T>.<>c <>9;
                    public static Func<Delegate, Delegate, bool, Comparison<TreeListNodeBase>> <>9__2_0;

                    static <>c();
                    internal Comparison<TreeListNodeBase> <CreateInvoker>b__2_0(Delegate getter, Delegate comparer, bool isCustomSort);
                }
            }
        }

        public abstract class TreeListSortTransformBuilder : GenericInvoker<Func<Delegate, Delegate, Delegate>, TreeListNodeComparerBase.TreeListSortTransformBuilder.Impl<object, object>>
        {
            protected TreeListSortTransformBuilder();

            public class Impl<ColumnType, TransformedType> : TreeListNodeComparerBase.TreeListSortTransformBuilder
            {
                private static Func<TreeListNodeBase, TransformedType> Build(Func<TreeListNodeBase, ColumnType> getter, Func<TreeListNodeBase, ColumnType, TransformedType> transformer);
                protected override Func<Delegate, Delegate, Delegate> CreateInvoker();

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly TreeListNodeComparerBase.TreeListSortTransformBuilder.Impl<ColumnType, TransformedType>.<>c <>9;
                    public static Func<Delegate, Delegate, Delegate> <>9__1_0;

                    static <>c();
                    internal Delegate <CreateInvoker>b__1_0(Delegate getter, Delegate transformer);
                }
            }
        }

        public abstract class TreeListValuesGetterCacheBuilder : GenericInvoker<Func<TreeListNodeComparerBase, TreeListDataColumnSortInfo, Delegate, Delegate>, TreeListNodeComparerBase.TreeListValuesGetterCacheBuilder.Impl<object>>
        {
            protected TreeListValuesGetterCacheBuilder();

            public class Impl<T> : TreeListNodeComparerBase.TreeListValuesGetterCacheBuilder
            {
                private static Func<TreeListNodeBase, T> Build(TreeListNodeComparerBase comparer, TreeListDataColumnSortInfo sortInfo, Func<TreeListNodeBase, T> getter);
                protected override Func<TreeListNodeComparerBase, TreeListDataColumnSortInfo, Delegate, Delegate> CreateInvoker();
                private static void FillCache(ITreeListNodeCollection nodes, Func<TreeListNodeBase, T> getter, T[] cache);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly TreeListNodeComparerBase.TreeListValuesGetterCacheBuilder.Impl<T>.<>c <>9;
                    public static Func<TreeListNodeComparerBase, TreeListDataColumnSortInfo, Delegate, Delegate> <>9__2_0;

                    static <>c();
                    internal Delegate <CreateInvoker>b__2_0(TreeListNodeComparerBase comparer, TreeListDataColumnSortInfo sortInfo, Delegate getter);
                }
            }
        }
    }
}

