namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsCondFmtColorScaleParams
    {
        private const int fixedPartSize = 6;
        private const double minDomain = 0.0;
        private const double midDomain = 0.5;
        private const double maxDomain = 1.0;
        private XlsCondFmtValueObject minValue = new XlsCondFmtValueObject();
        private XlsCondFmtValueObject midValue = new XlsCondFmtValueObject();
        private XlsCondFmtValueObject maxValue = new XlsCondFmtValueObject();
        private XlColor minColor = XlColor.Empty;
        private XlColor midColor = XlColor.Empty;
        private XlColor maxColor = XlColor.Empty;

        public int GetSize()
        {
            int num = (6 + (this.MinValue.GetSize() + 0x20)) + (this.MaxValue.GetSize() + 0x20);
            if (this.ColorScaleType == XlCondFmtColorScaleType.ColorScale3)
            {
                num += this.MidValue.GetSize() + 0x20;
            }
            return num;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort) 0);
            writer.Write((byte) 0);
            int num = (this.ColorScaleType == XlCondFmtColorScaleType.ColorScale3) ? 3 : 2;
            writer.Write((byte) num);
            writer.Write((byte) num);
            writer.Write((byte) 3);
            this.minValue.Write(writer);
            writer.Write((double) 0.0);
            if (num == 3)
            {
                this.midValue.Write(writer);
                writer.Write((double) 0.5);
            }
            this.maxValue.Write(writer);
            writer.Write((double) 1.0);
            writer.Write((double) 0.0);
            XlsCondFmtHelper.WriteColor(writer, this.MinColor);
            if (num == 3)
            {
                writer.Write((double) 0.5);
                XlsCondFmtHelper.WriteColor(writer, this.MidColor);
            }
            writer.Write((double) 1.0);
            XlsCondFmtHelper.WriteColor(writer, this.MaxColor);
        }

        public XlCondFmtColorScaleType ColorScaleType { get; set; }

        public XlsCondFmtValueObject MinValue =>
            this.minValue;

        public XlsCondFmtValueObject MidValue =>
            this.midValue;

        public XlsCondFmtValueObject MaxValue =>
            this.maxValue;

        public XlColor MinColor
        {
            get => 
                this.minColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.minColor = empty;
            }
        }

        public XlColor MidColor
        {
            get => 
                this.midColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.midColor = empty;
            }
        }

        public XlColor MaxColor
        {
            get => 
                this.maxColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.maxColor = empty;
            }
        }
    }
}

