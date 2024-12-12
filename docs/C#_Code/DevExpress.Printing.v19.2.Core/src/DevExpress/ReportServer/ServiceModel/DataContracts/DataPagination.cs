namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    [DataContract, KnownType(typeof(DataPaginationByCount)), KnownType(typeof(DataPaginationByDate))]
    public abstract class DataPagination
    {
        protected DataPagination()
        {
        }
    }
}

