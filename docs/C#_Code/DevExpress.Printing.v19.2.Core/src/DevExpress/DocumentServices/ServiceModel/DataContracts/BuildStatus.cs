namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;

    [StructLayout(LayoutKind.Sequential), DataContract]
    public struct BuildStatus
    {
        [DataMember]
        public DevExpress.DocumentServices.ServiceModel.DataContracts.DocumentId DocumentId { get; set; }
        [DataMember]
        public int PageCount { get; set; }
        [DataMember]
        public int ProgressPosition { get; set; }
        [DataMember]
        public TaskStatus Status { get; set; }
        [DataMember]
        public ServiceFault Fault { get; set; }
    }
}

