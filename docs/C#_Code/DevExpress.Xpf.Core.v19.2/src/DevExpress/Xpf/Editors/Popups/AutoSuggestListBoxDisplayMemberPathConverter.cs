namespace DevExpress.Xpf.Editors.Popups
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class AutoSuggestListBoxDisplayMemberPathConverter : IValueConverter
    {
        private string GetDisplayMemberPathCore(string value) => 
            (value == "") ? null : value;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                return this.GetDisplayMemberPathCore((string) value);
            }
            AutoSuggestEdit edit = value as AutoSuggestEdit;
            return (((edit == null) || (edit.ItemTemplate != null)) ? this.GetDisplayMemberPathCore(string.Empty) : this.GetDisplayMemberPathCore(edit.DisplayMember));
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

