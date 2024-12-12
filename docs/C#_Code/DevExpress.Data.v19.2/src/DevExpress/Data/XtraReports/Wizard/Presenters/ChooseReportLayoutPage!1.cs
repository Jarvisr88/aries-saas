namespace DevExpress.Data.XtraReports.Wizard.Presenters
{
    using DevExpress.Data.WizardFramework;
    using DevExpress.Data.XtraReports.Wizard.Views;
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Presenters.ChooseReportLayoutPage<TModel> class from the DevExpress.XtraReports assembly instead.")]
    public class ChooseReportLayoutPage<TModel> : WizardPageBase<IChooseReportLayoutPageView, TModel> where TModel: ReportModel
    {
        public ChooseReportLayoutPage(IChooseReportLayoutPageView view);
        public override void Begin();
        public override void Commit();
        public override Type GetNextPageType();

        public override bool FinishEnabled { get; }

        public override bool MoveNextEnabled { get; }
    }
}

