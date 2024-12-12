namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    public class XlColor
    {
        private static readonly XlColor empty = new XlColor(XlColorType.Empty);
        private static readonly XlColor auto = new XlColor(XlColorType.Auto);
        private static readonly XlColor defaultForeground = new XlColor(XlColorType.Indexed, 0x40);
        private static readonly XlColor defaultBackground = new XlColor(XlColorType.Indexed, 0x41);
        private static Dictionary<XlThemeColor, Color> themeColorTable = CreateThemeColorTable();
        private static Dictionary<XlThemeColor, Color> theme2013ColorTable = CreateTheme2013ColorTable();
        private XlColorType colorType;
        private int argb;
        private XlThemeColor themeColor;
        private double tint;

        protected XlColor(XlColorType colorType)
        {
            this.colorType = colorType;
            this.argb = 0;
            this.themeColor = XlThemeColor.None;
            this.tint = 0.0;
        }

        protected XlColor(int argb)
        {
            this.colorType = XlColorType.Rgb;
            this.argb = argb;
            this.themeColor = XlThemeColor.None;
            this.tint = 0.0;
        }

        protected XlColor(XlColorType colorType, int colorIndex)
        {
            this.colorType = colorType;
            this.argb = colorIndex;
            this.themeColor = XlThemeColor.None;
            this.tint = 0.0;
        }

        protected XlColor(XlThemeColor themeColor, double tint)
        {
            this.colorType = XlColorType.Theme;
            this.argb = 0;
            this.themeColor = themeColor;
            this.tint = tint;
        }

        private unsafe Color ApplyTint(Color value)
        {
            if (this.tint == 0.0)
            {
                return value;
            }
            float num3 = (Math.Max(Math.Max(value.R, value.G), value.B) == Math.Min(Math.Min(value.R, value.G), value.B)) ? 0.6666667f : (value.GetHue() / 360f);
            float saturation = value.GetSaturation();
            float brightness = value.GetBrightness();
            if (this.tint < 0.0)
            {
                brightness *= 1f + ((float) this.tint);
            }
            if (this.tint > 0.0)
            {
                brightness = (brightness * (1f - ((float) this.tint))) + ((float) this.tint);
            }
            float num6 = (brightness < 0.5) ? (brightness * (1f + saturation)) : ((brightness + saturation) - (brightness * saturation));
            float num7 = (2f * brightness) - num6;
            float[] numArray = new float[] { num3 + 0.3333333f, num3, num3 - 0.3333333f };
            for (int i = 0; i < 3; i++)
            {
                if (numArray[i] < 0f)
                {
                    float* singlePtr1 = &(numArray[i]);
                    singlePtr1[0]++;
                }
                if (numArray[i] > 1f)
                {
                    float* singlePtr2 = &(numArray[i]);
                    singlePtr2[0]--;
                }
                numArray[i] = ((6f * numArray[i]) >= 1f) ? ((((6f * numArray[i]) < 1f) || ((6f * numArray[i]) >= 3f)) ? ((((6f * numArray[i]) < 3f) || ((6f * numArray[i]) >= 4f)) ? num7 : (num7 + ((num6 - num7) * (4f - (6f * numArray[i]))))) : num6) : (num7 + (((num6 - num7) * 6f) * numArray[i]));
            }
            return DXColor.FromArgb(0xff, this.ToIntValue(numArray[0]), this.ToIntValue(numArray[1]), this.ToIntValue(numArray[2]));
        }

        internal Color ConvertToRgb(XlDocumentTheme theme) => 
            (this.ColorType != XlColorType.Rgb) ? ((this.ColorType != XlColorType.Theme) ? DXColor.Empty : this.ApplyTint((theme == XlDocumentTheme.Office2010) ? themeColorTable[this.themeColor] : theme2013ColorTable[this.themeColor])) : DXColor.FromArgb(this.argb);

        private static Dictionary<XlThemeColor, Color> CreateTheme2013ColorTable() => 
            new Dictionary<XlThemeColor, Color> { 
                { 
                    XlThemeColor.Light1,
                    DXColor.FromArgb(0xff, 0xff, 0xff, 0xff)
                },
                { 
                    XlThemeColor.Dark1,
                    DXColor.FromArgb(0xff, 0, 0, 0)
                },
                { 
                    XlThemeColor.Light2,
                    DXColor.FromArgb(0xff, 0xe7, 230, 230)
                },
                { 
                    XlThemeColor.Dark2,
                    DXColor.FromArgb(0xff, 0x44, 0x54, 0x6a)
                },
                { 
                    XlThemeColor.Accent1,
                    DXColor.FromArgb(0xff, 0x5b, 0x9b, 0xd5)
                },
                { 
                    XlThemeColor.Accent2,
                    DXColor.FromArgb(0xff, 0xed, 0x7d, 0x31)
                },
                { 
                    XlThemeColor.Accent3,
                    DXColor.FromArgb(0xff, 0xa5, 0xa5, 0xa5)
                },
                { 
                    XlThemeColor.Accent4,
                    DXColor.FromArgb(0xff, 0xff, 0xc0, 0)
                },
                { 
                    XlThemeColor.Accent5,
                    DXColor.FromArgb(0xff, 0x44, 0x72, 0xc4)
                },
                { 
                    XlThemeColor.Accent6,
                    DXColor.FromArgb(0xff, 0x70, 0xad, 0x47)
                },
                { 
                    XlThemeColor.Hyperlink,
                    DXColor.FromArgb(0xff, 5, 0x63, 0xc1)
                },
                { 
                    XlThemeColor.FollowedHyperlink,
                    DXColor.FromArgb(0xff, 0x95, 0x4f, 0x72)
                }
            };

        private static Dictionary<XlThemeColor, Color> CreateThemeColorTable() => 
            new Dictionary<XlThemeColor, Color> { 
                { 
                    XlThemeColor.Light1,
                    DXColor.FromArgb(0xff, 0xff, 0xff, 0xff)
                },
                { 
                    XlThemeColor.Dark1,
                    DXColor.FromArgb(0xff, 0, 0, 0)
                },
                { 
                    XlThemeColor.Light2,
                    DXColor.FromArgb(0xff, 0xee, 0xec, 0xe1)
                },
                { 
                    XlThemeColor.Dark2,
                    DXColor.FromArgb(0xff, 0x1f, 0x49, 0x7d)
                },
                { 
                    XlThemeColor.Accent1,
                    DXColor.FromArgb(0xff, 0x4f, 0x81, 0xbd)
                },
                { 
                    XlThemeColor.Accent2,
                    DXColor.FromArgb(0xff, 0xc0, 80, 0x4d)
                },
                { 
                    XlThemeColor.Accent3,
                    DXColor.FromArgb(0xff, 0x9b, 0xbb, 0x59)
                },
                { 
                    XlThemeColor.Accent4,
                    DXColor.FromArgb(0xff, 0x80, 100, 0xa2)
                },
                { 
                    XlThemeColor.Accent5,
                    DXColor.FromArgb(0xff, 0x4b, 0xac, 0xc6)
                },
                { 
                    XlThemeColor.Accent6,
                    DXColor.FromArgb(0xff, 0xf7, 150, 70)
                },
                { 
                    XlThemeColor.Hyperlink,
                    DXColor.FromArgb(0xff, 0, 0, 0xff)
                },
                { 
                    XlThemeColor.FollowedHyperlink,
                    DXColor.FromArgb(0xff, 0x80, 0, 0x80)
                }
            };

        public override bool Equals(object obj)
        {
            XlColor color = obj as XlColor;
            return ((color != null) ? ((this.colorType == color.colorType) ? ((this.colorType != XlColorType.Rgb) ? ((this.colorType != XlColorType.Theme) || ((this.themeColor == color.themeColor) && (this.tint == color.tint))) : (this.argb == color.argb)) : false) : false);
        }

        private int FixIntValue(int value) => 
            (value < 0) ? 0 : ((value > 0xff) ? 0xff : value);

        public static XlColor FromArgb(int argb)
        {
            argb |= -16777216;
            return new XlColor(argb);
        }

        public static XlColor FromArgb(byte red, byte green, byte blue) => 
            new XlColor((((red << 0x10) | (green << 8)) | blue) | -16777216);

        public static XlColor FromTheme(XlThemeColor themeColor, double tint)
        {
            if (themeColor == XlThemeColor.None)
            {
                throw new ArgumentException("themeColor");
            }
            if ((tint < -1.0) || (tint > 1.0))
            {
                throw new ArgumentOutOfRangeException("tint out of range -1...1");
            }
            return new XlColor(themeColor, tint);
        }

        public override int GetHashCode()
        {
            int hashCode = this.ColorType.GetHashCode();
            if (this.colorType == XlColorType.Rgb)
            {
                hashCode ^= this.argb;
            }
            else if (this.colorType == XlColorType.Theme)
            {
                hashCode = (hashCode ^ this.themeColor.GetHashCode()) ^ this.tint.GetHashCode();
            }
            return hashCode;
        }

        public static implicit operator Color(XlColor value) => 
            value.Rgb;

        public static implicit operator XlColor(Color value) => 
            ((value == DXColor.Empty) || (value == DXColor.Transparent)) ? Empty : new XlColor(value.ToArgb() | -16777216);

        private int ToIntValue(float value) => 
            this.FixIntValue((int) Math.Round((double) (255f * value), 0));

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.colorType.ToString());
            if (this.colorType == XlColorType.Rgb)
            {
                builder.Append(": ");
                builder.Append(this.argb.ToString("X8"));
            }
            else if (this.colorType == XlColorType.Theme)
            {
                builder.Append(": ");
                builder.Append(this.themeColor.ToString());
                builder.Append(", ");
                builder.Append(this.tint.ToString());
            }
            return builder.ToString();
        }

        public static XlColor Empty =>
            empty;

        public static XlColor Auto =>
            auto;

        public static XlColor DefaultForeground =>
            defaultForeground;

        public static XlColor DefaultBackground =>
            defaultBackground;

        public XlColorType ColorType =>
            this.colorType;

        public Color Rgb =>
            (this.ColorType != XlColorType.Rgb) ? ((this.ColorType != XlColorType.Theme) ? DXColor.Empty : this.ApplyTint(themeColorTable[this.themeColor])) : DXColor.FromArgb(this.argb);

        public XlThemeColor ThemeColor =>
            this.themeColor;

        public double Tint =>
            this.tint;

        public bool IsEmpty =>
            this.colorType == XlColorType.Empty;

        public bool IsAutoOrEmpty =>
            (this.colorType == XlColorType.Auto) || (this.colorType == XlColorType.Empty);

        public int ColorIndex =>
            (this.ColorType != XlColorType.Indexed) ? -1 : this.argb;
    }
}

