namespace DevExpress.Data.XtraReports.Wizard.Views
{
    using DevExpress.Data.XtraReports.Wizard;
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Views.IChooseSummaryOptionsPageView class from the DevExpress.XtraReports assembly instead.")]
    public interface IChooseSummaryOptionsPageView
    {
        void FillSummaryOptions(ColumnInfoSummaryOptions[] summaryOptions);
        void ShowWaitIndicator(bool show);

        bool IgnoreNullValues { get; set; }
    }
}

