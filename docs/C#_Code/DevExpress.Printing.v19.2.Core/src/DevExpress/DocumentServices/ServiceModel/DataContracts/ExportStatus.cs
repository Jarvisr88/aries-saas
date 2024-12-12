namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;

    [StructLayout(LayoutKind.Sequential), DataContract]
    public struct ExportStatus
    {
        [DataMember]
        public DevExpress.DocumentServices.ServiceModel.DataContracts.ExportId ExportId { get; set; }
        [DataMember]
        public TaskStatus Status { get; set; }
        [DataMember]
        public int ProgressPosition { get; set; }
        [DataMember]
        public ServiceFault Fault { get; set; }
    }
}

