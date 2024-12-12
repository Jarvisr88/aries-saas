namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class VerticalScrollBarRowSpanConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => 
            (!(values[0] is ScrollBarMode) || !(values[1] is Orientation?)) ? 1 : (((((ScrollBarMode) values[0]) != ScrollBarMode.TouchOverlap) || (((Orientation?) values[1]) == null)) ? 1 : ((((Orientation) ((Orientation?) values[1]).Value) == Orientation.Vertical) ? 2 : 1));

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

