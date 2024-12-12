namespace DevExpress.XtraExport.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;

    internal class XlNumFmtNumericFractionExplicit : XlNumFmtNumericFraction
    {
        protected XlNumFmtNumericFractionExplicit()
        {
        }

        public XlNumFmtNumericFractionExplicit(IEnumerable<IXlNumFmtElement> elements, int percentCount, int integerCount, int preFractionIndex, int fractionSeparatorIndex, int divisorIndex, int dividentCount, int divisor, bool grouping, bool isNegativePart) : base(elements, percentCount, integerCount, preFractionIndex, fractionSeparatorIndex, divisorIndex, dividentCount, divisor, grouping, isNegativePart)
        {
        }

        protected override Size CalculateRationalApproximation(decimal value)
        {
            int num = (int) value;
            value -= num;
            return new Size(((int) Math.Round((decimal) (value * this.ExplicitDivisor), 0, MidpointRounding.AwayFromZero)) + (num * this.ExplicitDivisor), this.ExplicitDivisor);
        }

        protected override void FormatDivisor(int divisor, int endIndex, CultureInfo culture, XlNumFmtResult result)
        {
            for (int i = base.FractionSeparatorIndex; i < endIndex; i++)
            {
                base[i].FormatEmpty(culture, result);
            }
            result.Text = result.Text + this.ExplicitDivisor;
        }

        private int ExplicitDivisor =>
            base.DivisorCount;
    }
}

