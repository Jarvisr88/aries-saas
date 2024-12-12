namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PdfException : Exception
    {
        public PdfException()
        {
        }

        public PdfException(string message) : base(message)
        {
        }

        public PdfException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}

