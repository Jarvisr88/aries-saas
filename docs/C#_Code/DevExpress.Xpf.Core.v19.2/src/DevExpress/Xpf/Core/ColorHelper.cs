namespace DevExpress.Xpf.Core
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Windows.Media;

    public static class ColorHelper
    {
        private static ColorDescription[] defaultColors;
        public static readonly System.Windows.Media.Color DefaultColor;

        static ColorHelper()
        {
            ColorDescription[] descriptionArray1 = new ColorDescription[0x8d];
            descriptionArray1[0] = new ColorDescription("Transparent", "#00FFFFFF");
            descriptionArray1[1] = new ColorDescription("Snow", "#FFFFFAFA");
            descriptionArray1[2] = new ColorDescription("GhostWhite", "#FFF8F8FF");
            descriptionArray1[3] = new ColorDescription("WhiteSmoke", "#FFF5F5F5");
            descriptionArray1[4] = new ColorDescription("Gainsboro", "#FFDCDCDC");
            descriptionArray1[5] = new ColorDescription("FloralWhite", "#FFFFFAF0");
            descriptionArray1[6] = new ColorDescription("OldLace", "#FFFDF5E6");
            descriptionArray1[7] = new ColorDescription("Linen", "#FFFAF0E6");
            descriptionArray1[8] = new ColorDescription("AntiqueWhite", "#FFFAEBD7");
            descriptionArray1[9] = new ColorDescription("PapayaWhip", "#FFFFEFD5");
            descriptionArray1[10] = new ColorDescription("BlanchedAlmond", "#FFFFEBCD");
            descriptionArray1[11] = new ColorDescription("Bisque", "#FFFFE4C4");
            descriptionArray1[12] = new ColorDescription("PeachPuff", "#FFFFDAB9");
            descriptionArray1[13] = new ColorDescription("NavajoWhite", "#FFFFDEAD");
            descriptionArray1[14] = new ColorDescription("Moccasin", "#FFFFE4B5");
            descriptionArray1[15] = new ColorDescription("Cornsilk", "#FFFFF8DC");
            descriptionArray1[0x10] = new ColorDescription("Ivory", "#FFFFFFF0");
            descriptionArray1[0x11] = new ColorDescription("LemonChiffon", "#FFFFFACD");
            descriptionArray1[0x12] = new ColorDescription("Seashell", "#FFFFF5EE");
            descriptionArray1[0x13] = new ColorDescription("Honeydew", "#FFF0FFF0");
            descriptionArray1[20] = new ColorDescription("MintCream", "#FFF5FFFA");
            descriptionArray1[0x15] = new ColorDescription("DeepBlue", "#FFF0FFFF");
            descriptionArray1[0x16] = new ColorDescription("AliceBlue", "#FFF0F8FF");
            descriptionArray1[0x17] = new ColorDescription("Lavender", "#FFE6E6FA");
            descriptionArray1[0x18] = new ColorDescription("LavenderBlush", "#FFFFF0F5");
            descriptionArray1[0x19] = new ColorDescription("MistyRose", "#FFFFE4E1");
            descriptionArray1[0x1a] = new ColorDescription("White", "#FFFFFFFF");
            descriptionArray1[0x1b] = new ColorDescription("Black", "#FF000000");
            descriptionArray1[0x1c] = new ColorDescription("DarkSlateGray", "#FF2F4F4F");
            descriptionArray1[0x1d] = new ColorDescription("DimGray", "#FF696969");
            descriptionArray1[30] = new ColorDescription("SlateGray", "#FF708090");
            descriptionArray1[0x1f] = new ColorDescription("LightSlateGray", "#FF778899");
            descriptionArray1[0x20] = new ColorDescription("DarkGray", "#FFA9A9A9");
            descriptionArray1[0x21] = new ColorDescription("Gray", "#FF808080");
            descriptionArray1[0x22] = new ColorDescription("Silver", "#FFC0C0C0");
            descriptionArray1[0x23] = new ColorDescription("LightGray", "#FFD3D3D3");
            descriptionArray1[0x24] = new ColorDescription("DarkSlateBlue", "#FF483D8B");
            descriptionArray1[0x25] = new ColorDescription("SlateBlue", "#FF6A5ACD");
            descriptionArray1[0x26] = new ColorDescription("MediumSlateBlue", "#FF7B68EE");
            descriptionArray1[0x27] = new ColorDescription("MidnightBlue", "#FF191970");
            descriptionArray1[40] = new ColorDescription("Navy", "#FF000080");
            descriptionArray1[0x29] = new ColorDescription("DarkBlue", "#FF00008B");
            descriptionArray1[0x2a] = new ColorDescription("MediumBlue", "#FF0000CD");
            descriptionArray1[0x2b] = new ColorDescription("Blue", "#FF0000FF");
            descriptionArray1[0x2c] = new ColorDescription("RoyalBlue", "#FF4169E1");
            descriptionArray1[0x2d] = new ColorDescription("CornflowerBlue", "#FF6495ED");
            descriptionArray1[0x2e] = new ColorDescription("DodgerBlue", "#FF1E90FF");
            descriptionArray1[0x2f] = new ColorDescription("DeepSkyBlue", "#FF00BFFF");
            descriptionArray1[0x30] = new ColorDescription("SkyBlue", "#FF87CEEB");
            descriptionArray1[0x31] = new ColorDescription("LightSkyBlue", "#FF87CEFA");
            descriptionArray1[50] = new ColorDescription("SteelBlue", "#FF4682B4");
            descriptionArray1[0x33] = new ColorDescription("LightSteelBlue", "#FFB0C4DE");
            descriptionArray1[0x34] = new ColorDescription("LightBlue", "#FFADD8E6");
            descriptionArray1[0x35] = new ColorDescription("PowderBlue", "#FFB0E0E6");
            descriptionArray1[0x36] = new ColorDescription("PaleTurquoise", "#FFAFEEEE");
            descriptionArray1[0x37] = new ColorDescription("DarkTurquoise", "#FF00CED1");
            descriptionArray1[0x38] = new ColorDescription("MediumTurquoise", "#FF48D1CC");
            descriptionArray1[0x39] = new ColorDescription("Turquoise", "#FF40E0D0");
            descriptionArray1[0x3a] = new ColorDescription("Aqua", "#FF00FFFF");
            descriptionArray1[0x3b] = new ColorDescription("DarkCyan", "#FF008B8B");
            descriptionArray1[60] = new ColorDescription("Cyan", "#FF00FFFF");
            descriptionArray1[0x3d] = new ColorDescription("LightCyan", "#FFE0FFFF");
            descriptionArray1[0x3e] = new ColorDescription("CadetBlue", "#FF5F9EA0");
            descriptionArray1[0x3f] = new ColorDescription("MediumAquamarine", "#FF66CDAA");
            descriptionArray1[0x40] = new ColorDescription("Aquamarine", "#FF7FFFD4");
            descriptionArray1[0x41] = new ColorDescription("DarkGreen", "#FF006400");
            descriptionArray1[0x42] = new ColorDescription("DarkOliveGreen", "#FF556B2F");
            descriptionArray1[0x43] = new ColorDescription("DarkSeaGreen", "#FF8FBC8F");
            descriptionArray1[0x44] = new ColorDescription("Teal", "#FF008080");
            descriptionArray1[0x45] = new ColorDescription("SeaGreen", "#FF2E8B57");
            descriptionArray1[70] = new ColorDescription("MediumSeaGreen", "#FF3CB371");
            descriptionArray1[0x47] = new ColorDescription("LightSeaGreen", "#FF20B2AA");
            descriptionArray1[0x48] = new ColorDescription("PaleGreen", "#FF98FB98");
            descriptionArray1[0x49] = new ColorDescription("LightGreen", "#FF90EE90");
            descriptionArray1[0x4a] = new ColorDescription("SpringGreen", "#FF00FF7F");
            descriptionArray1[0x4b] = new ColorDescription("LawnGreen", "#FF7CFC00");
            descriptionArray1[0x4c] = new ColorDescription("Green", "#FF008000");
            descriptionArray1[0x4d] = new ColorDescription("Chartreuse", "#FF7FFF00");
            descriptionArray1[0x4e] = new ColorDescription("MediumSpringGreen", "#FF00FA9A");
            descriptionArray1[0x4f] = new ColorDescription("GreenYellow", "#FFADFF2F");
            descriptionArray1[80] = new ColorDescription("Lime", "#FF00FF00");
            descriptionArray1[0x51] = new ColorDescription("LimeGreen", "#FF32CD32");
            descriptionArray1[0x52] = new ColorDescription("YellowGreen", "#FF9ACD32");
            descriptionArray1[0x53] = new ColorDescription("ForestGreen", "#FF228B22");
            descriptionArray1[0x54] = new ColorDescription("Olive", "#FF808000");
            descriptionArray1[0x55] = new ColorDescription("OliveDrab", "#FF6B8E23");
            descriptionArray1[0x56] = new ColorDescription("DarkKhaki", "#FFBDB76B");
            descriptionArray1[0x57] = new ColorDescription("Khaki", "#FFF0E68C");
            descriptionArray1[0x58] = new ColorDescription("PaleGoldenrod", "#FFEEE8AA");
            descriptionArray1[0x59] = new ColorDescription("LightGoldenrodYellow", "#FFFAFAD2");
            descriptionArray1[90] = new ColorDescription("LightYellow", "#FFFFFFE0");
            descriptionArray1[0x5b] = new ColorDescription("Yellow", "#FFFFFF00");
            descriptionArray1[0x5c] = new ColorDescription("Gold", "#FFFFD700");
            descriptionArray1[0x5d] = new ColorDescription("Goldenrod", "#FFDAA520");
            descriptionArray1[0x5e] = new ColorDescription("DarkGoldenrod", "#FFB8860B");
            descriptionArray1[0x5f] = new ColorDescription("RosyBrown", "#FFBC8F8F");
            descriptionArray1[0x60] = new ColorDescription("IndianRed", "#FFCD5C5C");
            descriptionArray1[0x61] = new ColorDescription("SaddleBrown", "#FF8B4513");
            descriptionArray1[0x62] = new ColorDescription("Sienna", "#FFA0522D");
            descriptionArray1[0x63] = new ColorDescription("Peru", "#FFCD853F");
            descriptionArray1[100] = new ColorDescription("Burlywood", "#FFDEB887");
            descriptionArray1[0x65] = new ColorDescription("Beige", "#FFF5F5DC");
            descriptionArray1[0x66] = new ColorDescription("Wheat", "#FFF5DEB3");
            descriptionArray1[0x67] = new ColorDescription("SandyBrown", "#FFF4A460");
            descriptionArray1[0x68] = new ColorDescription("Tan", "#FFD2B48C");
            descriptionArray1[0x69] = new ColorDescription("Chocolate", "#FFD2691E");
            descriptionArray1[0x6a] = new ColorDescription("Firebrick", "#FFB22222");
            descriptionArray1[0x6b] = new ColorDescription("Brown", "#FFA52A2A");
            descriptionArray1[0x6c] = new ColorDescription("DarkSalmon", "#FFE9967A");
            descriptionArray1[0x6d] = new ColorDescription("Salmon", "#FFFA8072");
            descriptionArray1[110] = new ColorDescription("LightSalmon", "#FFFFA07A");
            descriptionArray1[0x6f] = new ColorDescription("Orange", "#FFFFA500");
            descriptionArray1[0x70] = new ColorDescription("DarkOrange", "#FFFF8C00");
            descriptionArray1[0x71] = new ColorDescription("Coral", "#FFFF7F50");
            descriptionArray1[0x72] = new ColorDescription("LightCoral", "#FFF08080");
            descriptionArray1[0x73] = new ColorDescription("Tomato", "#FFFF6347");
            descriptionArray1[0x74] = new ColorDescription("OrangeRed", "#FFFF4500");
            descriptionArray1[0x75] = new ColorDescription("Red", "#FFFF0000");
            descriptionArray1[0x76] = new ColorDescription("HotPink", "#FFFF69B4");
            descriptionArray1[0x77] = new ColorDescription("DeepPink", "#FFFF1493");
            descriptionArray1[120] = new ColorDescription("Pink", "#FFFFC0CB");
            descriptionArray1[0x79] = new ColorDescription("LightPink", "#FFFFB6C1");
            descriptionArray1[0x7a] = new ColorDescription("PaleVioletRed", "#FFDB7093");
            descriptionArray1[0x7b] = new ColorDescription("Maroon", "#FF800000");
            descriptionArray1[0x7c] = new ColorDescription("MediumVioletRed", "#FFC71585");
            descriptionArray1[0x7d] = new ColorDescription("Magenta", "#FFFF00FF");
            descriptionArray1[0x7e] = new ColorDescription("Fuchsia", "#FFFF00FF");
            descriptionArray1[0x7f] = new ColorDescription("Violet", "#FFEE82EE");
            descriptionArray1[0x80] = new ColorDescription("Plum", "#FFDDA0DD");
            descriptionArray1[0x81] = new ColorDescription("Orchid", "#FFDA70D6");
            descriptionArray1[130] = new ColorDescription("MediumOrchid", "#FFBA55D3");
            descriptionArray1[0x83] = new ColorDescription("DarkOrchid", "#FF9932CC");
            descriptionArray1[0x84] = new ColorDescription("DarkViolet", "#FF9400D3");
            descriptionArray1[0x85] = new ColorDescription("BlueViolet", "#FF8A2BE2");
            descriptionArray1[0x86] = new ColorDescription("Purple", "#FF800080");
            descriptionArray1[0x87] = new ColorDescription("MediumPurple", "#FF9370DB");
            descriptionArray1[0x88] = new ColorDescription("Thistle", "#FFD8BFD8");
            descriptionArray1[0x89] = new ColorDescription("DarkMagenta", "#FF8B008B");
            descriptionArray1[0x8a] = new ColorDescription("DarkRed", "#FF8B0000");
            descriptionArray1[0x8b] = new ColorDescription("Indigo", "#FF4B0082");
            descriptionArray1[140] = new ColorDescription("Crimson", "#FFDC143C");
            defaultColors = descriptionArray1;
            DefaultColor = System.Windows.Media.Color.FromArgb(0xff, 0x80, 0x80, 0x80);
            Array.Sort<ColorDescription>(defaultColors, new Comparison<ColorDescription>(ColorDescription.Compare));
        }

        public static System.Windows.Media.Color Brightness(System.Windows.Media.Color c, double brightness) => 
            (brightness < 0.0) ? Dark(c, -brightness) : Light(c, brightness);

        public static System.Windows.Media.Color CreateColorFromString(string stringValue)
        {
            System.Windows.Media.Color? nullable = CreateColorFromStringCore(stringValue);
            if (nullable != null)
            {
                return nullable.Value;
            }
            return new System.Windows.Media.Color();
        }

        private static System.Windows.Media.Color? CreateColorFromStringCore(string stringValue)
        {
            int num2;
            byte num4;
            int index = FindColorDescriptionIndexByName(stringValue);
            string str = (index != -1) ? defaultColors[index].Value : stringValue;
            if (!str.StartsWith("#"))
            {
                return null;
            }
            if ((str.Length != 7) && (str.Length != 9))
            {
                return null;
            }
            if (!int.TryParse(str.Substring(1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2))
            {
                return null;
            }
            System.Windows.Media.Color transparent = Colors.Transparent;
            transparent.B = (byte) (num2 & 0xff);
            transparent.G = (byte) ((num2 >> 8) & 0xff);
            transparent.R = (byte) ((num2 >> 0x10) & 0xff);
            if (str.Length != 7)
            {
                num4 = transparent.A = (byte) ((num2 >> 0x18) & 0xff);
            }
            else
            {
                num4 = transparent.A = 0xff;
            }
            transparent.A = num4;
            return new System.Windows.Media.Color?(transparent);
        }

        private static System.Windows.Media.Color Dark(System.Windows.Media.Color color, double factor)
        {
            factor = Math.Min(Math.Max(0.0, factor), 1.0);
            System.Drawing.Color color2 = ControlPaint.Dark(System.Drawing.Color.FromArgb(0xff, color.R, color.G, color.B), (float) factor);
            return System.Windows.Media.Color.FromArgb(color.A, color2.R, color2.G, color2.B);
        }

        private static int FindColorDescriptionIndexByName(string name)
        {
            int num = 0;
            int index = defaultColors.GetLength(0) - 1;
            int num3 = 0;
            int num4 = 0;
            num4 = defaultColors[0].Name.CompareTo(name);
            if (num4 > 0)
            {
                return -1;
            }
            if (num4 == 0)
            {
                return 0;
            }
            num4 = defaultColors[index].Name.CompareTo(name);
            if (num4 < 0)
            {
                return -1;
            }
            if (num4 == 0)
            {
                return 0;
            }
            while (true)
            {
                num3 = (((index - num) % 2) == 0) ? ((index + num) / 2) : (((index + num) - 1) / 2);
                num4 = defaultColors[num3].Name.CompareTo(name);
                if (num4 == 0)
                {
                    return num3;
                }
                if (num4 > 0)
                {
                    if (index == num3)
                    {
                        return -1;
                    }
                    index = num3;
                }
                else
                {
                    if (num == num3)
                    {
                        return -1;
                    }
                    num = num3;
                }
                if ((num == index) && (num == num3))
                {
                    return -1;
                }
            }
        }

        public static System.Windows.Media.Color FromHSB(double h, double s, double b)
        {
            double d = h / 60.0;
            double num3 = d - Math.Floor(d);
            double num4 = b * (1.0 - s);
            double num5 = b * (1.0 - (num3 * s));
            double num6 = b * (1.0 - ((1.0 - num3) * s));
            switch ((((int) Math.Floor(d)) % 6))
            {
                case 0:
                    return System.Windows.Media.Color.FromArgb(0xff, (byte) (b * 255.0), (byte) (num6 * 255.0), (byte) (num4 * 255.0));

                case 1:
                    return System.Windows.Media.Color.FromArgb(0xff, (byte) (num5 * 255.0), (byte) (b * 255.0), (byte) (num4 * 255.0));

                case 2:
                    return System.Windows.Media.Color.FromArgb(0xff, (byte) (num4 * 255.0), (byte) (b * 255.0), (byte) (num6 * 255.0));

                case 3:
                    return System.Windows.Media.Color.FromArgb(0xff, (byte) (num4 * 255.0), (byte) (num5 * 255.0), (byte) (b * 255.0));

                case 4:
                    return System.Windows.Media.Color.FromArgb(0xff, (byte) (num6 * 255.0), (byte) (num6 * 255.0), (byte) (b * 255.0));

                case 5:
                    return System.Windows.Media.Color.FromArgb(0xff, (byte) (b * 255.0), (byte) (num4 * 255.0), (byte) (num5 * 255.0));
            }
            return Colors.White;
        }

        public static float GetBrightness(System.Windows.Media.Color c) => 
            System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B).GetBrightness();

        public static float GetHue(System.Windows.Media.Color c) => 
            System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B).GetHue();

        public static float GetSaturation(System.Windows.Media.Color c) => 
            System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B).GetSaturation();

        public static bool IsTransparent(System.Windows.Media.Color c) => 
            c.A == 0;

        public static bool IsValidColorStringValue(string stringValue) => 
            CreateColorFromStringCore(stringValue) != null;

        private static System.Windows.Media.Color Light(System.Windows.Media.Color color, double factor)
        {
            factor = Math.Min(Math.Max(0.0, factor), 1.0);
            System.Drawing.Color color2 = ControlPaint.Light(System.Drawing.Color.FromArgb(0xff, color.R, color.G, color.B), (float) (factor * 2.0));
            return System.Windows.Media.Color.FromArgb(color.A, color2.R, color2.G, color2.B);
        }

        public static byte MixAlpha(byte a1, byte a2) => 
            Convert.ToByte((double) (((((double) a1) / 255.0) * (((double) a2) / 255.0)) * 255.0));

        public static System.Windows.Media.Brush MixColors(System.Windows.Media.Brush brush, System.Windows.Media.Color targetColor)
        {
            if (brush == null)
            {
                return null;
            }
            System.Windows.Media.Brush brush2 = brush.CloneCurrentValue();
            SolidColorBrush brush3 = brush2 as SolidColorBrush;
            if (brush3 != null)
            {
                brush3.Color = OverlayColor(brush3.Color, targetColor);
            }
            else
            {
                GradientBrush brush4 = brush2 as GradientBrush;
                if (brush4 != null)
                {
                    foreach (GradientStop stop in brush4.GradientStops)
                    {
                        stop.Color = OverlayColor(stop.Color, targetColor);
                    }
                }
            }
            return brush2;
        }

        public static System.Windows.Media.Brush MixColors2(System.Windows.Media.Brush brush, System.Windows.Media.Color targetColor)
        {
            if (brush == null)
            {
                return null;
            }
            System.Windows.Media.Brush brush2 = brush.CloneCurrentValue();
            SolidColorBrush brush3 = brush2 as SolidColorBrush;
            if (brush3 != null)
            {
                brush3.Color = MixColors2(brush3.Color, targetColor);
                brush3.Freeze();
                return brush3;
            }
            GradientBrush brush4 = brush2 as GradientBrush;
            if (brush4 == null)
            {
                return brush2;
            }
            foreach (GradientStop stop in brush4.GradientStops)
            {
                stop.Color = MixColors2(stop.Color, targetColor);
            }
            brush4.Freeze();
            return brush4;
        }

        private static System.Windows.Media.Color MixColors2(System.Windows.Media.Color baseColor, System.Windows.Media.Color targetColor)
        {
            double b = Math.Min(1f, GetBrightness(baseColor));
            double hue = GetHue(targetColor);
            if (hue > 0.0)
            {
                System.Windows.Media.Color color = FromHSB(hue, (double) (GetSaturation(baseColor) * 0.45f), b);
                return System.Windows.Media.Color.FromArgb(targetColor.A, color.R, color.G, color.B);
            }
            byte r = (byte) (b * 255.0);
            return OverlayColor(System.Windows.Media.Color.FromArgb(baseColor.A, r, r, r), targetColor);
        }

        private static byte MultiplyComponents(int component1, int component2)
        {
            int num = ((2 * component1) * component2) / 0xff;
            return ((num > 0xff) ? 0xff : ((byte) num));
        }

        private static void NormalizeGradientBrush(GradientBrush gradientBrush)
        {
            for (int i = 0; i < (gradientBrush.GradientStops.Count - 1); i++)
            {
                int num2 = Math.Max(gradientBrush.GradientStops[i].Color.R, gradientBrush.GradientStops[i + 1].Color.R);
                int num3 = Math.Min(gradientBrush.GradientStops[i].Color.R, gradientBrush.GradientStops[i + 1].Color.R);
                if ((num3 < 0x80) && (num2 > 0x80))
                {
                    int num4 = gradientBrush.GradientStops[i].Color.R - gradientBrush.GradientStops[i + 1].Color.R;
                    double num5 = gradientBrush.GradientStops[i + 1].Offset - gradientBrush.GradientStops[i].Offset;
                    double num6 = ((((double) (gradientBrush.GradientStops[i].Color.R - DefaultColor.R)) / ((double) num4)) * num5) + gradientBrush.GradientStops[i].Offset;
                    int num7 = gradientBrush.GradientStops[i].Color.A - gradientBrush.GradientStops[i + 1].Color.A;
                    System.Windows.Media.Color defaultColor = DefaultColor;
                    System.Windows.Media.Color color = gradientBrush.GradientStops[i + 1].Color;
                    defaultColor.A = (byte) (((int) (((gradientBrush.GradientStops[i].Offset - num6) / num5) * num7)) + color.A);
                    GradientStop stop1 = new GradientStop();
                    stop1.Color = defaultColor;
                    stop1.Offset = num6;
                    gradientBrush.GradientStops.Insert(i + 1, stop1);
                }
            }
        }

        public static System.Windows.Media.Brush NormalizeGradients(System.Windows.Media.Brush brush)
        {
            System.Windows.Media.Brush brush2 = brush.CloneCurrentValue();
            GradientBrush gradientBrush = brush2 as GradientBrush;
            if (gradientBrush == null)
            {
                return brush2;
            }
            NormalizeGradientBrush(gradientBrush);
            return gradientBrush;
        }

        public static System.Windows.Media.Color OverlayColor(System.Windows.Media.Color color1, System.Windows.Media.Color color2) => 
            System.Windows.Media.Color.FromArgb(MixAlpha(color1.A, color2.A), OverlayComponents(color1.R, color2.R), OverlayComponents(color1.G, color2.G), OverlayComponents(color1.B, color2.B));

        private static byte OverlayComponents(byte component1, byte component2) => 
            (component1 >= 0x80) ? ((component1 <= 0x80) ? component2 : ScreenComponents(component1, component2)) : MultiplyComponents(component1, component2);

        private static byte ScreenComponents(int component1, int component2)
        {
            int num = 0xff - (((2 * (0xff - component1)) * (0xff - component2)) / 0xff);
            return ((num < 0) ? 0 : ((byte) num));
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ColorDescription
        {
            public string Name;
            public string Value;
            public ColorDescription(string name, string value)
            {
                this.Name = name;
                this.Value = value;
            }

            public static int Compare(ColorHelper.ColorDescription value1, ColorHelper.ColorDescription value2) => 
                value1.Name.CompareTo(value2.Name);
        }
    }
}

