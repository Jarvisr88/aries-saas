namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.Utils;
    using DevExpress.Utils.Zip;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class CssImageRepository : ImageRepositoryBase, IImageRepository, IDisposable
    {
        private Dictionary<Image, long> imageKeyTable = new Dictionary<Image, long>();
        private Dictionary<long, string> keyBase64Table = new Dictionary<long, string>();
        private Dictionary<long, string> keyCssClassTable = new Dictionary<long, string>();
        private IScriptContainer scriptContainer;
        private System.Drawing.ImageConverter imageConverter;

        protected override void AddImageToTable(Image image, long key, string url, bool autoDisposeImage)
        {
            throw new NotSupportedException();
        }

        private void Clear()
        {
            this.imageKeyTable.Clear();
            this.keyCssClassTable.Clear();
            this.keyBase64Table.Clear();
        }

        protected override bool ContainsImageKey(long key) => 
            this.keyCssClassTable.ContainsKey(key);

        public override void Dispose()
        {
            this.ScriptContainer = null;
            this.ImageConverter = null;
        }

        protected override void FinalizeImage(Image image, bool autoDisposeImage)
        {
            if (autoDisposeImage)
            {
                image.Dispose();
            }
        }

        protected override string FormatImageURL(string imageFileName) => 
            string.Empty;

        private string GetBase64(Image image)
        {
            byte[] bytes = null;
            long key = this.GetKey(image, ref bytes);
            return this.GetBase64(key, image, bytes);
        }

        private string GetBase64(long key, Image image, byte[] bytes)
        {
            string str;
            if (!this.keyBase64Table.TryGetValue(key, out str))
            {
                bytes ??= this.ToBytes(image);
                str = Convert.ToBase64String(bytes);
                this.keyBase64Table.Add(key, str);
            }
            return str;
        }

        public string GetClassNameByImage(Image image)
        {
            byte[] bytes = null;
            string str;
            long key = this.GetKey(image, ref bytes);
            if (!this.keyCssClassTable.TryGetValue(key, out str) && (this.ScriptContainer != null))
            {
                string str2 = this.GetBase64(key, image, bytes);
                str = this.ScriptContainer.RegisterCssClass($"background-image:url(data:image/{HtmlImageHelper.GetMimeType(image)};base64,{str2});background-repeat:no-repeat;");
                this.keyCssClassTable.Add(key, str);
            }
            return str;
        }

        private static long GetHashCode(byte[] bytes) => 
            Adler32.Calculate(bytes);

        public string GetImageSource(Image image, bool autoDisposeImage)
        {
            base.RaiseRequestImageSource(image);
            return $"data:image/{HtmlImageHelper.GetMimeType(image)};base64,{this.GetBase64(image)}";
        }

        private long GetKey(Image image, ref byte[] bytes)
        {
            long hashCode;
            if (!this.imageKeyTable.TryGetValue(image, out hashCode))
            {
                bytes = this.ToBytes(image);
                hashCode = GetHashCode(bytes);
                this.imageKeyTable.Add(image, hashCode);
            }
            return hashCode;
        }

        public int GetStreamLength(Image image) => 
            this.GetBase64(image).Length;

        protected override string GetUrl(long key) => 
            string.Empty;

        public string GetWatermarkDataByImage(Image image)
        {
            byte[] bytes = null;
            long key = this.GetKey(image, ref bytes);
            return $"data:image/{HtmlImageHelper.GetMimeType(image)};base64,{this.GetBase64(key, image, bytes)}";
        }

        protected override void SaveImage(Image image, string fileName)
        {
            throw new NotSupportedException();
        }

        void IDisposable.Dispose()
        {
            this.Clear();
            this.Dispose();
        }

        private byte[] ToBytes(Image image) => 
            (byte[]) this.ImageConverter.ConvertTo(image, typeof(byte[]));

        public IScriptContainer ScriptContainer
        {
            get => 
                this.scriptContainer;
            set
            {
                this.Clear();
                this.scriptContainer = value;
            }
        }

        private System.Drawing.ImageConverter ImageConverter
        {
            get
            {
                this.imageConverter ??= new System.Drawing.ImageConverter();
                return this.imageConverter;
            }
            set => 
                this.imageConverter = value;
        }
    }
}

