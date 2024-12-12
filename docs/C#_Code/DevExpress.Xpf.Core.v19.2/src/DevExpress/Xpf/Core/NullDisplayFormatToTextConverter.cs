namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Localization;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class NullDisplayFormatToTextConverter : IValueConverter
    {
        private XtraLocalizer<EditorStringId> localizer = EditorLocalizer.Active;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            !string.IsNullOrEmpty((string) value) ? value : this.localizer.GetLocalizedString(EditorStringId.DisplayFormatNullValue);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

