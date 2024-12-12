namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class TTFFileException : Exception
    {
        public TTFFileException()
        {
        }

        public TTFFileException(string message) : base(message)
        {
        }

        public TTFFileException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}

