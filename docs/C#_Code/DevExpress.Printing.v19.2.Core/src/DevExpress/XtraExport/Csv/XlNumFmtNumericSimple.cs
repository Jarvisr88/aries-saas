namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal class XlNumFmtNumericSimple : XlNumFmtSimple
    {
        private bool isNegativePart;
        private bool grouping;
        private int percentCount;
        private int integerCount;
        private int decimalCount;
        private int displayFactor;
        private int decimalSeparatorIndex;

        protected XlNumFmtNumericSimple()
        {
        }

        public XlNumFmtNumericSimple(int percentCount, int integerCount, int decimalCount, int displayFactor, int decimalSeparatorIndex, bool grouping, bool isNegativePart)
        {
            this.percentCount = percentCount;
            this.integerCount = integerCount;
            this.decimalCount = decimalCount;
            this.displayFactor = displayFactor;
            this.decimalSeparatorIndex = decimalSeparatorIndex;
            this.grouping = grouping;
            this.isNegativePart = isNegativePart;
        }

        public XlNumFmtNumericSimple(IEnumerable<IXlNumFmtElement> elements, int percentCount, int integerCount, int decimalCount, int displayFactor, int decimalSeparatorIndex, bool grouping, bool isNegativePart) : base(elements)
        {
            this.percentCount = percentCount;
            this.integerCount = integerCount;
            this.decimalCount = decimalCount;
            this.displayFactor = displayFactor;
            this.decimalSeparatorIndex = decimalSeparatorIndex;
            this.grouping = grouping;
            this.isNegativePart = isNegativePart;
        }

        protected string ApplyNegativeSign(string text, CultureInfo culture) => 
            "-" + text;

        public override XlNumFmtResult FormatCore(XlVariantValue value, CultureInfo culture)
        {
            XlNumFmtResult result = new XlNumFmtResult();
            if (!value.IsNumeric)
            {
                result.Text = value.ToText(culture).TextValue;
                return result;
            }
            result.Text = string.Empty;
            this.FormatSimple(value, culture, result, base.Count - 1);
            return result;
        }

        protected void FormatDecimalPart(double numericValue, int startIndex, int endIndex, int digitsCount, CultureInfo culture, XlNumFmtResult result)
        {
            decimal num2 = (decimal) Math.Pow(10.0, (double) digitsCount);
            decimal d = (decimal) numericValue;
            d = Math.Round((decimal) ((d - Math.Truncate(d)) * num2), 0, MidpointRounding.AwayFromZero);
            int num4 = startIndex;
            while ((d > 0M) && (num4 < base.Count))
            {
                IXlNumFmtElement element = base[num4];
                if (!element.IsDigit)
                {
                    element.FormatEmpty(culture, result);
                }
                else
                {
                    num2 /= 10M;
                    int num = (int) (d / num2);
                    element.Format(num, culture, result);
                    d -= num * num2;
                }
                num4++;
            }
            while (num4 <= endIndex)
            {
                base[num4].FormatEmpty(culture, result);
                num4++;
            }
        }

        protected void FormatIntegerPart(double numericValue, int startIndex, int endIndex, int digitsCount, bool grouping, CultureInfo culture, XlNumFmtResult result)
        {
            XlNumFmtResult result2 = new XlNumFmtResult();
            int num = startIndex;
            if (numericValue > 0.0)
            {
                int num2 = digitsCount;
                int num3 = 0;
                while (true)
                {
                    if ((numericValue <= 0.0) || (num2 <= 0))
                    {
                        if ((digitsCount > 0) || (this.decimalSeparatorIndex >= 0))
                        {
                            while (numericValue > 0.0)
                            {
                                XlNumFmtDigitZero.Instance.Format(numericValue % 10.0, culture, result2);
                                numericValue = Math.Truncate((double) (numericValue / 10.0));
                                if (grouping && (((num3 % 3) == 0) && (num3 != 0)))
                                {
                                    XlNumFmtGroupSeparator.Instance.FormatEmpty(culture, result2);
                                }
                                num3++;
                                result.Text = result2.Text + result.Text;
                                result2.Text = string.Empty;
                            }
                        }
                        break;
                    }
                    IXlNumFmtElement element = base[num];
                    if (!element.IsDigit)
                    {
                        element.FormatEmpty(culture, result2);
                    }
                    else
                    {
                        element.Format(numericValue % 10.0, culture, result2);
                        numericValue = Math.Truncate((double) (numericValue / 10.0));
                        num2--;
                        if (grouping && (((num3 % 3) == 0) && (num3 != 0)))
                        {
                            XlNumFmtGroupSeparator.Instance.FormatEmpty(culture, result2);
                        }
                        num3++;
                    }
                    result.Text = result2.Text + result.Text;
                    result2.Text = string.Empty;
                    num--;
                }
            }
            while (num >= endIndex)
            {
                base[num].FormatEmpty(culture, result2);
                result.Text = result2.Text + result.Text;
                result2.Text = string.Empty;
                num--;
            }
            result.IsError = result.IsError || result2.IsError;
        }

        protected void FormatSimple(XlVariantValue value, CultureInfo culture, XlNumFmtResult result, int endIndex)
        {
            if (value.NumericValue == 0.0)
            {
                for (int i = 0; i <= endIndex; i++)
                {
                    base[i].FormatEmpty(culture, result);
                }
            }
            else
            {
                double numericValue = Math.Round((double) ((double.Parse(Math.Abs(value.NumericValue).ToString()) * Math.Pow(100.0, (double) this.percentCount)) * Math.Pow(0.001, (double) this.displayFactor)), this.decimalCount, MidpointRounding.AwayFromZero);
                if (this.decimalSeparatorIndex < 0)
                {
                    this.FormatIntegerPart(numericValue, endIndex, 0, this.integerCount, this.grouping, culture, result);
                }
                else
                {
                    this.FormatIntegerPart(Math.Truncate(numericValue), this.decimalSeparatorIndex - 1, 0, this.integerCount, this.grouping, culture, result);
                    XlNumFmtDecimalSeparator.Instance.FormatEmpty(culture, result);
                    this.FormatDecimalPart(numericValue, this.decimalSeparatorIndex + 1, endIndex, this.decimalCount, culture, result);
                }
            }
            if (!this.isNegativePart && (value.NumericValue < 0.0))
            {
                result.Text = this.ApplyNegativeSign(result.Text, culture);
            }
        }

        public override XlVariantValue Round(XlVariantValue value, CultureInfo culture) => 
            value.IsNumeric ? Math.Round(value.NumericValue, (this.decimalCount + (this.percentCount * 2)) + (this.displayFactor * 3), MidpointRounding.AwayFromZero) : value;

        public override XlNumFmtType Type =>
            XlNumFmtType.Numeric;

        protected bool IsNegativePart
        {
            get => 
                this.isNegativePart;
            set => 
                this.isNegativePart = value;
        }

        protected bool Grouping
        {
            get => 
                this.grouping;
            set => 
                this.grouping = value;
        }

        protected int DecimalSeparatorIndex
        {
            get => 
                this.decimalSeparatorIndex;
            set => 
                this.decimalSeparatorIndex = value;
        }

        protected int PercentCount
        {
            get => 
                this.percentCount;
            set => 
                this.percentCount = value;
        }

        protected int IntegerCount
        {
            get => 
                this.integerCount;
            set => 
                this.integerCount = value;
        }

        protected int DecimalCount
        {
            get => 
                this.decimalCount;
            set => 
                this.decimalCount = value;
        }

        protected int DisplayFactor
        {
            get => 
                this.displayFactor;
            set => 
                this.displayFactor = value;
        }
    }
}

