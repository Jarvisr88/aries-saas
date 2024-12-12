namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ScrollContentPresenterMarginCorrector : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            value;

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

