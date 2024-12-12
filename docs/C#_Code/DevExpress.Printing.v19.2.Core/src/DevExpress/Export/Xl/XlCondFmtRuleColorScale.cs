namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class XlCondFmtRuleColorScale : XlCondFmtRule
    {
        private XlCondFmtValueObject minValue;
        private XlCondFmtValueObject midpointValue;
        private XlCondFmtValueObject maxValue;
        private XlColor minColor;
        private XlColor midpointColor;
        private XlColor maxColor;

        public XlCondFmtRuleColorScale() : base(XlCondFmtType.ColorScale)
        {
            XlCondFmtValueObject obj1 = new XlCondFmtValueObject();
            obj1.ObjectType = XlCondFmtValueObjectType.Min;
            this.minValue = obj1;
            XlCondFmtValueObject obj2 = new XlCondFmtValueObject();
            obj2.ObjectType = XlCondFmtValueObjectType.Percentile;
            obj2.Value = 50.0;
            this.midpointValue = obj2;
            XlCondFmtValueObject obj3 = new XlCondFmtValueObject();
            obj3.ObjectType = XlCondFmtValueObjectType.Max;
            this.maxValue = obj3;
            this.ColorScaleType = XlCondFmtColorScaleType.ColorScale3;
            this.minColor = DXColor.FromArgb(0xff, 0xf8, 0x69, 0x6b);
            this.midpointColor = DXColor.FromArgb(0xff, 0xff, 0xeb, 0x84);
            this.maxColor = DXColor.FromArgb(0xff, 0x63, 190, 0x7b);
        }

        public XlCondFmtColorScaleType ColorScaleType { get; set; }

        public XlCondFmtValueObject MinValue =>
            this.minValue;

        public XlCondFmtValueObject MidpointValue =>
            this.midpointValue;

        public XlCondFmtValueObject MaxValue =>
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

        public XlColor MidpointColor
        {
            get => 
                this.midpointColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.midpointColor = empty;
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

