namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class FutureRecordHeaderRefNoFlags : FutureRecordHeaderBase
    {
        private XlsRef8 range = new XlsRef8();

        public static FutureRecordHeaderRefNoFlags FromStream(XlReader reader)
        {
            FutureRecordHeaderRefNoFlags flags = new FutureRecordHeaderRefNoFlags();
            flags.Read(reader);
            return flags;
        }

        public override short GetSize() => 
            10;

        protected override void ReadCore(XlReader reader)
        {
            this.range = XlsRef8.FromStream(reader);
        }

        protected override void WriteCore(BinaryWriter writer)
        {
            this.range.Write(writer);
        }

        public XlsRef8 Range
        {
            get => 
                this.range;
            set
            {
                value ??= new XlsRef8();
                this.range = value;
            }
        }
    }
}

