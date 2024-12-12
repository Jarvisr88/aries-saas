namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Globalization;

    internal class XlCustomFormatter : IXlValueFormatter
    {
        private readonly IXlNumFmt provider;

        public XlCustomFormatter(IXlNumFmt provider)
        {
            Guard.ArgumentNotNull(provider, "provider");
            this.provider = provider;
        }

        public string Format(XlVariantValue value, CultureInfo culture) => 
            this.provider.Format(value, culture).Text;
    }
}

