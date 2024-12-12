namespace DevExpress.Data.XtraReports.DataProviders
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum DataMemberType
    {
        [Display(Name="Table")]
        public const DataMemberType Table = DataMemberType.Table;,
        [Display(Name="View")]
        public const DataMemberType View = DataMemberType.View;,
        [Display(Name="StoredProcedure")]
        public const DataMemberType StoredProcedure = DataMemberType.StoredProcedure;,
        [Display(Name="Query")]
        public const DataMemberType Query = DataMemberType.Query;
    }
}

