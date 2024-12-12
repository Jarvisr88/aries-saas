namespace DevExpress.Office.Layout
{
    using DevExpress.Office.Utils;
    using System;
    using System.Drawing;

    public class DocumentLayoutUnitPixelsConverter : DocumentLayoutUnitConverter
    {
        private readonly float dpi;

        public DocumentLayoutUnitPixelsConverter(float dpi) : this(dpi, dpi)
        {
        }

        public DocumentLayoutUnitPixelsConverter(float screenDpiX, float screenDpiY) : base(screenDpiX, screenDpiY)
        {
            this.dpi = screenDpiX;
        }

        public override float DocumentsToFontUnitsF(float value) => 
            Units.DocumentsToPointsF(value);

        public override Rectangle DocumentsToLayoutUnits(Rectangle value) => 
            Units.DocumentsToPixels(value, this.Dpi, this.Dpi);

        public override RectangleF DocumentsToLayoutUnits(RectangleF value) => 
            Units.DocumentsToPixels(value, this.Dpi, this.Dpi);

        public override int DocumentsToLayoutUnits(int value) => 
            Units.DocumentsToPixels(value, this.Dpi);

        public override float FontUnitsToLayoutUnitsF(float value) => 
            Units.PointsToPixelsF(value, this.Dpi);

        public override float InchesToFontUnitsF(float value) => 
            Units.InchesToPointsF(value);

        public override Rectangle LayoutUnitsToDocuments(Rectangle value) => 
            Units.PixelsToDocuments(value, this.Dpi, this.Dpi);

        public override RectangleF LayoutUnitsToDocuments(RectangleF value) => 
            Units.PixelsToDocuments(value, this.Dpi, this.Dpi);

        public override int LayoutUnitsToDocuments(int value) => 
            Units.PixelsToDocuments(value, this.Dpi);

        public override Size LayoutUnitsToHundredthsOfInch(Size value) => 
            Units.PixelsToHundredthsOfInch(value, this.Dpi);

        public override int LayoutUnitsToHundredthsOfInch(int value) => 
            Units.PixelsToHundredthsOfInch(value, this.Dpi);

        public override int LayoutUnitsToPixels(int value, float dpi) => 
            value;

        public override Point LayoutUnitsToPixels(Point value, float dpiX, float dpiY) => 
            value;

        public override Rectangle LayoutUnitsToPixels(Rectangle value, float dpiX, float dpiY) => 
            value;

        public override Size LayoutUnitsToPixels(Size value, float dpiX, float dpiY) => 
            value;

        public override float LayoutUnitsToPixelsF(float value, float dpi) => 
            value;

        public override float LayoutUnitsToPointsF(float value) => 
            Units.PixelsToPointsF(value, this.Dpi);

        public override int LayoutUnitsToTwips(int value) => 
            Units.PixelsToTwips(value, this.Dpi);

        public override long LayoutUnitsToTwips(long value) => 
            Units.PixelsToTwipsL(value, this.Dpi);

        public override float MillimetersToFontUnitsF(float value) => 
            Units.MillimetersToPointsF(value);

        public override int Pixels96DPIToLayoutUnits(int value, float dpi) => 
            Units.MulDiv(value, dpi, 0x60);

        public override float Pixels96DPIToLayoutUnitsF(float value, float dpi) => 
            Units.MulDivF(value, dpi, 96f);

        public override int PixelsToLayoutUnits(int value, float dpi) => 
            value;

        public override Rectangle PixelsToLayoutUnits(Rectangle value, float dpiX, float dpiY) => 
            value;

        public override Size PixelsToLayoutUnits(Size value, float dpiX, float dpiY) => 
            value;

        public override float PixelsToLayoutUnitsF(float value, float dpi) => 
            value;

        public override SizeF PixelsToLayoutUnitsF(SizeF value, float dpiX, float dpiY) => 
            value;

        public override int PointsToFontUnits(int value) => 
            value;

        public override float PointsToFontUnitsF(float value) => 
            value;

        public override int PointsToLayoutUnits(int value) => 
            Units.PointsToPixels(value, this.Dpi);

        public override float PointsToLayoutUnitsF(float value) => 
            Units.PointsToPixelsF(value, this.Dpi);

        public override int SnapToPixels(int value, float dpi) => 
            value;

        public override int TwipsToLayoutUnits(int value) => 
            Units.TwipsToPixels(value, this.Dpi);

        public override long TwipsToLayoutUnits(long value) => 
            Units.TwipsToPixelsL(value, this.Dpi);

        public override float Dpi =>
            this.dpi;

        public override LayoutGraphicsUnit GraphicsPageUnit =>
            LayoutGraphicsUnit.Pixel;

        public override float GraphicsPageScale =>
            1f;

        public override LayoutGraphicsUnit FontUnit =>
            LayoutGraphicsUnit.Point;

        public override float FontSizeScale =>
            72f / this.Dpi;
    }
}

