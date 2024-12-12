namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class BinaryDataReaderBase
    {
        protected BinaryDataReaderBase()
        {
        }

        public virtual bool ReadBoolean() => 
            false;

        public virtual byte ReadByte() => 
            0;

        public virtual byte[] ReadBytes(int count) => 
            null;

        public virtual double ReadDouble() => 
            0.0;

        public virtual short ReadInt16() => 
            0;

        public virtual int ReadInt32() => 
            0;

        public virtual long ReadInt64() => 
            0L;

        public virtual string ReadString() => 
            null;

        [CLSCompliant(false)]
        public virtual ushort ReadUInt16() => 
            0;

        [CLSCompliant(false)]
        public virtual uint ReadUInt32() => 
            0;

        [CLSCompliant(false)]
        public virtual ulong ReadUInt64() => 
            0UL;

        public virtual long Seek(long offset, SeekOrigin origin) => 
            0L;

        public virtual long StreamLength { get; }

        public virtual long Position { get; set; }
    }
}

