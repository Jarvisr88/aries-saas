namespace DevExpress.Office
{
    using DevExpress.Office.Utils;
    using System;
    using System.Drawing;

    public class DocumentModelUnitDocumentConverter : DocumentModelUnitConverter
    {
        public DocumentModelUnitDocumentConverter()
        {
        }

        public DocumentModelUnitDocumentConverter(float screenDpiX, float screenDpiY) : base(screenDpiX, screenDpiY)
        {
        }

        public override float CentimetersToModelUnitsF(float value) => 
            Units.CentimetersToDocumentsF(value);

        public override DocumentModelUnitToLayoutUnitConverter CreateConverterToLayoutUnits(DocumentLayoutUnit unit, float dpi)
        {
            switch (unit)
            {
                case DocumentLayoutUnit.Document:
                    return new DocumentModelDocumentsToLayoutDocumentsConverter();

                case DocumentLayoutUnit.Pixel:
                    return new DocumentModelDocumentsToLayoutPixelsConverter(dpi);
            }
            return new DocumentModelDocumentsToLayoutTwipsConverter();
        }

        public override Size DocumentsToModelUnits(Size value) => 
            value;

        public override int DocumentsToModelUnits(int value) => 
            value;

        public override float DocumentsToModelUnitsF(float value) => 
            value;

        public override int EmuToModelUnits(int value) => 
            Units.EmuToDocuments(value);

        public override float EmuToModelUnitsD(double value) => 
            Units.EmuToDocumentsD(value);

        public override float EmuToModelUnitsF(int value) => 
            Units.EmuToDocumentsF(value);

        public override long EmuToModelUnitsL(long value) => 
            Units.EmuToDocumentsL(value);

        public override int FDToModelUnits(int value) => 
            (value * 0xea60) / 0x10000;

        public override Size HundredthsOfInchToModelUnits(Size value) => 
            Units.HundredthsOfInchToDocuments(value);

        public override int HundredthsOfInchToModelUnits(int value) => 
            Units.HundredthsOfInchToDocuments(value);

        public override Size HundredthsOfMillimeterToModelUnits(Size value) => 
            Units.HundredthsOfMillimeterToDocuments(value);

        public override int HundredthsOfMillimeterToModelUnits(int value) => 
            Units.HundredthsOfMillimeterToDocuments(value);

        public override int HundredthsOfMillimeterToModelUnitsRound(int value) => 
            (int) Math.Round((double) (((double) (300 * value)) / 2540.0));

        public override float InchesToModelUnitsF(float value) => 
            Units.InchesToDocumentsF(value);

        public override float MillimetersToModelUnitsF(float value) => 
            Units.MillimetersToDocumentsF(value);

        public override float ModelUnitsToCentimetersF(float value) => 
            Units.DocumentsToCentimetersF(value);

        public override float ModelUnitsToDocumentsF(float value) => 
            value;

        public override int ModelUnitsToEmu(int value) => 
            Units.DocumentsToEmu(value);

        public override double ModelUnitsToEmuD(float value) => 
            Units.DocumentsToEmuD(value);

        public override int ModelUnitsToEmuF(float value) => 
            Units.DocumentsToEmuF(value);

        public override long ModelUnitsToEmuL(long value) => 
            Units.DocumentsToEmuL(value);

        public override int ModelUnitsToFD(int value) => 
            (value * 0x10000) / 0xea60;

        public override Size ModelUnitsToHundredthsOfInch(Size value) => 
            Units.DocumentsToHundredthsOfInch(value);

        public override int ModelUnitsToHundredthsOfInch(int value) => 
            Units.DocumentsToHundredthsOfInch(value);

        public override Size ModelUnitsToHundredthsOfMillimeter(Size value) => 
            Units.DocumentsToHundredthsOfMillimeter(value);

        public override float ModelUnitsToInchesF(float value) => 
            Units.DocumentsToInchesF(value);

        public override float ModelUnitsToMillimetersF(float value) => 
            Units.DocumentsToMillimetersF(value);

        public override int ModelUnitsToPixels(int value, float dpi) => 
            Units.DocumentsToPixels(value, dpi);

        public override float ModelUnitsToPixelsF(float value, float dpi) => 
            Units.DocumentsToPixelsF(value, dpi);

        public override float ModelUnitsToPointsF(float value) => 
            Units.DocumentsToPointsF(value);

        public override float ModelUnitsToPointsFRound(float value) => 
            Units.DocumentsToPointsFRound(value);

        public override Size ModelUnitsToTwips(Size value) => 
            Units.DocumentsToTwips(value);

        public override int ModelUnitsToTwips(int value) => 
            Units.DocumentsToTwips(value);

        public override float ModelUnitsToTwipsF(float value) => 
            Units.DocumentsToTwipsF(value);

        public override float PicasToModelUnitsF(float value) => 
            Units.PicasToDocumentsF(value);

        public override int PixelsToModelUnits(int value, float dpi) => 
            Units.PixelsToDocuments(value, dpi);

        public override Size PixelsToModelUnits(Size value, float dpiX, float dpiY) => 
            Units.PixelsToDocuments(value, dpiX, dpiY);

        public override int PointsToModelUnits(int value) => 
            Units.PointsToDocuments(value);

        public override float PointsToModelUnitsF(float value) => 
            Units.PointsToDocumentsF(value);

        public override int RoundModelUnitsToPixels(int value, float dpi) => 
            Units.RoundDocumentsToPixels(value, dpi);

        public override Size TwipsToModelUnits(Size value) => 
            Units.TwipsToDocuments(value);

        public override int TwipsToModelUnits(int value) => 
            Units.TwipsToDocuments(value);
    }
}

