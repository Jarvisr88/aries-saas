namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using DevExpress.Data.XtraReports.DataProviders;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class StoredDataSourceInfo : DataSourceInfo
    {
        public override bool Equals(object obj)
        {
            StoredDataSourceInfo info = obj as StoredDataSourceInfo;
            return (base.Equals(obj) && Equals(this.Id, info.Id));
        }

        public override int GetHashCode() => 
            base.GetHashCode() ^ this.Id.GetHashCode();

        [DataMember]
        public int Id { get; set; }
    }
}

