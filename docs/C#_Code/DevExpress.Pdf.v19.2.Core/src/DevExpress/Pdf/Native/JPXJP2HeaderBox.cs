namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXJP2HeaderBox : JPXBox
    {
        public const int Type = 0x6a703268;

        public JPXJP2HeaderBox(PdfBigEndianStreamReader reader, int length, JPXImage image)
        {
            long num = reader.Position + length;
            if (!(Parse(reader, image) is JPXImageHeaderBox))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            bool flag = false;
            while (reader.Position < num)
            {
                if (!(Parse(reader, image) is JPXColorSpecificationBox))
                {
                    continue;
                }
                flag = true;
            }
            if ((reader.Position > num) || !flag)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }
    }
}

