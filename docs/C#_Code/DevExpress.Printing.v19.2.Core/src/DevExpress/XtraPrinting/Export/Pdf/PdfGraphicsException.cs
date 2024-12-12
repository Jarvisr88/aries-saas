namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PdfGraphicsException : Exception
    {
        public PdfGraphicsException()
        {
        }

        public PdfGraphicsException(string message) : base(message)
        {
        }

        public PdfGraphicsException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}

