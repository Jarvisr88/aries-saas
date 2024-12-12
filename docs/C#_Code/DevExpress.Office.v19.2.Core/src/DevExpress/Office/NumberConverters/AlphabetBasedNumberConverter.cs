namespace DevExpress.Office.NumberConverters
{
    using System;

    public abstract class AlphabetBasedNumberConverter : OrdinalBasedNumberConverter
    {
        protected AlphabetBasedNumberConverter()
        {
        }

        public override string ConvertNumberCore(long value)
        {
            if (value == 0)
            {
                return string.Empty;
            }
            value -= 1L;
            return new string(this.Alphabet[(int) ((IntPtr) (value % ((long) this.AlphabetSize)))], (((int) value) / this.AlphabetSize) + 1);
        }

        protected internal override long MinValue =>
            0L;

        protected internal override long MaxValue =>
            780L;

        protected internal abstract char[] Alphabet { get; }

        protected internal abstract int AlphabetSize { get; }
    }
}

