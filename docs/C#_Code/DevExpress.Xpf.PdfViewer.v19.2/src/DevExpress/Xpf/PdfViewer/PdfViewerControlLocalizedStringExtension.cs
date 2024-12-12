namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Windows.Markup;

    public class PdfViewerControlLocalizedStringExtension : MarkupExtension
    {
        private readonly PdfViewerStringId stringID;

        public PdfViewerControlLocalizedStringExtension(PdfViewerStringId stringID)
        {
            this.stringID = stringID;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            PdfViewerLocalizer.GetString(this.stringID);
    }
}

