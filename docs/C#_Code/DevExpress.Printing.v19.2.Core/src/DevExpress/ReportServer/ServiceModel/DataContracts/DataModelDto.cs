namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class DataModelDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string ConnectionString { get; set; }

        [DataMember]
        public string DbSchema { get; set; }

        [DataMember]
        public string Provider { get; set; }

        [DataMember]
        public int? OptimisticLock { get; set; }
    }
}

