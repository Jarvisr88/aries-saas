namespace DevExpress.Pdf
{
    using System;

    public class PdfIncorrectPasswordException : Exception
    {
        internal PdfIncorrectPasswordException() : base(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectPdfPassword))
        {
        }
    }
}

