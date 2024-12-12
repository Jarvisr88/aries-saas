namespace DevExpress.Emf
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security;

    public class EmfPlusImage : EmfPlusObject, IDisposable
    {
        private System.Drawing.Image image;
        private byte[] imageData;

        public EmfPlusImage(EmfPlusReader reader)
        {
            reader.ReadInt32();
            EmfPlusImageDataType type = (EmfPlusImageDataType) reader.ReadInt32();
            if (type != EmfPlusImageDataType.ImageDataTypeBitmap)
            {
                if (type == EmfPlusImageDataType.ImageDataTypeMetafile)
                {
                    if (reader.ReadInt32() == 2)
                    {
                        reader.ReadBytes(0x18);
                    }
                    byte[] buffer = reader.ReadBytes(reader.ReadInt32());
                    this.image = System.Drawing.Image.FromStream(new MemoryStream(buffer));
                }
            }
            else
            {
                int width = reader.ReadInt32();
                int height = reader.ReadInt32();
                int stride = reader.ReadInt32();
                int num4 = reader.ReadInt32();
                PixelFormat pixelFormat = (num4 != 0x1a400e) ? ((PixelFormat) num4) : PixelFormat.Format64bppPArgb;
                byte[] bitmapData = reader.ReadBytes((int) (reader.BaseStream.Length - reader.BaseStream.Position));
                if (reader.ReadInt32() != 0)
                {
                    this.imageData = bitmapData;
                }
                else
                {
                    this.image = this.CreateBitmap(width, height, stride, pixelFormat, bitmapData);
                }
            }
        }

        public EmfPlusImage(System.Drawing.Image image)
        {
            this.image = image;
        }

        [SecuritySafeCritical]
        private Bitmap CreateBitmap(int width, int height, int stride, PixelFormat pixelFormat, byte[] bitmapData)
        {
            Bitmap bitmap = new Bitmap(width, height, pixelFormat);
            int startIndex = 0;
            if ((pixelFormat & PixelFormat.Indexed) != PixelFormat.Undefined)
            {
                using (BinaryReader reader = new BinaryReader(new MemoryStream(bitmapData)))
                {
                    int num2 = reader.ReadInt32();
                    int num3 = reader.ReadInt32();
                    ColorPalette palette = bitmap.Palette;
                    Color[] entries = palette.Entries;
                    int index = 0;
                    while (true)
                    {
                        if (index >= num3)
                        {
                            startIndex += 4 * (2 + num3);
                            bitmap.Palette = palette;
                            break;
                        }
                        entries[index] = Color.FromArgb(reader.ReadInt32());
                        index++;
                    }
                }
            }
            BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, pixelFormat);
            Marshal.Copy(bitmapData, startIndex, bitmapdata.Scan0, stride * height);
            bitmap.UnlockBits(bitmapdata);
            return bitmap;
        }

        public void Dispose()
        {
            if (this.image != null)
            {
                this.image.Dispose();
            }
        }

        private void SaveImageToStream(Stream stream)
        {
            ImageFormat rawFormat = this.image.RawFormat;
            ImageFormat format = (rawFormat.Equals(ImageFormat.Gif) || (rawFormat.Equals(ImageFormat.Jpeg) || (rawFormat.Equals(ImageFormat.Png) || rawFormat.Equals(ImageFormat.Tiff)))) ? rawFormat : ImageFormat.Png;
            this.image.Save(stream, format);
        }

        public override void Write(EmfContentWriter writer)
        {
            writer.Write(-608169982);
            writer.Write(1);
            writer.Write(new byte[0x10]);
            writer.Write(1);
            if (this.imageData != null)
            {
                writer.Write(this.imageData);
            }
            else
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    this.SaveImageToStream(stream);
                    writer.Write(stream.GetBuffer(), 0, (int) stream.Length);
                }
            }
        }

        public System.Drawing.Image Image
        {
            get
            {
                if (this.image == null)
                {
                    this.image = new Bitmap(new MemoryStream(this.imageData));
                    this.imageData = null;
                }
                return this.image;
            }
        }

        public override EmfPlusObjectType Type =>
            EmfPlusObjectType.ObjectTypeImage;

        public override int Size
        {
            get
            {
                int length;
                if (this.imageData != null)
                {
                    length = this.imageData.Length;
                }
                else
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        this.SaveImageToStream(stream);
                        length = (int) stream.Length;
                    }
                }
                return (0x1c + length);
            }
        }
    }
}

