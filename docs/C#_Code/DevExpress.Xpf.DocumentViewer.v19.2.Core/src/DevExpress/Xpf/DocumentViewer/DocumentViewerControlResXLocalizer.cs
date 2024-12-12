namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Resources;

    public class DocumentViewerControlResXLocalizer : DXResXLocalizer<DocumentViewerStringId>
    {
        private ResourceManager resourceManager;

        public DocumentViewerControlResXLocalizer() : base(new DocumentViewerLocalizer())
        {
            this.resourceManager = new ResourceManager("DevExpress.Xpf.DocumentViewer.LocalizationRes", typeof(DocumentViewerControlResXLocalizer).Assembly);
        }

        protected override ResourceManager CreateResourceManagerCore()
        {
            ResourceManager resourceManager = this.resourceManager;
            if (this.resourceManager == null)
            {
                ResourceManager local1 = this.resourceManager;
                resourceManager = this.resourceManager = new ResourceManager("DevExpress.Xpf.DocumentViewer.LocalizationRes", typeof(DocumentViewerControlResXLocalizer).Assembly);
            }
            return resourceManager;
        }

        public override string GetLocalizedString(DocumentViewerStringId id) => 
            this.resourceManager.GetString("DocumentViewerStringId." + id) ?? string.Empty;
    }
}

