namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public class FontMetrics
    {
        private float ascent;
        private float descent;
        private float lineSpacing;
        private float fontDpi;
        private float pageDpi;

        public FontMetrics();
        public FontMetrics(Font font, GraphicsUnit pageUnit);
        public float CalculateHeight(int lineCount);
        public static FontMetrics CreateInstance(Font font, GraphicsUnit pageUnit);

        public float Ascent { get; }
    }
}

