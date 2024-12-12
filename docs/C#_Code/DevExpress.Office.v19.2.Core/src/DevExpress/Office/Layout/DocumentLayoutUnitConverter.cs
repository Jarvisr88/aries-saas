namespace DevExpress.Office.Layout
{
    using DevExpress.Office;
    using System;
    using System.Drawing;

    public abstract class DocumentLayoutUnitConverter : DpiSupport
    {
        protected DocumentLayoutUnitConverter()
        {
        }

        protected DocumentLayoutUnitConverter(float screenDpiX, float screenDpiY) : base(screenDpiX, screenDpiY)
        {
        }

        public static DocumentLayoutUnitConverter Create(DocumentLayoutUnit unit, float dpi) => 
            Create(unit, dpi, dpi);

        public static DocumentLayoutUnitConverter Create(DocumentLayoutUnit unit, float dpiX, float dpiY)
        {
            switch (unit)
            {
                case DocumentLayoutUnit.Document:
                    return new DocumentLayoutUnitDocumentConverter(dpiX, dpiY);

                case DocumentLayoutUnit.Pixel:
                    return new DocumentLayoutUnitPixelsConverter(dpiX, dpiY);
            }
            return new DocumentLayoutUnitTwipsConverter(dpiX, dpiY);
        }

        public abstract float DocumentsToFontUnitsF(float value);
        public abstract Rectangle DocumentsToLayoutUnits(Rectangle value);
        public abstract RectangleF DocumentsToLayoutUnits(RectangleF value);
        public abstract int DocumentsToLayoutUnits(int value);
        public abstract float FontUnitsToLayoutUnitsF(float value);
        public abstract float InchesToFontUnitsF(float value);
        public abstract Rectangle LayoutUnitsToDocuments(Rectangle value);
        public abstract RectangleF LayoutUnitsToDocuments(RectangleF value);
        public abstract int LayoutUnitsToDocuments(int value);
        public abstract Size LayoutUnitsToHundredthsOfInch(Size value);
        public abstract int LayoutUnitsToHundredthsOfInch(int value);
        public Point LayoutUnitsToPixels(Point value) => 
            this.LayoutUnitsToPixels(value, base.ScreenDpiX, base.ScreenDpiY);

        public Rectangle LayoutUnitsToPixels(Rectangle value) => 
            this.LayoutUnitsToPixels(value, base.ScreenDpiX, base.ScreenDpiY);

        public Size LayoutUnitsToPixels(Size value) => 
            this.LayoutUnitsToPixels(value, base.ScreenDpiX, base.ScreenDpiY);

        public int LayoutUnitsToPixels(int value) => 
            this.LayoutUnitsToPixels(value, base.ScreenDpi);

        public abstract int LayoutUnitsToPixels(int value, float dpi);
        public abstract Point LayoutUnitsToPixels(Point value, float dpiX, float dpiY);
        public abstract Rectangle LayoutUnitsToPixels(Rectangle value, float dpiX, float dpiY);
        public abstract Size LayoutUnitsToPixels(Size value, float dpiX, float dpiY);
        public float LayoutUnitsToPixelsF(float value) => 
            this.LayoutUnitsToPixelsF(value, base.ScreenDpi);

        public abstract float LayoutUnitsToPixelsF(float value, float dpi);
        public abstract float LayoutUnitsToPointsF(float value);
        public abstract int LayoutUnitsToTwips(int value);
        public abstract long LayoutUnitsToTwips(long value);
        public abstract float MillimetersToFontUnitsF(float value);
        public int Pixels96DPIToLayoutUnits(int value) => 
            this.Pixels96DPIToLayoutUnits(value, base.ScreenDpi);

        public abstract int Pixels96DPIToLayoutUnits(int value, float dpi);
        public float Pixels96DPIToLayoutUnitsF(float value) => 
            this.Pixels96DPIToLayoutUnitsF(value, base.ScreenDpi);

        public abstract float Pixels96DPIToLayoutUnitsF(float value, float dpi);
        public Rectangle PixelsToLayoutUnits(Rectangle value) => 
            this.PixelsToLayoutUnits(value, base.ScreenDpiX, base.ScreenDpiY);

        public Size PixelsToLayoutUnits(Size value) => 
            this.PixelsToLayoutUnits(value, base.ScreenDpiX, base.ScreenDpiY);

        public int PixelsToLayoutUnits(int value) => 
            this.PixelsToLayoutUnits(value, base.ScreenDpi);

        public abstract int PixelsToLayoutUnits(int value, float dpi);
        public abstract Rectangle PixelsToLayoutUnits(Rectangle value, float dpiX, float dpiY);
        public abstract Size PixelsToLayoutUnits(Size value, float dpiX, float dpiY);
        public SizeF PixelsToLayoutUnitsF(SizeF value) => 
            this.PixelsToLayoutUnitsF(value, base.ScreenDpiX, base.ScreenDpiY);

        public float PixelsToLayoutUnitsF(float value) => 
            this.PixelsToLayoutUnitsF(value, base.ScreenDpi);

        public abstract float PixelsToLayoutUnitsF(float value, float dpi);
        public abstract SizeF PixelsToLayoutUnitsF(SizeF value, float dpiX, float dpiY);
        public abstract int PointsToFontUnits(int value);
        public abstract float PointsToFontUnitsF(float value);
        public abstract int PointsToLayoutUnits(int value);
        public abstract float PointsToLayoutUnitsF(float value);
        public int SnapToPixels(int value) => 
            this.SnapToPixels(value, base.ScreenDpi);

        public abstract int SnapToPixels(int value, float dpi);
        public abstract int TwipsToLayoutUnits(int value);
        public abstract long TwipsToLayoutUnits(long value);

        public abstract float Dpi { get; }

        public abstract LayoutGraphicsUnit GraphicsPageUnit { get; }

        public abstract float GraphicsPageScale { get; }

        public abstract LayoutGraphicsUnit FontUnit { get; }

        public abstract float FontSizeScale { get; }
    }
}

