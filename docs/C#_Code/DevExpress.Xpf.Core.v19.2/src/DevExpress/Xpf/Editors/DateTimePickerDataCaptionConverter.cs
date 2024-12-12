namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DateTimePickerDataCaptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTimePickerData)
            {
                DateTimePickerData data = (DateTimePickerData) value;
                switch (data.DateTimePart)
                {
                    case DateTimePart.Day:
                        return $"{data.Value:dddd}";

                    case DateTimePart.Month:
                        return $"{data.Value:MMMM}";

                    case DateTimePart.Hour12:
                    case DateTimePart.Hour24:
                        return EditorLocalizer.GetString(EditorStringId.DatePickerHours);

                    case DateTimePart.Minute:
                        return EditorLocalizer.GetString(EditorStringId.DatePickerMinutes);

                    case DateTimePart.Second:
                        return EditorLocalizer.GetString(EditorStringId.DatePickerSeconds);

                    case DateTimePart.Millisecond:
                        return EditorLocalizer.GetString(EditorStringId.DatePickerMilliseconds);
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

