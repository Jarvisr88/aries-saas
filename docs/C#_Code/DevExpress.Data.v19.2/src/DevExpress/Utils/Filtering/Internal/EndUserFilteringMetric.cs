namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    internal sealed class EndUserFilteringMetric : EndUserFilteringElement, IEndUserFilteringMetric, IEndUserFilteringElement
    {
        private static readonly System.Type ExplicitLookupMetricAttributesTypeDefinition;
        private static readonly System.Type ExplicitRangeMetricAttributesTypeDefinition;

        static EndUserFilteringMetric();
        public EndUserFilteringMetric(Func<IServiceProvider> getServiceProvider, string path);
        [DebuggerStepThrough, DebuggerHidden]
        internal bool IsExplicitLookup();
        private static bool IsExplicitLookup(FilterAttributes filterAttributes);
        [DebuggerStepThrough, DebuggerHidden]
        internal bool IsExplicitLookup(Func<FilterLookupAttribute, bool> predicate);
        private static bool IsExplicitLookup(FilterAttributes filterAttributes, Func<FilterLookupAttribute, bool> condition);
        [DebuggerStepThrough, DebuggerHidden]
        internal bool IsExplicitRange();
        private static bool IsExplicitRange(FilterAttributes filterAttributes);
        [DebuggerStepThrough, DebuggerHidden]
        internal void WithAttributes(Action<AnnotationAttributes> action);

        public System.Type Type { get; }

        public System.Type AttributesTypeDefinition { get; }

        public IMetricAttributes Attributes { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EndUserFilteringMetric.<>c <>9;
            public static Func<IMetadataProvider, Func<string, Type>> <>9__2_0;
            public static Func<IMetadataProvider, Func<string, Type>> <>9__4_0;
            public static Func<IMetadataProvider, Func<string, IMetricAttributes>> <>9__6_0;
            public static Func<IMetadataProvider, Func<string, AnnotationAttributes>> <>9__7_0;
            public static Func<IMetadataProvider, Func<string, FilterAttributes>> <>9__8_0;
            public static Func<FilterAttributes, bool> <>9__8_1;
            public static Func<IMetadataProvider, Func<string, FilterAttributes>> <>9__9_0;
            public static Func<Type, bool> <>9__11_0;
            public static Func<IMetadataProvider, Func<string, FilterAttributes>> <>9__13_0;
            public static Func<FilterAttributes, bool> <>9__13_1;
            public static Func<Type, bool> <>9__15_0;

            static <>c();
            internal Func<string, IMetricAttributes> <get_Attributes>b__6_0(IMetadataProvider x);
            internal Func<string, Type> <get_AttributesTypeDefinition>b__4_0(IMetadataProvider x);
            internal Func<string, Type> <get_Type>b__2_0(IMetadataProvider x);
            internal bool <IsExplicitLookup>b__11_0(Type typedefinition);
            internal Func<string, FilterAttributes> <IsExplicitLookup>b__8_0(IMetadataProvider x);
            internal bool <IsExplicitLookup>b__8_1(FilterAttributes filterAttributes);
            internal Func<string, FilterAttributes> <IsExplicitLookup>b__9_0(IMetadataProvider x);
            internal Func<string, FilterAttributes> <IsExplicitRange>b__13_0(IMetadataProvider x);
            internal bool <IsExplicitRange>b__13_1(FilterAttributes filterAttributes);
            internal bool <IsExplicitRange>b__15_0(Type typedefinition);
            internal Func<string, AnnotationAttributes> <WithAttributes>b__7_0(IMetadataProvider x);
        }
    }
}

