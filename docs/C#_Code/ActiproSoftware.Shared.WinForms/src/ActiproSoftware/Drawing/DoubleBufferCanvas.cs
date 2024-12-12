namespace ActiproSoftware.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public class DoubleBufferCanvas : IDisposable, IDoubleBufferCanvas
    {
        private Bitmap #Vk;
        private Rectangle #4qe;
        private Control #lp;
        private System.Drawing.Graphics #5qe;
        private ImageAttributes #9qe;
        private System.Drawing.Graphics #are;

        public DoubleBufferCanvas(Control control)
        {
            this.#lp = control;
        }

        public void Copy(Rectangle sourceRect, Point destLocation)
        {
            if (this.#9qe == null)
            {
                this.#9qe = new ImageAttributes();
                ColorMatrix newColorMatrix = new ColorMatrix {
                    Matrix11 = 1f,
                    Matrix22 = 1f,
                    Matrix33 = 1f
                };
                this.#9qe.SetColorMatrix(newColorMatrix);
            }
            this.#5qe.DrawImage(this.#Vk, new Rectangle(destLocation.X, destLocation.Y, sourceRect.Width, sourceRect.Height), sourceRect.Left, sourceRect.Top, sourceRect.Width, sourceRect.Height, GraphicsUnit.Pixel, this.#9qe);
        }

        public void Dispose()
        {
            this.Reset();
            this.#lp = null;
        }

        public void Flush()
        {
            if ((this.#are != null) && (this.#Vk != null))
            {
                if ((this.#4qe.Width > 0) && (this.#4qe.Height > 0))
                {
                    this.#are.DrawImage(this.#Vk, this.#4qe.Left, this.#4qe.Top, this.#4qe, GraphicsUnit.Pixel);
                }
                this.#are = null;
            }
        }

        public void Invert(Rectangle bounds)
        {
            Bitmap image = new Bitmap(bounds.Width, bounds.Height);
            int x = 0;
            while (x < bounds.Width)
            {
                int y = 0;
                while (true)
                {
                    if (y >= bounds.Height)
                    {
                        x++;
                        break;
                    }
                    Color pixel = this.#Vk.GetPixel(bounds.Left + x, bounds.Top + y);
                    image.SetPixel(x, y, Color.FromArgb(0xff - pixel.R, 0xff - pixel.G, 0xff - pixel.B));
                    y++;
                }
            }
            this.#5qe.DrawImage(image, bounds.Left, bounds.Top, new Rectangle(0, 0, bounds.Width, bounds.Height), GraphicsUnit.Pixel);
            image.Dispose();
        }

        public void PrepareGraphics(PaintEventArgs e)
        {
            this.#4qe = e.ClipRectangle;
            this.#are = e.Graphics;
            if (this.#Vk == null)
            {
                this.#Vk = new Bitmap(Math.Max(1, this.#lp.ClientSize.Width), Math.Max(1, this.#lp.ClientSize.Height), this.#are);
                this.#5qe = System.Drawing.Graphics.FromImage(this.#Vk);
            }
            this.#5qe.Clip = e.Graphics.Clip;
        }

        public void Reset()
        {
            if (this.#9qe != null)
            {
                this.#9qe.Dispose();
                this.#9qe = null;
            }
            if (this.#5qe != null)
            {
                this.#5qe.Dispose();
                this.#5qe = null;
            }
            if (this.#Vk != null)
            {
                this.#Vk.Dispose();
                this.#Vk = null;
            }
        }

        public System.Drawing.Graphics Graphics =>
            this.#5qe;
    }
}

