namespace DevExpress.XtraReports.Native
{
    using DevExpress.Data;
    using System;

    public interface IDataContainer : IDataContainerBase, IDataContainerBase2
    {
        object GetSerializableDataSource();

        object DataAdapter { get; set; }
    }
}

