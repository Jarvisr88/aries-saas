namespace DevExpress.Data.XtraReports.Wizard
{
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.ReportType class from the DevExpress.XtraReports assembly instead.")]
    public enum ReportType
    {
        public const ReportType Standard = ReportType.Standard;,
        public const ReportType Label = ReportType.Label;,
        [EditorBrowsable(EditorBrowsableState.Never)]
        public const ReportType Empty = ReportType.Empty;,
        [EditorBrowsable(EditorBrowsableState.Never)]
        public const ReportType ReportStorage = ReportType.ReportStorage;
    }
}

