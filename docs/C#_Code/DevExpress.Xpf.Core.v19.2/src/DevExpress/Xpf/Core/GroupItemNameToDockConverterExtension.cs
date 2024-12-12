namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class GroupItemNameToDockConverterExtension : MarkupExtension, IValueConverter
    {
        private static GroupItemNameToDockConverterExtension converter;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            string str = value.ToString();
            return ((str == "Left") ? Dock.Left : ((str == "Right") ? Dock.Right : value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            converter ??= new GroupItemNameToDockConverterExtension();
    }
}

