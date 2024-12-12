namespace DevExpress.Office.Utils
{
    using System;

    public class UnicodeSubrange
    {
        private readonly char lowValue;
        private readonly char hiValue;
        private readonly int bit;

        public UnicodeSubrange(char low, char hi, int bit)
        {
            this.lowValue = low;
            this.hiValue = hi;
            this.bit = bit;
        }

        public bool ContainsChar(char ch) => 
            (ch >= this.LowValue) && (ch <= this.HiValue);

        public char LowValue =>
            this.lowValue;

        public char HiValue =>
            this.hiValue;

        public int Bit =>
            this.bit;
    }
}

