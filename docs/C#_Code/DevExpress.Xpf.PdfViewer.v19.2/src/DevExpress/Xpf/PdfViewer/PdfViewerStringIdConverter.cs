namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Utils.Localization;
    using DevExpress.Xpf.Core;

    public class PdfViewerStringIdConverter : StringIdConverter<PdfViewerStringId>
    {
        protected override XtraLocalizer<PdfViewerStringId> Localizer =>
            PdfViewerLocalizer.Active;
    }
}

