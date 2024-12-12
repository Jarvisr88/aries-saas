namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows.Forms;

    public static class RichEditHelper
    {
        public static RectangleF CorrectRtfLineBounds(float dpi, RichTextBox richTextBox, RectangleF bounds, int charFrom, out int charEnd);
        public static void DrawRtf(Graphics gr, RichTextBox richTextBox, RectangleF bounds);
        [SecuritySafeCritical]
        private static int FormatRangeInternal(bool measure, Graphics graphics, RichTextBox richTextBox, RectangleF bounds, int charFrom, int charTo, out int charEnd);
        public static Image GetRtfImage(RichTextBox richTextBox, float dpi, RectangleF bounds);
        public static int MeasureRtfInPixels(string rtfText, RectangleF bounds, int minHeight);
    }
}

