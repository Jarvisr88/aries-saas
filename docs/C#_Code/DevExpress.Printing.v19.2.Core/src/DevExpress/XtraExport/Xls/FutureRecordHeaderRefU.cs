namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class FutureRecordHeaderRefU : FutureRecordHeaderFlagsBase
    {
        private XlsRef8 range = new XlsRef8();

        public static FutureRecordHeaderRefU FromStream(XlReader reader)
        {
            FutureRecordHeaderRefU fu = new FutureRecordHeaderRefU();
            fu.Read(reader);
            return fu;
        }

        public override short GetSize() => 
            12;

        protected override void ReadCore(XlReader reader)
        {
            base.ReadCore(reader);
            this.range = XlsRef8.FromStream(reader);
        }

        protected override void WriteCore(BinaryWriter writer)
        {
            base.WriteCore(writer);
            this.range.Write(writer);
        }

        public XlsRef8 Range
        {
            get => 
                this.range;
            set
            {
                if (value != null)
                {
                    this.range = value;
                }
                else
                {
                    this.range = new XlsRef8();
                }
            }
        }
    }
}

