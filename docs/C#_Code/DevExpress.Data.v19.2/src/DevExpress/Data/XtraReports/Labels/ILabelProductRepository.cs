namespace DevExpress.Data.XtraReports.Labels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Labels.ILabelProductRepository interface from the DevExpress.XtraReports assembly instead.")]
    public interface ILabelProductRepository
    {
        PaperKindData GetPaperKindData(int paperKindId);
        IEnumerable<PaperKindData> GetSortedPaperKinds();
        IEnumerable<LabelDetails> GetSortedProductDetails(int productId);
        IEnumerable<LabelProduct> GetSortedProducts();
    }
}

