namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal sealed class DefaultEndUserFilteringMetricViewModelFactory : IEndUserFilteringMetricViewModelFactory
    {
        internal static readonly IEndUserFilteringMetricViewModelFactory Instance;

        static DefaultEndUserFilteringMetricViewModelFactory();
        private DefaultEndUserFilteringMetricViewModelFactory();
        public IEndUserFilteringMetricViewModel Create(IEndUserFilteringMetric metric, IEndUserFilteringMetricViewModelValueBox valueBox);
    }
}

