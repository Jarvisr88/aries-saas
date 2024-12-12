namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXColorSpecificationBox : JPXBox
    {
        public const int Type = 0x636f6c72;

        public JPXColorSpecificationBox(PdfBigEndianStreamReader reader, int length)
        {
            if (length < 3)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            byte num = reader.ReadByte();
            reader.Skip(2);
            if (num != 1)
            {
                reader.Skip(length - 3);
            }
            else
            {
                if (length != 7)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                reader.Skip(4);
            }
        }
    }
}

