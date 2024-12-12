namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    internal sealed class ExcelFilteringSettingsFactory : EndUserFilteringSettingsFactory
    {
        internal static readonly IEndUserFilteringSettingsFactory Instance;

        static ExcelFilteringSettingsFactory();
        private ExcelFilteringSettingsFactory();
        protected sealed override IEndUserFilteringSettings Create(IEndUserFilteringMetadataProvider type, IEndUserFilteringMetadataProvider customAttributes);
        protected sealed override IEndUserFilteringMetadataProvider CreateCustomAttributesMetadataProvider(IEnumerable<IEndUserFilteringMetricAttributes> customAttributes);

        private sealed class CustomAttributesMetadataProvider : EndUserFilteringSettingsFactory.CustomAttributesMetadataProviderBase
        {
            public CustomAttributesMetadataProvider(IEnumerable<IEndUserFilteringMetricAttributes> customAttributes);
            [IteratorStateMachine(typeof(ExcelFilteringSettingsFactory.CustomAttributesMetadataProvider.<GetEnumeratorCore>d__1))]
            protected override IEnumerator<IEndUserFilteringMetric> GetEnumeratorCore();

            [CompilerGenerated]
            private sealed class <GetEnumeratorCore>d__1 : IEnumerator<IEndUserFilteringMetric>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private IEndUserFilteringMetric <>2__current;
                public ExcelFilteringSettingsFactory.CustomAttributesMetadataProvider <>4__this;
                private AnnotationAttributes <existing>5__1;
                private IEndUserFilteringMetricAttributes <a>5__2;
                private AnnotationAttributes <annotationAttributes>5__3;
                private IEnumerator<IEndUserFilteringMetricAttributes> <>7__wrap1;

                [DebuggerHidden]
                public <GetEnumeratorCore>d__1(int <>1__state);
                private void <>m__Finally1();
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

