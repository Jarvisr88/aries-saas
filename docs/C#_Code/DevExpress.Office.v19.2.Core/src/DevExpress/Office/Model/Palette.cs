namespace DevExpress.Office.Model
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class Palette : ICloneable<Palette>, ISupportsCopyFrom<Palette>
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
        private bool isCustomIndexedColorTable;

        public Palette()
        {
            this.Reset();
        }

        public Palette Clone()
        {
            Palette palette = new Palette();
            palette.CopyFrom(this);
            return palette;
        }

        public void CopyFrom(Palette value)
        {
            this.colorTable = new Dictionary<int, Color>();
            foreach (KeyValuePair<int, Color> pair in value.colorTable)
            {
                this.colorTable.Add(pair.Key, pair.Value);
            }
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

        private Color GetColor(int index)
        {
            Color color;
            if (!this.colorTable.TryGetValue(index, out color))
            {
                Exceptions.ThrowInternalException();
            }
            return color;
        }

        private double GetColorDistance(Color x, Color y, double rgbWeight) => 
            ColorDifference.HSB(x, y) + (ColorDifference.RGB(x, y) * rgbWeight);

        public int GetColorIndex(Color color)
        {
            int num = this.GetExactColorIndex(color, 0, 0x7fff);
            if (num != -1)
            {
                return num;
            }
            if (color.A == 0xff)
            {
                color = DXColor.FromArgb(0, color.R, color.G, color.B);
                num = this.GetExactColorIndex(color, 0, 0x7fff);
                if (num != -1)
                {
                    return num;
                }
            }
            return 0x40;
        }

        public int GetColorIndex(ThemeDrawingColorCollection colors, ColorModelInfo colorInfo, bool foreground)
        {
            if (colorInfo.ColorType == ColorType.Auto)
            {
                return (foreground ? 0x40 : 0x41);
            }
            if (colorInfo.ColorType == ColorType.Index)
            {
                if (!this.IsValidColorIndex(colorInfo.ColorIndex))
                {
                    return (foreground ? 0x40 : 0x41);
                }
                if (colorInfo.ColorIndex >= 8)
                {
                    return colorInfo.ColorIndex;
                }
            }
            Color color = colorInfo.ToRgb(this, colors);
            return this.GetPaletteNearestColorIndex(color);
        }

        public int GetColorIndex(ThemeDrawingColorCollection colors, ColorModelInfoCache cache, int index, bool foreground)
        {
            if (!cache.IsIndexValid(index))
            {
                return (foreground ? 0x40 : 0x41);
            }
            ColorModelInfo info = cache[index];
            if (cache.DefaultItem == info)
            {
                return (foreground ? 0x40 : 0x41);
            }
            if (info.ColorType == ColorType.Auto)
            {
                return (foreground ? 0x40 : 0x41);
            }
            if (info.ColorType == ColorType.Index)
            {
                if (!this.IsValidColorIndex(info.ColorIndex))
                {
                    return (foreground ? 0x40 : 0x41);
                }
                if (info.ColorIndex <= 0x41)
                {
                    return info.ColorIndex;
                }
            }
            Color color = info.ToRgb(this, colors);
            return this.GetPaletteNearestColorIndex(color);
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

        public int GetFontColorIndex(ThemeDrawingColorCollection colors, ColorModelInfo colorInfo)
        {
            if (colorInfo.ColorType == ColorType.Auto)
            {
                return 0x7fff;
            }
            if (colorInfo.ColorType == ColorType.Index)
            {
                return (this.IsValidColorIndex(colorInfo.ColorIndex) ? colorInfo.ColorIndex : 0x7fff);
            }
            Color color = colorInfo.ToRgb(this, colors);
            return this.GetPaletteNearestColorIndex(color);
        }

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
            int num = this.GetExactColorIndex(color, 8, 0x3f);
            return ((num == -1) ? this.GetNearestColorIndexCore(color, 8, 0x3f) : num);
        }

        private bool IsCompatibleColors(Color x, Color y)
        {
            bool flag = (x.R == x.G) && (x.R == x.B);
            return (flag == ((y.R == y.G) && (y.R == y.B)));
        }

        public bool IsEqualColors(Palette other)
        {
            Guard.ArgumentNotNull(other, "other");
            for (int i = 0; i < 0x40; i++)
            {
                if (this[i] != other[i])
                {
                    return false;
                }
            }
            return ((this[0x4d] == other[0x4d]) && ((this[0x4e] == other[0x4e]) && ((this[0x4f] == other[0x4f]) && (this[0x7fff] == other[0x7fff]))));
        }

        public bool IsValidColorIndex(int index) => 
            this.colorTable.ContainsKey(index);

        public void Reset()
        {
            this.colorTable = this.CreateDefaultColorTable();
        }

        private void SetColor(int index, Color value)
        {
            if (!this.colorTable.ContainsKey(index))
            {
                Exceptions.ThrowInternalException();
            }
            else if (this.colorTable[index] != value)
            {
                this.IsCustomIndexedColorTable = true;
                this.colorTable[index] = value;
            }
        }

        public Color this[int index]
        {
            get => 
                this.GetColor(index);
            set => 
                this.SetColor(index, value);
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

        public bool IsCustomIndexedColorTable
        {
            get => 
                this.isCustomIndexedColorTable;
            private set => 
                this.isCustomIndexedColorTable = value;
        }

        private class ColorDistanceInfo : IComparable<Palette.ColorDistanceInfo>
        {
            public int CompareTo(Palette.ColorDistanceInfo other) => 
                (this.Distance <= other.Distance) ? ((this.Distance >= other.Distance) ? 0 : -1) : 1;

            public double Distance { get; set; }

            public int ColorIndex { get; set; }
        }
    }
}

