namespace DevExpress.XtraReports.UI
{
    using DevExpress.XtraReports;
    using DevExpress.XtraReports.Native;
    using System;

    [Obsolete("Use the PrintHelper class instead.")]
    public class ReportPrintToolWpf : ReportPrintToolInternal
    {
        public ReportPrintToolWpf(IReport report) : base(report)
        {
        }
    }
}

