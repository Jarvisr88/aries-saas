namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;

    public interface IGroupMetricAttributesQuery : IMetricAttributesQuery
    {
        IGroupMetricAttributesQuery Initialize(object[] path, CriteriaOperator criteria, string group);
    }
}

