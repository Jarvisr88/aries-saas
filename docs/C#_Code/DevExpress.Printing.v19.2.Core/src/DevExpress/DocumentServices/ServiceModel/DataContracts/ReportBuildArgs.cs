namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class ReportBuildArgs
    {
        [DataMember]
        public ReportParameter[] Parameters { get; set; }

        [DataMember]
        public byte[] SerializedPageData { get; set; }

        [DataMember]
        public byte[] SerializedWatermark { get; set; }

        [DataMember]
        public Dictionary<string, bool> DrillDownKeys { get; set; }

        [DataMember]
        public Dictionary<string, List<SortingFieldInfoContract>> BandSorting { get; set; }

        [DataMember]
        public object CustomArgs { get; set; }
    }
}

