namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;

    public interface IPdfWidgetAnnotationBuilder : IPdfAnnotationBuilder
    {
        PdfWidgetAppearanceCharacteristics CreateAppearanceCharacteristics();
        PdfAnnotationBorderStyle CreateBorderStyle();
    }
}

