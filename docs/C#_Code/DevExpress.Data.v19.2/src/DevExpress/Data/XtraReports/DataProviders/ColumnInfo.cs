namespace DevExpress.Data.XtraReports.DataProviders
{
    using DevExpress.Data.Browsing.Design;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class ColumnInfo : EntityInfo<string>, ICloneable
    {
        public object Clone();
        public override bool Equals(object obj);
        public override int GetHashCode();

        [DataMember]
        public DevExpress.Data.Browsing.Design.TypeSpecifics TypeSpecifics { get; set; }
    }
}

