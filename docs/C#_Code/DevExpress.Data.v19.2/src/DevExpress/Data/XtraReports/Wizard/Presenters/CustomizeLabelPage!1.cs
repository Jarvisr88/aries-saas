namespace DevExpress.Data.XtraReports.Wizard.Presenters
{
    using DevExpress.Data.WizardFramework;
    using DevExpress.Data.XtraReports.Labels;
    using DevExpress.Data.XtraReports.Wizard.Views;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Presenters.CustomizeLabelPage<TModel> class from the DevExpress.XtraReports assembly instead.")]
    public class CustomizeLabelPage<TModel> : WizardPageBase<ICustomizeLabelPageView, TModel> where TModel: ReportModel
    {
        private readonly ILabelProductRepository repository;
        private GraphicsUnit currentGraphicsUnit;

        public CustomizeLabelPage(ICustomizeLabelPageView view, ILabelProductRepository repository);
        public override void Begin();
        public override void Commit();
        private List<PaperKindViewInfo> GetPageSizeList();
        private void UpdateBaseValues();
        private void UpdateLabelsCountText();
        private void UpdatePaperKindText();
        private void View_LabelInformationChanged(object sender, EventArgs e);
        private void View_SelectedPaperKindChanged(object sender, EventArgs e);
        private void View_UnitChanged(object sender, EventArgs e);

        public override bool MoveNextEnabled { get; }

        public override bool FinishEnabled { get; }
    }
}

