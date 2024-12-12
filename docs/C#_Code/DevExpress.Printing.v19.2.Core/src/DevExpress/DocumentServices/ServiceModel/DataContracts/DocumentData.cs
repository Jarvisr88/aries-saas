namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class DocumentData
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DocumentMapTreeViewNode DocumentMap { get; set; }

        [DataMember]
        public byte[] SerializedPageData { get; set; }

        [DataMember]
        public byte[] SerializedWatermark { get; set; }

        [DataMember]
        public byte[] ExportOptions { get; set; }

        [DataMember]
        public Dictionary<string, bool> DrillDownKeys { get; set; }

        [DataMember]
        public Dictionary<string, List<SortingFieldInfoContract>> BandSortingKeys { get; set; }

        [DataMember]
        public DevExpress.XtraPrinting.Native.AvailableExportModes AvailableExportModes { get; set; }

        [DataMember]
        public ExportOptionKind HiddenOptions { get; set; }

        [DataMember]
        public bool CanChangePageSettings { get; set; }
    }
}

