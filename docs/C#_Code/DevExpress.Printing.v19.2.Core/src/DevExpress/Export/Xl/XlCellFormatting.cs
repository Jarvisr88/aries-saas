namespace DevExpress.Export.Xl
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class XlCellFormatting : XlFormatting, ISupportsCopyFrom<XlCellFormatting>
    {
        public XlCellFormatting Clone()
        {
            XlCellFormatting formatting = new XlCellFormatting();
            formatting.CopyFrom(this);
            return formatting;
        }

        public void CopyFrom(XlCellFormatting other)
        {
            if (other == null)
            {
                base.Font = null;
                base.Fill = null;
                base.Alignment = null;
                base.NetFormatString = null;
                base.IsDateTimeFormatString = false;
                base.NumberFormat = null;
                base.Border = null;
            }
            else
            {
                base.Font = CopyObject<XlFont>(other.Font);
                base.Fill = CopyObject<XlFill>(other.Fill);
                base.Alignment = CopyObject<XlCellAlignment>(other.Alignment);
                base.Border = CopyObject<XlBorder>(other.Border);
                base.NetFormatString = other.NetFormatString;
                base.IsDateTimeFormatString = other.IsDateTimeFormatString;
                base.NumberFormat = other.NumberFormat;
            }
        }

        internal static bool EqualFonts(XlCellFormatting first, XlCellFormatting second) => 
            !ReferenceEquals(first, second) ? ((first != null) && ((second != null) && EqualObjects<XlFont>(first.Font, second.Font))) : true;

        private static bool EqualObjects<T>(T first, T second) where T: class => 
            (first != second) ? ((first != null) ? first.Equals(second) : false) : true;

        public static bool Equals(XlCellFormatting first, XlCellFormatting second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }
            if ((first != null) && (second != null))
            {
                if (!EqualObjects<XlFill>(first.Fill, second.Fill))
                {
                    return false;
                }
                if (!EqualObjects<XlFont>(first.Font, second.Font))
                {
                    return false;
                }
                if (!EqualObjects<XlBorder>(first.Border, second.Border))
                {
                    return false;
                }
                if (!EqualObjects<XlCellAlignment>(first.Alignment, second.Alignment))
                {
                    return false;
                }
                if (!EqualObjects<XlNumberFormat>(first.NumberFormat, second.NumberFormat))
                {
                    return false;
                }
                if ((string.IsNullOrEmpty(first.NetFormatString) && string.IsNullOrEmpty(second.NetFormatString)) || string.Equals(first.NetFormatString, second.NetFormatString, StringComparison.Ordinal))
                {
                    return (first.IsDateTimeFormatString == second.IsDateTimeFormatString);
                }
            }
            return false;
        }

        public static XlCellFormatting FromNetFormat(string formatString, bool isDateTimeFormat) => 
            new XlCellFormatting { 
                NetFormatString = formatString,
                IsDateTimeFormatString = isDateTimeFormat
            };

        public static XlCellFormatting Merge(XlCellFormatting target, XlCellFormatting source)
        {
            if (target == null)
            {
                return CopyObject<XlCellFormatting>(source);
            }
            target.MergeWith(source);
            return target;
        }

        public void MergeWith(XlCellFormatting other)
        {
            if (other != null)
            {
                if (other.Font != null)
                {
                    base.Font = CopyObject<XlFont>(other.Font);
                }
                if (other.Fill != null)
                {
                    base.Fill = CopyObject<XlFill>(other.Fill);
                }
                if (other.Alignment != null)
                {
                    base.Alignment = CopyObject<XlCellAlignment>(other.Alignment);
                }
                if (other.Border != null)
                {
                    base.Border = CopyObject<XlBorder>(other.Border);
                }
                if (other.NetFormatString != null)
                {
                    base.NetFormatString = other.NetFormatString;
                    base.IsDateTimeFormatString = other.IsDateTimeFormatString;
                }
                if (other.NumberFormat != null)
                {
                    base.NumberFormat = other.NumberFormat;
                }
            }
        }

        public void MergeWith(XlDifferentialFormatting other)
        {
            if (other != null)
            {
                if ((base.Font == null) && (other.Font != null))
                {
                    base.Font = CopyObject<XlFont>(other.Font);
                }
                if ((base.Fill == null) && (other.Fill != null))
                {
                    if (other.Fill.PatternType != XlPatternType.Solid)
                    {
                        base.Fill = CopyObject<XlFill>(other.Fill);
                    }
                    else
                    {
                        base.Fill = new XlFill();
                        base.Fill.PatternType = XlPatternType.Solid;
                        base.Fill.ForeColor = other.Fill.BackColor;
                    }
                }
                if ((base.Alignment == null) && (other.Alignment != null))
                {
                    base.Alignment = CopyObject<XlCellAlignment>(other.Alignment);
                }
                if ((base.Border == null) && (other.Border != null))
                {
                    base.Border = CopyObject<XlBorder>(other.Border);
                }
                if ((base.NetFormatString == null) && (other.NetFormatString != null))
                {
                    base.NetFormatString = other.NetFormatString;
                    base.IsDateTimeFormatString = other.IsDateTimeFormatString;
                }
                if ((base.NumberFormat == null) && (other.NumberFormat != null))
                {
                    base.NumberFormat = other.NumberFormat;
                }
            }
        }

        public static implicit operator XlCellFormatting(XlBorder border) => 
            new XlCellFormatting { Border = border };

        public static implicit operator XlCellFormatting(XlCellAlignment alignment) => 
            new XlCellFormatting { Alignment = alignment };

        public static implicit operator XlCellFormatting(XlFill fill) => 
            new XlCellFormatting { Fill = fill };

        public static implicit operator XlCellFormatting(XlFont font) => 
            new XlCellFormatting { Font = font };

        public static implicit operator XlCellFormatting(XlNumberFormat numberFormat) => 
            new XlCellFormatting { NumberFormat = numberFormat };

        public static XlCellFormatting Themed(XlThemeColor themeColor, double tint)
        {
            if ((themeColor != XlThemeColor.Accent1) && ((themeColor != XlThemeColor.Accent2) && ((themeColor != XlThemeColor.Accent3) && ((themeColor != XlThemeColor.Accent4) && ((themeColor != XlThemeColor.Accent5) && (themeColor != XlThemeColor.Accent6))))))
            {
                throw new ArgumentException("themeColor: accent color required");
            }
            XlCellFormatting formatting = new XlCellFormatting {
                Font = XlFont.BodyFont()
            };
            formatting.Font.Color = XlColor.FromTheme((tint >= 0.5) ? XlThemeColor.Dark1 : XlThemeColor.Light1, 0.0);
            formatting.Fill = XlFill.SolidFill(XlColor.FromTheme(themeColor, tint));
            return formatting;
        }

        public XlDifferentialFormatting ToDifferentialFormatting()
        {
            XlDifferentialFormatting formatting = new XlDifferentialFormatting {
                Font = CopyObject<XlFont>(base.Font)
            };
            if (base.Fill != null)
            {
                if (base.Fill.PatternType != XlPatternType.Solid)
                {
                    formatting.Fill = CopyObject<XlFill>(base.Fill);
                }
                else
                {
                    formatting.Fill = new XlFill();
                    formatting.Fill.PatternType = XlPatternType.Solid;
                    formatting.Fill.BackColor = base.Fill.ForeColor;
                }
            }
            formatting.Alignment = CopyObject<XlCellAlignment>(base.Alignment);
            formatting.Border = CopyObject<XlBorder>(base.Border);
            formatting.NetFormatString = base.NetFormatString;
            formatting.IsDateTimeFormatString = base.IsDateTimeFormatString;
            formatting.NumberFormat = base.NumberFormat;
            return formatting;
        }

        public static XlCellFormatting Bad
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = new XlFont()
                };
                formatting.Font.Color = DXColor.FromArgb(0xff, 0x9c, 0, 6);
                formatting.Fill = new XlFill();
                formatting.Fill.PatternType = XlPatternType.Solid;
                formatting.Fill.ForeColor = DXColor.FromArgb(0xff, 0xff, 0xc7, 0xce);
                return formatting;
            }
        }

        public static XlCellFormatting Good
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = new XlFont()
                };
                formatting.Font.Color = DXColor.FromArgb(0xff, 0, 0x61, 0);
                formatting.Fill = new XlFill();
                formatting.Fill.PatternType = XlPatternType.Solid;
                formatting.Fill.ForeColor = DXColor.FromArgb(0xff, 0xc6, 0xef, 0xce);
                return formatting;
            }
        }

        public static XlCellFormatting Neutral
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = new XlFont()
                };
                formatting.Font.Color = DXColor.FromArgb(0xff, 0x9c, 0x65, 0);
                formatting.Fill = new XlFill();
                formatting.Fill.PatternType = XlPatternType.Solid;
                formatting.Fill.ForeColor = DXColor.FromArgb(0xff, 0xff, 0xeb, 0x9c);
                return formatting;
            }
        }

        public static XlCellFormatting Calculation
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = new XlFont()
                };
                formatting.Font.Color = DXColor.FromArgb(0xff, 250, 0x7d, 0);
                formatting.Font.Bold = true;
                formatting.Fill = new XlFill();
                formatting.Fill.PatternType = XlPatternType.Solid;
                formatting.Fill.ForeColor = DXColor.FromArgb(0xff, 0xf2, 0xf2, 0xf2);
                formatting.Border = new XlBorder();
                formatting.Border.LeftColor = DXColor.FromArgb(0xff, 0x7f, 0x7f, 0x7f);
                formatting.Border.LeftLineStyle = XlBorderLineStyle.Thin;
                formatting.Border.RightColor = DXColor.FromArgb(0xff, 0x7f, 0x7f, 0x7f);
                formatting.Border.RightLineStyle = XlBorderLineStyle.Thin;
                formatting.Border.TopColor = DXColor.FromArgb(0xff, 0x7f, 0x7f, 0x7f);
                formatting.Border.TopLineStyle = XlBorderLineStyle.Thin;
                formatting.Border.BottomColor = DXColor.FromArgb(0xff, 0x7f, 0x7f, 0x7f);
                formatting.Border.BottomLineStyle = XlBorderLineStyle.Thin;
                return formatting;
            }
        }

        public static XlCellFormatting CheckCell
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = XlFont.BodyFont()
                };
                formatting.Font.Color = XlColor.FromTheme(XlThemeColor.Light1, 0.0);
                formatting.Font.Bold = true;
                formatting.Fill = XlFill.SolidFill(DXColor.FromArgb(0xa5, 0xa5, 0xa5));
                formatting.Border = XlBorder.OutlineBorders(DXColor.FromArgb(0x3f, 0x3f, 0x3f), XlBorderLineStyle.Double);
                return formatting;
            }
        }

        public static XlCellFormatting Explanatory
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = new XlFont()
                };
                formatting.Font.Color = DXColor.FromArgb(0xff, 0x7f, 0x7f, 0x7f);
                formatting.Font.Italic = true;
                return formatting;
            }
        }

        public static XlCellFormatting Input
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = new XlFont()
                };
                formatting.Font.Color = DXColor.FromArgb(0xff, 0x3f, 0x3f, 0x76);
                formatting.Fill = new XlFill();
                formatting.Fill.PatternType = XlPatternType.Solid;
                formatting.Fill.ForeColor = DXColor.FromArgb(0xff, 0xff, 0xcc, 0x99);
                formatting.Border = new XlBorder();
                formatting.Border.LeftColor = DXColor.FromArgb(0xff, 0x7f, 0x7f, 0x7f);
                formatting.Border.LeftLineStyle = XlBorderLineStyle.Thin;
                formatting.Border.RightColor = DXColor.FromArgb(0xff, 0x7f, 0x7f, 0x7f);
                formatting.Border.RightLineStyle = XlBorderLineStyle.Thin;
                formatting.Border.TopColor = DXColor.FromArgb(0xff, 0x7f, 0x7f, 0x7f);
                formatting.Border.TopLineStyle = XlBorderLineStyle.Thin;
                formatting.Border.BottomColor = DXColor.FromArgb(0xff, 0x7f, 0x7f, 0x7f);
                formatting.Border.BottomLineStyle = XlBorderLineStyle.Thin;
                return formatting;
            }
        }

        public static XlCellFormatting LinkedCell
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = new XlFont()
                };
                formatting.Font.Color = DXColor.FromArgb(0xff, 250, 0x7d, 0);
                formatting.Border = new XlBorder();
                formatting.Border.BottomColor = DXColor.FromArgb(0xff, 0xff, 0x80, 1);
                formatting.Border.BottomLineStyle = XlBorderLineStyle.Double;
                return formatting;
            }
        }

        public static XlCellFormatting Note
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Fill = new XlFill()
                };
                formatting.Fill.PatternType = XlPatternType.Solid;
                formatting.Fill.ForeColor = DXColor.FromArgb(0xff, 0xff, 0xff, 0xcc);
                formatting.Border = new XlBorder();
                formatting.Border.LeftColor = DXColor.FromArgb(0xff, 0xb2, 0xb2, 0xb2);
                formatting.Border.LeftLineStyle = XlBorderLineStyle.Thin;
                formatting.Border.RightColor = DXColor.FromArgb(0xff, 0xb2, 0xb2, 0xb2);
                formatting.Border.RightLineStyle = XlBorderLineStyle.Thin;
                formatting.Border.TopColor = DXColor.FromArgb(0xff, 0xb2, 0xb2, 0xb2);
                formatting.Border.TopLineStyle = XlBorderLineStyle.Thin;
                formatting.Border.BottomColor = DXColor.FromArgb(0xff, 0xb2, 0xb2, 0xb2);
                formatting.Border.BottomLineStyle = XlBorderLineStyle.Thin;
                return formatting;
            }
        }

        public static XlCellFormatting Output
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = new XlFont()
                };
                formatting.Font.Color = DXColor.FromArgb(0xff, 0x3f, 0x3f, 0x3f);
                formatting.Fill = new XlFill();
                formatting.Fill.PatternType = XlPatternType.Solid;
                formatting.Fill.ForeColor = DXColor.FromArgb(0xff, 0xf2, 0xf2, 0xf2);
                formatting.Border = new XlBorder();
                formatting.Border.LeftColor = DXColor.FromArgb(0xff, 0x3f, 0x3f, 0x3f);
                formatting.Border.LeftLineStyle = XlBorderLineStyle.Thin;
                formatting.Border.RightColor = DXColor.FromArgb(0xff, 0x3f, 0x3f, 0x3f);
                formatting.Border.RightLineStyle = XlBorderLineStyle.Thin;
                formatting.Border.TopColor = DXColor.FromArgb(0xff, 0x3f, 0x3f, 0x3f);
                formatting.Border.TopLineStyle = XlBorderLineStyle.Thin;
                formatting.Border.BottomColor = DXColor.FromArgb(0xff, 0x3f, 0x3f, 0x3f);
                formatting.Border.BottomLineStyle = XlBorderLineStyle.Thin;
                return formatting;
            }
        }

        public static XlCellFormatting WarningText
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = new XlFont()
                };
                formatting.Font.Color = DXColor.Red;
                return formatting;
            }
        }

        public static XlCellFormatting Hyperlink
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = new XlFont()
                };
                formatting.Font.Color = DXColor.Blue;
                formatting.Font.Underline = XlUnderlineType.Single;
                return formatting;
            }
        }

        public static XlCellFormatting Heading1
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = XlFont.BodyFont()
                };
                formatting.Font.Color = XlColor.FromTheme(XlThemeColor.Dark2, 0.0);
                formatting.Font.Size = 15.0;
                formatting.Font.Bold = true;
                formatting.Border = new XlBorder();
                formatting.Border.BottomColor = XlColor.FromTheme(XlThemeColor.Accent1, 0.0);
                formatting.Border.BottomLineStyle = XlBorderLineStyle.Thick;
                return formatting;
            }
        }

        public static XlCellFormatting Heading2
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = XlFont.BodyFont()
                };
                formatting.Font.Color = XlColor.FromTheme(XlThemeColor.Dark2, 0.0);
                formatting.Font.Size = 13.0;
                formatting.Font.Bold = true;
                formatting.Border = new XlBorder();
                formatting.Border.BottomColor = XlColor.FromTheme(XlThemeColor.Accent1, 0.5);
                formatting.Border.BottomLineStyle = XlBorderLineStyle.Thick;
                return formatting;
            }
        }

        public static XlCellFormatting Heading3
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = XlFont.BodyFont()
                };
                formatting.Font.Color = XlColor.FromTheme(XlThemeColor.Dark2, 0.0);
                formatting.Font.Bold = true;
                formatting.Border = new XlBorder();
                formatting.Border.BottomColor = XlColor.FromTheme(XlThemeColor.Accent1, 0.4);
                formatting.Border.BottomLineStyle = XlBorderLineStyle.Medium;
                return formatting;
            }
        }

        public static XlCellFormatting Heading4
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = XlFont.BodyFont()
                };
                formatting.Font.Color = XlColor.FromTheme(XlThemeColor.Dark2, 0.0);
                formatting.Font.Bold = true;
                return formatting;
            }
        }

        public static XlCellFormatting Title
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = XlFont.HeadingsFont()
                };
                formatting.Font.Color = XlColor.FromTheme(XlThemeColor.Dark2, 0.0);
                formatting.Font.Bold = true;
                formatting.Font.Size = 18.0;
                return formatting;
            }
        }

        public static XlCellFormatting Total
        {
            get
            {
                XlCellFormatting formatting = new XlCellFormatting {
                    Font = XlFont.BodyFont()
                };
                formatting.Font.Color = XlColor.FromTheme(XlThemeColor.Dark1, 0.0);
                formatting.Font.Bold = true;
                formatting.Border = new XlBorder();
                formatting.Border.TopColor = XlColor.FromTheme(XlThemeColor.Accent1, 0.0);
                formatting.Border.TopLineStyle = XlBorderLineStyle.Thin;
                formatting.Border.BottomColor = XlColor.FromTheme(XlThemeColor.Accent1, 0.0);
                formatting.Border.BottomLineStyle = XlBorderLineStyle.Double;
                return formatting;
            }
        }
    }
}

