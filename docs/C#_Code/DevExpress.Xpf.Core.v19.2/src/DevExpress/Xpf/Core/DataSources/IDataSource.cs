namespace DevExpress.Xpf.Core.DataSources
{
    using System;

    public interface IDataSource
    {
        object ContextInstance { get; }

        Type ContextType { get; set; }

        object Data { get; }

        string Path { get; set; }
    }
}

