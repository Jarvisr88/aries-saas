namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsChunk : IXlsChunk
    {
        private byte[] data = new byte[0];

        public XlsChunk(short recordType)
        {
            this.RecordType = recordType;
        }

        public virtual int GetMaxDataSize() => 
            0x2020;

        protected virtual short GetSize() => 
            (short) this.data.Length;

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.RecordType);
            writer.Write(this.GetSize());
            this.WriteCore(writer);
        }

        protected virtual void WriteCore(BinaryWriter writer)
        {
            if (this.data.Length != 0)
            {
                writer.Write(this.data);
            }
        }

        public short RecordType { get; private set; }

        public byte[] Data
        {
            get => 
                this.data;
            set
            {
                if (value == null)
                {
                    this.data = new byte[0];
                }
                else
                {
                    if (value.Length > this.GetMaxDataSize())
                    {
                        throw new ArgumentException("Data exceed maximun record data length");
                    }
                    this.data = value;
                }
            }
        }
    }
}

