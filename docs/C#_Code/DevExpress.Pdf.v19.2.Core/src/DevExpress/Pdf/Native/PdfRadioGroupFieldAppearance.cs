namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfRadioGroupFieldAppearance
    {
        private readonly bool shouldGeneratePressedAppearance;
        private readonly PdfAcroFormButtonStyle buttonStyle;
        private readonly PdfAcroFormFieldAppearance appearance;

        public PdfRadioGroupFieldAppearance(PdfAcroFormRadioGroupField field)
        {
            this.shouldGeneratePressedAppearance = field.ShouldGeneratePressedAppearance;
            this.buttonStyle = field.ButtonStyle;
            this.appearance = field.Appearance;
        }

        public PdfRadioGroupFieldAppearance(bool shouldGeneratePressedAppearance, PdfAcroFormButtonStyle buttonStyle, PdfAcroFormFieldAppearance appearance)
        {
            this.shouldGeneratePressedAppearance = shouldGeneratePressedAppearance;
            this.buttonStyle = buttonStyle;
            this.appearance = appearance;
        }

        public bool ShouldGeneratePressedAppearance =>
            this.shouldGeneratePressedAppearance;

        public PdfAcroFormButtonStyle ButtonStyle =>
            this.buttonStyle;

        public PdfAcroFormFieldAppearance Appearance =>
            this.appearance;
    }
}

