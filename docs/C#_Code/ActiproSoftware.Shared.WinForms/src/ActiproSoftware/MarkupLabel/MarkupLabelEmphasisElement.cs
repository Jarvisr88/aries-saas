namespace ActiproSoftware.MarkupLabel
{
    using System;

    public class MarkupLabelEmphasisElement : MarkupLabelElement
    {
        public MarkupLabelEmphasisElement() : base(#G.#eg(0x2bb6))
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

