namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfButtonFormFieldFadedAppearanceBuilder : PdfButtonFormFieldAppearanceBuilder
    {
        private const double fadeFactor = 0.25098039215686274;

        public PdfButtonFormFieldFadedAppearanceBuilder(PdfWidgetAnnotation widget, PdfButtonFormField buttonFormField, PdfRadioGroupFieldAppearance field, bool isChecked) : base(widget, buttonFormField, field, isChecked)
        {
        }

        public PdfButtonFormFieldFadedAppearanceBuilder(PdfWidgetAnnotation widget, PdfButtonFormField buttonFormField, PdfAcroFormCheckBoxField field, bool isChecked) : base(widget, buttonFormField, field, isChecked)
        {
        }

        protected override void DrawBeveledBorder(PdfFormCommandConstructor constructor)
        {
            this.DrawInsetBorder(constructor);
        }

        private static double FadeColorComponent(double colorComponent) => 
            PdfMathUtils.Max(0.0, colorComponent - 0.25098039215686274);

        private static PdfRgbaColor GetFadedColor(PdfRgbaColor color)
        {
            color ??= new PdfRgbaColor(1.0, 1.0, 1.0, 1.0);
            return new PdfRgbaColor(FadeColorComponent(color.R), FadeColorComponent(color.G), FadeColorComponent(color.B), color.A);
        }

        protected override PdfRgbaColor BackgroundColor =>
            GetFadedColor(base.BackgroundColor);
    }
}

