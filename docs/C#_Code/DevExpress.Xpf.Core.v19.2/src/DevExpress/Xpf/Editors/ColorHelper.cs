namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Media;

    public static class ColorHelper
    {
        private static Color ChangeBrightness(Color color, int modification)
        {
            int g = color.G;
            int b = color.B;
            int num4 = (150 * modification) / 100;
            int num = color.R + num4;
            if (num < 0)
            {
                num = 0;
            }
            if (num > 0xff)
            {
                num = 0xff;
            }
            g += num4;
            if (g < 0)
            {
                g = 0;
            }
            if (g > 0xff)
            {
                g = 0xff;
            }
            b += num4;
            if (b < 0)
            {
                b = 0;
            }
            if (b > 0xff)
            {
                b = 0xff;
            }
            return Color.FromArgb(0xff, (byte) num, (byte) g, (byte) b);
        }

        public static Color ColorFromHex(string hex)
        {
            if (hex.Length != 9)
            {
                return new Color();
            }
            string s = hex.Substring(1, 2);
            string str2 = hex.Substring(3, 2);
            string str3 = hex.Substring(5, 2);
            string str4 = hex.Substring(7, 2);
            Color color = new Color();
            try
            {
                int num = int.Parse(s, NumberStyles.HexNumber);
                int num2 = int.Parse(str2, NumberStyles.HexNumber);
                int num3 = int.Parse(str3, NumberStyles.HexNumber);
                int num4 = int.Parse(str4, NumberStyles.HexNumber);
                color = Color.FromArgb(byte.Parse(num.ToString()), byte.Parse(num2.ToString()), byte.Parse(num3.ToString()), byte.Parse(num4.ToString()));
            }
            catch
            {
                return new Color();
            }
            return color;
        }

        public static ColorPalette CreateGradientPalette(string name, ColorCollection source)
        {
            List<ColorCollection> columns = new List<ColorCollection>();
            for (int i = 0; i < source.Count; i++)
            {
                columns.Add(GenerateColumnFromColor(source[i]));
            }
            return new CustomPalette(name, GetColorsFromColumns(columns), true);
        }

        private static ColorCollection GenerateColumnFromColor(Color color)
        {
            ColorCollection colors = new ColorCollection();
            int[] numArray = new int[] { -6, -15, -30, -45, -75 };
            int[] numArray2 = new int[] { 50, 0x23, 0x19, -15, -30 };
            int length = numArray.Length;
            List<Color> list = new List<Color> {
                ColorFromHex("#FFF2F2F2"),
                ColorFromHex("#FFD8D8D8"),
                ColorFromHex("#FFBFBFBF"),
                ColorFromHex("#FFA5A5A5"),
                ColorFromHex("#FF7F7F7F")
            };
            List<Color> list2 = new List<Color> {
                ColorFromHex("#FF7F7F7F"),
                ColorFromHex("#FF595959"),
                ColorFromHex("#FF3F3F3F"),
                ColorFromHex("#FF262626"),
                ColorFromHex("#FF0C0C0C")
            };
            for (int i = 0; i < length; i++)
            {
                if (color.Equals(Colors.White))
                {
                    colors.Add(list[i]);
                }
                else if (color.Equals(Colors.Black))
                {
                    colors.Add(list2[i]);
                }
                else
                {
                    int[] numArray3 = !IsLight(color) ? numArray2 : numArray;
                    int modification = numArray3[i];
                    colors.Add(ChangeBrightness(color, modification));
                }
            }
            return colors;
        }

        private static int GetBrightness(Color c) => 
            (int) Math.Sqrt((((c.R * c.R) * 0.241) + ((c.G * c.G) * 0.691)) + ((c.B * c.B) * 0.068));

        private static ColorCollection GetColorsFromColumns(List<ColorCollection> columns)
        {
            int num = 5;
            ColorCollection colors = new ColorCollection();
            int num2 = 0;
            while (num2 < num)
            {
                int num3 = 0;
                while (true)
                {
                    if (num3 >= columns.Count)
                    {
                        num2++;
                        break;
                    }
                    colors.Add(columns[num3][num2]);
                    num3++;
                }
            }
            return colors;
        }

        private static bool IsLight(Color color) => 
            GetBrightness(color) > 150;
    }
}

