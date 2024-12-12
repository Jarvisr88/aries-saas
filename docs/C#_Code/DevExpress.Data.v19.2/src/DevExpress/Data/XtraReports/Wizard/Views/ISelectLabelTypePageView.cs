namespace DevExpress.Data.XtraReports.Wizard.Views
{
    using DevExpress.Data.XtraReports.Labels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Views.ISelectLabelTypePageView class from the DevExpress.XtraReports assembly instead.")]
    public interface ISelectLabelTypePageView
    {
        event EventHandler SelectedLabelProductChanged;

        event EventHandler SelectedLabelProductDetailsChanged;

        void FillLabelDetails(IEnumerable<LabelDetails> details);
        void FillLabelProducts(IEnumerable<LabelProduct> products);
        void UpdateLabelInformation();

        PaperKindData CurrentPaperKindData { get; set; }

        LabelProduct SelectedProduct { get; set; }

        LabelDetails SelectedDetails { get; set; }
    }
}

