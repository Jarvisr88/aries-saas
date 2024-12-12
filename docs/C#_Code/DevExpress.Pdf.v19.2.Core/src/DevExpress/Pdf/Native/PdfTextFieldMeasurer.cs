namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Text.Fonts;
    using System;

    public class PdfTextFieldMeasurer
    {
        private readonly PdfStringFormat format;
        private readonly PdfInteractiveFormField formField;
        private readonly PdfTextFormFieldAppearanceBuilder builder;
        private readonly bool multiline;

        public PdfTextFieldMeasurer(PdfStringFormat format, PdfInteractiveFormField formField, PdfTextFormFieldAppearanceBuilder builder, bool multiline)
        {
            this.format = format;
            this.formField = formField;
            this.builder = builder;
            this.multiline = multiline;
        }

        public float CalcFontSize(string newValue, PdfExportFont fontData, float currentFontSize) => 
            (float) this.formField.CalcFontSize(newValue, this.builder.GetActualContentBounds(currentFontSize).Item2, fontData);

        public bool IsTextFit(string text, PdfExportFontInfo fontInfo)
        {
            this.format.FormatFlags &= ~PdfStringFormatFlags.NoWrap;
            Tuple<PdfFontMetrics, PdfRectangle> actualContentBounds = this.builder.GetActualContentBounds(fontInfo.FontSize);
            PdfRectangle rectangle = actualContentBounds.Item2;
            PdfFontMetrics metrics = actualContentBounds.Item1;
            if (this.multiline)
            {
                if (this.MeasureString(text, fontInfo, metrics, this.format, new DXSizeF((float) rectangle.Width, float.MaxValue)).Height <= rectangle.Height)
                {
                    return true;
                }
                if ((metrics.GetAscent((double) fontInfo.FontSize) + metrics.GetDescent((double) fontInfo.FontSize)) < rectangle.Height)
                {
                    return false;
                }
            }
            return (this.MeasureString(text, fontInfo, metrics, this.format, new DXSizeF(float.MaxValue, float.MaxValue)).Width <= rectangle.Width);
        }

        private DXSizeF MeasureString(string text, PdfExportFontInfo fontInfo, PdfFontMetrics metrics, PdfStringFormat format, DXSizeF size) => 
            DXLineFormatter.MeasureText(text, new DXSizeF?(size), metrics, fontInfo.FontSize, format, 0f, new PdfWidgetShaper(fontInfo.Font, this.formField.TextState), DXKerningMode.None).Size;
    }
}

