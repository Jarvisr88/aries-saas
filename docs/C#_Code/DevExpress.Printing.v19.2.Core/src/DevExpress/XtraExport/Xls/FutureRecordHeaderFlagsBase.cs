namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class FutureRecordHeaderFlagsBase : FutureRecordHeaderBase
    {
        protected FutureRecordHeaderFlagsBase()
        {
        }

        protected override void ReadCore(XlReader reader)
        {
            ushort num = reader.ReadUInt16();
            this.RangeOfCells = Convert.ToBoolean((int) (num & 1));
            this.Alert = Convert.ToBoolean((int) (num & 2));
        }

        protected override void WriteCore(BinaryWriter writer)
        {
            ushort num = 0;
            if (this.RangeOfCells)
            {
                num = (ushort) (num | 1);
            }
            if (this.Alert)
            {
                num = (ushort) (num | 2);
            }
            writer.Write(num);
        }

        public bool RangeOfCells { get; set; }

        public bool Alert { get; set; }
    }
}

