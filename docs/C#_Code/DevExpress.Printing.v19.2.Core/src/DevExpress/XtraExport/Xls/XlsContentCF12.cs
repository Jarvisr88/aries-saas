namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentCF12 : XlsContentCFBase, IXlsCondFmtWithRuleTemplate
    {
        private FutureRecordHeader header;
        private XlsDxfN12 format;
        private byte[] activeFormulaBytes;
        private int priority;
        private int stdDev;
        private XlsCondFmtDatabarParams dataBarParams;
        private XlsCondFmtFilterParams filterParams;
        private XlsCondFmtIconSetParams iconSetParams;
        private XlsCondFmtColorScaleParams colorScaleParams;

        public XlsContentCF12()
        {
            FutureRecordHeader header1 = new FutureRecordHeader();
            header1.RecordTypeId = 0x87a;
            this.header = header1;
            this.format = new XlsDxfN12();
            this.activeFormulaBytes = new byte[0];
            this.dataBarParams = new XlsCondFmtDatabarParams();
            this.filterParams = new XlsCondFmtFilterParams();
            this.iconSetParams = new XlsCondFmtIconSetParams();
            this.colorScaleParams = new XlsCondFmtColorScaleParams();
            this.RuleTemplate = XlsCFRuleTemplate.CellValue;
        }

        public override int GetSize()
        {
            int num = ((((12 + base.GetSize()) + 2) + this.activeFormulaBytes.Length) + 5) + 0x11;
            if (base.RuleType == XlCondFmtType.DataBar)
            {
                num += this.dataBarParams.GetSize();
            }
            else if (base.RuleType == XlCondFmtType.Top10)
            {
                num += this.filterParams.GetSize();
            }
            else if (base.RuleType == XlCondFmtType.IconSet)
            {
                num += this.iconSetParams.GetSize();
            }
            else if (base.RuleType == XlCondFmtType.ColorScale)
            {
                num += this.colorScaleParams.GetSize();
            }
            return num;
        }

        public override void Write(BinaryWriter writer)
        {
            this.header.RangeOfCells = false;
            this.header.Write(writer);
            base.Write(writer);
            ushort length = (ushort) this.activeFormulaBytes.Length;
            writer.Write(length);
            if (length > 0)
            {
                writer.Write(this.activeFormulaBytes);
            }
            byte num2 = 0;
            if (this.StopIfTrue)
            {
                num2 = (byte) (num2 | 2);
            }
            writer.Write(num2);
            writer.Write((ushort) this.Priority);
            writer.Write((ushort) this.RuleTemplate);
            XlsCondFmtHelper.WriteTemplateParams(writer, this);
            if (base.RuleType == XlCondFmtType.DataBar)
            {
                this.dataBarParams.Write(writer);
            }
            else if (base.RuleType == XlCondFmtType.Top10)
            {
                this.filterParams.Write(writer);
            }
            else if (base.RuleType == XlCondFmtType.IconSet)
            {
                this.iconSetParams.Write(writer);
            }
            else if (base.RuleType == XlCondFmtType.ColorScale)
            {
                this.colorScaleParams.Write(writer);
            }
        }

        public XlsDxfN12 Format =>
            this.format;

        public byte[] ActiveFormulaBytes
        {
            get => 
                this.activeFormulaBytes;
            set
            {
                if ((value == null) || (value.Length < 2))
                {
                    this.activeFormulaBytes = new byte[0];
                }
                else
                {
                    int length = BitConverter.ToUInt16(value, 0);
                    this.activeFormulaBytes = new byte[length];
                    Array.Copy(value, 2, this.activeFormulaBytes, 0, length);
                }
            }
        }

        public bool StopIfTrue { get; set; }

        public int Priority
        {
            get => 
                this.priority;
            set
            {
                base.CheckValue(value, 0, 0xffff, "Priority");
                this.priority = value;
            }
        }

        public XlsCFRuleTemplate RuleTemplate { get; set; }

        public bool FilterTop { get; set; }

        public bool FilterPercent { get; set; }

        public int FilterValue { get; set; }

        public XlCondFmtSpecificTextType TextRule { get; set; }

        public int StdDev
        {
            get => 
                this.stdDev;
            set
            {
                base.CheckValue(value, 0, 3, "StdDev");
                this.stdDev = value;
            }
        }

        public XlsCondFmtDatabarParams DataBarParams =>
            this.dataBarParams;

        public XlsCondFmtFilterParams FilterParams =>
            this.filterParams;

        public XlsCondFmtIconSetParams IconSetParams =>
            this.iconSetParams;

        public XlsCondFmtColorScaleParams ColorScaleParams =>
            this.colorScaleParams;

        public override FutureRecordHeaderBase RecordHeader =>
            this.header;

        protected override XlsDxfN DifferentialFormat =>
            this.format;
    }
}

