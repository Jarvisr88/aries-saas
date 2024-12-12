namespace DevExpress.Office.Model
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class ColorModelInfo : ICloneable<ColorModelInfo>, ISupportsCopyFrom<ColorModelInfo>, ISupportsSizeOf
    {
        public const int DefaultColorIndex = -1;
        private DevExpress.Office.Model.ColorType colorType;
        private Color rgb;
        private ThemeColorIndex theme = ThemeColorIndex.None;
        private ThemeColorValues themeValue = ThemeColorValues.None;
        private int colorIndex = -1;
        private bool auto;
        private double tint;

        public ColorModelInfo Clone()
        {
            ColorModelInfo info = new ColorModelInfo();
            info.CopyFrom(this);
            return info;
        }

        public static int ConvertColorIndex(ColorModelInfoCache sourceCache, int sourceColorIndex, ColorModelInfoCache targetCache)
        {
            ColorModelInfo info = sourceCache[sourceColorIndex];
            return GetColorIndex(targetCache, info);
        }

        public void CopyFrom(ColorModelInfo value)
        {
            this.colorType = value.colorType;
            this.rgb = value.rgb;
            this.theme = value.theme;
            this.themeValue = value.themeValue;
            this.colorIndex = value.colorIndex;
            this.auto = value.auto;
            this.tint = value.Tint;
        }

        public static ColorModelInfo Create(ThemeColorIndex index) => 
            Create(index, 0.0);

        public static ColorModelInfo Create(Color rgb) => 
            new ColorModelInfo { Rgb = rgb };

        public static ColorModelInfo Create(int colorIndex) => 
            new ColorModelInfo { ColorIndex = colorIndex };

        public static ColorModelInfo Create(ThemeColorIndex index, double tint) => 
            new ColorModelInfo { 
                Theme = index,
                Tint = tint
            };

        public static ColorModelInfo CreateAutomatic() => 
            new ColorModelInfo { Auto = true };

        public override bool Equals(object obj)
        {
            ColorModelInfo info = obj as ColorModelInfo;
            return ((info != null) ? ((this.colorType == info.colorType) && ((this.rgb == info.Rgb) && ((this.theme == info.Theme) && ((this.colorIndex == info.ColorIndex) && ((this.auto == info.auto) && ((this.tint == info.Tint) && (this.themeValue == info.themeValue))))))) : false);
        }

        public static int GetAutomaticColorIndex(ColorModelInfoCache cache) => 
            GetColorIndex(cache, CreateAutomatic());

        public static int GetColorIndex(ColorModelInfoCache cache, ColorModelInfo info) => 
            cache.GetItemIndex(info);

        public static int GetColorIndex(ColorModelInfoCache cache, Color color)
        {
            ColorModelInfo info = Create(color);
            return GetColorIndex(cache, info);
        }

        public static int GetDefaultBackgroundColorIndex(ColorModelInfoCache cache) => 
            GetColorIndex(cache, Create(0x41));

        public static int GetDefaultForegroundColorIndex(ColorModelInfoCache cache) => 
            GetColorIndex(cache, Create(0x40));

        public override int GetHashCode() => 
            HashCodeHelper.Calculate((int) this.colorType, this.rgb.GetHashCode(), this.theme.GetHashCode(), this.colorIndex, this.auto.GetHashCode(), this.tint.GetHashCode(), (int) this.themeValue);

        private void RestoreDefaultValues()
        {
            this.theme = ThemeColorIndex.None;
            this.themeValue = ThemeColorValues.None;
            this.colorIndex = -1;
            this.auto = false;
            this.rgb = DXColor.Empty;
        }

        private void SetColorType(DevExpress.Office.Model.ColorType colorType)
        {
            this.RestoreDefaultValues();
            this.colorType = colorType;
        }

        public int SizeOf() => 
            ObjectSizeHelper.CalculateApproxObjectSize32(this, true);

        public Color ToRgb(Palette palette, ThemeDrawingColorCollection colors)
        {
            Color empty = DXColor.Empty;
            switch (this.colorType)
            {
                case DevExpress.Office.Model.ColorType.Rgb:
                    empty = this.Rgb;
                    break;

                case DevExpress.Office.Model.ColorType.Theme:
                    if (this.Theme != ThemeColorIndex.None)
                    {
                        empty = colors.GetColor(this.Theme);
                    }
                    break;

                case DevExpress.Office.Model.ColorType.Index:
                    if (this.ColorIndex != -1)
                    {
                        empty = palette[this.ColorIndex];
                        if (empty.A == 0)
                        {
                            empty = DXColor.FromArgb(0xff, empty.R, empty.G, empty.B);
                        }
                    }
                    break;

                default:
                    break;
            }
            return ColorHSL.CalculateColorRGB(empty, this.tint);
        }

        public DevExpress.Office.Model.ColorType ColorType =>
            this.colorType;

        public Color Rgb
        {
            get => 
                this.rgb;
            set
            {
                if (this.ColorType != DevExpress.Office.Model.ColorType.Rgb)
                {
                    this.SetColorType(DevExpress.Office.Model.ColorType.Rgb);
                }
                if (this.rgb != value)
                {
                    this.rgb = value;
                }
            }
        }

        public ThemeColorIndex Theme
        {
            get => 
                this.theme;
            set
            {
                if (this.ColorType != DevExpress.Office.Model.ColorType.Theme)
                {
                    this.SetColorType(DevExpress.Office.Model.ColorType.Theme);
                }
                if (this.theme != value)
                {
                    this.theme = value;
                }
            }
        }

        public ThemeColorValues ThemeValue
        {
            get => 
                this.themeValue;
            set
            {
                if (this.ColorType != DevExpress.Office.Model.ColorType.Theme)
                {
                    this.SetColorType(DevExpress.Office.Model.ColorType.Theme);
                }
                if (this.themeValue != value)
                {
                    this.themeValue = value;
                }
            }
        }

        public int ColorIndex
        {
            get => 
                this.colorIndex;
            set
            {
                if (this.ColorType != DevExpress.Office.Model.ColorType.Index)
                {
                    this.SetColorType(DevExpress.Office.Model.ColorType.Index);
                }
                if (this.colorIndex != value)
                {
                    this.colorIndex = value;
                }
            }
        }

        public bool Auto
        {
            get => 
                this.auto;
            set
            {
                if (this.ColorType != DevExpress.Office.Model.ColorType.Auto)
                {
                    this.SetColorType(DevExpress.Office.Model.ColorType.Auto);
                }
                if (this.auto != value)
                {
                    this.auto = value;
                }
            }
        }

        public double Tint
        {
            get => 
                this.tint;
            set
            {
                if (Math.Abs(value) > 1.0)
                {
                    Exceptions.ThrowInternalException();
                }
                this.tint = value;
            }
        }

        public bool IsEmpty =>
            DXColor.IsTransparentOrEmpty(this.Rgb) && (this.ColorType == DevExpress.Office.Model.ColorType.Rgb);
    }
}

