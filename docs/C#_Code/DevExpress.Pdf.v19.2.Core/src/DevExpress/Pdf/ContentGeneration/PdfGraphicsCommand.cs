namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfGraphicsCommand : PdfDisposableObject
    {
        protected PdfGraphicsCommand()
        {
        }

        protected override void Dispose(bool disposing)
        {
        }

        public abstract void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page);
    }
}

