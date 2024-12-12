namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;

    public static class PdfDCTDataValidator
    {
        public static byte[] ChangeImageHeight(byte[] imageData, int newHeight)
        {
            using (MemoryStream stream = new MemoryStream(imageData))
            {
                PdfBigEndianStreamReader reader = new PdfBigEndianStreamReader(stream);
                while (true)
                {
                    if (reader.ReadByte() != 0xff)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    byte num = reader.ReadByte();
                    if ((((num >= 0xc0) && (num <= 0xc3)) || ((num >= 0xc5) && (num <= 0xc7))) || ((num >= 0xc9) && (num <= 0xcb)))
                    {
                        break;
                    }
                    if ((num != 0xd8) && (num != 0xd9))
                    {
                        stream.Position += reader.ReadInt16();
                    }
                }
                int index = ((int) stream.Position) + 3;
                byte[] buffer = (byte[]) imageData.Clone();
                buffer[index] = (byte) (newHeight >> 8);
                buffer[index + 1] = (byte) newHeight;
                return buffer;
            }
        }
    }
}

