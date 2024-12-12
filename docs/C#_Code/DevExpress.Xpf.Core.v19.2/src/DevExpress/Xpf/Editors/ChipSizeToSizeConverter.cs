namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ChipSizeToSizeConverter : IValueConverter
    {
        private const double SmallSize = 13.0;
        private const double MediumSize = 16.0;
        private const double LargeSize = 20.0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ChipSize size = (ChipSize) value;
            return ((size == ChipSize.Medium) ? 16.0 : ((size == ChipSize.Large) ? 20.0 : 13.0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

