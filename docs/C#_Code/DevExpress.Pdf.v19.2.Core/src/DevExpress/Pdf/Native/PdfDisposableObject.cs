namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfDisposableObject : IDisposable
    {
        protected PdfDisposableObject()
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);
    }
}

