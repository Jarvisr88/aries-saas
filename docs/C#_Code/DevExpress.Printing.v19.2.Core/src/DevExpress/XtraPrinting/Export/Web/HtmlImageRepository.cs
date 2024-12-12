namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.IO;

    public class HtmlImageRepository : InMemoryHtmlImageRepository
    {
        private string rootPath;

        public HtmlImageRepository(string rootPath, string imagePath) : base(imagePath)
        {
            this.rootPath = "";
            this.rootPath = rootPath;
        }

        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                File.SetAttributes(path, FileAttributes.Archive);
            }
        }

        protected override void SaveImage(Image image, string fileName)
        {
            string path = Path.Combine(this.rootPath, base.imagePath);
            CreateDirectory(path);
            HtmlImageHelper.SaveImage(image, Path.Combine(path, fileName));
        }
    }
}

