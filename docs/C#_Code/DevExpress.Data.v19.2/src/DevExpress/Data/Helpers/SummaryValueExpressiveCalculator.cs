namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class SummaryValueExpressiveCalculator
    {
        public static bool ForceV17_2NullsInTypedSumBehavior_WouldBeRemovedAt18_2;

        public static object Calculate(SummaryItemType summaryItemType, IEnumerable valuesEnumerable, Type valuesType, bool ignoreNulls, IComparer customComparer, Func<string[]> exceptionAuxInfoGetter);
        private static object DoTypedAverage(IEnumerable valuesEnumerable, Type valuesType, Func<string[]> exceptionAuxInfoGetter);
        private static object DoTypedSum(IEnumerable valuesEnumerable, Type valuesType, Func<string[]> exceptionAuxInfoGetter);
        private static object DoUntypedMinMax(bool isMax, IEnumerable<object> preparedEnumerable, IComparer customComparer);
        private static object DoUntypedSumWithCount(IEnumerable<object> preparedEnumerable, out int cnt);
        private static T? GetTypedSumStartValue<T>(T value) where T: struct;
        private static object ProcessUntypedCalculation(SummaryItemType summaryItemType, IEnumerable values, bool ignoreNulls, IComparer customComparer);
        private static object ProcessUntypedCalculationWithTypedFallback(SummaryItemType summaryItemType, IEnumerable iEnumerable, bool ignoreNulls, IComparer customComparer, Func<string[]> exceptionAuxInfoGetter);
        private static Type TryDetermineUntypedType(object[] values);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SummaryValueExpressiveCalculator.<>c <>9;
            public static Func<float?, float?> <>9__0_0;
            public static Func<double?, double?> <>9__0_1;
            public static Func<decimal?, decimal?> <>9__1_0;
            public static Func<double?, double?> <>9__1_1;
            public static Func<float?, float?> <>9__1_2;
            public static Func<TimeSpan?, double?> <>9__1_3;
            public static Func<long?, bool> <>9__5_0;
            public static Func<long?, long> <>9__5_1;
            public static Func<ulong?, bool> <>9__5_2;
            public static Func<ulong?, ulong> <>9__5_3;
            public static Func<ulong?, bool> <>9__5_4;
            public static Func<ulong?, ulong> <>9__5_5;
            public static Func<TimeSpan?, double?> <>9__5_6;
            public static Func<object, object> <>9__7_0;
            public static Func<object, bool> <>9__7_1;
            public static Func<object, bool> <>9__10_0;
            public static Func<object, Type> <>9__10_1;

            static <>c();
            internal float? <Calculate>b__0_0(float? v);
            internal double? <Calculate>b__0_1(double? v);
            internal decimal? <DoTypedAverage>b__1_0(decimal? v);
            internal double? <DoTypedAverage>b__1_1(double? v);
            internal float? <DoTypedAverage>b__1_2(float? v);
            internal double? <DoTypedAverage>b__1_3(TimeSpan? v);
            internal bool <DoTypedSum>b__5_0(long? v);
            internal long <DoTypedSum>b__5_1(long? v);
            internal bool <DoTypedSum>b__5_2(ulong? v);
            internal ulong <DoTypedSum>b__5_3(ulong? v);
            internal bool <DoTypedSum>b__5_4(ulong? v);
            internal ulong <DoTypedSum>b__5_5(ulong? v);
            internal double? <DoTypedSum>b__5_6(TimeSpan? v);
            internal object <ProcessUntypedCalculation>b__7_0(object x);
            internal bool <ProcessUntypedCalculation>b__7_1(object x);
            internal bool <TryDetermineUntypedType>b__10_0(object v);
            internal Type <TryDetermineUntypedType>b__10_1(object v);
        }

        public abstract class MinMaxComparableClassApplier : GenericInvoker<Func<IEnumerable, bool, object>, SummaryValueExpressiveCalculator.MinMaxComparableClassApplier.Impl<object>>
        {
            protected MinMaxComparableClassApplier();

            public class Impl<T> : SummaryValueExpressiveCalculator.MinMaxComparableClassApplier
            {
                protected override Func<IEnumerable, bool, object> CreateInvoker();
                private static object DoMax(IEnumerable<T> src);
                private static object DoMin(IEnumerable<T> src);
                private static object DoMinMax(IEnumerable<T> src, bool isMax);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly SummaryValueExpressiveCalculator.MinMaxComparableClassApplier.Impl<T>.<>c <>9;
                    public static Func<IEnumerable, bool, object> <>9__3_0;

                    static <>c();
                    internal object <CreateInvoker>b__3_0(IEnumerable src, bool isMax);
                }
            }
        }
    }
}

