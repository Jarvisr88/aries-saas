namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class DataPaginationByCount : DataPagination
    {
        [DataMember]
        public int Offset { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}

