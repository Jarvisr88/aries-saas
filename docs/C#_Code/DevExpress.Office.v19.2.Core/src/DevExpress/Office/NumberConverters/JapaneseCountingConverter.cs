namespace DevExpress.Office.NumberConverters
{
    using System;
    using System.Text;

    public class JapaneseCountingConverter : OrdinalBasedNumberConverter
    {
        private static string[] japaneseDigits;
        private static string[] japaneseDegrees;

        static JapaneseCountingConverter()
        {
            string[] textArray1 = new string[10];
            textArray1[0] = "〇";
            textArray1[1] = "一";
            textArray1[2] = "二";
            textArray1[3] = "三";
            textArray1[4] = "四";
            textArray1[5] = "五";
            textArray1[6] = "六";
            textArray1[7] = "七";
            textArray1[8] = "八";
            textArray1[9] = "九";
            japaneseDigits = textArray1;
            japaneseDegrees = new string[] { "", "十", "百", "千", "万" };
        }

        public override string ConvertNumberCore(long value)
        {
            if (value == 0)
            {
                return japaneseDigits[0];
            }
            StringBuilder builder = new StringBuilder();
            int index = 0;
            while (true)
            {
                if (index < japaneseDegrees.Length)
                {
                    builder.Insert(0, this.GetDigitSymbol(value % ((long) 10), japaneseDegrees[index]));
                    long num1 = value / ((long) 10);
                    if ((value = num1) != 0)
                    {
                        index++;
                        continue;
                    }
                }
                return builder.ToString();
            }
        }

        private string GetDigitSymbol(long digit, string degreeSymbol) => 
            (digit == 0) ? string.Empty : ((digit == 1L) ? (!string.IsNullOrEmpty(degreeSymbol) ? degreeSymbol : japaneseDigits[1]) : (japaneseDigits[(int) ((IntPtr) digit)] + degreeSymbol));

        protected internal override long MinValue =>
            0L;

        protected internal override long MaxValue =>
            0x186a0L;

        protected internal override NumberingFormat Type =>
            NumberingFormat.JapaneseCounting;
    }
}

