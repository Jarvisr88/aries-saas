namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal abstract class EndUserFilteringSettingsFactory : IEndUserFilteringSettingsFactory
    {
        private static readonly HashSet<Type> dxMetadataTypes;

        static EndUserFilteringSettingsFactory();
        protected EndUserFilteringSettingsFactory();
        protected abstract IEndUserFilteringSettings Create(IEndUserFilteringMetadataProvider type, IEndUserFilteringMetadataProvider customAttributes);
        protected abstract IEndUserFilteringMetadataProvider CreateCustomAttributesMetadataProvider(IEnumerable<IEndUserFilteringMetricAttributes> customAttributes);
        protected virtual IEndUserFilteringMetadataProvider CreateTypeMetadataProvider(Type type);
        IEndUserFilteringSettings IEndUserFilteringSettingsFactory.Create(Type type, IEnumerable<IEndUserFilteringMetricAttributes> customAttributes);
        private static bool IsDXMetadata(Type type, string baseType);
        private static bool IsImage(Type type);
        private static bool IsMetadata(Type type);
        protected static bool SkipFiltering(FilterAttributes filterAttributes);
        protected static bool SkipFiltering(string path, Type type);
        protected static bool SkipFiltering(Type type, FilterAttributes filterAttributes);

        protected abstract class CustomAttributesMetadataProviderBase : IEndUserFilteringMetadataProvider, IEnumerable<IEndUserFilteringMetric>, IEnumerable, IEndUserFilteringMetricAttributesProvider, IEqualityComparer<IEndUserFilteringMetricAttributes>
        {
            protected readonly IEnumerable<IEndUserFilteringMetricAttributes> customAttributes;
            protected IMetadataStorage metadataStorage;

            protected CustomAttributesMetadataProviderBase(IEnumerable<IEndUserFilteringMetricAttributes> customAttributes);
            protected bool AllowMerging(IEndUserFilteringMetricAttributes a);
            private static FilterAttributes CreateFilterAttributes(IEndUserFilteringMetricAttributes a, bool? enumIntegerValues, bool? allowNull);
            void IEndUserFilteringMetadataProvider.SetMetadataStorage(IMetadataStorage metadataStorage);
            protected AnnotationAttributes EnsureAnnotationAttributes(IEndUserFilteringMetricAttributes a, out AnnotationAttributes existing);
            protected FilterAttributes EnsureFilterAttributes(IEndUserFilteringMetricAttributes a, bool? enumIntegerValues, bool? isDataColumnAllowNull, bool? isXpoForcedNullability);
            private static bool? GetAllowNull(bool? isDataColumnAllowNull, bool? isXpoForcedNullability);
            protected abstract IEnumerator<IEndUserFilteringMetric> GetEnumeratorCore();
            protected IServiceProvider GetServiceProvider();
            IEnumerator<IEndUserFilteringMetric> IEnumerable<IEndUserFilteringMetric>.GetEnumerator();
            bool IEqualityComparer<IEndUserFilteringMetricAttributes>.Equals(IEndUserFilteringMetricAttributes x, IEndUserFilteringMetricAttributes y);
            int IEqualityComparer<IEndUserFilteringMetricAttributes>.GetHashCode(IEndUserFilteringMetricAttributes a);
            IEnumerator IEnumerable.GetEnumerator();

            IEnumerable<IEndUserFilteringMetricAttributes> IEndUserFilteringMetricAttributesProvider.Attributes { get; }
        }

        protected sealed class TypeMetadataProvider : IEndUserFilteringMetadataProvider, IEnumerable<IEndUserFilteringMetric>, IEnumerable
        {
            private HashSet<Type> typesHash;
            private Stack<KeyValuePair<string, PropertyDescriptor>> propertiesStack;
            private IMetadataStorage metadataStorage;

            public TypeMetadataProvider(Type type, string rootPath = null);
            void IEndUserFilteringMetadataProvider.SetMetadataStorage(IMetadataStorage metadataStorage);
            [IteratorStateMachine(typeof(EndUserFilteringSettingsFactory.TypeMetadataProvider.<GetEnumeratorCore>d__8))]
            private IEnumerator<IEndUserFilteringMetric> GetEnumeratorCore();
            private static string GetPath(string rootPath, PropertyDescriptor pd);
            private IServiceProvider GetServiceProvider();
            private void PopulateLevel(string rootPath, PropertyDescriptorCollection properties, Type propertyType = null);
            IEnumerator<IEndUserFilteringMetric> IEnumerable<IEndUserFilteringMetric>.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator();

            [CompilerGenerated]
            private sealed class <GetEnumeratorCore>d__8 : IEnumerator<IEndUserFilteringMetric>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private IEndUserFilteringMetric <>2__current;
                public EndUserFilteringSettingsFactory.TypeMetadataProvider <>4__this;
                private string <path>5__1;
                private AnnotationAttributes <attributes>5__2;
                private PropertyDescriptor <pd>5__3;

                [DebuggerHidden]
                public <GetEnumeratorCore>d__8(int <>1__state);
                private bool MoveNext();
                [DebuggerHidden]
                void IEnumerator.Reset();
                [DebuggerHidden]
                void IDisposable.Dispose();

                IEndUserFilteringMetric IEnumerator<IEndUserFilteringMetric>.Current { [DebuggerHidden] get; }

                object IEnumerator.Current { [DebuggerHidden] get; }
            }
        }
    }
}

