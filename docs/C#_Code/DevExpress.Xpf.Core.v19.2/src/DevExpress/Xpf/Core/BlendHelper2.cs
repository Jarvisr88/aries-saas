namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;

    public class BlendHelper2 : DependencyObject
    {
        public static readonly DependencyProperty InvalidateVisualTreeProperty = DependencyProperty.RegisterAttached("InvalidateVisualTree", typeof(object), typeof(BlendHelper2), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty ThemeInfoProperty = DependencyProperty.RegisterAttached("ThemeInfo", typeof(string), typeof(BlendHelper2), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.Inherits));
        public static readonly DependencyProperty StyleInfoProperty = DependencyProperty.RegisterAttached("StyleInfo", typeof(string), typeof(BlendHelper2), new FrameworkPropertyMetadata(string.Empty));
        private static readonly object PaletteLocker = new object();
        private static volatile uint[] palette = new uint[0];
        private const byte ColorIndexId = 0x1d;

        public static event EventHandler PaletteChanged;

        public static Color CalcPalette(Color color)
        {
            uint[] palette;
            if (BlendHelper2.palette == null)
            {
                return color;
            }
            object paletteLocker = PaletteLocker;
            lock (paletteLocker)
            {
                palette = BlendHelper2.palette;
            }
            if (palette == null)
            {
                return color;
            }
            byte a = color.A;
            if (a != 0x1d)
            {
                return color;
            }
            byte g = color.G;
            byte b = color.B;
            byte[] data = new byte[4];
            data[0] = a;
            data[2] = g;
            data[3] = b;
            if (Crc8.Calc(data) != color.R)
            {
                return color;
            }
            int index = (g << 8) | b;
            return (((index < 0) || (index > palette.Length)) ? color : UIntToColor(palette[index]));
        }

        public static object GetInvalidateVisualTree(DependencyObject d) => 
            d.GetValue(InvalidateVisualTreeProperty);

        public static string GetStyleInfo(DependencyObject obj) => 
            (string) obj.GetValue(StyleInfoProperty);

        public static string GetThemeInfo(DependencyObject obj) => 
            (string) obj.GetValue(ThemeInfoProperty);

        private static void RaisePaletteChanged()
        {
            EventHandler paletteChanged = PaletteChanged;
            if (paletteChanged != null)
            {
                paletteChanged(null, EventArgs.Empty);
            }
        }

        public static void SetInvalidateVisualTree(DependencyObject d, object value)
        {
            d.SetValue(InvalidateVisualTreeProperty, value);
        }

        [CLSCompliant(false)]
        public static void SetPalette(uint[] colors)
        {
            object paletteLocker = PaletteLocker;
            lock (paletteLocker)
            {
                palette = colors;
            }
            RaisePaletteChanged();
        }

        public static void SetStyleInfo(DependencyObject obj, string value)
        {
            obj.SetValue(StyleInfoProperty, value);
        }

        public static void SetThemeInfo(DependencyObject obj, string value)
        {
            obj.SetValue(ThemeInfoProperty, value);
        }

        private static Color UIntToColor(uint color) => 
            Color.FromArgb((byte) (color >> 0x18), (byte) (color >> 0x10), (byte) (color >> 8), (byte) color);
    }
}

