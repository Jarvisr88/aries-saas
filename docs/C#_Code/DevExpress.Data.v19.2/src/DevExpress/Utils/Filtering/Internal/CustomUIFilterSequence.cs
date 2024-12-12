namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal sealed class CustomUIFilterSequence : CustomUIFilter
    {
        private static readonly IDictionary<CustomUIFilterType, SummaryItemTypeEx> map;
        private static readonly IDictionary<CustomUIFilterType, SummaryItemTypeEx> percentsMap;

        static CustomUIFilterSequence()
        {
            Dictionary<CustomUIFilterType, SummaryItemTypeEx> dictionary1 = new Dictionary<CustomUIFilterType, SummaryItemTypeEx>();
            dictionary1.Add(CustomUIFilterType.TopN, SummaryItemTypeEx.Top);
            dictionary1.Add(CustomUIFilterType.BottomN, SummaryItemTypeEx.Bottom);
            map = dictionary1;
            Dictionary<CustomUIFilterType, SummaryItemTypeEx> dictionary2 = new Dictionary<CustomUIFilterType, SummaryItemTypeEx>();
            dictionary2.Add(CustomUIFilterType.TopN, SummaryItemTypeEx.TopPercent);
            dictionary2.Add(CustomUIFilterType.BottomN, SummaryItemTypeEx.BottomPercent);
            percentsMap = dictionary2;
        }

        public CustomUIFilterSequence(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override bool AllowCore(ICustomUIFiltersOptions userOptions)
        {
            Func<ICustomUIFiltersOptions, bool> get = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<ICustomUIFiltersOptions, bool> local1 = <>c.<>9__4_0;
                get = <>c.<>9__4_0 = opt => opt.ShowSequences;
            }
            return userOptions.Get<ICustomUIFiltersOptions, bool>(get, ExcelFilterOptions.Default.ShowSequences.GetValueOrDefault(true));
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            null;

        protected sealed override ICustomUIFilterSummaryItem CreateSummaryItem()
        {
            CustomUIFilter.CustomUIFilterSummaryItem item1 = new CustomUIFilter.CustomUIFilterSummaryItem(base.GetPath());
            item1.Type = map[base.id];
            item1.Argument = CustomUIFilterSequenceDefaultArguments.Count;
            return item1;
        }

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue)
        {
            object obj2;
            object obj3;
            ISummaryMetricAttributes metricAttributes = base.GetMetricAttributes<ISummaryMetricAttributes>();
            if ((metricAttributes != null) && (metricAttributes.TryGetDataController(out obj2) && base.SummaryItem.QueryValue(obj2, out obj3)))
            {
                IEnumerable source = obj3 as IEnumerable;
                if ((source != null) && source.OfType<object>().Any<object>())
                {
                    return new InOperator(metric.Path, source);
                }
            }
            return null;
        }

        protected sealed override ICustomUIFilterValue GetCustomUIFilterDialogViewModelParameter(ICustomUIFilter activeFilter)
        {
            ICustomUIFilterValue value1 = base.Value;
            ICustomUIFilterValue value2 = value1;
            if (value1 == null)
            {
                ICustomUIFilterValue local1 = value1;
                object[] values = new object[] { base.SummaryItem.Argument, this.GetSequenceQualifier() };
                value2 = this.CreateValue(values);
            }
            return value2;
        }

        private SequenceQualifier GetSequenceQualifier() => 
            (base.SummaryItem.Type == ((SummaryItemTypeEx) percentsMap[base.id])) ? SequenceQualifier.Percents : SequenceQualifier.Items;

        private SummaryItemTypeEx GetSummaryType(SequenceQualifier qualifier) => 
            (qualifier == SequenceQualifier.Items) ? map[base.id] : percentsMap[base.id];

        internal static bool Match(CustomUIFilterType filterType) => 
            map.ContainsKey(filterType);

        protected sealed override void OnValueChanged()
        {
            if (base.Value == null)
            {
                base.ResetSummaryItem();
            }
            else
            {
                object[] objArray = base.Value.Value as object[];
                base.SummaryItem.Type = this.GetSummaryType((SequenceQualifier) objArray[1]);
                base.SummaryItem.Argument = (decimal) GetValue(objArray[0], typeof(decimal));
            }
        }

        protected sealed override void QueryViewModelResult(ICustomUIFilterDialogViewModel viewModel)
        {
            object[] values = new object[] { base.SummaryItem.Argument, this.GetSequenceQualifier() };
            viewModel.SetResult(values);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomUIFilterSequence.<>c <>9 = new CustomUIFilterSequence.<>c();
            public static Func<ICustomUIFiltersOptions, bool> <>9__4_0;

            internal bool <AllowCore>b__4_0(ICustomUIFiltersOptions opt) => 
                opt.ShowSequences;
        }
    }
}

