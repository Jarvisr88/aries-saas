namespace DevExpress.XtraPrinting.Export.Web
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class InMemoryHtmlImageRepository : ImageRepositoryBase
    {
        private Dictionary<long, string> imageUrlTable = new Dictionary<long, string>();
        protected string imagePath = "";

        public InMemoryHtmlImageRepository(string imagePath)
        {
            this.imagePath = imagePath;
        }

        protected override void AddImageToTable(Image image, long key, string url, bool autoDisposeImage)
        {
            this.imageUrlTable.Add(key, url);
        }

        protected override bool ContainsImageKey(long key) => 
            this.imageUrlTable.ContainsKey(key);

        public override void Dispose()
        {
        }

        protected override void FinalizeImage(Image image, bool autoDisposeImage)
        {
            if (autoDisposeImage)
            {
                image.Dispose();
            }
        }

        protected override string FormatImageURL(string imageFileName) => 
            this.imagePath + "/" + imageFileName;

        protected override string GetUrl(long key)
        {
            string str;
            return (!this.imageUrlTable.TryGetValue(key, out str) ? string.Empty : str);
        }

        protected override void SaveImage(Image image, string fileName)
        {
        }
    }
}

