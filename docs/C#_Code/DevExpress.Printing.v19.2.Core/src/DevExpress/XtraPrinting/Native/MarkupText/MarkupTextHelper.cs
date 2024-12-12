namespace DevExpress.XtraPrinting.Native.MarkupText
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public static class MarkupTextHelper
    {
        public static float GetBottomSplitValue(PrintingStringInfo stringInfo, float brickTop, float pageBottom);
        public static SizeF GetContentSize(string text, int maxWidth, BrickStyle style, IMeasurer measurer, ImageItemCollection imageResources);
        public static string GetSimpleContentString(PrintingStringInfo stringInfo);
        public static List<XlRichTextRun> GetXlRichTextRuns(PrintingStringInfo stringInfo);
        public static bool IsSimpleString(string text);
    }
}

