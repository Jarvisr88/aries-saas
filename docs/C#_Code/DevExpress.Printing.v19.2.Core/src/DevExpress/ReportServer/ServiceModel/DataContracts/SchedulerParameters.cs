namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class SchedulerParameters
    {
        [DataMember]
        public ParametersBinding Binding { get; set; }

        [DataMember]
        public SchedulerParametersDictionary Parameters { get; set; }
    }
}

