namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class SearchInfoConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => 
            ((values == null) || (values.Length != 2)) ? null : string.Format(EditorLocalizer.Active.GetLocalizedString(EditorStringId.LookUpResultInfo), values[0], values[1]);

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

