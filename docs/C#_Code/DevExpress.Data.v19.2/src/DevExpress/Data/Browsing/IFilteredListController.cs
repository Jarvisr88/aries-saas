namespace DevExpress.Data.Browsing
{
    using DevExpress.Data.Filtering;
    using System;

    public interface IFilteredListController : IListController
    {
        CriteriaOperator FilterCriteria { get; set; }
    }
}

