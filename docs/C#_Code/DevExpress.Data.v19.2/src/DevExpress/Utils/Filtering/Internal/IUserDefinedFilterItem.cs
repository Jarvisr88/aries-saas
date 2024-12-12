namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;

    public interface IUserDefinedFilterItem
    {
        string Name { get; }

        CriteriaOperator Criteria { get; }
    }
}

