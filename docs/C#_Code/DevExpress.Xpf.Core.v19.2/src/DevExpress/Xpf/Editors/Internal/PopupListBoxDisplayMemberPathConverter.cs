namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class PopupListBoxDisplayMemberPathConverter : IValueConverter
    {
        private string GetDisplayMemberPathCore(string value) => 
            (value == "") ? null : value;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                return this.GetDisplayMemberPathCore((string) value);
            }
            ISelectorEdit edit = value as ISelectorEdit;
            return (((edit == null) || (edit.ItemTemplate != null)) ? this.GetDisplayMemberPathCore(string.Empty) : this.GetDisplayMemberPathCore(edit.DisplayMember));
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

