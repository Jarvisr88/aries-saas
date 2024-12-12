namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Globalization;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class MarginSideToPlacementConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MarginSide side = Guard.ArgumentMatchType<MarginSide>(value, "value");
            switch (side)
            {
                case MarginSide.Left:
                    return PlacementMode.Left;

                case MarginSide.Top:
                    return PlacementMode.Top;

                case (MarginSide.Top | MarginSide.Left):
                    break;

                case MarginSide.Right:
                    return PlacementMode.Right;

                default:
                    if (side != MarginSide.Bottom)
                    {
                        break;
                    }
                    return PlacementMode.Bottom;
            }
            throw new InvalidOperationException();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

