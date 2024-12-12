namespace DevExpress.Data.ODataLinq.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Linq;

    public interface IODataServerModeFrontEndOwner
    {
        bool IsReadyForTakeOff();

        Type ElementType { get; }

        IQueryable Query { get; }

        string[] KeyExpressions { get; }

        string DefaultSorting { get; }

        CriteriaOperator FixedFilterCriteria { get; }

        string Properties { get; }
    }
}

