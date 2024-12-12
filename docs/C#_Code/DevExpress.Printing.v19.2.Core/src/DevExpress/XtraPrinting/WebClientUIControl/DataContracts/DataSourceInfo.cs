namespace DevExpress.XtraPrinting.WebClientUIControl.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class DataSourceInfo
    {
        [DataMember(Name="id")]
        public string Id { get; set; }

        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name="data")]
        public string Data { get; set; }

        [DataMember(Name="isSqlDataSource")]
        public bool IsSqlDataSource { get; set; }

        [DataMember(Name="isJsonDataSource", EmitDefaultValue=false)]
        public bool IsJsonDataSource { get; set; }

        [DataMember(Name="dataSerializer")]
        public string DataSerializer { get; set; }
    }
}

