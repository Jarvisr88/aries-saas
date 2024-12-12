namespace DevExpress.Xpf.Core.HandleDecorator
{
    using System;
    using System.Drawing;

    public class ThemeElementImage
    {
        private System.Drawing.Image image;
        private PaddingEdges sizingMargins;
        private int imageCount;
        private ThemeImageLayout layout;
        private ThemeImageStretch stretch;

        public Rectangle GetImageBounds(int index)
        {
            if ((this.Image == null) || ((index < 0) || (index >= this.ImageCount)))
            {
                return Rectangle.Empty;
            }
            Size imageSize = this.GetImageSize();
            Rectangle rectangle = new Rectangle(Point.Empty, imageSize);
            if (this.ImageCount != 1)
            {
                if (this.Layout == ThemeImageLayout.Horizontal)
                {
                    rectangle.X = imageSize.Width * index;
                }
                else
                {
                    rectangle.Y = imageSize.Height * index;
                }
            }
            return rectangle;
        }

        internal unsafe Size GetImageSize()
        {
            if ((this.Image == null) || (this.ImageCount == 0))
            {
                return Size.Empty;
            }
            Size size = this.Image.Size;
            if (this.ImageCount != 1)
            {
                if (this.Layout == ThemeImageLayout.Horizontal)
                {
                    Size* sizePtr1 = &size;
                    sizePtr1.Width /= this.ImageCount;
                }
                else
                {
                    Size* sizePtr2 = &size;
                    sizePtr2.Height /= this.ImageCount;
                }
            }
            return size;
        }

        public System.Drawing.Image Image
        {
            get => 
                this.image;
            set => 
                this.image = value;
        }

        public PaddingEdges SizingMargins
        {
            get => 
                this.sizingMargins;
            set => 
                this.sizingMargins = value;
        }

        public int ImageCount
        {
            get => 
                this.imageCount;
            set => 
                this.imageCount = value;
        }

        public ThemeImageLayout Layout
        {
            get => 
                this.layout;
            set => 
                this.layout = value;
        }

        public ThemeImageStretch Stretch
        {
            get => 
                this.stretch;
            set => 
                this.stretch = value;
        }
    }
}

