namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract, TypeConverter(typeof(ReportLayoutRevisionIdentityConverter))]
    public class ReportLayoutRevisionIdentity : InstanceIdentity
    {
        public ReportLayoutRevisionIdentity()
        {
        }

        public ReportLayoutRevisionIdentity(int id)
        {
            this.Id = id;
        }

        public override string ToString() => 
            "ReportLayoutRevisionIdentity_" + this.Id;

        [DataMember]
        public int Id { get; set; }
    }
}

