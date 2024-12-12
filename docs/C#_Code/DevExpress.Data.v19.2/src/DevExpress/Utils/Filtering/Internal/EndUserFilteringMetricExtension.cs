namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public static class EndUserFilteringMetricExtension
    {
        internal static Type[] EnsureValueTypes(this IEndUserFilteringMetric metric, string[] paths);
        [DebuggerStepThrough, DebuggerHidden]
        public static bool IsExplicitLookup(this IEndUserFilteringMetric metric);
        [DebuggerStepThrough, DebuggerHidden]
        public static bool IsExplicitLookup(this IEndUserFilteringMetric metric, Func<FilterLookupAttribute, bool> predicate);
        [DebuggerStepThrough, DebuggerHidden]
        public static bool IsExplicitRange(this IEndUserFilteringMetric metric);
        [DebuggerStepThrough, DebuggerHidden]
        internal static bool IsFlagEnum(this IEndUserFilteringMetric metric);
        [DebuggerStepThrough, DebuggerHidden]
        public static void WithAttributes(this IEndUserFilteringMetric metric, Action<AnnotationAttributes> action);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EndUserFilteringMetricExtension.<>c <>9;
            public static Func<EndUserFilteringMetric, bool> <>9__1_0;
            public static Func<EndUserFilteringMetric, bool> <>9__3_0;
            public static Func<IEnumChoiceMetricAttributes, bool> <>9__4_0;
            public static Func<IEndUserFilteringMetric, Type> <>9__5_0;

            static <>c();
            internal Type <EnsureValueTypes>b__5_0(IEndUserFilteringMetric x);
            internal bool <IsExplicitLookup>b__1_0(EndUserFilteringMetric m);
            internal bool <IsExplicitRange>b__3_0(EndUserFilteringMetric m);
            internal bool <IsFlagEnum>b__4_0(IEnumChoiceMetricAttributes enumChoice);
        }
    }
}

