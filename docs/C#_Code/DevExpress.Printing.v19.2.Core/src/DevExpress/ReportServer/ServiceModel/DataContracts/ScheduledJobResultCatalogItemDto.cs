namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class ScheduledJobResultCatalogItemDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public JobResultStatus Status { get; set; }
    }
}

