namespace DevExpress.Data.XtraReports.Wizard.Presenters
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.Data.XtraReports.ServiceModel;
    using DevExpress.Data.XtraReports.Wizard.Views;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This class is no longer used in the current Report Wizard implementation.")]
    public class SelectDataSourcePage<TModel> : ReportWizardServiceClientPage<ISelectDataSourcePageView, TModel> where TModel: ReportModel
    {
        private IEnumerable<DataSourceInfo> dataSources;

        public SelectDataSourcePage(ISelectDataSourcePageView view, IReportWizardServiceClient client);
        public override void Begin();
        private void client_GetDataSourcesCompleted(object sender, ScalarOperationCompletedEventArgs<IEnumerable<DataSourceInfo>> e);
        public override void Commit();
        public override Type GetNextPageType();
        private void view_SelectedDataSourceChanged(object sender, EventArgs e);

        public override bool MoveNextEnabled { get; }
    }
}

