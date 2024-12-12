namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal abstract class XlValueFormatterBase : IXlValueFormatter
    {
        protected XlValueFormatterBase()
        {
        }

        public virtual string Format(XlVariantValue value, CultureInfo culture) => 
            !value.IsNumeric ? (!value.IsText ? (!value.IsBoolean ? (!value.IsError ? string.Empty : value.ErrorValue.Name) : (value.BooleanValue ? "TRUE" : "FALSE")) : value.TextValue) : this.FormatNumeric(value, culture);

        protected abstract string FormatNumeric(XlVariantValue value, CultureInfo culture);
        protected abstract string GetFormatString(CultureInfo culture);
    }
}

