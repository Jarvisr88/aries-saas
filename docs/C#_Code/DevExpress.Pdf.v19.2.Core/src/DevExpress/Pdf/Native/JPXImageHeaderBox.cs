namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXImageHeaderBox : JPXBox
    {
        public const int Type = 0x69686472;

        public JPXImageHeaderBox(PdfBigEndianStreamReader reader, int length)
        {
            if (length != 14)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int num2 = reader.ReadInt32();
            int num3 = reader.ReadInt16();
            if ((reader.ReadInt32() < 1) || ((num2 < 1) || (num3 < 1)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            byte num4 = reader.ReadByte();
            reader.Skip(3);
        }
    }
}

