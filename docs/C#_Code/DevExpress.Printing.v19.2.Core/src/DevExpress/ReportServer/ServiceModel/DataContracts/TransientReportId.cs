namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class TransientReportId : InstanceIdentity
    {
        public TransientReportId()
        {
        }

        public TransientReportId(string publicId)
        {
            this.PublicId = publicId;
        }

        public override string ToString() => 
            "TransientReport_" + this.PublicId;

        [DataMember]
        public string PublicId { get; set; }
    }
}

