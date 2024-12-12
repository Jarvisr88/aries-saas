namespace DevExpress.Office.NumberConverters
{
    using System;
    using System.Text;

    public class DescriptiveOrdinalThaiNumberConverter : DescriptiveThaiNumberConverterBase
    {
        protected internal override string ConvertDigitsToString(DigitInfoCollection digits)
        {
            StringBuilder builder = new StringBuilder();
            int count = digits.Count;
            builder.Append("ที่");
            for (int i = 0; i < count; i++)
            {
                builder.Append(digits[i].ConvertToString());
            }
            if (builder.Length > 0)
            {
                builder[0] = char.ToUpper(builder[0]);
            }
            return builder.ToString();
        }

        protected internal override NumberingFormat Type =>
            NumberingFormat.OrdinalText;
    }
}

