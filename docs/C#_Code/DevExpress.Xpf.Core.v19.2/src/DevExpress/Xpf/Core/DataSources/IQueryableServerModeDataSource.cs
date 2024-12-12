namespace DevExpress.Xpf.Core.DataSources
{
    using System;
    using System.Linq;

    internal interface IQueryableServerModeDataSource : IDataSource
    {
        IQueryable QueryableSource { get; set; }
    }
}

