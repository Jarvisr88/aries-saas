namespace DevExpress.Data.XtraReports.DataProviders
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class EntityInfo<TName>
    {
        protected EntityInfo();
        public override bool Equals(object obj);
        public override int GetHashCode();

        [DataMember]
        public TName Name { get; set; }

        [DataMember]
        public string DisplayName { get; set; }
    }
}

