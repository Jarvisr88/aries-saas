namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class ReportIdentity : InstanceIdentity
    {
        public ReportIdentity()
        {
        }

        public ReportIdentity(int id)
        {
            this.Id = id;
        }

        public override string ToString() => 
            "ReportIdentity_" + this.Id;

        [DataMember]
        public int Id { get; set; }
    }
}

