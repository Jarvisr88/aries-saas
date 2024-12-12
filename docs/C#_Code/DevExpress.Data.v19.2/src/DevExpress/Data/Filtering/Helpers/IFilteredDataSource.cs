namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public interface IFilteredDataSource
    {
        CriteriaOperator Filter { get; set; }
    }
}

