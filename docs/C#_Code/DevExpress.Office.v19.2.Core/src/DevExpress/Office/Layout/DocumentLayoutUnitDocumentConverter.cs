namespace DevExpress.Office.Layout
{
    using DevExpress.Office.Utils;
    using System;
    using System.Drawing;

    public class DocumentLayoutUnitDocumentConverter : DocumentLayoutUnitConverter
    {
        public DocumentLayoutUnitDocumentConverter()
        {
        }

        public DocumentLayoutUnitDocumentConverter(float screenDpiX, float screenDpiY) : base(screenDpiX, screenDpiY)
        {
        }

        public override float DocumentsToFontUnitsF(float value) => 
            value;

        public override Rectangle DocumentsToLayoutUnits(Rectangle value) => 
            value;

        public override RectangleF DocumentsToLayoutUnits(RectangleF value) => 
            value;

        public override int DocumentsToLayoutUnits(int value) => 
            value;

        public override float FontUnitsToLayoutUnitsF(float value) => 
            value;

        public override float InchesToFontUnitsF(float value) => 
            Units.InchesToDocumentsF(value);

        public override Rectangle LayoutUnitsToDocuments(Rectangle value) => 
            value;

        public override RectangleF LayoutUnitsToDocuments(RectangleF value) => 
            value;

        public override int LayoutUnitsToDocuments(int value) => 
            value;

        public override Size LayoutUnitsToHundredthsOfInch(Size value) => 
            Units.DocumentsToHundredthsOfInch(value);

        public override int LayoutUnitsToHundredthsOfInch(int value) => 
            Units.DocumentsToHundredthsOfInch(value);

        public override int LayoutUnitsToPixels(int value, float dpi) => 
            Units.DocumentsToPixels(value, dpi);

        public override Point LayoutUnitsToPixels(Point value, float dpiX, float dpiY) => 
            Units.DocumentsToPixels(value, dpiX, dpiY);

        public override Rectangle LayoutUnitsToPixels(Rectangle value, float dpiX, float dpiY) => 
            Units.DocumentsToPixels(value, dpiX, dpiY);

        public override Size LayoutUnitsToPixels(Size value, float dpiX, float dpiY) => 
            Units.DocumentsToPixels(value, dpiX, dpiY);

        public override float LayoutUnitsToPixelsF(float value, float dpi) => 
            Units.DocumentsToPixelsF(value, dpi);

        public override float LayoutUnitsToPointsF(float value) => 
            Units.DocumentsToPointsF(value);

        public override int LayoutUnitsToTwips(int value) => 
            Units.DocumentsToTwips(value);

        public override long LayoutUnitsToTwips(long value) => 
            Units.DocumentsToTwipsL(value);

        public override float MillimetersToFontUnitsF(float value) => 
            Units.MillimetersToDocumentsF(value);

        public override int Pixels96DPIToLayoutUnits(int value, float dpi) => 
            Units.PixelsToDocuments(value, 96f);

        public override float Pixels96DPIToLayoutUnitsF(float value, float dpi) => 
            Units.PixelsToDocumentsF(value, 96f);

        public override int PixelsToLayoutUnits(int value, float dpi) => 
            Units.PixelsToDocuments(value, dpi);

        public override Rectangle PixelsToLayoutUnits(Rectangle value, float dpiX, float dpiY) => 
            Units.PixelsToDocuments(value, dpiX, dpiY);

        public override Size PixelsToLayoutUnits(Size value, float dpiX, float dpiY) => 
            Units.PixelsToDocuments(value, dpiX, dpiY);

        public override float PixelsToLayoutUnitsF(float value, float dpi) => 
            Units.PixelsToDocumentsF(value, dpi);

        public override SizeF PixelsToLayoutUnitsF(SizeF value, float dpiX, float dpiY) => 
            Units.PixelsToDocumentsF(value, dpiX, dpiY);

        public override int PointsToFontUnits(int value) => 
            Units.PointsToDocuments(value);

        public override float PointsToFontUnitsF(float value) => 
            Units.PointsToDocumentsF(value);

        public override int PointsToLayoutUnits(int value) => 
            Units.PointsToDocuments(value);

        public override float PointsToLayoutUnitsF(float value) => 
            Units.PointsToDocumentsF(value);

        public override int SnapToPixels(int value, float dpi) => 
            value;

        public override int TwipsToLayoutUnits(int value) => 
            Units.TwipsToDocuments(value);

        public override long TwipsToLayoutUnits(long value) => 
            Units.TwipsToDocumentsL(value);

        public override float Dpi =>
            300f;

        public override LayoutGraphicsUnit GraphicsPageUnit =>
            LayoutGraphicsUnit.Document;

        public override float GraphicsPageScale =>
            1f;

        public override LayoutGraphicsUnit FontUnit =>
            LayoutGraphicsUnit.Document;

        public override float FontSizeScale =>
            1f;
    }
}

