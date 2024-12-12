namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtExprCondition : XlNumFmtCondition
    {
        private string expression;

        public XlNumFmtExprCondition(string expression)
        {
            this.expression = expression;
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            result.Text = result.Text + "[" + this.expression + "]";
        }
    }
}

