namespace DevExpress.Xpf.Core.DataSources
{
    using System;

    internal interface ITypedDataSource : IDataSource
    {
        Type AdapterType { get; }
    }
}

