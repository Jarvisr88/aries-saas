namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXFileTypeBox : JPXBox
    {
        public const int Type = 0x66747970;

        public JPXFileTypeBox(PdfBigEndianStreamReader reader, int length)
        {
            if ((length < 12) || ((length % 4) != 0))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            reader.Skip(length);
        }
    }
}

