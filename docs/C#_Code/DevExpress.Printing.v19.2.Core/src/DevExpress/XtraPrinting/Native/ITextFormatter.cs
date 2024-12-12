namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public interface ITextFormatter
    {
        string[] FormatHtmlMultilineText(string multilineText, Font font, float width, float height, StringFormat stringFormat);
        string[] FormatHtmlMultilineText(string multilineText, Font font, float width, float height, StringFormat stringFormat, bool designateNewLines);
        string[] FormatMultilineText(string multilineText, Font font, float width, float height, StringFormat stringFormat);
    }
}

