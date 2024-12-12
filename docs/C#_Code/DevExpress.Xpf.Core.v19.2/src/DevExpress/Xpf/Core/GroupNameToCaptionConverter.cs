namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Localization;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class GroupNameToCaptionConverter : IValueConverter
    {
        private XtraLocalizer<EditorStringId> localizer = EditorLocalizer.Active;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DisplayFormatGroupType)
            {
                switch (((DisplayFormatGroupType) value))
                {
                    case DisplayFormatGroupType.Default:
                        return this.localizer.GetLocalizedString(EditorStringId.DisplayFormatGroupTypeDefault);

                    case DisplayFormatGroupType.Number:
                        return this.localizer.GetLocalizedString(EditorStringId.DisplayFormatGroupTypeNumber);

                    case DisplayFormatGroupType.Percent:
                        return this.localizer.GetLocalizedString(EditorStringId.DisplayFormatGroupTypePercent);

                    case DisplayFormatGroupType.Currency:
                        return this.localizer.GetLocalizedString(EditorStringId.DisplayFormatGroupTypeCurrency);

                    case DisplayFormatGroupType.Special:
                        return this.localizer.GetLocalizedString(EditorStringId.DisplayFormatGroupTypeSpecial);

                    case DisplayFormatGroupType.Datetime:
                        return this.localizer.GetLocalizedString(EditorStringId.DisplayFormatGroupTypeDatetime);

                    case DisplayFormatGroupType.Custom:
                        return this.localizer.GetLocalizedString(EditorStringId.DisplayFormatGroupTypeCustom);
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

