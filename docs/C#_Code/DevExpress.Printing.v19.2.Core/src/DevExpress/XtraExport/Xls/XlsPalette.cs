namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class XlsPalette
    {
        public const int BuiltInColorsCount = 8;
        public const int DefaultForegroundColorIndex = 0x40;
        public const int DefaultBackgroundColorIndex = 0x41;
        public const int SystemWindowFrameColorIndex = 0x42;
        public const int System3DFaceColorIndex = 0x43;
        public const int System3DTextColorIndex = 0x44;
        public const int System3DHighlightColorIndex = 0x45;
        public const int System3DShadowColorIndex = 70;
        public const int SystemHighlightColorIndex = 0x47;
        public const int SystemControlTextColorIndex = 0x48;
        public const int SystemControlScrollColorIndex = 0x49;
        public const int SystemControlInverseColorIndex = 0x4a;
        public const int SystemControlBodyColorIndex = 0x4b;
        public const int SystemControlFrameColorIndex = 0x4c;
        public const int DefaultChartForegroundColorIndex = 0x4d;
        public const int DefaultChartBackgroundColorIndex = 0x4e;
        public const int ChartNeutralColorIndex = 0x4f;
        public const int ToolTipFillColorIndex = 80;
        public const int ToolTipTextColorIndex = 0x51;
        public const int FontAutomaticColorIndex = 0x7fff;
        private Dictionary<int, Color> colorTable;
        private bool[] usedEntries;
        private bool isDefault;

        public XlsPalette()
        {
            this.colorTable = this.CreateDefaultColorTable();
            this.isDefault = true;
            this.InitializeUsedEntries();
        }

        private double ColorDifferenceHSB(Color x, Color y)
        {
            double num = Math.Abs((float) (x.GetHue() - y.GetHue()));
            if (num > 180.0)
            {
                num = 360.0 - num;
            }
            double num3 = Math.Abs((float) (x.GetSaturation() - y.GetSaturation())) * 1.5;
            return (((Math.Abs((float) (x.GetBrightness() - y.GetBrightness())) * 3.0) + (num / 57.3)) + num3);
        }

        private double ColorDifferenceRGB(Color x, Color y)
        {
            double num = (x.R - y.R) / 255.0;
            double num2 = (x.G - y.G) / 255.0;
            double num3 = (x.B - y.B) / 255.0;
            return Math.Sqrt(((num * num) + (num2 * num2)) + (num3 * num3));
        }

        private Dictionary<int, Color> CreateDefaultColorTable()
        {
            Dictionary<int, Color> dictionary = new Dictionary<int, Color> {
                { 
                    0,
                    DXColor.FromArgb(0, 0, 0, 0)
                },
                { 
                    1,
                    DXColor.FromArgb(0, 0xff, 0xff, 0xff)
                },
                { 
                    2,
                    DXColor.FromArgb(0, 0xff, 0, 0)
                },
                { 
                    3,
                    DXColor.FromArgb(0, 0, 0xff, 0)
                },
                { 
                    4,
                    DXColor.FromArgb(0, 0, 0, 0xff)
                },
                { 
                    5,
                    DXColor.FromArgb(0, 0xff, 0xff, 0)
                },
                { 
                    6,
                    DXColor.FromArgb(0, 0xff, 0, 0xff)
                },
                { 
                    7,
                    DXColor.FromArgb(0, 0, 0xff, 0xff)
                },
                { 
                    8,
                    DXColor.FromArgb(0, 0, 0, 0)
                },
                { 
                    9,
                    DXColor.FromArgb(0, 0xff, 0xff, 0xff)
                },
                { 
                    10,
                    DXColor.FromArgb(0, 0xff, 0, 0)
                },
                { 
                    11,
                    DXColor.FromArgb(0, 0, 0xff, 0)
                },
                { 
                    12,
                    DXColor.FromArgb(0, 0, 0, 0xff)
                },
                { 
                    13,
                    DXColor.FromArgb(0, 0xff, 0xff, 0)
                },
                { 
                    14,
                    DXColor.FromArgb(0, 0xff, 0, 0xff)
                },
                { 
                    15,
                    DXColor.FromArgb(0, 0, 0xff, 0xff)
                },
                { 
                    0x10,
                    DXColor.FromArgb(0, 0x80, 0, 0)
                },
                { 
                    0x11,
                    DXColor.FromArgb(0, 0, 0x80, 0)
                },
                { 
                    0x12,
                    DXColor.FromArgb(0, 0, 0, 0x80)
                },
                { 
                    0x13,
                    DXColor.FromArgb(0, 0x80, 0x80, 0)
                },
                { 
                    20,
                    DXColor.FromArgb(0, 0x80, 0, 0x80)
                },
                { 
                    0x15,
                    DXColor.FromArgb(0, 0, 0x80, 0x80)
                },
                { 
                    0x16,
                    DXColor.FromArgb(0, 0xc0, 0xc0, 0xc0)
                },
                { 
                    0x17,
                    DXColor.FromArgb(0, 0x80, 0x80, 0x80)
                },
                { 
                    0x18,
                    DXColor.FromArgb(0, 0x99, 0x99, 0xff)
                },
                { 
                    0x19,
                    DXColor.FromArgb(0, 0x99, 0x33, 0x66)
                },
                { 
                    0x1a,
                    DXColor.FromArgb(0, 0xff, 0xff, 0xcc)
                },
                { 
                    0x1b,
                    DXColor.FromArgb(0, 0xcc, 0xff, 0xff)
                },
                { 
                    0x1c,
                    DXColor.FromArgb(0, 0x66, 0, 0x66)
                },
                { 
                    0x1d,
                    DXColor.FromArgb(0, 0xff, 0x80, 0x80)
                },
                { 
                    30,
                    DXColor.FromArgb(0, 0, 0x66, 0xcc)
                },
                { 
                    0x1f,
                    DXColor.FromArgb(0, 0xcc, 0xcc, 0xff)
                },
                { 
                    0x20,
                    DXColor.FromArgb(0, 0, 0, 0x80)
                },
                { 
                    0x21,
                    DXColor.FromArgb(0, 0xff, 0, 0xff)
                },
                { 
                    0x22,
                    DXColor.FromArgb(0, 0xff, 0xff, 0)
                },
                { 
                    0x23,
                    DXColor.FromArgb(0, 0, 0xff, 0xff)
                },
                { 
                    0x24,
                    DXColor.FromArgb(0, 0x80, 0, 0x80)
                },
                { 
                    0x25,
                    DXColor.FromArgb(0, 0x80, 0, 0)
                },
                { 
                    0x26,
                    DXColor.FromArgb(0, 0, 0x80, 0x80)
                },
                { 
                    0x27,
                    DXColor.FromArgb(0, 0, 0, 0xff)
                },
                { 
                    40,
                    DXColor.FromArgb(0, 0, 0xcc, 0xff)
                },
                { 
                    0x29,
                    DXColor.FromArgb(0, 0xcc, 0xff, 0xff)
                },
                { 
                    0x2a,
                    DXColor.FromArgb(0, 0xcc, 0xff, 0xcc)
                },
                { 
                    0x2b,
                    DXColor.FromArgb(0, 0xff, 0xff, 0x99)
                },
                { 
                    0x2c,
                    DXColor.FromArgb(0, 0x99, 0xcc, 0xff)
                },
                { 
                    0x2d,
                    DXColor.FromArgb(0, 0xff, 0x99, 0xcc)
                },
                { 
                    0x2e,
                    DXColor.FromArgb(0, 0xcc, 0x99, 0xff)
                },
                { 
                    0x2f,
                    DXColor.FromArgb(0, 0xff, 0xcc, 0x99)
                },
                { 
                    0x30,
                    DXColor.FromArgb(0, 0x33, 0x66, 0xff)
                },
                { 
                    0x31,
                    DXColor.FromArgb(0, 0x33, 0xcc, 0xcc)
                },
                { 
                    50,
                    DXColor.FromArgb(0, 0x99, 0xcc, 0)
                },
                { 
                    0x33,
                    DXColor.FromArgb(0, 0xff, 0xcc, 0)
                },
                { 
                    0x34,
                    DXColor.FromArgb(0, 0xff, 0x99, 0)
                },
                { 
                    0x35,
                    DXColor.FromArgb(0, 0xff, 0x66, 0)
                },
                { 
                    0x36,
                    DXColor.FromArgb(0, 0x66, 0x66, 0x99)
                },
                { 
                    0x37,
                    DXColor.FromArgb(0, 150, 150, 150)
                },
                { 
                    0x38,
                    DXColor.FromArgb(0, 0, 0x33, 0x66)
                },
                { 
                    0x39,
                    DXColor.FromArgb(0, 0x33, 0x99, 0x66)
                },
                { 
                    0x3a,
                    DXColor.FromArgb(0, 0, 0x33, 0)
                },
                { 
                    0x3b,
                    DXColor.FromArgb(0, 0x33, 0x33, 0)
                },
                { 
                    60,
                    DXColor.FromArgb(0, 0x99, 0x33, 0)
                },
                { 
                    0x3d,
                    DXColor.FromArgb(0, 0x99, 0x33, 0x66)
                },
                { 
                    0x3e,
                    DXColor.FromArgb(0, 0x33, 0x33, 0x99)
                },
                { 
                    0x3f,
                    DXColor.FromArgb(0, 0x33, 0x33, 0x33)
                },
                { 
                    0x42,
                    DXSystemColors.WindowFrame
                },
                { 
                    0x43,
                    DXSystemColors.Control
                },
                { 
                    0x44,
                    DXSystemColors.ControlText
                },
                { 
                    0x45,
                    DXSystemColors.ControlLight
                },
                { 
                    70,
                    DXSystemColors.ControlDark
                },
                { 
                    0x47,
                    DXSystemColors.Highlight
                },
                { 
                    0x48,
                    DXSystemColors.ControlText
                }
            };
            Color scrollBar = DXSystemColors.ScrollBar;
            dictionary.Add(0x49, scrollBar);
            dictionary.Add(0x4a, DXColor.FromArgb(0, ~scrollBar.R, ~scrollBar.G, ~scrollBar.B));
            dictionary.Add(0x4b, DXSystemColors.Window);
            dictionary.Add(0x4c, DXSystemColors.WindowFrame);
            dictionary.Add(0x40, DXSystemColors.WindowText);
            dictionary.Add(0x41, DXSystemColors.Window);
            dictionary.Add(0x4d, DXColor.FromArgb(0, 0, 0, 0));
            dictionary.Add(0x4e, DXColor.FromArgb(0, 0xff, 0xff, 0xff));
            dictionary.Add(0x4f, DXColor.FromArgb(0, 0, 0, 0));
            dictionary.Add(80, DXSystemColors.Info);
            dictionary.Add(0x51, DXSystemColors.InfoText);
            dictionary.Add(0x7fff, DXColor.Empty);
            return dictionary;
        }

        private int FindEntry()
        {
            for (int i = 0x3f; i >= 8; i--)
            {
                if (!this.usedEntries[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetBackgroundColorIndex(XlColor color, XlDocumentTheme theme) => 
            !color.IsAutoOrEmpty ? this.GetPaletteNearestColorIndex(color.ConvertToRgb(theme)) : 0x41;

        private double GetColorDistance(Color x, Color y, double rgbWeight) => 
            this.ColorDifferenceHSB(x, y) + (this.ColorDifferenceRGB(x, y) * rgbWeight);

        public int GetColorIndex(Color color)
        {
            int num = this.GetExactColorIndex(color, 8, 0x3f);
            if (num != -1)
            {
                return num;
            }
            if (color.A == 0xff)
            {
                color = DXColor.FromArgb(0, color.R, color.G, color.B);
                num = this.GetExactColorIndex(color, 8, 0x3f);
                if (num != -1)
                {
                    return num;
                }
            }
            return 0x40;
        }

        private int GetExactColorIndex(Color color, int minIndex, int maxIndex)
        {
            int key;
            using (Dictionary<int, Color>.Enumerator enumerator = this.colorTable.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<int, Color> current = enumerator.Current;
                        if ((current.Key < minIndex) || ((current.Key > maxIndex) || (current.Value != color)))
                        {
                            continue;
                        }
                        key = current.Key;
                    }
                    else
                    {
                        return -1;
                    }
                    break;
                }
            }
            return key;
        }

        public int GetFontColorIndex(XlColor color, XlDocumentTheme theme) => 
            !color.IsAutoOrEmpty ? this.GetPaletteNearestColorIndex(color.ConvertToRgb(theme)) : 0x7fff;

        public int GetForegroundColorIndex(XlColor color, XlDocumentTheme theme) => 
            !color.IsAutoOrEmpty ? this.GetPaletteNearestColorIndex(color.ConvertToRgb(theme)) : 0x40;

        public int GetNearestColorIndex(Color color)
        {
            int num = this.GetExactColorIndex(color, 0, 0x3f);
            return ((num == -1) ? this.GetNearestColorIndexCore(color, 0, 0x3f) : num);
        }

        private int GetNearestColorIndexCore(Color color, int minIndex, int maxIndex)
        {
            List<ColorDistanceInfo> list = new List<ColorDistanceInfo>();
            foreach (KeyValuePair<int, Color> pair in this.colorTable)
            {
                if ((pair.Key >= minIndex) && ((pair.Key <= maxIndex) && this.IsCompatibleColors(pair.Value, color)))
                {
                    ColorDistanceInfo item = new ColorDistanceInfo {
                        Distance = this.GetColorDistance(color, pair.Value, 3.0),
                        ColorIndex = pair.Key
                    };
                    list.Add(item);
                }
            }
            list.Sort();
            if (list.Count > 5)
            {
                list.RemoveRange(5, list.Count - 5);
            }
            int colorIndex = -1;
            double maxValue = double.MaxValue;
            foreach (ColorDistanceInfo info2 in list)
            {
                if (colorIndex == -1)
                {
                    colorIndex = info2.ColorIndex;
                    maxValue = this.GetColorDistance(color, this.colorTable[info2.ColorIndex], 1.5);
                    continue;
                }
                double num3 = this.GetColorDistance(color, this.colorTable[info2.ColorIndex], 1.5);
                if (num3 < maxValue)
                {
                    colorIndex = info2.ColorIndex;
                    maxValue = num3;
                }
            }
            return colorIndex;
        }

        public int GetPaletteNearestColorIndex(Color color)
        {
            int index = this.GetExactColorIndex(color, 8, 0x3f);
            if (index != -1)
            {
                if (this.UseCustomPalette)
                {
                    this.usedEntries[index] ??= true;
                }
                return index;
            }
            if (this.UseCustomPalette)
            {
                int num2 = this.FindEntry();
                if (num2 != -1)
                {
                    this.isDefault = false;
                    this.colorTable[num2] = color;
                    this.usedEntries[num2] = true;
                    return num2;
                }
            }
            return this.GetNearestColorIndexCore(color, 8, 0x3f);
        }

        private void InitializeUsedEntries()
        {
            this.usedEntries = new bool[0x40];
            for (int i = 0; i < 0x40; i++)
            {
                this.usedEntries[i] = false;
            }
        }

        private bool IsCompatibleColors(Color x, Color y)
        {
            bool flag = (x.R == x.G) && (x.R == x.B);
            return (flag == ((y.R == y.G) && (y.R == y.B)));
        }

        public bool IsValidColorIndex(int index) => 
            this.colorTable.ContainsKey(index);

        public Color this[int index]
        {
            get => 
                this.colorTable[index];
            set => 
                this.colorTable[index] = value;
        }

        public Color DefaultForegroundColor
        {
            get => 
                this.colorTable[0x40];
            set => 
                this.colorTable[0x40] = value;
        }

        public Color DefaultBackgroundColor
        {
            get => 
                this.colorTable[0x41];
            set => 
                this.colorTable[0x41] = value;
        }

        public Color DefaultChartForegroundColor
        {
            get => 
                this.colorTable[0x4d];
            set => 
                this.colorTable[0x4d] = value;
        }

        public Color DefaultChartBackgroundColor
        {
            get => 
                this.colorTable[0x4e];
            set => 
                this.colorTable[0x4e] = value;
        }

        public Color ChartNeutralColor =>
            this.colorTable[0x4f];

        public Color ToolTipTextColor
        {
            get => 
                this.colorTable[0x51];
            set => 
                this.colorTable[0x51] = value;
        }

        public Color FontAutomaticColor
        {
            get => 
                this.colorTable[0x7fff];
            set => 
                this.colorTable[0x7fff] = value;
        }

        public bool IsDefault =>
            this.isDefault;

        public bool UseCustomPalette { get; set; }

        private class ColorDistanceInfo : IComparable<XlsPalette.ColorDistanceInfo>
        {
            public int CompareTo(XlsPalette.ColorDistanceInfo other) => 
                (this.Distance <= other.Distance) ? ((this.Distance >= other.Distance) ? 0 : -1) : 1;

            public double Distance { get; set; }

            public int ColorIndex { get; set; }
        }
    }
}

