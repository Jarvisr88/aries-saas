namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal class XlNumFmtComposite : List<XlNumFmtSimple>, IXlNumFmt
    {
        private XlNumFmtSimple CalculateActualPart(XlVariantValue value)
        {
            int count = base.Count;
            if (count == 0)
            {
                return null;
            }
            if (value.IsNumeric)
            {
                if (base[count - 1].Type == XlNumFmtType.Text)
                {
                    count--;
                }
                return ((count > 1) ? (((count <= 2) || (value.NumericValue != 0.0)) ? ((value.NumericValue >= 0.0) ? base[0] : base[1]) : base[2]) : base[0]);
            }
            if (count == 4)
            {
                return base[3];
            }
            XlNumFmtSimple simple = base[count - 1];
            return ((simple.Type != XlNumFmtType.Text) ? null : simple);
        }

        public bool EnclosedInParenthesesForPositive() => 
            (base.Count >= 1) ? base[0].EnclosedInParenthesesForPositive() : false;

        public XlNumFmtResult Format(XlVariantValue value, CultureInfo culture)
        {
            XlNumFmtSimple simple = this.CalculateActualPart(value);
            if (simple != null)
            {
                return simple.Format(value, culture);
            }
            return new XlNumFmtResult { Text = value.ToText(culture).TextValue };
        }

        public XlVariantValue Round(XlVariantValue value, CultureInfo culture)
        {
            XlNumFmtSimple simple = this.CalculateActualPart(value);
            return ((simple == null) ? value : simple.Round(value, culture));
        }

        public XlNumFmtType Type =>
            base[0].Type;
    }
}

