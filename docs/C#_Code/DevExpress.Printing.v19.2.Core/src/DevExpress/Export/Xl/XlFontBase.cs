namespace DevExpress.Export.Xl
{
    using System;

    public abstract class XlFontBase
    {
        private const uint MaskBold = 1;
        private const uint MaskCondense = 2;
        private const uint MaskExtend = 4;
        private const uint MaskItalic = 8;
        private const uint MaskOutline = 0x10;
        private const uint MaskShadow = 0x20;
        private const uint MaskStrikeThrough = 0x40;
        private const uint MaskUnderline = 0x1f80;
        private const uint MaskScript = 0x6000;
        private const uint MaskSchemeStyle = 0x18000;
        private uint packedValues;
        private int charset;
        private string name;
        private double size;

        protected XlFontBase()
        {
        }

        public virtual void CopyFrom(XlFontBase value)
        {
            this.packedValues = value.packedValues;
            this.Charset = value.Charset;
            this.Name = value.Name;
            this.Size = value.Size;
        }

        public override bool Equals(object obj)
        {
            XlFontBase base2 = obj as XlFontBase;
            return ((base2 != null) ? ((this.packedValues == base2.packedValues) && ((this.Charset == base2.Charset) && ((this.Name == base2.Name) && (this.Size == base2.Size)))) : false);
        }

        private bool GetBooleanValue(uint mask) => 
            (this.packedValues & mask) != 0;

        public override int GetHashCode() => 
            ((this.packedValues.GetHashCode() ^ this.charset) ^ this.size.GetHashCode()) ^ ((this.name == null) ? 0 : this.name.GetHashCode());

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

        public bool Bold
        {
            get => 
                this.GetBooleanValue(1);
            set => 
                this.SetBooleanValue(1, value);
        }

        public bool Condense
        {
            get => 
                this.GetBooleanValue(2);
            set => 
                this.SetBooleanValue(2, value);
        }

        public bool Extend
        {
            get => 
                this.GetBooleanValue(4);
            set => 
                this.SetBooleanValue(4, value);
        }

        public bool Italic
        {
            get => 
                this.GetBooleanValue(8);
            set => 
                this.SetBooleanValue(8, value);
        }

        public bool Outline
        {
            get => 
                this.GetBooleanValue(0x10);
            set => 
                this.SetBooleanValue(0x10, value);
        }

        public bool Shadow
        {
            get => 
                this.GetBooleanValue(0x20);
            set => 
                this.SetBooleanValue(0x20, value);
        }

        public bool StrikeThrough
        {
            get => 
                this.GetBooleanValue(0x40);
            set => 
                this.SetBooleanValue(0x40, value);
        }

        public int Charset
        {
            get => 
                this.charset;
            set => 
                this.charset = value;
        }

        public string Name
        {
            get => 
                this.name;
            set => 
                this.name = value;
        }

        public double Size
        {
            get => 
                this.size;
            set => 
                this.size = value;
        }

        public XlFontSchemeStyles SchemeStyle
        {
            get => 
                (XlFontSchemeStyles) ((this.packedValues & 0x18000) >> 15);
            set
            {
                this.packedValues &= 0xfffe7fff;
                this.packedValues |= (uint) ((((int) value) << 15) & 0x18000);
            }
        }

        public XlScriptType Script
        {
            get => 
                (XlScriptType) ((this.packedValues & 0x6000) >> 13);
            set
            {
                this.packedValues &= 0xffff9fff;
                this.packedValues |= (uint) ((((int) value) << 13) & 0x6000);
            }
        }

        public XlUnderlineType Underline
        {
            get => 
                (XlUnderlineType) ((this.packedValues & 0x1f80) >> 7);
            set
            {
                this.packedValues &= 0xffffe07f;
                this.packedValues |= (uint) ((((int) value) << 7) & 0x1f80);
            }
        }
    }
}

