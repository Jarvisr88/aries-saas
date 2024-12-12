namespace DevExpress.Office.Utils
{
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Text;

    public class FileBlipStoreEntry : BlipBase
    {
        private const uint emptySlotOffset = uint.MaxValue;
        private const int headerVersion = 2;
        private const int blipStoreHeaderSize = 0x24;
        private const short tag = 0xff;
        private const int defaultDelay = 0x44;
        private const byte defaultNameLength = 0;
        private const int unusedDataSize = 0x18;
        private int refCount;
        private uint embeddedBlipOffset;
        private byte blipType;
        private string name;
        private int embeddedBlipSize;
        private bool hasDelayedStream;
        private BinaryReader embeddedReader;
        private BlipBase embeddedBlip;

        public FileBlipStoreEntry(BlipBase blip, bool hasDelayedStream)
        {
            this.refCount = 1;
            Guard.ArgumentNotNull(blip, "blip");
            this.embeddedBlip = blip;
            this.SetImage(this.embeddedBlip);
            this.blipType = 7;
            this.embeddedBlipSize = this.embeddedBlip.GetSize();
            this.hasDelayedStream = hasDelayedStream;
        }

        public FileBlipStoreEntry(OfficeImage image, bool hasDelayedStream)
        {
            this.refCount = 1;
            this.embeddedBlip = BlipFactory.CreateBlipFromImage(image);
            this.SetImage(this.embeddedBlip);
            this.blipType = this.GetBlipType(image.RawFormat);
            this.embeddedBlipSize = this.embeddedBlip.GetSize();
            this.hasDelayedStream = hasDelayedStream;
        }

        public FileBlipStoreEntry(BinaryReader reader, BinaryReader embeddedReader, OfficeArtRecordHeader header)
        {
            this.refCount = 1;
            this.embeddedReader = embeddedReader;
            this.Read(reader, header);
        }

        public override OfficeArtRecordHeader CreateRecordHeader() => 
            new OfficeArtRecordHeader { 
                Version = 2,
                InstanceInfo = this.blipType,
                TypeCode = 0xf007,
                Length = this.HasDelayedStream ? 0x24 : (0x24 + this.EmbeddedBlipSize)
            };

        protected virtual byte GetBlipType(OfficeImageFormat format)
        {
            switch (format)
            {
                case OfficeImageFormat.None:
                    return 1;

                case OfficeImageFormat.Bmp:
                    return 6;

                case OfficeImageFormat.Emf:
                    return 2;

                case OfficeImageFormat.Gif:
                    return 6;

                case OfficeImageFormat.Jpeg:
                    return 5;

                case OfficeImageFormat.MemoryBmp:
                    return 6;

                case OfficeImageFormat.Png:
                    return 6;

                case OfficeImageFormat.Tiff:
                    return 0x11;

                case OfficeImageFormat.Wmf:
                    return 3;
            }
            return 1;
        }

        public override int GetSize() => 
            this.HasDelayedStream ? 0x2c : (0x2c + this.EmbeddedBlipSize);

        protected override void Read(BinaryReader reader, OfficeArtRecordHeader header)
        {
            reader.BaseStream.Seek((long) 0x18, SeekOrigin.Current);
            this.ReadCore(reader);
            this.ReadName(reader);
            this.ReadEmbeddedBlip(reader);
        }

        private void ReadCore(BinaryReader reader)
        {
            this.refCount = reader.ReadInt32();
            this.embeddedBlipOffset = reader.ReadUInt32();
            reader.BaseStream.Seek(1L, SeekOrigin.Current);
        }

        private void ReadEmbeddedBlip(BinaryReader reader)
        {
            if ((this.embeddedBlipOffset != uint.MaxValue) && (this.ReferenceCount != 0))
            {
                if (!ReferenceEquals(this.EmbeddedReader, reader))
                {
                    this.EmbeddedReader.BaseStream.Seek((long) this.embeddedBlipOffset, SeekOrigin.Begin);
                }
                OfficeArtRecordHeader header = OfficeArtRecordHeader.FromStream(this.embeddedReader);
                this.embeddedBlip = BlipFactory.CreateBlipFromStream(this.embeddedReader, header);
                if ((this.embeddedBlip != null) && (this.embeddedBlip.Image != null))
                {
                    this.SetImage(this.embeddedBlip);
                }
                else
                {
                    base.Image = UriBasedOfficeImageBase.CreatePlaceHolder(null, 0, 0);
                }
            }
        }

        private void ReadName(BinaryReader reader)
        {
            byte num = reader.ReadByte();
            reader.BaseStream.Seek(2L, SeekOrigin.Current);
            if (num != 0)
            {
                byte[] bytes = reader.ReadBytes(num - 2);
                this.name = Encoding.Unicode.GetString(bytes, 0, bytes.Length);
                reader.BaseStream.Seek(2L, SeekOrigin.Current);
            }
        }

        public override MD4MessageDigest Write(BinaryWriter writer)
        {
            Guard.ArgumentNotNull(writer, "writer");
            long num = this.WriteCore(writer);
            writer.Write(0x44);
            this.WriteName(writer);
            MD4MessageDigest digest = this.embeddedBlip.Write(writer);
            long position = writer.BaseStream.Position;
            writer.Seek((int) num, SeekOrigin.Begin);
            writer.Write(digest.ToArray());
            writer.Seek((int) position, SeekOrigin.Begin);
            return digest;
        }

        public void Write(BinaryWriter writer, BinaryWriter embeddedWriter)
        {
            Guard.ArgumentNotNull(writer, "writer");
            Guard.ArgumentNotNull(embeddedWriter, "embeddedWriter");
            long num = this.WriteCore(writer);
            if (this.HasDelayedStream)
            {
                writer.Write((int) embeddedWriter.BaseStream.Position);
            }
            else
            {
                writer.Write(0);
            }
            this.WriteName(writer);
            MD4MessageDigest digest = this.embeddedBlip.Write(embeddedWriter);
            long position = writer.BaseStream.Position;
            writer.Seek((int) num, SeekOrigin.Begin);
            writer.Write(digest.ToArray());
            writer.Seek((int) position, SeekOrigin.Begin);
        }

        private long WriteCore(BinaryWriter writer)
        {
            this.CreateRecordHeader().Write(writer);
            writer.Write(this.blipType);
            writer.Write(this.blipType);
            long position = writer.BaseStream.Position;
            writer.Seek(0x10, SeekOrigin.Current);
            writer.Write((short) 0xff);
            writer.Write(this.EmbeddedBlipSize);
            writer.Write(this.ReferenceCount);
            return position;
        }

        private void WriteName(BinaryWriter writer)
        {
            writer.BaseStream.Seek(1L, SeekOrigin.Current);
            writer.Write((byte) 0);
            writer.BaseStream.Seek(2L, SeekOrigin.Current);
        }

        public string Name =>
            this.name;

        public int ReferenceCount
        {
            get => 
                this.refCount;
            set => 
                this.refCount = value;
        }

        protected internal BinaryReader EmbeddedReader =>
            this.embeddedReader;

        protected internal int EmbeddedBlipSize =>
            this.embeddedBlipSize;

        protected internal bool HasDelayedStream
        {
            get => 
                this.hasDelayedStream;
            set => 
                this.hasDelayedStream = value;
        }

        public override int SingleMD4Message =>
            this.embeddedBlip.SingleMD4Message;

        public override int DoubleMD4Message =>
            this.embeddedBlip.DoubleMD4Message;
    }
}

