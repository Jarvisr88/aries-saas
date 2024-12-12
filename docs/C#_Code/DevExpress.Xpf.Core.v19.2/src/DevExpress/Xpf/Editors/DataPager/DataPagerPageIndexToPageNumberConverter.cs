namespace DevExpress.Xpf.Editors.DataPager
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DataPagerPageIndexToPageNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataPagerCurrentPageParams @params = (DataPagerCurrentPageParams) value;
            return Math.Min(@params.PageIndex + 1, @params.PageCount);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            new DataPagerCurrentPageParams { 
                PageCount = -1,
                PageIndex = (int) (((decimal) value) - 1M)
            };
    }
}

