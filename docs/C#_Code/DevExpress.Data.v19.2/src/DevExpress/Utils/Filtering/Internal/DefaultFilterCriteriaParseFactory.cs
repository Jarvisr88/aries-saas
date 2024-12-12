namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;

    internal sealed class DefaultFilterCriteriaParseFactory : IFilterCriteriaParseFactory
    {
        internal static readonly IFilterCriteriaParseFactory Instance = new DefaultFilterCriteriaParseFactory();

        private DefaultFilterCriteriaParseFactory()
        {
        }

        DevExpress.Utils.Filtering.ParseFilterCriteriaEventArgs IFilterCriteriaParseFactory.Create(IEndUserFilteringMetricViewModel metricViewModel) => 
            new ParseFilterCriteriaEventArgs(metricViewModel);

        private sealed class ParseFilterCriteriaEventArgs : DevExpress.Utils.Filtering.ParseFilterCriteriaEventArgs
        {
            public ParseFilterCriteriaEventArgs(IEndUserFilteringMetricViewModel metricViewModel) : base(metricViewModel.Metric.Path, metricViewModel.Value)
            {
            }
        }
    }
}

