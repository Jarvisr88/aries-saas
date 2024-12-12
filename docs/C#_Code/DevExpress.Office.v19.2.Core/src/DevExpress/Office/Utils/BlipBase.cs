namespace DevExpress.Office.Utils
{
    using DevExpress.Utils;
    using DevExpress.Utils.Crypt;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class BlipBase : IDisposable
    {
        public const int MD4MessageSize = 0x10;
        public const int TagSize = 1;
        internal const int MetafileHeaderSize = 0x22;
        internal const byte DefaultTagValue = 0xff;
        private OfficeImage image;
        private Stream imageBytesStream;
        private DocMetafileHeader metafileHeader;

        protected BlipBase()
        {
            this.TagValue = 0xff;
        }

        protected BlipBase(OfficeImage image)
        {
            this.TagValue = 0xff;
            this.SetImage(image);
        }

        protected BlipBase(BinaryReader reader, OfficeArtRecordHeader header)
        {
            Guard.ArgumentNotNull(reader, "reader");
            this.TagValue = 0xff;
            this.Read(reader, header);
        }

        protected internal virtual int CalcMD4MessageOffset(int uidInfo)
        {
            if (uidInfo == this.SingleMD4Message)
            {
                return 0x11;
            }
            if (uidInfo == this.DoubleMD4Message)
            {
                return 0x21;
            }
            OfficeArtExceptions.ThrowInvalidContent();
            return 0;
        }

        protected virtual Stream CreateImageBytesStream() => 
            this.image?.GetImageBytesStreamSafe(this.Format);

        protected virtual DocMetafileHeader CreateMetafileHeader()
        {
            DocMetafileHeader header = new DocMetafileHeader();
            int imageBytesLength = this.ImageBytesLength;
            int width = this.Image.SizeInPixels.Width;
            int height = this.Image.SizeInPixels.Height;
            header.Compressed = false;
            header.CompressedSize = imageBytesLength;
            header.UncompressedSize = imageBytesLength;
            header.Right = 0;
            header.Left = width;
            header.Top = 0;
            header.Bottom = height;
            header.HeightInEmus = height;
            header.WidthInEmus = width;
            return header;
        }

        public abstract OfficeArtRecordHeader CreateRecordHeader();
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this.imageBytesStream != null))
            {
                this.imageBytesStream.Dispose();
                this.imageBytesStream = null;
            }
        }

        ~BlipBase()
        {
            this.Dispose(false);
        }

        public virtual int GetSize() => 
            0x19 + this.ImageBytesLength;

        protected virtual void LoadImageFromStream(MemoryStream imageStream)
        {
            this.Image = OfficeImage.CreateImage(imageStream);
        }

        protected virtual void Read(BinaryReader reader, OfficeArtRecordHeader header)
        {
            int num = this.CalcMD4MessageOffset(header.InstanceInfo);
            reader.BaseStream.Seek((long) num, SeekOrigin.Current);
            int count = header.Length - num;
            MemoryStream imageStream = new MemoryStream(reader.ReadBytes(count));
            this.LoadImageFromStream(imageStream);
        }

        protected virtual MD4MessageDigest SaveImageToStream(BinaryWriter writer)
        {
            Stream imageBytesStream = this.ImageBytesStream;
            if (imageBytesStream == null)
            {
                return new MD4MessageDigest();
            }
            byte[] buffer = new byte[0x1000];
            int length = buffer.Length;
            long imageBytesLength = this.ImageBytesLength;
            MD4HashCalculator calculator = new MD4HashCalculator();
            uint[] initialCheckSumValue = calculator.InitialCheckSumValue;
            while (imageBytesLength >= length)
            {
                imageBytesStream.Read(buffer, 0, length);
                imageBytesLength -= length;
                writer.Write(buffer);
                initialCheckSumValue = calculator.UpdateCheckSum(initialCheckSumValue, buffer, 0, length);
            }
            if (imageBytesLength > 0L)
            {
                imageBytesStream.Read(buffer, 0, (int) imageBytesLength);
                writer.Write(buffer, 0, (int) imageBytesLength);
                initialCheckSumValue = calculator.UpdateCheckSum(initialCheckSumValue, buffer, 0, (int) imageBytesLength);
            }
            return new MD4MessageDigest(calculator.GetFinalCheckSum(initialCheckSumValue));
        }

        protected internal virtual void SetImage(BlipBase blip)
        {
            this.SetImage(blip.Image);
            this.metafileHeader = blip.metafileHeader;
            this.TagValue = blip.TagValue;
        }

        protected internal virtual void SetImage(DocMetafileReader reader)
        {
            this.SetImage(reader.Image);
            this.metafileHeader = reader.MetafileHeader;
        }

        private void SetImage(OfficeImage image)
        {
            if (image != null)
            {
                this.image = image;
            }
        }

        public virtual MD4MessageDigest Write(BinaryWriter writer)
        {
            this.CreateRecordHeader().Write(writer);
            long position = writer.BaseStream.Position;
            writer.Seek(0x10, SeekOrigin.Current);
            writer.Write(this.TagValue);
            MD4MessageDigest digest = this.SaveImageToStream(writer);
            long num2 = writer.BaseStream.Position;
            writer.Seek((int) position, SeekOrigin.Begin);
            writer.Write(digest.ToArray());
            writer.Seek((int) num2, SeekOrigin.Begin);
            return digest;
        }

        public abstract int SingleMD4Message { get; }

        public abstract int DoubleMD4Message { get; }

        public byte TagValue { get; protected set; }

        public DocMetafileHeader MetafileHeader =>
            this.metafileHeader;

        public OfficeImage Image
        {
            get => 
                this.image;
            set => 
                this.SetImage(value);
        }

        protected internal Stream ImageBytesStream
        {
            get
            {
                this.imageBytesStream ??= this.CreateImageBytesStream();
                return this.imageBytesStream;
            }
        }

        protected internal int ImageBytesLength
        {
            get
            {
                this.imageBytesStream ??= this.CreateImageBytesStream();
                return ((this.imageBytesStream != null) ? ((int) this.imageBytesStream.Length) : 0);
            }
        }

        protected internal virtual OfficeImageFormat Format =>
            (this.image == null) ? OfficeImageFormat.None : this.image.RawFormat;
    }
}

