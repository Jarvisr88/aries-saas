namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;

    public interface IFilterCriteriaQueryContext
    {
        void RaiseQueryFilterCriteria(QueryFilterCriteriaEventArgs args);
    }
}

