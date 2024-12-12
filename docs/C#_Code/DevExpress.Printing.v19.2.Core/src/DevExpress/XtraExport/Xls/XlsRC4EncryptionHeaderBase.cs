namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class XlsRC4EncryptionHeaderBase
    {
        protected XlsRC4EncryptionHeaderBase()
        {
        }

        public virtual int GetSize() => 
            4;

        public void Read(XlReader reader)
        {
            this.VersionMajor = reader.ReadNotCryptedInt16();
            this.VersionMinor = reader.ReadNotCryptedInt16();
            this.ReadCore(reader);
        }

        protected abstract void ReadCore(XlReader reader);
        public void Write(BinaryWriter writer)
        {
            writer.Write(this.VersionMajor);
            writer.Write(this.VersionMinor);
            this.WriteCore(writer);
        }

        protected abstract void WriteCore(BinaryWriter writer);

        public short VersionMinor { get; protected set; }

        public short VersionMajor { get; protected set; }
    }
}

