namespace DevExpress.Office.Utils
{
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class XlReader : BinaryDataReaderBase, IDisposable
    {
        private readonly bool externalBaseReader;
        private BinaryReader baseReader;

        public XlReader(BinaryReader reader)
        {
            Guard.ArgumentNotNull(reader, "reader");
            this.baseReader = reader;
            this.externalBaseReader = true;
        }

        public XlReader(MemoryStream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            this.baseReader = new BinaryReader(stream);
            this.externalBaseReader = false;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.externalBaseReader)
            {
                this.baseReader.Dispose();
            }
            this.baseReader = null;
        }

        public override bool ReadBoolean() => 
            this.BaseReader.ReadBoolean();

        public override byte ReadByte() => 
            this.BaseReader.ReadByte();

        public override byte[] ReadBytes(int count) => 
            this.BaseReader.ReadBytes(count);

        public override double ReadDouble() => 
            this.BaseReader.ReadDouble();

        public override short ReadInt16() => 
            this.BaseReader.ReadInt16();

        public override int ReadInt32() => 
            this.BaseReader.ReadInt32();

        public override long ReadInt64() => 
            this.BaseReader.ReadInt64();

        public virtual byte[] ReadNotCryptedBytes(int count) => 
            this.BaseReader.ReadBytes(count);

        public virtual short ReadNotCryptedInt16() => 
            this.BaseReader.ReadInt16();

        public virtual int ReadNotCryptedInt32() => 
            this.BaseReader.ReadInt32();

        [CLSCompliant(false)]
        public virtual ushort ReadNotCryptedUInt16() => 
            this.BaseReader.ReadUInt16();

        public override string ReadString() => 
            this.baseReader.ReadString();

        [CLSCompliant(false)]
        public override ushort ReadUInt16() => 
            this.BaseReader.ReadUInt16();

        [CLSCompliant(false)]
        public override uint ReadUInt32() => 
            this.BaseReader.ReadUInt32();

        [CLSCompliant(false)]
        public override ulong ReadUInt64() => 
            this.BaseReader.ReadUInt64();

        public override long Seek(long offset, SeekOrigin origin) => 
            this.BaseReader.BaseStream.Seek(offset, origin);

        public virtual void SetRecordSize(int size)
        {
        }

        protected internal BinaryReader BaseReader =>
            this.baseReader;

        public override long StreamLength =>
            this.BaseReader.BaseStream.Length;

        public override long Position
        {
            get => 
                this.BaseReader.BaseStream.Position;
            set => 
                this.BaseReader.BaseStream.Position = value;
        }
    }
}

