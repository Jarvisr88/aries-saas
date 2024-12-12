namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm;
    using DevExpress.Utils;
    using System;

    internal class PreviewModelWrapper : IPreviewModelWrapper, IDisposable
    {
        private readonly LinkPreviewModel previewModel;

        public PreviewModelWrapper(LinkPreviewModel previewModel)
        {
            Guard.ArgumentNotNull(previewModel, "previewModel");
            this.previewModel = previewModel;
        }

        public void Dispose()
        {
            LinkBase link = this.previewModel.Link;
            this.previewModel.Dispose();
            link.Dispose();
        }

        public object PreviewModel =>
            this.previewModel;
    }
}

