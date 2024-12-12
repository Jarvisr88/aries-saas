namespace ActiproSoftware.MarkupLabel
{
    using #H;
    using ActiproSoftware.Drawing;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class MarkupLabelImageUIElement : MarkupLabelUIElement
    {
        private float #Kte;
        private System.Drawing.Image #M0d;

        public MarkupLabelImageUIElement(MarkupLabelElement element, System.Drawing.Image image) : base(element)
        {
            this.#M0d = image;
        }

        protected override Size MeasureOverride(Graphics g, Size availableSize)
        {
            Size cachedSize = this.CachedSize;
            if (cachedSize.IsEmpty)
            {
                if (this.#Kte == 0f)
                {
                    Font font = base.Element.GetFont();
                    this.#Kte = (font.Size * font.FontFamily.GetCellDescent(font.Style)) / ((float) font.FontFamily.GetEmHeight(font.Style));
                }
                base.CachedSize = (this.#M0d == null) ? new Size(12, 12) : new Size(this.#M0d.Width, this.#M0d.Height);
            }
            return base.CachedSize;
        }

        protected override void OnRender(PaintEventArgs e)
        {
            if (this.#M0d != null)
            {
                DrawingHelper.DrawImage(e.Graphics, this.#M0d, base.Bounds.Left, base.Bounds.Top, 1f, RotateFlipType.RotateNoneFlipNone);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.White, base.Bounds);
                Pen pen = new Pen(Color.Red);
                e.Graphics.DrawRectangle(pen, base.Bounds.Left, base.Bounds.Top, base.Bounds.Width - 1, base.Bounds.Height - 1);
                e.Graphics.DrawLine(pen, base.Bounds.Left, base.Bounds.Top, base.Bounds.Right - 1, base.Bounds.Bottom - 1);
                e.Graphics.DrawLine(pen, base.Bounds.Left, base.Bounds.Bottom - 1, base.Bounds.Right - 1, base.Bounds.Top);
                pen.Dispose();
            }
        }

        public override int Descent =>
            (((MarkupLabelImageElement) this.Element).Align.Align != #G.#eg(0x2ea2)) ? 0 : ((int) Math.Round((double) this.#Kte));

        public System.Drawing.Image Image
        {
            get => 
                this.#M0d;
            set => 
                this.#M0d = value;
        }
    }
}

