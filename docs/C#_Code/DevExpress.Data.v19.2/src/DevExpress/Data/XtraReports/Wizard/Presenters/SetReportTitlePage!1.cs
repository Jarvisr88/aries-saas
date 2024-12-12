namespace DevExpress.Data.XtraReports.Wizard.Presenters
{
    using DevExpress.Data.WizardFramework;
    using DevExpress.Data.XtraReports.Wizard.Views;
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Presenters.SetReportTitlePage<TModel> class from the DevExpress.XtraReports assembly instead.")]
    public class SetReportTitlePage<TModel> : WizardPageBase<ISetReportTitlePageView, TModel> where TModel: ReportModel
    {
        public SetReportTitlePage(ISetReportTitlePageView view);
        public override void Begin();
        public override void Commit();

        public override bool FinishEnabled { get; }
    }
}

