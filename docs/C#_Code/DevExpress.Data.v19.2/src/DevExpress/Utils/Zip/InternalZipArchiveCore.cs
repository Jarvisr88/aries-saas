namespace DevExpress.Utils.Zip
{
    using DevExpress.Utils;
    using DevExpress.Utils.Zip.Internal;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Text;

    public class InternalZipArchiveCore : IDisposable
    {
        private readonly Stream zipStream;
        private readonly BinaryWriter writer;
        private readonly List<CentralDirectoryEntry> centralDirectory;
        private readonly bool requireDisposeForStream;

        public InternalZipArchiveCore(Stream stream)
        {
            this.zipStream = stream;
            this.writer = new BinaryWriter(this.zipStream);
            this.centralDirectory = new List<CentralDirectoryEntry>();
        }

        public InternalZipArchiveCore(string zipFileName) : this(new FileStream(zipFileName, FileMode.Create, FileAccess.Write))
        {
            this.requireDisposeForStream = true;
        }

        public void Add(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                this.Add(Path.GetFileName(fileName), File.GetLastWriteTime(fileName), stream);
            }
        }

        public void Add(string name, DateTime fileTime, Stream stream)
        {
            DeflateCompressionStrategy compressionStrategy = new DeflateCompressionStrategy();
            this.WriteFile(name, fileTime, stream, compressionStrategy, null);
        }

        public void Add(string fileName, DateTime fileTime, string content)
        {
            this.Add(fileName, fileTime, this.UTF8Encoding.GetBytes(content));
        }

        public void Add(string fileName, DateTime fileTime, byte[] content)
        {
            using (MemoryStream stream = new MemoryStream(content, false))
            {
                this.Add(fileName, fileTime, stream);
            }
        }

        public void Add(string fileName, DateTime fileTime, byte[] content, int index, int count)
        {
            using (MemoryStream stream = new MemoryStream(content, index, count, false))
            {
                this.Add(fileName, fileTime, stream);
            }
        }

        public void AddCompressed(string name, DateTime fileTime, CompressedStream compressedStream)
        {
            Stream sourceStream = compressedStream.Stream;
            ICompressionStrategy compressionStrategy = new UseCompressedStreamCompressionStrategy(compressedStream);
            name = name.Replace('\\', '/');
            CentralDirectoryEntry dirEntry = new CentralDirectoryEntry {
                MsDosDateTime = ZipDateTimeHelper.ToMsDosDateTime(fileTime),
                Crc32 = compressionStrategy.Crc32,
                CompressedSize = (int) (sourceStream.Length - sourceStream.Position),
                FileName = name,
                UncompressedSize = compressedStream.UncompressedSize,
                RelativeOffset = (int) this.ZipStream.Position
            };
            dirEntry.GeneralPurposeFlag = this.CalculateGeneralPurposeFlag(dirEntry.FileName, compressionStrategy);
            this.WriteDirectoryEntry(dirEntry, compressionStrategy, dirEntry.UncompressedSize == 0);
            compressionStrategy.Compress(sourceStream, this.ZipStream, null);
            this.CentralDirectory.Add(dirEntry);
        }

        private short CalculateGeneralPurposeFlag(string fileName, ICompressionStrategy compressionStrategy)
        {
            Encoding defaultEncoding = this.GetDefaultEncoding();
            short num = (short) (2 | compressionStrategy.GetGeneralPurposeBitFlag());
            if (!(!ReferenceEquals(this.UTF8Encoding, defaultEncoding) && ZipEncodingHelper.CanCodeToEncoding(defaultEncoding, fileName)))
            {
                num = (short) (num | 0x800);
            }
            return num;
        }

        private byte[] ConvertToByteArray(string value, bool useUtf) => 
            !string.IsNullOrEmpty(value) ? (!useUtf ? this.GetDefaultEncoding().GetBytes(value) : this.UTF8Encoding.GetBytes(value)) : new byte[0];

        protected internal virtual Stream CreateDeflateStream(Stream stream) => 
            new DeflateStream(stream, CompressionMode.Compress, true);

        protected virtual IZipExtraFieldCollection CreateExtraFieldCollection() => 
            null;

        protected virtual Encoding GetDefaultEncoding() => 
            DXEncoding.ASCII;

        internal DevExpress.Utils.Zip.Internal.CompressionMethod GetDeflateStreamCompressionMethod() => 
            DevExpress.Utils.Zip.Internal.CompressionMethod.Deflate;

        void IDisposable.Dispose()
        {
            this.WriteCentralDirectory();
            if (this.zipStream != null)
            {
                if (this.requireDisposeForStream)
                {
                    this.zipStream.Dispose();
                }
                else
                {
                    this.zipStream.Flush();
                }
            }
        }

        private void WriteCentralDirectory()
        {
            long position = this.ZipStream.Position;
            for (int i = 0; i < this.CentralDirectory.Count; i++)
            {
                CentralDirectoryEntry entry = this.CentralDirectory[i];
                this.Writer.Write((uint) 0x2014b50);
                this.Writer.Write((short) 20);
                this.Writer.Write((short) 20);
                this.Writer.Write(entry.GeneralPurposeFlag);
                this.Writer.Write((short) entry.CompressionMethod);
                this.Writer.Write(entry.MsDosDateTime);
                this.Writer.Write(entry.Crc32);
                this.Writer.Write(entry.CompressedSize);
                this.Writer.Write(entry.UncompressedSize);
                bool useUtf = (entry.GeneralPurposeFlag & 0x800) != 0;
                byte[] buffer = this.ConvertToByteArray(entry.FileName, useUtf);
                byte[] buffer2 = this.ConvertToByteArray(entry.Comment, useUtf);
                this.Writer.Write((short) buffer.Length);
                short num4 = (entry.ExtraFields != null) ? entry.ExtraFields.CalculateSize(ExtraFieldType.CentralDirectoryEntry) : ((short) 0);
                this.Writer.Write(num4);
                this.Writer.Write((short) buffer2.Length);
                this.Writer.Write((short) 0);
                this.Writer.Write((short) 0);
                this.Writer.Write(entry.FileAttributes);
                this.Writer.Write(entry.RelativeOffset);
                this.Writer.Write(buffer);
                if (entry.ExtraFields != null)
                {
                    entry.ExtraFields.Write(this.writer, ExtraFieldType.CentralDirectoryEntry);
                }
                this.Writer.Write(buffer2);
            }
            long num2 = this.ZipStream.Position;
            this.Writer.Write((uint) 0x6054b50);
            this.Writer.Write((short) 0);
            this.Writer.Write((short) 0);
            this.Writer.Write((short) this.CentralDirectory.Count);
            this.Writer.Write((short) this.CentralDirectory.Count);
            this.Writer.Write((uint) (num2 - position));
            this.Writer.Write((uint) position);
            this.Writer.Write((short) 0);
        }

        private long WriteDirectoryEntry(CentralDirectoryEntry dirEntry, ICompressionStrategy compressionStrategy, bool zeroSized)
        {
            this.Writer.Write((uint) 0x4034b50);
            this.Writer.Write((short) 20);
            this.Writer.Write(dirEntry.GeneralPurposeFlag);
            dirEntry.CompressionMethod = zeroSized ? DevExpress.Utils.Zip.Internal.CompressionMethod.Store : compressionStrategy.CompressionMethod;
            this.Writer.Write((short) dirEntry.CompressionMethod);
            this.Writer.Write(dirEntry.MsDosDateTime);
            long position = this.ZipStream.Position;
            this.Writer.Write(dirEntry.Crc32);
            this.Writer.Write(dirEntry.CompressedSize);
            this.Writer.Write(dirEntry.UncompressedSize);
            bool useUtf = (dirEntry.GeneralPurposeFlag & 0x800) != 0;
            byte[] buffer = this.ConvertToByteArray(dirEntry.FileName, useUtf);
            this.Writer.Write((short) buffer.Length);
            compressionStrategy.PrepareExtraFields(dirEntry.ExtraFields);
            short num2 = (dirEntry.ExtraFields != null) ? dirEntry.ExtraFields.CalculateSize(ExtraFieldType.LocalHeader) : ((short) 0);
            this.Writer.Write(num2);
            this.Writer.Write(buffer);
            if (dirEntry.ExtraFields != null)
            {
                dirEntry.ExtraFields.Write(this.Writer, ExtraFieldType.LocalHeader);
            }
            return position;
        }

        protected virtual CentralDirectoryEntry WriteFile(string name, DateTime fileTime, Stream stream, ICompressionStrategy compressionStrategy, IZipComplexOperationProgress progress)
        {
            name = name.Replace('\\', '/');
            CentralDirectoryEntry dirEntry = new CentralDirectoryEntry {
                ExtraFields = this.CreateExtraFieldCollection(),
                MsDosDateTime = ZipDateTimeHelper.ToMsDosDateTime(fileTime),
                FileName = name
            };
            long num = 0L;
            try
            {
                num = stream.Position;
            }
            catch (NotSupportedException)
            {
            }
            int num2 = (int) (stream.Length - num);
            dirEntry.RelativeOffset = (int) this.ZipStream.Position;
            dirEntry.GeneralPurposeFlag = this.CalculateGeneralPurposeFlag(dirEntry.FileName, compressionStrategy);
            if (this.ZipStream.CanSeek)
            {
                dirEntry.UncompressedSize = num2;
            }
            else
            {
                dirEntry.GeneralPurposeFlag = (short) (dirEntry.GeneralPurposeFlag | 8);
            }
            long num3 = this.WriteDirectoryEntry(dirEntry, compressionStrategy, num2 == 0);
            compressionStrategy.Compress(stream, this.ZipStream, progress);
            long position = this.ZipStream.Position;
            dirEntry.Crc32 = compressionStrategy.Crc32;
            dirEntry.CompressedSize = (int) (position - this.ZipStream.Position);
            if (this.ZipStream.CanSeek)
            {
                this.zipStream.Position = num3;
                this.Writer.Write(dirEntry.Crc32);
                this.Writer.Write(dirEntry.CompressedSize);
                this.ZipStream.Position = position;
            }
            else
            {
                dirEntry.UncompressedSize = num2;
                this.Writer.Write((uint) 0x8074b50);
                this.Writer.Write(dirEntry.Crc32);
                this.Writer.Write(dirEntry.CompressedSize);
                this.Writer.Write(dirEntry.UncompressedSize);
            }
            this.CentralDirectory.Add(dirEntry);
            return dirEntry;
        }

        protected internal List<CentralDirectoryEntry> CentralDirectory =>
            this.centralDirectory;

        private BinaryWriter Writer =>
            this.writer;

        protected internal Stream ZipStream =>
            this.zipStream;

        private Encoding UTF8Encoding =>
            Encoding.UTF8;
    }
}

