namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class TextAlignmentToHorizontalAlignmentConverter : MarkupExtension, IValueConverter
    {
        private static Dictionary<TextAlignment, HorizontalAlignment> dictionary = new Dictionary<TextAlignment, HorizontalAlignment>();

        static TextAlignmentToHorizontalAlignmentConverter()
        {
            dictionary.Add(TextAlignment.Left, HorizontalAlignment.Left);
            dictionary.Add(TextAlignment.Right, HorizontalAlignment.Right);
            dictionary.Add(TextAlignment.Center, HorizontalAlignment.Center);
            dictionary.Add(TextAlignment.Justify, HorizontalAlignment.Stretch);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (!Enum.IsDefined(typeof(TextAlignment), value))
            {
                throw new ArgumentException("value");
            }
            return dictionary[(TextAlignment) value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

