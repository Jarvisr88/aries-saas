namespace DevExpress.Utils.Zip
{
    using DevExpress.Utils;
    using DevExpress.Utils.Zip.Internal;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class InternalZipFile
    {
        private Encoding fileNameEncoding = DXEncoding.Default;
        private Stream internalFileDataStream;

        public InternalZipFile()
        {
            this.FileLastModificationTime = DateTime.Now;
        }

        protected virtual IDecompressionStrategy CreateDecompressionStrategy() => 
            (this.UncompressedSize != 0) ? ((this.CompressionMethod != DevExpress.Utils.Zip.Internal.CompressionMethod.Deflate) ? ((IDecompressionStrategy) new NoCompressionDecompressionStrategy()) : ((IDecompressionStrategy) new DeflateDecompressionStrategy())) : ((IDecompressionStrategy) new ZeroLengthContentDecompressionStrategy());

        public virtual Stream CreateDecompressionStream() => 
            !this.IsEncrypted ? this.CreateDecompressionStream(this.ContentRawDataStreamProxy) : null;

        internal virtual Stream CreateDecompressionStream(StreamProxy streamProxy) => 
            !this.IsEncrypted ? this.CreateDecompressionStrategy().Decompress(streamProxy.CreateRawStream()) : null;

        protected virtual InternalZipExtraFieldFactory CreateInternalZipExtraFieldFactory() => 
            FactorySingleton<InternalZipExtraFieldFactory>.Instance;

        private Encoding GetActualEncoding() => 
            ((this.GeneralPurposeBitFlag & ZipFlags.EFS) == ZipFlags.EFS) ? Encoding.UTF8 : this.DefaultEncoding;

        protected internal virtual void ReadLocalHeader(BinaryReader reader)
        {
            this.VersionToExtract = reader.ReadInt16();
            this.GeneralPurposeBitFlag = (ZipFlags) reader.ReadInt16();
            this.IsEncrypted = (this.GeneralPurposeBitFlag & ZipFlags.Encrypted) == ZipFlags.Encrypted;
            this.CompressionMethod = (DevExpress.Utils.Zip.Internal.CompressionMethod) reader.ReadInt16();
            int data = reader.ReadInt32();
            try
            {
                this.FileLastModificationTime = ZipDateTimeHelper.FromMsDos(data);
            }
            catch (ArgumentOutOfRangeException)
            {
                this.FileLastModificationTime = DateTime.MinValue;
            }
            this.Crc32 = reader.ReadInt32();
            if (this.IsEncrypted)
            {
                this.CheckByte = ((this.GeneralPurposeBitFlag & ZipFlags.UseDataFromDataDescriptor) == ZipFlags.DeflateNormalCompression) ? ((byte) ((this.Crc32 >> 0x18) & 0xff)) : ((byte) ((data >> 8) & 0xff));
            }
            this.CompressedSize = reader.ReadUInt32();
            this.UncompressedSize = reader.ReadUInt32();
            this.FileNameLength = reader.ReadInt16();
            this.LocalHeaderExtraFieldLength = reader.ReadInt16();
            this.FileName = this.ReadString(reader, this.FileNameLength);
            this.ReadLocalHeaderExtraFields(reader, this.LocalHeaderExtraFieldLength);
            Stream baseStream = reader.BaseStream;
            long position = reader.BaseStream.Position;
            baseStream.Seek(this.CompressedSize, SeekOrigin.Current);
            if ((this.GeneralPurposeBitFlag & ZipFlags.UseDataFromDataDescriptor) != ZipFlags.DeflateNormalCompression)
            {
                this.SeekToDataDescriptorData(reader, this.CompressedSize);
                this.Crc32 = reader.ReadInt32();
                this.CompressedSize = reader.ReadUInt32();
                this.UncompressedSize = reader.ReadUInt32();
            }
            this.ContentRawDataStreamProxy = new StreamProxy(baseStream, position, (this.CompressedSize == 0) ? -1L : this.CompressedSize, this.CompressionMethod != DevExpress.Utils.Zip.Internal.CompressionMethod.Store);
        }

        protected virtual void ReadLocalHeaderExtraFields(BinaryReader reader, short extraFieldLength)
        {
            InternalZipExtraFieldFactory headerFactory = this.CreateInternalZipExtraFieldFactory();
            ZipExtraFieldComposition.Read(reader, (long) extraFieldLength, headerFactory).Apply(this);
        }

        protected string ReadString(BinaryReader reader, int count)
        {
            byte[] bytes = reader.ReadBytes(count);
            return this.GetActualEncoding().GetString(bytes, 0, bytes.Length);
        }

        protected internal int SearchForPattern(byte[] bytes, byte[] pattern)
        {
            int length = bytes.Length;
            int num2 = pattern.Length;
            if (length >= num2)
            {
                int index = 0;
                for (int i = 0; i < length; i++)
                {
                    if (bytes[i] == pattern[index])
                    {
                        index++;
                        if (index >= num2)
                        {
                            return ((i - num2) + 1);
                        }
                    }
                    else
                    {
                        if (index > 0)
                        {
                            i--;
                        }
                        index = 0;
                    }
                }
            }
            return -1;
        }

        protected internal virtual void SeekToDataDescriptorData(BinaryReader reader, long compressedSize)
        {
            Stream baseStream = reader.BaseStream;
            if (compressedSize != 0)
            {
                if (reader.ReadInt32() != 0x8074b50)
                {
                    baseStream.Seek((long) (-4), SeekOrigin.Current);
                }
            }
            else
            {
                byte[] pattern = new byte[] { 80, 0x4b, 7, 8 };
                byte[] buffer = new byte[7];
                long position = baseStream.Position;
                baseStream.Read(buffer, 0, 7);
                do
                {
                    int num2 = this.SearchForPattern(buffer, pattern);
                    if (num2 >= 0)
                    {
                        long offset = baseStream.Position;
                        baseStream.Seek((long) (num2 - 3), SeekOrigin.Current);
                        long num4 = (baseStream.Position - position) - 4L;
                        long num5 = baseStream.Position;
                        reader.ReadInt32();
                        int num6 = reader.ReadInt32();
                        if (num4 == num6)
                        {
                            baseStream.Seek(num5, SeekOrigin.Begin);
                            return;
                        }
                        baseStream.Seek(offset, SeekOrigin.Begin);
                    }
                    buffer[0] = buffer[4];
                    buffer[1] = buffer[5];
                    buffer[2] = buffer[6];
                }
                while (baseStream.Read(buffer, 3, 4) != 0);
            }
        }

        protected short FileNameLength { get; set; }

        protected short LocalHeaderExtraFieldLength { get; set; }

        protected int Crc32 { get; set; }

        protected ZipFlags GeneralPurposeBitFlag { get; set; }

        public long UncompressedSize { get; protected internal set; }

        public long CompressedSize { get; protected internal set; }

        protected StreamProxy ContentRawDataStreamProxy { get; private set; }

        protected DevExpress.Utils.Zip.Internal.CompressionMethod CompressionMethod { get; set; }

        public Stream FileDataStream
        {
            get
            {
                this.internalFileDataStream ??= this.CreateDecompressionStream(this.ContentRawDataStreamProxy);
                return this.internalFileDataStream;
            }
        }

        public string FileName { get; protected set; }

        public Encoding DefaultEncoding
        {
            get => 
                this.fileNameEncoding;
            set
            {
                value ??= DXEncoding.Default;
                this.fileNameEncoding = value;
            }
        }

        public DateTime FileLastModificationTime { get; set; }

        public bool IsEncrypted { get; private set; }

        public byte CheckByte { get; private set; }

        protected short VersionToExtract { get; set; }
    }
}

