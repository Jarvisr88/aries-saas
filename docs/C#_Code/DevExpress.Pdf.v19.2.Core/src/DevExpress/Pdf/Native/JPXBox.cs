namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class JPXBox
    {
        protected JPXBox()
        {
        }

        public static JPXBox Parse(PdfBigEndianStreamReader reader, JPXImage image)
        {
            int length = reader.ReadInt32();
            int num2 = reader.ReadInt32();
            int num3 = (int) (reader.Length - reader.Position);
            if (length == 0)
            {
                length = num3;
            }
            else if ((length - 8) > num3)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            if (num2 <= 0x69686472)
            {
                if (num2 == 0x636f6c72)
                {
                    return new JPXColorSpecificationBox(reader, length);
                }
                if (num2 == 0x66747970)
                {
                    return new JPXFileTypeBox(reader, length);
                }
                if (num2 == 0x69686472)
                {
                    return new JPXImageHeaderBox(reader, length);
                }
            }
            else
            {
                if (num2 == 0x6a502020)
                {
                    return new JPXSignatureBox(reader, length);
                }
                if (num2 == 0x6a703263)
                {
                    return new JPXContiguousCodeStreamBox(reader, length, image);
                }
                if (num2 == 0x6a703268)
                {
                    return new JPXJP2HeaderBox(reader, length, image);
                }
            }
            return new JPXUnknownBox(reader, length);
        }
    }
}

