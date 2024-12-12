namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public class BuildHelper
    {
        private static RectangleF DecreaseRectHeight(RectangleF rect, float delta, float minHeight);
        public static float GetDocBandHeight(DocumentBand docBand, RectangleF bounds, float usefulPageWidth, bool includeBottomSpans);
        public static RectangleF SubtractBandHeight(RectangleF bounds, float height, float minHeight);
    }
}

