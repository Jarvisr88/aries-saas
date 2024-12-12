namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class DuplexModeToDisplayTextAndDescriptionConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            Duplex? nullable2;
            Duplex simplex;
            Duplex? nullable = value as Duplex?;
            if ((nullable == null) || (((Duplex) nullable.Value) == Duplex.Default))
            {
                return null;
            }
            if (this.DisplayTextType == Type.DisplayText)
            {
                nullable2 = nullable;
                simplex = Duplex.Simplex;
                return (((((Duplex) nullable2.GetValueOrDefault()) == simplex) ? (nullable2 != null) : false) ? PrintingLocalizer.GetString(PrintingStringId.PrintOneSided) : PrintingLocalizer.GetString(PrintingStringId.PrintOnBothSides));
            }
            nullable2 = nullable;
            simplex = Duplex.Simplex;
            if ((((Duplex) nullable2.GetValueOrDefault()) == simplex) ? (nullable2 != null) : false)
            {
                return PrintingLocalizer.GetString(PrintingStringId.PrintOneSidedDescription);
            }
            nullable2 = nullable;
            simplex = Duplex.Horizontal;
            return (!((((Duplex) nullable2.GetValueOrDefault()) == simplex) ? (nullable2 != null) : false) ? PrintingLocalizer.GetString(PrintingStringId.DuplexMode_Vertical) : PrintingLocalizer.GetString(PrintingStringId.DuplexMode_Horizontal));
        }

        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public Type DisplayTextType { get; set; }

        public enum Type
        {
            DisplayText,
            Description
        }
    }
}

