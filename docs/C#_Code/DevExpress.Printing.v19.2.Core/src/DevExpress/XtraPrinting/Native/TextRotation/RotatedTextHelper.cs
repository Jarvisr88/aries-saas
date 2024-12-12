namespace DevExpress.XtraPrinting.Native.TextRotation
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class RotatedTextHelper : IDisposable
    {
        private Font font;
        private System.Drawing.StringFormat prototype;
        private StringFormatContainer sfContainer;
        private string text;

        public RotatedTextHelper(Font font, System.Drawing.StringFormat prototype, string text);
        public static float GetAngleInRadians(float angleInDegrees);
        private RectangleF GetBounds(RectangleF clientBounds, SizeF textSize, float angle, DevExpress.XtraPrinting.Native.TextRotation.TextRotation textRotation);
        public RectangleF GetBounds(RectangleF clientBounds, float angle, DevExpress.XtraPrinting.Native.TextRotation.TextRotation textRotation, float width, GraphicsUnit pageUnit, Measurer measurer);
        private static PointF GetOffset(RectangleF rect, double radAngle, DevExpress.XtraPrinting.Native.TextRotation.TextRotation textRotation);
        public static StringAlignment GetRotatedAlignment(TextAlignment alignment, float angle);
        public static StringAlignment GetRotatedLineAlignment(TextAlignment alignment, float angle);
        private SizeF MeasureString(string text, float width, GraphicsUnit pageUnit, Measurer measurer);
        void IDisposable.Dispose();
        private static StringFormatContainer ValidateStringFormat(System.Drawing.StringFormat sf, string text);
        internal SizeF ValidateTextSize(RectangleF clientBounds, SizeF textSize, float angle);

        public System.Drawing.StringFormat StringFormat { get; }
    }
}

