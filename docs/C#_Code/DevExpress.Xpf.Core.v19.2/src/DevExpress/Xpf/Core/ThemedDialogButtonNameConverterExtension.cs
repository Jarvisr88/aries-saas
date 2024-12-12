namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class ThemedDialogButtonNameConverterExtension : MarkupExtension, IValueConverter
    {
        private static ThemedDialogButtonNameConverterExtension converter;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }
            string str = value.ToString();
            return ("PART_" + str + "DialogButton");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            converter ??= new ThemedDialogButtonNameConverterExtension();
    }
}

