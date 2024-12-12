namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class DuplexModeToDisplayTextConverter : MarkupExtension, IValueConverter
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
            Duplex? nullable2 = nullable;
            Duplex simplex = Duplex.Simplex;
            if ((((Duplex) nullable2.GetValueOrDefault()) == simplex) ? (nullable2 != null) : false)
            {
                return PrintingLocalizer.GetString(PrintingStringId.DuplexMode_Simplex);
            }
            nullable2 = nullable;
            simplex = Duplex.Horizontal;
            return (!((((Duplex) nullable2.GetValueOrDefault()) == simplex) ? (nullable2 != null) : false) ? PrintingLocalizer.GetString(PrintingStringId.DuplexMode_Vertical) : PrintingLocalizer.GetString(PrintingStringId.DuplexMode_Horizontal));
        }

        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

