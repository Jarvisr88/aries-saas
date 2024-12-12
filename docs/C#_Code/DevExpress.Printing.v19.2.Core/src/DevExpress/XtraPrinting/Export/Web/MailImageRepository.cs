namespace DevExpress.XtraPrinting.Export.Web
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class MailImageRepository : ImageRepositoryBase
    {
        internal const string ContentIdPrefix = "cid:";
        private Dictionary<long, DevExpress.XtraPrinting.Export.Web.ImageInfo> imageTable = new Dictionary<long, DevExpress.XtraPrinting.Export.Web.ImageInfo>();

        protected override void AddImageToTable(Image image, long key, string url, bool autoDisposeImage)
        {
            if ((image.Size.Height != 1) || (image.Size.Width != 1))
            {
                this.imageTable.Add(key, new DevExpress.XtraPrinting.Export.Web.ImageInfo(image, GetImageContentId(url), autoDisposeImage));
            }
        }

        protected override bool ContainsImageKey(long key) => 
            this.imageTable.ContainsKey(key);

        public override void Dispose()
        {
            foreach (DevExpress.XtraPrinting.Export.Web.ImageInfo info in this.ImagesTable.Values)
            {
                info.FinalizeImage();
            }
        }

        protected override void FinalizeImage(Image image, bool autoDisposeImage)
        {
        }

        protected override string FormatImageURL(string imageFileName) => 
            "cid:" + imageFileName;

        internal static string GetImageContentId(string url) => 
            url.Substring("cid:".Length);

        protected override string GetUrl(long key)
        {
            DevExpress.XtraPrinting.Export.Web.ImageInfo info;
            return (!this.imageTable.TryGetValue(key, out info) ? string.Empty : ("cid:" + info.ContentId));
        }

        protected override void SaveImage(Image image, string fileName)
        {
        }

        public Dictionary<long, DevExpress.XtraPrinting.Export.Web.ImageInfo> ImagesTable =>
            this.imageTable;
    }
}

