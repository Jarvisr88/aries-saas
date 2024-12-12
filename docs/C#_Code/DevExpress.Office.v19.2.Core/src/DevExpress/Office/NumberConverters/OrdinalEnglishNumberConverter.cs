namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalEnglishNumberConverter : OrdinalBasedNumberConverter
    {
        private static string[] ending = new string[] { "st", "nd", "rd", "th" };

        public override string ConvertNumberCore(long value)
        {
            long num = value % ((long) 100);
            if (num >= 0x15)
            {
                value -= 1L;
                num = value % ((long) 10);
                return ((num >= 3L) ? $"{(value + 1L)}{ending[3]}" : $"{(value + 1L)}{ending[(int) ((IntPtr) (num % 3L))]}");
            }
            long num1 = num - 1L;
            if (num1 > 2L)
            {
                long local1 = num1;
            }
            else
            {
                switch (((uint) num1))
                {
                    case 0:
                        return $"{value}{ending[0]}";

                    case 1:
                        return $"{value}{ending[1]}";

                    case 2:
                        return $"{value}{ending[2]}";

                    default:
                        break;
                }
            }
            return $"{value}{ending[3]}";
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.Ordinal;
    }
}

