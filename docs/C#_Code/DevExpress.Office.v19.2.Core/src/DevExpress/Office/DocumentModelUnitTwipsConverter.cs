namespace DevExpress.Office
{
    using DevExpress.Office.Utils;
    using System;
    using System.Drawing;

    public class DocumentModelUnitTwipsConverter : DocumentModelUnitConverter
    {
        public DocumentModelUnitTwipsConverter()
        {
        }

        public DocumentModelUnitTwipsConverter(float screenDpiX, float screenDpiY) : base(screenDpiX, screenDpiY)
        {
        }

        public override float CentimetersToModelUnitsF(float value) => 
            Units.CentimetersToTwipsF(value);

        public override DocumentModelUnitToLayoutUnitConverter CreateConverterToLayoutUnits(DocumentLayoutUnit unit, float dpi)
        {
            switch (unit)
            {
                case DocumentLayoutUnit.Document:
                    return new DocumentModelTwipsToLayoutDocumentsConverter();

                case DocumentLayoutUnit.Pixel:
                    return new DocumentModelTwipsToLayoutPixelsConverter(dpi);
            }
            return new DocumentModelTwipsToLayoutTwipsConverter();
        }

        public override Size DocumentsToModelUnits(Size value) => 
            Units.DocumentsToTwips(value);

        public override int DocumentsToModelUnits(int value) => 
            Units.DocumentsToTwips(value);

        public override float DocumentsToModelUnitsF(float value) => 
            Units.DocumentsToTwipsF(value);

        public override int EmuToModelUnits(int value) => 
            Units.EmuToTwips(value);

        public override float EmuToModelUnitsD(double value) => 
            Units.EmuToTwipsD(value);

        public override float EmuToModelUnitsF(int value) => 
            Units.EmuToTwipsF(value);

        public override long EmuToModelUnitsL(long value) => 
            Units.EmuToTwipsL(value);

        public override int FDToModelUnits(int value) => 
            (int) ((value * 0x753L) / 0x800L);

        public override Size HundredthsOfInchToModelUnits(Size value) => 
            Units.HundredthsOfInchToTwips(value);

        public override int HundredthsOfInchToModelUnits(int value) => 
            Units.HundredthsOfInchToTwips(value);

        public override Size HundredthsOfMillimeterToModelUnits(Size value) => 
            Units.HundredthsOfMillimeterToTwips(value);

        public override int HundredthsOfMillimeterToModelUnits(int value) => 
            Units.HundredthsOfMillimeterToTwips(value);

        public override int HundredthsOfMillimeterToModelUnitsRound(int value) => 
            (int) Math.Round((double) (((double) (0x5a0 * value)) / 2540.0));

        public override float InchesToModelUnitsF(float value) => 
            Units.InchesToTwipsF(value);

        public override float MillimetersToModelUnitsF(float value) => 
            Units.MillimetersToTwipsF(value);

        public override float ModelUnitsToCentimetersF(float value) => 
            Units.TwipsToCentimetersF(value);

        public override float ModelUnitsToDocumentsF(float value) => 
            Units.TwipsToDocumentsF(value);

        public override int ModelUnitsToEmu(int value) => 
            Units.TwipsToEmu(value);

        public override double ModelUnitsToEmuD(float value) => 
            Units.TwipsToEmuD(value);

        public override int ModelUnitsToEmuF(float value) => 
            Units.TwipsToEmuF(value);

        public override long ModelUnitsToEmuL(long value) => 
            Units.TwipsToEmuL(value);

        public override int ModelUnitsToFD(int value) => 
            (int) ((value * 0x800L) / 0x753L);

        public override Size ModelUnitsToHundredthsOfInch(Size value) => 
            Units.TwipsToHundredthsOfInch(value);

        public override int ModelUnitsToHundredthsOfInch(int value) => 
            Units.TwipsToHundredthsOfInch(value);

        public override Size ModelUnitsToHundredthsOfMillimeter(Size value) => 
            Units.TwipsToHundredthsOfMillimeter(value);

        public override float ModelUnitsToInchesF(float value) => 
            Units.TwipsToInchesF(value);

        public override float ModelUnitsToMillimetersF(float value) => 
            Units.TwipsToMillimetersF(value);

        public override int ModelUnitsToPixels(int value, float dpi) => 
            Units.TwipsToPixels(value, dpi);

        public override float ModelUnitsToPixelsF(float value, float dpi) => 
            Units.TwipsToPixelsF(value, dpi);

        public override float ModelUnitsToPointsF(float value) => 
            Units.TwipsToPointsF(value);

        public override float ModelUnitsToPointsFRound(float value) => 
            Units.TwipsToPointsFRound(value);

        public override Size ModelUnitsToTwips(Size value) => 
            value;

        public override int ModelUnitsToTwips(int value) => 
            value;

        public override float ModelUnitsToTwipsF(float value) => 
            value;

        public override float PicasToModelUnitsF(float value) => 
            Units.PicasToTwipsF(value);

        public override int PixelsToModelUnits(int value, float dpi) => 
            Units.PixelsToTwips(value, dpi);

        public override Size PixelsToModelUnits(Size value, float dpiX, float dpiY) => 
            Units.PixelsToTwips(value, dpiX, dpiY);

        public override int PointsToModelUnits(int value) => 
            Units.PointsToTwips(value);

        public override float PointsToModelUnitsF(float value) => 
            Units.PointsToTwipsF(value);

        public override int RoundModelUnitsToPixels(int value, float dpi) => 
            Units.RoundTwipsToPixels(value, dpi);

        public override Size TwipsToModelUnits(Size value) => 
            value;

        public override int TwipsToModelUnits(int value) => 
            value;
    }
}

