namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class GroupItemNameToHorizontalAlignmentConverterExtension : MarkupExtension, IValueConverter
    {
        private static GroupItemNameToHorizontalAlignmentConverterExtension converter;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            string str = value.ToString();
            return ((str == "Left") ? HorizontalAlignment.Left : ((str == "Right") ? HorizontalAlignment.Right : ((str == "Center") ? HorizontalAlignment.Center : value)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            converter ??= new GroupItemNameToHorizontalAlignmentConverterExtension();
    }
}

