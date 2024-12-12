namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Windows.Media;

    public static class Text2ColorHelper
    {
        public static readonly Color DefaultColor = Colors.Black;

        public static object Convert(object value, bool includeAlpha = true)
        {
            if (!(value is Color))
            {
                return DefaultColor;
            }
            Color color = (Color) value;
            return (includeAlpha ? color.ToString(CultureInfo.InvariantCulture).ToUpperInvariant() : color.ToString(CultureInfo.InvariantCulture).ToUpperInvariant().Remove(1, 2));
        }

        public static Color ConvertBack(object value)
        {
            Color color;
            TryConvert(value, out color);
            return color;
        }

        private static bool IsNumber(string value)
        {
            uint num;
            return (uint.TryParse(value, NumberStyles.AllowHexSpecifier, CultureInfo.CurrentCulture, out num) && (value.Length <= 8));
        }

        public static bool TryConvert(object value, out Color result)
        {
            bool flag;
            result = DefaultColor;
            string str = value as string;
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            str = str.Replace("#", "");
            if (!IsNumber(str))
            {
                try
                {
                    result = (Color) ColorConverter.ConvertFromString(str);
                    return true;
                }
                catch
                {
                }
            }
            if (str.Length > 8)
            {
                return false;
            }
            if (str.Length == 3)
            {
                str = string.Format("FF{0}{0}{1}{1}{2}{2}", str[0], str[1], str[2]);
            }
            else if (str.Length != 4)
            {
                if (str.Length == 6)
                {
                    str = "FF" + str;
                }
            }
            else
            {
                str = string.Format("{0}{0}{1}{1}{2}{2}{3}{3}", new object[] { str[0], str[1], str[2], str[3] });
            }
            if (str.Length != 8)
            {
                return false;
            }
            try
            {
                byte a = System.Convert.ToByte(str.Substring(0, 2), 0x10);
                result = Color.FromArgb(a, System.Convert.ToByte(str.Substring(2, 2), 0x10), System.Convert.ToByte(str.Substring(4, 2), 0x10), System.Convert.ToByte(str.Substring(6, 2), 0x10));
                flag = true;
            }
            catch
            {
                return false;
            }
            return flag;
        }
    }
}

