namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class XlCondFmtRuleDataBar : XlCondFmtRuleWithGuid
    {
        private XlCondFmtValueObject minValue;
        private XlCondFmtValueObject maxValue;
        private int minLength;
        private int maxLength;
        private XlColor fillColor;
        private XlColor borderColor;
        private XlColor negativeFillColor;
        private XlColor negativeBorderColor;
        private XlColor axisColor;

        public XlCondFmtRuleDataBar() : base(XlCondFmtType.DataBar)
        {
            this.minValue = new XlCondFmtValueObject();
            this.maxValue = new XlCondFmtValueObject();
            this.maxLength = 100;
            this.Direction = XlDataBarDirection.Context;
            this.fillColor = XlColor.Empty;
            this.borderColor = XlColor.Empty;
            this.negativeFillColor = DXColor.Red;
            this.negativeBorderColor = XlColor.Empty;
            this.AxisPosition = XlCondFmtAxisPosition.Automatic;
            this.axisColor = DXColor.Black;
            this.ShowValues = true;
            this.minValue.ObjectType = XlCondFmtValueObjectType.AutoMin;
            this.maxValue.ObjectType = XlCondFmtValueObjectType.AutoMax;
        }

        private void CheckLength(int value, string name)
        {
            if ((value < 0) || (value > 100))
            {
                throw new ArgumentException(name);
            }
        }

        public XlDataBarDirection Direction { get; set; }

        public bool GradientFill { get; set; }

        public XlColor FillColor
        {
            get => 
                this.fillColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.fillColor = empty;
            }
        }

        public XlColor BorderColor
        {
            get => 
                this.borderColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.borderColor = empty;
            }
        }

        public XlColor NegativeFillColor
        {
            get => 
                this.negativeFillColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.negativeFillColor = empty;
            }
        }

        public XlColor NegativeBorderColor
        {
            get => 
                this.negativeBorderColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.negativeBorderColor = empty;
            }
        }

        public XlCondFmtAxisPosition AxisPosition { get; set; }

        public XlColor AxisColor
        {
            get => 
                this.axisColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.axisColor = empty;
            }
        }

        public XlCondFmtValueObject MinValue =>
            this.minValue;

        public XlCondFmtValueObject MaxValue =>
            this.maxValue;

        public bool ShowValues { get; set; }

        public int MinLength
        {
            get => 
                this.minLength;
            set
            {
                this.CheckLength(value, "MinLength");
                this.minLength = value;
            }
        }

        public int MaxLength
        {
            get => 
                this.maxLength;
            set
            {
                this.CheckLength(value, "MaxLength");
                this.maxLength = value;
            }
        }
    }
}

