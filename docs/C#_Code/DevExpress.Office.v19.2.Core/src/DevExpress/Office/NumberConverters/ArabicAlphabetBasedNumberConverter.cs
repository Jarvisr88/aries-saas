namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class ArabicAlphabetBasedNumberConverter : AlphabetBasedNumberConverter
    {
        protected ArabicAlphabetBasedNumberConverter()
        {
        }

        public override string ConvertNumberCore(long value)
        {
            if (value == 0)
            {
                return string.Empty;
            }
            value -= 1L;
            int n = (((int) value) / this.AlphabetSize) + 1;
            return RepeatString(this.Alphabet[(int) ((IntPtr) (value % ((long) this.AlphabetSize)))].ToString() + "‌", n);
        }

        private static string RepeatString(string s, int n)
        {
            string str = s;
            for (int i = 0; i < (n - 1); i++)
            {
                str = str + s;
            }
            return str;
        }
    }
}

