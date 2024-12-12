namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_FONT_METRICS1
    {
        private DWRITE_FONT_METRICS metrics;
        private short glyphBoxLeft;
        private short glyphBoxTop;
        private short glyphBoxRight;
        private short glyphBoxBottom;
        private short subscriptPositionX;
        private short subscriptPositionY;
        private short subscriptSizeX;
        private short subscriptSizeY;
        private short superscriptPositionX;
        private short superscriptPositionY;
        private short superscriptSizeX;
        private short superscriptSizeY;
        private int hasTypographicMetrics;
        public short DesignUnitsPerEm =>
            this.metrics.DesignUnitsPerEm;
        public short Ascent =>
            this.metrics.Ascent;
        public short Descent =>
            this.metrics.Descent;
        public short LineGap =>
            this.metrics.LineGap;
        public short UnderlinePosition =>
            this.metrics.UnderlinePosition;
        public short UnderlineThickness =>
            this.metrics.UnderlineThickness;
        public short StrikethroughPosition =>
            this.metrics.StrikethroughPosition;
        public short StrikethroughThickness =>
            this.metrics.StrikethroughThickness;
        public short SubscriptPositionX =>
            this.subscriptPositionX;
        public short SubscriptPositionY =>
            this.subscriptPositionY;
        public short SubscriptSizeX =>
            this.subscriptSizeX;
        public short SubscriptSizeY =>
            this.subscriptSizeY;
        public short SuperscriptPositionX =>
            this.superscriptPositionX;
        public short SuperscriptPositionY =>
            this.superscriptPositionY;
        public short SuperscriptSizeX =>
            this.superscriptSizeX;
        public short SuperscriptSizeY =>
            this.superscriptSizeY;
        public bool HasTypographicMetrics =>
            this.hasTypographicMetrics != 0;
    }
}

