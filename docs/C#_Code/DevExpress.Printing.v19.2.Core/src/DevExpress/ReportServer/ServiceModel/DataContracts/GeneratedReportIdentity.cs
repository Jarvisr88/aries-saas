namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class GeneratedReportIdentity : InstanceIdentity
    {
        public GeneratedReportIdentity()
        {
        }

        public GeneratedReportIdentity(int id)
        {
            this.Id = id;
        }

        public override string ToString() => 
            "GeneratedReportIdentity_" + this.Id;

        [DataMember]
        public int Id { get; set; }
    }
}

