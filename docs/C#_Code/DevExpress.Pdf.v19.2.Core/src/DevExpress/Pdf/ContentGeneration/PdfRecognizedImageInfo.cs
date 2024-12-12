namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf.Native;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfRecognizedImageInfo
    {
        private readonly int width;
        private readonly int height;
        private readonly PdfRecognizedImageFormat type;
        public static PdfRecognizedImageInfo DetectImage(Stream data)
        {
            PdfRecognizedImageInfo info;
            data.Position = 0L;
            try
            {
                PdfBigEndianStreamReader reader = new PdfBigEndianStreamReader(data);
                byte num = reader.ReadByte();
                if (num > 0x49)
                {
                    if (num == 0x4d)
                    {
                        if ((reader.ReadByte() == 0x4d) && (reader.ReadInt16() == 0x2a))
                        {
                            return new PdfRecognizedImageInfo(PdfRecognizedImageFormat.Tiff);
                        }
                    }
                    else if (num == 0xd7)
                    {
                        if ((reader.ReadByte() == 0xcd) && ((reader.ReadByte() == 0xc6) && (reader.ReadByte() == 0x9a)))
                        {
                            return new PdfRecognizedImageInfo(PdfRecognizedImageFormat.Metafile);
                        }
                    }
                    else if ((num == 0xff) && (reader.ReadByte() == 0xd8))
                    {
                        int height = 0;
                        int width = 0;
                        int num4 = 0;
                        int num5 = 0;
                        long length = reader.Length;
                        while (true)
                        {
                            if (reader.Finish)
                            {
                                info = (num5 == 3) ? new PdfRecognizedImageInfo((num4 == 0) ? PdfRecognizedImageFormat.RGBJpeg : PdfRecognizedImageFormat.Unrecognized, width, height) : ((num5 == 4) ? (((num4 == 0) || (num4 == 2)) ? new PdfRecognizedImageInfo(PdfRecognizedImageFormat.YCCKJpeg, width, height) : new PdfRecognizedImageInfo(PdfRecognizedImageFormat.Unrecognized, width, height)) : new PdfRecognizedImageInfo(PdfRecognizedImageFormat.Unrecognized, width, height));
                                break;
                            }
                            int num7 = reader.ReadInt16();
                            int num8 = reader.ReadInt16();
                            switch (num7)
                            {
                                case 0xffc0:
                                case 0xffc1:
                                case 0xffc2:
                                case 0xffc3:
                                case 0xffc5:
                                case 0xffc6:
                                case 0xffc7:
                                case 0xffc9:
                                case 0xffca:
                                case 0xffcb:
                                case 0xffcd:
                                case 0xffce:
                                case 0xffcf:
                                    reader.ReadByte();
                                    height = reader.ReadInt16();
                                    width = reader.ReadInt16();
                                    num5 = reader.ReadByte();
                                    num8 -= 6;
                                    break;

                                case 0xffc4:
                                case 0xffc8:
                                case 0xffcc:
                                    break;

                                default:
                                    if ((num7 == 0xffee) && (num8 >= 12))
                                    {
                                        reader.Skip(11);
                                        num4 = reader.ReadByte();
                                        num8 -= 12;
                                    }
                                    break;
                            }
                            num8 -= 2;
                            if (num8 > 0)
                            {
                                reader.Skip(Math.Min((int) (length - reader.Position), num8));
                            }
                        }
                        return info;
                    }
                }
                else if (num == 1)
                {
                    if (reader.ReadByte() == 0)
                    {
                        num = reader.ReadByte();
                        if (((num == 0) || (num == 9)) && (reader.ReadByte() == 0))
                        {
                            return new PdfRecognizedImageInfo(PdfRecognizedImageFormat.Metafile);
                        }
                    }
                }
                else if (num == 2)
                {
                    if ((reader.ReadByte() == 0) && ((reader.ReadByte() == 9) && (reader.ReadByte() == 0)))
                    {
                        return new PdfRecognizedImageInfo(PdfRecognizedImageFormat.Metafile);
                    }
                }
                else if ((num == 0x49) && ((reader.ReadByte() == 0x49) && ((reader.ReadByte() == 0x2a) && (reader.ReadByte() == 0))))
                {
                    return new PdfRecognizedImageInfo(PdfRecognizedImageFormat.Tiff);
                }
                info = new PdfRecognizedImageInfo(PdfRecognizedImageFormat.Unrecognized);
            }
            finally
            {
                data.Position = 0L;
            }
            return info;
        }

        public int JpegWidth =>
            this.width;
        public int JpegHeight =>
            this.height;
        public PdfRecognizedImageFormat Type =>
            this.type;
        public PdfRecognizedImageInfo(PdfRecognizedImageFormat type) : this(type, 0, 0)
        {
        }

        public PdfRecognizedImageInfo(PdfRecognizedImageFormat type, int width, int height)
        {
            this.type = type;
            this.width = width;
            this.height = height;
        }
    }
}

