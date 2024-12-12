namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Core;
    using System;
    using System.ComponentModel;

    public class PrintingImageSelectorExtension : ImageSelectorExtension
    {
        private static readonly TypeConverter UriTypeConverter = TypeDescriptor.GetConverter(typeof(Uri));

        private static object GetPrintingSource(object source)
        {
            if ((source != null) && UriTypeConverter.CanConvertFrom(source.GetType()))
            {
                Uri uri = (Uri) UriTypeConverter.ConvertFrom(source);
                if (!uri.IsAbsoluteUri)
                {
                    source = new Uri($"pack://application:,,,/{"DevExpress.Xpf.Printing.v19.2"};component/{uri.OriginalString}");
                }
            }
            return source;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            base.Source = GetPrintingSource(base.Source);
            base.SvgSource = GetPrintingSource(base.SvgSource);
            return base.ProvideValue(serviceProvider);
        }
    }
}

