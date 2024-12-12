namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Text.Fonts;
    using System;

    public static class TextWidthHelper
    {
        public static double GetTextWidth(string text, PdfExportFont exportFont, float fontSize, PdfInteractiveFormFieldTextState textState)
        {
            DXSizeF? size = null;
            return (double) MeasureString(text, size, exportFont, exportFont.Metrics, fontSize, textState).Width;
        }

        public static DXSizeF MeasureString(string text, DXSizeF? size, PdfExportFont exportFont, PdfFontMetrics metrics, float fontSize, PdfInteractiveFormFieldTextState textState)
        {
            PdfStringFormat genericTypographic = PdfStringFormat.GenericTypographic;
            genericTypographic.FormatFlags = 0;
            return DXLineFormatter.MeasureText(text, size, metrics, fontSize, genericTypographic, 0f, new PdfWidgetShaper(exportFont, textState), DXKerningMode.None).Size;
        }
    }
}

