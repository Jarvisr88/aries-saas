namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class DocumentExportArgs
    {
        [DataMember]
        public ExportFormat Format { get; set; }

        [DataMember]
        public byte[] SerializedExportOptions { get; set; }

        [DataMember]
        public object CustomArgs { get; set; }
    }
}

