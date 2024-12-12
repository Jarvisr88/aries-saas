namespace DevExpress.Office.Utils
{
    using DevExpress.Office.PInvoke;
    using System;
    using System.Drawing;
    using System.IO;
    using System.IO.Compression;

    public class DocMetafileReader
    {
        private DocMetafileHeader metafileHeader;
        private OfficeImage image;

        private void CheckCompressedData(BinaryReader reader)
        {
            byte num3 = (byte) (reader.ReadByte() & 0x20);
            if ((((byte) (reader.ReadByte() & 15)) != 8) | (num3 != 0))
            {
                OfficeArtExceptions.ThrowInvalidContent();
            }
        }

        public void Read(BinaryReader reader)
        {
            this.metafileHeader = DocMetafileHeader.FromStream(reader);
            if (!this.metafileHeader.Compressed)
            {
                using (MemoryStream stream = new MemoryStream(reader.ReadBytes(this.metafileHeader.UncompressedSize)))
                {
                    this.image = OfficeImage.CreateImage(stream);
                }
            }
            else
            {
                this.CheckCompressedData(reader);
                byte[] buffer = reader.ReadBytes(this.metafileHeader.CompressedSize - 2);
                using (MemoryStream stream2 = new MemoryStream(buffer))
                {
                    int widthInHundredthsOfMillimeter;
                    int heightInHundredthsOfMillimeter;
                    buffer = new byte[this.metafileHeader.UncompressedSize];
                    new DeflateStream(stream2, CompressionMode.Decompress).Read(buffer, 0, this.metafileHeader.UncompressedSize);
                    MemoryStream imageStream = new MemoryStream();
                    imageStream.Write(buffer, 0, buffer.Length);
                    if ((this.MetafileHeader.HeightInEmus == 0) && (this.MetafileHeader.WidthInEmus == 0))
                    {
                        widthInHundredthsOfMillimeter = this.MetafileHeader.Right - this.MetafileHeader.Left;
                        heightInHundredthsOfMillimeter = this.MetafileHeader.Bottom - this.MetafileHeader.Top;
                    }
                    else
                    {
                        widthInHundredthsOfMillimeter = this.MetafileHeader.WidthInHundredthsOfMillimeter;
                        heightInHundredthsOfMillimeter = this.MetafileHeader.HeightInHundredthsOfMillimeter;
                    }
                    OfficeNativeImage image2 = OfficeImage.CreateImage(new MemoryStreamBasedImage(MetafileHelper.CreateMetafile(imageStream, Win32.MapMode.Anisotropic, widthInHundredthsOfMillimeter, heightInHundredthsOfMillimeter), imageStream));
                    Size size = new Size(this.MetafileHeader.WidthInHundredthsOfMillimeter, this.MetafileHeader.HeightInHundredthsOfMillimeter);
                    ((OfficeMetafileImageWin) image2).MetafileSizeInHundredthsOfMillimeter = size;
                    this.image = image2;
                }
            }
        }

        public DocMetafileHeader MetafileHeader =>
            this.metafileHeader;

        public OfficeImage Image =>
            this.image;
    }
}

