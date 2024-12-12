namespace DevExpress.XtraExport.Xlsx
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct XlCellXf
    {
        private const byte MaskApplyNumberFormat = 1;
        private const byte MaskApplyFont = 2;
        private const byte MaskApplyFill = 4;
        private const byte MaskApplyBorder = 8;
        private const byte MaskApplyProtection = 0x10;
        private const byte MaskApplyAlignment = 0x20;
        private const byte MaskQuotePrefix = 0x40;
        private byte packedValues;
        public int NumberFormatId { get; set; }
        public int FontId { get; set; }
        public int FillId { get; set; }
        public int BorderId { get; set; }
        public int XfId { get; set; }
        public int AlignmentId { get; set; }
        public bool ApplyNumberFormat
        {
            get => 
                this.GetBooleanValue(1);
            set => 
                this.SetBooleanValue(1, value);
        }
        public bool ApplyFont
        {
            get => 
                this.GetBooleanValue(2);
            set => 
                this.SetBooleanValue(2, value);
        }
        public bool ApplyFill
        {
            get => 
                this.GetBooleanValue(4);
            set => 
                this.SetBooleanValue(4, value);
        }
        public bool ApplyBorder
        {
            get => 
                this.GetBooleanValue(8);
            set => 
                this.SetBooleanValue(8, value);
        }
        public bool ApplyAlignment
        {
            get => 
                this.GetBooleanValue(0x20);
            set => 
                this.SetBooleanValue(0x20, value);
        }
        public bool ApplyProtection
        {
            get => 
                this.GetBooleanValue(0x10);
            set => 
                this.SetBooleanValue(0x10, value);
        }
        public bool QuotePrefix
        {
            get => 
                this.GetBooleanValue(0x40);
            set => 
                this.SetBooleanValue(0x40, value);
        }
        private bool GetBooleanValue(byte mask) => 
            (this.packedValues & mask) != 0;

        private void SetBooleanValue(byte mask, bool bitVal)
        {
            if (bitVal)
            {
                this.packedValues = (byte) (this.packedValues | mask);
            }
            else
            {
                this.packedValues = (byte) (this.packedValues & ~mask);
            }
        }
    }
}

