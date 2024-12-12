namespace DevExpress.Data.Helpers
{
    using System;

    public class ExpressiveSortInfo
    {
        public class Cell
        {
            public readonly Type ValuesType;
            public readonly Type GetterType;
            public readonly Type ComparerType;
            public readonly Delegate Transformator;
            public readonly bool TransformatorWithSourceIndex;
            public readonly Delegate Comparer;
            public readonly bool ComparerWithSourceIndex;
            public readonly bool IsFinalComparer;
            public readonly bool IsComparerThreadSafe;

            private Cell(Type _ValuesType, Type _GetterType, Type _ComparerType, Delegate transformator, bool _TransformatorWithSourceIndex, Delegate valuesComparer, bool _ValuesComparerWithSourceIndex, bool isValuesComparerFinalComparer, bool isComparerThreadSafe);
            public static ExpressiveSortInfo.Cell Create(Type _ValuesType);
            public static ExpressiveSortInfo.Cell Create<G>(Type _ValuesType, Func<G, G, int> comparer, bool isThreadSafe);
            public static ExpressiveSortInfo.Cell Create<G>(Type _ValuesType, Func<G, G, int?> comparer, bool isThreadSafe);
            public static ExpressiveSortInfo.Cell Create<G>(Type _ValuesType, Func<int, int, G, G, int> comparer, bool isThreadSafe);
            public static ExpressiveSortInfo.Cell Create<G>(Type _ValuesType, Func<int, int, G, G, int?> comparer, bool isThreadSafe);
            public static ExpressiveSortInfo.Cell Create<G, C>(Type _ValuesType, Func<G, C> transformator, Func<C, C, int> comparer, bool isThreadSafe);
            public static ExpressiveSortInfo.Cell Create<G, C>(Type _ValuesType, Func<G, C> transformator, Func<C, C, int?> comparer, bool isThreadSafe);
            public static ExpressiveSortInfo.Cell Create<G, C>(Type _ValuesType, Func<G, C> transformator, Func<int, int, C, C, int> comparer, bool isThreadSafe);
            public static ExpressiveSortInfo.Cell Create<G, C>(Type _ValuesType, Func<G, C> transformator, Func<int, int, C, C, int?> comparer, bool isThreadSafe);
            public static ExpressiveSortInfo.Cell Create<G, C>(Type _ValuesType, Func<int, G, C> transformator, Func<C, C, int> comparer, bool isThreadSafe);
            public static ExpressiveSortInfo.Cell Create<G, C>(Type _ValuesType, Func<int, G, C> transformator, Func<C, C, int?> comparer, bool isThreadSafe);
            public static ExpressiveSortInfo.Cell Create<G, C>(Type _ValuesType, Func<int, G, C> transformator, Func<int, int, C, C, int> comparer, bool isThreadSafe);
            public static ExpressiveSortInfo.Cell Create<G, C>(Type _ValuesType, Func<int, G, C> transformator, Func<int, int, C, C, int?> comparer, bool isThreadSafe);
            public static ExpressiveSortInfo.Cell Create(Type _ValuesType, Type _GetterType, Delegate valuesComparer, bool _ValuesComparerWithSourceIndex, bool isValuesComparerFinalComparer, bool isComparerThreadSafe);
            public static ExpressiveSortInfo.Cell Create(Type _ValuesType, Type _GetterType, Type _ComparerType, Delegate transformator, bool _TransformatorWithSourceIndex, Delegate valuesComparer, bool _ValuesComparerWithSourceIndex, bool isValuesComparerFinalComparer, bool isComparerThreadSafe);
        }

        public class Row
        {
            public readonly Delegate RowsComparer;
            public readonly bool IsFinalComparer;
            public readonly bool IsComparerThreadSafe;

            public Row(Delegate rowsComparer, bool isFinalComparer, bool isComparerThreadSafe);
        }
    }
}

