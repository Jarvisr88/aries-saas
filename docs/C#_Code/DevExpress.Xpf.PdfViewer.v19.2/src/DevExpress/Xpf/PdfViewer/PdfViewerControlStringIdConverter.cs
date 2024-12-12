namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Utils.Localization;
    using DevExpress.Xpf.Core;

    public class PdfViewerControlStringIdConverter : StringIdConverter<PdfViewerStringId>
    {
        protected override XtraLocalizer<PdfViewerStringId> Localizer =>
            PdfViewerLocalizer.Active;
    }
}

