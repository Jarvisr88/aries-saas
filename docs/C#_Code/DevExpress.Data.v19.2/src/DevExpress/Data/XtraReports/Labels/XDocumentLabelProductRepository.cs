namespace DevExpress.Data.XtraReports.Labels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Xml.Linq;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Labels.XDocumentLabelProductRepository class from the DevExpress.XtraReports assembly instead.")]
    public class XDocumentLabelProductRepository : ILabelProductRepository
    {
        private readonly XDocument document;
        private XNamespace ns;

        public XDocumentLabelProductRepository(XDocument document);
        private PaperKindData CreatePaperKindData(XElement xElement);
        public PaperKindData GetPaperKindData(int id);
        public IEnumerable<PaperKindData> GetSortedPaperKinds();
        public IEnumerable<LabelDetails> GetSortedProductDetails(int productId);
        public IEnumerable<LabelProduct> GetSortedProducts();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly XDocumentLabelProductRepository.<>c <>9;
            public static Func<LabelProduct, string> <>9__3_1;
            public static Func<LabelDetails, string> <>9__4_3;
            public static Func<PaperKindData, string> <>9__5_1;

            static <>c();
            internal string <GetSortedPaperKinds>b__5_1(PaperKindData x);
            internal string <GetSortedProductDetails>b__4_3(LabelDetails d);
            internal string <GetSortedProducts>b__3_1(LabelProduct p);
        }
    }
}

