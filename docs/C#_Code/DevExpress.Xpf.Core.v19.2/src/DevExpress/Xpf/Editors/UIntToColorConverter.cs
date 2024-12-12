namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class UIntToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return new Color();
            }
            uint num = System.Convert.ToUInt32(value);
            return Color.FromArgb((byte) (num >> 0x18), (byte) ((num >> 0x10) & 0xff), (byte) ((num >> 8) & 0xff), (byte) (num & 0xff));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 0;
            }
            Color color = (Color) value;
            return (uint) ((((color.A << 0x18) + (color.R << 0x10)) + (color.G << 8)) + color.B);
        }
    }
}

