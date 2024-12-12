namespace DevExpress.Xpf.Editors.DataPager
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DataPagerPageCountToTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            string.Format(EditorLocalizer.Active.GetLocalizedString(EditorStringId.Of), value.ToString());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

