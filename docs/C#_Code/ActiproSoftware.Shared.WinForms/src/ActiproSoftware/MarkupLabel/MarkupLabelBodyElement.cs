namespace ActiproSoftware.MarkupLabel
{
    using #H;
    using ActiproSoftware.WinUICore;
    using System;
    using System.Drawing;

    public class MarkupLabelBodyElement : MarkupLabelElement
    {
        private ActiproSoftware.MarkupLabel.MarkupLabel #ste;

        public MarkupLabelBodyElement(ActiproSoftware.MarkupLabel.MarkupLabel markupLabel) : base(#G.#eg(0x2c1c))
        {
            this.#ste = markupLabel;
        }

        public override Color GetBackColor(UIElementDrawState drawState) => 
            Color.Empty;

        public override Font GetFont(string fontFamily, float fontSize, MarkupLabelFontWeight fontWeight, MarkupLabelFontStyle fontStyle, MarkupLabelTextDecoration textDecoration, UIElementDrawState drawState)
        {
            if ((fontFamily == null) || (fontFamily == #G.#eg(0x2c25)))
            {
                fontFamily = this.#ste.Font.FontFamily.Name;
            }
            if (fontSize == 0f)
            {
                fontSize = this.#ste.Font.SizeInPoints;
            }
            int num = this.#ste.#2we(fontFamily, fontSize, fontWeight == MarkupLabelFontWeight.Bold, fontStyle == MarkupLabelFontStyle.Italic, textDecoration == MarkupLabelTextDecoration.Underline, textDecoration == MarkupLabelTextDecoration.LineThrough);
            Font font = (Font) this.#ste.FontCache[num];
            if (font == null)
            {
                FontStyle regular = FontStyle.Regular;
                if (fontWeight == MarkupLabelFontWeight.Bold)
                {
                    regular |= FontStyle.Bold;
                }
                if (fontStyle == MarkupLabelFontStyle.Italic)
                {
                    regular |= FontStyle.Italic;
                }
                if (textDecoration == MarkupLabelTextDecoration.Underline)
                {
                    regular |= FontStyle.Underline;
                }
                else if (textDecoration == MarkupLabelTextDecoration.LineThrough)
                {
                    regular |= FontStyle.Strikeout;
                }
                try
                {
                    font = new Font(fontFamily, fontSize, regular, GraphicsUnit.Point);
                }
                catch
                {
                    font = new Font(#G.#eg(0x2c32), fontSize, regular, GraphicsUnit.Point);
                }
                this.#ste.FontCache[num] = font;
            }
            return font;
        }

        public override Color GetForeColor(UIElementDrawState drawState) => 
            this.#ste.ForeColor;

        public override ActiproSoftware.MarkupLabel.MarkupLabel MarkupLabel =>
            this.#ste;
    }
}

