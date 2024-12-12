namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class PrintStatus
    {
        [DataMember]
        public DevExpress.DocumentServices.ServiceModel.DataContracts.PrintId PrintId { get; set; }

        [DataMember]
        public TaskStatus Status { get; set; }

        [DataMember]
        public int ProgressPosition { get; set; }

        [DataMember]
        public ServiceFault Fault { get; set; }
    }
}

