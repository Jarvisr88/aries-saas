namespace DevExpress.Office
{
    using System;
    using System.Drawing;

    public interface IDocumentModelUnitConverter
    {
        float CentimetersToModelUnitsF(float value);
        int DegreeToModelUnits(float value);
        Size DocumentsToModelUnits(Size value);
        int DocumentsToModelUnits(int value);
        float DocumentsToModelUnitsF(float value);
        int EmuToModelUnits(int value);
        int FDToModelUnits(int value);
        Size HundredthsOfInchToModelUnits(Size value);
        int HundredthsOfInchToModelUnits(int value);
        Size HundredthsOfMillimeterToModelUnits(Size value);
        int HundredthsOfMillimeterToModelUnits(int value);
        int HundredthsOfMillimeterToModelUnitsRound(int value);
        float InchesToModelUnitsF(float value);
        float MillimetersToModelUnitsF(float value);
        float ModelUnitsToCentimetersF(float value);
        int ModelUnitsToDegree(int value);
        float ModelUnitsToDocumentsF(float value);
        int ModelUnitsToFD(int value);
        Size ModelUnitsToHundredthsOfInch(Size value);
        int ModelUnitsToHundredthsOfInch(int value);
        Size ModelUnitsToHundredthsOfMillimeter(Size value);
        float ModelUnitsToInchesF(float value);
        float ModelUnitsToMillimetersF(float value);
        int ModelUnitsToPixels(int value, float dpi);
        float ModelUnitsToPixelsF(float value, float dpi);
        float ModelUnitsToPointsF(float value);
        float ModelUnitsToPointsFRound(float value);
        Size ModelUnitsToTwips(Size value);
        int ModelUnitsToTwips(int value);
        float ModelUnitsToTwipsF(float value);
        float PicasToModelUnitsF(float value);
        int PixelsToModelUnits(int value, float dpi);
        Size PixelsToModelUnits(Size value, float dpiX, float dpiY);
        int PointsToModelUnits(int value);
        float PointsToModelUnitsF(float value);
        Size TwipsToModelUnits(Size value);
        int TwipsToModelUnits(int value);
    }
}

