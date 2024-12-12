namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Utils;
    using DevExpress.Utils.Filtering;
    using DevExpress.Utils.IoC;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class EndUserFilteringSettings : IEndUserFilteringSettings, IEnumerable<IEndUserFilteringMetric>, IEnumerable, IMetadataStorage, IMetricAttributesCache, IEqualityComparer<IEndUserFilteringMetric>, IServiceProvider
    {
        private IOrderedStorage<IEndUserFilteringMetric> storage;
        private IEndUserFilteringMetadataProvider customMetadataProvider;
        internal static readonly IEnumerable<IEndUserFilteringMetricAttributes> EmptyAttributes;
        private readonly IntegrityContainer serviceContainer;
        private Hashtable disabledItems;
        private IDictionary<string, int> orders;
        private IDictionary<string, AnnotationAttributes> attributes;
        private readonly IDictionary<string, FilterAttributes> filterAttributes;
        private readonly IDictionary<string, Type> typeDefinitionsCache;
        private readonly IDictionary<string, IMetricAttributes> metricsAttributesCache;

        static EndUserFilteringSettings();
        public EndUserFilteringSettings(IEndUserFilteringMetadataProvider typeMetadataProvider, IEndUserFilteringMetadataProvider customMetadataProvider);
        private static FilterValuesType CheckDefaultValuesType(IEndUserFilteringMetric metric, FilterValuesType valuesType);
        protected virtual IBehaviorProvider CreateBehaviorProvider();
        protected virtual IMetadataProvider CreateMetadataProvider();
        bool IEndUserFilteringSettings.Ensure(string path, Type type, FilterType filterType, FilterValuesType valuesType, FilterGroupType groupType);
        IEnumerable<KeyValuePair<string, TValue>> IEndUserFilteringSettings.GetPairs<TValue>(Func<IEndUserFilteringMetric, TValue> accessor);
        void IMetadataStorage.SetAttributes(string path, AnnotationAttributes value);
        void IMetadataStorage.SetAttributes(string path, FilterAttributes value);
        void IMetadataStorage.SetEnabled(string path, bool value);
        void IMetadataStorage.SetOrder(string path, int? value);
        bool IMetadataStorage.TryGetValue(string path, out AnnotationAttributes value);
        bool IMetadataStorage.TryGetValue(string path, out FilterAttributes value);
        bool IMetadataStorage.TryGetValue(string path, out int value);
        IMetricAttributes IMetricAttributesCache.GetValueOrCache(string path, Func<IMetricAttributes> create);
        Type IMetricAttributesCache.GetValueOrCache(string path, Func<Type> create);
        void IMetricAttributesCache.Reset();
        private bool Ensure(string path, Type type);
        private bool Ensure(IEndUserFilteringMetric metric, FilterType filterType, FilterValuesType valuesType, FilterGroupType groupType);
        private void EnsureFilterByDisplayText(IEndUserFilteringMetric metric, FilterType filterType);
        private bool Force<TAttribute>(IMetadataStorage storage, string path) where TAttribute: FilterAttribute;
        private bool Force(IMetadataStorage storage, string path, Type type);
        private bool ForceGroupFilter(IEndUserFilteringMetric metric, FilterGroupType groupType);
        private bool ForceGroupFilter(IMetadataStorage storage, string path);
        private bool ForceGroupFilter(IMetadataStorage storage, string path, FilterGroupAttribute filterGroup);
        private bool ForceLookupFilter(IEndUserFilteringMetric metric);
        private bool ForceRangeFilter(IEndUserFilteringMetric metric);
        private FilterGroupAttribute GetFilterGroupAttribute(IMetadataStorage storage, string path);
        protected virtual ILazyMetricAttributesFactory GetLazyMetricAttributesFactory();
        private static T GetValueOrCache<T>(string path, Func<T> create, IDictionary<string, T> cache);
        private bool IsBooleanChoice(IEndUserFilteringMetric metric);
        private bool IsLookup(IEndUserFilteringMetric metric);
        private bool IsRange(IEndUserFilteringMetric metric);
        private void RegisterServices();
        private void Reset(string path);
        private bool Reset<TAttribute>(IMetadataStorage storage, string path) where TAttribute: FilterAttribute;
        private bool ResetLookupFilter(IEndUserFilteringMetric metric);
        private void ResetOrder();
        private bool ResetRangeFilter(IEndUserFilteringMetric metric);
        private void SetAttributesCore(string path, AnnotationAttributes value);
        private void SetEnabledCore(string path, bool value);
        private void SetFilterAttributesCore(string path, FilterAttributes value);
        private void SetOrderCore(string path, int? value);
        IEnumerator<IEndUserFilteringMetric> IEnumerable<IEndUserFilteringMetric>.GetEnumerator();
        bool IEqualityComparer<IEndUserFilteringMetric>.Equals(IEndUserFilteringMetric x, IEndUserFilteringMetric y);
        int IEqualityComparer<IEndUserFilteringMetric>.GetHashCode(IEndUserFilteringMetric m);
        IEnumerator IEnumerable.GetEnumerator();
        object IServiceProvider.GetService(Type serviceType);
        private bool UpdateFilterAttributes(string path);
        private bool UpdateGroupFilter(IEndUserFilteringMetric metric);
        private bool UpdateGroupFilter(IMetadataStorage storage, string path, FilterGroupAttribute filterGroup);

        IEnumerable<string> IEndUserFilteringSettings.Paths { get; }

        IEndUserFilteringMetric IEndUserFilteringSettings.this[string path] { get; }

        IEnumerable<IEndUserFilteringMetricAttributes> IEndUserFilteringSettings.CustomAttributes { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EndUserFilteringSettings.<>c <>9;
            public static Func<IEndUserFilteringMetric, int> <>9__2_0;
            public static Func<IEndUserFilteringMetric, string> <>9__3_0;
            public static Func<IEndUserFilteringMetric, string> <>9__25_0;
            public static Func<IEndUserFilteringMetric, string> <>9__27_0;
            public static Func<IEndUserFilteringMetricAttributesProvider, IEnumerable<IEndUserFilteringMetricAttributes>> <>9__30_0;
            public static Func<IEndUserFilteringMetric, int> <>9__47_0;

            static <>c();
            internal int <.ctor>b__2_0(IEndUserFilteringMetric m);
            internal string <DevExpress.Utils.Filtering.Internal.IEndUserFilteringSettings.Ensure>b__3_0(IEndUserFilteringMetric x);
            internal IEnumerable<IEndUserFilteringMetricAttributes> <DevExpress.Utils.Filtering.Internal.IEndUserFilteringSettings.get_CustomAttributes>b__30_0(IEndUserFilteringMetricAttributesProvider provider);
            internal string <DevExpress.Utils.Filtering.Internal.IEndUserFilteringSettings.get_Item>b__27_0(IEndUserFilteringMetric m);
            internal string <DevExpress.Utils.Filtering.Internal.IEndUserFilteringSettings.get_Paths>b__25_0(IEndUserFilteringMetric m);
            internal int <ResetOrder>b__47_0(IEndUserFilteringMetric m);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__31<TValue>
        {
            public static readonly EndUserFilteringSettings.<>c__31<TValue> <>9;
            public static Func<IEndUserFilteringMetric, string> <>9__31_0;

            static <>c__31();
            internal string <DevExpress.Utils.Filtering.Internal.IEndUserFilteringSettings.GetPairs>b__31_0(IEndUserFilteringMetric m);
        }
    }
}

