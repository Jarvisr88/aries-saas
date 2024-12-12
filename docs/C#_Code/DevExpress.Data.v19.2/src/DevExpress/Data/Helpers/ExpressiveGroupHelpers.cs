namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class ExpressiveGroupHelpers
    {
        public static Func<int, int, bool> GetGroupRowsEqualizer(IDataControllerSort expressiveSortClient, BaseDataControllerHelper getValueSource, DataColumnInfo dataColumnInfo);
        private static ExpressiveGroupHelpers.GroupEqualizerResult GetGroupRowsEqualizerFromCellCompareInfo(ExpressiveSortInfo.Cell clientMethodInfo, IComparer customComparer);

        public abstract class FinalEqualizerByValuesCompareMaker : GenericInvoker<Func<ExpressiveSortInfo.Cell, ExpressiveGroupHelpers.GroupEqualizerResult>, ExpressiveGroupHelpers.FinalEqualizerByValuesCompareMaker.Impl<object>>
        {
            protected FinalEqualizerByValuesCompareMaker();

            public class Impl<T> : ExpressiveGroupHelpers.FinalEqualizerByValuesCompareMaker
            {
                protected override Func<ExpressiveSortInfo.Cell, ExpressiveGroupHelpers.GroupEqualizerResult> CreateInvoker();
                private static ExpressiveGroupHelpers.GroupEqualizerResult Make(ExpressiveSortInfo.Cell sortedByClientMethodInfo);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly ExpressiveGroupHelpers.FinalEqualizerByValuesCompareMaker.Impl<T>.<>c <>9;
                    public static Func<object, object, bool> <>9__0_1;
                    public static Func<ExpressiveSortInfo.Cell, ExpressiveGroupHelpers.GroupEqualizerResult> <>9__1_0;

                    static <>c();
                    internal ExpressiveGroupHelpers.GroupEqualizerResult <CreateInvoker>b__1_0(ExpressiveSortInfo.Cell sortedByClientMethodInfo);
                    internal bool <Make>b__0_1(object value1, object value2);
                }
            }
        }

        public abstract class FinalProcessor : GenericInvoker<Func<ExpressiveGroupHelpers.GroupEqualizerResult, Func<Type, Delegate>, Func<int, int, bool>>, ExpressiveGroupHelpers.FinalProcessor.Impl<object, object>>
        {
            protected FinalProcessor();

            public class Impl<GetterType, TransformedType> : ExpressiveGroupHelpers.FinalProcessor
            {
                private static void CreateCachedAccessors(ExpressiveGroupHelpers.GroupEqualizerResult r, Func<Type, Delegate> getterGetter, out Func<int, TransformedType> accessorBase, out Func<int, TransformedType> accessorRunning);
                protected override Func<ExpressiveGroupHelpers.GroupEqualizerResult, Func<Type, Delegate>, Func<int, int, bool>> CreateInvoker();
                private static Func<int, TransformedType> GetNonCachedAccessor(ExpressiveGroupHelpers.GroupEqualizerResult r, Func<Type, Delegate> getterGetter);
                private static Func<int, int, bool> Make(ExpressiveGroupHelpers.GroupEqualizerResult r, Func<Type, Delegate> getterGetter);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly ExpressiveGroupHelpers.FinalProcessor.Impl<GetterType, TransformedType>.<>c <>9;
                    public static Func<ExpressiveGroupHelpers.GroupEqualizerResult, Func<Type, Delegate>, Func<int, int, bool>> <>9__3_0;

                    static <>c();
                    internal Func<int, int, bool> <CreateInvoker>b__3_0(ExpressiveGroupHelpers.GroupEqualizerResult r, Func<Type, Delegate> getterGetter);
                }
            }
        }

        public class GroupEqualizerResult
        {
            public readonly Type GetterType;
            public readonly Type TransformerType;
            public readonly bool TransformerWithSourceIndex;
            public readonly Delegate Transformer;
            public readonly bool EqualizerWithSourceIndices;
            public readonly Delegate Equalizer;

            public GroupEqualizerResult(Type _GetterType, bool _EqualizerWithSourceIndices, Delegate _Equalizer);
            public GroupEqualizerResult(Type _GetterType, Type _TransformerType, bool _TransformerWithSourceIndex, Delegate _Transformer, bool _EqualizerWithSourceIndices, Delegate _Equalizer);
            public static ExpressiveGroupHelpers.GroupEqualizerResult Create<T>(Func<T, T, bool> rv);
            public static ExpressiveGroupHelpers.GroupEqualizerResult Create<T>(Func<int, int, T, T, bool> rv);
            public static ExpressiveGroupHelpers.GroupEqualizerResult Create<T>(Type _GetterType, bool _TransformerWithSourceIndex, Delegate _Transformer, Func<T, T, bool> eq);
            public static ExpressiveGroupHelpers.GroupEqualizerResult Create<T>(Type _GetterType, bool _TransformerWithSourceIndex, Delegate _Transformer, Func<int, int, T, T, bool> eq);
        }

        public abstract class GroupRowsEqualizerFromClientSortInfoMaker : GenericInvoker<Func<ExpressiveSortInfo.Cell, ExpressiveGroupHelpers.GroupEqualizerResult>, ExpressiveGroupHelpers.GroupRowsEqualizerFromClientSortInfoMaker.Impl<object>>
        {
            protected GroupRowsEqualizerFromClientSortInfoMaker();
            public static ExpressiveGroupHelpers.GroupEqualizerResult MakeFrom(ExpressiveSortInfo.Cell sortedByClientMethodInfo);

            public class Impl<T> : ExpressiveGroupHelpers.GroupRowsEqualizerFromClientSortInfoMaker
            {
                protected override Func<ExpressiveSortInfo.Cell, ExpressiveGroupHelpers.GroupEqualizerResult> CreateInvoker();
                private static ExpressiveGroupHelpers.GroupEqualizerResult Make(ExpressiveSortInfo.Cell sortedByClientMethodInfo);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly ExpressiveGroupHelpers.GroupRowsEqualizerFromClientSortInfoMaker.Impl<T>.<>c <>9;
                    public static Func<ExpressiveSortInfo.Cell, ExpressiveGroupHelpers.GroupEqualizerResult> <>9__1_0;

                    static <>c();
                    internal ExpressiveGroupHelpers.GroupEqualizerResult <CreateInvoker>b__1_0(ExpressiveSortInfo.Cell cell);
                }
            }
        }
    }
}

