namespace DevExpress.Data.WcfLinq.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Linq;

    public interface IWcfServerModeFrontEndOwner
    {
        bool IsReadyForTakeOff();

        Type ElementType { get; }

        IQueryable Query { get; }

        string KeyExpression { get; }

        string DefaultSorting { get; }

        CriteriaOperator FixedFilterCriteria { get; }
    }
}

