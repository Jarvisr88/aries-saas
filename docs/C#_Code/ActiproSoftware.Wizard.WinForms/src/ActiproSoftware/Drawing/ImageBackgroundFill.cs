namespace ActiproSoftware.Drawing
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Drawing.Drawing2D;

    public class ImageBackgroundFill : BackgroundFill
    {
        private System.Drawing.Image #M0d;
        private ImageBackgroundFillStyle #JEd;

        public ImageBackgroundFill()
        {
            this.ResetImage();
            this.ResetStyle();
        }

        public ImageBackgroundFill(System.Drawing.Image image) : this()
        {
            this.#M0d = image;
        }

        public ImageBackgroundFill(System.Drawing.Image image, ImageBackgroundFillStyle style) : this(image)
        {
            this.#JEd = style;
        }

        public override BackgroundFill Clone()
        {
            ImageBackgroundFill fill1 = new ImageBackgroundFill();
            ImageBackgroundFill fill2 = new ImageBackgroundFill();
            fill2.Image = this.Image;
            ImageBackgroundFill local1 = fill2;
            ImageBackgroundFill local2 = fill2;
            local2.Style = this.Style;
            return local2;
        }

        public override void Draw(Graphics g, Rectangle bounds, Rectangle brushBounds, Sides side)
        {
            Draw(g, bounds, brushBounds, this.Image, this.Style);
        }

        public static void Draw(Graphics g, Rectangle bounds, Rectangle brushBounds, System.Drawing.Image image)
        {
            Draw(g, bounds, brushBounds, image, ImageBackgroundFillStyle.Tile);
        }

        public static void Draw(Graphics g, Rectangle bounds, Rectangle brushBounds, System.Drawing.Image image, ImageBackgroundFillStyle style)
        {
            if ((bounds.Width > 0) && ((bounds.Height > 0) && ((brushBounds.Width > 0) && (brushBounds.Height > 0))))
            {
                if (image == null)
                {
                    Pen pen = new Pen(Color.Red);
                    g.DrawRectangle(pen, bounds.Left, bounds.Top, bounds.Width - 1, bounds.Height - 1);
                    g.DrawLine(pen, bounds.Left, bounds.Top, bounds.Right - 1, bounds.Bottom - 1);
                    g.DrawLine(pen, bounds.Left, bounds.Bottom - 1, bounds.Right - 1, bounds.Top);
                    pen.Dispose();
                }
                else
                {
                    switch (style)
                    {
                        case ImageBackgroundFillStyle.Tile:
                            DrawingHelper.DrawWrappedImage(g, bounds, brushBounds, image, WrapMode.Tile);
                            return;

                        case ImageBackgroundFillStyle.TileFlipX:
                            DrawingHelper.DrawWrappedImage(g, bounds, brushBounds, image, WrapMode.TileFlipX);
                            return;

                        case ImageBackgroundFillStyle.TileFlipY:
                            DrawingHelper.DrawWrappedImage(g, bounds, brushBounds, image, WrapMode.TileFlipY);
                            return;

                        case ImageBackgroundFillStyle.TileFlipXY:
                            DrawingHelper.DrawWrappedImage(g, bounds, brushBounds, image, WrapMode.TileFlipXY);
                            return;

                        case ImageBackgroundFillStyle.Stretch:
                        {
                            if (bounds == brushBounds)
                            {
                                g.DrawImage(image, bounds);
                                return;
                            }
                            Bitmap bitmap = new Bitmap(image, brushBounds.Width, brushBounds.Height);
                            Graphics.FromImage(bitmap).Dispose();
                            g.DrawImage(bitmap, bounds, bounds.Left - brushBounds.Left, bounds.Top - brushBounds.Top, bounds.Width, bounds.Height, GraphicsUnit.Pixel);
                            bitmap.Dispose();
                            return;
                        }
                    }
                }
            }
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is ImageBackgroundFill))
            {
                return false;
            }
            ImageBackgroundFill fill = this;
            ImageBackgroundFill fill2 = (ImageBackgroundFill) obj;
            return (base.Equals(obj) && (ReferenceEquals(fill.Image, fill2.Image) && (fill.Style == fill2.Style)));
        }

        public override Brush GetBrush(Rectangle bounds, Sides side)
        {
            TextureBrush brush;
            if (this.#M0d == null)
            {
                return new SolidBrush(Color.White);
            }
            ImageBackgroundFillStyle style = this.#JEd;
            switch (style)
            {
                case ImageBackgroundFillStyle.TileFlipX:
                    brush = new TextureBrush(this.#M0d, WrapMode.TileFlipX);
                    break;

                case ImageBackgroundFillStyle.TileFlipY:
                    brush = new TextureBrush(this.#M0d, WrapMode.TileFlipY);
                    break;

                case ImageBackgroundFillStyle.TileFlipXY:
                    brush = new TextureBrush(this.#M0d, WrapMode.TileFlipXY);
                    break;

                default:
                    brush = new TextureBrush(this.#M0d, WrapMode.Tile);
                    break;
            }
            brush.TranslateTransform((float) bounds.Left, (float) bounds.Top);
            return brush;
        }

        public override int GetHashCode() => 
            this.GetHashCode();

        public virtual void ResetImage()
        {
            this.Image = null;
        }

        public virtual void ResetStyle()
        {
            this.Style = ImageBackgroundFillStyle.Tile;
        }

        public virtual bool ShouldSerializeImage() => 
            this.Image != null;

        public virtual bool ShouldSerializeStyle() => 
            this.Style != ImageBackgroundFillStyle.Tile;

        [Category("Appearance"), Description("The image to tile in the background."), Editor("ActiproSoftware.Drawing.Design.ImageEditor, ActiproSoftware.Shared.WinForms.Design, Version=20.1.402.0, Culture=neutral, PublicKeyToken=c27e062d3c1a4763", typeof(UITypeEditor)), RefreshProperties(RefreshProperties.All)]
        public System.Drawing.Image Image
        {
            get => 
                this.#M0d;
            set
            {
                if (!ReferenceEquals(this.#M0d, value))
                {
                    this.#M0d = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Appearance"), Description("The style of the image.")]
        public ImageBackgroundFillStyle Style
        {
            get => 
                this.#JEd;
            set
            {
                if (this.#JEd != value)
                {
                    this.#JEd = value;
                    this.OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
    }
}

