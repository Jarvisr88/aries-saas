namespace DevExpress.Office
{
    using System;
    using System.Drawing;

    public abstract class DocumentModelUnitConverter : DpiSupport, IDocumentModelUnitConverter
    {
        protected DocumentModelUnitConverter()
        {
        }

        protected DocumentModelUnitConverter(float screenDpiX, float screenDpiY) : base(screenDpiX, screenDpiY)
        {
        }

        public int AdjAngleToModelUnits(int value) => 
            value;

        public int CeilingModelUnitsToEmu(float value) => 
            (int) Math.Ceiling(this.ModelUnitsToEmuD(value));

        public long CeilingModelUnitsToEmuL(float value) => 
            (long) Math.Ceiling(this.ModelUnitsToEmuD(value));

        public abstract float CentimetersToModelUnitsF(float value);
        public DocumentModelUnitToLayoutUnitConverter CreateConverterToLayoutUnits(DocumentLayoutUnit unit) => 
            this.CreateConverterToLayoutUnits(unit, base.ScreenDpi);

        public abstract DocumentModelUnitToLayoutUnitConverter CreateConverterToLayoutUnits(DocumentLayoutUnit unit, float dpi);
        public int DegreeToModelUnits(float value) => 
            (int) Math.Round((double) (value * 60000f));

        public abstract Size DocumentsToModelUnits(Size value);
        public abstract int DocumentsToModelUnits(int value);
        public abstract float DocumentsToModelUnitsF(float value);
        public abstract int EmuToModelUnits(int value);
        public abstract float EmuToModelUnitsD(double value);
        public abstract float EmuToModelUnitsF(int value);
        public abstract long EmuToModelUnitsL(long value);
        public abstract int FDToModelUnits(int value);
        public abstract Size HundredthsOfInchToModelUnits(Size value);
        public abstract int HundredthsOfInchToModelUnits(int value);
        public abstract Size HundredthsOfMillimeterToModelUnits(Size value);
        public abstract int HundredthsOfMillimeterToModelUnits(int value);
        public abstract int HundredthsOfMillimeterToModelUnitsRound(int value);
        public abstract float InchesToModelUnitsF(float value);
        public abstract float MillimetersToModelUnitsF(float value);
        public int ModelUnitsToAdjAngle(int value) => 
            value;

        public abstract float ModelUnitsToCentimetersF(float value);
        public int ModelUnitsToDegree(int value) => 
            value / 0xea60;

        public float ModelUnitsToDegreeF(int value) => 
            ((float) value) / 60000f;

        public abstract float ModelUnitsToDocumentsF(float value);
        public abstract int ModelUnitsToEmu(int value);
        public abstract double ModelUnitsToEmuD(float value);
        public abstract int ModelUnitsToEmuF(float value);
        public abstract long ModelUnitsToEmuL(long value);
        public abstract int ModelUnitsToFD(int value);
        public abstract Size ModelUnitsToHundredthsOfInch(Size value);
        public abstract int ModelUnitsToHundredthsOfInch(int value);
        public abstract Size ModelUnitsToHundredthsOfMillimeter(Size value);
        public abstract float ModelUnitsToInchesF(float value);
        public abstract float ModelUnitsToMillimetersF(float value);
        public int ModelUnitsToPixels(int value) => 
            this.ModelUnitsToPixels(value, base.ScreenDpi);

        public abstract int ModelUnitsToPixels(int value, float dpi);
        public float ModelUnitsToPixelsF(float value) => 
            this.ModelUnitsToPixelsF(value, base.ScreenDpi);

        public abstract float ModelUnitsToPixelsF(float value, float dpi);
        public abstract float ModelUnitsToPointsF(float value);
        public abstract float ModelUnitsToPointsFRound(float value);
        public double ModelUnitsToRadians(int value) => 
            ((3.1415926535897931 * value) / 60000.0) / 180.0;

        public abstract Size ModelUnitsToTwips(Size value);
        public abstract int ModelUnitsToTwips(int value);
        public abstract float ModelUnitsToTwipsF(float value);
        public abstract float PicasToModelUnitsF(float value);
        public Size PixelsToModelUnits(Size value) => 
            this.PixelsToModelUnits(value, base.ScreenDpiX, base.ScreenDpiY);

        public int PixelsToModelUnits(int value) => 
            this.PixelsToModelUnits(value, base.ScreenDpi);

        public abstract int PixelsToModelUnits(int value, float dpi);
        public abstract Size PixelsToModelUnits(Size value, float dpiX, float dpiY);
        public abstract int PointsToModelUnits(int value);
        public abstract float PointsToModelUnitsF(float value);
        public int RadiansToModelUnits(double value) => 
            (int) Math.Round((double) (((value * 60000.0) * 180.0) / 3.1415926535897931));

        public abstract int RoundModelUnitsToPixels(int value, float dpi);
        public abstract Size TwipsToModelUnits(Size value);
        public abstract int TwipsToModelUnits(int value);
    }
}

