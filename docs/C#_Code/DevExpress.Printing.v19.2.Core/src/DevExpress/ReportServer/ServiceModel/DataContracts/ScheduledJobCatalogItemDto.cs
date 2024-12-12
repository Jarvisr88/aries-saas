namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class ScheduledJobCatalogItemDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ScheduledTaskMode TaskMode { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public bool Enabled { get; set; }

        [DataMember]
        public int? ReportId { get; set; }
    }
}

