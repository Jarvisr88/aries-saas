namespace DevExpress.Pdf.ContentGeneration.Extensions
{
    using DevExpress.Text.Fonts;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class PdfExportExtensions
    {
        public static string GetFontName(this DXFontDescriptor descriptor)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in descriptor.FamilyName)
            {
                if ((ch != ' ') && ((ch != '\r') && (ch != '\n')))
                {
                    builder.Append(ch);
                }
            }
            string str = string.Empty;
            if (descriptor.IsBold())
            {
                str = str + "Bold";
            }
            if (descriptor.Style.HasFlag(DXFontStyle.Italic))
            {
                str = str + "Italic";
            }
            if (descriptor.Style.HasFlag(DXFontStyle.Oblique))
            {
                str = str + "Oblique";
            }
            if (!string.IsNullOrEmpty(str))
            {
                builder.Append(",");
                builder.Append(str);
            }
            return builder.ToString();
        }

        public static bool IsBold(this DXFontDescriptor descriptor) => 
            descriptor.Weight >= DXFontWeight.Bold;

        public static bool IsItalic(this DXFontDescriptor descriptor) => 
            descriptor.Style != DXFontStyle.Regular;

        public static bool IsSymbolFont(this DXFontDescriptor descriptor) => 
            descriptor.FamilyName == "Symbol";
    }
}

