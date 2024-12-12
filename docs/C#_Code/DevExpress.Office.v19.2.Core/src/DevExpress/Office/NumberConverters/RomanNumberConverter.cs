namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class RomanNumberConverter : OrdinalBasedNumberConverter
    {
        protected RomanNumberConverter()
        {
        }

        public override string ConvertNumberCore(long value)
        {
            string str = string.Empty;
            int index = this.Romans.Length - 1;
            while (index >= 0)
            {
                while (true)
                {
                    if (value < this.Arabics[index])
                    {
                        index--;
                        break;
                    }
                    value -= this.Arabics[index];
                    str = str + this.Romans[index];
                }
            }
            return str;
        }

        protected internal abstract int[] Arabics { get; }

        protected internal abstract string[] Romans { get; }
    }
}

