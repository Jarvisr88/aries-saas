namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    public interface ITableViewHitInfo : IDataViewHitInfo
    {
        bool IsRowIndicator { get; }
    }
}

