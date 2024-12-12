namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class CustomUIFilterAggregate : CustomUIFilter
    {
        private static readonly IDictionary<CustomUIFilterType, BinaryOperatorType> map;

        static CustomUIFilterAggregate()
        {
            Dictionary<CustomUIFilterType, BinaryOperatorType> dictionary1 = new Dictionary<CustomUIFilterType, BinaryOperatorType>();
            dictionary1.Add(CustomUIFilterType.AboveAverage, BinaryOperatorType.GreaterOrEqual);
            dictionary1.Add(CustomUIFilterType.BelowAverage, BinaryOperatorType.LessOrEqual);
            map = dictionary1;
        }

        public CustomUIFilterAggregate(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override bool AllowCore(ICustomUIFiltersOptions userOptions)
        {
            Func<ICustomUIFiltersOptions, bool> get = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<ICustomUIFiltersOptions, bool> local1 = <>c.<>9__3_0;
                get = <>c.<>9__3_0 = opt => opt.ShowAggregates;
            }
            return userOptions.Get<ICustomUIFiltersOptions, bool>(get, ExcelFilterOptions.Default.ShowAggregates.GetValueOrDefault(true));
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            null;

        protected sealed override ICustomUIFilterSummaryItem CreateSummaryItem()
        {
            CustomUIFilter.CustomUIFilterSummaryItem item1 = new CustomUIFilter.CustomUIFilterSummaryItem(base.GetPath());
            item1.Type = SummaryItemTypeEx.Average;
            return item1;
        }

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue) => 
            new BinaryOperator(new OperandProperty(metric.Path), new OperandValue(filterValue.Value), map[base.id]);

        internal static bool Match(CustomUIFilterType filterType) => 
            map.ContainsKey(filterType);

        protected sealed override void QueryViewModelResult(ICustomUIFilterDialogViewModel viewModel)
        {
            object obj2;
            ISummaryMetricAttributes metricAttributes = base.GetMetricAttributes<ISummaryMetricAttributes>();
            if ((metricAttributes != null) && metricAttributes.TryGetDataController(out obj2))
            {
                this.QueryViewModelResultFromSummaryItem(viewModel, obj2);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomUIFilterAggregate.<>c <>9 = new CustomUIFilterAggregate.<>c();
            public static Func<ICustomUIFiltersOptions, bool> <>9__3_0;

            internal bool <AllowCore>b__3_0(ICustomUIFiltersOptions opt) => 
                opt.ShowAggregates;
        }
    }
}

