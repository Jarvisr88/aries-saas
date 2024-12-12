namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfFitBBoxHorizontallyDestination : PdfDestination
    {
        internal const string Name = "FitBH";
        private readonly double? top;

        private PdfFitBBoxHorizontallyDestination(PdfFitBBoxHorizontallyDestination destination, int objectNumber) : base(destination, objectNumber)
        {
            this.top = destination.top;
        }

        public PdfFitBBoxHorizontallyDestination(PdfPage page, double? top) : base(page)
        {
            this.top = top;
        }

        internal PdfFitBBoxHorizontallyDestination(PdfDocumentCatalog documentCatalog, object pageObject, double? top) : base(documentCatalog, pageObject)
        {
            this.top = top;
        }

        protected override void AddWriteableParameters(IList<object> parameters)
        {
            parameters.Add(new PdfName("FitBH"));
            AddParameter(parameters, this.top);
        }

        protected override PdfDestination CreateDuplicate(int objectNumber) => 
            new PdfFitBBoxHorizontallyDestination(this, objectNumber);

        protected internal override PdfTarget CreateTarget(IList<PdfPage> pages) => 
            new PdfTarget(PdfTargetMode.FitBBoxHorizontally, base.CalculatePageIndex(pages), null, base.ValidateVerticalCoordinate(this.top));

        public double? Top =>
            this.top;
    }
}

