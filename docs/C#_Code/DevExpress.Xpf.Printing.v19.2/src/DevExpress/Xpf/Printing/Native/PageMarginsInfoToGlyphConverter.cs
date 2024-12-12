namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    internal class PageMarginsInfoToGlyphConverter : MarkupExtension, IValueConverter
    {
        private readonly Margins normal = new Margins(100, 100, 100, 100);
        private readonly Margins narrow = new Margins(50, 50, 50, 50);
        private readonly Margins moderate = new Margins(0x4b, 100, 0x4b, 100);
        private readonly Margins wide = new Margins(200, 200, 200, 200);

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            PageMarginInfo info = value as PageMarginInfo;
            if (info == null)
            {
                return null;
            }
            string format = "pack://application:,,,/{0};component/Images/PageMargins{1}.svg";
            string str2 = string.Empty;
            if (info.Margins.Equals(this.normal))
            {
                str2 = "normal";
            }
            else if (info.Margins.Equals(this.narrow))
            {
                str2 = "narrow";
            }
            else if (info.Margins.Equals(this.moderate))
            {
                str2 = "moderate";
            }
            else
            {
                if (!info.Margins.Equals(this.wide))
                {
                    return null;
                }
                str2 = "wide";
            }
            SvgImageSourceExtension extension1 = new SvgImageSourceExtension();
            extension1.Uri = new Uri(string.Format(format, "DevExpress.Xpf.Printing.v19.2", str2));
            return extension1.ProvideValue(null);
        }

        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

