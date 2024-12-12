namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class BooleanValueBox<T> : SimpleValueBox<bool>, IBooleanValueViewModel, ISimpleValueViewModel<bool>, IValueViewModel, IFilterValueViewModel
    {
        [CompilerGenerated, DebuggerHidden]
        private void <>n__0(bool? value);
        CriteriaOperator IFilterValueViewModel.CreateFilterCriteria();
        private static bool? GetBoolOrNull(OperandValue value);
        private static bool? GetBoolOrNull(object value);
        protected sealed override bool InitializeWithNull(bool useInversion);
        protected sealed override bool InitializeWithValues(object[] uniqueAndSortedValues, bool useInversion);
        private bool TryParseBinary(string path, CriteriaOperator criteria);
        protected sealed override bool TryParseCore(IEndUserFilteringMetric metric, CriteriaOperator criteria);

        public override bool? Value { get; set; }

        protected IBooleanChoiceMetricAttributes MetricAttributes { get; }

        [Browsable(false)]
        public string TrueName { get; }

        [Browsable(false)]
        public string FalseName { get; }

        [Browsable(false)]
        public string DefaultName { get; }

        [Browsable(false)]
        public bool? DefaultValue { get; }
    }
}

