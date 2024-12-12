namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfFitDestination : PdfDestination
    {
        internal const string Name = "Fit";

        public PdfFitDestination(PdfPage page) : base(page)
        {
        }

        internal PdfFitDestination(PdfDocumentCatalog documentCatalog, object pageObject) : base(documentCatalog, pageObject)
        {
        }

        private PdfFitDestination(PdfFitDestination destination, int objectNumber) : base(destination, objectNumber)
        {
        }

        protected override void AddWriteableParameters(IList<object> parameters)
        {
            parameters.Add(new PdfName("Fit"));
        }

        protected override PdfDestination CreateDuplicate(int objectNumber) => 
            new PdfFitDestination(this, objectNumber);

        protected internal override PdfTarget CreateTarget(IList<PdfPage> pages) => 
            new PdfTarget(PdfTargetMode.Fit, base.CalculatePageIndex(pages));
    }
}

