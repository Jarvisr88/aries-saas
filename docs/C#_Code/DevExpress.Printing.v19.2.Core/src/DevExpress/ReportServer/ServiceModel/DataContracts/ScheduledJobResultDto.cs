namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class ScheduledJobResultDto : ScheduledJobResultCatalogItemDto
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string ExecutionParameters { get; set; }

        [DataMember]
        public string Recipients { get; set; }
    }
}

