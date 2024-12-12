namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class EnumNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
            {
                return null;
            }
            string enumItemDisplayText = EnumExtensions.GetEnumItemDisplayText(value);
            if (this.UseLocalizer)
            {
                enumItemDisplayText = ConditionalFormattingLocalizer.GetString((ConditionalFormattingStringId) Enum.Parse(typeof(ConditionalFormattingStringId), enumItemDisplayText, false));
            }
            return enumItemDisplayText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            null;

        public bool UseLocalizer { get; set; }
    }
}

