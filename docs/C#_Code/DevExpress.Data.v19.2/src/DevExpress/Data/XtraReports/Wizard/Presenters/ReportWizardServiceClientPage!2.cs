namespace DevExpress.Data.XtraReports.Wizard.Presenters
{
    using DevExpress.Data.WizardFramework;
    using DevExpress.Data.XtraReports.ServiceModel;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This class is no longer used in the current Report Wizard implementation.")]
    public abstract class ReportWizardServiceClientPage<TView, TModel> : WizardPageBase<TView, TModel> where TModel: ReportModel
    {
        private Guid pageSessionId;

        protected ReportWizardServiceClientPage(TView view, IReportWizardServiceClient client);
        public override void Begin();
        public override void Commit();
        protected bool HandleError(AsyncCompletedEventArgs args, string operationContext);

        protected IReportWizardServiceClient Client { get; private set; }

        protected Guid PageSessionId { get; }
    }
}

