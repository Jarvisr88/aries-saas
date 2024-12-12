namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;

    internal class XlNumFmtNumericFraction : XlNumFmtNumericSimple
    {
        private int dividendCount;
        private int divisorCount;

        protected XlNumFmtNumericFraction()
        {
        }

        public XlNumFmtNumericFraction(IEnumerable<IXlNumFmtElement> elements, int percentCount, int integerCount, int preFractionIndex, int fractionSeparatorIndex, int divisorIndex, int dividentCount, int divisorCount, bool grouping, bool isNegativePart) : base(elements, percentCount, integerCount, fractionSeparatorIndex, divisorIndex, preFractionIndex, grouping, isNegativePart)
        {
            this.dividendCount = dividentCount;
            this.divisorCount = divisorCount;
        }

        protected virtual Size CalculateRationalApproximation(decimal value)
        {
            int num6;
            int num7;
            decimal num8;
            int num = (int) value;
            value -= num;
            int num2 = 0;
            int num3 = 1;
            int num4 = 1;
            int num5 = 1;
            if (Math.Abs(value) < Math.Abs((decimal) (value - num4)))
            {
                num8 = Math.Abs(value);
                num6 = num2;
                num7 = num3;
            }
            else
            {
                num8 = Math.Abs((decimal) (value - num4));
                num6 = num4;
                num7 = num5;
            }
            int num9 = (int) Math.Pow(10.0, (double) this.divisorCount);
            while (true)
            {
                int num10 = num3 + num5;
                if (num10 >= num9)
                {
                    return new Size(num6 + (num * num7), num7);
                }
                int num11 = num2 + num4;
                if ((value * num10) < num11)
                {
                    num4 = num11;
                    num5 = num10;
                }
                else
                {
                    num2 = num11;
                    num3 = num10;
                }
                decimal num12 = num11 / num10;
                if (Math.Abs((decimal) (value - num12)) < num8)
                {
                    num8 = Math.Abs((decimal) (value - num12));
                    num6 = num11;
                    num7 = num10;
                }
            }
        }

        public override XlNumFmtResult FormatCore(XlVariantValue value, CultureInfo culture)
        {
            Size size;
            XlNumFmtResult result = new XlNumFmtResult();
            if (!value.IsNumeric)
            {
                result.Text = value.ToText(culture).TextValue;
                return result;
            }
            result.Text = string.Empty;
            XlNumFmtResult result2 = new XlNumFmtResult {
                Text = string.Empty
            };
            double d = Math.Abs(value.NumericValue) * Math.Pow(100.0, (double) base.PercentCount);
            double numericValue = Math.Truncate(d);
            int endIndex = 0;
            if (base.IntegerCount <= 0)
            {
                size = this.CalculateRationalApproximation((decimal) d);
            }
            else
            {
                size = this.CalculateRationalApproximation((decimal) (d - numericValue));
                while (true)
                {
                    IXlNumFmtElement element = base[endIndex];
                    if (element.IsDigit)
                    {
                        if (numericValue != 0.0)
                        {
                            base.FormatIntegerPart(numericValue, this.PreFractionIndex, endIndex, base.IntegerCount, base.Grouping, culture, result2);
                        }
                        else
                        {
                            this.FormatZeroIntegerPart(this.PreFractionIndex, endIndex, culture, result2);
                            if (size.Width != 0)
                            {
                                result2.Text = new string(' ', result2.Text.Length);
                            }
                        }
                        result.Text = result.Text + result2.Text;
                        break;
                    }
                    element.FormatEmpty(culture, result);
                    endIndex++;
                }
            }
            int width = size.Width;
            int height = size.Height;
            result2.Text = string.Empty;
            if (width != 0)
            {
                base.FormatIntegerPart((double) width, this.FractionSeparatorIndex - 1, this.PreFractionIndex + 1, this.dividendCount, false, culture, result2);
                result2.Text = result2.Text + "/";
                this.FormatDivisor(height, this.DivisorIndex, culture, result2);
            }
            else
            {
                this.FormatZeroIntegerPart(this.FractionSeparatorIndex - 1, this.PreFractionIndex + 1, culture, result2);
                result2.Text = result2.Text + "/";
                this.FormatDivisor(height, this.DivisorIndex, culture, result2);
                if (base.IntegerCount > 0)
                {
                    result2.Text = new string(' ', result2.Text.Length);
                }
            }
            result.Text = result.Text + result2.Text;
            for (endIndex = this.DivisorIndex; endIndex < base.Count; endIndex++)
            {
                base[endIndex].FormatEmpty(culture, result);
            }
            if (!base.IsNegativePart && (value.NumericValue < 0.0))
            {
                result.Text = base.ApplyNegativeSign(result.Text, culture);
            }
            return result;
        }

        protected virtual void FormatDivisor(int divisor, int endIndex, CultureInfo culture, XlNumFmtResult result)
        {
            int num = (int) Math.Pow(10.0, Math.Truncate(Math.Log10((double) divisor)) + 1.0);
            int fractionSeparatorIndex = this.FractionSeparatorIndex;
            while ((num > 1) && (fractionSeparatorIndex < endIndex))
            {
                IXlNumFmtElement element = base[fractionSeparatorIndex];
                if (!element.IsDigit)
                {
                    element.FormatEmpty(culture, result);
                }
                else
                {
                    num /= 10;
                    int num2 = divisor % num;
                    element.Format((divisor - num2) / num, culture, result);
                    divisor = num2;
                }
                fractionSeparatorIndex++;
            }
            while (fractionSeparatorIndex < endIndex)
            {
                base[fractionSeparatorIndex].FormatEmpty(culture, result);
                fractionSeparatorIndex++;
            }
        }

        private void FormatZeroIntegerPart(int startIndex, int endIndex, CultureInfo culture, XlNumFmtResult result)
        {
            XlNumFmtResult result2 = new XlNumFmtResult {
                Text = string.Empty
            };
            int num = startIndex;
            while (true)
            {
                if (num >= endIndex)
                {
                    IXlNumFmtElement element = base[num];
                    if (!element.IsDigit)
                    {
                        element.FormatEmpty(culture, result2);
                        result.Text = result2.Text + result.Text;
                        result2.Text = string.Empty;
                        num--;
                        continue;
                    }
                    num--;
                }
                while (num >= endIndex)
                {
                    base[num].FormatEmpty(culture, result2);
                    result.Text = result2.Text + result.Text;
                    result2.Text = string.Empty;
                    num--;
                }
                result.IsError = result.IsError || result2.IsError;
                return;
            }
        }

        public override XlVariantValue Round(XlVariantValue value, CultureInfo culture)
        {
            if (!value.IsNumeric)
            {
                return value;
            }
            double numericValue = value.NumericValue;
            Size size = this.CalculateRationalApproximation((decimal) Math.Abs(numericValue));
            return ((Math.Sign(numericValue) * (((double) size.Width) / ((double) size.Height))) * Math.Pow(10.0, (double) (base.PercentCount * 2)));
        }

        protected int PreFractionIndex =>
            base.DecimalSeparatorIndex;

        protected int FractionSeparatorIndex =>
            base.DecimalCount;

        protected int DivisorIndex =>
            base.DisplayFactor;

        protected int DivisorCount
        {
            get => 
                this.divisorCount;
            set => 
                this.divisorCount = value;
        }

        protected int DividendCount
        {
            get => 
                this.dividendCount;
            set => 
                this.dividendCount = value;
        }
    }
}

