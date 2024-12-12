namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class ReportParameterContainer
    {
        [DataMember]
        public bool ShouldRequestParameters { get; set; }

        [DataMember]
        public ReportParameter[] Parameters { get; set; }
    }
}

