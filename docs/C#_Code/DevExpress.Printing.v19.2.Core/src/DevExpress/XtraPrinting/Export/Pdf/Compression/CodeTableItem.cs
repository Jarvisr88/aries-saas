namespace DevExpress.XtraPrinting.Export.Pdf.Compression
{
    using System;

    public class CodeTableItem
    {
        private int start;
        private int end;
        private int codeLength;
        private int codeStart;

        public CodeTableItem(int start, int end, int codeLength, int codeStart)
        {
            this.start = start;
            this.end = end;
            this.codeLength = codeLength;
            this.codeStart = codeStart;
        }

        public bool Contains(int value) => 
            (value >= this.start) && (value <= this.end);

        public void Encode(int value, BitBuffer bitBuffer)
        {
            int b = Utils.BitReverse(this.codeStart + (value - this.start), this.codeLength);
            bitBuffer.WriteBits(b, this.codeLength);
        }

        public int Start =>
            this.start;

        public int End =>
            this.end;

        public int CodeLength =>
            this.codeLength;

        public int CodeStart =>
            this.codeStart;
    }
}

