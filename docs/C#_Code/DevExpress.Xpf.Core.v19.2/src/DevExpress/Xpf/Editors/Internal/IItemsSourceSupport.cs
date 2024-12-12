namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Editors;
    using System;

    public interface IItemsSourceSupport
    {
        object ItemsSource { get; }

        string PointValueMember { get; }

        string PointArgumentMember { get; }

        SparklineSortOrder PointArgumentSortOrder { get; }

        CriteriaOperator FilterCriteria { get; }
    }
}

