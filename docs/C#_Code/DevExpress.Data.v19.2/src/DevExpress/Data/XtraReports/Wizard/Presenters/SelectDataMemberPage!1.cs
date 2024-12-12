namespace DevExpress.Data.XtraReports.Wizard.Presenters
{
    using DevExpress.Data.XtraReports.ServiceModel;
    using DevExpress.Data.XtraReports.Wizard.Views;
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This class is no longer used in the current Report Wizard implementation.")]
    public class SelectDataMemberPage<TModel> : ReportWizardServiceClientPage<ISelectDataMemberPageView, TModel> where TModel: ReportModel
    {
        public SelectDataMemberPage(ISelectDataMemberPageView view, IReportWizardServiceClient client);
        public override void Begin();
        public override void Commit();
        public override Type GetNextPageType();
        private void operation_GetDataMembersCompleted(object sender, AsyncCompletedEventArgs e);
        private void view_SelectedDataMemberChanged(object sender, EventArgs e);

        public override bool MoveNextEnabled { get; }
    }
}

