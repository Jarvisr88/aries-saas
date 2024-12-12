namespace DevExpress.Data.XtraReports.Wizard.Views
{
    using DevExpress.Data.XtraReports.Wizard;
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Views.IChooseReportLayoutPageView class from the DevExpress.XtraReports assembly instead.")]
    public interface IChooseReportLayoutPageView
    {
        bool IsGroupedReport { get; set; }

        bool Portrait { get; set; }

        bool AdjustFieldWidth { get; set; }

        DevExpress.Data.XtraReports.Wizard.ReportLayout ReportLayout { get; set; }
    }
}

