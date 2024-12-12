namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfFitHorizontallyDestination : PdfDestination
    {
        internal const string Name = "FitH";
        private readonly double? top;

        private PdfFitHorizontallyDestination(PdfFitHorizontallyDestination destination, int objectNumber) : base(destination, objectNumber)
        {
            this.top = destination.top;
        }

        public PdfFitHorizontallyDestination(PdfPage page, double? top) : base(page)
        {
            this.top = top;
        }

        internal PdfFitHorizontallyDestination(PdfDocumentCatalog documentCatalog, object pageObject, double? top) : base(documentCatalog, pageObject)
        {
            this.top = top;
        }

        protected override void AddWriteableParameters(IList<object> parameters)
        {
            parameters.Add(new PdfName("FitH"));
            AddParameter(parameters, this.top);
        }

        protected override PdfDestination CreateDuplicate(int objectNumber) => 
            new PdfFitHorizontallyDestination(this, objectNumber);

        protected internal override PdfTarget CreateTarget(IList<PdfPage> pages) => 
            new PdfTarget(PdfTargetMode.FitHorizontally, base.CalculatePageIndex(pages), null, base.ValidateVerticalCoordinate(this.top));

        public double? Top =>
            this.top;
    }
}

