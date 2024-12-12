namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXUnknownBox : JPXBox
    {
        public JPXUnknownBox(PdfBigEndianStreamReader reader, int length)
        {
            reader.Skip(length);
        }
    }
}

