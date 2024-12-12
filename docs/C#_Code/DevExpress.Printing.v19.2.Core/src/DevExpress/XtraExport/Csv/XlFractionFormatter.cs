namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal class XlFractionFormatter : XlNumericFormatterBase
    {
        private readonly int divisorCount;

        public XlFractionFormatter(int divisorCount)
        {
            this.divisorCount = divisorCount;
        }

        private double CalculateApproximation(int divident, int divisor, double value) => 
            ((double) divident) / ((double) divisor);

        private FractionData CalculateFractionParts(double value)
        {
            int num5;
            int num6;
            double num7;
            FractionData data = new FractionData();
            if (value == 0.0)
            {
                return data;
            }
            data.IntegerPart = Math.Truncate(value);
            value -= data.IntegerPart;
            int num = 0;
            int num2 = 1;
            int num3 = 1;
            int num4 = 1;
            if (Math.Abs(value) < Math.Abs((double) (value - num3)))
            {
                num7 = Math.Abs(value);
                num5 = num;
                num6 = num2;
            }
            else
            {
                num7 = Math.Abs((double) (value - num3));
                num5 = num3;
                num6 = num4;
            }
            while (true)
            {
                int divisor = num2 + num4;
                if (!this.IsDivisorOk(divisor))
                {
                    data.Dividend = num5;
                    data.Divisor = num6;
                    return data;
                }
                int divident = num + num3;
                if ((value * divisor) < divident)
                {
                    num3 = divident;
                    num4 = divisor;
                }
                else
                {
                    num = divident;
                    num2 = divisor;
                }
                double num10 = this.CalculateApproximation(divident, divisor, value);
                if (Math.Abs((double) (value - num10)) < num7)
                {
                    num7 = Math.Abs((double) (value - num10));
                    num5 = divident;
                    num6 = divisor;
                }
            }
        }

        protected override string FormatNumeric(XlVariantValue value, CultureInfo culture)
        {
            FractionData data = this.CalculateFractionParts(Math.Abs(value.NumericValue));
            if ((data.IntegerPart == 0.0) && (data.Dividend == 0))
            {
                return "0";
            }
            StringBuilder builder = new StringBuilder();
            if (value.NumericValue < 0.0)
            {
                builder.Append('-');
            }
            if (data.IntegerPart != 0.0)
            {
                builder.Append(data.IntegerPart.ToString(culture));
            }
            if (data.Dividend != 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(' ');
                }
                builder.Append(data.Dividend.ToString(culture));
                builder.Append('/');
                builder.Append(data.Divisor.ToString(culture));
            }
            return builder.ToString();
        }

        protected override string GetFormatString(CultureInfo culture) => 
            string.Empty;

        private bool IsDivisorOk(int divisor) => 
            (this.divisorCount >= 1) ? (divisor < Math.Pow(10.0, (double) this.divisorCount)) : (divisor <= 2);

        private class FractionData
        {
            public double IntegerPart { get; set; }

            public int Dividend { get; set; }

            public int Divisor { get; set; }
        }
    }
}

