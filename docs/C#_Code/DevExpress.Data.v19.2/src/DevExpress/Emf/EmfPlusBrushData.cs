namespace DevExpress.Emf
{
    using System;

    [Flags]
    public enum EmfPlusBrushData
    {
        BrushDataPath = 1,
        BrushDataTransform = 2,
        BrushDataPresetColors = 4,
        BrushDataBlendFactors = 8,
        BrushDataFocusScales = 0x40,
        BrushDataIsGammaCorrected = 0x80,
        BrushDataDoNotTransform = 0x100
    }
}

