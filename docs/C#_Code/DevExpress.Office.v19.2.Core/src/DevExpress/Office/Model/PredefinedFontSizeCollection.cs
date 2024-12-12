namespace DevExpress.Office.Model
{
    using DevExpress.Utils.Internal;
    using System;
    using System.Collections.Generic;

    public class PredefinedFontSizeCollection : List<int>
    {
        public const int MinFontSize = 1;
        public const int MaxFontSize = 0x5dc;

        public PredefinedFontSizeCollection()
        {
            this.CreateDefaultContent();
        }

        protected internal virtual int CalcNextTen(int value) => 
            value + (10 - (value % 10));

        protected internal virtual int CalcPrevTen(int value) => 
            ((value % 10) == 0) ? (value - 10) : (value - (value % 10));

        public int CalculateNextFontSize(int fontSize) => 
            ValidateFontSize(this.CalculateNextFontSizeCore(fontSize));

        protected internal virtual int CalculateNextFontSizeCore(int fontSize)
        {
            if (base.Count == 0)
            {
                return (fontSize + 1);
            }
            if (fontSize < base[0])
            {
                return (fontSize + 1);
            }
            int num = base.BinarySearch(fontSize);
            num = (num >= 0) ? (num + 1) : ~num;
            return ((num >= base.Count) ? this.CalcNextTen(fontSize) : base[num]);
        }

        public int CalculatePreviousFontSize(int fontSize) => 
            ValidateFontSize(this.CalculatePreviousFontSizeCore(fontSize));

        protected internal virtual int CalculatePreviousFontSizeCore(int fontSize)
        {
            if (base.Count == 0)
            {
                return (fontSize - 1);
            }
            if (fontSize <= base[0])
            {
                return (fontSize - 1);
            }
            int num = base.BinarySearch(fontSize);
            return ((num < 0) ? ((num == ~base.Count) ? ((fontSize <= this.CalcNextTen(base[base.Count - 1])) ? base[~num - 1] : this.CalcPrevTen(fontSize)) : base[~num - 1]) : base[num - 1]);
        }

        protected internal virtual void CreateDefaultContent()
        {
            foreach (int num in FontSizeManager.GetPredefinedFontSizes())
            {
                base.Add(num);
            }
        }

        public static int ValidateFontSize(int value) => 
            Math.Max(Math.Min(value, 0x5dc), 1);
    }
}

