namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class FutureRecordHeaderBase
    {
        protected FutureRecordHeaderBase()
        {
        }

        public abstract short GetSize();
        public void Read(XlReader reader)
        {
            this.RecordTypeId = reader.ReadInt16();
            this.ReadCore(reader);
        }

        protected abstract void ReadCore(XlReader reader);
        public void Write(BinaryWriter writer)
        {
            writer.Write(this.RecordTypeId);
            this.WriteCore(writer);
        }

        protected abstract void WriteCore(BinaryWriter writer);

        public virtual short RecordTypeId { get; set; }
    }
}

