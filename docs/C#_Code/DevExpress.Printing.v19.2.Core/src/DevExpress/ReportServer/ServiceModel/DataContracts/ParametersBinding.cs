namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class ParametersBinding
    {
        [DataMember]
        public int DataModelId { get; set; }

        [DataMember]
        public string DataMember { get; set; }

        [DataMember]
        public string EmailField { get; set; }

        [DataMember]
        public string DisplayNameField { get; set; }
    }
}

