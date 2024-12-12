namespace DevExpress.Data.XtraReports.DataProviders
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class DataSourceInfo : EntityInfo<string>, ICloneable
    {
        public object Clone();
        public override bool Equals(object obj);
        public override int GetHashCode();

        [DataMember]
        public bool TablesOrViewsSupported { get; set; }
    }
}

