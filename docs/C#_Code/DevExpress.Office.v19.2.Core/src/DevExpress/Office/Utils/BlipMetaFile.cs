namespace DevExpress.Office.Utils
{
    using DevExpress.Utils.Crypt;
    using DevExpress.Utils.Zip;
    using System;
    using System.IO;
    using System.IO.Compression;

    public abstract class BlipMetaFile : BlipBase
    {
        private const int bufferSize = 0x4000;
        private const int maxUncompressedSize = 0x8000;
        private int uncompressedSize;
        private MD4MessageDigest md4Digest;

        protected BlipMetaFile(OfficeImage image) : base(image)
        {
        }

        protected BlipMetaFile(BinaryReader reader, OfficeArtRecordHeader header) : base(reader, header)
        {
        }

        private void CalcMD4Digest(Stream stream)
        {
            int num2;
            byte[] buffer = new byte[0x4000];
            MD4HashCalculator calculator = new MD4HashCalculator();
            uint[] initialCheckSumValue = calculator.InitialCheckSumValue;
            for (int i = (int) stream.Length; i > 0; i -= num2)
            {
                num2 = Math.Min(i, 0x4000);
                stream.Read(buffer, 0, num2);
                initialCheckSumValue = calculator.UpdateCheckSum(initialCheckSumValue, buffer, 0, num2);
            }
            stream.Position = 0L;
            this.md4Digest = new MD4MessageDigest(calculator.GetFinalCheckSum(initialCheckSumValue));
        }

        protected internal override int CalcMD4MessageOffset(int uidInfo)
        {
            if (uidInfo == this.SingleMD4Message)
            {
                return 0x10;
            }
            if (uidInfo == this.DoubleMD4Message)
            {
                return 0x20;
            }
            Exceptions.ThrowInternalException();
            return 0;
        }

        protected override Stream CreateImageBytesStream()
        {
            Stream stream = base.CreateImageBytesStream();
            this.uncompressedSize = (int) stream.Length;
            if (this.uncompressedSize <= 0x8000)
            {
                this.CalcMD4Digest(stream);
                return stream;
            }
            ChunkedMemoryStream stream2 = new ChunkedMemoryStream();
            stream2.WriteByte(120);
            stream2.WriteByte(1);
            Adler32CheckSumCalculator calculator = new Adler32CheckSumCalculator();
            uint initialCheckSumValue = calculator.InitialCheckSumValue;
            MD4HashCalculator calculator2 = new MD4HashCalculator();
            uint[] numArray = calculator2.InitialCheckSumValue;
            using (DeflateStream stream3 = new DeflateStream(stream2, CompressionMode.Compress, true))
            {
                int num3;
                byte[] buffer = new byte[0x4000];
                for (int i = this.uncompressedSize; i > 0; i -= num3)
                {
                    num3 = Math.Min(i, 0x4000);
                    stream.Read(buffer, 0, num3);
                    stream3.Write(buffer, 0, num3);
                    initialCheckSumValue = calculator.UpdateCheckSum(initialCheckSumValue, buffer, 0, num3);
                    numArray = calculator2.UpdateCheckSum(numArray, buffer, 0, num3);
                }
            }
            this.md4Digest = new MD4MessageDigest(calculator2.GetFinalCheckSum(numArray));
            byte[] bytes = BitConverter.GetBytes(calculator.GetFinalCheckSum(initialCheckSumValue));
            stream2.Write(bytes, 0, bytes.Length);
            stream2.Flush();
            stream2.Position = 0L;
            return stream2;
        }

        protected override DocMetafileHeader CreateMetafileHeader()
        {
            DocMetafileHeader header = new DocMetafileHeader();
            int imageBytesLength = base.ImageBytesLength;
            int width = base.Image.SizeInPixels.Width;
            int height = base.Image.SizeInPixels.Height;
            header.Compressed = imageBytesLength != this.uncompressedSize;
            header.CompressedSize = imageBytesLength;
            header.UncompressedSize = this.uncompressedSize;
            header.Left = 0;
            header.Right = width;
            header.Top = 0;
            header.Bottom = height;
            header.HeightInEmus = this.PixelsToEmu(height);
            header.WidthInEmus = this.PixelsToEmu(width);
            return header;
        }

        public override int GetSize() => 
            0x3a + base.ImageBytesLength;

        private int PixelsToEmu(int pixels) => 
            pixels * 0x2535;

        protected override void Read(BinaryReader reader, OfficeArtRecordHeader header)
        {
            long offset = reader.BaseStream.Position + header.Length;
            int num2 = this.CalcMD4MessageOffset(header.InstanceInfo);
            reader.BaseStream.Seek((long) num2, SeekOrigin.Current);
            DocMetafileReader reader2 = new DocMetafileReader();
            reader2.Read(reader);
            if ((reader.BaseStream.Position != offset) && (reader.BaseStream.Length >= offset))
            {
                reader.BaseStream.Seek(offset, SeekOrigin.Begin);
            }
            this.SetImage(reader2);
        }

        public override MD4MessageDigest Write(BinaryWriter writer)
        {
            this.CreateRecordHeader().Write(writer);
            writer.Write(this.md4Digest.ToArray());
            this.CreateMetafileHeader().Write(writer);
            byte[] buffer = new byte[0x4000];
            int imageBytesLength = base.ImageBytesLength;
            Stream imageBytesStream = base.ImageBytesStream;
            while (imageBytesLength > 0)
            {
                int count = Math.Min(imageBytesLength, 0x4000);
                imageBytesStream.Read(buffer, 0, count);
                writer.Write(buffer, 0, count);
                imageBytesLength -= count;
            }
            return this.md4Digest;
        }
    }
}

