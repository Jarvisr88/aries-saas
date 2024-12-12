namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class HorizontalContentAlignmentToTextAlignmentConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (((HorizontalAlignment) value))
            {
                case HorizontalAlignment.Left:
                    return TextAlignment.Left;

                case HorizontalAlignment.Center:
                    return TextAlignment.Center;

                case HorizontalAlignment.Right:
                    return TextAlignment.Right;
            }
            return TextAlignment.Justify;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

