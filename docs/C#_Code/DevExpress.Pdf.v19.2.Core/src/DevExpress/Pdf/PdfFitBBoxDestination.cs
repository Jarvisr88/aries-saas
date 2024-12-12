namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfFitBBoxDestination : PdfDestination
    {
        internal const string Name = "FitB";

        public PdfFitBBoxDestination(PdfPage page) : base(page)
        {
        }

        internal PdfFitBBoxDestination(PdfDocumentCatalog documentCatalog, object pageObject) : base(documentCatalog, pageObject)
        {
        }

        private PdfFitBBoxDestination(PdfFitBBoxDestination destination, int objectNumber) : base(destination, objectNumber)
        {
        }

        protected override void AddWriteableParameters(IList<object> parameters)
        {
            parameters.Add(new PdfName("FitB"));
        }

        protected override PdfDestination CreateDuplicate(int objectNumber) => 
            new PdfFitBBoxDestination(this, objectNumber);

        protected internal override PdfTarget CreateTarget(IList<PdfPage> pages) => 
            new PdfTarget(PdfTargetMode.FitBBox, base.CalculatePageIndex(pages));
    }
}

