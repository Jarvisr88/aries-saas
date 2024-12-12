namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal sealed class DefaultCustomUIFiltersFactory : ICustomUIFiltersFactory
    {
        internal static readonly ICustomUIFiltersFactory Instance = new DefaultCustomUIFiltersFactory();

        private DefaultCustomUIFiltersFactory()
        {
        }

        ICustomUIFilters ICustomUIFiltersFactory.Create(IEndUserFilteringMetric metric, IMetricAttributesQuery query, Func<IServiceProvider> getServiceProvider)
        {
            CustomUIFiltersType numeric = CustomUIFiltersType.Numeric;
            if (metric.AttributesTypeDefinition == typeof(IRangeMetricAttributes<>))
            {
                IRangeMetricAttributes attributes = (IRangeMetricAttributes) metric.Attributes;
                if ((attributes != null) && !attributes.IsNumericRange)
                {
                    numeric = CustomUIFiltersType.DateTime;
                }
            }
            if (metric.AttributesTypeDefinition == typeof(ILookupMetricAttributes<>))
            {
                if ((metric.Type == typeof(string)) || ((metric.Type == typeof(object)) || TypeHelper.IsExpandableType(metric.Type)))
                {
                    numeric = CustomUIFiltersType.Text;
                }
                else
                {
                    ILookupMetricAttributes attributes = (ILookupMetricAttributes) metric.Attributes;
                    if (attributes != null)
                    {
                        IDisplayMetricAttributes attributes3 = (IDisplayMetricAttributes) metric.Attributes;
                        if (MetricAttributes.IsDateTimeRange(metric.Type))
                        {
                            numeric = ((attributes3 == null) || !attributes3.FilterByDisplayText) ? CustomUIFiltersType.DateTime : CustomUIFiltersType.Text;
                        }
                        if (MetricAttributes.IsBooleanChoice(metric.Type))
                        {
                            numeric = ((attributes3 == null) || !attributes3.FilterByDisplayText) ? CustomUIFiltersType.Boolean : CustomUIFiltersType.Text;
                        }
                        if (MetricAttributes.IsEnumChoice(metric.Type))
                        {
                            numeric = ((attributes3 == null) || !attributes3.FilterByDisplayText) ? CustomUIFiltersType.Enum : CustomUIFiltersType.Text;
                        }
                        if (!string.IsNullOrEmpty(attributes.DisplayMember))
                        {
                            numeric = CustomUIFiltersType.Text;
                        }
                        numeric ??= (((attributes3 == null) || !attributes3.FilterByDisplayText) ? CustomUIFiltersType.Numeric : CustomUIFiltersType.Text);
                    }
                }
            }
            if ((metric.AttributesTypeDefinition == typeof(IChoiceMetricAttributes<>)) && (((IBooleanChoiceMetricAttributes) metric.Attributes) != null))
            {
                numeric = CustomUIFiltersType.Boolean;
            }
            if ((metric.AttributesTypeDefinition == typeof(IEnumChoiceMetricAttributes<>)) && (((IEnumChoiceMetricAttributes) metric.Attributes) != null))
            {
                numeric = CustomUIFiltersType.Enum;
            }
            if (metric.AttributesTypeDefinition == typeof(IGroupMetricAttributes<>))
            {
                numeric = GetDefaultGroupFilterType(metric);
            }
            return new CustomUIFilters(metric, query, numeric, getServiceProvider);
        }

        private static CustomUIFiltersType GetDefaultGroupFilterType(IEndUserFilteringMetric metric)
        {
            Type type = metric.EnumDataType ?? metric.Type;
            CustomUIFiltersType numeric = CustomUIFiltersType.Numeric;
            if (MetricAttributes.IsDateTimeRange(type))
            {
                numeric = CustomUIFiltersType.DateTime;
            }
            if (MetricAttributes.IsBooleanChoice(type))
            {
                numeric = CustomUIFiltersType.Boolean;
            }
            if (MetricAttributes.IsEnumChoice(type))
            {
                numeric = CustomUIFiltersType.Enum;
            }
            if ((type == typeof(string)) || ((type == typeof(object)) || TypeHelper.IsExpandableType(type)))
            {
                numeric = CustomUIFiltersType.Text;
            }
            IDisplayMetricAttributes attributes = metric.Attributes as IDisplayMetricAttributes;
            if ((attributes != null) && attributes.FilterByDisplayText)
            {
                numeric = CustomUIFiltersType.Text;
            }
            return numeric;
        }
    }
}

