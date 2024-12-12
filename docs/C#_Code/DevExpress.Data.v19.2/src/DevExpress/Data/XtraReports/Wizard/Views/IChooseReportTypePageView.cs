namespace DevExpress.Data.XtraReports.Wizard.Views
{
    using DevExpress.Data.XtraReports.Wizard;
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Views.IChooseReportTypePageView class from the DevExpress.XtraReports assembly instead.")]
    public interface IChooseReportTypePageView
    {
        DevExpress.Data.XtraReports.Wizard.ReportType ReportType { get; set; }
    }
}

