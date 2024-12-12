namespace DevExpress.Data.XtraReports.Wizard
{
    using DevExpress.Data.XtraReports.DataProviders;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.ColumnInfoSummaryOptions class from the DevExpress.XtraReports assembly instead.")]
    public class ColumnInfoSummaryOptions
    {
        public ColumnInfoSummaryOptions(DevExpress.Data.XtraReports.DataProviders.ColumnInfo column, SummaryOptionFlags flags);

        public DevExpress.Data.XtraReports.DataProviders.ColumnInfo ColumnInfo { get; set; }

        public SummaryOptions Options { get; private set; }
    }
}

