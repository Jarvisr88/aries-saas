namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class DuplexModeToGlyphConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            Duplex? nullable = value as Duplex?;
            if ((nullable == null) || (((Duplex) nullable.Value) == Duplex.Default))
            {
                return null;
            }
            string uriString = string.Empty;
            Duplex? nullable2 = nullable;
            Duplex simplex = Duplex.Simplex;
            if ((((Duplex) nullable2.GetValueOrDefault()) == simplex) ? (nullable2 != null) : false)
            {
                uriString = $"pack://application:,,,/{"DevExpress.Xpf.Printing.v19.2"};component/Images/Printers/DuplexSimplex.svg";
            }
            else
            {
                nullable2 = nullable;
                simplex = Duplex.Horizontal;
                uriString = !((((Duplex) nullable2.GetValueOrDefault()) == simplex) ? (nullable2 != null) : false) ? $"pack://application:,,,/{"DevExpress.Xpf.Printing.v19.2"};component/Images/Printers/DuplexVertical.svg" : $"pack://application:,,,/{"DevExpress.Xpf.Printing.v19.2"};component/Images/Printers/DuplexHorizontal.svg";
            }
            SvgImageSourceExtension extension1 = new SvgImageSourceExtension();
            extension1.Uri = new Uri(uriString);
            return extension1.ProvideValue(null);
        }

        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

