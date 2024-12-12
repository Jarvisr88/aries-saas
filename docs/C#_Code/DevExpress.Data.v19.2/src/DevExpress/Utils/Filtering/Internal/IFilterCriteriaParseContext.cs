namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;

    public interface IFilterCriteriaParseContext
    {
        void RaiseParseFilterCriteria(DevExpress.Utils.Filtering.ParseFilterCriteriaEventArgs args);
    }
}

