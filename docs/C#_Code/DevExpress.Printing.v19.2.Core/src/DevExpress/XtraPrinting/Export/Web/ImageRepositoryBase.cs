namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public abstract class ImageRepositoryBase : ImageRepositoryRequest, IImageRepository, IDisposable
    {
        protected ImageRepositoryBase()
        {
        }

        protected abstract void AddImageToTable(Image image, long key, string url, bool autoDisposeImage);
        protected abstract bool ContainsImageKey(long key);
        string IImageRepository.GetImageSource(Image img, bool autoDisposeImage)
        {
            if (img == null)
            {
                return string.Empty;
            }
            base.RaiseRequestImageSource(img);
            long imageHashCode = HtmlImageHelper.GetImageHashCode(img);
            if (this.ContainsImageKey(imageHashCode))
            {
                return this.GetUrl(imageHashCode);
            }
            try
            {
                string mimeType = HtmlImageHelper.GetMimeType(img);
                string fileName = $"{imageHashCode}.{mimeType}";
                this.SaveImage(img, fileName);
                string url = this.FormatImageURL(fileName);
                this.AddImageToTable(img, imageHashCode, url, autoDisposeImage);
                this.FinalizeImage(img, autoDisposeImage);
                return url;
            }
            catch
            {
                return string.Empty;
            }
        }

        public abstract void Dispose();
        protected abstract void FinalizeImage(Image image, bool autoDisposeImage);
        protected abstract string FormatImageURL(string imageFileName);
        protected abstract string GetUrl(long key);
        protected abstract void SaveImage(Image image, string fileName);
    }
}

