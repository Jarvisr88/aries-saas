namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentCFEx : XlsContentBase, IXlsCondFmtWithRuleTemplate
    {
        private FutureRecordHeaderRefU header;
        private int id;
        private XlsDxfN12 format;

        public XlsContentCFEx()
        {
            FutureRecordHeaderRefU fu1 = new FutureRecordHeaderRefU();
            fu1.RecordTypeId = 0x87b;
            this.header = fu1;
            this.format = new XlsDxfN12();
        }

        public override int GetSize()
        {
            int num = 0x12;
            if (!this.IsCF12)
            {
                num += 0x19;
                if (this.HasFormat)
                {
                    num += this.format.GetSize();
                }
            }
            return num;
        }

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            this.header.RangeOfCells = true;
            this.header.Write(writer);
            writer.Write(this.IsCF12 ? 1 : 0);
            writer.Write((ushort) this.id);
            if (!this.IsCF12)
            {
                writer.Write((ushort) this.CFIndex);
                writer.Write(XlsCondFmtHelper.OperatorToCode(this.Operator));
                writer.Write((byte) this.RuleTemplate);
                writer.Write((ushort) this.Priority);
                byte num = 0;
                if (this.IsActive)
                {
                    num = (byte) (num | 1);
                }
                if (this.StopIfTrue)
                {
                    num = (byte) (num | 2);
                }
                writer.Write(num);
                writer.Write(this.HasFormat ? ((byte) 1) : ((byte) 0));
                if (this.HasFormat)
                {
                    this.Format.Write(writer);
                }
                XlsCondFmtHelper.WriteTemplateParams(writer, this);
            }
        }

        public XlsRef8 Range
        {
            get => 
                this.header.Range;
            set => 
                this.header.Range = value;
        }

        public bool IsCF12 { get; set; }

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

        public int CFIndex { get; set; }

        public XlCondFmtOperator Operator { get; set; }

        public XlsCFRuleTemplate RuleTemplate { get; set; }

        public int Priority { get; set; }

        public bool IsActive { get; set; }

        public bool StopIfTrue { get; set; }

        public bool HasFormat { get; set; }

        public XlsDxfN12 Format =>
            this.format;

        public bool FilterTop { get; set; }

        public bool FilterPercent { get; set; }

        public int FilterValue { get; set; }

        public XlCondFmtSpecificTextType TextRule { get; set; }

        public int StdDev { get; set; }

        public override FutureRecordHeaderBase RecordHeader =>
            this.header;
    }
}

