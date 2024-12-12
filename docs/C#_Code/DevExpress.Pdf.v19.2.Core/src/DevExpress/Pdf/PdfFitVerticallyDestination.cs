namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfFitVerticallyDestination : PdfDestination
    {
        internal const string Name = "FitV";
        private readonly double? left;

        private PdfFitVerticallyDestination(PdfFitVerticallyDestination destination, int objectNumber) : base(destination, objectNumber)
        {
            this.left = destination.left;
        }

        public PdfFitVerticallyDestination(PdfPage page, double? left) : base(page)
        {
            this.left = left;
        }

        internal PdfFitVerticallyDestination(PdfDocumentCatalog documentCatalog, object pageObject, double? left) : base(documentCatalog, pageObject)
        {
            this.left = left;
        }

        protected override void AddWriteableParameters(IList<object> parameters)
        {
            parameters.Add(new PdfName("FitV"));
            AddParameter(parameters, this.left);
        }

        protected override PdfDestination CreateDuplicate(int objectNumber) => 
            new PdfFitVerticallyDestination(this, objectNumber);

        protected internal override PdfTarget CreateTarget(IList<PdfPage> pages) => 
            new PdfTarget(PdfTargetMode.FitVertically, base.CalculatePageIndex(pages), this.left, null);

        public double? Left =>
            this.left;
    }
}

