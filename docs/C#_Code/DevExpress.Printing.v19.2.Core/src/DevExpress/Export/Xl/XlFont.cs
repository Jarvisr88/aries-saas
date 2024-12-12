namespace DevExpress.Export.Xl
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class XlFont : XlFontBase, ISupportsCopyFrom<XlFont>
    {
        private XlColor color = XlColor.FromTheme(XlThemeColor.Dark1, 0.0);

        public XlFont()
        {
            base.Name = "Calibri";
            base.Size = 11.0;
            this.FontFamily = XlFontFamily.Swiss;
            base.SchemeStyle = XlFontSchemeStyles.Minor;
        }

        public static XlFont BodyFont() => 
            new XlFont { 
                Name = "Calibri",
                Size = 11.0,
                FontFamily = XlFontFamily.Swiss,
                SchemeStyle = XlFontSchemeStyles.Minor,
                Color = XlColor.FromTheme(XlThemeColor.Dark1, 0.0)
            };

        public XlFont Clone()
        {
            XlFont font = new XlFont();
            font.CopyFrom(this);
            return font;
        }

        public void CopyFrom(XlFont value)
        {
            Guard.ArgumentNotNull(value, "value");
            base.CopyFrom(value);
            this.FontFamily = value.FontFamily;
            this.Color = value.Color;
        }

        public static XlFont CustomFont(string fontName) => 
            CustomFont(fontName, 11.0, XlColor.FromTheme(XlThemeColor.Dark1, 0.0));

        public static XlFont CustomFont(string fontName, double size) => 
            CustomFont(fontName, size, XlColor.FromTheme(XlThemeColor.Dark1, 0.0));

        public static XlFont CustomFont(string fontName, double size, XlColor color) => 
            new XlFont { 
                Name = fontName,
                Size = size,
                Color = color,
                SchemeStyle = XlFontSchemeStyles.None
            };

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
            {
                return false;
            }
            XlFont font = obj as XlFont;
            return ((font != null) ? ((this.FontFamily == font.FontFamily) && this.color.Equals(font.Color)) : false);
        }

        public override int GetHashCode() => 
            (base.GetHashCode() ^ this.FontFamily.GetHashCode()) ^ this.Color.GetHashCode();

        public static XlFont HeadingsFont() => 
            new XlFont { 
                Name = "Cambria",
                Size = 11.0,
                FontFamily = XlFontFamily.Roman,
                SchemeStyle = XlFontSchemeStyles.Major,
                Color = XlColor.FromTheme(XlThemeColor.Dark1, 0.0)
            };

        public XlFontFamily FontFamily { get; set; }

        public XlColor Color
        {
            get => 
                this.color;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.color = empty;
            }
        }
    }
}

