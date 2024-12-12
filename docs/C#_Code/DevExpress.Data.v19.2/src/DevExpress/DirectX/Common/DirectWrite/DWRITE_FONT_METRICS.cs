namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_FONT_METRICS
    {
        private short designUnitsPerEm;
        private short ascent;
        private short descent;
        private short lineGap;
        private short capHeight;
        private short xHeight;
        private short underlinePosition;
        private short underlineThickness;
        private short strikethroughPosition;
        private short strikethroughThickness;
        public short DesignUnitsPerEm =>
            this.designUnitsPerEm;
        public short Ascent =>
            this.ascent;
        public short Descent =>
            this.descent;
        public short LineGap =>
            this.lineGap;
        public short CapHeight =>
            this.capHeight;
        public short XHeight =>
            this.xHeight;
        public short UnderlinePosition =>
            this.underlinePosition;
        public short UnderlineThickness =>
            this.underlineThickness;
        public short StrikethroughPosition =>
            this.strikethroughPosition;
        public short StrikethroughThickness =>
            this.strikethroughThickness;
    }
}

