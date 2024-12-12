namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Utils;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class DefaultFilterUIEditorPropertiesFactory : IFilterUIEditorPropertiesFactory
    {
        public static readonly IFilterUIEditorPropertiesFactory Instance = new DefaultFilterUIEditorPropertiesFactory();

        private DefaultFilterUIEditorPropertiesFactory()
        {
        }

        IFilterUIEditorProperties IFilterUIEditorPropertiesFactory.Create(IEndUserFilteringMetric metric)
        {
            if (metric.AttributesTypeDefinition == typeof(IRangeMetricAttributes<>))
            {
                return new FilterUIEditorProperties(metric);
            }
            if (metric.AttributesTypeDefinition == typeof(ILookupMetricAttributes<>))
            {
                return new FilterUIEditorProperties((ILookupMetricAttributes) metric.Attributes);
            }
            if (metric.AttributesTypeDefinition == typeof(IEnumChoiceMetricAttributes<>))
            {
                return new FilterUIEditorProperties((IEnumChoiceMetricAttributes) metric.Attributes);
            }
            if (metric.AttributesTypeDefinition == typeof(IChoiceMetricAttributes<>))
            {
                return new FilterUIEditorProperties((IBooleanChoiceMetricAttributes) metric.Attributes);
            }
            if (metric.AttributesTypeDefinition != typeof(IGroupMetricAttributes<>))
            {
                throw new NotSupportedException(metric.AttributesTypeDefinition.ToString());
            }
            return new FilterUIEditorProperties((IGroupMetricAttributes) metric.Attributes);
        }

        private sealed class FilterUIEditorProperties : IFilterUIEditorProperties
        {
            internal FilterUIEditorProperties(IBooleanChoiceMetricAttributes booleanChoiceMetricAttributes)
            {
                this.BooleanUIEditorType = new DevExpress.Utils.Filtering.BooleanUIEditorType?(booleanChoiceMetricAttributes.EditorType);
            }

            internal FilterUIEditorProperties(IEndUserFilteringMetric rangeMetric)
            {
                IRangeMetricAttributes attributes = (IRangeMetricAttributes) rangeMetric.Attributes;
                if (attributes.IsNumericRange)
                {
                    this.RangeUIEditorType = new DevExpress.Utils.Filtering.RangeUIEditorType?(attributes.NumericRangeUIEditorType);
                }
                else
                {
                    this.DateTimeRangeUIEditorType = new DevExpress.Utils.Filtering.DateTimeRangeUIEditorType?(attributes.DateTimeRangeUIEditorType);
                }
                AnnotationAttributes.ConditionallyAPTCA(() => this.AssignDataType(rangeMetric));
            }

            internal FilterUIEditorProperties(IEnumChoiceMetricAttributes enumChoiceMetricAttributes)
            {
                this.LookupUIEditorType = new DevExpress.Utils.Filtering.LookupUIEditorType?(enumChoiceMetricAttributes.EditorType);
                this.EnumType = enumChoiceMetricAttributes.EnumType;
                this.UseFlags = new bool?(enumChoiceMetricAttributes.UseFlags);
            }

            internal FilterUIEditorProperties(IGroupMetricAttributes groupMetricAttributes)
            {
                this.GroupUIEditorType = new DevExpress.Utils.Filtering.GroupUIEditorType?(groupMetricAttributes.EditorType);
            }

            internal FilterUIEditorProperties(ILookupMetricAttributes lookupMetricAttributes)
            {
                this.LookupUIEditorType = new DevExpress.Utils.Filtering.LookupUIEditorType?(lookupMetricAttributes.EditorType);
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            private void AssignDataType(IEndUserFilteringMetric rangeMetric)
            {
                this.DataType = rangeMetric.DataType;
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            private void AssignDataType(IFilterUIEditorProperties properties)
            {
                this.DataType = properties.DataType;
            }

            void IFilterUIEditorProperties.Assign(IFilterUIEditorProperties properties)
            {
                this.RangeUIEditorType = properties.RangeUIEditorType;
                this.DateTimeRangeUIEditorType = properties.DateTimeRangeUIEditorType;
                this.LookupUIEditorType = properties.LookupUIEditorType;
                this.BooleanUIEditorType = properties.BooleanUIEditorType;
                this.GroupUIEditorType = properties.GroupUIEditorType;
                AnnotationAttributes.ConditionallyAPTCA(() => this.AssignDataType(properties));
                this.EnumType = properties.EnumType;
                this.UseFlags = properties.UseFlags;
            }

            public DevExpress.Utils.Filtering.RangeUIEditorType? RangeUIEditorType { get; private set; }

            public DevExpress.Utils.Filtering.DateTimeRangeUIEditorType? DateTimeRangeUIEditorType { get; private set; }

            public DevExpress.Utils.Filtering.LookupUIEditorType? LookupUIEditorType { get; private set; }

            public DevExpress.Utils.Filtering.BooleanUIEditorType? BooleanUIEditorType { get; private set; }

            public DevExpress.Utils.Filtering.GroupUIEditorType? GroupUIEditorType { get; private set; }

            public System.ComponentModel.DataAnnotations.DataType? DataType { get; private set; }

            public Type EnumType { get; private set; }

            public bool? UseFlags { get; private set; }
        }
    }
}

