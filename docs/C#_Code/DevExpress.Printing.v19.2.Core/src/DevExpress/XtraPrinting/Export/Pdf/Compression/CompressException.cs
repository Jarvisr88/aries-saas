namespace DevExpress.XtraPrinting.Export.Pdf.Compression
{
    using System;

    public class CompressException : Exception
    {
        public CompressException()
        {
        }

        public CompressException(string message) : base(message)
        {
        }

        public CompressException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}

