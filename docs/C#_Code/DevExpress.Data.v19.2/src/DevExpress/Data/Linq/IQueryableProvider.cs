namespace DevExpress.Data.Linq
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public interface IQueryableProvider
    {
        event EventHandler QueryableChanged;

        IQueryable Queryable { get; }
    }
}

