namespace DevExpress.Pdf.Native
{
    using System;

    public static class PdfTrueTypeCollectionFontFile
    {
        public static PdfFontFile ReadFontFile(byte[] fileData, int index)
        {
            using (PdfBinaryStream stream = new PdfBinaryStream(fileData))
            {
                long num = 0L;
                if (0x74746366 == stream.ReadInt())
                {
                    stream.ReadInt();
                    long num2 = (long) ((ulong) stream.ReadInt());
                    if (index >= num2)
                    {
                        throw new Exception();
                    }
                    stream.Position += index * 4;
                    num = (long) ((ulong) stream.ReadInt());
                }
                stream.Position = num;
                return new PdfFontFile(stream);
            }
        }

        public static PdfFontFile ReadFontFile(byte[] fileData, string fontName)
        {
            PdfFontFile file2;
            using (PdfBinaryStream stream = new PdfBinaryStream(fileData))
            {
                byte[] buffer = stream.ReadArray(4);
                byte[] buffer2 = stream.ReadArray(4);
                long num = (long) ((ulong) stream.ReadInt());
                long[] numArray = new long[num];
                int index = 0;
                while (true)
                {
                    if (index >= num)
                    {
                        long num3 = 0L;
                        while (true)
                        {
                            if (num3 >= num)
                            {
                                file2 = null;
                            }
                            else
                            {
                                stream.Position = numArray[(int) ((IntPtr) num3)];
                                PdfFontFile file = new PdfFontFile(stream);
                                if ((file.Name == null) || !file.Name.ContainsFontFamilyName(fontName))
                                {
                                    file.Dispose();
                                    num3 += 1L;
                                    continue;
                                }
                                file2 = file;
                            }
                            break;
                        }
                        break;
                    }
                    numArray[index] = (long) ((ulong) stream.ReadInt());
                    index++;
                }
            }
            return file2;
        }
    }
}

