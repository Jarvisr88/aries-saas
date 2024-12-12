namespace ActiproSoftware.MarkupLabel
{
    using System;

    public class MarkupLabelItalicElement : MarkupLabelElement
    {
        public MarkupLabelItalicElement() : base(#G.#eg(0x2bbb))
        {
        }

        public override MarkupLabelCssData GetDefaultCssData()
        {
            MarkupLabelCssData data1 = new MarkupLabelCssData();
            MarkupLabelCssData data2 = new MarkupLabelCssData();
            data2.FontStyle = MarkupLabelFontStyle.Italic;
            return data2;
        }
    }
}

