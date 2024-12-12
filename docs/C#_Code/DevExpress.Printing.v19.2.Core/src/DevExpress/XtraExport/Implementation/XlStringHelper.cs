namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;

    internal static class XlStringHelper
    {
        public static XlRichTextString CheckLength(XlRichTextString text, IXlDocumentOptionsEx options)
        {
            if (text.Length <= 0x7fff)
            {
                return text;
            }
            if (!options.TruncateStringsToMaxLength)
            {
                throw new InvalidOperationException("String length exceed 32767 characters!");
            }
            return text.Truncate(0x7fff);
        }

        public static string CheckLength(string text, IXlDocumentOptionsEx options)
        {
            if (text.Length <= 0x7fff)
            {
                return text;
            }
            if (!options.TruncateStringsToMaxLength)
            {
                throw new InvalidOperationException("String length exceed 32767 characters!");
            }
            return text.Substring(0, 0x7fff);
        }
    }
}

