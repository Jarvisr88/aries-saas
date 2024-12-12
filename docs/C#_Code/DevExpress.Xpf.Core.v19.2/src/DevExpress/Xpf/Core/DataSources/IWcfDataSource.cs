namespace DevExpress.Xpf.Core.DataSources
{
    using System;

    public interface IWcfDataSource : IDataSource
    {
        Uri ServiceRoot { get; }
    }
}

