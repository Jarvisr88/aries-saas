namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;

    internal sealed class ExcelLazyMetricAttributesFactory : LazyMetricAttributesFactoryBase
    {
        public static readonly ILazyMetricAttributesFactory Instance = new ExcelLazyMetricAttributesFactory();

        private ExcelLazyMetricAttributesFactory()
        {
        }

        protected sealed override IMetricAttributes CreateDateTimeRangeCore(Type type)
        {
            object min = MetricAttributes.IsTimeSpan(type) ? ((object) TimeSpan.MinValue) : ((object) DateTime.MinValue);
            return MetricAttributes.CreateRange(type, min, MetricAttributes.IsTimeSpan(type) ? ((object) TimeSpan.MaxValue) : ((object) DateTime.MaxValue), null, null, null, DateTimeRangeUIEditorType.Default, LazyMetricAttributesFactoryBase.EmptyRangeMembers);
        }

        protected sealed override IMetricAttributes CreateNumericRangeCore(Type type)
        {
            object min = DefaultValuesCache.Get(Nullable.GetUnderlyingType(type) ?? type);
            return MetricAttributes.CreateRange(type, min, min, null, null, null, null, RangeUIEditorType.Default, LazyMetricAttributesFactoryBase.EmptyRangeMembers);
        }
    }
}

