namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using System;

    public interface IPdfExportFontProvider
    {
        PdfExportFont GetMatchingFont(PdfSetTextFontCommand setTextFontCommand);
        PdfExportFont GetMatchingFont(string fontFamily, PdfFontStyle style);
    }
}

