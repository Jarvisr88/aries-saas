namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;
    using System.IO;

    public class PdfPageInfo : IXObjectsOwner
    {
        private PdfDocument document;
        private SizeF sizeInPoints;
        private PdfPage page;
        private PdfContents contents;
        private PdfXObjects xObjects = new PdfXObjects();

        public PdfPageInfo(SizeF sizeInPoints, PdfDocument document, PdfHashtable pdfHashtable)
        {
            this.sizeInPoints = sizeInPoints;
            this.document = document;
            this.page = this.document.Catalog.Pages.CreatePage();
            this.page.MediaBox = new PdfRectangle(0f, 0f, (float) ((int) Math.Round((double) this.sizeInPoints.Width, MidpointRounding.AwayFromZero)), (float) ((int) Math.Round((double) this.sizeInPoints.Height, MidpointRounding.AwayFromZero)));
            this.contents = new PdfContents(this.page, this.document.Compressed, pdfHashtable);
            this.contents.Register(this.document.XRef);
            this.page.InitializeContents(this.contents);
        }

        public void AddExistingXObject(PdfXObject xObject)
        {
            this.page.XObjects.AddUnique(xObject);
        }

        public void AddNewXObject(PdfXObject xObject)
        {
            this.xObjects.Add(xObject);
            this.page.XObjects.Add(xObject);
            xObject.Register(this.document.XRef);
        }

        void IXObjectsOwner.AddPattern(PdfPattern pattern)
        {
            this.page.AddPattern(pattern);
        }

        void IXObjectsOwner.AddShading(PdfShading shading)
        {
            this.page.AddShading(shading);
        }

        void IXObjectsOwner.AddTransparencyGS(PdfTransparencyGS gs)
        {
            this.page.AddTransparencyGS(gs);
        }

        public void WriteAndClose(StreamWriter writer)
        {
            this.contents.FillUp();
            this.contents.Write(writer);
            this.contents.Close();
            this.xObjects.FillUp();
            this.xObjects.Write(writer);
            this.xObjects.CloseAndClear();
        }

        public SizeF SizeInPoints =>
            this.sizeInPoints;

        public PdfPage Page =>
            this.page;

        public PdfDrawContext Context =>
            this.contents.DrawContext;
    }
}

