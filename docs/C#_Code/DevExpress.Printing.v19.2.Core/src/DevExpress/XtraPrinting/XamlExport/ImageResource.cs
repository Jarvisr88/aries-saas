namespace DevExpress.XtraPrinting.XamlExport
{
    using System;
    using System.Drawing;

    public class ImageResource : XamlResourceBase
    {
        private System.Drawing.Image image;

        public ImageResource(System.Drawing.Image image)
        {
            this.image = image;
        }

        public override bool Equals(object obj)
        {
            bool flag3;
            ImageResource resource = obj as ImageResource;
            if (resource == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (this.image == null)
            {
                return ReferenceEquals(resource.Image, null);
            }
            if (resource.Image == null)
            {
                return false;
            }
            System.Drawing.Image image = this.image;
            lock (image)
            {
                ImageResource resource2 = resource;
                lock (resource2)
                {
                    flag3 = (resource.Image.Size == this.image.Size) && ReferenceEquals(resource.Image, this.image);
                }
            }
            return flag3;
        }

        public override int GetHashCode() => 
            this.Image.GetHashCode();

        public System.Drawing.Image Image =>
            this.image;
    }
}

