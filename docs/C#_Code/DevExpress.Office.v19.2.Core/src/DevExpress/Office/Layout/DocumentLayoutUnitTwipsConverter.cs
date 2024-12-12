namespace DevExpress.Office.Layout
{
    using DevExpress.Office.Utils;
    using System;
    using System.Drawing;

    public class DocumentLayoutUnitTwipsConverter : DocumentLayoutUnitConverter
    {
        public DocumentLayoutUnitTwipsConverter()
        {
        }

        public DocumentLayoutUnitTwipsConverter(float screenDpiX, float screenDpiY) : base(screenDpiX, screenDpiY)
        {
        }

        public override float DocumentsToFontUnitsF(float value) => 
            Units.DocumentsToPointsF(value);

        public override Rectangle DocumentsToLayoutUnits(Rectangle value) => 
            Units.DocumentsToTwips(value);

        public override RectangleF DocumentsToLayoutUnits(RectangleF value) => 
            Units.DocumentsToTwips(value);

        public override int DocumentsToLayoutUnits(int value) => 
            Units.DocumentsToTwips(value);

        public override float FontUnitsToLayoutUnitsF(float value) => 
            Units.PointsToTwipsF(value);

        public override float InchesToFontUnitsF(float value) => 
            Units.InchesToPointsF(value);

        public override Rectangle LayoutUnitsToDocuments(Rectangle value) => 
            Units.TwipsToDocuments(value);

        public override RectangleF LayoutUnitsToDocuments(RectangleF value) => 
            Units.TwipsToDocuments(value);

        public override int LayoutUnitsToDocuments(int value) => 
            Units.TwipsToDocuments(value);

        public override Size LayoutUnitsToHundredthsOfInch(Size value) => 
            Units.TwipsToHundredthsOfInch(value);

        public override int LayoutUnitsToHundredthsOfInch(int value) => 
            Units.TwipsToHundredthsOfInch(value);

        public override int LayoutUnitsToPixels(int value, float dpi) => 
            Units.TwipsToPixels(value, dpi);

        public override Point LayoutUnitsToPixels(Point value, float dpiX, float dpiY) => 
            Units.TwipsToPixels(value, dpiX, dpiY);

        public override Rectangle LayoutUnitsToPixels(Rectangle value, float dpiX, float dpiY) => 
            Units.TwipsToPixels(value, dpiX, dpiY);

        public override Size LayoutUnitsToPixels(Size value, float dpiX, float dpiY) => 
            Units.TwipsToPixels(value, dpiX, dpiY);

        public override float LayoutUnitsToPixelsF(float value, float dpi) => 
            Units.TwipsToPixelsF(value, dpi);

        public override float LayoutUnitsToPointsF(float value) => 
            Units.TwipsToPointsF(value);

        public override int LayoutUnitsToTwips(int value) => 
            value;

        public override long LayoutUnitsToTwips(long value) => 
            value;

        public override float MillimetersToFontUnitsF(float value) => 
            Units.MillimetersToPointsF(value);

        public override int Pixels96DPIToLayoutUnits(int value, float dpi) => 
            Units.PixelsToTwips(value, 96f);

        public override float Pixels96DPIToLayoutUnitsF(float value, float dpi) => 
            Units.PixelsToTwipsF(value, 96f);

        public override int PixelsToLayoutUnits(int value, float dpi) => 
            Units.PixelsToTwips(value, dpi);

        public override Rectangle PixelsToLayoutUnits(Rectangle value, float dpiX, float dpiY) => 
            Units.PixelsToTwips(value, dpiX, dpiY);

        public override Size PixelsToLayoutUnits(Size value, float dpiX, float dpiY) => 
            Units.PixelsToTwips(value, dpiX, dpiY);

        public override float PixelsToLayoutUnitsF(float value, float dpi) => 
            Units.PixelsToTwipsF(value, dpi);

        public override SizeF PixelsToLayoutUnitsF(SizeF value, float dpiX, float dpiY) => 
            Units.PixelsToTwipsF(value, dpiX, dpiY);

        public override int PointsToFontUnits(int value) => 
            value;

        public override float PointsToFontUnitsF(float value) => 
            value;

        public override int PointsToLayoutUnits(int value) => 
            Units.PointsToTwips(value);

        public override float PointsToLayoutUnitsF(float value) => 
            Units.PointsToTwipsF(value);

        public override int SnapToPixels(int value, float dpi) => 
            (int) Math.Round((double) ((this.Dpi * Math.Round((double) ((dpi * value) / this.Dpi))) / ((double) dpi)));

        public override int TwipsToLayoutUnits(int value) => 
            value;

        public override long TwipsToLayoutUnits(long value) => 
            value;

        public override float Dpi =>
            1440f;

        public override LayoutGraphicsUnit GraphicsPageUnit =>
            LayoutGraphicsUnit.Point;

        public override float GraphicsPageScale =>
            0.05f;

        public override LayoutGraphicsUnit FontUnit =>
            LayoutGraphicsUnit.Point;

        public override float FontSizeScale =>
            0.05f;
    }
}

