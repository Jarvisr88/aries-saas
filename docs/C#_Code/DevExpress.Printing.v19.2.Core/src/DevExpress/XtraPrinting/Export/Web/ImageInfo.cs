namespace DevExpress.XtraPrinting.Export.Web
{
    using System;
    using System.Drawing;

    public class ImageInfo
    {
        private System.Drawing.Image image;
        private string contentId;
        private bool disposeImage;

        public ImageInfo(System.Drawing.Image img, string contentId, bool disposeImage)
        {
            this.image = img;
            this.contentId = contentId;
            this.disposeImage = disposeImage;
        }

        public void FinalizeImage()
        {
            if (this.disposeImage && (this.image != null))
            {
                this.image.Dispose();
                this.image = null;
            }
        }

        public System.Drawing.Image Image =>
            this.image;

        public string ContentId =>
            this.contentId;
    }
}

