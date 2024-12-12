namespace DevExpress.Export.Xl
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class XlCellAlignment : ISupportsCopyFrom<XlCellAlignment>
    {
        private const uint MaskHorizontalAlignment = 7;
        private const uint MaskVerticalAlignment = 0x38;
        private const uint MaskWrapText = 0x40;
        private const uint MaskJustifyLastLine = 0x80;
        private const uint MaskShrinkToFit = 0x100;
        private const uint MaskReadingOrder = 0x600;
        private uint packedValues = 0x10;
        private int textRotation;
        private byte indent;
        private int relativeIndent;

        protected virtual void CheckIndent(byte value)
        {
            if (value > 250)
            {
                throw new ArgumentOutOfRangeException("Indent out of range 0...250!");
            }
        }

        protected virtual void CheckRelativeIndent(int value)
        {
            if (((value < -15) || (value > 15)) && (value != 0xff))
            {
                throw new ArgumentException("Relative indent must be from -15 to 15 or 255.");
            }
        }

        protected virtual void CheckTextRotation(int value)
        {
            if (((value < 0) || (value > 180)) && (value != 0xff))
            {
                throw new ArgumentException("Text rotation must be 0...180 degrees or 255 for vertical text.");
            }
        }

        public XlCellAlignment Clone()
        {
            XlCellAlignment alignment = new XlCellAlignment();
            alignment.CopyFrom(this);
            return alignment;
        }

        public void CopyFrom(XlCellAlignment value)
        {
            Guard.ArgumentNotNull(value, "value");
            this.packedValues = value.packedValues;
            this.textRotation = value.textRotation;
            this.indent = value.indent;
            this.relativeIndent = value.relativeIndent;
        }

        public override bool Equals(object obj)
        {
            XlCellAlignment alignment = obj as XlCellAlignment;
            return ((alignment != null) ? ((this.packedValues == alignment.packedValues) && ((this.textRotation == alignment.textRotation) && ((this.indent == alignment.indent) && (this.relativeIndent == alignment.relativeIndent)))) : false);
        }

        public static XlCellAlignment FromHV(XlHorizontalAlignment horizontalAlignment, XlVerticalAlignment verticalAlignment) => 
            new XlCellAlignment { 
                HorizontalAlignment = horizontalAlignment,
                VerticalAlignment = verticalAlignment
            };

        private bool GetBooleanValue(uint mask) => 
            (this.packedValues & mask) != 0;

        public override int GetHashCode() => 
            ((this.packedValues.GetHashCode() ^ this.textRotation) ^ this.indent) ^ this.relativeIndent;

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

        public bool WrapText
        {
            get => 
                this.GetBooleanValue(0x40);
            set => 
                this.SetBooleanValue(0x40, value);
        }

        public bool JustifyLastLine
        {
            get => 
                this.GetBooleanValue(0x80);
            set => 
                this.SetBooleanValue(0x80, value);
        }

        public bool ShrinkToFit
        {
            get => 
                this.GetBooleanValue(0x100);
            set => 
                this.SetBooleanValue(0x100, value);
        }

        public int TextRotation
        {
            get => 
                this.textRotation;
            set
            {
                this.CheckTextRotation(value);
                this.textRotation = value;
            }
        }

        public byte Indent
        {
            get => 
                this.indent;
            set
            {
                if (value > 250)
                {
                    throw new ArgumentOutOfRangeException("Indent out of range 0...250!");
                }
                this.indent = value;
            }
        }

        public int RelativeIndent
        {
            get => 
                this.relativeIndent;
            set
            {
                this.CheckRelativeIndent(value);
                this.relativeIndent = value;
            }
        }

        public XlHorizontalAlignment HorizontalAlignment
        {
            get => 
                ((XlHorizontalAlignment) this.packedValues) & XlHorizontalAlignment.Distributed;
            set
            {
                this.packedValues &= (uint) (-8);
                this.packedValues = (uint) (((XlHorizontalAlignment) this.packedValues) | (value & XlHorizontalAlignment.Distributed));
            }
        }

        public XlVerticalAlignment VerticalAlignment
        {
            get => 
                (XlVerticalAlignment) ((this.packedValues & 0x38) >> 3);
            set
            {
                this.packedValues &= (uint) (-57);
                this.packedValues |= (uint) ((((int) value) << 3) & 0x38);
            }
        }

        public XlReadingOrder ReadingOrder
        {
            get => 
                (XlReadingOrder) ((this.packedValues & 0x600) >> 9);
            set
            {
                this.packedValues &= 0xfffff9ff;
                this.packedValues |= (uint) ((((int) value) << 9) & 0x600);
            }
        }
    }
}

