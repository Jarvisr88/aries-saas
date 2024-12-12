namespace ActiproSoftware.MarkupLabel
{
    using ActiproSoftware.Drawing;
    using ActiproSoftware.WinUICore;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class MarkupLabelTextUIElement : MarkupLabelUIElement
    {
        private float #Kte;
        private string #Hd;

        public MarkupLabelTextUIElement(MarkupLabelElement element, string text) : base(element)
        {
            this.#Hd = text;
        }

        protected override Size MeasureOverride(Graphics g, Size availableSize)
        {
            Size cachedSize = this.CachedSize;
            if (cachedSize.IsEmpty)
            {
                Font font = base.Element.GetFont();
                if (this.#Kte == 0f)
                {
                    this.#Kte = (font.Size * font.FontFamily.GetCellDescent(font.Style)) / ((float) font.FontFamily.GetEmHeight(font.Style));
                }
                base.CachedSize = DrawingHelper.MeasureString(g, this.#Hd, font, base.Element.MarkupLabel.StringFormat);
            }
            return base.CachedSize;
        }

        protected override void OnRender(PaintEventArgs e)
        {
            Font font = this.Element.GetFont();
            Color backColor = base.Element.GetBackColor(base.GetDrawState());
            if (backColor != Color.Empty)
            {
                SolidBrush brush2 = new SolidBrush(backColor);
                e.Graphics.FillRectangle(brush2, base.Bounds);
                brush2.Dispose();
            }
            SolidBrush brush = new SolidBrush(base.Element.GetForeColor(base.GetDrawState()));
            DrawingHelper.DrawString(e.Graphics, this.#Hd, font, brush.Color, base.Bounds, base.Element.MarkupLabel.StringFormat);
            brush.Dispose();
        }

        public override Rectangle ClipBounds =>
            ((IUIElement) this.Parent).ClipBounds;

        public override int Descent =>
            (int) Math.Round((double) this.#Kte);

        public string Text =>
            this.#Hd;
    }
}

