namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class ExpressiveSortHelpers
    {
        private static Comparison<int> BeadNextCellComparer(Comparison<int> existingChain, Comparison<int> nextBead);
        private static Comparison<int> CreateBySourceRowIndexComparison(Func<int, int> handles2SourceIndicesMapper, bool isDesc);
        private static ExpressiveSortInfo.Cell GetCompareRowsCellInfo(IDataControllerSort expressiveSortClient, DataColumnInfo dataColumnInfo, Type baseExtractorType, ColumnSortOrder order);
        public static Type GetGetGetRowValueType(DataColumnInfo dci);
        private static bool GuessIsThreadSafeTypeAndDefaultComparer<T>();
        private static bool IsGenericComparable<T>();
        internal static bool IsGenericEquatable<T>();
        private static ExpressiveSortInfo.Cell MakeFinalCellComparerFromNonFinal(ExpressiveSortInfo.Cell sortedByClientMethodInfo, IComparer customComparer);
        public static Comparison<int> MakeRowsCompare(IDataControllerSort expressiveSortClient, DataColumnSortInfo[] sortInfos, BaseDataControllerHelper getValueSource, Func<int, int> handles2SourceIndicesMapper, Func<Type, Delegate, bool, int, Tuple<Delegate, Action<IEnumerable<int>>, Action>> finalCompareValueGetterCacher, ref bool isPlinqAble, out Action<IEnumerable<int>> prepareCaches, out Action disposeAfterUse);
        private static Comparison<int> MakeRowsCompareCore(IDataControllerSort expressiveSortClient, IEnumerable<DataColumnSortInfo> sortInfos, Func<DataColumnSortInfo, Type, Delegate> getterGetter, Func<int, int> handles2SourceIndicesMapper, Func<Type, Delegate, bool, int, Tuple<Delegate, Action<IEnumerable<int>>, Action>> finalCompareValueGetterCacher, ref bool isPlinqAble, out Action<IEnumerable<int>> prepareCaches, out Action disposeAfterUse);
        private static Comparison<int> MapNonFinalRowsComparerToHandles(Func<int, int> handles2SourceIndicesMapper, Comparison<int> comp, Func<int, int, int?> rowsComparer);
        private static Comparison<int> MapRowsComparerToHandles(Func<int, int> handles2SourceIndicesMapper, Func<int, int, int> typedComparer);
        private static ExpressiveSortHelpers.CellCompareLarvae[] PrepareCells(IDataControllerSort expressiveSortClient, IEnumerable<DataColumnSortInfo> sortInfos, Func<DataColumnSortInfo, Type, Delegate> getterGetter, out bool isThreadSafe);
        private static ExpressiveSortHelpers.CellCompareLarvae Transform(DataColumnSortInfo sortInfo, ExpressiveSortInfo.Cell cellMethodInfo, Func<DataColumnSortInfo, Type, Delegate> getterGetter);

        private static class _GuessIsThreadSafeTypeAndDefaultComparerMemo<T>
        {
            public static readonly bool ThreadSafeTypeAndComparerGuessed;
            public static bool IsGenericComparable;
            public static readonly bool IsGenericEquatable;

            static _GuessIsThreadSafeTypeAndDefaultComparerMemo();
            private static bool _GuessIsThreadSafeTypeAndDefaultComparer_Core(Type type);
            private static bool _IsGenericComparableCore(Type type);
            private static bool _IsGenericEqualzableCore(Type type);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExpressiveSortHelpers.<>c <>9;
            public static Func<<>f__AnonymousType0<ExpressiveSortHelpers.CellCompareLarvae, Tuple<Delegate, Action<IEnumerable<int>>, Action>>, Action<IEnumerable<int>>> <>9__6_1;
            public static Func<<>f__AnonymousType0<ExpressiveSortHelpers.CellCompareLarvae, Tuple<Delegate, Action<IEnumerable<int>>, Action>>, Action> <>9__6_2;
            public static Comparison<int> <>9__10_0;
            public static Comparison<int> <>9__10_1;
            public static Func<<>f__AnonymousType1<DataColumnSortInfo, ExpressiveSortInfo.Cell>, bool> <>9__14_1;

            static <>c();
            internal int <CreateBySourceRowIndexComparison>b__10_0(int r1, int r2);
            internal int <CreateBySourceRowIndexComparison>b__10_1(int r1, int r2);
            internal Action<IEnumerable<int>> <MakeRowsCompareCore>b__6_1(<>f__AnonymousType0<ExpressiveSortHelpers.CellCompareLarvae, Tuple<Delegate, Action<IEnumerable<int>>, Action>> item);
            internal Action <MakeRowsCompareCore>b__6_2(<>f__AnonymousType0<ExpressiveSortHelpers.CellCompareLarvae, Tuple<Delegate, Action<IEnumerable<int>>, Action>> item);
            internal bool <PrepareCells>b__14_1(<>f__AnonymousType1<DataColumnSortInfo, ExpressiveSortInfo.Cell> cd);
        }

        private class CellCompareLarvae
        {
            public readonly Type ComparerArgumentType;
            public readonly Delegate CompareValuesExtractor;
            public readonly Delegate Comparer;
            public readonly bool ComparerWithSourceIndices;
            public readonly bool IsDescending;

            public CellCompareLarvae(Type _ComparerArgumentType, Delegate _CompareValuesExtractor, Delegate _Comparer, bool _ComparerWithSourceIndices, bool _IsDescending);
        }

        public abstract class CellComparerBuilder : GenericInvoker<Func<Func<int, int>, Delegate, Delegate, bool, bool, Comparison<int>>, ExpressiveSortHelpers.CellComparerBuilder.Impl<object>>
        {
            protected CellComparerBuilder();

            public class Impl<T> : ExpressiveSortHelpers.CellComparerBuilder
            {
                private static Comparison<int> BuildForComparerWithoutSourceIndices(Func<int, T> finalGetter, Func<T, T, int> comparer, bool isNegative);
                private static Comparison<int> BuildForComparerWithSourceIndices(Func<int, int> handles2sourceindices, Func<int, T> finalGetter, Func<int, int, T, T, int> comparer, bool isNegative);
                protected override Func<Func<int, int>, Delegate, Delegate, bool, bool, Comparison<int>> CreateInvoker();

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly ExpressiveSortHelpers.CellComparerBuilder.Impl<T>.<>c <>9;
                    public static Func<Func<int, int>, Delegate, Delegate, bool, bool, Comparison<int>> <>9__2_0;

                    static <>c();
                    internal Comparison<int> <CreateInvoker>b__2_0(Func<int, int> handles2sourceindices, Delegate getter, Delegate comparer, bool comparerWithSourceIndices, bool isDesc);
                }
            }
        }

        public abstract class FinalComparableByValuesCompareMaker : GenericInvoker<Func<ExpressiveSortInfo.Cell, ExpressiveSortInfo.Cell>, ExpressiveSortHelpers.FinalComparableByValuesCompareMaker.Impl<object>>
        {
            protected FinalComparableByValuesCompareMaker();

            public class Impl<ComparerType> : ExpressiveSortHelpers.FinalComparableByValuesCompareMaker
            {
                protected override Func<ExpressiveSortInfo.Cell, ExpressiveSortInfo.Cell> CreateInvoker();
                private static ExpressiveSortInfo.Cell Make(ExpressiveSortInfo.Cell sortedByClientMethodInfo);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly ExpressiveSortHelpers.FinalComparableByValuesCompareMaker.Impl<ComparerType>.<>c <>9;
                    public static Func<ExpressiveSortInfo.Cell, ExpressiveSortInfo.Cell> <>9__1_0;

                    static <>c();
                    internal ExpressiveSortInfo.Cell <CreateInvoker>b__1_0(ExpressiveSortInfo.Cell sortedByClientMethodInfo);
                }
            }
        }

        public abstract class TransformBuilder : GenericInvoker<Func<Delegate, Delegate, bool, Delegate>, ExpressiveSortHelpers.TransformBuilder.Impl<object, object>>
        {
            protected TransformBuilder();

            public class Impl<ColumnType, TransformedType> : ExpressiveSortHelpers.TransformBuilder
            {
                private static Func<int, TransformedType> Build(Func<int, ColumnType> getter, Func<ColumnType, TransformedType> transformer);
                private static Func<int, TransformedType> Build(Func<int, ColumnType> getter, Func<int, ColumnType, TransformedType> transformer);
                protected override Func<Delegate, Delegate, bool, Delegate> CreateInvoker();

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly ExpressiveSortHelpers.TransformBuilder.Impl<ColumnType, TransformedType>.<>c <>9;
                    public static Func<Delegate, Delegate, bool, Delegate> <>9__2_0;

                    static <>c();
                    internal Delegate <CreateInvoker>b__2_0(Delegate getter, Delegate transformer, bool transWithSourceIndex);
                }
            }
        }
    }
}

