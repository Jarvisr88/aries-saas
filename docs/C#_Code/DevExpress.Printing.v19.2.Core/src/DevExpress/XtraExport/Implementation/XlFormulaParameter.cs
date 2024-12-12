namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    internal class XlFormulaParameter : IXlFormulaParameter
    {
        public XlFormulaParameter(XlVariantValue value)
        {
            this.Value = value;
        }

        public string ToString(CultureInfo culture)
        {
            string textValue = this.Value.ToText().TextValue;
            if (string.IsNullOrEmpty(textValue))
            {
                textValue = string.Empty;
            }
            if (this.Value.IsText)
            {
                textValue = "\"" + textValue + "\"";
            }
            return textValue;
        }

        public XlVariantValue Value { get; set; }
    }
}

