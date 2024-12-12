namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class ScheduledJobDto : ScheduledJobCatalogItemDto
    {
        [DataMember]
        public IEnumerable<int> InternalSubscribers { get; set; }

        [DataMember]
        public DevExpress.ReportServer.ServiceModel.DataContracts.EmailReportFormat EmailReportFormat { get; set; }

        [DataMember]
        public bool EmailBlankReport { get; set; }

        [DataMember]
        public DevExpress.ReportServer.ServiceModel.DataContracts.EmailRecipientKind EmailRecipientKind { get; set; }

        [DataMember]
        public string SerializedRecurrenceInfo { get; set; }

        [DataMember]
        public DevExpress.ReportServer.ServiceModel.DataContracts.SchedulerParameters SchedulerParameters { get; set; }

        [DataMember]
        public string ExternalSubscribers { get; set; }

        [DataMember]
        public string ExportToSharedFolder { get; set; }

        [DataMember]
        public int RetentionPeriod { get; set; }

        [DataMember]
        public int? OptimisticLock { get; set; }
    }
}

