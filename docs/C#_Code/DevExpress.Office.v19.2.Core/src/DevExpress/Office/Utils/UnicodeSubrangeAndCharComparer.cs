namespace DevExpress.Office.Utils
{
    using System;

    public class UnicodeSubrangeAndCharComparer : IComparable<UnicodeSubrange>
    {
        private readonly char ch;

        public UnicodeSubrangeAndCharComparer(char ch)
        {
            this.ch = ch;
        }

        public int CompareTo(UnicodeSubrange other) => 
            (this.ch >= other.LowValue) ? ((this.ch <= other.HiValue) ? 0 : -1) : 1;
    }
}

