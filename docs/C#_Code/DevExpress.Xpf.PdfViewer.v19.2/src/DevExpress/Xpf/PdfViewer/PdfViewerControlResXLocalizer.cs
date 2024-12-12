namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Resources;

    public class PdfViewerControlResXLocalizer : DXResXLocalizer<PdfViewerStringId>
    {
        private ResourceManager resourceManager;

        public PdfViewerControlResXLocalizer() : base(new PdfViewerLocalizer())
        {
            this.resourceManager = new ResourceManager("DevExpress.Xpf.PdfViewer.LocalizationRes", typeof(PdfViewerControlResXLocalizer).Assembly);
        }

        protected override ResourceManager CreateResourceManagerCore()
        {
            ResourceManager resourceManager = this.resourceManager;
            if (this.resourceManager == null)
            {
                ResourceManager local1 = this.resourceManager;
                resourceManager = this.resourceManager = new ResourceManager("DevExpress.Xpf.PdfViewer.LocalizationRes", typeof(PdfViewerControlResXLocalizer).Assembly);
            }
            return resourceManager;
        }

        public override string GetLocalizedString(PdfViewerStringId id) => 
            this.resourceManager.GetString("PdfViewerStringId." + id) ?? string.Empty;
    }
}

