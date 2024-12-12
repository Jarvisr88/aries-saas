namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IEndUserFilteringViewModelPropertyValues : IEnumerable<IEndUserFilteringMetricViewModel>, IEnumerable, IEndUserFilteringCriteriaChangeAware
    {
        void ApplyFilterCriteria(Func<object> getViewModel, CriteriaOperator criteria);
        void ApplyFilterCriteria(Func<object> getViewModel, string path, CriteriaOperator criteria);
        void EnsureValueType(string path);
        IEndUserFilteringViewModelPropertyValues GetNestedValues(string rootPath);
        bool ParseFilterCriteria(string path, CriteriaOperator criteria);
        CriteriaOperator QueryFilterCriteria(string path, CriteriaOperator criteria);

        IEndUserFilteringMetricViewModel this[string path] { get; }
    }
}

