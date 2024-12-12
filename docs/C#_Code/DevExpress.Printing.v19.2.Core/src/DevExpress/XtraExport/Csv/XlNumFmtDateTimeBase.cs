namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal abstract class XlNumFmtDateTimeBase : XlNumFmtSimple
    {
        public const int SystemLongDate = 0xf800;
        public const int SystemLongTime = 0xf400;

        protected XlNumFmtDateTimeBase()
        {
        }

        protected XlNumFmtDateTimeBase(IEnumerable<IXlNumFmtElement> elements) : base(elements)
        {
        }

        public override unsafe XlNumFmtResult FormatCore(XlVariantValue value, CultureInfo culture)
        {
            XlNumFmtResult result = new XlNumFmtResult();
            if (!value.IsNumeric)
            {
                result.Text = value.ToText(culture).TextValue;
                return result;
            }
            result.Text = string.Empty;
            if (value.NumericValue < 0.0)
            {
                result.IsError = true;
                return result;
            }
            if (!this.HasMilliseconds)
            {
                XlVariantValue* valuePtr1 = &value;
                valuePtr1.NumericValue += TimeSpan.FromMilliseconds(500.0).TotalDays;
            }
            this.FormatDateTime(value, culture, result);
            return result;
        }

        protected virtual void FormatDateTime(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            foreach (IXlNumFmtElement element in this)
            {
                element.Format(value, culture, result);
            }
        }

        public override XlNumFmtType Type =>
            XlNumFmtType.DateTime;

        protected virtual bool HasMilliseconds =>
            false;
    }
}

