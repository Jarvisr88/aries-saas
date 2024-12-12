namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfFitBBoxVerticallyDestination : PdfDestination
    {
        internal const string Name = "FitBV";
        private readonly double? left;

        private PdfFitBBoxVerticallyDestination(PdfFitBBoxVerticallyDestination destination, int objectNumber) : base(destination, objectNumber)
        {
            this.left = destination.left;
        }

        public PdfFitBBoxVerticallyDestination(PdfPage page, double? left) : base(page)
        {
            this.left = left;
        }

        internal PdfFitBBoxVerticallyDestination(PdfDocumentCatalog documentCatalog, object pageObject, double? left) : base(documentCatalog, pageObject)
        {
            this.left = left;
        }

        protected override void AddWriteableParameters(IList<object> parameters)
        {
            parameters.Add(new PdfName("FitBV"));
            AddParameter(parameters, this.left);
        }

        protected override PdfDestination CreateDuplicate(int objectNumber) => 
            new PdfFitBBoxVerticallyDestination(this, objectNumber);

        protected internal override PdfTarget CreateTarget(IList<PdfPage> pages) => 
            new PdfTarget(PdfTargetMode.FitBBoxVertically, base.CalculatePageIndex(pages), this.left, null);

        public double? Left =>
            this.left;
    }
}

