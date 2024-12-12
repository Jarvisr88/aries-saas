namespace DevExpress.Office.Utils
{
    using DevExpress.Office.Model;
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.IO;

    public class MemoryStreamBasedImage
    {
        private readonly System.Drawing.Image image;
        private readonly MemoryStream imageStream;
        private readonly IUniqueImageId id;

        public MemoryStreamBasedImage(System.Drawing.Image image, MemoryStream imageStream) : this(image, imageStream, new NativeImageId(image))
        {
        }

        public MemoryStreamBasedImage(System.Drawing.Image image, MemoryStream imageStream, IUniqueImageId id)
        {
            Guard.ArgumentNotNull(id, "id");
            this.id = id;
            this.image = image;
            this.imageStream = imageStream;
        }

        public IUniqueImageId Id =>
            this.id;

        public System.Drawing.Image Image =>
            this.image;

        public MemoryStream ImageStream =>
            this.imageStream;
    }
}

