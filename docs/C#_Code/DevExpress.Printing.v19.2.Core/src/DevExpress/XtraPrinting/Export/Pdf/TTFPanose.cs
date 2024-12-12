namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TTFPanose
    {
        public byte bFamilyType;
        public byte bSerifType;
        public byte bWeight;
        public byte bProportion;
        public byte bContrast;
        public byte bStrokeVariation;
        public byte bArmStyle;
        public byte bLetterForm;
        public byte bMidline;
        public byte bXHeight;
    }
}

