namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class ParameterLookUpValues
    {
        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public LookUpValueCollection LookUpValues { get; set; }
    }
}

