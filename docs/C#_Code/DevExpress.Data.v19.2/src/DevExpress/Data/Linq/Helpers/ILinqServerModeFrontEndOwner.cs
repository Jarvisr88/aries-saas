namespace DevExpress.Data.Linq.Helpers
{
    using System;
    using System.Linq;

    public interface ILinqServerModeFrontEndOwner
    {
        bool IsReadyForTakeOff();

        Type ElementType { get; }

        IQueryable QueryableSource { get; }

        string KeyExpression { get; }

        string DefaultSorting { get; }
    }
}

