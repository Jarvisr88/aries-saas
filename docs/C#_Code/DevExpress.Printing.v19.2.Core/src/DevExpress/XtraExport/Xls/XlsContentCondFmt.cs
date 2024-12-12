namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentCondFmt : XlsContentBase
    {
        private const int fixedPartSize = 14;
        private int recordCount;
        private int id;
        private XlsRef8 boundRange = new XlsRef8();
        private readonly List<XlsRef8> ranges = new List<XlsRef8>();

        protected virtual void CheckRecordCount(int value)
        {
            base.CheckValue(value, 1, 3, "RecordCount");
        }

        public override int GetSize() => 
            14 + (this.Ranges.Count * 8);

        public override void Read(XlReader reader, int size)
        {
            this.recordCount = reader.ReadUInt16();
            ushort num = reader.ReadUInt16();
            this.ToughRecalc = Convert.ToBoolean((int) (num & 1));
            this.id = (num & 0xfffe) >> 1;
            this.boundRange = XlsRef8.FromStream(reader);
            int num2 = reader.ReadUInt16();
            for (int i = 0; i < num2; i++)
            {
                this.Ranges.Add(XlsRef8.FromStream(reader));
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.RecordCount);
            ushort num = (ushort) (this.Id << 1);
            if (this.ToughRecalc)
            {
                num = (ushort) (num | 1);
            }
            writer.Write(num);
            this.boundRange.Write(writer);
            int count = this.Ranges.Count;
            writer.Write((ushort) count);
            for (int i = 0; i < count; i++)
            {
                this.Ranges[i].Write(writer);
            }
        }

        public int RecordCount
        {
            get => 
                this.recordCount;
            set
            {
                this.CheckRecordCount(value);
                this.recordCount = value;
            }
        }

        public bool ToughRecalc { get; set; }

        public int Id
        {
            get => 
                this.id;
            set
            {
                base.CheckValue(value, 0, 0x7fff, "Id");
                this.id = value;
            }
        }

        public XlsRef8 BoundRange
        {
            get => 
                this.boundRange;
            set
            {
                if (value != null)
                {
                    this.boundRange = value;
                }
                else
                {
                    this.boundRange = new XlsRef8();
                }
            }
        }

        public List<XlsRef8> Ranges =>
            this.ranges;
    }
}

