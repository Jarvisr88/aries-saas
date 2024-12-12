namespace ActiproSoftware.MarkupLabel
{
    using #H;
    using System;
    using System.Drawing;

    public class MarkupLabelLineBreakUIElement : MarkupLabelUIElement
    {
        private float #Kte;

        public MarkupLabelLineBreakUIElement(MarkupLabelElement element) : base(element)
        {
        }

        protected override Size MeasureOverride(Graphics g, Size availableSize)
        {
            Size cachedSize = this.CachedSize;
            if (cachedSize.IsEmpty)
            {
                Font font = base.Element.GetFont();
                if (this.#Kte == 0f)
                {
                    this.#Kte = (font.GetHeight(g) / ((float) font.FontFamily.GetLineSpacing(font.Style))) * font.FontFamily.GetCellDescent(font.Style);
                }
                SizeF ef = g.MeasureString(#G.#eg(0x2eaf), font, 0x7fffffff, base.Element.MarkupLabel.StringFormat);
                base.CachedSize = new Size(0, (int) Math.Ceiling((double) ef.Height));
            }
            return base.CachedSize;
        }

        public override int Descent =>
            (int) Math.Round((double) this.#Kte);

        public override bool HardLineBreakBefore =>
            true;
    }
}

