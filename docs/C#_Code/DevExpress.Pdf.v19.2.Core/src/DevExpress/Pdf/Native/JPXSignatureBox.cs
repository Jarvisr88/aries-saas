namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXSignatureBox : JPXBox
    {
        public const int Type = 0x6a502020;
        private const int content = 0xd0a870a;

        public JPXSignatureBox(PdfBigEndianStreamReader reader, int length)
        {
            if ((length != 4) || (reader.ReadInt32() != 0xd0a870a))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }
    }
}

