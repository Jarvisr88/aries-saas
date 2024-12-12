namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using DevExpress.Utils.Text.Internal;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Drawing;

    internal static class FormattingUtils
    {
        private static XlCellFormatting specialAreaCellFormatting;

        private static bool AllowSetFormatString(XlCellFormatting cellFormatting, string columnFormatString) => 
            !string.IsNullOrEmpty(columnFormatString) && ReferenceEquals(cellFormatting.NumberFormat, null);

        private static bool CheckDateTimeType(IColumn gridColumn) => 
            (gridColumn.FormatSettings.ActualDataType == typeof(DateTime?)) || (gridColumn.FormatSettings.ActualDataType == typeof(TimeSpan?));

        private static bool CheckTypeIgnoreNullable(Type typeToCheck, Type isTypeof)
        {
            if ((typeToCheck == null) || (isTypeof == null))
            {
                return false;
            }
            isTypeof = (Nullable.GetUnderlyingType(isTypeof) != null) ? Nullable.GetUnderlyingType(isTypeof) : isTypeof;
            typeToCheck = (Nullable.GetUnderlyingType(typeToCheck) != null) ? Nullable.GetUnderlyingType(typeToCheck) : typeToCheck;
            return (typeToCheck == isTypeof);
        }

        public static XlFont CreateFont(StringFontSettings settings)
        {
            if (!(settings.IsStyleSet && (settings.Style != FontStyle.Regular)) && (settings.Color == Color.Empty))
            {
                return null;
            }
            XlFont font = new XlFont {
                Color = settings.Color,
                Size = settings.Size
            };
            if ((settings.Style & FontStyle.Bold) == FontStyle.Bold)
            {
                font.Bold = true;
            }
            if ((settings.Style & FontStyle.Italic) == FontStyle.Italic)
            {
                font.Italic = true;
            }
            if ((settings.Style & FontStyle.Underline) == FontStyle.Underline)
            {
                font.Underline = XlUnderlineType.Single;
            }
            if ((settings.Style & FontStyle.Strikeout) == FontStyle.Strikeout)
            {
                font.StrikeThrough = true;
            }
            return font;
        }

        public static void GetActualFormatString(XlCellFormatting format, string formatString, Type colType, FormatType fType)
        {
            if (format != null)
            {
                if (format.NumberFormat == null)
                {
                    format.NetFormatString = GetFormatStringByType(formatString, colType);
                }
                if (CheckTypeIgnoreNullable(colType, typeof(DateTime)) || (fType == FormatType.DateTime))
                {
                    if (string.IsNullOrEmpty(format.NetFormatString))
                    {
                        format.NetFormatString = StandardFormats.DecimalOrDateTime;
                    }
                    format.IsDateTimeFormatString = true;
                }
                if (CheckTypeIgnoreNullable(colType, typeof(TimeSpan)))
                {
                    if (string.IsNullOrEmpty(format.NetFormatString))
                    {
                        format.NumberFormat = XlNumberFormat.Span;
                    }
                    format.IsDateTimeFormatString = true;
                }
            }
        }

        public static XlCellFormatting GetColumnAppearanceFormGridColumn(IColumn col, bool notRawDataMode)
        {
            XlCellFormatting formatting = new XlCellFormatting();
            if (notRawDataMode)
            {
                formatting.CopyFrom(col.Appearance);
            }
            return formatting;
        }

        public static XlCellFormatting GetDefault() => 
            new XlCellFormatting { 
                Font = new XlFont(),
                Fill = new XlFill(),
                Alignment = new XlCellAlignment(),
                Border = new XlBorder()
            };

        private static string GetFormatStringByType(string formatStr, Type type) => 
            string.IsNullOrEmpty(formatStr) ? (!(type == typeof(int)) ? (!(type == typeof(double)) ? string.Empty : "n2") : StandardFormats.DecimalOrDateTime) : formatStr;

        public static bool IsRunContainsNewLineChars(XlRichTextRun run) => 
            run.Text.Contains("\n") || (run.Text.Contains("\r\n") || run.Text.Contains("\r"));

        public static void PrimaryFormatColumn(IColumn col, XlCellFormatting format)
        {
            SetFormat(col, format, col.FormatSettings.FormatString);
        }

        public static void SetBoldFont(XlCellFormatting formatting)
        {
            if (formatting != null)
            {
                if (formatting.Font == null)
                {
                    formatting.Font = new XlFont();
                    formatting.Font.Bold = true;
                }
                else if (formatting.Font.Equals(SpecialAreaDefaultFormatting.Font))
                {
                    formatting.Font.Bold = true;
                }
            }
        }

        public static void SetBorder(XlCellFormatting format, bool allowHorzLines, bool allowVertLines)
        {
            if (format != null)
            {
                XlBorder border = format.Border;
                if (allowHorzLines)
                {
                    SetHorzLines(ref border);
                }
                if (allowVertLines)
                {
                    SetVertLines(ref border);
                }
                format.Border = border;
            }
        }

        public static void SetCellFormatting(XlCellFormatting cellFormatting, bool allowHorzLines, bool allowVertLines, IColumn col)
        {
            SetBorder(cellFormatting, allowHorzLines, allowVertLines);
            SetFormat(col, cellFormatting, col.FormatSettings.FormatString);
        }

        public static void SetCellFormatting(XlCellFormatting cellFormatting, bool allowHorzLines, bool allowVertLines, bool condition)
        {
            if (condition)
            {
                SetBoldFont(cellFormatting);
            }
            SetBorder(cellFormatting, allowHorzLines, allowVertLines);
        }

        public static void SetCellFormatting(XlCellFormatting cellFormatting, bool allowHorzLines, bool allowVertLines, string formatString)
        {
            SetBoldFont(cellFormatting);
            SetBorder(cellFormatting, allowHorzLines, allowVertLines);
        }

        public static void SetCellFormatting(XlCellFormatting cellFormatting, IColumn gridColumn, XlCellFormatting appearanceFooter, bool allowHorzLines, bool allowVertLines, bool condition)
        {
            if (condition && ((gridColumn != null) && (cellFormatting != null)))
            {
                if (!XlCellFormatting.Equals(appearanceFooter, SpecialAreaDefaultFormatting))
                {
                    cellFormatting.CopyFrom(appearanceFooter);
                }
                else
                {
                    if (cellFormatting.Font != null)
                    {
                        cellFormatting.Font.CopyFrom(SpecialAreaDefaultFormatting.Font);
                    }
                    SetBoldFont(cellFormatting);
                }
                string formatString = gridColumn.FormatSettings.FormatString;
                cellFormatting.NetFormatString = AllowSetFormatString(cellFormatting, formatString) ? formatString : string.Empty;
            }
            SetBorder(cellFormatting, allowHorzLines, allowVertLines);
        }

        public static void SetCellFormatting(XlCellFormatting cellFormatting, bool allowHorzLines, bool allowVertLines, string formatString, IColumn col, bool setbold, bool condition)
        {
            if (setbold)
            {
                SetBoldFont(cellFormatting);
            }
            SetBorder(cellFormatting, allowHorzLines, allowVertLines);
            if (condition)
            {
                SetFormat(col, cellFormatting, formatString);
            }
            else
            {
                cellFormatting.NetFormatString = formatString;
            }
        }

        private static void SetFormat(IColumn gridColumn, XlCellFormatting format, string formatString)
        {
            Type colType = CheckDateTimeType(gridColumn) ? Nullable.GetUnderlyingType(gridColumn.FormatSettings.ActualDataType) : gridColumn.FormatSettings.ActualDataType;
            GetActualFormatString(format, formatString, colType, gridColumn.FormatSettings.FormatType);
        }

        public static void SetHorzLines(ref XlBorder border)
        {
            border ??= new XlBorder();
            border.BottomLineStyle = XlBorderLineStyle.Thin;
            border.BottomColor = Color.Black;
            border.TopLineStyle = XlBorderLineStyle.Thin;
            border.TopColor = Color.Black;
        }

        public static void SetHyperlinkFormat(XlCellFormatting cellFormatting)
        {
            if (cellFormatting != null)
            {
                cellFormatting.Font.Color = Color.Blue;
                cellFormatting.Font.Underline = XlUnderlineType.Single;
            }
        }

        public static void SetRichTextCellWrap(IXlCell cell)
        {
            cell.Formatting ??= new XlCellFormatting();
            cell.Formatting.Alignment ??= new XlCellAlignment();
            cell.Formatting.Alignment.WrapText = true;
        }

        public static void SetVertLines(ref XlBorder border)
        {
            border ??= new XlBorder();
            border.LeftLineStyle = XlBorderLineStyle.Thin;
            border.LeftColor = Color.Black;
            border.RightLineStyle = XlBorderLineStyle.Thin;
            border.RightColor = Color.Black;
        }

        public static void ValidateTextRun(XlRichTextRun run)
        {
            int num = -1809083443;
            if ((run.Font != null) && (run.Font.GetHashCode() == num))
            {
                run.Font = null;
            }
        }

        public static XlCellFormatting SpecialAreaDefaultFormatting
        {
            get
            {
                if (specialAreaCellFormatting == null)
                {
                    specialAreaCellFormatting = new XlCellFormatting();
                    XlCellAlignment alignment1 = new XlCellAlignment();
                    alignment1.WrapText = false;
                    alignment1.VerticalAlignment = XlVerticalAlignment.Center;
                    alignment1.HorizontalAlignment = XlHorizontalAlignment.General;
                    specialAreaCellFormatting.Alignment = alignment1;
                    specialAreaCellFormatting.Font = new XlFont();
                    specialAreaCellFormatting.Font.Bold = false;
                    specialAreaCellFormatting.Font.StrikeThrough = false;
                    specialAreaCellFormatting.Font.Underline = XlUnderlineType.None;
                    specialAreaCellFormatting.Font.Italic = false;
                    specialAreaCellFormatting.Font.FontFamily = XlFontFamily.Swiss;
                    specialAreaCellFormatting.Font.Name = "Calibri";
                    specialAreaCellFormatting.Font.Size = 11.0;
                    specialAreaCellFormatting.Font.SchemeStyle = XlFontSchemeStyles.Minor;
                    specialAreaCellFormatting.Font.Color = XlColor.FromTheme(XlThemeColor.Dark1, 0.0);
                }
                return specialAreaCellFormatting;
            }
        }
    }
}

