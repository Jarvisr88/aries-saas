namespace DevExpress.Office.Utils
{
    using System;

    public enum OfficeColorTransform
    {
        None = 0,
        Darken = 0x100,
        Lighten = 0x200,
        AddGray = 0x300,
        SubtractGray = 0x400,
        ReverseSubtractGray = 0x500,
        Threshold = 0x600,
        Invert = 0x2000,
        ToggleHighBit = 0x4000,
        ConvertToGrayscale = 0x8000
    }
}

