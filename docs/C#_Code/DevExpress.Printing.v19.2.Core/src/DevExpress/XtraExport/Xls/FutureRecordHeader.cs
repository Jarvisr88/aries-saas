namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class FutureRecordHeader : FutureRecordHeaderFlagsBase
    {
        public static FutureRecordHeader FromStream(XlReader reader)
        {
            FutureRecordHeader header = new FutureRecordHeader();
            header.Read(reader);
            return header;
        }

        public override short GetSize() => 
            12;

        protected override void ReadCore(XlReader reader)
        {
            base.ReadCore(reader);
            reader.ReadUInt64();
        }

        protected override void WriteCore(BinaryWriter writer)
        {
            base.WriteCore(writer);
            writer.Write((ulong) 0UL);
        }
    }
}

