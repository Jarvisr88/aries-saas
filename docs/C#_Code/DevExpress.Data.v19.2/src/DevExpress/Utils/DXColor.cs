namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public static class DXColor
    {
        public static readonly Color Empty = Color.Empty;
        public static readonly Color Transparent = Color.Transparent;
        public static readonly Color Black = Color.Black;
        public static readonly Color Gray = Color.Gray;
        public static readonly Color Azure = Color.Azure;
        public static readonly Color Wheat = Color.Wheat;
        public static readonly Color Bisque = Color.Bisque;
        public static readonly Color BlanchedAlmond = Color.BlanchedAlmond;
        public static readonly Color Beige = Color.Beige;
        public static readonly Color Cyan = Color.Cyan;
        public static readonly Color Red = Color.Red;
        public static readonly Color Blue = Color.Blue;
        public static readonly Color Green = Color.Green;
        public static readonly Color Yellow = Color.Yellow;
        public static readonly Color White = Color.White;
        public static readonly Color RosyBrown = Color.RosyBrown;
        public static readonly Color LightGreen = Color.LightGreen;
        public static readonly Color YellowGreen = Color.YellowGreen;
        public static readonly Color AliceBlue = Color.AliceBlue;
        public static readonly Color DimGray = Color.DimGray;
        public static readonly Color Teal = Color.Teal;
        public static readonly Color Sienna = Color.Sienna;
        public static readonly Color SaddleBrown = Color.SaddleBrown;
        public static readonly Color SeaGreen = Color.SeaGreen;
        public static readonly Color Snow = Color.Snow;
        public static readonly Color Maroon = Color.Maroon;
        public static readonly Color Aqua = Color.Aqua;
        public static readonly Color Aquamarine = Color.Aquamarine;
        public static readonly Color Silver = Color.Silver;
        public static readonly Color Magenta = Color.Magenta;
        public static readonly Color DarkBlue = Color.DarkBlue;
        public static readonly Color DarkCyan = Color.DarkCyan;
        public static readonly Color DarkGreen = Color.DarkGreen;
        public static readonly Color DarkMagenta = Color.DarkMagenta;
        public static readonly Color DarkRed = Color.DarkRed;
        public static readonly Color LightGray = Color.LightGray;
        public static readonly Color Brown = Color.Brown;
        public static readonly Color SkyBlue = Color.SkyBlue;
        public static readonly Color SteelBlue = Color.SteelBlue;
        public static readonly Color Coral = Color.Coral;
        public static readonly Dictionary<string, Color> PredefinedColors = CreatePredefinedColorsTable();

        public static Color Blend(Color color, Color backgroundColor)
        {
            if (color.A >= 0xff)
            {
                return FromArgb(color.R, color.G, color.B);
            }
            float num = ((float) color.A) / 255f;
            float num2 = 1f - num;
            return FromArgb((int) ((color.R * num) + (backgroundColor.R * num2)), (int) ((color.G * num) + (backgroundColor.G * num2)), (int) ((color.B * num) + (backgroundColor.B * num2)));
        }

        public static Color CalculateNearestColor(ICollection<Color> colorsToChooseFrom, Color value)
        {
            float hue = value.GetHue();
            float brightness = value.GetBrightness();
            float saturation = value.GetSaturation();
            Color empty = Empty;
            float maxValue = float.MaxValue;
            foreach (Color color2 in colorsToChooseFrom)
            {
                float num5 = Math.Abs((float) (color2.GetHue() - hue));
                if (num5 > 180f)
                {
                    num5 = 360f - num5;
                }
                float num6 = color2.GetSaturation() - saturation;
                float num7 = color2.GetBrightness() - brightness;
                float num8 = ((num5 * num5) + (num6 * num6)) + (num7 * num7);
                if (num8 < maxValue)
                {
                    maxValue = num8;
                    empty = color2;
                }
            }
            return empty;
        }

        private static Dictionary<string, Color> CreatePredefinedColorsTable() => 
            new Dictionary<string, Color> { 
                { 
                    "AliceBlue",
                    AliceBlue
                },
                { 
                    "AntiqueWhite",
                    FromArgb(0xff, 250, 0xeb, 0xd7)
                },
                { 
                    "Aqua",
                    Aqua
                },
                { 
                    "Aquamarine",
                    Aquamarine
                },
                { 
                    "Azure",
                    Azure
                },
                { 
                    "Beige",
                    Beige
                },
                { 
                    "Bisque",
                    Bisque
                },
                { 
                    "Black",
                    Black
                },
                { 
                    "BlanchedAlmond",
                    BlanchedAlmond
                },
                { 
                    "Blue",
                    Blue
                },
                { 
                    "BlueViolet",
                    FromArgb(0xff, 0x8a, 0x2b, 0xe2)
                },
                { 
                    "Brown",
                    FromArgb(0xff, 0xa5, 0x2a, 0x2a)
                },
                { 
                    "BurlyWood",
                    FromArgb(0xff, 0xde, 0xb8, 0x87)
                },
                { 
                    "CadetBlue",
                    FromArgb(0xff, 0x5f, 0x9e, 160)
                },
                { 
                    "Chartreuse",
                    FromArgb(0xff, 0x7f, 0xff, 0)
                },
                { 
                    "Chocolate",
                    FromArgb(0xff, 210, 0x69, 30)
                },
                { 
                    "Coral",
                    FromArgb(0xff, 0xff, 0x7f, 80)
                },
                { 
                    "CornflowerBlue",
                    FromArgb(0xff, 100, 0x95, 0xed)
                },
                { 
                    "Cornsilk",
                    FromArgb(0xff, 0xff, 0xf8, 220)
                },
                { 
                    "Crimson",
                    FromArgb(0xff, 220, 20, 60)
                },
                { 
                    "Cyan",
                    Cyan
                },
                { 
                    "DarkBlue",
                    DarkBlue
                },
                { 
                    "DarkCyan",
                    DarkCyan
                },
                { 
                    "DarkGoldenrod",
                    FromArgb(0xff, 0xb8, 0x86, 11)
                },
                { 
                    "DarkGray",
                    FromArgb(0xff, 0xa9, 0xa9, 0xa9)
                },
                { 
                    "DarkGreen",
                    DarkGreen
                },
                { 
                    "DarkKhaki",
                    FromArgb(0xff, 0xbd, 0xb7, 0x6b)
                },
                { 
                    "DarkMagenta",
                    DarkMagenta
                },
                { 
                    "DarkOliveGreen",
                    FromArgb(0xff, 0x55, 0x6b, 0x2f)
                },
                { 
                    "DarkOrange",
                    FromArgb(0xff, 0xff, 140, 0)
                },
                { 
                    "DarkOrchid",
                    FromArgb(0xff, 0x99, 50, 0xcc)
                },
                { 
                    "DarkRed",
                    DarkRed
                },
                { 
                    "DarkSalmon",
                    FromArgb(0xff, 0xe9, 150, 0x7a)
                },
                { 
                    "DarkSeaGreen",
                    FromArgb(0xff, 0x8f, 0xbc, 0x8f)
                },
                { 
                    "DarkSlateBlue",
                    FromArgb(0xff, 0x48, 0x3d, 0x8b)
                },
                { 
                    "DarkSlateGray",
                    FromArgb(0xff, 0x2f, 0x4f, 0x4f)
                },
                { 
                    "DarkTurquoise",
                    FromArgb(0xff, 0, 0xce, 0xd1)
                },
                { 
                    "DarkViolet",
                    FromArgb(0xff, 0x94, 0, 0xd3)
                },
                { 
                    "DeepPink",
                    FromArgb(0xff, 0xff, 20, 0x93)
                },
                { 
                    "DeepSkyBlue",
                    FromArgb(0xff, 0, 0xbf, 0xff)
                },
                { 
                    "DimGray",
                    DimGray
                },
                { 
                    "DodgerBlue",
                    FromArgb(0xff, 30, 0x90, 0xff)
                },
                { 
                    "FireBrick",
                    FromArgb(0xff, 0xb2, 0x22, 0x22)
                },
                { 
                    "FloralWhite",
                    FromArgb(0xff, 0xff, 250, 240)
                },
                { 
                    "ForestGreen",
                    FromArgb(0xff, 0x22, 0x8b, 0x22)
                },
                { 
                    "Fuchsia",
                    FromArgb(0xff, 0xff, 0, 0xff)
                },
                { 
                    "Gainsboro",
                    FromArgb(0xff, 220, 220, 220)
                },
                { 
                    "GhostWhite",
                    FromArgb(0xff, 0xf8, 0xf8, 0xff)
                },
                { 
                    "Gold",
                    FromArgb(0xff, 0xff, 0xd7, 0)
                },
                { 
                    "Goldenrod",
                    FromArgb(0xff, 0xda, 0xa5, 0x20)
                },
                { 
                    "Gray",
                    Gray
                },
                { 
                    "Green",
                    Green
                },
                { 
                    "GreenYellow",
                    FromArgb(0xff, 0xad, 0xff, 0x2f)
                },
                { 
                    "Honeydew",
                    FromArgb(0xff, 240, 0xff, 240)
                },
                { 
                    "HotPink",
                    FromArgb(0xff, 0xff, 0x69, 180)
                },
                { 
                    "IndianRed",
                    FromArgb(0xff, 0xcd, 0x5c, 0x5c)
                },
                { 
                    "Indigo",
                    FromArgb(0xff, 0x4b, 0, 130)
                },
                { 
                    "Ivory",
                    FromArgb(0xff, 0xff, 0xff, 240)
                },
                { 
                    "Khaki",
                    FromArgb(0xff, 240, 230, 140)
                },
                { 
                    "Lavender",
                    FromArgb(0xff, 230, 230, 250)
                },
                { 
                    "LavenderBlush",
                    FromArgb(0xff, 0xff, 240, 0xf5)
                },
                { 
                    "LawnGreen",
                    FromArgb(0xff, 0x7c, 0xfc, 0)
                },
                { 
                    "LemonChiffon",
                    FromArgb(0xff, 0xff, 250, 0xcd)
                },
                { 
                    "LightBlue",
                    FromArgb(0xff, 0xad, 0xd8, 230)
                },
                { 
                    "LightCoral",
                    FromArgb(0xff, 240, 0x80, 0x80)
                },
                { 
                    "LightCyan",
                    FromArgb(0xff, 0xe0, 0xff, 0xff)
                },
                { 
                    "LightGoldenrodYellow",
                    FromArgb(0xff, 250, 250, 210)
                },
                { 
                    "LightGray",
                    LightGray
                },
                { 
                    "LightGreen",
                    LightGreen
                },
                { 
                    "LightPink",
                    FromArgb(0xff, 0xff, 0xb6, 0xc1)
                },
                { 
                    "LightSalmon",
                    FromArgb(0xff, 0xff, 160, 0x7a)
                },
                { 
                    "LightSeaGreen",
                    FromArgb(0xff, 0x20, 0xb2, 170)
                },
                { 
                    "LightSkyBlue",
                    FromArgb(0xff, 0x87, 0xce, 250)
                },
                { 
                    "LightSlateGray",
                    FromArgb(0xff, 0x77, 0x88, 0x99)
                },
                { 
                    "LightSteelBlue",
                    FromArgb(0xff, 0xb0, 0xc4, 0xde)
                },
                { 
                    "LightYellow",
                    FromArgb(0xff, 0xff, 0xff, 0xe0)
                },
                { 
                    "Lime",
                    FromArgb(0xff, 0, 0xff, 0)
                },
                { 
                    "LimeGreen",
                    FromArgb(0xff, 50, 0xcd, 50)
                },
                { 
                    "Linen",
                    FromArgb(0xff, 250, 240, 230)
                },
                { 
                    "Magenta",
                    Magenta
                },
                { 
                    "Maroon",
                    Maroon
                },
                { 
                    "MediumAquaMarine",
                    FromArgb(0xff, 0x66, 0xcd, 170)
                },
                { 
                    "MediumBlue",
                    FromArgb(0xff, 0, 0, 0xcd)
                },
                { 
                    "MediumOrchid",
                    FromArgb(0xff, 0xba, 0x55, 0xd3)
                },
                { 
                    "MediumPurple",
                    FromArgb(0xff, 0x93, 0x70, 0xd8)
                },
                { 
                    "MediumSeaGreen",
                    FromArgb(0xff, 60, 0xb3, 0x71)
                },
                { 
                    "MediumSlateBlue",
                    FromArgb(0xff, 0x7b, 0x68, 0xee)
                },
                { 
                    "MediumSpringGreen",
                    FromArgb(0xff, 0, 250, 0x9a)
                },
                { 
                    "MediumTurquoise",
                    FromArgb(0xff, 0x48, 0xd1, 0xcc)
                },
                { 
                    "MediumVioletRed",
                    FromArgb(0xff, 0xc7, 0x15, 0x85)
                },
                { 
                    "MidnightBlue",
                    FromArgb(0xff, 0x19, 0x19, 0x70)
                },
                { 
                    "MintCream",
                    FromArgb(0xff, 0xf5, 0xff, 250)
                },
                { 
                    "MistyRose",
                    FromArgb(0xff, 0xff, 0xe4, 0xe1)
                },
                { 
                    "Moccasin",
                    FromArgb(0xff, 0xff, 0xe4, 0xb5)
                },
                { 
                    "NavajoWhite",
                    FromArgb(0xff, 0xff, 0xde, 0xad)
                },
                { 
                    "Navy",
                    FromArgb(0xff, 0, 0, 0x80)
                },
                { 
                    "OldLace",
                    FromArgb(0xff, 0xfd, 0xf5, 230)
                },
                { 
                    "Olive",
                    FromArgb(0xff, 0x80, 0x80, 0)
                },
                { 
                    "OliveDrab",
                    FromArgb(0xff, 0x6b, 0x8e, 0x23)
                },
                { 
                    "Orange",
                    FromArgb(0xff, 0xff, 0xa5, 0)
                },
                { 
                    "OrangeRed",
                    FromArgb(0xff, 0xff, 0x45, 0)
                },
                { 
                    "Orchid",
                    FromArgb(0xff, 0xda, 0x70, 0xd6)
                },
                { 
                    "PaleGoldenrod",
                    FromArgb(0xff, 0xee, 0xe8, 170)
                },
                { 
                    "PaleGreen",
                    FromArgb(0xff, 0x98, 0xfb, 0x98)
                },
                { 
                    "PaleTurquoise",
                    FromArgb(0xff, 0xaf, 0xee, 0xee)
                },
                { 
                    "PaleVioletRed",
                    FromArgb(0xff, 0xd8, 0x70, 0x93)
                },
                { 
                    "PapayaWhip",
                    FromArgb(0xff, 0xff, 0xef, 0xd5)
                },
                { 
                    "PeachPuff",
                    FromArgb(0xff, 0xff, 0xda, 0xb9)
                },
                { 
                    "Peru",
                    FromArgb(0xff, 0xcd, 0x85, 0x3f)
                },
                { 
                    "Pink",
                    FromArgb(0xff, 0xff, 0xc0, 0xcb)
                },
                { 
                    "Plum",
                    FromArgb(0xff, 0xdd, 160, 0xdd)
                },
                { 
                    "PowderBlue",
                    FromArgb(0xff, 0xb0, 0xe0, 230)
                },
                { 
                    "Purple",
                    FromArgb(0xff, 0x80, 0, 0x80)
                },
                { 
                    "Red",
                    Red
                },
                { 
                    "RosyBrown",
                    RosyBrown
                },
                { 
                    "RoyalBlue",
                    FromArgb(0xff, 0x41, 0x69, 0xe1)
                },
                { 
                    "SaddleBrown",
                    SaddleBrown
                },
                { 
                    "Salmon",
                    FromArgb(0xff, 250, 0x80, 0x72)
                },
                { 
                    "SandyBrown",
                    FromArgb(0xff, 0xf4, 0xa4, 0x60)
                },
                { 
                    "SeaGreen",
                    SeaGreen
                },
                { 
                    "SeaShell",
                    FromArgb(0xff, 0xff, 0xf5, 0xee)
                },
                { 
                    "Sienna",
                    Sienna
                },
                { 
                    "Silver",
                    Silver
                },
                { 
                    "SkyBlue",
                    FromArgb(0xff, 0x87, 0xce, 0xeb)
                },
                { 
                    "SlateBlue",
                    FromArgb(0xff, 0x6a, 90, 0xcd)
                },
                { 
                    "SlateGray",
                    FromArgb(0xff, 0x70, 0x80, 0x90)
                },
                { 
                    "Snow",
                    Snow
                },
                { 
                    "SpringGreen",
                    FromArgb(0xff, 0, 0xff, 0x7f)
                },
                { 
                    "SteelBlue",
                    FromArgb(0xff, 70, 130, 180)
                },
                { 
                    "Tan",
                    FromArgb(0xff, 210, 180, 140)
                },
                { 
                    "Teal",
                    Teal
                },
                { 
                    "Thistle",
                    FromArgb(0xff, 0xd8, 0xbf, 0xd8)
                },
                { 
                    "Tomato",
                    FromArgb(0xff, 0xff, 0x63, 0x47)
                },
                { 
                    "Turquoise",
                    FromArgb(0xff, 0x40, 0xe0, 0xd0)
                },
                { 
                    "Transparent",
                    Transparent
                },
                { 
                    "Violet",
                    FromArgb(0xff, 0xee, 130, 0xee)
                },
                { 
                    "Wheat",
                    Wheat
                },
                { 
                    "White",
                    White
                },
                { 
                    "WhiteSmoke",
                    FromArgb(0xff, 0xf5, 0xf5, 0xf5)
                },
                { 
                    "Yellow",
                    Yellow
                },
                { 
                    "YellowGreen",
                    YellowGreen
                }
            };

        public static Color FromArgb(int argb) => 
            Color.FromArgb(argb);

        public static Color FromArgb(int red, int green, int blue) => 
            Color.FromArgb(red, green, blue);

        public static Color FromArgb(int alpha, int red, int green, int blue) => 
            Color.FromArgb(alpha, red, green, blue);

        public static Color FromHtml(string htmlColor) => 
            ColorTranslator.FromHtml(htmlColor);

        public static Color FromName(string name) => 
            Color.FromName(name);

        public static Color FromOle(int oleColor) => 
            ColorTranslator.FromOle(oleColor);

        public static bool IsEmpty(Color c) => 
            c.IsEmpty;

        public static bool IsSemitransparentColor(Color color) => 
            (color.A > 0) && (color.A < 0xff);

        public static bool IsTransparentColor(Color color) => 
            color.A == 0;

        public static bool IsTransparentOrEmpty(Color color) => 
            (color == Empty) || (color == Transparent);

        public static int ToArgb(Color color) => 
            color.ToArgb();

        public static string ToHtml(Color c) => 
            ColorTranslator.ToHtml(c);

        public static int ToOle(Color color) => 
            ColorTranslator.ToOle(color);

        public static int ToWin32(Color c) => 
            ColorTranslator.ToWin32(c);
    }
}

