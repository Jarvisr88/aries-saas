namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal class XlNumFmtNumericExponent : XlNumFmtNumericSimple
    {
        private bool explicitSign;
        private int expIndex;
        private int expCount;

        protected XlNumFmtNumericExponent()
        {
        }

        public XlNumFmtNumericExponent(IEnumerable<IXlNumFmtElement> elements, int integerCount, int decimalCount, int decimalSeparatorIndex, int expIndex, int expCount, bool explicitSign, bool grouping, bool isNegativePart) : base(elements, 0, integerCount, decimalCount, 0, decimalSeparatorIndex, grouping, isNegativePart)
        {
            this.explicitSign = explicitSign;
            this.expIndex = expIndex;
            this.expCount = expCount;
        }

        private Tuple<double, double> CalculateExponent(double numericValue)
        {
            double d = Math.Abs(numericValue);
            double num2 = Math.Floor((double) (Math.Floor(Math.Log10(d)) / ((double) base.IntegerCount))) * base.IntegerCount;
            return new Tuple<double, double>(Math.Round((double) (d / Math.Pow(10.0, num2)), base.DecimalCount, MidpointRounding.AwayFromZero), num2);
        }

        public override XlNumFmtResult FormatCore(XlVariantValue value, CultureInfo culture)
        {
            double num;
            XlNumFmtResult result = new XlNumFmtResult();
            if (!value.IsNumeric)
            {
                result.Text = value.ToText(culture).TextValue;
                return result;
            }
            result.Text = string.Empty;
            if (value.NumericValue != 0.0)
            {
                Tuple<double, double> tuple = this.CalculateExponent(value.NumericValue);
                num = tuple.Item2;
                base.FormatSimple(tuple.Item1, culture, result, this.expIndex - 1);
            }
            else
            {
                num = 0.0;
                for (int i = 0; i < this.expIndex; i++)
                {
                    base[i].Format(value, culture, result);
                }
            }
            result.Text = result.Text + "E";
            if (num < 0.0)
            {
                result.Text = result.Text + "-";
            }
            else if (this.explicitSign)
            {
                result.Text = result.Text + "+";
            }
            XlNumFmtResult result2 = new XlNumFmtResult {
                Text = string.Empty
            };
            base.FormatIntegerPart(Math.Abs(num), base.Count - 1, this.expIndex, this.expCount, false, culture, result2);
            result.Text = result.Text + result2.Text;
            if (!base.IsNegativePart && (value.NumericValue < 0.0))
            {
                result.Text = base.ApplyNegativeSign(result.Text, culture);
            }
            return result;
        }

        public override XlVariantValue Round(XlVariantValue value, CultureInfo culture)
        {
            if (!value.IsNumeric || (value.NumericValue == 0.0))
            {
                return value;
            }
            Tuple<double, double> tuple = this.CalculateExponent(value.NumericValue);
            return (Math.Round(tuple.Item1, base.DecimalCount, MidpointRounding.AwayFromZero) * Math.Pow(10.0, tuple.Item2));
        }
    }
}

