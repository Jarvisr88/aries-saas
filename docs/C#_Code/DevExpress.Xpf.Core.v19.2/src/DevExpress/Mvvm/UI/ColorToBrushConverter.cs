namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Data;
    using System.Windows.Media;

    public class ColorToBrushConverter : IValueConverter
    {
        public static SolidColorBrush Convert(object value, byte? customA = new byte?())
        {
            System.Windows.Media.Color color;
            if (value == null)
            {
                return null;
            }
            if (value is System.Drawing.Color)
            {
                System.Drawing.Color color2 = (System.Drawing.Color) value;
                color = System.Windows.Media.Color.FromArgb(color2.A, color2.R, color2.G, color2.B);
            }
            else
            {
                color = (System.Windows.Media.Color) value;
            }
            if (customA != null)
            {
                color.A = customA.Value;
            }
            return BrushesCache.GetBrush(color);
        }

        public static System.Windows.Media.Color ConvertBack(object value)
        {
            if (value != null)
            {
                return ((SolidColorBrush) value).Color;
            }
            return new System.Windows.Media.Color();
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            Convert(value, this.CustomA);

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            ConvertBack(value);

        public byte? CustomA { get; set; }
    }
}

