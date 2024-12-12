namespace DevExpress.Office.Services.Implementation
{
    using DevExpress.Office.Services;
    using DevExpress.Office.Utils;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    [ComVisible(true)]
    public class FileBasedUriProvider : IUriProvider
    {
        private const string fileUriPrefix = "file:///";
        private string lastPath;
        private string lastName;
        private string lastExtension;
        private int lastFileIndex;

        public string CreateCssUri(string rootUri, string styleText, string relativeUri)
        {
            if (string.IsNullOrEmpty(styleText))
            {
                return string.Empty;
            }
            rootUri = Path.GetDirectoryName(rootUri);
            if (!this.EnsureDirectoryExists(rootUri))
            {
                return string.Empty;
            }
            string fileName = this.CreateNextFileName(rootUri, "style", "css");
            this.SaveCssProperties(fileName, styleText);
            return (string.IsNullOrEmpty(relativeUri) ? ("file:///" + fileName) : this.GetRelativeFileName(rootUri, relativeUri, fileName));
        }

        public virtual string CreateImageUri(string rootUri, OfficeImage image, string relativeUri)
        {
            if (image == null)
            {
                return string.Empty;
            }
            rootUri = Path.GetDirectoryName(rootUri);
            if (!this.EnsureDirectoryExists(rootUri))
            {
                return string.Empty;
            }
            string fileName = this.CreateNextFileName(rootUri, "image", this.GetImageFileExtension(image));
            if (this.TryToSaveImageInNativeFormat(image.NativeImage, fileName))
            {
                return (string.IsNullOrEmpty(relativeUri) ? ("file:///" + fileName) : this.GetRelativeFileName(rootUri, relativeUri, fileName));
            }
            fileName = this.CreateNextFileName(rootUri, "image", "png");
            return (!this.TryToSaveImageAsPng(image.NativeImage, fileName) ? string.Empty : (string.IsNullOrEmpty(relativeUri) ? ("file:///" + fileName) : this.GetRelativeFileName(rootUri, relativeUri, fileName)));
        }

        protected internal virtual string CreateNextFileName(string path, string name, string extension)
        {
            int num = this.GetInitialFileIndex(path, name, extension);
            while (true)
            {
                string str = $"{name}{num}.{extension}";
                str = Path.Combine(path, str);
                if (!File.Exists(str))
                {
                    this.lastFileIndex = num;
                    return str;
                }
                num++;
            }
        }

        protected internal virtual bool EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return Directory.Exists(path);
        }

        protected internal virtual string GetImageFileExtension(OfficeImage image)
        {
            string extension = OfficeImage.GetExtension(image.RawFormat);
            return (string.IsNullOrEmpty(extension) ? ((image.RawFormat != OfficeImageFormat.MemoryBmp) ? "img" : "png") : extension);
        }

        protected internal virtual int GetInitialFileIndex(string path, string name, string extension)
        {
            path = Path.GetFullPath(path);
            if ((string.Compare(path, this.lastPath, StringComparison.OrdinalIgnoreCase) == 0) && ((string.Compare(name, this.lastName, StringComparison.OrdinalIgnoreCase) == 0) && (string.Compare(extension, this.lastExtension, StringComparison.OrdinalIgnoreCase) == 0)))
            {
                return (this.lastFileIndex + 1);
            }
            this.lastPath = path;
            this.lastName = name;
            this.lastExtension = extension;
            this.lastFileIndex = 0;
            return this.lastFileIndex;
        }

        protected internal string GetRelativeFileName(string rootUri, string relativeUri, string fileName)
        {
            StringBuilder builder = new StringBuilder(fileName);
            try
            {
                if (rootUri.Length > 0)
                {
                    builder.Remove(0, rootUri.Length + 1);
                }
                builder.Insert(0, relativeUri);
            }
            catch
            {
                Exceptions.ThrowArgumentException("invalid relativeUri", relativeUri);
                return ("file:///" + fileName);
            }
            return builder.ToString();
        }

        protected internal void SaveCssProperties(string fileName, string styleText)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.ASCII))
                {
                    writer.Write(styleText);
                }
            }
        }

        protected internal virtual bool TryToSaveImageAsPng(Image image, string fileName)
        {
            try
            {
                ImageCodecInfo imageCodecInfo = OfficeImageWin.GetImageCodecInfo(OfficeImageHelper.GetImageFormat(OfficeImageFormat.Png));
                image.Save(fileName, imageCodecInfo, null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected internal virtual bool TryToSaveImageInNativeFormat(Image image, string fileName)
        {
            try
            {
                image.Save(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

