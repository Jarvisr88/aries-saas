namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using System;
    using System.Drawing;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class HtmlStyleRender
    {
        public static string GetCorrectedFamilyName(string familyName) => 
            Regex.IsMatch(familyName, @"[\s\d]") ? ("'" + familyName + "'") : familyName;

        public static string GetFontHtml(Font font) => 
            (font != null) ? GetFontHtml(FontHelper.GetFamilyName(font), font.Size, font.Unit, font.Bold, font.Italic, font.Strikeout, font.Underline) : string.Empty;

        public static void GetFontHtml(Font font, DXCssStyleCollection style)
        {
            if (font != null)
            {
                GetFontHtml(FontHelper.GetFamilyName(font), font.Size, font.Unit, font.Bold, font.Italic, font.Strikeout, font.Underline, false, style);
            }
        }

        public static string GetFontHtml(string fontFamilyName, float size, GraphicsUnit unit, bool bold, bool italic, bool strikeout, bool underline) => 
            GetFontHtml(fontFamilyName, size, unit, bold, italic, strikeout, underline, false);

        public static string GetFontHtml(string fontFamilyName, float size, GraphicsUnit unit, bool bold, bool italic, bool strikeout, bool underline, bool inPixels)
        {
            string str = inPixels ? HtmlConvert.FontSizeToStringInPixels(size, unit) : HtmlConvert.FontSizeToString(size, unit);
            return $"font-family:{GetCorrectedFamilyName(fontFamilyName)}; font-size:{str}; font-weight:{GetFontWeight(bold)}; font-style:{GetFontStyle(italic)}; {GetTextDecoration(strikeout, underline)}";
        }

        public static void GetFontHtml(string fontFamilyName, float size, GraphicsUnit unit, bool bold, bool italic, bool strikeout, bool underline, bool inPixels, DXCssStyleCollection style)
        {
            string str = inPixels ? HtmlConvert.FontSizeToStringInPixels(size, unit) : HtmlConvert.FontSizeToString(size, unit);
            style.Add("font-family", GetCorrectedFamilyName(fontFamilyName));
            style.Add("font-size", str);
            style.Add("font-weight", GetFontWeight(bold));
            style.Add("font-style", GetFontStyle(italic));
            string textDecorationValue = GetTextDecorationValue(strikeout, underline);
            if (!string.IsNullOrEmpty(textDecorationValue))
            {
                style.Add("text-decoration", textDecorationValue);
            }
        }

        public static object GetFontHtmlInPixels(Font font) => 
            (font != null) ? GetFontHtml(FontHelper.GetFamilyName(font), font.Size, font.Unit, font.Bold, font.Italic, font.Strikeout, font.Underline, true) : string.Empty;

        public static string GetFontStyle(bool isItalic) => 
            isItalic ? "italic" : "normal";

        public static string GetFontWeight(bool isBold) => 
            isBold ? "bold" : "normal";

        public static string GetHtmlStyle(Font font, Color foreColor) => 
            $"color:{HtmlConvert.ToHtml(foreColor)};{GetFontHtml(font)}";

        public static string GetHtmlStyle(Font font, Color foreColor, Color backColor) => 
            $"color:{HtmlConvert.ToHtml(foreColor)};background-color:{HtmlConvert.ToHtml(backColor)};{GetFontHtml(font)}";

        public static void GetHtmlStyle(Font font, Color foreColor, Color backColor, DXCssStyleCollection style)
        {
            style.Add("color", HtmlConvert.ToHtml(foreColor));
            style.Add("background-color", HtmlConvert.ToHtml(backColor));
            GetFontHtml(font, style);
        }

        public static string GetHtmlStyle(string fontFamilyName, float size, GraphicsUnit unit, bool bold, bool italic, bool strikeout, bool underline, Color foreColor, Color backColor) => 
            GetHtmlStyle(fontFamilyName, size, unit, bold, italic, strikeout, underline, foreColor, backColor, false);

        public static void GetHtmlStyle(string fontFamilyName, float size, GraphicsUnit unit, bool bold, bool italic, bool strikeout, bool underline, Color foreColor, Color backColor, DXCssStyleCollection style)
        {
            GetHtmlStyle(fontFamilyName, size, unit, bold, italic, strikeout, underline, foreColor, backColor, false, style);
        }

        public static string GetHtmlStyle(string fontFamilyName, float size, GraphicsUnit unit, bool bold, bool italic, bool strikeout, bool underline, Color foreColor, Color backColor, bool useFontSizeInPixels)
        {
            string str = GetFontHtml(fontFamilyName, size, unit, bold, italic, strikeout, underline, useFontSizeInPixels);
            return $"color:{HtmlConvert.ToHtml(foreColor)};background-color:{HtmlConvert.ToHtml(backColor)};{str}";
        }

        public static void GetHtmlStyle(string fontFamilyName, float size, GraphicsUnit unit, bool bold, bool italic, bool strikeout, bool underline, Color foreColor, Color backColor, bool useFontSizeInPixels, DXCssStyleCollection style)
        {
            style.Add("color", HtmlConvert.ToHtml(foreColor));
            style.Add("background-color", HtmlConvert.ToHtml(backColor));
            GetFontHtml(fontFamilyName, size, unit, bold, italic, strikeout, underline, useFontSizeInPixels, style);
        }

        public static string GetTextDecoration(bool strikeout, bool underline)
        {
            StringBuilder builder = new StringBuilder();
            string textDecorationValue = GetTextDecorationValue(strikeout, underline);
            if (!string.IsNullOrEmpty(textDecorationValue))
            {
                builder.Append("text-decoration:");
                builder.Append(textDecorationValue);
                builder.Append(";");
            }
            return builder.ToString();
        }

        public static string GetTextDecorationValue(bool strikeout, bool underline)
        {
            StringBuilder builder = new StringBuilder();
            if (strikeout)
            {
                builder.Append(" line-through");
            }
            if (underline)
            {
                builder.Append(" underline");
            }
            return builder.ToString();
        }
    }
}

