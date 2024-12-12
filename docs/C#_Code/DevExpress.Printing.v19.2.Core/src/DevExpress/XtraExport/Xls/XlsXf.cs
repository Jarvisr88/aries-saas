namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    internal class XlsXf
    {
        private const long MaskIsStyle = 1L;
        private const long MaskIsLocked = 2L;
        private const long MaskIsHidden = 4L;
        private const long MaskQuotePrefix = 8L;
        private const long MaskApplyNumberFormat = 0x10L;
        private const long MaskApplyFont = 0x20L;
        private const long MaskApplyFill = 0x40L;
        private const long MaskApplyBorder = 0x80L;
        private const long MaskApplyProtection = 0x100L;
        private const long MaskApplyAlignment = 0x200L;
        private const long MaskNumberFormatId = 0x1ff000L;
        private const long MaskFontId = 0x3ff000000L;
        private const long MaskStyleId = 0x1fff000000000L;
        private long packedValues = 0x3f2L;

        public override bool Equals(object obj)
        {
            XlsXf xf = obj as XlsXf;
            return ((xf != null) ? ((this.packedValues == xf.packedValues) && (this.Fill.Equals(xf.Fill) && (this.Border.Equals(xf.Border) && this.Alignment.Equals(xf.Alignment)))) : false);
        }

        private bool GetBooleanValue(long mask) => 
            (this.packedValues & mask) != 0L;

        public override int GetHashCode() => 
            ((this.packedValues.GetHashCode() ^ this.Fill.GetHashCode()) ^ this.Border.GetHashCode()) ^ this.Alignment.GetHashCode();

        private void SetBooleanValue(long mask, bool bitVal)
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

        public bool IsStyleFormat
        {
            get => 
                this.GetBooleanValue(1L);
            set => 
                this.SetBooleanValue(1L, value);
        }

        public bool IsLocked
        {
            get => 
                this.GetBooleanValue(2L);
            set => 
                this.SetBooleanValue(2L, value);
        }

        public bool IsHidden
        {
            get => 
                this.GetBooleanValue(4L);
            set => 
                this.SetBooleanValue(4L, value);
        }

        public bool ApplyNumberFormat
        {
            get => 
                this.GetBooleanValue((long) 0x10);
            set => 
                this.SetBooleanValue((long) 0x10, value);
        }

        public bool ApplyFont
        {
            get => 
                this.GetBooleanValue((long) 0x20);
            set => 
                this.SetBooleanValue((long) 0x20, value);
        }

        public bool ApplyFill
        {
            get => 
                this.GetBooleanValue((long) 0x40);
            set => 
                this.SetBooleanValue((long) 0x40, value);
        }

        public bool ApplyBorder
        {
            get => 
                this.GetBooleanValue(0x80L);
            set => 
                this.SetBooleanValue(0x80L, value);
        }

        public bool ApplyAlignment
        {
            get => 
                this.GetBooleanValue(0x200L);
            set => 
                this.SetBooleanValue(0x200L, value);
        }

        public bool ApplyProtection
        {
            get => 
                this.GetBooleanValue(0x100L);
            set => 
                this.SetBooleanValue(0x100L, value);
        }

        public bool QuotePrefix
        {
            get => 
                this.GetBooleanValue(8L);
            set => 
                this.SetBooleanValue(8L, value);
        }

        public int FontId
        {
            get => 
                (int) ((this.packedValues & 0x3ff000000L) >> 0x18);
            set
            {
                this.packedValues &= -17163091969L;
                this.packedValues |= (value << 0x18) & 0x3ff000000L;
            }
        }

        public int NumberFormatId
        {
            get => 
                (int) ((this.packedValues & 0x1ff000L) >> 12);
            set
            {
                this.packedValues &= -2093057L;
                this.packedValues |= (value << 12) & 0x1ff000L;
            }
        }

        public int StyleId
        {
            get => 
                (int) ((this.packedValues & 0x1fff000000000L) >> 0x24);
            set
            {
                this.packedValues &= -562881233944577L;
                this.packedValues |= (value << 0x24) & 0x1fff000000000L;
            }
        }

        public XlFill Fill { get; set; }

        public XlBorder Border { get; set; }

        public XlCellAlignment Alignment { get; set; }
    }
}

