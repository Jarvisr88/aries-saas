namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Windows.Markup;

    public class DocumentViewerControlLocalizedStringExtension : MarkupExtension
    {
        private readonly DocumentViewerStringId stringID;

        public DocumentViewerControlLocalizedStringExtension(DocumentViewerStringId stringID)
        {
            this.stringID = stringID;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            DocumentViewerLocalizer.GetString(this.stringID);
    }
}

