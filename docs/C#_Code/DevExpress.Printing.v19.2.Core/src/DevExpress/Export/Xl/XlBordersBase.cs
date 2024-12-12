namespace DevExpress.Export.Xl
{
    using System;
    using System.ComponentModel;

    public abstract class XlBordersBase
    {
        private const uint MaskLeftLineStyle = 15;
        private const uint MaskRightLineStyle = 240;
        private const uint MaskTopLineStyle = 0xf00;
        private const uint MaskBottomLineStyle = 0xf000;
        private const uint MaskDiagonalLineStyle = 0xf0000;
        private const uint MaskHorizontalLineStyle = 0xf00000;
        private const uint MaskVerticalLineStyle = 0xf000000;
        private const uint MaskDiagonalUp = 0x10000000;
        private const uint MaskDiagonalDown = 0x20000000;
        private const uint MaskOutline = 0x40000000;
        private uint packedValues = 0x40000000;

        protected XlBordersBase()
        {
        }

        public void CopyFrom(XlBordersBase value)
        {
            this.packedValues = value.packedValues;
        }

        public override bool Equals(object obj)
        {
            XlBordersBase base2 = obj as XlBordersBase;
            return ((base2 != null) ? (this.packedValues == base2.packedValues) : false);
        }

        private bool GetBooleanValue(uint mask) => 
            (this.packedValues & mask) != 0;

        private XlBorderLineStyle GetBorderDiagonalDownLineStyle() => 
            this.DiagonalDown ? this.DiagonalLineStyle : XlBorderLineStyle.None;

        private XlBorderLineStyle GetBorderDiagonalUpLineStyle() => 
            this.DiagonalUp ? this.DiagonalLineStyle : XlBorderLineStyle.None;

        private XlBorderLineStyle GetBorderLineStyleValue(uint mask, int bits) => 
            (XlBorderLineStyle) ((this.packedValues & mask) >> (bits & 0x1f));

        public override int GetHashCode() => 
            (int) this.packedValues;

        private void SetBooleanValue(uint mask, bool bitVal)
        {
            if (bitVal)
            {
                this.packedValues |= mask;
            }
            else
            {
                this.packedValues &= ~mask;
            }
        }

        private void SetBorderDiagonalDownLineStyle(XlBorderLineStyle lineStyle)
        {
            this.DiagonalDown = lineStyle != XlBorderLineStyle.None;
            this.DiagonalLineStyle = lineStyle;
        }

        private void SetBorderDiagonalUpLineStyle(XlBorderLineStyle lineStyle)
        {
            this.DiagonalUp = lineStyle != XlBorderLineStyle.None;
            this.DiagonalLineStyle = lineStyle;
        }

        private void SetBorderLineStyleValue(uint mask, int bits, XlBorderLineStyle value)
        {
            this.packedValues &= ~mask;
            this.packedValues |= (((uint) value) << (bits & 0x1f)) & mask;
        }

        public XlBorderLineStyle LeftLineStyle
        {
            get => 
                this.GetBorderLineStyleValue(15, 0);
            set => 
                this.SetBorderLineStyleValue(15, 0, value);
        }

        public XlBorderLineStyle RightLineStyle
        {
            get => 
                this.GetBorderLineStyleValue(240, 4);
            set => 
                this.SetBorderLineStyleValue(240, 4, value);
        }

        public XlBorderLineStyle TopLineStyle
        {
            get => 
                this.GetBorderLineStyleValue(0xf00, 8);
            set => 
                this.SetBorderLineStyleValue(0xf00, 8, value);
        }

        public XlBorderLineStyle BottomLineStyle
        {
            get => 
                this.GetBorderLineStyleValue(0xf000, 12);
            set => 
                this.SetBorderLineStyleValue(0xf000, 12, value);
        }

        public XlBorderLineStyle DiagonalLineStyle
        {
            get => 
                this.GetBorderLineStyleValue(0xf0000, 0x10);
            set => 
                this.SetBorderLineStyleValue(0xf0000, 0x10, value);
        }

        [Browsable(false)]
        public XlBorderLineStyle HorizontalLineStyle
        {
            get => 
                this.GetBorderLineStyleValue(0xf00000, 20);
            set => 
                this.SetBorderLineStyleValue(0xf00000, 20, value);
        }

        [Browsable(false)]
        public XlBorderLineStyle VerticalLineStyle
        {
            get => 
                this.GetBorderLineStyleValue(0xf000000, 0x18);
            set => 
                this.SetBorderLineStyleValue(0xf000000, 0x18, value);
        }

        public XlBorderLineStyle DiagonalUpLineStyle
        {
            get => 
                this.GetBorderDiagonalUpLineStyle();
            set => 
                this.SetBorderDiagonalUpLineStyle(value);
        }

        public XlBorderLineStyle DiagonalDownLineStyle
        {
            get => 
                this.GetBorderDiagonalDownLineStyle();
            set => 
                this.SetBorderDiagonalDownLineStyle(value);
        }

        public bool DiagonalUp
        {
            get => 
                this.GetBooleanValue(0x10000000);
            set => 
                this.SetBooleanValue(0x10000000, value);
        }

        public bool DiagonalDown
        {
            get => 
                this.GetBooleanValue(0x20000000);
            set => 
                this.SetBooleanValue(0x20000000, value);
        }

        public bool Outline
        {
            get => 
                this.GetBooleanValue(0x40000000);
            set => 
                this.SetBooleanValue(0x40000000, value);
        }

        protected bool HasBorderLines =>
            (this.packedValues & 0xfffffff) != 0;
    }
}

