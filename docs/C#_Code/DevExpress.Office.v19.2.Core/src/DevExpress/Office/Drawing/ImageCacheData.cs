namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class ImageCacheData
    {
        public ImageCacheData()
        {
        }

        public ImageCacheData(System.Drawing.Image image, System.Drawing.Image grayedImage)
        {
            this.Image = image;
            this.GrayedImage = grayedImage;
        }

        public System.Drawing.Image GetImage(bool grayed) => 
            grayed ? this.GrayedImage : this.Image;

        public void SetImage(System.Drawing.Image image, bool grayed)
        {
            if (grayed)
            {
                this.GrayedImage = image;
            }
            else
            {
                this.Image = image;
            }
        }

        public System.Drawing.Image Image { get; set; }

        public System.Drawing.Image GrayedImage { get; set; }
    }
}

