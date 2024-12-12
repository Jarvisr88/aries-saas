namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal class XlNumFmtGeneral : XlNumFmtSimple
    {
        public XlNumFmtGeneral()
        {
        }

        public XlNumFmtGeneral(IEnumerable<IXlNumFmtElement> elements, int generalIndex)
        {
            base.AddRange(elements);
        }

        public override XlNumFmtResult Format(XlVariantValue value, CultureInfo culture) => 
            new XlNumFmtResult { Text = value.ToText(culture).TextValue };

        public override XlNumFmtResult FormatCore(XlVariantValue value, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }

        public string FormatNumeric(XlVariantValue value, CultureInfo culture) => 
            value.ToText(culture).TextValue;

        public override XlNumFmtType Type =>
            XlNumFmtType.General;
    }
}

