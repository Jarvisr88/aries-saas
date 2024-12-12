namespace DevExpress.Xpf.Core.DataSources
{
    using System;
    using System.Linq;

    public interface IWcfServerModeDataSource : IWcfDataSource, IDataSource
    {
        object DataServiceContext { get; set; }

        IQueryable Query { get; set; }
    }
}

