namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsCondFmtDatabarParams
    {
        private const int fixedPartSize = 0x16;
        private XlsCondFmtValueObject maxValue = new XlsCondFmtValueObject();
        private XlsCondFmtValueObject minValue = new XlsCondFmtValueObject();
        private XlColor barColor = XlColor.Empty;

        public int GetSize() => 
            (0x16 + this.maxValue.GetSize()) + this.minValue.GetSize();

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort) 0);
            writer.Write((byte) 0);
            byte num = 0;
            if (this.RightToLeft)
            {
                num = (byte) (num | 1);
            }
            if (this.ShowBarOnly)
            {
                num = (byte) (num | 2);
            }
            writer.Write(num);
            writer.Write((byte) this.PercentMin);
            writer.Write((byte) this.PercentMax);
            XlsCondFmtHelper.WriteColor(writer, this.BarColor);
            this.minValue.Write(writer);
            this.maxValue.Write(writer);
        }

        public bool RightToLeft { get; set; }

        public bool ShowBarOnly { get; set; }

        public int PercentMin { get; set; }

        public int PercentMax { get; set; }

        public XlColor BarColor
        {
            get => 
                this.barColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.barColor = empty;
            }
        }

        public XlsCondFmtValueObject MaxValue =>
            this.maxValue;

        public XlsCondFmtValueObject MinValue =>
            this.minValue;
    }
}

