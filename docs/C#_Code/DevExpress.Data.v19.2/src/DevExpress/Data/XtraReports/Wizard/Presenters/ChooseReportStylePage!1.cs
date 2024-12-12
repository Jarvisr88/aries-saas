namespace DevExpress.Data.XtraReports.Wizard.Presenters
{
    using DevExpress.Data.WizardFramework;
    using DevExpress.Data.XtraReports.Wizard.Views;
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Presenters.ChooseReportStylePage<TModel> class from the DevExpress.XtraReports assembly instead.")]
    public class ChooseReportStylePage<TModel> : WizardPageBase<IChooseReportStylePageView, TModel> where TModel: ReportModel
    {
        public ChooseReportStylePage(IChooseReportStylePageView view);
        public override void Begin();
        public override void Commit();
        public override Type GetNextPageType();

        public override bool FinishEnabled { get; }

        public override bool MoveNextEnabled { get; }
    }
}

