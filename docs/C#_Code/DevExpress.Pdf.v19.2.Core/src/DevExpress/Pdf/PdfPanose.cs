namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfPanose
    {
        private const int length = 10;
        public PdfPanoseFamilyKind FamilyKind { get; set; }
        public PdfPanoseSerifStyle SerifStyle { get; set; }
        public PdfPanoseWeight Weight { get; set; }
        public PdfPanoseProportion Proportion { get; set; }
        public PdfPanoseContrast Contrast { get; set; }
        public PdfPanoseStrokeVariation StrokeVariation { get; set; }
        public PdfPanoseArmStyle ArmStyle { get; set; }
        public PdfPanoseLetterform LetterForm { get; set; }
        public PdfPanoseMidline Midline { get; set; }
        public PdfPanoseXHeight XHeight { get; set; }
        internal bool IsDefault =>
            (this.FamilyKind == PdfPanoseFamilyKind.Any) && ((this.SerifStyle == PdfPanoseSerifStyle.Any) && ((this.Weight == PdfPanoseWeight.Any) && ((this.Proportion == PdfPanoseProportion.Any) && ((this.Contrast == PdfPanoseContrast.Any) && ((this.StrokeVariation == PdfPanoseStrokeVariation.Any) && ((this.ArmStyle == PdfPanoseArmStyle.Any) && ((this.LetterForm == PdfPanoseLetterform.Any) && ((this.Midline == PdfPanoseMidline.Any) && (this.XHeight == PdfPanoseXHeight.Any)))))))));
        internal PdfPanose(PdfBinaryStream stream)
        {
            this = new PdfPanose();
            byte[] buffer = stream.ReadArray(10);
            this.FamilyKind = (PdfPanoseFamilyKind) buffer[0];
            this.SerifStyle = (PdfPanoseSerifStyle) buffer[1];
            this.Weight = (PdfPanoseWeight) buffer[2];
            this.Proportion = (PdfPanoseProportion) buffer[3];
            this.Contrast = (PdfPanoseContrast) buffer[4];
            this.StrokeVariation = (PdfPanoseStrokeVariation) buffer[5];
            this.ArmStyle = (PdfPanoseArmStyle) buffer[6];
            this.LetterForm = (PdfPanoseLetterform) buffer[7];
            this.Midline = (PdfPanoseMidline) buffer[8];
            this.XHeight = (PdfPanoseXHeight) buffer[9];
        }

        internal void Write(PdfBinaryStream stream)
        {
            byte[] buffer1 = new byte[10];
            buffer1[0] = (byte) this.FamilyKind;
            buffer1[1] = (byte) this.SerifStyle;
            buffer1[2] = (byte) this.Weight;
            buffer1[3] = (byte) this.Proportion;
            buffer1[4] = (byte) this.Contrast;
            buffer1[5] = (byte) this.StrokeVariation;
            buffer1[6] = (byte) this.ArmStyle;
            buffer1[7] = (byte) this.LetterForm;
            buffer1[8] = (byte) this.Midline;
            buffer1[9] = (byte) this.XHeight;
            byte[] array = buffer1;
            stream.WriteArray(array);
        }
    }
}

