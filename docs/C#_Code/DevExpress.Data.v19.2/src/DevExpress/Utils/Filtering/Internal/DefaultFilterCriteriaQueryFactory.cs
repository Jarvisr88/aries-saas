namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;

    internal sealed class DefaultFilterCriteriaQueryFactory : IFilterCriteriaQueryFactory
    {
        internal static readonly IFilterCriteriaQueryFactory Instance;

        static DefaultFilterCriteriaQueryFactory();
        private DefaultFilterCriteriaQueryFactory();
        QueryFilterCriteriaEventArgs IFilterCriteriaQueryFactory.Create(IEndUserFilteringMetricViewModel metricViewModel);

        private sealed class QueryFilterCriteriaEventArgs : QueryFilterCriteriaEventArgs
        {
            public QueryFilterCriteriaEventArgs(IEndUserFilteringMetricViewModel metricViewModel);
        }
    }
}

