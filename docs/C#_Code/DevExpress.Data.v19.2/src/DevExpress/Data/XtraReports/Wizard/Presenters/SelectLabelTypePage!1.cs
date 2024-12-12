namespace DevExpress.Data.XtraReports.Wizard.Presenters
{
    using DevExpress.Data.WizardFramework;
    using DevExpress.Data.XtraReports.Labels;
    using DevExpress.Data.XtraReports.Wizard.Views;
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Presenters.SelectLabelTypePage<TModel> class from the DevExpress.XtraReports assembly instead.")]
    public class SelectLabelTypePage<TModel> : WizardPageBase<ISelectLabelTypePageView, TModel> where TModel: ReportModel
    {
        private readonly ILabelProductRepository repository;

        public SelectLabelTypePage(ISelectLabelTypePageView view, ILabelProductRepository repository);
        public override void Begin();
        public override void Commit();
        public override Type GetNextPageType();
        private void View_SelectedLabelProductChanged(object sender, EventArgs e);
        private void View_SelectedLabelProductDetailsChanged(object sender, EventArgs e);

        public override bool MoveNextEnabled { get; }

        public override bool FinishEnabled { get; }
    }
}

