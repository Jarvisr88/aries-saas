namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract, KnownType(typeof(DateTimeCalculationKind))]
    public class SchedulerParameter
    {
        public SchedulerParameter(object value) : this(SchedulerParametersSource.Static, value)
        {
        }

        public SchedulerParameter(SchedulerParametersSource source, object value)
        {
            this.Source = source;
            this.Value = value;
        }

        [DataMember]
        public SchedulerParametersSource Source { get; set; }

        [DataMember]
        public object Value { get; set; }
    }
}

