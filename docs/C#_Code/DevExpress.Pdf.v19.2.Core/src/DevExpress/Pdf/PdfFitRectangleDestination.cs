namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfFitRectangleDestination : PdfDestination
    {
        internal const string Name = "FitR";
        private readonly PdfRectangle rectangle;

        private PdfFitRectangleDestination(PdfFitRectangleDestination destination, int objectNumber) : base(destination, objectNumber)
        {
            PdfRectangle rectangle = destination.rectangle;
            this.rectangle = (rectangle == null) ? null : new PdfRectangle(rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Top);
        }

        public PdfFitRectangleDestination(PdfPage page, PdfRectangle rectangle) : base(page)
        {
            this.rectangle = rectangle;
        }

        internal PdfFitRectangleDestination(PdfDocumentCatalog documentCatalog, object pageObject, PdfRectangle rectangle) : base(documentCatalog, pageObject)
        {
            this.rectangle = rectangle;
        }

        protected override void AddWriteableParameters(IList<object> parameters)
        {
            parameters.Add(new PdfName("FitR"));
            parameters.Add(this.rectangle.Left);
            parameters.Add(this.rectangle.Bottom);
            parameters.Add(this.rectangle.Right);
            parameters.Add(this.rectangle.Top);
        }

        protected override PdfDestination CreateDuplicate(int objectNumber) => 
            new PdfFitRectangleDestination(this, objectNumber);

        protected internal override PdfTarget CreateTarget(IList<PdfPage> pages) => 
            new PdfTarget(PdfTargetMode.FitRectangle, base.CalculatePageIndex(pages), this.rectangle);

        public PdfRectangle Rectangle =>
            this.rectangle;
    }
}

