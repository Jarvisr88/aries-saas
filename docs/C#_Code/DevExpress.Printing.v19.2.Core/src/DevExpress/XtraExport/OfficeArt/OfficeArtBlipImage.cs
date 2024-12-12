namespace DevExpress.XtraExport.OfficeArt
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils.Zip;
    using DevExpress.XtraExport.Implementation;
    using System;
    using System.IO;
    using System.IO.Compression;

    internal class OfficeArtBlipImage : OfficeArtBlipBase
    {
        private readonly XlsPictureData pictureData;
        private byte[] compressedImageBytes;
        private const int maxUncompressedSize = 0x8000;

        public OfficeArtBlipImage(XlsPictureData pictureData)
        {
            this.pictureData = pictureData;
        }

        protected internal override int GetSize() => 
            (this.pictureData.ImageFormat != XlsImageFormat.Emf) ? ((this.pictureData.ImageBytes.Length + this.pictureData.ImageDigest.Length) + 1) : ((this.CompressedImageBytes.Length + this.pictureData.ImageDigest.Length) + 0x22);

        protected internal override void WriteCore(BinaryWriter writer)
        {
            writer.Write(this.pictureData.ImageDigest);
            if (this.pictureData.ImageFormat != XlsImageFormat.Emf)
            {
                writer.Write((byte) 0xff);
                writer.Write(this.pictureData.ImageBytes);
            }
            else
            {
                new OfficeArtBlipMetafileHeader { 
                    UncompressedSize = this.pictureData.ImageBytes.Length,
                    CompressedSize = this.CompressedImageBytes.Length,
                    Compressed = this.pictureData.ImageBytes != this.CompressedImageBytes,
                    Right = 0,
                    Left = this.pictureData.WidthInPixels,
                    Top = 0,
                    Bottom = this.pictureData.HeightInPixels,
                    HeightInEmus = this.pictureData.HeightInPixels,
                    WidthInEmus = this.pictureData.WidthInPixels
                }.Write(writer);
                writer.Write(this.CompressedImageBytes);
            }
        }

        public override int HeaderInstanceInfo =>
            (this.pictureData.ImageFormat != XlsImageFormat.Jpeg) ? ((this.pictureData.ImageFormat != XlsImageFormat.Tiff) ? ((this.pictureData.ImageFormat != XlsImageFormat.Emf) ? 0x6e0 : 980) : 0x6e4) : 0x46a;

        public override int HeaderTypeCode =>
            (this.pictureData.ImageFormat != XlsImageFormat.Jpeg) ? ((this.pictureData.ImageFormat != XlsImageFormat.Tiff) ? ((this.pictureData.ImageFormat != XlsImageFormat.Emf) ? 0xf01e : 0xf01a) : 0xf029) : 0xf01d;

        public override int HeaderVersion =>
            0;

        public override byte BlipType =>
            (this.pictureData.ImageFormat != XlsImageFormat.Jpeg) ? ((this.pictureData.ImageFormat != XlsImageFormat.Tiff) ? ((this.pictureData.ImageFormat != XlsImageFormat.Emf) ? 6 : 2) : 0x11) : 5;

        public override byte[] Digest =>
            this.pictureData.ImageDigest;

        public override int NumberOfReferences =>
            this.pictureData.NumberOfReferences;

        protected byte[] CompressedImageBytes
        {
            get
            {
                if (this.compressedImageBytes == null)
                {
                    if (this.pictureData.ImageBytes.Length <= 0x8000)
                    {
                        this.compressedImageBytes = this.pictureData.ImageBytes;
                    }
                    else
                    {
                        ChunkedMemoryStream stream = new ChunkedMemoryStream();
                        stream.WriteByte(120);
                        stream.WriteByte(1);
                        Adler32CheckSumCalculator calculator = new Adler32CheckSumCalculator();
                        uint initialCheckSumValue = calculator.InitialCheckSumValue;
                        using (DeflateStream stream2 = new DeflateStream(stream, CompressionMode.Compress, true))
                        {
                            stream2.Write(this.pictureData.ImageBytes, 0, this.pictureData.ImageBytes.Length);
                            initialCheckSumValue = calculator.UpdateCheckSum(initialCheckSumValue, this.pictureData.ImageBytes, 0, this.pictureData.ImageBytes.Length);
                        }
                        byte[] bytes = BitConverter.GetBytes(calculator.GetFinalCheckSum(initialCheckSumValue));
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                        this.compressedImageBytes = stream.ToArray();
                    }
                }
                return this.compressedImageBytes;
            }
        }
    }
}

