namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class DataPaginationByDate : DataPagination
    {
        [DataMember]
        public DateTime From { get; set; }

        [DataMember]
        public TimeSpan Interval { get; set; }
    }
}

