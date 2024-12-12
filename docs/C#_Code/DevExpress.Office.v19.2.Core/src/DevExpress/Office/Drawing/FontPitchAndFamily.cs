namespace DevExpress.Office.Drawing
{
    using System;

    [Flags]
    public enum FontPitchAndFamily : byte
    {
        DefaultPitch = 0,
        FixedPitch = 1,
        VariablePitch = 2,
        DontCare = 0,
        Roman = 0x10,
        Swiss = 0x20,
        Modern = 0x30,
        Script = 0x40,
        Decorative = 80
    }
}

