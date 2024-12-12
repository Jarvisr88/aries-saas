namespace DevExpress.Office.NumberConverters
{
    using System;
    using System.Text;

    public class HexNumberConverter : OrdinalBasedNumberConverter
    {
        private static char[] hexdigit = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        public override string ConvertNumberCore(long value)
        {
            StringBuilder builder = new StringBuilder();
            while (value != 0)
            {
                builder.Insert(0, hexdigit[(int) ((IntPtr) (value % ((long) 0x10)))]);
                value = value >> 4;
            }
            return builder.ToString();
        }

        protected internal override long MinValue =>
            0L;

        protected internal override NumberingFormat Type =>
            NumberingFormat.Hex;
    }
}

