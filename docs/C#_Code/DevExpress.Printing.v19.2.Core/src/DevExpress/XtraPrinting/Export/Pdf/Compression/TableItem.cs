namespace DevExpress.XtraPrinting.Export.Pdf.Compression
{
    using System;

    public class TableItem
    {
        private int start;
        private int end;
        private int base_;
        private int extraBitsCount;

        public TableItem(int start, int end, int base_, int extraBitsCount)
        {
            this.start = start;
            this.end = end;
            this.base_ = base_;
            this.extraBitsCount = extraBitsCount;
        }

        public bool Contains(int value) => 
            (value >= this.start) && (value <= this.end);

        public void EncodeExtraBits(int value, BitBuffer bitBuffer)
        {
            bitBuffer.WriteBits(value - this.start, this.extraBitsCount);
        }

        public int Start =>
            this.start;

        public int End =>
            this.end;

        public int Base =>
            this.base_;

        public int ExtraBitsCount =>
            this.extraBitsCount;
    }
}

