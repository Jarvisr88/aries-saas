namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal static class FormattingExtensions
    {
        private static XlFont ConvertFont(XlCellFont xlCellFont)
        {
            XlFont font1 = new XlFont();
            font1.Bold = xlCellFont.Bold;
            font1.Color = xlCellFont.Color;
            font1.Size = (Math.Abs(xlCellFont.Size) < 0.1) ? 11.0 : xlCellFont.Size;
            XlFont local1 = font1;
            local1.Name = GetCorrectFontName(xlCellFont);
            local1.Italic = xlCellFont.Italic;
            local1.FontFamily = XlFontFamily.Auto;
            local1.Underline = xlCellFont.Underline;
            local1.StrikeThrough = xlCellFont.StrikeThrough;
            local1.SchemeStyle = XlFontSchemeStyles.None;
            return local1;
        }

        public static XlCellFormatting ConvertWith(this XlFormattingObject from, bool allowHorzLines, bool allowVertLines)
        {
            if (from == null)
            {
                return null;
            }
            XlCellFormatting result = new XlCellFormatting {
                Alignment = from.Alignment
            };
            GetFill(from, result);
            result.Border = from.Border;
            if ((result.Border == null) || Equals(result.Border, new XlBorder()))
            {
                GetBorder(allowHorzLines, allowVertLines, result);
            }
            GetFont(from, result);
            GetFormat(from, result);
            return result;
        }

        public static void GetActual(this XlCellFormatting format, FormatSettings settings)
        {
            if (settings != null)
            {
                FormattingUtils.GetActualFormatString(format, settings.FormatString, settings.ActualDataType, settings.FormatType);
            }
        }

        private static void GetBorder(bool allowHorzLines, bool allowVertLines, XlCellFormatting result)
        {
            FormattingUtils.SetBorder(result, allowHorzLines, allowVertLines);
        }

        private static string GetCorrectFontName(XlCellFont xlCellFont) => 
            string.IsNullOrEmpty(xlCellFont.Name) ? "Calibri" : xlCellFont.Name;

        private static void GetFill(XlFormattingObject from, XlCellFormatting result)
        {
            if (!Equals(from.BackColor, Color.Empty))
            {
                result.Fill = new XlFill();
                result.Fill.ForeColor = GetForeColor(from);
                result.Fill.PatternType = XlPatternType.Solid;
            }
        }

        private static void GetFont(XlFormattingObject from, XlCellFormatting result)
        {
            result.Font = new XlFont();
            if (from.Font != null)
            {
                result.Font = ConvertFont(from.Font);
            }
        }

        private static Color GetForeColor(XlFormattingObject fromFormatting) => 
            (fromFormatting.BackColor != Color.Empty) ? fromFormatting.BackColor : Color.White;

        private static void GetFormat(XlFormattingObject from, XlCellFormatting cellFormat)
        {
            cellFormat.NumberFormat = from.NumberFormat;
            if (from.FormatType != FormatType.None)
            {
                cellFormat.NetFormatString = from.FormatString;
            }
            if (from.FormatType == FormatType.DateTime)
            {
                string netFormatString = cellFormat.NetFormatString;
                string str2 = string.Empty;
                if (cellFormat.NumberFormat != null)
                {
                    str2 = cellFormat.NumberFormat.ToString();
                }
                if (string.IsNullOrEmpty(netFormatString) && string.IsNullOrEmpty(str2))
                {
                    cellFormat.NetFormatString = StandardFormats.DecimalOrDateTime;
                }
                cellFormat.IsDateTimeFormatString = true;
            }
        }
    }
}

