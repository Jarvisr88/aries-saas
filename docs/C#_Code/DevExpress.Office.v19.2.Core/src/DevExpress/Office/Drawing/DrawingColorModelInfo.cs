namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;

    public class DrawingColorModelInfo : ICloneable<DrawingColorModelInfo>, ISupportsCopyFrom<DrawingColorModelInfo>, ISupportsSizeOf, ISupportsBinaryReadWrite
    {
        private static Dictionary<SystemColorValues, Color> systemColorTable = CreateSystemColorTable();
        private DrawingColorType colorType;
        private Color rgb;
        private SystemColorValues systemColor;
        private SchemeColorValues schemeColor;
        private string preset;
        private ScRGBColor scRgb;
        private ColorHSL hsl;

        public DrawingColorModelInfo()
        {
            this.RestoreDefaultValues();
        }

        public DrawingColorModelInfo Clone()
        {
            DrawingColorModelInfo info = new DrawingColorModelInfo();
            info.CopyFrom(this);
            return info;
        }

        public void CopyFrom(DrawingColorModelInfo value)
        {
            Guard.ArgumentNotNull(value, "DrawingColorInfo");
            this.colorType = value.colorType;
            this.rgb = value.rgb;
            this.schemeColor = value.schemeColor;
            this.systemColor = value.systemColor;
            this.preset = value.preset;
            this.scRgb = value.scRgb;
            this.hsl = value.hsl;
        }

        public static DrawingColorModelInfo CreateARGB(Color argb) => 
            new DrawingColorModelInfo { Rgb = argb };

        public static DrawingColorModelInfo CreateHSL(int hue, int saturation, int luminance) => 
            new DrawingColorModelInfo { Hsl = new ColorHSL(hue, saturation, luminance) };

        public static DrawingColorModelInfo CreatePreset(string preset) => 
            new DrawingColorModelInfo { Preset = preset };

        internal static DrawingColorModelInfo CreateRGB(Color rgb) => 
            new DrawingColorModelInfo { Rgb = DXColor.FromArgb(0xff, rgb.R, rgb.G, rgb.B) };

        public static DrawingColorModelInfo CreateScheme(SchemeColorValues schemeColor) => 
            new DrawingColorModelInfo { SchemeColor = schemeColor };

        public static DrawingColorModelInfo CreateScRgb(int scR, int scG, int scB) => 
            new DrawingColorModelInfo { ScRgb = new ScRGBColor(scR, scG, scB) };

        public static DrawingColorModelInfo CreateSystem(SystemColorValues systemColor) => 
            new DrawingColorModelInfo { SystemColor = systemColor };

        private static Dictionary<SystemColorValues, Color> CreateSystemColorTable() => 
            new Dictionary<SystemColorValues, Color> { 
                { 
                    SystemColorValues.Sc3dDkShadow,
                    DXSystemColors.ControlDarkDark
                },
                { 
                    SystemColorValues.Sc3dLight,
                    DXSystemColors.ControlLightLight
                },
                { 
                    SystemColorValues.ScActiveBorder,
                    DXSystemColors.ActiveBorder
                },
                { 
                    SystemColorValues.ScActiveCaption,
                    DXSystemColors.ActiveCaption
                },
                { 
                    SystemColorValues.ScAppWorkspace,
                    DXSystemColors.AppWorkspace
                },
                { 
                    SystemColorValues.ScBackground,
                    DXSystemColors.Desktop
                },
                { 
                    SystemColorValues.ScBtnFace,
                    DXSystemColors.Control
                },
                { 
                    SystemColorValues.ScBtnHighlight,
                    DXSystemColors.ControlLight
                },
                { 
                    SystemColorValues.ScBtnShadow,
                    DXSystemColors.ControlDark
                },
                { 
                    SystemColorValues.ScBtnText,
                    DXSystemColors.ControlText
                },
                { 
                    SystemColorValues.ScCaptionText,
                    DXSystemColors.ActiveCaptionText
                },
                { 
                    SystemColorValues.ScGradientActiveCaption,
                    DXSystemColors.GradientActiveCaption
                },
                { 
                    SystemColorValues.ScGradientInactiveCaption,
                    DXSystemColors.GradientInactiveCaption
                },
                { 
                    SystemColorValues.ScGrayText,
                    DXSystemColors.GrayText
                },
                { 
                    SystemColorValues.ScHighlight,
                    DXSystemColors.Highlight
                },
                { 
                    SystemColorValues.ScHighlightText,
                    DXSystemColors.HighlightText
                },
                { 
                    SystemColorValues.ScHotLight,
                    DXSystemColors.HotTrack
                },
                { 
                    SystemColorValues.ScInactiveBorder,
                    DXSystemColors.InactiveBorder
                },
                { 
                    SystemColorValues.ScInactiveCaption,
                    DXSystemColors.InactiveCaption
                },
                { 
                    SystemColorValues.ScInactiveCaptionText,
                    DXSystemColors.InactiveCaptionText
                },
                { 
                    SystemColorValues.ScInfoBk,
                    DXSystemColors.Info
                },
                { 
                    SystemColorValues.ScInfoText,
                    DXSystemColors.InfoText
                },
                { 
                    SystemColorValues.ScMenu,
                    DXSystemColors.Menu
                },
                { 
                    SystemColorValues.ScMenuBar,
                    DXSystemColors.MenuBar
                },
                { 
                    SystemColorValues.ScMenuHighlight,
                    DXSystemColors.MenuHighlight
                },
                { 
                    SystemColorValues.ScMenuText,
                    DXSystemColors.MenuText
                },
                { 
                    SystemColorValues.ScScrollBar,
                    DXSystemColors.ScrollBar
                },
                { 
                    SystemColorValues.ScWindow,
                    DXSystemColors.Window
                },
                { 
                    SystemColorValues.ScWindowFrame,
                    DXSystemColors.WindowFrame
                },
                { 
                    SystemColorValues.ScWindowText,
                    DXSystemColors.WindowText
                }
            };

        void ISupportsBinaryReadWrite.Read(BinaryReader reader)
        {
            this.colorType = (DrawingColorType) reader.ReadInt32();
            this.rgb = this.ReadColor(reader);
            this.schemeColor = (SchemeColorValues) reader.ReadInt32();
            this.systemColor = (SystemColorValues) reader.ReadInt32();
            this.preset = reader.ReadString();
            int scR = reader.ReadInt32();
            this.scRgb = new ScRGBColor(scR, reader.ReadInt32(), reader.ReadInt32());
            float hue = reader.ReadSingle();
            this.hsl = new ColorHSL(hue, reader.ReadSingle(), reader.ReadSingle());
        }

        void ISupportsBinaryReadWrite.Write(BinaryWriter writer)
        {
            writer.Write((int) this.colorType);
            this.WriteColor(writer, this.rgb);
            writer.Write((int) this.schemeColor);
            writer.Write((int) this.systemColor);
            writer.Write(this.preset);
            writer.Write(this.scRgb.ScR);
            writer.Write(this.scRgb.ScG);
            writer.Write(this.scRgb.ScB);
            writer.Write(this.hsl.FloatHue);
            writer.Write(this.hsl.FloatLuminance);
            writer.Write(this.hsl.FloatSaturation);
        }

        public override bool Equals(object obj)
        {
            DrawingColorModelInfo info = obj as DrawingColorModelInfo;
            return ((info != null) ? ((this.colorType == info.colorType) && ((this.rgb == info.Rgb) && ((this.schemeColor == info.schemeColor) && ((this.systemColor == info.systemColor) && ((this.preset == info.preset) && (this.scRgb.Equals(info.scRgb) && this.hsl.Equals(info.hsl))))))) : false);
        }

        public override int GetHashCode() => 
            (((((((int) this.colorType) ^ this.rgb.GetHashCode()) ^ this.schemeColor.GetHashCode()) ^ this.systemColor.GetHashCode()) ^ this.preset.GetHashCode()) ^ this.scRgb.GetHashCode()) ^ this.hsl.GetHashCode();

        private Color GetRgbFromPreset()
        {
            string key = char.ToUpper(this.preset[0]).ToString() + this.preset.Substring(1);
            return (!DXColor.PredefinedColors.ContainsKey(key) ? DXColor.Empty : DXColor.PredefinedColors[key]);
        }

        private Color GetRgbFromSchemeColor(ThemeDrawingColorCollection colors, Color styleColor) => 
            (this.SchemeColor != SchemeColorValues.Style) ? ((this.SchemeColor == SchemeColorValues.Empty) ? DXColor.Empty : colors.GetColor(this.SchemeColor)) : styleColor;

        private Color GetRgbFromSystemColor() => 
            (this.SystemColor == SystemColorValues.Empty) ? DXColor.Empty : systemColorTable[this.SystemColor];

        private Color ReadColor(BinaryReader reader)
        {
            byte num = reader.ReadByte();
            return ((num != 0) ? ((num != 1) ? ((num != 2) ? Color.FromArgb(reader.ReadInt32()) : Color.FromName(reader.ReadString())) : Color.FromKnownColor((KnownColor) reader.ReadInt16())) : DXColor.Empty);
        }

        private void RestoreDefaultValues()
        {
            this.rgb = DXColor.Empty;
            this.schemeColor = SchemeColorValues.Empty;
            this.systemColor = SystemColorValues.Empty;
            this.preset = string.Empty;
            this.scRgb = ScRGBColor.DefaultValue;
            this.hsl = ColorHSL.DefaultValue;
        }

        private void SetColorType(DrawingColorType colorType)
        {
            this.RestoreDefaultValues();
            this.colorType = colorType;
        }

        public int SizeOf() => 
            DXMarshal.SizeOf(base.GetType());

        public static Color SRgbToRgb(string hexColor) => 
            DXColor.FromArgb(int.Parse(hexColor.Substring(0, 2), NumberStyles.HexNumber), int.Parse(hexColor.Substring(2, 2), NumberStyles.HexNumber), int.Parse(hexColor.Substring(4, 2), NumberStyles.HexNumber));

        public Color ToRgb(ThemeDrawingColorCollection colors) => 
            this.ToRgb(colors, DXColor.Empty);

        public Color ToRgb(ThemeDrawingColorCollection colors, Color styleColor)
        {
            switch (this.colorType)
            {
                case DrawingColorType.System:
                    return this.GetRgbFromSystemColor();

                case DrawingColorType.Scheme:
                    return this.GetRgbFromSchemeColor(colors, styleColor);

                case DrawingColorType.Preset:
                    return this.GetRgbFromPreset();

                case DrawingColorType.ScRgb:
                    return this.scRgb.ToRgb();

                case DrawingColorType.Hsl:
                    return this.hsl.ToRgb();
            }
            return this.rgb;
        }

        private void WriteColor(BinaryWriter writer, Color color)
        {
            if (color.IsEmpty)
            {
                writer.Write((byte) 0);
            }
            else if (color.IsKnownColor)
            {
                writer.Write((byte) 1);
                writer.Write((short) color.ToKnownColor());
            }
            else if (color.IsNamedColor)
            {
                writer.Write((byte) 2);
                writer.Write(color.Name);
            }
            else
            {
                writer.Write((byte) 3);
                writer.Write(color.ToArgb());
            }
        }

        public DrawingColorType ColorType =>
            this.colorType;

        public Color Rgb
        {
            get => 
                this.rgb;
            set
            {
                if (this.ColorType != DrawingColorType.Rgb)
                {
                    this.SetColorType(DrawingColorType.Rgb);
                }
                if (this.rgb != value)
                {
                    this.rgb = value;
                }
            }
        }

        public SystemColorValues SystemColor
        {
            get => 
                this.systemColor;
            set
            {
                if (this.ColorType != DrawingColorType.System)
                {
                    this.SetColorType(DrawingColorType.System);
                }
                if (this.systemColor != value)
                {
                    this.systemColor = value;
                }
            }
        }

        public SchemeColorValues SchemeColor
        {
            get => 
                this.schemeColor;
            set
            {
                if (this.ColorType != DrawingColorType.Scheme)
                {
                    this.SetColorType(DrawingColorType.Scheme);
                }
                if (this.schemeColor != value)
                {
                    this.schemeColor = value;
                }
            }
        }

        public ColorHSL Hsl
        {
            get => 
                this.hsl;
            set
            {
                if (this.ColorType != DrawingColorType.Hsl)
                {
                    this.SetColorType(DrawingColorType.Hsl);
                }
                if (!this.hsl.Equals(value))
                {
                    this.hsl = value;
                }
            }
        }

        public string Preset
        {
            get => 
                this.preset;
            set
            {
                if (this.ColorType != DrawingColorType.Preset)
                {
                    this.SetColorType(DrawingColorType.Preset);
                }
                if ((this.preset != value) && !string.IsNullOrEmpty(value))
                {
                    this.preset = value;
                }
            }
        }

        public ScRGBColor ScRgb
        {
            get => 
                this.scRgb;
            set
            {
                if (this.ColorType != DrawingColorType.ScRgb)
                {
                    this.SetColorType(DrawingColorType.ScRgb);
                }
                if (!this.scRgb.Equals(value))
                {
                    this.scRgb = value;
                }
            }
        }

        public bool IsEmpty =>
            DXColor.IsTransparentOrEmpty(this.Rgb) && (this.ColorType == DrawingColorType.Rgb);
    }
}

