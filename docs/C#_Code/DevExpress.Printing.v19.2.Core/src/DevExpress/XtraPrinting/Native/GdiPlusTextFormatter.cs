namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public class GdiPlusTextFormatter : TextFormatter
    {
        public GdiPlusTextFormatter(GraphicsUnit pageUnit, Measurer measurer);
        public override string[] FormatMultilineText(string text, Font font, float width, float height, StringFormat stringFormat);
        private string GetNextLine(string paragraph, Font font, float width, float height, StringFormat stringFormat, ref int charactersFitted);
        private static string GetTrimmedLineTail(StringTrimming trimming);
        private static string GetTrimmedString(string s, int start, int end);
    }
}

