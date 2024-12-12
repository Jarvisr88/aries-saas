namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public class Measurement
    {
        [ThreadStatic]
        private static DevExpress.XtraPrinting.Native.Measurer measurer;
        public const string FontMeasureGlyph = "gM";

        public static SizeF MeasureString(string text, Font font, GraphicsUnit pageUnit);
        public static SizeF MeasureString(string text, Font font, PointF location, StringFormat stringFormat, GraphicsUnit pageUnit);
        public static SizeF MeasureString(string text, Font font, SizeF size, StringFormat stringFormat, GraphicsUnit pageUnit);
        public static SizeF MeasureString(string text, Font font, float width, StringFormat stringFormat, GraphicsUnit pageUnit);

        public static DevExpress.XtraPrinting.Native.Measurer Measurer { get; }
    }
}

