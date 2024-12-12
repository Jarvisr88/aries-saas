namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using DevExpress.Printing.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class ReportNameIdentity : InstanceIdentity
    {
        public ReportNameIdentity()
        {
        }

        public ReportNameIdentity(string value)
        {
            this.Value = value;
        }

        public static ReportNameIdentity GenerateNew() => 
            new ReportNameIdentity(IdGenerator.GenerateRandomId());

        public override string ToString() => 
            this.Value;

        [DataMember]
        public string Value { get; set; }
    }
}

